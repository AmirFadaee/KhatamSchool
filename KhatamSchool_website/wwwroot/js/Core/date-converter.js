document.addEventListener(
    "DOMContentLoaded",
    function () {


        document
            .querySelectorAll(".persian-date")
            .forEach(function (element) {


                let target =
                    element.dataset.target;



                flatpickr(
                    element,
                    {

                        locale: "fa",


                        disableMobile: true,


                        allowInput: false,


                        dateFormat: "Y/m/d",


                        onChange: function (
                            selectedDates
                        ) {



                            if (!selectedDates.length)
                                return;



                            let date =
                                selectedDates[0];



                            // Gregorian
                            let gy =
                                date.getFullYear();


                            let gm =
                                date.getMonth() + 1;


                            let gd =
                                date.getDate();




                            // Convert to Jalali
                            let jalaliDate =
                                jalaali.toJalaali(
                                    gy,
                                    gm,
                                    gd
                                );



                            element.value =

                                jalaliDate.jy +
                                "/" +
                                String(jalaliDate.jm)
                                    .padStart(2, "0")
                                +
                                "/" +
                                String(jalaliDate.jd)
                                    .padStart(2, "0");




                            // Save Gregorian

                            let hidden =
                                jalaali.toGregorian(

                                    jalaliDate.jy,

                                    jalaliDate.jm,

                                    jalaliDate.jd

                                );



                            document
                                .getElementById(target)
                                .value =


                                hidden.gy +
                                "-" +
                                String(hidden.gm)
                                    .padStart(2, "0")
                                +
                                "-" +
                                String(hidden.gd)
                                    .padStart(2, "0");

                        }


                    });


            });


    });