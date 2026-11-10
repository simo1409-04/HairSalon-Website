(() => {
    const form = document.getElementById("reservation-form");
    const calendarRoot = document.getElementById("booking-calendar");

    if (!form || !calendarRoot) return;

    const isCreate = form.dataset.bookingMode === "create";
    const slots = [
        "10:00", "10:50", "11:40", "12:30", "14:00", "14:50",
        "15:40", "16:30", "17:20", "18:10", "19:00"
    ];

    const hairdresserSelect = document.getElementById("hairdresser-select");
    const hairdresserStep = document.getElementById("hairdresser-step");
    const hairdresserChoices = document.querySelectorAll("input[name='hairdresserChoice']");
    const hairdresserChoiceStatus = document.getElementById("hairdresser-choice-status");
    const hairdresserInput = document.getElementById("selected-hairdresser-id");
    const dateInput = document.getElementById("selected-reservation-date");
    const timeInput = document.getElementById("selected-reservation-time");
    const calendarStep = document.getElementById("calendar-step");
    const detailsStep = document.getElementById("details-step");
    const confirmationStep = document.getElementById("confirmation-step");
    const heading = document.getElementById("reservation-heading");
    const monthTitle = document.getElementById("calendar-month-title");
    const calendarDays = document.getElementById("calendar-days");
    const previousMonthButton = document.getElementById("previous-month");
    const nextMonthButton = document.getElementById("next-month");
    const timeSlots = document.getElementById("time-slots");
    const dateLabel = document.getElementById("selected-date-label");
    const calendarStatus = document.getElementById("calendar-status");
    const continueButton = document.getElementById("continue-to-details");
    const selectionError = document.getElementById("calendar-selection-error");
    const servicesError = document.getElementById("services-error");
    const serviceCheckboxes = document.querySelectorAll("input[name='serviceIds']");
    const totalPrice = document.getElementById("total-price");
    const notification = document.getElementById("form-validation-notification");

    const today = new Date();
    today.setHours(0, 0, 0, 0);

    let displayedMonth = new Date(today.getFullYear(), today.getMonth(), 1);
    let selectedDate = "";
    let selectedTime = "";
    let busySlots = {};
    let confirmationReady = false;
    let notificationTimer;
    let hideNotificationTimer;

    // Скритите полета пазят избора при редакция и след неуспешно изпращане.
    const savedSelection = {
        hairdresserId: hairdresserInput.value,
        date: dateInput.value.slice(0, 10),
        time: timeInput.value.slice(0, 5)
    };
    const originalEditSelection = isCreate ? null : savedSelection;
    let restoreAfterLoad = null;

    if ([...hairdresserSelect.options].some(option => option.value === savedSelection.hairdresserId)) {
        hairdresserSelect.value = savedSelection.hairdresserId;
        restoreAfterLoad = savedSelection;
    }

    const monthFormat = new Intl.DateTimeFormat("bg-BG", {
        month: "long", year: "numeric"
    });
    const dateFormat = new Intl.DateTimeFormat("bg-BG", {
        weekday: "long", day: "numeric", month: "long", year: "numeric"
    });
    const priceFormat = new Intl.NumberFormat("bg-BG", {
        minimumFractionDigits: 2, maximumFractionDigits: 2
    });

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

    function isClosedDay(date) {
        return date.getDay() === 1 || date.getDay() === 2;
    }

    function isPastSlot(key, time) {
        const [hour, minute] = time.split(":").map(Number);
        const slotDate = dateFromKey(key);
        slotDate.setHours(hour, minute);
        return slotDate <= new Date();
    }

    function availableSlots(key) {
        if (isClosedDay(dateFromKey(key))) return [];

        return slots.filter(time => {
            if (isPastSlot(key, time)) return false;
            if (!busySlots[key]?.includes(time)) return true;

            // При редакция текущият час на резервацията остава достъпен.
            return originalEditSelection !== null &&
                hairdresserSelect.value === originalEditSelection.hairdresserId &&
                key === originalEditSelection.date &&
                time === originalEditSelection.time;
        });
    }

    // Календар и свободни часове.
    function chooseDate(key) {
        selectedDate = key;
        selectedTime = "";
        dateInput.value = key;
        timeInput.value = "";
        selectionError.hidden = true;
        renderCalendar();
        renderTimeSlots();
        continueButton.disabled = true;
    }

    function chooseTime(time) {
        selectedTime = time;
        timeInput.value = time;
        selectionError.hidden = true;
        renderTimeSlots();
        continueButton.disabled = false;
    }

    function renderCalendar() {
        const year = displayedMonth.getFullYear();
        const month = displayedMonth.getMonth();
        const firstWeekday = (new Date(year, month, 1).getDay() + 6) % 7;
        const daysInMonth = new Date(year, month + 1, 0).getDate();

        monthTitle.textContent = monthFormat.format(displayedMonth);
        previousMonthButton.disabled =
            displayedMonth <= new Date(today.getFullYear(), today.getMonth(), 1);
        calendarDays.replaceChildren();

        for (let index = 0; index < firstWeekday; index++) {
            const empty = document.createElement("span");
            empty.className = "calendar-day--empty";
            empty.setAttribute("aria-hidden", "true");
            calendarDays.appendChild(empty);
        }

        for (let day = 1; day <= daysInMonth; day++) {
            const date = new Date(year, month, day);
            const key = dateKey(date);
            const freeTimes = availableSlots(key);
            const button = document.createElement("button");
            button.type = "button";
            button.className = "calendar-day";
            button.textContent = day;

            let description;
            if (date < today) {
                button.classList.add("calendar-day--past");
                description = "минала дата";
                button.disabled = true;
            } else if (isClosedDay(date)) {
                button.classList.add("calendar-day--past");
                button.title = "Почивен ден";
                description = "почивен ден";
                button.disabled = true;
            } else if (freeTimes.length === 0) {
                button.classList.add("calendar-day--full");
                description = "няма свободни часове";
                button.disabled = true;
            } else {
                button.classList.add("calendar-day--available");
                description = `${freeTimes.length} свободни часа`;
                button.addEventListener("click", () => chooseDate(key));
            }

            button.setAttribute("aria-label", `${day}. ${month + 1}. ${year} – ${description}`);
            button.setAttribute("aria-pressed", String(selectedDate === key));
            if (selectedDate === key) button.classList.add("calendar-day--selected");
            calendarDays.appendChild(button);
        }
    }

    function renderTimeSlots() {
        timeSlots.replaceChildren();

        if (!selectedDate) {
            dateLabel.textContent = "След това ще видиш свободните часове.";
            return;
        }

        dateLabel.textContent = dateFormat.format(dateFromKey(selectedDate));
        const freeTimes = availableSlots(selectedDate);

        if (freeTimes.length === 0) {
            const message = document.createElement("p");
            message.className = "time-slots__empty";
            message.textContent = "За този ден вече няма свободни часове.";
            timeSlots.appendChild(message);
            return;
        }

        for (const time of freeTimes) {
            const button = document.createElement("button");
            button.type = "button";
            button.className = "time-slot";
            button.textContent = time;
            button.setAttribute("aria-pressed", String(selectedTime === time));
            if (selectedTime === time) button.classList.add("time-slot--selected");
            button.addEventListener("click", () => chooseTime(time));
            timeSlots.appendChild(button);
        }
    }

    function restoreSelection(saved) {
        if (!/^\d{4}-\d{2}-\d{2}$/.test(saved.date)) return;
        const date = dateFromKey(saved.date);
        if (dateKey(date) !== saved.date || date < today || availableSlots(saved.date).length === 0) return;

        displayedMonth = new Date(date.getFullYear(), date.getMonth(), 1);
        selectedDate = saved.date;
        selectedTime = availableSlots(saved.date).includes(saved.time) ? saved.time : "";
        dateInput.value = selectedDate;
        timeInput.value = selectedTime;
        continueButton.disabled = !selectedTime;
        renderCalendar();
        renderTimeSlots();

        if (isCreate && form.dataset.serverErrors === "true" && selectedTime) {
            showDetails();
        }
    }

    async function loadBusySlots() {
        const hairdresserId = hairdresserSelect.value;
        hairdresserInput.value = hairdresserId;
        selectedDate = "";
        selectedTime = "";
        dateInput.value = "";
        timeInput.value = "";
        continueButton.disabled = true;
        selectionError.hidden = true;
        calendarDays.replaceChildren();
        renderTimeSlots();

        if (!hairdresserId) {
            busySlots = {};
            renderCalendar();
            calendarStatus.textContent = "Избери фризьор, за да видиш графика.";
            return false;
        }

        calendarStatus.textContent = "Зареждаме актуалния график...";

        try {
            const url = new URL(calendarRoot.dataset.busySlotsUrl, window.location.origin);
            url.searchParams.set("id", hairdresserId);
            const response = await fetch(url, { headers: { Accept: "application/json" } });
            if (!response.ok) throw new Error("Неуспешно зареждане на графика.");

            const data = await response.json();
            if (hairdresserSelect.value !== hairdresserId) return false;

            busySlots = {};
            for (const [date, times] of Object.entries(data)) {
                busySlots[date] = times.map(time => time.slice(0, 5));
            }

            renderCalendar();
            calendarStatus.textContent = "Избери свободен ден и удобен час.";

            if (restoreAfterLoad?.hairdresserId === hairdresserId) {
                const saved = restoreAfterLoad;
                restoreAfterLoad = null;
                restoreSelection(saved);
            }
            return true;
        } catch {
            if (hairdresserSelect.value !== hairdresserId) return false;
            busySlots = {};
            renderCalendar();
            calendarStatus.textContent = "Графикът не може да бъде зареден. Опитай отново.";
            return false;
        }
    }

    // Преминаване между календара, данните и потвърждението.
    function endTime(start) {
        const [hour, minute] = start.split(":").map(Number);
        const end = new Date(2000, 0, 1, hour, minute + 45);
        return `${pad(end.getHours())}:${pad(end.getMinutes())}`;
    }

    function hasValidSelection() {
        return hairdresserSelect.value && selectedDate && selectedTime &&
            availableSlots(selectedDate).includes(selectedTime);
    }

    function showSelectionError() {
        if (hairdresserStep && !hairdresserSelect.value) {
            hairdresserStep.hidden = false;
            calendarStep.hidden = true;
            hairdresserChoiceStatus.textContent = "Моля, избери фризьор.";
            hairdresserStep.scrollIntoView({ behavior: "smooth", block: "start" });
            return;
        }
        showCalendar();
        selectionError.textContent = "Моля, избери фризьор, дата и свободен час.";
        selectionError.hidden = false;
    }

    function leaveConfirmation() {
        if (!isCreate) return;
        confirmationReady = false;
        confirmationStep.hidden = true;
        heading.hidden = false;
        form.classList.remove("reservation-form--confirmation");
    }

    function showCalendar() {
        leaveConfirmation();
        detailsStep.hidden = true;
        if (hairdresserStep) hairdresserStep.hidden = true;
        calendarStep.hidden = false;
        calendarStep.scrollIntoView({ behavior: "smooth", block: "start" });
    }

    function showDetails() {
        if (!hasValidSelection()) {
            showSelectionError();
            return;
        }

        leaveConfirmation();
        const hairdresserName = hairdresserSelect.selectedOptions[0].textContent.trim();
        const date = dateFormat.format(dateFromKey(selectedDate));
        document.getElementById("booking-summary").textContent =
            `${hairdresserName} · ${date} · ${selectedTime} – ${endTime(selectedTime)}`;

        calendarStep.hidden = true;
        if (hairdresserStep) hairdresserStep.hidden = true;
        detailsStep.hidden = false;
        detailsStep.scrollIntoView({ behavior: "smooth", block: "start" });
    }

    function selectedServices() {
        return [...serviceCheckboxes].filter(checkbox => checkbox.checked);
    }

    function updateTotalPrice() {
        let total = 0;
        for (const checkbox of selectedServices()) {
            total += Number(checkbox.dataset.price);
        }
        totalPrice.textContent = `${priceFormat.format(total)} eur.`;
    }

    function fieldsAreValid() {
        if (window.jQuery?.validator) {
            return window.jQuery(form).validate().form();
        }
        return form.checkValidity();
    }

    function showValidationNotification() {
        clearTimeout(notificationTimer);
        clearTimeout(hideNotificationTimer);
        notification.hidden = false;
        requestAnimationFrame(() => {
            notification.classList.add("form-validation-notification--visible");
        });
        notificationTimer = setTimeout(() => {
            notification.classList.remove("form-validation-notification--visible");
            hideNotificationTimer = setTimeout(() => {
                notification.hidden = true;
            }, 350);
        }, 10000);
    }

    function showConfirmation() {
        if (!hasValidSelection()) {
            showSelectionError();
            return;
        }

        const services = selectedServices();
        const validFields = fieldsAreValid();
        servicesError.textContent = services.length ? "" : "Моля, избери поне една услуга.";
        if (!validFields) showValidationNotification();
        if (!validFields || services.length === 0) return;

        document.getElementById("confirmation-name").textContent =
            form.elements.namedItem("ClientName").value.trim();
        document.getElementById("confirmation-phone").textContent =
            form.elements.namedItem("ClientPhone").value.trim();
        document.getElementById("confirmation-hairdresser").textContent =
            hairdresserSelect.selectedOptions[0].textContent.trim();
        document.getElementById("confirmation-date").textContent =
            dateFormat.format(dateFromKey(selectedDate));
        document.getElementById("confirmation-time").textContent =
            `${selectedTime} – ${endTime(selectedTime)}`;

        const servicesList = document.getElementById("confirmation-services");
        servicesList.replaceChildren();
        for (const checkbox of services) {
            const item = document.createElement("li");
            const name = document.createElement("span");
            const price = document.createElement("strong");
            name.textContent = checkbox.nextElementSibling
                .querySelector(".service-option__name").textContent.trim();
            price.textContent = `${priceFormat.format(Number(checkbox.dataset.price))} eur.`;
            item.append(name, price);
            servicesList.appendChild(item);
        }
        document.getElementById("confirmation-total").textContent = totalPrice.textContent;

        const serverError = form.querySelector(".reservation-validation-summary");
        if (serverError) serverError.hidden = true;

        confirmationReady = true;
        calendarStep.hidden = true;
        detailsStep.hidden = true;
        heading.hidden = true;
        form.classList.add("reservation-form--confirmation");
        confirmationStep.hidden = false;
        confirmationStep.scrollIntoView({ behavior: "smooth", block: "start" });
    }

    // Действия на потребителя.
    if (hairdresserStep) {
        for (const choice of hairdresserChoices) {
            choice.addEventListener("click", async () => {
                hairdresserSelect.value = choice.value;
                restoreAfterLoad = null;
                hairdresserChoiceStatus.textContent = "Зареждаме свободните часове...";

                const loaded = await loadBusySlots();
                if (!loaded) {
                    if (hairdresserSelect.value === choice.value) {
                        hairdresserChoiceStatus.textContent = calendarStatus.textContent;
                    }
                    return;
                }

                hairdresserChoiceStatus.textContent = "";
                hairdresserStep.hidden = true;
                calendarStep.hidden = false;
                calendarStep.scrollIntoView({ behavior: "smooth", block: "start" });
            });
        }

        document.getElementById("change-hairdresser").addEventListener("click", () => {
            calendarStep.hidden = true;
            hairdresserStep.hidden = false;
            hairdresserStep.scrollIntoView({ behavior: "smooth", block: "start" });
        });

        for (const image of hairdresserStep.querySelectorAll(".hairdresser-choice__photo img")) {
            image.addEventListener("error", () => { image.hidden = true; });
            if (image.complete && image.naturalWidth === 0) image.hidden = true;
        }
    } else {
        hairdresserSelect.addEventListener("change", () => {
            restoreAfterLoad = null;
            loadBusySlots();
        });
    }

    previousMonthButton.addEventListener("click", () => {
        displayedMonth = new Date(displayedMonth.getFullYear(), displayedMonth.getMonth() - 1, 1);
        renderCalendar();
    });
    nextMonthButton.addEventListener("click", () => {
        displayedMonth = new Date(displayedMonth.getFullYear(), displayedMonth.getMonth() + 1, 1);
        renderCalendar();
    });

    continueButton.addEventListener("click", showDetails);
    document.getElementById("back-to-calendar").addEventListener("click", showCalendar);
    document.getElementById("change-booking").addEventListener("click", showCalendar);

    for (const checkbox of serviceCheckboxes) {
        checkbox.addEventListener("change", () => {
            servicesError.textContent = "";
            updateTotalPrice();
        });
    }

    if (isCreate) {
        document.getElementById("complete-reservation")
            .addEventListener("click", showConfirmation);
        document.getElementById("back-to-details")
            .addEventListener("click", showDetails);
    } else {
        document.getElementById("complete-reservation").addEventListener("click", () => {
            if (!fieldsAreValid()) showValidationNotification();
        });
    }

    form.addEventListener("submit", event => {
        if (isCreate && !confirmationReady) {
            event.preventDefault();
            showConfirmation();
            return;
        }

        if (!hasValidSelection()) {
            event.preventDefault();
            showSelectionError();
            return;
        }

        if (selectedServices().length === 0) {
            event.preventDefault();
            if (isCreate) showDetails();
            servicesError.textContent = "Моля, избери поне една услуга.";
            return;
        }

        if (!fieldsAreValid()) {
            event.preventDefault();
            if (isCreate) showDetails();
            showValidationNotification();
        }
    });

    updateTotalPrice();
    if (hairdresserStep) {
        calendarStep.hidden = true;
        if (hairdresserSelect.value) {
            hairdresserChoiceStatus.textContent = "Зареждаме свободните часове...";
            loadBusySlots().then(loaded => {
                if (!loaded) {
                    hairdresserChoiceStatus.textContent = calendarStatus.textContent;
                    return;
                }
                hairdresserChoiceStatus.textContent = "";
                hairdresserStep.hidden = true;
                if (detailsStep.hidden) calendarStep.hidden = false;
            });
        }
    } else {
        loadBusySlots();
    }
})();
