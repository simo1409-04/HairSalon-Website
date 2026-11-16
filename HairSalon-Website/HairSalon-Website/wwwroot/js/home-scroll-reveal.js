(() => {
    const revealRoots = document.querySelectorAll(".graphite-home, [data-reveal-root]");
    const revealElements = [];
    const safeRevealElements = Array.from(document.querySelectorAll("[data-safe-reveal]"));

    revealRoots.forEach((root) => {
        root.querySelectorAll("[data-reveal]").forEach((element) => revealElements.push(element));
    });

    if (revealElements.length === 0 && safeRevealElements.length === 0) {
        return;
    }

    const reducedMotion = window.matchMedia("(prefers-reduced-motion: reduce)").matches;
    if (reducedMotion || !("IntersectionObserver" in window)) {
        revealElements.forEach((element) => element.classList.add("is-visible"));
        return;
    }

    if (safeRevealElements.length > 0 && typeof safeRevealElements[0].animate === "function") {
        const safeObserver = new IntersectionObserver((entries) => {
            entries.forEach((entry) => {
                if (!entry.isIntersecting) {
                    return;
                }

                const direction = entry.target.dataset.safeReveal === "right" ? 1 : -1;
                entry.target.animate([
                    {
                        opacity: 0,
                        transform: `translate3d(${direction * 56}px, 0, 0) scale(.985)`
                    },
                    {
                        opacity: 1,
                        transform: "translate3d(0, 0, 0) scale(1)"
                    }
                ], {
                    duration: 850,
                    easing: "cubic-bezier(.2, .7, .2, 1)",
                    fill: "none"
                });

                safeObserver.unobserve(entry.target);
            });
        }, {
            threshold: 0.08,
            rootMargin: "0px 0px 5% 0px"
        });

        safeRevealElements.forEach((element) => safeObserver.observe(element));
    }

    if (revealElements.length === 0) {
        return;
    }

    revealRoots.forEach((root) => root.classList.add("reveal-ready"));

    const pendingElements = new Set(revealElements);
    let observer;
    let fallbackFrame;

    const revealElement = (element) => {
        element.classList.add("is-visible");
        pendingElements.delete(element);
        observer?.unobserve(element);

        if (pendingElements.size === 0) {
            window.removeEventListener("scroll", requestFallbackCheck);
            window.removeEventListener("resize", requestFallbackCheck);
        }
    };

    const revealElementsInView = () => {
        fallbackFrame = undefined;
        const viewportHeight = window.innerHeight || document.documentElement.clientHeight;

        pendingElements.forEach((element) => {
            const bounds = element.getBoundingClientRect();
            if (bounds.top <= viewportHeight * 1.08 && bounds.bottom >= -50) {
                revealElement(element);
            }
        });
    };

    function requestFallbackCheck() {
        if (fallbackFrame !== undefined) {
            return;
        }

        fallbackFrame = window.requestAnimationFrame(revealElementsInView);
    }

    observer = new IntersectionObserver((entries) => {
        entries.forEach((entry) => {
            if (!entry.isIntersecting) {
                return;
            }

            revealElement(entry.target);
        });
    }, {
        threshold: 0.03,
        rootMargin: "0px 0px 8% 0px"
    });

    revealElements.forEach((element) => observer.observe(element));
    window.addEventListener("scroll", requestFallbackCheck, { passive: true });
    window.addEventListener("resize", requestFallbackCheck);
    requestFallbackCheck();
})();
