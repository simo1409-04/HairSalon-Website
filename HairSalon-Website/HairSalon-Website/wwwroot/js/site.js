// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.querySelectorAll("[data-tempdata-notification]").forEach((notification) => {
    const duration = Number(notification.dataset.tempdataDuration) || 5000;
    window.setTimeout(() => {
        notification.classList.add("site-notification--hide");
        window.setTimeout(() => notification.remove(), 400);
    }, duration);
});
