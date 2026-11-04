document.addEventListener("DOMContentLoaded", () => {

    //═══════════════════════════════
    // IMAGE UPLOAD
    //═══════════════════════════════
    const uploadArea = document.getElementById("teacherUploadArea");
    const imageInput = document.getElementById("teacherImage");
    const preview = document.getElementById("teacherPreview");

    let selectedImage = null;

    if (uploadArea && imageInput && preview) {
        uploadArea.addEventListener("click", () => imageInput.click());

        uploadArea.addEventListener("dragover", e => {
            e.preventDefault();
            preview.style.borderColor = "rgba(99,102,241,.6)";
            preview.style.background = "rgba(99,102,241,.08)";
        });

        uploadArea.addEventListener("dragleave", () => {
            preview.style.borderColor = "";
            preview.style.background = "";
        });

        uploadArea.addEventListener("drop", e => {
            e.preventDefault();
            const file = e.dataTransfer.files[0];
            if (file && file.type.startsWith("image/"))
                loadImage(file);
        });

        imageInput.addEventListener("change", () => {
            if (imageInput.files[0])
                loadImage(imageInput.files[0]);
        });
    } else {
        console.warn("عناصر آپلود تصویر معلم پیدا نشدند.");
    }

    function loadImage(file) {
        selectedImage = file;
        const reader = new FileReader();
        reader.onload = e => {
            preview.classList.add("has-image");
            preview.innerHTML = `
                <img src="${e.target.result}">
                <button class="upload-change-btn"
                    onclick="document.getElementById('teacherImage').click();return false;">
                    تغییر تصویر
                </button>
            `;
        };
        reader.readAsDataURL(file);
    }

    //═══════════════════════════════
    // SUBMIT
    //═══════════════════════════════
    const submitBtn = document.getElementById("teacherSubmit");

    if (submitBtn) {
        submitBtn.addEventListener("click", async () => {
            const idEl = document.getElementById("tId");
            const firstNameEl = document.getElementById("tFirstName");
            const lastNameEl = document.getElementById("tLastName");
            const personnelCodeEl = document.getElementById("tPersonnelCode");
            const hireDateEl = document.getElementById("tHireDate");

            if (!idEl || !firstNameEl || !lastNameEl || !personnelCodeEl || !hireDateEl) {
                console.error("یک یا چند فیلد فرم معلم در صفحه پیدا نشد.");
                return;
            }

            const id = idEl.value;
            const firstName = firstNameEl.value.trim();
            const lastName = lastNameEl.value.trim();
            const personnelCode = personnelCodeEl.value.trim();
            const hireDate = hireDateEl.value;

            if (!firstName || !lastName || !personnelCode || !hireDate) {
                Swal.fire({
                    icon: "warning",
                    title: "فیلدهای الزامی",
                    text: "نام، نام خانوادگی، کد پرسنلی و تاریخ استخدام الزامی است.",
                    background: "#13131f",
                    color: "#fff",
                    confirmButtonColor: "#4f46e5"
                });
                return;
            }

            const formData = new FormData();
            formData.append("Id", id);
            formData.append("FirstName", firstName);
            formData.append("LastName", lastName);
            formData.append("PersonnelCode", personnelCode);
            formData.append("Specialty", document.getElementById("tSpecialty")?.value.trim() || "");
            formData.append("Bio", document.getElementById("tBio")?.value.trim() || "");
            formData.append("YearsOfExperience", document.getElementById("tExperience")?.value || 0);
            formData.append("HireDate", hireDate);
            formData.append("DisplayOrder", document.getElementById("tDisplayOrder")?.value || 0);
            formData.append("IsActive", document.getElementById("tIsActive")?.checked ?? true);
            formData.append("CurrentProfileImagePath", document.getElementById("tCurrentImage")?.value || "");

            if (selectedImage)
                formData.append("ProfileImage", selectedImage);

            const html = submitBtn.innerHTML;
            submitBtn.disabled = true;
            submitBtn.innerHTML = `<span>در حال ذخیره...</span>`;

            try {
                const response = await fetch("/Admin/UpdateTeacher", {
                    method: "POST",
                    body: formData
                });

                const result = await response.json();

                if (result.succeded) {
                    Swal.fire({
                        icon: "success",
                        title: "ثبت شد",
                        text: "تغییرات با موفقیت ذخیره شد.",
                        background: "#13131f",
                        color: "#fff",
                        confirmButtonColor: "#4f46e5"
                    }).then(() => {
                        location.href = "/Admin/TeachersList";
                    });
                } else {
                    Swal.fire({
                        icon: "error",
                        title: "خطا",
                        text: result.message,
                        background: "#13131f",
                        color: "#fff"
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
                submitBtn.innerHTML = html;
            }
        });
    } else {
        console.warn("دکمه‌ی ثبت معلم (teacherSubmit) در صفحه پیدا نشد.");
    }

    //═══════════════════════════════
    // INPUT ANIMATION
    //═══════════════════════════════
    document.querySelectorAll(".af-group input,.af-group textarea").forEach(el => {
        el.addEventListener("focus", () => {
            gsap.to(el, { borderColor: "rgba(99,102,241,.5)", duration: .3 });
        });
        el.addEventListener("blur", () => {
            gsap.to(el, { borderColor: "rgba(99,102,241,.15)", duration: .3 });
        });
    });

});