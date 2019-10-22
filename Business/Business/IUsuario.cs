using System;
using System.Collections.Generic;

namespace Business
{
    public interface IUsuario
    {
        string Nombre { get; set; }
        int DNI { get; set; }
        DateTime FechaNacimiento { get; set; }
        string Direccion { get; set; }
        string Mail { get; set; }
        string Password { get; set; }
        DateTime FechaAceptacion { get; set; }
        string Rol { get; set; }
        string FindByMail();
    }
}
