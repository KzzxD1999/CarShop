'use-strict'

let popupLoginIn = document.getElementById("popup-loginIn");



var today = new Date().getHours();
//put whatever future date you want below
var showUntil = new Date().getHours() + 20;

if (sessionStorage.getItem("shown") === null) {
    setTimeout(function () {
        if (today < showUntil) {
            console.log(showUntil);
            popupLoginIn.style.transition = "all 1.5s";
            popupLoginIn.style.opacity = 0;
            popupLoginIn.style.display = "none";
            sessionStorage.setItem("shown", "true");

        }
    }, 5000);
} else {
    popupLoginIn.style.display = "none";
}

function SessionDelete() {
    sessionStorage.removeItem("shown");
}