$(document).ready(function () {
    let isOpen = true;

    $("#burgerBtn").click(function () {
        if (isOpen) {
            $("#mainContent").addClass("closed");
            $("#sidebar").addClass("closed");
            $("#navbar").addClass("closed");
            isOpen = false
        } else {
            $("#mainContent").removeClass("closed");
            $("#sidebar").removeClass("closed");
            $("#navbar").removeClass("closed");
            isOpen = true;
        }

        console.log("hhelo");
    });
});