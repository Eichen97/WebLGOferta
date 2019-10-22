using System;
using System.Collections.Generic;
using System.Data;
using Basic;

namespace Business
{
    partial class Singleton : ISingletonAleatorio
    {
        void ISingletonAleatorio.Add(Aleatorios Data)
        {
            IC.CreateCommand("Aleatorios_Insert");
            IC.ParameterAddInt("IDUsuario", Data.Usuario.ID);
            IC.ParameterAddVarchar("Aleatorio",40, Data.Aleatorio);
            IC.Update("Error: No se pudo insertar el aleatorio");
        }

        void ISingletonAleatorio.Delete(Aleatorios Data)
        {
            IC.CreateCommand("Aleatorios_Delete");
            IC.ParameterAddVarchar("Aleatorio", 40, Data.Aleatorio);
            IC.Update("Error: No se pudo eliminar aleatorio");
        }

        void ISingletonAleatorio.Find(Aleatorios Data)
        {
            IC.CreateCommand("Aleatorios_Find");
            IC.ParameterAddVarchar("Aleatorio", 40, Data.Aleatorio);
            DataRow DR = IC.Find("Error: El usuario ya ha sido utilizado");
            Data.Usuario = new Usuario();
            Data.Usuario.ID = int.Parse(DR["IDUsuario"].ToString());
            Data.Usuario.Find();

        }
    }
}
