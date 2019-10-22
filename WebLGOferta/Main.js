//SE DEFINEN VARIABLES GLOBALES
var contenedor,
    contenedorLogin,
    nav,
    navs,
    pantalla,
    usuario,
    evento,
    intervalo,
    user,
    clientes,
    usuarios,
    noAutorizados,
    xhttp = new XMLHttpRequest();    
//FUNCIONES 

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
function cargarControles() {
    contenedor = document.getElementById("idContainer");
    contenedorLogin = document.getElementById("containerLogin");
    nav = document.getElementById("nav");
    navs = new Navs();
    pantalla = new Pantallas();
    usuario = new Usuarios();
    evento = new Eventos();

}
function analizarUrl() {

    var datoUrl = window.location.href.split("?");
    if (datoUrl.length > 1) {
        var data = datoUrl[1];
        datoUrl = data.split("&");
        var accion = datoUrl[0];
        switch (accion) {
            case "Accion=recuperarPass":
                usuario.cambiarPassword(data);

                break;
            case "Accion=habilitacion":

                alert(post("default.aspx", data));
                break;
        }
    }
}
//EVENTO ON LOAD
window.onload = function () {
    cargarControles();
    navs.nav0();
    pantalla.inicio();
    pantalla.login();
    analizarUrl();
}