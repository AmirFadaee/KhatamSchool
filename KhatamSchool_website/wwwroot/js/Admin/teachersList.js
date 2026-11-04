document.addEventListener("DOMContentLoaded", () => {

    // ═══════════════════════════════
    // SEARCH
    // ═══════════════════════════════
    const searchInput = document.getElementById("searchInput");
    const rows = document.querySelectorAll(".al-row");

    function filterRows() {
        const search = searchInput.value.trim().toLowerCase();
        const activeFilter = document.querySelector(".al-filter-btn.active").dataset.filter;

        rows.forEach(row => {
            const name = row.dataset.name.toLowerCase();
            const code = row.dataset.code.toLowerCase();
            const status = row.dataset.status;

            const matchSearch = !search || name.includes(search) || code.includes(search);
            const matchFilter = activeFilter === "all" || status === activeFilter;

            row.classList.toggle("hidden", !(matchSearch && matchFilter));
        });
    }

    searchInput.addEventListener("input", filterRows);

    // ═══════════════════════════════
    // FILTER BUTTONS
    // ═══════════════════════════════
    document.querySelectorAll(".al-filter-btn").forEach(btn => {
        btn.addEventListener("click", () => {
            document.querySelectorAll(".al-filter-btn").forEach(b => b.classList.remove("active"));
            btn.classList.add("active");
            filterRows();
        });
    });

    // ═══════════════════════════════
    // ROW ANIMATION ON LOAD
    // ═══════════════════════════════
    gsap.fromTo(".al-row",
        { opacity: 0, y: 10 },
        { opacity: 1, y: 0, duration: 0.4, stagger: 0.05, ease: "power2.out" }
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
                title: "حذف معلم",
                html: `آیا از حذف <strong style="color:#f87171">${name}</strong> مطمئن هستید؟`,
                showCancelButton: true,
                confirmButtonText: "بله، حذف شود",
                cancelButtonText: "انصراف",
                background: "#13131f",
                color: "#ffffff",
                confirmButtonColor: "#ef4444",
                cancelButtonColor: "rgba(255,255,255,0.1)"
            });

            if (!confirm.isConfirmed) return;

            try {
                const response = await fetch(`/Admin/DeleteTeacher/${id}`, {
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
                        duration: 0.4,
                        ease: "power2.in",
                        onComplete: () => row.remove()
                    });

                    Swal.fire({
                        icon: "success",
                        title: "حذف شد",
                        text: `${name} با موفقیت حذف شد`,
                        timer: 2000,
                        showConfirmButton: false,
                        background: "#13131f",
                        color: "#ffffff"
                    });
                }
            } catch {
                Swal.fire({
                    icon: "error",
                    title: "خطا",
                    text: "عملیات با مشکل مواجه شد",
                    background: "#13131f",
                    color: "#ffffff",
                    confirmButtonColor: "#4f46e5"
                });
            }
        });
    });
});