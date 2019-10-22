using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Basic;
using System.Net;
using System.Net.Mail;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    Usuario Usuario = new Usuario();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request["Accion"]!=null)
        {
            switch(Request["Accion"])
            {
                case "changePass":
                    changePass();
                    break;
                case "mailRecuperacion":
                    recPassword();
                    break;
                case "recuperarPass":
                    recuperarPassword();
                    break;
                case "habilitacion":
                    habilitarUser();
                   break;
                   
            }
        }

        if(Request["HFAccion"]!=null)
        {
            switch (Request["HFAccion"])
            {
                case "modifyUser":
                    modifyUser();
                    break;
                case "listarUsuarios":
                    listUsuarios();
                    break;
            }
        }
        if(Request["accion"]!=null)
        {
            switch (Request["accion"])
            {
                case "login":
                    login();
                    break;
                case "ingresarUsuario":
                    ingresarUsuario();
                    break;
                case "listarUsuarios":
                    listUsuarios();
                    break;
                case "autorizarUsuario":
                    autorizarUsuario();
                    break;
                case "desautorizarUsuario":
                    desautorizarUsuario();
                    break;
                case "eliminarUsuario":
                    eliminarUsuario();
                    break;
                default:
                    break;
            }
        }
    }

    private void changePass()
    {
        Usuario.ID = int.Parse(Request["IDUsuario"].ToString());
        Usuario.Find();
        Usuario.Password = Request["Password"];
        try
        {
            Usuario.Modify();
            Response.Write("ok");
        }
        catch (Exception err)
        {

            Response.Write(err.Message);
        }
    }

    private void recuperarPassword()
    {
        try
        {
            Aleatorios A = new Aleatorios();
            A.Aleatorio = Request["Aleatorio"];
            A.Find();
            Usuario = A.Usuario;
            A.Delete();
            Response.Write(A.Usuario.Find());
        }
        catch (Exception err)
        {

            Response.Write(err.Message);
        }

    }

    private void habilitarUser()
    {
        try
        {
            Aleatorios A = new Aleatorios();
            A.Aleatorio = Request["Aleatorio"];
            A.Find();
            Usuario = A.Usuario;
            A.Delete();
            Usuario.Rol = "Cliente";
            Usuario.Modify();
            Response.Write("Usted ha sido habilitado, puede ingresar sus datos");
        }
        catch (Exception err)
        {
            Response.Write(err.Message);
        }
        
    }

    private void recPassword()
    {
        try
        {
            Usuario.Mail = Request["Mail"];
            Usuario.FindByMail();
            EnviarMail("recuperarPass");
            Response.Write("ok");
        }
        catch (Exception err)
        {

            Response.Write(err.Message);
        }

    }

    private void eliminarUsuario()
    {
        Usuario.ID = int.Parse(Request["idUsuario"].ToString());
        try
        {
            Usuario.Erase();
            Response.Write("ok");
        }
        catch (Exception err)
        {
            Response.Write(err.Message);
           
        }
    }

    private void desautorizarUsuario()
    {
        Usuario.ID = int.Parse(Request["idUsuario"].ToString());
        Usuario.Find();
        Usuario.Rol = "No Autorizado";
        try
        {
            Usuario.Modify();
            Response.Write("ok");
        }
        catch (Exception err)
        {

            Response.Write(err.Message);
        }
    }

    private void autorizarUsuario()
       
    {
        try
        { 
        Usuario.ID = int.Parse(Request["idUsuario"].ToString());
        Usuario.Find();
        Usuario.Rol = "En Proceso";
        Usuario.FechaAceptacion = DateTime.Now;
        Usuario.Password = "";
        Usuario.Modify();
        EnviarMail("habilitar");
        Response.Write("ok");
        }
        catch (Exception err)
        {

            Response.Write(err.Message);
        }
    }

    private void EnviarMail(string value)
    {
        MailMessage Mail = new MailMessage();
        Aleatorios A = new Aleatorios();
        A.Usuario = Usuario;
        A.Aleatorio = Usuario.Encriptar(Usuario.ID + "U");
        try
        {
            A.Add();
            if (value == "habilitar")
            {
                HabilitarUsuario(Mail, A);
            }
            else
            {
                RecuperarPassword(Mail, A);
            }
        }
        catch (Exception err)
        {
            Response.Write(err.Message);
        }
    }

    private void RecuperarPassword(MailMessage mail, Aleatorios a)
    {
        string Subject = "Recuperar contraseña de " + Usuario.Nombre;
        string Body = "La Gran Oferta ha autorizado a " + Usuario.Nombre + " a recuperar su contraseña "  +
            "<br/>Deberas ingresar a nuestro sitio para terminar el tramite pulsando <br/>" +
            "<a href='" + ConfigurationManager.AppSettings["URL"] + "?Accion=recuperarPass&Aleatorio=" + a.Aleatorio + "'>Ingresar a la gran oferta</a>";
        SendMail(mail, Body, Subject);
    }

    private void HabilitarUsuario(MailMessage mail, Aleatorios a)
    {
        string Subject = "Habilitar " + Usuario.Nombre;
        string Body = "La Gran Oferta ha decidido habilitar como cliente a: " + Usuario.Nombre +
            "<br/>Deberas ingresar a nuestro sitio para terminar el tramite pulsando <br/>" +
            "<a href='" + ConfigurationManager.AppSettings["URL"] + "?Accion=habilitacion&Aleatorio=" + a.Aleatorio + "'>Ingresar a la gran oferta</a>";
        SendMail(mail,Body,Subject);
    }

    private void SendMail(MailMessage Mail, string Body, string Subject)
    {
        Mail.From = new MailAddress(ConfigurationManager.AppSettings["FROM"]);
        Mail.To.Add(new MailAddress(Usuario.Mail));
        Mail.Subject = Subject;
        Mail.Body = Body;
        Mail.IsBodyHtml = true;
        Mail.Priority = MailPriority.Normal;
        SmtpClient Client = new SmtpClient("smtp.gmail.com", 587);
        string From = ConfigurationManager.AppSettings["FROM"];
        string Pass = ConfigurationManager.AppSettings["PASS"];
        Client.Credentials = new NetworkCredential(From, Pass);
        Client.EnableSsl = true;
        //Client.UseDefaultCredentials = false;
        Client.Send(Mail);
        Mail.Dispose();
    }

    private void ingresarUsuario()
    {
        Usuario.Nombre = Request["Nombre"].ToString();
        Usuario.DNI = int.Parse(Request["DNI"].ToString());
        Usuario.FechaNacimiento = DateTime.Parse(Request["FechaNacimiento"].ToString());
        Usuario.Direccion = Request["Direccion"].ToString();
        Usuario.Mail = Request["Mail"].ToString();
        Usuario.Password = Request["Password"].ToString();
        try
        {
            Usuario.Add();
            Response.Write("Usted ha ingresado sus datos correctamente\n Nos comunicaremos a la brevedad");
        }
        catch (Exception err)
        {

            Response.Write(err.Message);
        }
        

    }

    private void listUsuarios()
    {
        try
        {
            Response.Write(Usuario.ListToJson());
        }
        catch (Exception err)
        {

            Response.Write(err.Message);
        }
    }

    private void login()
    {
        Usuario.Mail = Request["Mail"].ToString();
        Usuario.Password = Request["Password"].ToString();
        try
        {
            string json = Usuario.Login();
            Response.Write(json);
        }
        catch (Exception err)
        {
            Response.Write(err.Message);
          
        }
    }
    private void modifyUser()
    {
        Usuario.ID = int.Parse(Request["HFID"].ToString());
        Usuario.Nombre = Request["tbNombre"].ToString();
        Usuario.DNI = int.Parse(Request["tbDNI"].ToString());
        Usuario.FechaNacimiento = DateTime.Parse(Request["tbFechaNacimiento"].ToString());
        Usuario.Direccion = Request["tbDireccion"].ToString();
        Usuario.Mail = Request["tbMail"].ToString();
        Usuario.Password = Request["tbPassword"].ToString();
        Usuario.Rol = Request["HFRol"].ToString();
        Usuario.FechaAceptacion = DateTime.Parse(Request["HFFechaAceptacion"].ToString());
        if (Request.Files.Count > 0)
        {
            IID IID = Usuario;
            IID.FU = Request.Files[0];
        }
        try
        {
            Usuario.Modify();
            Response.Write("<span id=\"MiSpan\">" +  Usuario.Find() + "</span>");
            
        }
        catch (Exception err)
        {
            Response.Write("<span id=\"MiSpan\">" + err.Message + "</span>");

        }
    }
}