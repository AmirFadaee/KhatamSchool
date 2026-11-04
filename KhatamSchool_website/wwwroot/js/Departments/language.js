document.addEventListener("DOMContentLoaded", () => {

    // ═══════════════════════════════
    // PARTICLES
    // ═══════════════════════════════
    const canvas = document.getElementById("langParticles");
    if (canvas) {
        const ctx = canvas.getContext("2d");
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;

        const particles = Array.from({ length: 60 }, () => ({
            x: Math.random() * canvas.width,
            y: Math.random() * canvas.height,
            r: Math.random() * 2 + 0.5,
            dx: (Math.random() - 0.5) * 0.4,
            dy: (Math.random() - 0.5) * 0.4,
            o: Math.random() * 0.5 + 0.1
        }));

        function drawParticles() {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            particles.forEach(p => {
                ctx.beginPath();
                ctx.arc(p.x, p.y, p.r, 0, Math.PI * 2);
                ctx.fillStyle = `rgba(0,180,255,${p.o})`;
                ctx.fill();
                p.x += p.dx;
                p.y += p.dy;
                if (p.x < 0 || p.x > canvas.width) p.dx *= -1;
                if (p.y < 0 || p.y > canvas.height) p.dy *= -1;
            });
            requestAnimationFrame(drawParticles);
        }

        drawParticles();

        window.addEventListener("resize", () => {
            canvas.width = window.innerWidth;
            canvas.height = window.innerHeight;
        });
    }

    // ═══════════════════════════════
    // MODAL
    // ═══════════════════════════════
    const modal = document.querySelector(".lang-modal");
    const backdrop = document.querySelector(".lang-backdrop");
    const closeBtn = document.querySelector(".lang-close-modal");

    function openModal(card) {
        document.getElementById("lModalImg").src = card.dataset.img || "";
        document.getElementById("lModalName").textContent = card.dataset.name || "";
        document.getElementById("lModalBadge").textContent = card.dataset.role || "";
        document.getElementById("lModalDesc").textContent = card.dataset.desc || "";
        document.getElementById("lModalExp").textContent = card.dataset.exp || "";
        document.getElementById("lModalLang").textContent = card.dataset.lang || "";
        modal.classList.add("show");
        gsap.fromTo(".lang-modal-content",
            { scale: 0.85, opacity: 0, y: 30 },
            { scale: 1, opacity: 1, y: 0, duration: 0.45, ease: "power3.out" }
        );
    }

    function closeModal() {
        gsap.to(".lang-modal-content", {
            scale: 0.9, opacity: 0, y: 20, duration: 0.3,
            ease: "power2.in",
            onComplete: () => modal.classList.remove("show")
        });
    }

    document.querySelectorAll(".lang-teacher-card").forEach(card => {
        card.addEventListener("click", () => openModal(card));
    });

    backdrop.addEventListener("click", closeModal);
    closeBtn.addEventListener("click", closeModal);
    document.addEventListener("keydown", e => { if (e.key === "Escape") closeModal(); });
});