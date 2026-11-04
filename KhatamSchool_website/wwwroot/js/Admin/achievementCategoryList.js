document.addEventListener("DOMContentLoaded", () => {

    // ═══════════════════════════════
    // SEARCH
    // ═══════════════════════════════

    const searchInput = document.getElementById("searchInput");
    const rows = document.querySelectorAll(".al-row");

    function filterRows() {

        const search = searchInput.value.trim().toLowerCase();

        rows.forEach(row => {

            const name = row.dataset.name.toLowerCase();

            const match = !search || name.includes(search);

            row.classList.toggle("hidden", !match);

        });

    }

    searchInput.addEventListener("input", filterRows);


    // ═══════════════════════════════
    // ROW ANIMATION
    // ═══════════════════════════════

    gsap.fromTo(

        ".al-row",

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
        }

    );


    // ═══════════════════════════════
    // DELETE
    // ═══════════════════════════════

    document.querySelectorAll(".al-action-btn.delete").forEach(btn => {

        btn.addEventListener("click", async () => {

            const id = btn.dataset.id;
            const name = btn.dataset.name;

            const confirm = await Swal.fire({

                icon: "warning",

                title: "حذف دسته",

                html: `آیا از حذف <strong style="color:#f87171">${name}</strong> مطمئن هستید؟`,

                showCancelButton: true,

                confirmButtonText: "بله، حذف شود",

                cancelButtonText: "انصراف",

                background: "#13131f",

                color: "#fff",

                confirmButtonColor: "#ef4444",

                cancelButtonColor: "rgba(255,255,255,.12)"

            });

            if (!confirm.isConfirmed)
                return;

            try {

                const response = await fetch(`/Admin/DeleteAchievementCategory/${id}`, {

                    method: "DELETE"

                });

                const result = await response.json();

                if (result.succeded) {

                    const row = btn.closest(".al-row");

                    gsap.to(row, {

                        opacity: 0,

                        x: -20,

                        height: 0,

                        padding: 0,

                        duration: .35,

                        ease: "power2.in",

                        onComplete: () => row.remove()

                    });

                    Swal.fire({

                        icon: "success",

                        title: "حذف شد",

                        text: `${name} حذف شد.`,

                        timer: 1800,

                        showConfirmButton: false,

                        background: "#13131f",

                        color: "#fff"

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

                    color: "#fff",

                    confirmButtonColor: "#4f46e5"

                });

            }

        });

    });

});