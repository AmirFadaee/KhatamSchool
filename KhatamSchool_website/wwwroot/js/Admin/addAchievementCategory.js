document.addEventListener("DOMContentLoaded", () => {

    // ═══════════════════════════════
    // ICON PREVIEW
    // ═══════════════════════════════

    const iconSelect = document.getElementById("cIcon");
    const iconPreview = document.getElementById("iconPreview");
    const iconName = document.getElementById("iconName");

    function updateIconPreview() {

        const icon = iconSelect.value || "fa-trophy";

        iconPreview.innerHTML = `
            <div class="upload-placeholder">
                <span class="upload-icon">
                    <i class="fa-solid ${icon}"></i>
                </span>
                <p id="iconName">${icon}</p>
            </div>
        `;

    }

    updateIconPreview();

    iconSelect.addEventListener("change", updateIconPreview);


    // ═══════════════════════════════
    // SUBMIT
    // ═══════════════════════════════

    const submitBtn = document.getElementById("categorySubmit");

    submitBtn.addEventListener("click", async () => {

        const title = document.getElementById("cTitle").value.trim();
        const icon = document.getElementById("cIcon").value;

        if (!title || !icon) {

            Swal.fire({
                icon: "warning",
                title: "اطلاعات ناقص است",
                text: "عنوان و آیکون الزامی هستند.",
                confirmButtonText: "باشه",
                background: "#13131f",
                color: "#fff",
                confirmButtonColor: "#4f46e5"
            });

            return;
        }


        const formData = new FormData();

        formData.append("Title", title);
        formData.append("Description", document.getElementById("cDescription").value.trim());
        formData.append("IconClass", icon);
        formData.append("DisplayOrder", document.getElementById("cDisplayOrder").value || 0);
        formData.append("IsActive", document.getElementById("cIsActive").checked);


        const originalHTML = submitBtn.innerHTML;

        submitBtn.disabled = true;

        submitBtn.innerHTML = `
            <svg width="18" height="18" viewBox="0 0 24 24"
                 fill="none"
                 stroke="currentColor"
                 stroke-width="2"
                 style="animation:spin 1s linear infinite">

                <path d="M12 2v4M12 18v4M4.93 4.93l2.83 2.83M16.24 16.24l2.83 2.83M2 12h4M18 12h4"/>

            </svg>

            <span>در حال ذخیره...</span>
        `;

        try {

            const response = await fetch("/Admin/AddAchievementCategory", {

                method: "POST",
                body: formData

            });

            const result = await response.json();

            if (result.succeded) {

                Swal.fire({

                    icon: "success",
                    title: "ثبت شد",
                    text: "دسته‌بندی با موفقیت ثبت شد.",
                    confirmButtonText: "باشه",
                    background: "#13131f",
                    color: "#fff",
                    confirmButtonColor: "#4f46e5"

                }).then(() => {

                    window.location.href = "/Admin/AchievementCategoryList";

                });

            }
            else {

                Swal.fire({

                    icon: "error",
                    title: "خطا",
                    text: result.message || "عملیات ناموفق بود.",
                    confirmButtonText: "باشه",
                    background: "#13131f",
                    color: "#fff",
                    confirmButtonColor: "#4f46e5"

                });

            }

        }
        catch {

            Swal.fire({

                icon: "error",
                title: "خطای ارتباط",
                text: "امکان ارتباط با سرور وجود ندارد.",
                confirmButtonText: "باشه",
                background: "#13131f",
                color: "#fff",
                confirmButtonColor: "#4f46e5"

            });

        }
        finally {

            submitBtn.disabled = false;
            submitBtn.innerHTML = originalHTML;

        }

    });



    // ═══════════════════════════════
    // INPUT EFFECTS
    // ═══════════════════════════════

    document
        .querySelectorAll(".af-group input, .af-group textarea, .af-group select")
        .forEach(el => {

            el.addEventListener("focus", () => {

                gsap.to(el, {

                    borderColor: "rgba(99,102,241,.5)",
                    duration: .3

                });

            });

            el.addEventListener("blur", () => {

                gsap.to(el, {

                    borderColor: "rgba(99,102,241,.15)",
                    duration: .3

                });

            });

        });

});