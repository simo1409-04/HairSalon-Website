(() => {
    const root = document.getElementById("team-schedule");
    if (!root) return;

    const people = [...root.querySelectorAll(".schedule-person")].map(element => ({
        element,
        id: Number(element.dataset.hairdresserId),
        name: element.dataset.personName,
        counts: JSON.parse(element.dataset.counts || "{}")
    }));

    if (people.length === 0) return;

    const monthTitle = root.querySelector("#schedule-month");
    const sheet = root.querySelector("#day-sheet");
    const sheetPerson = root.querySelector("#day-sheet-person");
    const sheetTitle = root.querySelector("#day-sheet-title");
    const sheetCount = root.querySelector("#day-sheet-count");
    const sheetList = root.querySelector("#day-sheet-list");

    const monthFormatter = new Intl.DateTimeFormat("bg-BG", {
        month: "long",
        year: "numeric"
    });

    const dateFormatter = new Intl.DateTimeFormat("bg-BG", {
        weekday: "long",
        day: "numeric",
        month: "long",
        year: "numeric"
    });

    const priceFormatter = new Intl.NumberFormat("bg-BG", {
        style: "currency",
        currency: "EUR"
    });

    const today = new Date();
    const todayKey = dateKey(today);

    let displayedMonth = new Date(today.getFullYear(), today.getMonth(), 1);
    let calendarScrollY = 0;
    let detailMode = null;
    let activePress = null;
    let suppressTouchClickUntil = 0;
    let requestVersion = 0;

    function pad(number) {
        return String(number).padStart(2, "0");
    }

    function dateKey(date) {
        return `${date.getFullYear()}-${pad(date.getMonth() + 1)}-${pad(date.getDate())}`;
    }

    function dateFromKey(key) {
        const [year, month, day] = key.split("-").map(Number);
        return new Date(year, month - 1, day);
    }

    function countFor(person, key) {
        return Number(person.counts[key] ?? 0);
    }

    function level(count) {
        if (count === 0) return "zero";
        if (count <= 3) return "low";
        if (count <= 6) return "medium";
        if (count <= 9) return "high";
        return "full";
    }

    function reservationText(count) {
        return count === 1 ? "1 резервация" : `${count} резервации`;
    }

    function makeElement(tag, className, text) {
        const element = document.createElement(tag);
        element.className = className;

        if (text !== undefined) {
            element.textContent = String(text);
        }

        return element;
    }

    function actionUrl(baseUrl, reservationId) {
        const url = new URL(baseUrl, window.location.href);
        url.pathname =
            `${url.pathname.replace(/\/$/, "")}/${encodeURIComponent(reservationId)}`;
        return url.toString();
    }

    function createBookingCard(reservation) {
        const card = makeElement("article", "booking-card");

        const heading = makeElement("div", "booking-card__heading");
        const time = makeElement(
            "time",
            "",
            String(reservation.reservationTime).slice(0, 5)
        );
        const client = makeElement(
            "strong",
            "",
            reservation.clientName
        );

        heading.append(time, client);
        card.appendChild(heading);

        if (reservation.clientPhone) {
            card.appendChild(
                makeElement(
                    "p",
                    "booking-card__phone",
                    `Телефон: ${reservation.clientPhone}`
                )
            );
        }

        if (Array.isArray(reservation.services) &&
            reservation.services.length > 0) {

            const services = makeElement("ul", "booking-card__services");

            reservation.services.forEach(service => {
                const item = document.createElement("li");
                const name = makeElement("span", "", service.name);
                const price = makeElement(
                    "span",
                    "",
                    priceFormatter.format(service.price)
                );

                item.append(name, price);
                services.appendChild(item);
            });

            card.appendChild(services);
        }

        const total = makeElement("div", "booking-card__total");
        total.append(
            makeElement("span", "", "Общо"),
            makeElement(
                "strong",
                "",
                priceFormatter.format(reservation.totalPrice)
            )
        );
        card.appendChild(total);

        const actions = makeElement("div", "booking-card__actions");

        const edit = makeElement("a", "", "Редактирай");
        edit.href = actionUrl(root.dataset.editUrl, reservation.id);

        const remove = makeElement("a", "", "Изтрий");
        remove.href = actionUrl(root.dataset.deleteUrl, reservation.id);

        actions.append(edit, remove);
        card.appendChild(actions);

        return card;
    }

    async function getReservations(person, key) {
        const url = new URL(
            root.dataset.reservationsUrl,
            window.location.href
        );

        url.pathname =
            `${url.pathname.replace(/\/$/, "")}/${encodeURIComponent(person.id)}`;
        url.searchParams.set("date", key);

        const response = await fetch(url, {
            headers: { Accept: "application/json" },
            credentials: "same-origin"
        });

        if (!response.ok) {
            throw new Error(`HTTP ${response.status}`);
        }

        if (!(response.headers.get("content-type") || "")
            .includes("application/json")) {
            throw new Error("Отговорът не е JSON.");
        }

        const reservations = await response.json();

        if (!Array.isArray(reservations)) {
            throw new Error("Невалиден списък с резервации.");
        }

        return reservations;
    }

    function showDetails(person, key, preview = false) {
        if (detailMode !== null) return;

        const thisRequest = ++requestVersion;

        calendarScrollY = window.scrollY;
        detailMode = preview ? "preview" : "persistent";

        sheetPerson.textContent = person.name;
        sheetTitle.textContent = dateFormatter.format(dateFromKey(key));
        sheetCount.textContent = reservationText(countFor(person, key));

        sheetList.replaceChildren(
            makeElement("p", "day-sheet__empty", "Зареждане…")
        );

        sheet.hidden = false;
        root.classList.add("team-schedule--details-open");
        root.classList.toggle("team-schedule--preview", preview);
        window.scrollTo({ top: 0, behavior: "auto" });

        getReservations(person, key)
            .then(reservations => {
                // Ако пръстът вече е отпуснат, не отваряй панела отново.
                if (thisRequest !== requestVersion ||
                    detailMode === null) return;

                reservations.sort((a, b) =>
                    String(a.reservationTime)
                        .localeCompare(String(b.reservationTime))
                );

                sheetCount.textContent =
                    reservationText(reservations.length);
                sheetList.replaceChildren();

                if (reservations.length === 0) {
                    sheetList.appendChild(
                        makeElement(
                            "p",
                            "day-sheet__empty",
                            "Няма резервации за този ден."
                        )
                    );
                    return;
                }

                reservations.forEach(reservation => {
                    sheetList.appendChild(
                        createBookingCard(reservation)
                    );
                });
            })
            .catch(() => {
                if (thisRequest !== requestVersion ||
                    detailMode === null) return;

                sheetList.replaceChildren(
                    makeElement(
                        "p",
                        "day-sheet__empty",
                        "Не успях да заредя резервациите. Опитай отново."
                    )
                );
            });
    }

    function closeDetails() {
        if (detailMode === null) return;

        ++requestVersion;
        detailMode = null;

        root.classList.remove("team-schedule--details-open");
        root.classList.remove("team-schedule--preview");
        sheet.hidden = true;

        window.scrollTo({
            top: calendarScrollY,
            behavior: "auto"
        });
    }

    function connectDay(button, person, key) {
        button.addEventListener("pointerdown", event => {
            if (event.pointerType === "mouse" || activePress) return;

            activePress = {
                id: event.pointerId,
                x: event.clientX,
                y: event.clientY,
                person,
                key,
                moved: false,
                long: false,
                timer: null
            };

            activePress.timer = window.setTimeout(() => {
                if (!activePress ||
                    activePress.id !== event.pointerId ||
                    activePress.moved ||
                    detailMode !== null) return;

                activePress.long = true;
                showDetails(person, key, true);
            }, 480);

            try {
                button.setPointerCapture(event.pointerId);
            } catch {
                // Браузърът може вече да е отменил докосването.
            }
        });

        button.addEventListener("click", event => {
            if (event.detail !== 0 &&
                event.pointerType !== "mouse" &&
                Date.now() < suppressTouchClickUntil) return;

            showDetails(person, key);
        });

        button.addEventListener("contextmenu", event => {
            event.preventDefault();
        });
    }

    document.addEventListener("pointermove", event => {
        if (!activePress ||
            activePress.id !== event.pointerId ||
            activePress.long) return;

        const movement = Math.hypot(
            event.clientX - activePress.x,
            event.clientY - activePress.y
        );

        if (movement > 12) {
            activePress.moved = true;
            window.clearTimeout(activePress.timer);
        }
    });

    document.addEventListener("pointerup", event => {
        if (!activePress ||
            activePress.id !== event.pointerId) return;

        const press = activePress;
        activePress = null;

        window.clearTimeout(press.timer);
        suppressTouchClickUntil = Date.now() + 700;

        if (press.long) {
            closeDetails();
        } else if (!press.moved) {
            showDetails(press.person, press.key);
        }
    });

    document.addEventListener("pointercancel", event => {
        if (!activePress ||
            activePress.id !== event.pointerId) return;

        const press = activePress;
        activePress = null;

        window.clearTimeout(press.timer);
        suppressTouchClickUntil = Date.now() + 700;

        if (press.long) closeDetails();
    });

    function createDay(person, year, month, day) {
        const date = new Date(year, month, day);
        const key = dateKey(date);
        const count = countFor(person, key);

        const isClosed = date.getDay() === 1 || date.getDay() === 2;
        const isPast = key < todayKey;
        const isInactive = isPast || isClosed;

        const button = document.createElement("button");
        button.type = "button";
        button.className =
            `schedule-day schedule-day--${level(count)}`;
        button.dataset.date = key;

        if (isInactive) {
            button.classList.add("schedule-day--inactive");
            button.disabled = true;
        }

        if (key === todayKey) {
            button.classList.add("schedule-day--today");
        }

        button.setAttribute(
            "aria-label",
            `${dateFormatter.format(date)}, ${reservationText(count)}` +
            `${isClosed ? ", почивен ден" : ""}` +
            `${isPast ? ", минал ден" : ""}`
        );

        const number = makeElement(
            "span",
            "schedule-day__number",
            day
        );
        const badge = makeElement(
            "span",
            "schedule-day__count",
            count
        );

        button.append(number, badge);

        if (!isInactive) {
            connectDay(button, person, key);
        }

        return button;
    }

    function renderMonth() {
        const year = displayedMonth.getFullYear();
        const month = displayedMonth.getMonth();
        const daysInMonth =
            new Date(year, month + 1, 0).getDate();
        const firstWeekday =
            (new Date(year, month, 1).getDay() + 6) % 7;

        monthTitle.textContent =
            monthFormatter.format(displayedMonth);

        people.forEach(person => {
            const grid =
                person.element.querySelector(".schedule-days");
            grid.replaceChildren();

            for (let i = 0; i < firstWeekday; i++) {
                const placeholder =
                    makeElement("span", "schedule-day--placeholder");
                placeholder.setAttribute("aria-hidden", "true");
                grid.appendChild(placeholder);
            }

            for (let day = 1; day <= daysInMonth; day++) {
                grid.appendChild(
                    createDay(person, year, month, day)
                );
            }

            person.element
                .querySelector("[data-person-today]")
                .textContent = countFor(person, todayKey);
        });

        root.querySelector("#schedule-today-total").textContent =
            people.reduce(
                (sum, person) =>
                    sum + countFor(person, todayKey),
                0
            );
    }

    root.querySelector("#schedule-previous")
        .addEventListener("click", () => {
            displayedMonth = new Date(
                displayedMonth.getFullYear(),
                displayedMonth.getMonth() - 1,
                1
            );
            renderMonth();
        });

    root.querySelector("#schedule-next")
        .addEventListener("click", () => {
            displayedMonth = new Date(
                displayedMonth.getFullYear(),
                displayedMonth.getMonth() + 1,
                1
            );
            renderMonth();
        });

    root.querySelector("#schedule-today")
        .addEventListener("click", () => {
            displayedMonth = new Date(
                today.getFullYear(),
                today.getMonth(),
                1
            );
            renderMonth();
        });

    root.querySelector("#day-sheet-close")
        .addEventListener("click", closeDetails);

    document.addEventListener("keydown", event => {
        if (event.key === "Escape") {
            closeDetails();
        }
    });

    renderMonth();
})();