window.addEventListener("scroll", function () {
    const header = document.getElementById("mainHeader");
    if (!header) return;

    if (window.scrollY > 50) {
        header.classList.add("scrolled");
    } else {
        header.classList.remove("scrolled");
    }
}, { passive: true });

document.addEventListener("DOMContentLoaded", function () {

    const modalElement = document.getElementById(
        "firstGradeRegistrationModal"
    );


    if (!modalElement) {
        return;
    }


    // بررسی وجود Bootstrap
    if (typeof bootstrap === "undefined") {
        console.error("Bootstrap JS is not loaded.");
        return;
    }


    // ایجاد Modal
    const registrationModal = new bootstrap.Modal(modalElement, {

        backdrop: true,

        keyboard: true,

        focus: true

    });


    // نمایش با تاخیر برای Animation بهتر
    setTimeout(() => {

        registrationModal.show();

    }, 900);


});