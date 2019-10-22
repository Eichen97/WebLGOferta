function Navs() {
    this.nav0 = function () {
        text = "<button class='buttonNav left' onclick='pantalla.inicio();'>" +
            "Inicio</button>" +
            "<button class='buttonNav left' onclick='pantalla.ofertas();'>" +
            "Ofertas</button>" +
            "<button class='button' onclick='pantalla.inscribirse();'>" +
            "Inscribirse</button>" +
            "<button class='button' onclick='pantalla.recuperarPassword();'>" +
            "R. contraseña</button>";
        nav.innerHTML = text;
    }
    
    this.navAdmin = function () {
        text = "<button class='buttonNav' onclick= 'pantalla.inicio();'>Inicio</button>";
        text += "<button class='buttonNav' onclick= 'usuario.perfil();'>Perfil</button>";
        text += "<button class='buttonNav' onclick= 'usuario.autorizar();'>Autorizaciones</button>";
        text += "<button class='buttonNav' onclick='navs.menuABM();' id='btABM'>ABM</button>";
        nav.innerHTML = text;
    }
    this.navCliente = function () {
        text = "<button class='buttonNav' onclick= 'pantalla.inicio();'>Inicio</button>";
        text += "<button class='buttonNav' onclick= 'usuario.perfil();'>Perfil</button>";
        text += "<button class='buttonNav' onclick= 'evento.comprar();'>Comprar</button>";
        nav.innerHTML = text;
    }
    this.menuABM = function () {
        var B = document.getElementById("btABM");
        B.className("btNavAmpliado")
        B.innerHTML =
            "<table class='tableNav'><tr><td onclick='navs.navAdmin();'> ABM </td></tr>" +
            "<tr><td onclick='usuario.crear();'> Usuarios </td></tr>" +
            "<tr><td onclick='sector.ABM();'> Sectores </td></tr>" +
            "<tr><td onclick='categoria.ABM();'> Categorias </td></tr>" +
            "<tr><td onclick='producto.ABM();'> Productos </td></tr></table>";
    }
}