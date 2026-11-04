document.addEventListener("DOMContentLoaded", () => {

    //═══════════════════════════════
    // SUBMIT
    //═══════════════════════════════
    const submitBtn = document.getElementById("registerSubmit");
    const submitBtnOriginalHTML = submitBtn.innerHTML;

    submitBtn.addEventListener("click", async () => {
        const fullname = document.getElementById("regFullname").value.trim();
        const username = document.getElementById("regUsername").value.trim();
        const password = document.getElementById("regPassword").value;

        if (!fullname || !username || !password) {
            Swal.fire({
                icon: "warning",
                title: "فیلدهای الزامی",
                text: "لطفاً تمام فیلدها را تکمیل کنید.",
                background: "#13131f",
                color: "#fff",
                confirmButtonColor: "#2547f4"
            });
            return;
        }

        if (password.length < 6) {
            Swal.fire({
                icon: "warning",
                title: "رمز عبور ضعیف",
                text: "رمز عبور باید حداقل ۶ کاراکتر باشد.",
                background: "#13131f",
                color: "#fff",
                confirmButtonColor: "#2547f4"
            });
            return;
        }

        submitBtn.disabled = true;
        submitBtn.textContent = "در حال ثبت‌نام...";

        try {
            const response = await fetch("/Account/AddUser", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ Fullname: fullname, Username: username, Password: password })
            });

            const result = await response.json();

            if (result.succeded) {
                Swal.fire({
                    icon: "success",
                    title: "ثبت‌نام موفق",
                    text: result.message,
                    background: "#13131f",
                    color: "#fff",
                    confirmButtonColor: "#2547f4"
                }).then(() => {
                    location.href = "/Account/Login";
                });
            } else {
                Swal.fire({
                    icon: "error",
                    title: "خطا",
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