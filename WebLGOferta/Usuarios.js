function Usuarios() {
    this.mail;
    this.password;
    this.Login = function () {
        try {
            this.mail = document.getElementById("TBMailLogin").value;
            this.password = document.getElementById("TBPasswordLogin").value;
            validarControlesLogin();
            var data = "accion=login&Mail=" + this.mail + "&Password=" + this.password;
            var json = post("Default.aspx", data);
            user = JSON.parse(json);
            pantalla.logueado();

        } catch (e) {
            alert(e);
        }
        this.logout = function () {
            user = undefined;
            pantalla.inicio();
            navs.nav0();
            pantalla.login();
        }
        function validarControlesLogin() {
            var regPassword = /^[\w\W\d]+$/;
            var emailRegex = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);

            if (usuario.mail == "")
                throw "Error: No se ingresó mail";

            if (!emailRegex.test(usuario.mail))
                throw "Error: Mail incorrecto";
            if (!regPassword.test(usuario.password))
                throw "Error: Contraseña no ingresada";
        }
    }
    this.perfil = function () {
        clearInterval(intervalo);
        var text = '<form action="Default.aspx" method="post" enctype="multipart/form-data" target="iframe"> ' +
            '<input type="hidden" id="HFAccion" name="HFAccion" value="modifyUser" /> ' +
            '<input type="hidden" id="HFID" name="HFID" value="' + user.ID + '" /> ' +
            '<input type="hidden" id="HFRol" name="HFRol" value="' + user.Rol + '" /> '+
            '<input type="hidden" id="HFFechaAceptacion" name="HFFechaAceptacion" value="' + user.FechaAceptacion + '" /> ' +
            '<table class="tablePerfil"> ' +
            '<tr><td colspan="2"><h3>Modificar Perfil</h3></td></tr> ' +
            '<tr><td>Nombre:</td><td><input class="tb" id="tbNombre" name="tbNombre" /></td></tr> ' +
            '<tr><td>DNI:</td><td><input class="tb" id="tbDNI" name="tbDNI" /></td></tr> ' +
            '<tr><td>Fecha Nac. (DD/MM/AAAA):</td><td><input class="tb" id="tbFechaNacimiento" name="tbFechaNacimiento" /></td></tr> ' +
            '<tr><td>Direccion:</td><td><input class="tb" id="tbDireccion" name="tbDireccion" /></td></tr> ' +
            '<tr><td>Mail:</td><td><input class="tb" id="tbMail" name="tbMail" /></td></tr> ' +
            '<tr><td>Contraseña:</td><td><input type="password" class="tb" id="tbPassword" name="tbPassword" placeholder="Ingresar sólo en caso de modificar" /></td></tr> ' +
            '<tr><td>Conf. contras.:</td><td><input type="password" class="tb" id="tbCheckPass" name="tbCheckPass" placeholder="Ingresar sólo en caso de modificar" /></td></tr> ' +
            '<tr><td>Rol</td><td>' + user.Rol + '</td></tr>' +
            '<tr><td>Imagen</td><td><input type="file" class="tb" name="file" /></td></tr> ' +
            '<tr><td><div class="bt" onclick="usuario.validar();">Modificar</div></td><td><input type="reset" class="bt" value="Cancelar" /></td></tr> ' +
            '</table> ' +
            '</form> ' +
            '<iframe id="iframe" name="iframe" style="display:none;" onload="usuario.iframeLoad();"></iframe> ';
        contenedor.innerHTML = text;
        document.getElementById("tbNombre").value = user.Nombre;
        document.getElementById("tbDNI").value = user.DNI;
        document.getElementById("tbFechaNacimiento").value = user.FechaNacimiento.substring(0,10);
        document.getElementById("tbDireccion").value = user.Direccion;
        document.getElementById("tbMail").value = user.Mail;
        document.getElementById("HFID").value = user.ID;
    }
    this.validar = function () {
        var Nombre = document.getElementById("tbNombre").value;
        var DNI = document.getElementById("tbDNI").value;
        var FechaNacimiento = document.getElementById("tbFechaNacimiento").value;
        var Direccion = document.getElementById("tbDireccion").value;
        var Mail = document.getElementById("tbMail").value;
        var Password = document.getElementById("tbPassword").value;
        var CheckPass = document.getElementById("tbCheckPass").value;
        if (Nombre == "" || DNI == "" || FechaNacimiento == "" || Direccion == "" || Mail == "") {
            alert("Hay campos vacios que completar");
            return;
        }
        DNI = parseInt(DNI);
        if (isNaN(DNI)) {
            alert("DNI incorrecto");
            return;
        }
        if (DNI < 4000000 || DNI > 100000000) {
            alert("DNI fuera de rango");
            return;
        }
        if (Password != CheckPass) {
            alert("No coinciden las contraseñas");
            return;
        }
            var emailRegex = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
        var fechaRegex = new RegExp(/^(?:(?:(?:0?[1-9]|1\d|2[0-8])[/](?:0?[1-9]|1[0-2])|(?:29|30)[/](?:0?[13-9]|1[0-2])|31[/](?:0?[13578]|1[02]))[/](?:0{2,3}[1-9]|0{1,2}[1-9]\d|0?[1-9]\d{2}|[1-9]\d{3})|29[/]0?2[/](?:\d{1,2}(?:0[48]|[2468][048]|[13579][26])|(?:0?[48]|[13579][26]|[2468][048])00))$/);
        if (!emailRegex.test(Mail)) {
            alert("Mail incorrecto");
            return;
        }
        if (!fechaRegex.test(FechaNacimiento)) {
            alert("Fecha de nacimiento incorrecta");
            return;
        }
        document.forms[0].submit();
        usuario.cancelar();
    }
    this.validar1 = function () {
        var Nombre = document.getElementById("tbNombre").value;
        var DNI = document.getElementById("tbDNI").value;
        var FechaNacimiento = document.getElementById("tbFechaNacimiento").value;
        var Direccion = document.getElementById("tbDireccion").value;
        var Mail = document.getElementById("tbMail").value;
        var Password = document.getElementById("tbPassword").value;
        var CheckPass = document.getElementById("tbCheckPass").value;
        if (Password == "") {
            alert("Hay que completar contraseña");
            return;
        }
        if (Nombre == "" || DNI == "" || FechaNacimiento == "" || Direccion == "" || Mail == "") {
            alert("Hay campos vacios que completar");
            return;
        }
        DNI = parseInt(DNI);
        if (isNaN(DNI)) {
            alert("DNI incorrecto");
            return;
        }
        if (DNI < 4000000 || DNI > 100000000) {
            alert("DNI fuera de rango");
            return;
        }
        if (Password != CheckPass) {
            alert("No coinciden las contraseñas");
            return;
        }
        var emailRegex = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
        var fechaRegex = new RegExp(/^(?:(?:(?:0?[1-9]|1\d|2[0-8])[/](?:0?[1-9]|1[0-2])|(?:29|30)[/](?:0?[13-9]|1[0-2])|31[/](?:0?[13578]|1[02]))[/](?:0{2,3}[1-9]|0{1,2}[1-9]\d|0?[1-9]\d{2}|[1-9]\d{3})|29[/]0?2[/](?:\d{1,2}(?:0[48]|[2468][048]|[13579][26])|(?:0?[48]|[13579][26]|[2468][048])00))$/);
        if (!emailRegex.test(Mail)) {
            alert("Mail incorrecto");
            return;
        }
        if (!fechaRegex.test(FechaNacimiento)) {
            alert("Fecha de nacimiento incorrecta");
            return;
        }
        var data = "Accion=ingresarUsuario&Nombre=" + Nombre + "&DNI=" + DNI + "&FechaNacimiento=" + FechaNacimiento +
            "&Direccion=" + Direccion + "&Mail=" + Mail + "&Password=" + Password;
        var json = post("Default.aspx", data);
        alert(json);
        user = undefined;
        pantalla.inicio();
    }

    this.cancelar = function () {
        document.getElementById("tbNombre").value = "";
        document.getElementById("tbDNI").value = "";
        document.getElementById("tbFechaNacimiento").value = "";
        document.getElementById("tbDireccion").value = "";
        document.getElementById("tbMail").value = "";
        document.getElementById("tbPassword").value ="";
        document.getElementById("tbCheckPass").value = "";
        document.getElementsByName("file")[0].value = "";


    }
    this.iframeLoad = function () {
        var ifr = document.getElementById("iframe");
        var SPJson = ifr.contentWindow.document.getElementById("MiSpan");
        if (SPJson != undefined) {
            var strJson = ifr.contentWindow.document.getElementById("MiSpan").innerHTML;
            try {
                user = JSON.parse(strJson);
                pantalla.logueado();
            } catch (e) { alert(strJson); return; }

        }

    }
    this.autorizar = function () {
        clearInterval(intervalo);
        var json = post("Default.aspx", "accion=listarUsuarios");



        try {
            usuarios = JSON.parse(json);
            clientes = new Array();
            noAutorizados = new Array();
            for (var i = 0; i < usuarios.length; i++) {
                if (usuarios[i].Rol == "No Autorizado") {
                    noAutorizados[noAutorizados.length] = usuarios[i];

                }
                if (usuarios[i].Rol == "Cliente" || usuarios[i].Rol=="En Proceso") {
                    clientes[clientes.length] = usuarios[i];
                }

            }

            var text = "<h3>Usuarios no autorizados</h3>" +
                "<div id='containernoAutorizados' class='containerAutorizacion'></div>" +
                "<h3>En Proceso o Clientes</h3>" +
                "<div id='containerClientes' class='containerAutorizacion'></div>";
            contenedor.innerHTML = text;

            var flistnoAutorizado = function (pos0, lista, filas) {
                var text = "";
                for (var i = 0; i < lista.length && i < pos0 + filas; i++) {
                   text+="<tr><td>" + lista[i].Mail + "</td><td>" +
                        lista[i].Nombre + "</td><td><img src='Imagenes/autorizar.jpg' class='icon' onclick='usuario.AutorizarUsuario(" + i + ");'/>" +
                        "</td></tr>";
                }
                return text;

            }
            if (noAutorizados.length > 0) {
                Tables("containernoAutorizados", 3, 3, noAutorizados, "<th>Mail</th><th>Nombre</th><th>Autorizar</th>", flistnoAutorizado);
            }
            var flistClientes = function (pos0, lista, filas) {
                var text = "";
                for (var i = 0; i < lista.length && i < pos0 + filas; i++) {
                text+="<tr><td>" + lista[i].Mail + "</td><td>" +
                        lista[i].Nombre +
                        "</td><td>" + lista[i].FechaAceptacion +
                        "</td><td><img src='Imagenes/desautorizar.jpg' class='icon' onclick='usuario.DesautorizarUsuario(" + i + ");'/></td>" +
                        "</td><td><img src='Imagenes/eliminar.jpg' class='icon' onclick='usuario.EliminarUsuario(" + i + ");'/>" +
                        "</td></tr>";
                   
                }
                return text;
            }
            if (clientes.length > 0) {
                Tables("containerClientes", 5, 3, clientes, "<th>Mail</th><th>Nombre</th><th>Fecha Aceptacion</th><th>Desautorizar</th><th>Eliminar</th>", flistClientes);
            }
            

            //function Tables(idcontainer, colums, rows, list, head, flist) {
            //function Tablas(idContenedor, cantFilas, cantCols, imprimirRegistros, listas, header) {
        } catch (e) {
            alert(e);
            return;
        }
    }
    this.AutorizarUsuario = function (i) {


        i = parseInt(i);
        var data = "accion=autorizarUsuario&idUsuario=" + noAutorizados[i].ID;
        var json = post("Default.aspx", data);
        if (json == "ok") {
            this.autorizar();
        }
        else {
            alert(json);
        }
    }
    this.DesautorizarUsuario = function (i) {
        i = parseInt(i);
        var data = "accion=desautorizarUsuario&idUsuario=" + clientes[i].ID;
        var json = post("Default.aspx", data);
        if (json == "ok") {
            this.autorizar();
        }
        else {
            alert(json);
        }

    }
    this.EliminarUsuario = function (i) {
        i = parseInt(i);
        var data = "accion=eliminarUsuario&idUsuario=" + clientes[i].ID;
        var json = post("Default.aspx", data);
        if (json == ok) {
            this.autorizar();
        }
        else {
            alert(json);
        }

    }
    this.cambiarPassword = function (data) {
        clearInterval(intervalo);
        var Json1 = post("Default.aspx", data);
        try {
            user = JSON.parse(Json1);
        } catch (e) {
            alert(Json1);
            return;
        }

        var text = "<h1>Recuperar contraseña de:" + user.Nombre + "</h1>" +
            "<table class='tablePerfil'>" +
            "<tr><td>Contraseña: </td><td><input type='password' class='tb' id='tbPassword'/></td></tr>" +
            "<tr><td>Confirmar contraseña: </td><td><input type='password' class='tb' id='tbPasswordConfirm'/></td></tr>" +
            "<tr><td colspan='2'><button class='bt' onclick='usuario.confirmPass();'>Modificar contraseña</button></td></tr></table>";
        contenedor.innerHTML = text;

    }
    this.confirmPass = function () {
        var pass = document.getElementById("tbPassword").value;
        var passC = document.getElementById("tbPasswordConfirm").value;
        if (pass != passC) {
            alert("Las contraseñas no coinciden");
            return;
        }
        var json = post("Default.aspx", "Accion=changePass&Password=" + pass + "&IDUsuario=" + user.ID);
        if (json == "ok") {
            alert("Se ha cambiado su contraseña, puede iniciar sesion");
            user = undefined;
            pantalla.inicio();
        }
        else {
            alert(json);
        }
 

    }
}