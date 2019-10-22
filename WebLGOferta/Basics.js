//teclado
function anularF5() {
    document.onkeydown = function () {

        if (window.event && window.event.keyCode == 116) {

            return false;

        }
    }
}

//AJAX
var xhttp = new XMLHttpRequest();
function post(Archname, data) {
    var p;
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            p = xhttp.responseText;
        }

    };
    xhttp.open("POST", Archname, false);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.send(data);
    return p;
}
//validacion
function validarMail(str) {
    var emailRegex = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    return emailRegex.test(str);
}
function validarFecha(str) {
    var pattern = new RegExp(/^(?:(?:(?:0?[1-9]|1\d|2[0-8])[/](?:0?[1-9]|1[0-2])|(?:29|30)[/](?:0?[13-9]|1[0-2])|31[/](?:0?[13578]|1[02]))[/](?:0{2,3}[1-9]|0{1,2}[1-9]\d|0?[1-9]\d{2}|[1-9]\d{3})|29[/]0?2[/](?:\d{1,2}(?:0[48]|[2468][048]|[13579][26])|(?:0?[48]|[13579][26]|[2468][048])00))$/);

    return (pattern.test(str));
}


