using System;
using System.Collections.Generic;
using System.Data;
using Basic;

namespace Business
{
    partial class Singleton : ISingletonGenrericLogin<Usuario>
    {
        void ISingletonGeneric<Usuario>.Add(Usuario Data)
        {
            IC.CreateCommand("Usuarios_Insert");
            IC.ParameterAddVarchar("Nombre", 40, Data.Nombre);
            IC.ParameterAddInt("DNI", Data.DNI);
            IC.ParameterAddDatetime("FechaNacimiento", Data.FechaNacimiento);
            IC.ParameterAddVarchar("Direccion", 50, Data.Direccion);
            IC.ParameterAddVarchar("Mail", 50, Data.Mail);
            IC.ParameterAddVarchar("Password", 40, Data.Password);
            Data.ID = IC.Insert("Error: No se pudo insertar el usuario");

        }

        void ISingletonGenrericLogin<Usuario>.DNIExists(Usuario Data)
        {
            IC.CreateCommand("Usuarios_DNIExists");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddInt("DNI", Data.DNI);
            IC.Exists("Error: Ya existe un usuario con el DNI ingresado");
        }

        string ISingletonGeneric<Usuario>.Erase(Usuario Data)
        {
            IC.CreateCommand("Usuarios_Delete");
            IC.ParameterAddInt("ID", Data.ID);
            IC.Update("Error: No se pudo eliminar el usuario");
            return ISGLU.ListToJson(Data);
        }

        string ISingletonGeneric<Usuario>.Find(Usuario Data)
        {
            IC.CreateCommand("Usuarios_Find");
            IC.ParameterAddInt("ID", Data.ID);
            DataRow DR= IC.Find("Error: No se pudo encontrar el usuario");
            ISGLU.MakeData(DR, Data);
            IID IID = Data;
            return IID.RowToJson(DR);
        }

        string ISingletonGenrericLogin<Usuario>.FindByMail(Usuario Data)
        {
            IC.CreateCommand("Usuarios_FindByMail");
            IC.ParameterAddVarchar("Mail", 50, Data.Mail);
            DataRow DR=IC.Find("Error: No se pudo encontrar el mail en nuestro sistema");
            ISGLU.MakeData(DR, Data);
            IID IID = Data;
            return IID.RowToJson(DR);
        }

        List<Usuario> ISingletonGeneric<Usuario>.List(Usuario Data)
        {
            IC.CreateCommand("Usuarios_List");
            DataTable DT = IC.List("Error: No se pudieron listar los usuarios");
            List<Usuario> Usuarios = new List<Usuario>();
            
            foreach (DataRow DR in DT.Rows)
            {
                Usuario U = new Usuario();
                ISGLU.MakeData(DR, U);
                Usuarios.Add(U);
            }
            return Usuarios;
        }

        string ISingletonGeneric<Usuario>.ListToJson(Usuario Data)
        {
            IC.CreateCommand("Usuarios_List");
            DataTable DT = IC.List("Error: No se pudieron listar los usuarios");
            IID IID = Data;
            return IID.TableToJson(DT);
        }

        string ISingletonGenrericLogin<Usuario>.Login(Usuario Data)
        {
            IC.CreateCommand("Usuarios_Login");
            IC.ParameterAddVarchar("Mail", 50, Data.Mail);
            IC.ParameterAddVarchar("Password", 40, Data.Password);
            DataRow DR = IC.Find("Error: No se pudo iniciar sesion");
            ISGLU.MakeData(DR, Data);
            IID IID = Data;
            return IID.RowToJson(DR);
        }

        void ISingletonGenrericLogin<Usuario>.MailExists(Usuario Data)
        {
            IC.CreateCommand("Usuarios_MailExists");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddVarchar("Mail", 50, Data.Mail);
            IC.Exists("Error: El mail ya existe en otro usuario");
        }

        void ISingletonGeneric<Usuario>.MakeData(DataRow Dr, Usuario Data)
        {
            Data.ID = int.Parse(Dr["ID"].ToString());
            Data.Nombre = Dr["Nombre"].ToString();
            Data.DNI = int.Parse(Dr["DNI"].ToString());
            Data.Direccion = Dr["Direccion"].ToString();
            Data.Mail = Dr["Mail"].ToString();
            Data.Password = Dr["Password"].ToString();
            Data.FechaNacimiento = DateTime.Parse(Dr["FechaNacimiento"].ToString());
            Data.FechaAceptacion = DateTime.Parse(Dr["FechaAceptacion"].ToString());

            Data.Rol = Dr["Rol"].ToString();

        }

        void ISingletonGeneric<Usuario>.Modify(Usuario Data)
        {
            IC.CreateCommand("Usuarios_Update");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddVarchar("Nombre", 40, Data.Nombre);
            IC.ParameterAddInt("DNI", Data.DNI);
            IC.ParameterAddDatetime("FechaNacimiento", Data.FechaNacimiento);
            IC.ParameterAddVarchar("Direccion", 50, Data.Direccion);
            IC.ParameterAddVarchar("Mail", 50, Data.Mail);
            IC.ParameterAddVarchar("Password", 40, Data.Password);
            IC.ParameterAddDatetime("FechaAceptacion", Data.FechaAceptacion);
            IC.ParameterAddVarchar("Rol", 24, Data.Rol);
            IC.Update("Error: No se pudo modificar el usuario");

        }
    }
}
