document.addEventListener("DOMContentLoaded", () => {
    const swiperEl = document.querySelector(".achSwiper");
    if (!swiperEl) return;

    const totalSlides = swiperEl.querySelectorAll(".swiper-slide").length;
    const progressBar = document.getElementById("achProgress");

    const swiper = new Swiper(".achSwiper", {
        slidesPerView: 1,
        spaceBetween: 30,
        centeredSlides: true,
        loop: totalSlides > 2,
        speed: 600,
        autoplay: {
            delay: 4000,
            disableOnInteraction: false,
            pauseOnMouseEnter: true
        },
        navigation: {
            nextEl: ".ach-next",
            prevEl: ".ach-prev"
        },
        pagination: {
            el: ".ach-pagination",
            clickable: true,
            renderBullet: (index, className) => {
                return `<span class="${className}" style="
                    width: 8px;
                    height: 8px;
                    border-radius: 50%;
                    background: var(--ach-primary);
                    opacity: 0.3;
                    cursor: pointer;
                    transition: all 0.3s;
                    display: inline-block;
                "></span>`;
            }
        },
        breakpoints: {
            768: { slidesPerView: 1.3 },
            1024: { slidesPerView: 1.6 }
        },
        on: {
            slideChange: (swiper) => updateProgress(swiper),
            init: (swiper) => updateProgress(swiper)
        }
    });

    function updateProgress(swiper) {
        if (!progressBar) return;
        const progress = ((swiper.realIndex + 1) / totalSlides) * 100;
        progressBar.style.width = `${progress}%`;
    }

    // GSAP entrance animation
    gsap.fromTo(".ach-card",
        { opacity: 0, y: 40 },
        { opacity: 1, y: 0, duration: 0.8, stagger: 0.1, ease: "power3.out" }
    );
});