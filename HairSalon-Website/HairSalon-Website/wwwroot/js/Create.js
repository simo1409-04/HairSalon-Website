
(() => {
    const calendarRoot = document.getElementById("booking-calendar");

    if (!calendarRoot) {
        return;
    }

    const slots = [
        "10:00",
        "10:50",
        "11:40",
        "12:30",
        "14:00",
        "14:50",
        "15:40",
        "16:30",
        "17:20",
        "18:10",
        "19:00"
    ];

    const nonWorkingWeekDays = new Set([1, 2]);

    const form = document.getElementById("reservation-form");
    const submitButton = document.getElementById("complete-reservation");
    const validationNotification = document.getElementById("form-validation-notification");
    const hairdresserSelect = document.getElementById("hairdresser-select");

    const selectedHairdresserInput = document.getElementById("selected-hairdresser-id");
    const selectedDateInput = document.getElementById("selected-reservation-date");
    const selectedTimeInput = document.getElementById("selected-reservation-time");

    const calendarStep = document.getElementById("calendar-step");
    const detailsStep = document.getElementById("details-step");

    const monthTitle = document.getElementById("calendar-month-title");
    const calendarDays = document.getElementById("calendar-days");
    const previousMonthButton = document.getElementById("previous-month");
    const nextMonthButton = document.getElementById("next-month");

    const timeSlots = document.getElementById("time-slots");
    const selectedDateLabel = document.getElementById("selected-date-label");
    const calendarStatus = document.getElementById("calendar-status");

    const continueButton = document.getElementById("continue-to-details");
    const backButton = document.getElementById("back-to-calendar");
    const changeBookingButton = document.getElementById("change-booking");

    const calendarSelectionError = document.getElementById("calendar-selection-error");
    const bookingSummary = document.getElementById("booking-summary");

    const totalPriceElement = document.getElementById("total-price");
    const servicesError = document.getElementById("services-error");
    const serviceCheckboxes = document.querySelectorAll("input[name='serviceIds']");

    const today = new Date();
    today.setHours(0, 0, 0, 0);

    const currentMonth = new Date(today.getFullYear(), today.getMonth(), 1);
    let displayedMonth = new Date(today.getFullYear(), today.getMonth(), 1);

    let selectedDate = "";
    let selectedTime = "";
    let busySlotsByDate = {};
    let validationNotificationTimer;
    let validationNotificationHideTimer;

    const restoreSelection = window.bookingRestoreSelection ?? null;
    let shouldRestoreSelection = Boolean(restoreSelection);

    if (shouldRestoreSelection) {
        hairdresserSelect.value = String(restoreSelection.hairdresserId);
    }

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
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    });

    function pad(value) {
        return String(value).padStart(2, "0");
    }

    function toDateKey(year, month, day) {
        return `${year}-${pad(month + 1)}-${pad(day)}`;
    }

    function dateFromKey(dateKey) {
        const [year, month, day] = dateKey.split("-").map(Number);

        return new Date(year, month - 1, day);
    }

    function isNonWorkingDay(dateKey) {
        return nonWorkingWeekDays.has(dateFromKey(dateKey).getDay());
    }

    function normalizeTime(value) {
        return String(value).slice(0, 5);
    }

    function isPastDate(year, month, day) {
        const date = new Date(year, month, day);
        date.setHours(0, 0, 0, 0);

        return date < today;
    }

    function getBusyTimes(dateKey) {
        return busySlotsByDate[dateKey] ?? [];
    }

    function getAvailableSlots(dateKey) {
        if (isNonWorkingDay(dateKey)) {
            return [];
        }

        const busyTimes = getBusyTimes(dateKey);

        return slots.filter(slot =>
            !busyTimes.includes(slot) &&
            !isPastSlot(dateKey, slot)
        );
    }

    function setCalendarStatus(message) {
        calendarStatus.textContent = message;
    }

    function updateContinueButton() {
        continueButton.disabled = !(selectedDate && selectedTime);
    }

    function renderCalendar() {
        const year = displayedMonth.getFullYear();
        const month = displayedMonth.getMonth();

        monthTitle.textContent = monthFormatter.format(displayedMonth);
        calendarDays.innerHTML = "";

        previousMonthButton.disabled = displayedMonth <= currentMonth;

        const firstDay = new Date(year, month, 1);
        const firstDayIndex = (firstDay.getDay() + 6) % 7;
        const daysInMonth = new Date(year, month + 1, 0).getDate();

        for (let index = 0; index < firstDayIndex; index++) {
            const emptyCell = document.createElement("span");
            emptyCell.className = "calendar-day--empty";
            emptyCell.setAttribute("aria-hidden", "true");

            calendarDays.appendChild(emptyCell);
        }

        for (let day = 1; day <= daysInMonth; day++) {
            const dateKey = toDateKey(year, month, day);
            const isPast = isPastDate(year, month, day);
            const isNonWorking = isNonWorkingDay(dateKey);
            const availableSlots = isPast ? [] : getAvailableSlots(dateKey);

            const dayButton = document.createElement("button");
            dayButton.type = "button";
            dayButton.className = "calendar-day";
            dayButton.textContent = day;

            if (isPast) {
                dayButton.classList.add("calendar-day--past");
                dayButton.disabled = true;
                dayButton.setAttribute("aria-label", `${day}. ${month + 1}. ${year} – минала дата`);
            } else if (isNonWorking) {
                dayButton.classList.add("calendar-day--past");
                dayButton.disabled = true;
                dayButton.title = "Почивен ден";
                dayButton.setAttribute("aria-label", `${day}. ${month + 1}. ${year} – почивен ден`);
            } else if (availableSlots.length === 0) {
                dayButton.classList.add("calendar-day--full");
                dayButton.disabled = true;
                dayButton.setAttribute("aria-label", `${day}. ${month + 1}. ${year} – няма свободни часове`);
            } else {
                dayButton.classList.add("calendar-day--available");
                dayButton.setAttribute(
                    "aria-label",
                    `${day}. ${month + 1}. ${year} – ${availableSlots.length} свободни часа`
                );

                dayButton.addEventListener("click", () => {
                    selectedDate = dateKey;
                    selectedTime = "";

                    selectedDateInput.value = selectedDate;
                    selectedTimeInput.value = "";

                    calendarSelectionError.hidden = true;

                    renderCalendar();
                    renderTimeSlots();
                    updateContinueButton();
                });
            }

            if (selectedDate === dateKey) {
                dayButton.classList.add("calendar-day--selected");
                dayButton.setAttribute("aria-pressed", "true");
            } else {
                dayButton.setAttribute("aria-pressed", "false");
            }

            calendarDays.appendChild(dayButton);
        }
    }

    function renderTimeSlots() {
        timeSlots.innerHTML = "";

        if (!selectedDate) {
            selectedDateLabel.textContent = "След това ще видиш свободните часове.";
            return;
        }

        const selectedDateObject = dateFromKey(selectedDate);
        selectedDateLabel.textContent = dateFormatter.format(selectedDateObject);

        const availableSlots = getAvailableSlots(selectedDate);

        if (availableSlots.length === 0) {
            timeSlots.innerHTML = `
                <p class="time-slots__empty">
                    За този ден вече няма свободни часове.
                </p>`;

            return;
        }

        availableSlots.forEach(slot => {
            const slotButton = document.createElement("button");
            slotButton.type = "button";
            slotButton.className = "time-slot";
            slotButton.textContent = slot;

            if (slot === selectedTime) {
                slotButton.classList.add("time-slot--selected");
                slotButton.setAttribute("aria-pressed", "true");
            } else {
                slotButton.setAttribute("aria-pressed", "false");
            }

            slotButton.addEventListener("click", () => {
                selectedTime = slot;
                selectedTimeInput.value = slot;

                calendarSelectionError.hidden = true;

                renderTimeSlots();
                updateContinueButton();
            });

            timeSlots.appendChild(slotButton);
        });
    }

    function normalizeBusySlots(payload) {
        const normalized = {};

        if (Array.isArray(payload)) {
            payload.forEach(item => {
                const date = item.reservationDate ?? item.ReservationDate;
                const time = item.reservationTime ?? item.ReservationTime;

                if (!date || !time) {
                    return;
                }

                if (!normalized[date]) {
                    normalized[date] = [];
                }

                normalized[date].push(normalizeTime(time));
            });

            return normalized;
        }

        Object.entries(payload ?? {}).forEach(([date, times]) => {
            normalized[date] = (times ?? []).map(normalizeTime);
        });

        return normalized;
    }

    async function loadBusySlots() {
        const hairdresserId = hairdresserSelect.value;

        selectedHairdresserInput.value = hairdresserId;
        selectedDate = "";
        selectedTime = "";

        selectedDateInput.value = "";
        selectedTimeInput.value = "";

        timeSlots.innerHTML = "";
        selectedDateLabel.textContent = "След това ще видиш свободните часове.";

        calendarSelectionError.hidden = true;
        continueButton.disabled = true;

        if (!hairdresserId) {
            busySlotsByDate = {};
            renderCalendar();
            setCalendarStatus("Избери фризьор, за да видиш графика.");
            return;
        }

        setCalendarStatus("Зареждаме актуалния график...");

        try {
            const url = new URL(
                calendarRoot.dataset.busySlotsUrl,
                window.location.origin
            );

            url.searchParams.set("id", hairdresserId);

            const response = await fetch(url, {
                headers: {
                    Accept: "application/json"
                }
            });

            if (!response.ok) {
                throw new Error("Неуспешно зареждане на графика.");
            }

            const payload = await response.json();

            busySlotsByDate = normalizeBusySlots(payload);

            renderCalendar();
            setCalendarStatus("Избери свободен ден и удобен час.");

            // Добавена логика:
            // Само при върнат модел, след нормалното AJAX зареждане.
            if (
                shouldRestoreSelection &&
                hairdresserId === String(restoreSelection.hairdresserId) &&
                hairdresserSelect.value === hairdresserId
            ) {
                shouldRestoreSelection = false;

                const restoredDate = restoreSelection.date;
                const restoredTime = normalizeTime(restoreSelection.time);
                const parsedDate = dateFromKey(restoredDate);

                if (
                    !Number.isNaN(parsedDate.getTime()) &&
                    parsedDate >= today &&
                    getAvailableSlots(restoredDate).includes(restoredTime)
                ) {
                    displayedMonth = new Date(
                        parsedDate.getFullYear(),
                        parsedDate.getMonth(),
                        1
                    );

                    selectedDate = restoredDate;
                    selectedTime = restoredTime;

                    selectedDateInput.value = restoredDate;
                    selectedTimeInput.value = restoredTime;

                    renderCalendar();
                    renderTimeSlots();
                    updateContinueButton();

                    showDetailsStep();
                }
            }
        } catch {
            busySlotsByDate = {};

            renderCalendar();
            setCalendarStatus("Графикът не може да бъде зареден. Опитай отново.");
        }
    }

    function updateTotalPrice() {
        let total = 0;

        serviceCheckboxes.forEach(checkbox => {
            if (checkbox.checked) {
                total += Number(checkbox.dataset.price ?? 0);
            }
        });

        totalPriceElement.textContent = `${priceFormatter.format(total)} eur.`;
    }

    function showDetailsStep() {
        if (!selectedDate || !selectedTime || !hairdresserSelect.value) {
            calendarSelectionError.textContent = "Моля, избери фризьор, дата и свободен час.";
            calendarSelectionError.hidden = false;

            return;
        }

        const hairdresserName =
            hairdresserSelect.options[hairdresserSelect.selectedIndex].text;

        const date = dateFormatter.format(dateFromKey(selectedDate));

        bookingSummary.textContent =
            `${hairdresserName} · ${date} · ${selectedTime} – ${getEndTime(selectedTime)}`;

        calendarStep.hidden = true;
        detailsStep.hidden = false;

        detailsStep.scrollIntoView({
            behavior: "smooth",
            block: "start"
        });
    }

    function getEndTime(startTime) {
        const [hour, minute] = startTime.split(":").map(Number);
        const end = new Date(2000, 0, 1, hour, minute + 45);

        return `${pad(end.getHours())}:${pad(end.getMinutes())}`;
    }

    function showCalendarStep() {
        detailsStep.hidden = true;
        calendarStep.hidden = false;

        calendarStep.scrollIntoView({
            behavior: "smooth",
            block: "start"
        });
    }

    hairdresserSelect.addEventListener("change", loadBusySlots);

    previousMonthButton.addEventListener("click", () => {
        displayedMonth = new Date(
            displayedMonth.getFullYear(),
            displayedMonth.getMonth() - 1,
            1
        );

        renderCalendar();
    });

    nextMonthButton.addEventListener("click", () => {
        displayedMonth = new Date(
            displayedMonth.getFullYear(),
            displayedMonth.getMonth() + 1,
            1
        );

        renderCalendar();
    });

    continueButton.addEventListener("click", showDetailsStep);
    backButton.addEventListener("click", showCalendarStep);
    changeBookingButton.addEventListener("click", showCalendarStep);

    serviceCheckboxes.forEach(checkbox => {
        checkbox.addEventListener("change", () => {
            servicesError.textContent = "";
            updateTotalPrice();
        });
    });

    submitButton.addEventListener("click", () => {
        const validator = window.jQuery && window.jQuery.validator
            ? window.jQuery(form).validate()
            : null;

        const isFormValid = validator
            ? validator.form()
            : form.checkValidity();

        if (isFormValid) {
            return;
        }

        clearTimeout(validationNotificationTimer);
        clearTimeout(validationNotificationHideTimer);

        validationNotification.hidden = false;

        requestAnimationFrame(() => {
            validationNotification.classList.add(
                "form-validation-notification--visible"
            );
        });

        validationNotificationTimer = setTimeout(() => {
            validationNotification.classList.remove(
                "form-validation-notification--visible"
            );

            validationNotificationHideTimer = setTimeout(() => {
                validationNotification.hidden = true;
            }, 350);
        }, 10000);
    });

    form.addEventListener("submit", event => {
        const hasService = [...serviceCheckboxes].some(
            checkbox => checkbox.checked
        );

        if (!selectedDate || !selectedTime || !hairdresserSelect.value) {
            event.preventDefault();

            showCalendarStep();

            calendarSelectionError.textContent =
                "Моля, избери фризьор, дата и свободен час.";

            calendarSelectionError.hidden = false;

            return;
        }

        if (!hasService) {
            event.preventDefault();

            servicesError.textContent = "Моля, избери поне една услуга.";
        }
    });

    loadBusySlots();
})();

function isPastSlot(dateKey, slot) {
    const [year, month, day] = dateKey.split("-").map(Number);
    const [hour, minute] = slot.split(":").map(Number);

    const slotDateTime = new Date(year, month - 1, day, hour, minute);
    const now = new Date();

    return slotDateTime <= now;
}
