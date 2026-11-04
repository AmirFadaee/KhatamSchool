document.addEventListener("DOMContentLoaded", () => {

    //═══════════════════════════════
    // SUBMIT
    //═══════════════════════════════
    const submitBtn = document.getElementById("loginSubmit");
    const submitBtnOriginalHTML = submitBtn.innerHTML;

    submitBtn.addEventListener("click", async () => {
        const username = document.getElementById("loginUsername").value.trim();
        const password = document.getElementById("loginPassword").value;

        if (!username || !password) {
            Swal.fire({
                icon: "warning",
                title: "فیلدهای الزامی",
                text: "لطفاً نام کاربری و رمز عبور را وارد کنید.",
                background: "#13131f",
                color: "#fff",
                confirmButtonColor: "#2547f4"
            });
            return;
        }

        submitBtn.disabled = true;
        submitBtn.textContent = "در حال ورود...";

        try {
            const response = await fetch("/Account/Login", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ Username: username, Password: password })
            });

            const result = await response.json();

            if (result.succeded) {
                Swal.fire({
                    icon: "success",
                    title: "ورود موفق",
                    text: result.message,
                    background: "#13131f",
                    color: "#fff",
                    confirmButtonColor: "#2547f4",
                    timer: 1200,
                    showConfirmButton: false
                }).then(() => {
                    location.href = "/Admin/Dashboard";
                });
            } else {
                Swal.fire({
                    icon: "error",
                    title: "ورود ناموفق",
                    text: result.message,
                    background: "#13131f",
                    color: "#fff",
                    confirmButtonColor: "#2547f4"
                });
            }
        } catch {
            Swal.fire({
                icon: "error",
                title: "خطا",
                text: "ارتباط با سرور برقرار نشد.",
                background: "#13131f",
                color: "#fff"
            });
        } finally {
            submitBtn.disabled = false;
            submitBtn.innerHTML = submitBtnOriginalHTML;
        }
    });
});