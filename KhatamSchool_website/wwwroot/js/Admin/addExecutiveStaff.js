document.addEventListener("DOMContentLoaded", () => {

    // IMAGE UPLOAD
    const uploadArea = document.getElementById("executiveUploadArea");
    const imageInput = document.getElementById("executiveImage");
    const preview = document.getElementById("executivePreview");
    let selectedImageFile = null;

    uploadArea.addEventListener("click", () => imageInput.click());

    uploadArea.addEventListener("dragover", e => {
        e.preventDefault();
        preview.style.borderColor = "rgba(99,102,241,0.6)";
    });

    uploadArea.addEventListener("dragleave", () => {
        preview.style.borderColor = "";
    });

    uploadArea.addEventListener("drop", e => {
        e.preventDefault();
        const file = e.dataTransfer.files[0];
        if (file && file.type.startsWith("image/")) handleImage(file);
    });

    imageInput.addEventListener("change", () => {
        if (imageInput.files[0]) handleImage(imageInput.files[0]);
    });

    function handleImage(file) {
        selectedImageFile = file;
        const reader = new FileReader();
        reader.onload = e => {
            preview.classList.add("has-image");
            preview.innerHTML = `
                <img src="${e.target.result}" alt="پیش‌نمایش" />
                <button class="upload-change-btn" onclick="document.getElementById('executiveImage').click(); return false;">
                    تغییر عکس
                </button>
            `;
        };
        reader.readAsDataURL(file);
    }

    // SUBMIT
    const submitBtn = document.getElementById("executiveSubmit");

    submitBtn.addEventListener("click", async () => {
        const firstName = document.getElementById("eFirstName").value.trim();
        const lastName = document.getElementById("eLastName").value.trim();
        const personnelCode = document.getElementById("ePersonnelCode").value.trim();
        const role = document.getElementById("eRole").value;
        const hireDate = document.getElementById("eHireDate").value;

        if (!firstName || !lastName || !personnelCode || !role || !hireDate) {
            Swal.fire({
                icon: "warning",
                title: "فیلدها را تکمیل کنید",
                text: "لطفاً فیلدهای الزامی را پر کنید",
                confirmButtonText: "باشه",
                background: "#13131f",
                color: "#ffffff",
                confirmButtonColor: "#4f46e5"
            });
            return;
        }

        const formData = new FormData();
        formData.append("FirstName", firstName);
        formData.append("LastName", lastName);
        formData.append("PersonnelCode", personnelCode);
        formData.append("Role", role);
        formData.append("HireDate", hireDate);
        formData.append("CustomRoleTitle", document.getElementById("eCustomRole").value.trim());
        formData.append("ExpertiseField", document.getElementById("eExpertiseField").value.trim());
        formData.append("Bio", document.getElementById("eBio").value.trim());
        formData.append("YearsOfExperience", document.getElementById("eExperience").value || 0);
        formData.append("DisplayOrder", document.getElementById("eDisplayOrder").value || 0);
        formData.append("IsActive", document.getElementById("eIsActive").checked);
        if (selectedImageFile) formData.append("ProfileImage", selectedImageFile);

        const originalHTML = submitBtn.innerHTML;
        submitBtn.disabled = true;
        submitBtn.innerHTML = `
            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" style="animation:spin 1s linear infinite">
                <path d="M12 2v4M12 18v4M4.93 4.93l2.83 2.83M16.24 16.24l2.83 2.83M2 12h4M18 12h4"/>
            </svg>
            <span>در حال ذخیره...</span>
        `;

        try {
            const response = await fetch("/Admin/AddExecutiveStaff", {
                method: "POST",
                body: formData
            });

            const result = await response.json();

            if (result.succeded) {
                Swal.fire({
                    icon: "success",
                    title: "ذخیره شد!",
                    text: "کادر اجرایی با موفقیت اضافه شد",
                    confirmButtonText: "باشه",
                    background: "#13131f",
                    color: "#ffffff",
                    confirmButtonColor: "#4f46e5"
                }).then(() => {
                    window.location.href = "/Admin/ExecutiveStaffList";
                });
            } else {
                Swal.fire({
                    icon: "error",
                    title: "خطا",
                    text: result.message || "مشکلی پیش آمد",
                    confirmButtonText: "باشه",
                    background: "#13131f",
                    color: "#ffffff",
                    confirmButtonColor: "#4f46e5"
                });
            }
        } catch {
            Swal.fire({
                icon: "error",
                title: "خطای اتصال",
                text: "ارتباط با سرور برقرار نشد",
                confirmButtonText: "باشه",
                background: "#13131f",
                color: "#ffffff",
                confirmButtonColor: "#4f46e5"
            });
        } finally {
            submitBtn.disabled = false;
            submitBtn.innerHTML = originalHTML;
        }
    });

    document.querySelectorAll(".af-group input, .af-group select, .af-group textarea").forEach(el => {
        el.addEventListener("focus", () => {
            gsap.to(el, { borderColor: "rgba(99,102,241,0.5)", duration: 0.3 });
        });
        el.addEventListener("blur", () => {
            gsap.to(el, { borderColor: "rgba(99,102,241,0.15)", duration: 0.3 });
        });
    });
});