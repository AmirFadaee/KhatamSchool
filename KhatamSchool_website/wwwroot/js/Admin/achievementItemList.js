document.addEventListener("DOMContentLoaded", () => {

    const searchInput = document.getElementById("searchInput");
    const rows = document.querySelectorAll(".al-row");

    function filterRows() {

        const search = searchInput.value.trim().toLowerCase();

        rows.forEach(row => {

            const title = row.dataset.name.toLowerCase();

            row.classList.toggle(
                "hidden",
                search && !title.includes(search)
            );

        });

    }

    searchInput.addEventListener("input", filterRows);

    gsap.fromTo(".al-row",
        {
            opacity: 0,
            y: 10
        },
        {
            opacity: 1,
            y: 0,
            stagger: 0.05,
            duration: .35
        });

    document.querySelectorAll(".delete").forEach(btn => {

        btn.addEventListener("click", async () => {

            const id = btn.dataset.id;
            const name = btn.dataset.name;

            const result = await Swal.fire({

                icon: "warning",
                title: "حذف دستاورد",
                html: `آیا از حذف <b>${name}</b> مطمئن هستید؟`,
                showCancelButton: true,
                confirmButtonText: "بله",
                cancelButtonText: "انصراف",

                background: "#13131f",
                color: "#fff"

            });

            if (!result.isConfirmed)
                return;

            try {

                const response = await fetch(`/Admin/DeleteAchievementItem/${id}`, {
                    method: "DELETE"
                });

                const json = await response.json();

                if (json.succeded) {

                    const row = btn.closest(".al-row");

                    gsap.to(row, {

                        opacity: 0,
                        x: -20,
                        height: 0,

                        onComplete: () => row.remove()

                    });

                    Swal.fire({

                        icon: "success",
                        title: "حذف شد",
                        timer: 1800,
                        showConfirmButton: false,

                        background: "#13131f",
                        color: "#fff"

                    });

                }

            }
            catch {

                Swal.fire({

                    icon: "error",
                    title: "خطا",

                    text: "مشکلی پیش آمد.",

                    background: "#13131f",
                    color: "#fff"

                });

            }

        });

    });

});