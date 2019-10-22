using System;
using System.Collections.Generic;
using Basic;
using System.Data;

namespace Business
{
    partial class Singleton : ISingletonCategoria
    {
        void ISingletonGeneric<Categoria>.Add(Categoria Data)
        {
            IC.CreateCommand("Categorias_Insert");
            IC.ParameterAddVarchar("Nombre", 30,Data.Nombre);
            IC.ParameterAddInt("IDSector", Data.Sector.ID);
            Data.ID = IC.Insert("Error: No se pudo agregar categoria");
        }

        string ISingletonGeneric<Categoria>.Erase(Categoria Data)
        {
            IC.CreateCommand("Categorias_Delete");
            IC.ParameterAddInt("ID", Data.ID);
            IC.Update("Error: No se pudo eliminar categoria");
            return Data.ListToJson();
        }

        bool ISingletonCategoria.Exists(Categoria Data)
        {
            IC.CreateCommand("Categorias_Exists");
            try
            {
                return IC.Exists("No existen categorias");
            }
            catch (Exception)
            {

                return true;
            }
        }

        string ISingletonGeneric<Categoria>.Find(Categoria Data)
        {
            IC.CreateCommand("Categorias_Find");
            IC.ParameterAddInt("ID", Data.ID);
            ISingletonCategoria ISC = this;
            DataRow DR = IC.Find("Error: No se pudo encontrar esta categoria");
            ISC.MakeData(DR, Data);
            IID IID = Data;
            return IID.RowToJson(DR);
        }

        List<Categoria> ISingletonGeneric<Categoria>.List(Categoria Data)
        {
            List<Categoria> Categorias = new List<Categoria>();
            IC.CreateCommand("Categorias_List");
            DataTable DT = IC.List("Error: No se pudo listar categorias");
            foreach (DataRow DR in DT.Rows)
            {
                Categoria Categoria = new Categoria();
                ISingletonCategoria ISC = this;
                ISC.MakeData(DR, Categoria);
                Categorias.Add(Categoria);
            }
            return Categorias;
        }

        string ISingletonGeneric<Categoria>.ListToJson(Categoria Data)
        {
            IC.CreateCommand("Categorias_List");
            DataTable DT = IC.List("Error: No se pudo listar categorias");
            IID IID = Data;
            return IID.TableToJson(DT);
        }

        void ISingletonGeneric<Categoria>.MakeData(DataRow Dr, Categoria Data)
        {
            Data.ID = int.Parse(Dr["ID"].ToString());
            Data.Nombre = Dr["Nombre"].ToString();
            Data.Sector = new Sector();
            Data.Sector.ID = int.Parse(Dr["IDSector"].ToString());
        }

        void ISingletonGeneric<Categoria>.Modify(Categoria Data)
        {
            IC.CreateCommand("Categorias_Update");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddVarchar("Nombre", 30, Data.Nombre);
            IC.ParameterAddInt("IDSector", Data.Sector.ID);
            IC.Update("Error: No se pudo modificar esta categoria");

        }

        bool ISingletonCategoria.NameExists(Categoria Data)
        {
            IC.CreateCommand("Categorias_NameExists");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddVarchar("Nombre", 30, Data.Nombre);
            return IC.Exists("Error: Existe otra categoria con el mismo nombre");
        }
    }
}
