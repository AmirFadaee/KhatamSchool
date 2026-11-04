document.addEventListener("DOMContentLoaded", () => {

    // ═══════════════════════════════
    // IMAGE UPLOAD
    // ═══════════════════════════════

    const uploadArea = document.getElementById("newsUploadArea");
    const imageInput = document.getElementById("newsImage");
    const preview = document.getElementById("newsPreview");

    let selectedImageFile = null;

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

        if (file && file.type.startsWith("image/")) {
            handleImage(file);
        }

    });

    imageInput.addEventListener("change", () => {

        if (imageInput.files[0]) {
            handleImage(imageInput.files[0]);
        }

    });

    function handleImage(file) {

        selectedImageFile = file;

        const reader = new FileReader();

        reader.onload = e => {

            preview.classList.add("has-image");

            preview.innerHTML = `
                <img src="${e.target.result}" alt="خبر">

                <button class="upload-change-btn"
                        onclick="document.getElementById('newsImage').click();return false;">

                    تغییر تصویر

                </button>
            `;

        };

        reader.readAsDataURL(file);

    }

    // ═══════════════════════════════
    // SUBMIT
    // ═══════════════════════════════

    const submitBtn = document.getElementById("newsSubmit");

    submitBtn.addEventListener("click", async () => {

        const title = document.getElementById("nTitle").value.trim();
        const summary = document.getElementById("nSummary").value.trim();
        const content = document.getElementById("nContent").value.trim();

        if (!title || !summary || !content) {

            Swal.fire({
                icon: "warning",
                title: "فیلدهای الزامی",
                text: "لطفاً تمام فیلدهای ستاره‌دار را تکمیل کنید.",
                confirmButtonText: "باشه",
                background: "#13131f",
                color: "#fff",
                confirmButtonColor: "#4f46e5"
            });

            return;
        }

        const formData = new FormData();

        formData.append("Title", title);
        formData.append("Summary", summary);
        formData.append("Content", content);

        formData.append(
            "PublishedAt",
            document.getElementById("nPublishedAt").value
        );

        formData.append(
            "IsPublished",
            document.getElementById("nIsPublished").checked
        );

        formData.append(
            "IsFeatured",
            document.getElementById("nIsFeatured").checked
        );

        if (selectedImageFile) {
            formData.append("CoverImage", selectedImageFile);
        }

        const originalHTML = submitBtn.innerHTML;

        submitBtn.disabled = true;

        submitBtn.innerHTML = `
            <svg width="18"
                 height="18"
                 viewBox="0 0 24 24"
                 fill="none"
                 stroke="currentColor"
                 stroke-width="2"
                 style="animation:spin 1s linear infinite">

                <path d="M12 2v4M12 18v4M4.93 4.93l2.83 2.83M16.24 16.24l2.83 2.83M2 12h4M18 12h4"/>

            </svg>

            <span>

                در حال ذخیره...

            </span>
        `;

        try {

            const response = await fetch("/Admin/AddNews", {

                method: "POST",
                body: formData

            });

            const result = await response.json();

            if (result.succeded) {

                Swal.fire({

                    icon: "success",
                    title: "موفق",

                    text: "خبر با موفقیت ثبت شد.",

                    confirmButtonText: "باشه",

                    background: "#13131f",
                    color: "#fff",
                    confirmButtonColor: "#4f46e5"

                }).then(() => {

                    window.location.href = "/Admin/NewsList";

                });

            }
            else {

                Swal.fire({

                    icon: "error",
                    title: "خطا",

                    text: result.message || "ثبت خبر انجام نشد.",

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

                title: "خطای اتصال",

                text: "ارتباط با سرور برقرار نشد.",

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
    // INPUT ANIMATION
    // ═══════════════════════════════

    document.querySelectorAll(".af-group input,.af-group textarea,.af-group select")
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