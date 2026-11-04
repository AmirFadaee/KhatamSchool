document.addEventListener("DOMContentLoaded", function () {
    AOS.init({
        duration: 1000,
        once: true,
        offset: 60
    });

    /* ============ شمارنده‌ی آماری (Statistics Counter) ============ */
    const counters = document.querySelectorAll(".counter");
    if (counters.length) {
        const animateCounter = (el) => {
            const target = parseInt(el.getAttribute("data-target"), 10) || 0;
            const duration = 1800;
            const startTime = performance.now();

            const tick = (now) => {
                const progress = Math.min((now - startTime) / duration, 1);
                const eased = 1 - Math.pow(1 - progress, 3); // easeOutCubic
                const value = Math.floor(eased * target);
                el.textContent = "+" + value.toLocaleString("fa-IR");

                if (progress < 1) {
                    requestAnimationFrame(tick);
                } else {
                    el.textContent = "+" + target.toLocaleString("fa-IR");
                }
            };
            requestAnimationFrame(tick);
        };

        const counterObserver = new IntersectionObserver((entries) => {
            entries.forEach((entry) => {
                if (entry.isIntersecting) {
                    animateCounter(entry.target);
                    counterObserver.unobserve(entry.target);
                }
            });
        }, { threshold: 0.5 });

        counters.forEach((counter) => counterObserver.observe(counter));
    }
});