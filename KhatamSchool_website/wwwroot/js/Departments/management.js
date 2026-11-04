document.addEventListener("DOMContentLoaded", () => {

    // ═══════════════════════════════
    // HOVER EFFECTS - STAFF CARDS
    // ═══════════════════════════════
    document.querySelectorAll(".staff-card").forEach(card => {
        card.addEventListener("mouseenter", () => {
            gsap.to(card.querySelector(".icon-ring"), {
                scale: 1.2,
                borderColor: "rgba(180,140,60,0.8)",
                duration: 0.3
            });
            gsap.to(card.querySelector(".staff-card-icon span"), {
                scale: 1.1,
                duration: 0.3
            });
        });

        card.addEventListener("mouseleave", () => {
            gsap.to(card.querySelector(".icon-ring"), {
                scale: 1,
                borderColor: "rgba(180,140,60,0.35)",
                duration: 0.3
            });
            gsap.to(card.querySelector(".staff-card-icon span"), {
                scale: 1,
                duration: 0.3
            });
        });
    });

    // ═══════════════════════════════
    // PARALLAX - PRINCIPAL CARD
    // ═══════════════════════════════
    const principal = document.querySelector(".executive-card.principal");
    if (principal) {
        principal.addEventListener("mousemove", e => {
            const rect = principal.getBoundingClientRect();
            const x = (e.clientX - rect.left) / rect.width - 0.5;
            const y = (e.clientY - rect.top) / rect.height - 0.5;
            gsap.to(principal, {
                rotateY: x * 8,
                rotateX: -y * 8,
                transformPerspective: 1000,
                duration: 0.4,
                ease: "power2.out"
            });
            gsap.to(principal.querySelector(".principal-glow"), {
                x: x * 40,
                y: y * 40,
                duration: 0.4
            });
        });

        principal.addEventListener("mouseleave", () => {
            gsap.to(principal, {
                rotateY: 0,
                rotateX: 0,
                duration: 0.6,
                ease: "power2.out"
            });
            gsap.to(principal.querySelector(".principal-glow"), {
                x: 0,
                y: 0,
                duration: 0.6
            });
        });
    }

    // ═══════════════════════════════
    // MODAL
    // ═══════════════════════════════
    const modal = document.querySelector(".management-modal");
    const backdrop = document.querySelector(".management-backdrop");
    const closeBtn = document.querySelector(".management-close-modal");

    function openModal(card) {
        document.getElementById("modalImg").src = card.dataset.img || "";
        document.getElementById("modalName").textContent = card.dataset.name || "";
        document.getElementById("modalBadge").textContent = card.dataset.role || "";
        document.getElementById("modalDesc").textContent = card.dataset.desc || "";
        document.getElementById("modalExp").textContent = card.dataset.exp || "";
        document.getElementById("modalField").textContent = card.dataset.field || "";

        modal.classList.add("show");

        gsap.fromTo(".management-modal-content",
            { scale: 0.85, opacity: 0, y: 30 },
            { scale: 1, opacity: 1, y: 0, duration: 0.45, ease: "power3.out" }
        );
    }

    function closeModal() {
        gsap.to(".management-modal-content", {
            scale: 0.9,
            opacity: 0,
            y: 20,
            duration: 0.3,
            ease: "power2.in",
            onComplete: () => modal.classList.remove("show")
        });
    }

    document.querySelectorAll(".executive-card").forEach(card => {
        card.addEventListener("click", () => openModal(card));
    });

    backdrop.addEventListener("click", closeModal);
    closeBtn.addEventListener("click", closeModal);

    document.addEventListener("keydown", e => {
        if (e.key === "Escape") closeModal();
    });
});
