document.addEventListener("DOMContentLoaded", () => {

    //═══════════════════════════════
    // SPLIT TEXT برای تیتر هیرو
    //═══════════════════════════════
    const titleEl = document.querySelector(".teachers-hero h1");
    if (titleEl) {
        const nodes = Array.from(titleEl.childNodes);
        titleEl.innerHTML = "";

        nodes.forEach(node => {
            if (node.nodeType === Node.TEXT_NODE) {
                node.textContent.trim().split(" ").filter(Boolean).forEach(word => {
                    titleEl.appendChild(makeWordSpan(word, false));
                    titleEl.appendChild(document.createTextNode(" "));
                });
            } else if (node.nodeType === Node.ELEMENT_NODE) {
                const isHighlight = node.classList.contains("highlight");
                const wrapper = document.createElement(node.tagName.toLowerCase());
                wrapper.className = node.className;
                node.textContent.trim().split(" ").filter(Boolean).forEach(word => {
                    wrapper.appendChild(makeWordSpan(word, isHighlight));
                    wrapper.appendChild(document.createTextNode(" "));
                });
                titleEl.appendChild(wrapper);
            }
        });
    }

    function makeWordSpan(word, isHighlight) {
        const outer = document.createElement("span");
        outer.className = "word";
        const inner = document.createElement("span");
        inner.className = "word-inner";
        inner.textContent = word;
        outer.appendChild(inner);
        return outer;
    }

    //═══════════════════════════════
    // انیمیشن ورود (GSAP Timeline)
    //═══════════════════════════════
    if (typeof gsap !== "undefined") {
        const tl = gsap.timeline({ defaults: { ease: "power4.out" } });

        tl.to(".hero-badge", { opacity: 1, y: 0, duration: .7 }, 0.1)
            .fromTo(".word-inner", {
                y: "110%", opacity: 0, filter: "blur(6px)"
            }, {
                y: "0%", opacity: 1, filter: "blur(0px)",
                duration: 1, stagger: 0.06
            }, 0.25)
            .to(".teachers-hero p", { opacity: 1, duration: .8 }, "-=0.4");
    }

    //═══════════════════════════════
    // SCROLL REVEAL کارت‌ها
    //═══════════════════════════════
    const cards = document.querySelectorAll(".teacher-card");
    //const cards = document.querySelectorAll(".teacher-card");
    //if (cards.length && typeof gsap !== "undefined") {
    //    const observer = new IntersectionObserver((entries) => {
    //        entries.forEach((entry, i) => {
    //            if (entry.isIntersecting) {
    //                gsap.to(entry.target, {
    //                    opacity: 1,
    //                    y: 0,
    //                    duration: .8,
    //                    delay: (i % 3) * 0.1,
    //                    ease: "power3.out"
    //                });
    //                observer.unobserve(entry.target);
    //            }
    //        });
    //    }, { threshold: 0.15 });

    //    cards.forEach(card => observer.observe(card));
    //} else {
    //    cards.forEach(card => {
    //        card.style.opacity = 1;
    //        card.style.transform = "none";
    //    });
    //}

    //═══════════════════════════════
    // اسپات‌لایت دنبال ماوس روی کارت‌ها
    //═══════════════════════════════
    cards.forEach(card => {
        card.addEventListener("mousemove", (e) => {
            const rect = card.getBoundingClientRect();
            const x = ((e.clientX - rect.left) / rect.width) * 100;
            const y = ((e.clientY - rect.top) / rect.height) * 100;
            card.style.setProperty("--x", `${x}%`);
            card.style.setProperty("--y", `${y}%`);
        });
    });

    //═══════════════════════════════
    // MODAL
    //═══════════════════════════════
    const modal = document.querySelector(".teacher-modal");
    const experienceWrapper = document.getElementById("modalExperienceWrapper");
    const experienceText = document.getElementById("modalTeacherExperience");

    document.addEventListener("click", e => {
        if (e.target.closest(".profile-btn")) {
            const card = e.target.closest(".teacher-card");
            if (card) {
                document.getElementById("modalTeacherImg").src = card.dataset.img || "/images/team/teacher-placeholder.jpg";
                document.getElementById("modalTeacherName").textContent = card.dataset.name || "";
                document.getElementById("modalTeacherSpecialty").textContent = card.dataset.specialty || "";
                document.getElementById("modalTeacherBio").textContent = card.dataset.bio || "توضیحاتی ثبت نشده است.";

                const experience = card.dataset.experience;
                if (experience && experience.trim() !== "") {
                    experienceText.textContent = `${experience} سال تجربه آموزشی`;
                    experienceWrapper.classList.remove("hidden");
                } else {
                    experienceWrapper.classList.add("hidden");
                }
            }
            modal.classList.add("show");
            document.body.style.overflow = "hidden";
        }

        if (
            e.target.classList.contains("modal-backdrop") ||
            e.target.classList.contains("close-modal")
        ) {
            modal.classList.remove("show");
            document.body.style.overflow = "";
        }
    });
});