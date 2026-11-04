document.addEventListener("DOMContentLoaded", function () {


    let currentStep = 1;

    const steps = document.querySelectorAll(".wizard-step");
    const progressSteps = document.querySelectorAll(".step");
    const progressBar = document.querySelector("#progressBar");


    function showStep(step) {


        steps.forEach(x => {

            x.classList.remove("active");

        });


        document
            .querySelector(`.wizard-step[data-step="${step}"]`)
            .classList.add("active");



        progressSteps.forEach(x => {


            let number = parseInt(x.dataset.step);


            if (number <= step) {

                x.classList.add("active");

            }
            else {

                x.classList.remove("active");

            }


        });



        progressBar.style.width =
            `${((step - 1) / 2) * 100}%`;



    }



    document.querySelectorAll(".btn-next")
        .forEach(btn => {


            btn.addEventListener("click", () => {


                if (validateStep(currentStep)) {

                    currentStep++;

                    showStep(currentStep);

                }


            });


        });



    document.querySelectorAll(".btn-prev")
        .forEach(btn => {


            btn.addEventListener("click", () => {


                currentStep--;

                showStep(currentStep);


            });


        });




    function validateStep(step) {


        let valid = true;



        const container =
            document.querySelector(
                `.wizard-step[data-step="${step}"]`
            );



        container
            .querySelectorAll("input,textarea,select")
            .forEach(input => {


                if (input.hasAttribute("name")
                    &&
                    input.value.trim() === "") {


                    input.classList.add("is-invalid");

                    valid = false;

                }
                else {

                    input.classList.remove("is-invalid");

                }


            });



        return valid;

    }




    $("#registrationForm")
        .submit(function (e) {


            e.preventDefault();



            if (!validateStep(3))
                return;



            let btn = $("#submitBtn");


            btn.prop("disabled", true);

            btn.html(
                `
        <span class="spinner-border spinner-border-sm"></span>
        در حال ارسال...
        `
            );

            let formData =
                new FormData(this);



            fetch("/Registration/Create",
                {
                    method: "POST",
                    body: formData

                })
                .then(response => response.json())

                .then(result => {


                    if (result.succeded) {


                        Swal.fire({

                            icon: "success",

                            title: "ثبت شد",

                            text: "درخواست شما با موفقیت ثبت شد",

                            confirmButtonText: "باشه",

                            timer: 3000

                        });


                        $("#registrationForm")[0].reset();


                        currentStep = 1;

                        showStep(1);


                    }
                    else {


                        Swal.fire({

                            icon: "error",

                            title: "خطا",

                            text: result.message

                        });


                    }


                })


                .catch(error => {


                    Swal.fire({

                        icon: "error",

                        title: "خطا",

                        text: error.message

                    });


                })


                .finally(() => {


                    btn.prop("disabled", false);


                    btn.html(
                        "ثبت درخواست"
                    );


                });



        });
});