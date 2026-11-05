(() => {
    const root = document.getElementById("team-schedule");
    if (!root) return;

    const people = [...root.querySelectorAll(".schedule-person")].map(element => {
        const byDate = new Map();
        const template = element.querySelector(".schedule-reservations");

        template.content.querySelectorAll(".booking-card").forEach(booking => {
            const date = booking.dataset.date;
            const list = byDate.get(date) ?? [];

            list.push(booking);
            byDate.set(date, list);
        });

        for (const list of byDate.values()) {
            list.sort((a, b) => a.dataset.time.localeCompare(b.dataset.time));
        }

        return {
            element,
            name: element.dataset.personName,
            byDate
        };
    });

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

    const today = new Date();
    let displayedMonth = new Date(today.getFullYear(), today.getMonth(), 1);
    let calendarScrollY = 0;
    let detailMode = null;
    let activePress = null;
    let suppressTouchClickUntil = 0;

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

    const todayKey = dateKey(today);

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

    function showDetails(person, key, preview = false) {
        if (detailMode !== null) return;

        const reservations = person.byDate.get(key) ?? [];

        sheetPerson.textContent = person.name;
        sheetTitle.textContent = dateFormatter.format(dateFromKey(key));
        sheetCount.textContent = reservationText(reservations.length);
        sheetList.replaceChildren();

        if (reservations.length === 0) {
            const empty = document.createElement("p");
            empty.className = "day-sheet__empty";
            empty.textContent = "Няма резервации за този ден.";
            sheetList.appendChild(empty);
        } else {
            reservations.forEach(reservation => {
                sheetList.appendChild(reservation.cloneNode(true));
            });
        }

        calendarScrollY = window.scrollY;
        detailMode = preview ? "preview" : "persistent";
        sheet.hidden = false;
        root.classList.add("team-schedule--details-open");
        root.classList.toggle("team-schedule--preview", preview);
        window.scrollTo({ top: 0, behavior: "auto" });
    }

    function closeDetails() {
        if (detailMode === null) return;

        root.classList.remove("team-schedule--details-open");
        root.classList.remove("team-schedule--preview");
        sheet.hidden = true;
        detailMode = null;
        window.scrollTo({ top: calendarScrollY, behavior: "auto" });
    }

    function connectDay(button, person, key) {
        button.addEventListener("pointerdown", event => {
            if (event.pointerType === "mouse") return;

            activePress = {
                id: event.pointerId,
                x: event.clientX,
                y: event.clientY,
                person,
                key,
                moved: false,
                long: false,
                timer: window.setTimeout(() => {
                    if (!activePress || activePress.id !== event.pointerId || activePress.moved || detailMode !== null) return;
                    activePress.long = true;
                    showDetails(person, key, true);
                }, 480)
            };

            try {
                button.setPointerCapture(event.pointerId);
            } catch {
                // Докосването може вече да е отменено от браузъра.
            }
        });

        button.addEventListener("click", event => {
            if (event.detail !== 0 && event.pointerType !== "mouse" && Date.now() < suppressTouchClickUntil) return;
            showDetails(person, key);
        });

        button.addEventListener("contextmenu", event => {
            event.preventDefault();
        });
    }

    document.addEventListener("pointermove", event => {
        if (!activePress || activePress.id !== event.pointerId || activePress.long) return;

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
        if (!activePress || activePress.id !== event.pointerId) return;

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
        if (!activePress || activePress.id !== event.pointerId) return;

        const press = activePress;
        activePress = null;
        window.clearTimeout(press.timer);
        suppressTouchClickUntil = Date.now() + 700;
        if (press.long) closeDetails();
    });

    function createDay(person, year, month, day) {
        const date = new Date(year, month, day);
        const key = dateKey(date);
        const count = (person.byDate.get(key) ?? []).length;
        const isClosed = date.getDay() === 1 || date.getDay() === 2;
        const isPast = key < todayKey;
        const isInactive = isPast || isClosed;

        const button = document.createElement("button");
        button.type = "button";
        button.className = `schedule-day schedule-day--${level(count)}`;
        button.dataset.date = key;

        if (isInactive) {
            button.classList.add("schedule-day--inactive");
            button.disabled = true;
        }
        if (key === todayKey) button.classList.add("schedule-day--today");

        button.setAttribute(
            "aria-label",
            `${dateFormatter.format(date)}, ${reservationText(count)}${isClosed ? ", почивен ден" : ""}${isPast ? ", минал ден" : ""}`
        );

        const number = document.createElement("span");
        number.className = "schedule-day__number";
        number.textContent = day;

        const badge = document.createElement("span");
        badge.className = "schedule-day__count";
        badge.textContent = count;

        button.append(number, badge);
        if (!isInactive) connectDay(button, person, key);

        return button;
    }

    function renderMonth() {
        const year = displayedMonth.getFullYear();
        const month = displayedMonth.getMonth();
        const daysInMonth = new Date(year, month + 1, 0).getDate();
        const firstWeekday = (new Date(year, month, 1).getDay() + 6) % 7;

        monthTitle.textContent = monthFormatter.format(displayedMonth);

        people.forEach(person => {
            const grid = person.element.querySelector(".schedule-days");
            grid.replaceChildren();

            for (let i = 0; i < firstWeekday; i++) {
                const placeholder = document.createElement("span");
                placeholder.className = "schedule-day--placeholder";
                placeholder.setAttribute("aria-hidden", "true");
                grid.appendChild(placeholder);
            }

            for (let day = 1; day <= daysInMonth; day++) {
                grid.appendChild(createDay(person, year, month, day));
            }

            const todayCount = (person.byDate.get(todayKey) ?? []).length;
            person.element.querySelector("[data-person-today]").textContent =
                todayCount;
        });

        const totalToday = people.reduce(
            (sum, person) => sum + (person.byDate.get(todayKey) ?? []).length,
            0
        );

        root.querySelector("#schedule-today-total").textContent = totalToday;
    }

    root.querySelector("#schedule-previous").addEventListener("click", () => {
        displayedMonth = new Date(
            displayedMonth.getFullYear(),
            displayedMonth.getMonth() - 1,
            1
        );

        closeDetails();
        renderMonth();
    });

    root.querySelector("#schedule-next").addEventListener("click", () => {
        displayedMonth = new Date(
            displayedMonth.getFullYear(),
            displayedMonth.getMonth() + 1,
            1
        );

        closeDetails();
        renderMonth();
    });

    root.querySelector("#schedule-today").addEventListener("click", () => {
        displayedMonth = new Date(today.getFullYear(), today.getMonth(), 1);

        closeDetails();
        renderMonth();
    });

    root.querySelector("#day-sheet-close").addEventListener(
        "click",
        closeDetails
    );

    document.addEventListener("keydown", event => {
        if (event.key === "Escape" && !sheet.hidden) {
            closeDetails();
        }
    });

    renderMonth();
})();
