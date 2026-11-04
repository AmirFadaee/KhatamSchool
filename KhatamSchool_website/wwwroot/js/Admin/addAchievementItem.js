document.addEventListener("DOMContentLoaded", () => {

    //═══════════════════════════════
    // IMAGE UPLOAD
    //═══════════════════════════════

    const uploadArea = document.getElementById("achievementUploadArea");
    const imageInput = document.getElementById("achievementImage");
    const preview = document.getElementById("achievementPreview");

    let selectedImage = null;

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

    function loadImage(file) {

        selectedImage = file;

        const reader = new FileReader();

        reader.onload = e => {

            preview.classList.add("has-image");

            preview.innerHTML = `

                <img src="${e.target.result}">

                <button class="upload-change-btn"
                    onclick="document.getElementById('achievementImage').click();return false;">

                    تغییر تصویر

                </button>

            `;

        };

        reader.readAsDataURL(file);

    }

    //═══════════════════════════════
    // CATEGORY CARD
    //═══════════════════════════════

    const cards = document.querySelectorAll(".achievement-category-card");

    cards.forEach(card => {

        const radio = card.querySelector("input");

        radio.addEventListener("change", () => {

            cards.forEach(c => c.classList.remove("selected"));

            card.classList.add("selected");

        });

    });

    //═══════════════════════════════
    // SUBMIT
    //═══════════════════════════════

    const submitBtn = document.getElementById("achievementSubmit");

    submitBtn.addEventListener("click", async () => {

        const title = document.getElementById("aTitle").value.trim();

        const description = document.getElementById("aDescription").value.trim();

        const category = document.querySelector("input[name='achievementCategory']:checked");

        if (!title || !category) {

            Swal.fire({

                icon: "warning",

                title: "فیلدهای الزامی",

                text: "عنوان و دسته بندی الزامی است.",

                background: "#13131f",

                color: "#fff",

                confirmButtonColor: "#4f46e5"

            });

            return;

        }

        const formData = new FormData();

        formData.append("Title", title);

        formData.append("Description", description);

        formData.append("CategoryId", category.value);

        formData.append("DisplayOrder",
            document.getElementById("aDisplayOrder").value || 0);

        formData.append("IsActive",
            document.getElementById("aIsActive").checked);

        if (selectedImage)
            formData.append("Image", selectedImage);

        const html = submitBtn.innerHTML;

        submitBtn.disabled = true;

        submitBtn.innerHTML = `
            <span>در حال ذخیره...</span>
        `;

        try {

            const response = await fetch("/Admin/AddAchievementItem", {

                method: "POST",

                body: formData

            });

            const result = await response.json();

            if (result.succeded) {

                Swal.fire({

                    icon: "success",

                    title: "ثبت شد",

                    text: "دستاورد با موفقیت ثبت شد.",

                    background: "#13131f",

                    color: "#fff",

                    confirmButtonColor: "#4f46e5"

                }).then(() => {

                    location.href = "/Admin/AchievementItemList";

                });

            }
            else {

                Swal.fire({

                    icon: "error",

                    title: "خطا",

                    text: result.message,

                    background: "#13131f",

                    color: "#fff"

                });

            }

        }
        catch {

            Swal.fire({

                icon: "error",

                title: "خطا",

                text: "ارتباط با سرور برقرار نشد.",

                background: "#13131f",

                color: "#fff"

            });

        }

        finally {

            submitBtn.disabled = false;

            submitBtn.innerHTML = html;

        }

    });

    //═══════════════════════════════
    // INPUT ANIMATION
    //═══════════════════════════════

    document.querySelectorAll(".af-group input,.af-group textarea").forEach(el => {

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