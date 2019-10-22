function Pantallas() {
    this.inicio = function () {
        clearInterval(intervalo);
        if (user == undefined) {
            pantalla.login();
        }
        text = "<picture class='portadainicio' id='portada' ></picture>";
        contenedor.innerHTML = text;
        cargarImagenes();
        intervalo = setInterval(cargarImagenes, 2000);
        function cargarImagenes() {
            var urlImagen = "Imagenes/portada/merca" + Math.floor(Math.random() * 17 + 1) + ".jpg";
            var text = "<img src='" + urlImagen + "' class='imagenPortada'/>";
            document.getElementById("portada").innerHTML = text;
        }
    }
    
    this.login = function () {
        var text = "<h3>Ingresar</h3>" +
            "<input class='tb' id = 'TBMailLogin' placeholder = 'Mail:' /> " +
            "<input class='tb' id = 'TBPasswordLogin' type='password' placeholder = 'Contraseña:' />" +
            "<button class='buttonLogin left' onclick='usuario.Login();'>Ingresar</button> " +
            "<button class='buttonLogin right' onclick='usuario.cancelLogin();'>Cancelar</button> ";
        document.getElementById("containerLogin").innerHTML = text;
    }

    this.logueado = function () {
        switch (user.Rol) {
            case "No Autorizado": alert("Usuario no autorizado");
                return; break;
            case "En Proceso": alert("Usuario no autorizado");
                return; break;
            case "Administrador": navs.navAdmin(); break;
            case "Cliente": navs.navCliente(); break;
        }
        var text = "<h3>Usuario</h3>" +
            "<table class='tableLogueado'><tr><td>" + user.Nombre +
            "</td><td rowspan='2'><img src='" + user.URL + "' class='imgLogin'/></td>" +
            "</tr><tr><td>" + user.Rol + "</td></tr><tr><td colspan='2'>" +
            "<button class='buttonLogin' onclick='usuario.logout();'>Salir</button>" +
            "</td></tr></table>";
        document.getElementById("containerLogin").innerHTML = text;
        switch (user.Rol) {

            case "Administrador": navs.navAdmin();
                break;
            case "Cliente": navs.navCliente();
                break;
            default: navs.nav0();
                break;
        }
        

    }
    this.inscribirse = function () {
        clearInterval(intervalo);
        document.getElementById("containerLogin").innerHTML = "";
        var text = "<h3>Inscripción de cliente</h3>" +
            "<table class='tablePerfil'>" +
            "<tr><td>Nombre:</td><td><input class='tb' id='tbNombre'/></td></tr>" +
            "<tr><td>DNI:</td><td><input class='tb' id='tbDNI'/></td></tr>" +
            "<tr><td>F. de Nacimiento:</td><td><input class='tb' id='tbFechaNacimiento' placeholder='DD/MM/AAAA'/></td></tr>" +
            "<tr><td>Direccion:</td><td><input class='tb' id='tbDireccion'/></td></tr>" +
            "<tr><td>Mail:</td><td><input class='tb' id='tbMail'/></td></tr>" +
            "<tr><td>Contraseña:</td><td><input class='tb' id='tbPassword' type='password'/></td></tr>" +
            "<tr><td>Conf. Contraseña:</td><td><input class='tb' id='tbCheckPass' type='password'/></td></tr>" +
            "<tr><td colspan='2'><button class='bt1 left' onclick='usuario.validar1();'>Inscribirse</button>" +
            "<button class='bt1 right' onclick='usuario.cancelar();'>Cancelar</button>";

        contenedor.innerHTML = text;
    }
    this.recuperarPassword = function () {
        clearInterval(intervalo);
        var text = "<h3>Recuperar contraseña</h3>" +
            "<table class='tablePerfil'>" +
            "<tr><td>Mail:</td><td><input class='tb' id='tbMail' /></td></tr>" +
            "<tr><td colspan='2'><button class='bt1' onclick='pantalla.cambioPass();'>Continuar</button></td></tr></table>";
        contenedor.innerHTML = text;
    }
    this.cambioPass = function () {
        var Mail = document.getElementById("tbMail").value;
        var emailRegex = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
        if (!emailRegex.test(Mail)) {
            alert("Mail incorrecto");
            return;
        }
        var json = post("Default.aspx", "Accion=mailRecuperacion&Mail=" + Mail);
        if (json == "ok") {
            alert("Se ha enviado un mail a su dirección para recuperar contraseña");
            pantalla.inicio();
        }
        else {
            alert(json);
        }
    }
}