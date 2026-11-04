new Swiper(".newsSwiper", {
    slidesPerView: 1,
    spaceBetween: 25,
    loop: true,
    autoplay: {
        delay: 3500,
        disableOnInteraction: false
    },
    navigation: {
        nextEl: ".news-next",
        prevEl: ".news-prev"
    },
    breakpoints: {
        768: { slidesPerView: 2 },
        1200: { slidesPerView: 3 }
    }
});

new Swiper(".departmentsSwiper", {
    slidesPerView: 1,
    spaceBetween: 30,
    loop: true,
    centeredSlides: true,
    navigation: {
        nextEl: ".dept-next",
        prevEl: ".dept-prev"
    },
    breakpoints: {
        768: { slidesPerView: 2 },
        1200: { slidesPerView: 3 }
    }
});

if (typeof $ !== "undefined" && $("[data-fancybox]").length) {
    $("[data-fancybox]").fancybox();
}