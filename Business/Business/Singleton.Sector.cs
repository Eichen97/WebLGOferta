using System;
using System.Collections.Generic;
using Basic;
using System.Data;

namespace Business
{
    partial class Singleton : ISingletonSector
    {
        void ISingletonGeneric<Sector>.Add(Sector Data)
        {
            IC.CreateCommand("Sectores_Insert");
            IC.ParameterAddVarchar("Nombre", 30, Data.Nombre);
            Data.ID = IC.Insert("Error: No se pudo insertar el sector");
        }

        string ISingletonGeneric<Sector>.Erase(Sector Data)
        {
            IC.CreateCommand("Sectores_Delete");
            IC.ParameterAddInt("ID", Data.ID);
            IC.Update("Error: No se pudo eliminar este sector");
            ISingletonSector ISS = this;
            return ISS.ListToJson(Data);
        }

        bool ISingletonSector.Exists(Sector Data)
        {
            IC.CreateCommand("Sectores_Exists");
            try
            {
                return IC.Exists("Error: No existen sectores");
            }
            catch (Exception)
            {
                return true;
            }
        }

        string ISingletonGeneric<Sector>.Find(Sector Data)
        {
            IC.CreateCommand("Sectores_Find");
            IC.ParameterAddInt("ID", Data.ID);
            DataRow DR = IC.Find("Error: No se pudo encontrar este sector");
            ISingletonSector ISS = this;
            ISS.MakeData(DR, Data);
            IID IID = Data;
            return IID.RowToJson(DR);
        }

        List<Sector> ISingletonGeneric<Sector>.List(Sector Data)
        {
            List<Sector> Sectores = new List<Sector>();
            IC.CreateCommand("Sectores_List");
            DataTable DT = IC.List("Error: No se pudo listar sectores");
            ISingletonSector ISS = this;
            foreach (DataRow DR in DT.Rows)
            {
                Sector Sector = new Sector();
                ISS.MakeData(DR, Sector);
                Sectores.Add(Sector);

            }
            return Sectores;
        }

        string ISingletonGeneric<Sector>.ListToJson(Sector Data)
        {
            IC.CreateCommand("Sectores_List");
            DataTable DT = IC.List("Error: No se pudo listar sectores");
            IID IID = Data;
            return IID.TableToJson(DT);
        }

        void ISingletonGeneric<Sector>.MakeData(DataRow Dr, Sector Data)
        {
            Data.ID = int.Parse(Dr["ID"].ToString());
            Data.Nombre = Dr["Nombre"].ToString();
            
        }

        void ISingletonGeneric<Sector>.Modify(Sector Data)
        {
            IC.CreateCommand("Sectores_Update");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddVarchar("Nombre", 30, Data.Nombre);
            IC.Update("Error: No se pudo modificar sector");

        }

        bool ISingletonSector.NameExists(Sector Data)
        {
            IC.CreateCommand("Sectores_NameExists");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddVarchar("Nombre", 30, Data.Nombre);
            return IC.Exists("Existe otro sector con el mismo nombre");
        }
    }
}
