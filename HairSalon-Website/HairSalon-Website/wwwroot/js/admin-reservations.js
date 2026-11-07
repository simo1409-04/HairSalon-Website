(() => {
    const schedule = document.getElementById("team-schedule");
    if (!schedule) return;

    const people = Array.from(schedule.querySelectorAll(".schedule-person"), element => ({
        element,
        id: Number(element.dataset.hairdresserId),
        name: element.dataset.personName,
        counts: JSON.parse(element.dataset.counts || "{}")
    }));
    if (people.length === 0) return;

    const monthTitle = schedule.querySelector("#schedule-month");
    const sheet = schedule.querySelector("#day-sheet");
    const sheetPerson = schedule.querySelector("#day-sheet-person");
    const sheetTitle = schedule.querySelector("#day-sheet-title");
    const sheetCount = schedule.querySelector("#day-sheet-count");
    const sheetList = schedule.querySelector("#day-sheet-list");

    const monthFormat = new Intl.DateTimeFormat("bg-BG", {
        month: "long",
        year: "numeric"
    });
    const dateFormat = new Intl.DateTimeFormat("bg-BG", {
        weekday: "long",
        day: "numeric",
        month: "long",
        year: "numeric"
    });
    const priceFormat = new Intl.NumberFormat("bg-BG", {
        style: "currency",
        currency: "EUR"
    });

    const today = new Date();
    const todayKey = getDateKey(today);
    let shownMonth = new Date(today.getFullYear(), today.getMonth(), 1);
    let savedScroll = 0;
    let currentRequest = 0;
    let touchPress = null;
    let ignoreClickUntil = 0;

    function getDateKey(date) {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, "0");
        const day = String(date.getDate()).padStart(2, "0");
        return `${year}-${month}-${day}`;
    }

    function getDate(key) {
        const [year, month, day] = key.split("-").map(Number);
        return new Date(year, month - 1, day);
    }

    function getCount(person, key) {
        return Number(person.counts[key] || 0);
    }

    function countText(count) {
        return count === 1 ? "1 резервация" : `${count} резервации`;
    }

    function colorFor(count) {
        if (count === 0) return "zero";
        if (count <= 3) return "low";
        if (count <= 6) return "medium";
        if (count <= 9) return "high";
        return "full";
    }

    function makeElement(tag, className, text) {
        const element = document.createElement(tag);
        element.className = className;
        if (text !== undefined) element.textContent = text;
        return element;
    }

    function urlWithId(base, id) {
        const url = new URL(base, window.location.href);
        url.pathname = `${url.pathname.replace(/\/$/, "")}/${encodeURIComponent(id)}`;
        return url.toString();
    }

    function makeBookingCard(reservation) {
        const card = makeElement("article", "booking-card");
        const heading = makeElement("div", "booking-card__heading");
        heading.append(
            makeElement("time", "", String(reservation.reservationTime).slice(0, 5)),
            makeElement("strong", "", reservation.clientName)
        );
        card.append(heading);

        if (reservation.clientPhone) {
            card.append(makeElement(
                "p", "booking-card__phone", `Телефон: ${reservation.clientPhone}`
            ));
        }

        if (Array.isArray(reservation.services) && reservation.services.length > 0) {
            const services = makeElement("ul", "booking-card__services");
            for (const service of reservation.services) {
                const item = document.createElement("li");
                item.append(
                    makeElement("span", "", service.name),
                    makeElement("span", "", priceFormat.format(service.price))
                );
                services.append(item);
            }
            card.append(services);
        }

        const total = makeElement("div", "booking-card__total");
        total.append(
            makeElement("span", "", "Общо"),
            makeElement("strong", "", priceFormat.format(reservation.totalPrice))
        );
        card.append(total);

        const actions = makeElement("form", "booking-card__actions");
        actions.method = "post";
        actions.action = urlWithId(schedule.dataset.deleteUrl, reservation.id);

        const editLink = makeElement("a", "", "Редактирай");
        editLink.href = urlWithId(schedule.dataset.editUrl, reservation.id);

        const deleteButton = makeElement("button", "", "Изтрий");
        deleteButton.type = "submit";

        actions.addEventListener("submit", event => {
            if (!window.confirm("Сигурни ли сте, че искате да изтриете тази резервация?")) {
                event.preventDefault();
            }
        });
        actions.append(editLink, deleteButton);
        card.append(actions);
        return card;
    }

    async function loadReservations(person, key, request) {
        try {
            const url = new URL(urlWithId(schedule.dataset.reservationsUrl, person.id));
            url.searchParams.set("date", key);

            const response = await fetch(url, {
                headers: { Accept: "application/json" },
                credentials: "same-origin"
            });
            if (!response.ok) throw new Error("Неуспешна заявка");

            const reservations = await response.json();
            if (!Array.isArray(reservations)) throw new Error("Невалиден отговор");

            // Стар отговор не бива да променя вече затворен панел.
            if (request !== currentRequest || sheet.hidden) return;

            reservations.sort((a, b) =>
                String(a.reservationTime).localeCompare(String(b.reservationTime))
            );
            sheetCount.textContent = countText(reservations.length);
            sheetList.replaceChildren();

            if (reservations.length === 0) {
                sheetList.append(makeElement(
                    "p", "day-sheet__empty", "Няма резервации за този ден."
                ));
            } else {
                for (const reservation of reservations) {
                    sheetList.append(makeBookingCard(reservation));
                }
            }
        } catch {
            if (request !== currentRequest || sheet.hidden) return;
            sheetList.replaceChildren(makeElement(
                "p", "day-sheet__empty", "Не успях да заредя резервациите. Опитай отново."
            ));
        }
    }

    function openDay(person, key, preview = false) {
        if (!sheet.hidden) return;

        savedScroll = window.scrollY;
        currentRequest++;
        sheetPerson.textContent = person.name;
        sheetTitle.textContent = dateFormat.format(getDate(key));
        sheetCount.textContent = countText(getCount(person, key));
        sheetList.replaceChildren(makeElement("p", "day-sheet__empty", "Зареждане…"));

        sheet.hidden = false;
        schedule.classList.add("team-schedule--details-open");
        schedule.classList.toggle("team-schedule--preview", preview);
        window.scrollTo({ top: 0, behavior: "auto" });

        loadReservations(person, key, currentRequest);
    }

    function closeDay() {
        if (sheet.hidden) return;
        currentRequest++;
        sheet.hidden = true;
        schedule.classList.remove("team-schedule--details-open", "team-schedule--preview");
        window.scrollTo({ top: savedScroll, behavior: "auto" });
    }

    function connectDay(button, person, key) {
        button.addEventListener("click", event => {
            // На телефон pointerup вече е отворил деня; следващият click е излишен.
            if (event.detail !== 0 && event.pointerType !== "mouse" &&
                Date.now() < ignoreClickUntil) return;
            openDay(person, key);
        });

        button.addEventListener("pointerdown", event => {
            if (event.pointerType === "mouse" || touchPress) return;

            touchPress = {
                id: event.pointerId,
                x: event.clientX,
                y: event.clientY,
                person,
                key,
                moved: false,
                held: false,
                timer: null
            };

            touchPress.timer = window.setTimeout(() => {
                if (!touchPress || touchPress.id !== event.pointerId ||
                    touchPress.moved || !sheet.hidden) return;

                touchPress.held = true;
                openDay(person, key, true);
            }, 480);

            try {
                button.setPointerCapture(event.pointerId);
            } catch {
                // Докосването може вече да е приключило.
            }
        });

        button.addEventListener("contextmenu", event => event.preventDefault());
    }

    document.addEventListener("pointermove", event => {
        if (!touchPress || touchPress.id !== event.pointerId || touchPress.held) return;

        const distance = Math.hypot(
            event.clientX - touchPress.x,
            event.clientY - touchPress.y
        );
        if (distance > 12) {
            touchPress.moved = true;
            window.clearTimeout(touchPress.timer);
        }
    });

    function finishTouch(event, cancelled) {
        if (!touchPress || touchPress.id !== event.pointerId) return;

        const press = touchPress;
        touchPress = null;
        window.clearTimeout(press.timer);
        ignoreClickUntil = Date.now() + 700;

        if (press.held) {
            closeDay();
        } else if (!cancelled && !press.moved) {
            openDay(press.person, press.key);
        }
    }

    document.addEventListener("pointerup", event => finishTouch(event, false));
    document.addEventListener("pointercancel", event => finishTouch(event, true));

    function makeDay(person, year, month, dayNumber) {
        const date = new Date(year, month, dayNumber);
        const key = getDateKey(date);
        const count = getCount(person, key);
        const closed = date.getDay() === 1 || date.getDay() === 2;
        const past = key < todayKey;
        const hasPastReservations = past && count > 0;

        const button = makeElement("button", `schedule-day schedule-day--${colorFor(count)}`);
        button.type = "button";
        button.dataset.date = key;
        button.setAttribute("aria-label",
            `${dateFormat.format(date)}, ${countText(count)}` +
            (closed ? ", почивен ден" : "") +
            (past ? ", минал ден" : "")
        );
        button.append(
            makeElement("span", "schedule-day__number", String(dayNumber)),
            makeElement("span", "schedule-day__count", String(count))
        );

        if (key === todayKey) button.classList.add("schedule-day--today");
        if (closed || past) {
            button.classList.add("schedule-day--inactive");
        }

        if (hasPastReservations || (!closed && !past)) {
            connectDay(button, person, key);
        } else {
            button.disabled = true;
        }
        return button;
    }

    function renderMonth() {
        const year = shownMonth.getFullYear();
        const month = shownMonth.getMonth();
        const daysInMonth = new Date(year, month + 1, 0).getDate();
        const blanks = (new Date(year, month, 1).getDay() + 6) % 7;

        monthTitle.textContent = monthFormat.format(shownMonth);
        for (const person of people) {
            const grid = person.element.querySelector(".schedule-days");
            grid.replaceChildren();

            for (let i = 0; i < blanks; i++) {
                const blank = makeElement("span", "schedule-day--placeholder");
                blank.setAttribute("aria-hidden", "true");
                grid.append(blank);
            }
            for (let day = 1; day <= daysInMonth; day++) {
                grid.append(makeDay(person, year, month, day));
            }

            person.element.querySelector("[data-person-today]").textContent =
                getCount(person, todayKey);
        }

        const totalToday = people.reduce((total, person) =>
            total + getCount(person, todayKey), 0
        );
        schedule.querySelector("#schedule-today-total").textContent = totalToday;
    }

    schedule.querySelector("#schedule-previous").addEventListener("click", () => {
        shownMonth = new Date(shownMonth.getFullYear(), shownMonth.getMonth() - 1, 1);
        renderMonth();
    });
    schedule.querySelector("#schedule-next").addEventListener("click", () => {
        shownMonth = new Date(shownMonth.getFullYear(), shownMonth.getMonth() + 1, 1);
        renderMonth();
    });
    schedule.querySelector("#schedule-today").addEventListener("click", () => {
        shownMonth = new Date(today.getFullYear(), today.getMonth(), 1);
        renderMonth();
    });
    schedule.querySelector("#day-sheet-close").addEventListener("click", closeDay);
    document.addEventListener("keydown", event => {
        if (event.key === "Escape") closeDay();
    });

    renderMonth();
})();
