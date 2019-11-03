'use-strict'

let popupLoginIn = document.getElementById("popup-loginIn");

function ShowPopupe() {
    popupLoginIn.style.transition = "all 1.5s";
    popupLoginIn.style.opacity = 0;
}
setTimeout(ShowPopupe, 5000);