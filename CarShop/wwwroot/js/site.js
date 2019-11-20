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


function getData(id) {
    fetch(`/Car/DetailsCar/${id}`)
        .then(response => {
            console.log(response);
        });
}



function ShowError(error, success, message) {
    let er = document.getElementById('error1');
    console.log(error);
    if (error == 'True') {
        er.style.background = "#e80020";
        er.style.opacity = 1;
        er.childNodes.item(1).innerHTM1 = message;
        UnShowError(er);
    }
    if (success == 'True') {
        er.style.background = "#549612";
        er.style.opacity = 1;
        er.childNodes.item(1).innerHTM1 = message;
        UnShowError(er);
    }
    
}

function UnShowError(er) {

    setTimeout(function () {
        er.style.opacity = 0;

        
    }, 5000);
}





function ad() {

    let currentUrl = document.baseURI;
    console.log(currentUrl);
    let error = document.getElementById("error");
    event.preventDefault();
    let description = new FormData(this.event.target);
    console.log(description);
    fetch('/Car/CreateDescription', {
        method: "POST",
        body: description,

    }).then(function (response) {
        if (!response.ok) {
            throw response;
        }
        return response.json();
    }).then(function (json) {
        if (json.messages == "sucess") {
            document.location.reload(true);
        } else {
            error.innerHTML = json.error;
        }
    });






 
    //var xmlhttp = new XMLHttpRequest();
    //var url = "/Car/CreateDescription";
    //var input = {};
    //input.Name = document.getElementById("name").value;
    //input.Text = document.getElementById("text").value;
    //input.IsShown = document.getElementById("isShown").value;
    //console.log(input.Name);
    //console.log(input.Text);
    //console.log(input.IsShown);
    //xmlhttp.onreadystatechange = function () {
    //    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
    //        var jsResp = JSON.parse(xmlhttp.responseText);
    //        if (jsResp.Status == "Success") {
    //            //show success
    //        }
    //        else {
    //            //show error
    //        }
    //    }
    //}
    //xmlhttp.open("POST", url, true);
    //xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    //xmlhttp.send(JSON.stringify(input));
}