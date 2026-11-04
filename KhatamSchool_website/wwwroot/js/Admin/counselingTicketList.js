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

            const parent =
                row.dataset.parent.toLowerCase();

            const student =
                row.dataset.student.toLowerCase();

            const code =
                row.dataset.code.toLowerCase();

            const subject =
                row.dataset.subject.toLowerCase();

            const status =
                row.dataset.status;

            const matchSearch =
                !search ||
                parent.includes(search) ||
                student.includes(search) ||
                code.includes(search) ||
                subject.includes(search);

            const matchFilter =
                activeFilter === "all" ||
                status === activeFilter;

            row.classList.toggle(
                "hidden",
                !(matchSearch && matchFilter)
            );

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
    // GSAP
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
            duration: .45,
            stagger: .05,
            ease: "power2.out"
        }

    );
});
document.querySelectorAll(".al-action-btn.view").forEach(btn => {

    btn.addEventListener("click", () => {

        document.getElementById("ticketMessageText").textContent =
            btn.dataset.message;

        new bootstrap.Modal(
            document.getElementById("ticketMessageModal")
        ).show();

    });

});
