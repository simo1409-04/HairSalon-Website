
document.addEventListener("DOMContentLoaded", () => {

    const buttons = Array.from(
        document.querySelectorAll("[data-gallery-open]")
    );

    const lightbox = document.getElementById("gb-lightbox");
    const image = document.getElementById("gb-lightbox-image");
    const counter = document.getElementById("gb-lightbox-counter");

    const previous = document.getElementById("gb-lightbox-prev");
    const next = document.getElementById("gb-lightbox-next");

    if (!lightbox || buttons.length === 0) return;

    const urls = buttons.map(button => button.dataset.imageUrl);

    let currentIndex = 0;
    let lastFocusedElement = null;

    function showImage(index) {

        currentIndex = (index + urls.length) % urls.length;

        image.src = urls[currentIndex];

        counter.textContent =
            `${currentIndex + 1} / ${urls.length}`;
    }

    function openLightbox(index) {

        lastFocusedElement = document.activeElement;

        showImage(index);

        lightbox.hidden = false;

        document.body.classList.add("gb-lightbox-open");

        lightbox.querySelector(".gb-lightbox__close").focus();
    }

    function closeLightbox() {

        lightbox.hidden = true;

        document.body.classList.remove("gb-lightbox-open");

        image.removeAttribute("src");

        lastFocusedElement?.focus();
    }

    buttons.forEach((button, index) => {

        button.addEventListener("click", () => {
            openLightbox(index);
        });

    });

    previous.addEventListener("click", () => {
        showImage(currentIndex - 1);
    });

    next.addEventListener("click", () => {
        showImage(currentIndex + 1);
    });

    lightbox.querySelectorAll("[data-lightbox-close]")
        .forEach(element => {
            element.addEventListener("click", closeLightbox);
        });

    document.addEventListener("keydown", event => {

        if (lightbox.hidden) return;

        if (event.key === "Escape") {
            closeLightbox();
        }

        if (event.key === "ArrowLeft") {
            showImage(currentIndex - 1);
        }

        if (event.key === "ArrowRight") {
            showImage(currentIndex + 1);
        }

        if (event.key === "Tab") {
            const focusable = Array.from(
                lightbox.querySelectorAll("button")
            );

            const first = focusable[0];
            const last = focusable[focusable.length - 1];

            if (event.shiftKey &&
                document.activeElement === first) {
                event.preventDefault();
                last.focus();
            } else if (
                !event.shiftKey &&
                document.activeElement === last
            ) {
                event.preventDefault();
                first.focus();
            }
        }
    });

});
