document.addEventListener("DOMContentLoaded", () => {

    // ═══════════════════════════════
    // MODAL
    // ═══════════════════════════════
    const modal = document.querySelector(".counsel-modal");
    const backdrop = document.querySelector(".counsel-backdrop");
    const closeBtn = document.querySelector(".counsel-close-modal");

    function openModal(card) {
        document.getElementById("cModalImg").src = card.dataset.img || "";
        document.getElementById("cModalName").textContent = card.dataset.name || "";
        document.getElementById("cModalBadge").textContent = card.dataset.role || "";
        document.getElementById("cModalDesc").textContent = card.dataset.desc || "";
        document.getElementById("cModalExp").textContent = card.dataset.exp || "";
        document.getElementById("cModalField").textContent = card.dataset.field || "";
        modal.classList.add("show");
        gsap.fromTo(".counsel-modal-content",
            { scale: 0.85, opacity: 0, y: 30 },
            { scale: 1, opacity: 1, y: 0, duration: 0.45, ease: "power3.out" }
        );
    }

    function closeModal() {
        gsap.to(".counsel-modal-content", {
            scale: 0.9, opacity: 0, y: 20, duration: 0.3,
            ease: "power2.in",
            onComplete: () => modal.classList.remove("show")
        });
    }

    document.querySelectorAll(".counsel-card").forEach(card => {
        card.addEventListener("click", () => openModal(card));
    });

    if (backdrop) backdrop.addEventListener("click", closeModal);
    if (closeBtn) closeBtn.addEventListener("click", closeModal);
    document.addEventListener("keydown", e => { if (e.key === "Escape") closeModal(); });

    // ═══════════════════════════════
    // TICKET FORM
    // ═══════════════════════════════
    const submitBtn = document.getElementById("ticketSubmit");

    if (submitBtn) {
        submitBtn.addEventListener("click", async () => {
            const name = document.getElementById("ticketName").value.trim();
            const student = document.getElementById("ticketStudent").value.trim();
            const phone = document.getElementById("ticketPhone").value.trim();
            const subject = document.getElementById("ticketSubject").value.trim();
            const message = document.getElementById("ticketMessage").value.trim();

            // اعتبارسنجی
            if (!name || !student || !phone || !subject || !message) {
                Swal.fire({
                    icon: "warning",
                    title: "فیلدها را تکمیل کنید",
                    text: "لطفاً همه فیلدها را پر کنید",
                    confirmButtonText: "باشه",
                    background: "#0f0520",
                    color: "#ffffff",
                    confirmButtonColor: "#7c3aed"
                });

                gsap.to(".ticket-form-card", {
                    keyframes: [
                        { x: -8, duration: 0.07 },
                        { x: 8, duration: 0.07 },
                        { x: -6, duration: 0.07 },
                        { x: 6, duration: 0.07 },
                        { x: 0, duration: 0.07 }
                    ]
                });
                return;
            }

            // انیمیشن لودینگ دکمه
            const originalHTML = submitBtn.innerHTML;
            submitBtn.disabled = true;
            submitBtn.innerHTML = `
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" style="animation: spin 1s linear infinite">
                <path d="M12 2v4M12 18v4M4.93 4.93l2.83 2.83M16.24 16.24l2.83 2.83M2 12h4M18 12h4M4.93 19.07l2.83-2.83M16.24 7.76l2.83-2.83"/>
            </svg>
            <span>در حال ارسال...</span>
        `;

            try {
                const response = await fetch("/Departments/SubmitTicket", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        parentName: name,
                        studentName: student,
                        phoneNumber: phone,
                        subject: subject,
                        message: message
                    })
                });

                const result = await response.json();

                if (result.succeded) {
                    Swal.fire({
                        icon: "success",
                        title: "تیکت ارسال شد!",
                        html: `
                        <p style="color:rgba(255,255,255,0.7)">تیکت شما با موفقیت ثبت شد</p>
                        <div style="margin-top:16px;padding:12px 20px;background:rgba(168,85,247,0.1);border:1px solid rgba(168,85,247,0.3);border-radius:12px;display:inline-block">
                            <span style="color:rgba(255,255,255,0.5);font-size:13px">کد پیگیری</span><br>
                            <strong style="color:#a855f7;font-size:18px;letter-spacing:2px">${result.trackingCode ?? ''}</strong>
                        </div>
                    `,
                        confirmButtonText: "متوجه شدم",
                        background: "#0f0520",
                        color: "#ffffff",
                        confirmButtonColor: "#7c3aed"
                    });

                    // پاک کردن فرم
                    document.getElementById("ticketName").value = "";
                    document.getElementById("ticketStudent").value = "";
                    document.getElementById("ticketPhone").value = "";
                    document.getElementById("ticketSubject").value = "";
                    document.getElementById("ticketMessage").value = "";

                } else {
                    Swal.fire({
                        icon: "error",
                        title: "خطا",
                        text: result.message || "مشکلی پیش آمد، دوباره تلاش کنید",
                        confirmButtonText: "باشه",
                        background: "#0f0520",
                        color: "#ffffff",
                        confirmButtonColor: "#7c3aed"
                    });
                }

            } catch (error) {
                Swal.fire({
                    icon: "error",
                    title: "خطای اتصال",
                    text: "ارتباط با سرور برقرار نشد",
                    confirmButtonText: "باشه",
                    background: "#0f0520",
                    color: "#ffffff",
                    confirmButtonColor: "#7c3aed"
                });
            } finally {
                submitBtn.disabled = false;
                submitBtn.innerHTML = originalHTML;
            }
        });
    }

    // focus effect روی input ها
    document.querySelectorAll(".tf-group input, .tf-group select, .tf-group textarea").forEach(el => {
        el.addEventListener("focus", () => {
            gsap.to(el, { borderColor: "rgba(168,85,247,0.5)", duration: 0.3 });
        });
        el.addEventListener("blur", () => {
            gsap.to(el, { borderColor: "rgba(168,85,247,0.15)", duration: 0.3 });
        });
    });
});