document.addEventListener("DOMContentLoaded", () => {

    // ═══════════════════════════════
    // SEARCH & FILTER
    // ═══════════════════════════════

    const searchInput = document.getElementById("searchInput");
    const rows = document.querySelectorAll(".al-row");

    function filterRows() {

        const search = searchInput.value.trim().toLowerCase();

        const activeFilter = document.querySelector(".al-filter-btn.active").dataset.filter;

        rows.forEach(row => {

            const title = row.dataset.title.toLowerCase();
            const status = row.dataset.status;
            const featured = row.dataset.featured;

            const matchSearch =
                !search ||
                title.includes(search);

            let matchFilter = true;

            switch (activeFilter) {

                case "published":
                    matchFilter = status === "published";
                    break;

                case "draft":
                    matchFilter = status === "draft";
                    break;

                case "featured":
                    matchFilter = featured === "1";
                    break;

                default:
                    matchFilter = true;
                    break;
            }

            row.classList.toggle("hidden", !(matchSearch && matchFilter));

        });

    }

    searchInput.addEventListener("input", filterRows);

    // ═══════════════════════════════
    // FILTER BUTTONS
    // ═══════════════════════════════

    document.querySelectorAll(".al-filter-btn").forEach(btn => {

        btn.addEventListener("click", () => {

            document.querySelectorAll(".al-filter-btn")
                .forEach(x => x.classList.remove("active"));

            btn.classList.add("active");

            filterRows();

        });

    });

    // ═══════════════════════════════
    // ROW ANIMATION
    // ═══════════════════════════════

    gsap.fromTo(".al-row",
        {
            opacity: 0,
            y: 12
        },
        {
            opacity: 1,
            y: 0,
            duration: .4,
            stagger: .05,
            ease: "power2.out"
        });

    // ═══════════════════════════════
    // DELETE NEWS
    // ═══════════════════════════════

    document.querySelectorAll(".al-action-btn.delete").forEach(btn => {

        btn.addEventListener("click", async () => {

            const id = btn.dataset.id;
            const title = btn.dataset.title;

            const confirm = await Swal.fire({

                icon: "warning",

                title: "حذف خبر",

                html: `
                    آیا از حذف خبر
                    <br><br>
                    <strong style="color:#f87171">
                        ${title}
                    </strong>
                    <br><br>
                    مطمئن هستید؟
                `,

                showCancelButton: true,

                confirmButtonText: "بله، حذف شود",

                cancelButtonText: "انصراف",

                background: "#13131f",

                color: "#ffffff",

                confirmButtonColor: "#ef4444",

                cancelButtonColor: "rgba(255,255,255,.12)"

            });

            if (!confirm.isConfirmed)
                return;

            try {

                const response = await fetch(`/Admin/DeleteNews/${id}`, {

                    method: "DELETE"

                });

                const result = await response.json();

                if (result.succeded) {

                    const row = btn.closest(".al-row");

                    gsap.to(row, {

                        opacity: 0,

                        x: -30,

                        height: 0,

                        paddingTop: 0,

                        paddingBottom: 0,

                        duration: .35,

                        ease: "power2.in",

                        onComplete: () => {

                            row.remove();

                            updateCounter();

                        }

                    });

                    Swal.fire({

                        icon: "success",

                        title: "حذف شد",

                        text: "خبر با موفقیت حذف شد.",

                        timer: 1800,

                        showConfirmButton: false,

                        background: "#13131f",

                        color: "#ffffff"

                    });

                }
                else {

                    Swal.fire({

                        icon: "error",

                        title: "خطا",

                        text: result.message || "حذف خبر انجام نشد.",

                        confirmButtonText: "باشه",

                        background: "#13131f",

                        color: "#ffffff",

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

                    color: "#ffffff",

                    confirmButtonColor: "#4f46e5"

                });

            }

        });

    });

    // ═══════════════════════════════
    // UPDATE HEADER COUNT
    // ═══════════════════════════════

    function updateCounter() {

        const count = document.querySelectorAll(".al-row").length;

        const headerText = document.querySelector(".admin-list-header p");

        if (headerText) {

            headerText.textContent = `${count} خبر ثبت شده`;

        }

    }

});