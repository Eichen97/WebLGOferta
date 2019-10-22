using System;
using System.Collections.Generic;
using System.Data;
using Basic;

namespace Business
{
    partial class Singleton : ISingletonGeneric<Pedido>
    {
        void ISingletonGeneric<Pedido>.Add(Pedido Data)
        {
            IC.CreateCommand("Pedidos_Insert");
            IC.ParameterAddInt("IDCliente",Data.IDCliente);
            Data.ID = IC.Insert("Error: No se pudo agregar este pedido");
        }

        string ISingletonGeneric<Pedido>.Erase(Pedido Data)
        {
            IC.CreateCommand("Pedidos_Delete");
            IC.ParameterAddInt("ID", Data.ID);
            IC.Update("Error: No se pudo eliminar este pedido");
            return Data.ListToJson();
        }

        string ISingletonGeneric<Pedido>.Find(Pedido Data)
        {
            IC.CreateCommand("Pedidos_Find");
            IC.ParameterAddInt("ID", Data.ID);
            DataRow DR = IC.Find("Error: No se pudo encontrar este pedido");
            IID IID = Data;
            ISPD.MakeData(DR, Data);
            return IID.RowToJson(DR);
        }

        List<Pedido> ISingletonGeneric<Pedido>.List(Pedido Data)
        {
            List<Pedido> Pedidos = new List<Pedido>();
            IC.CreateCommand("Pedidos_List");
            DataTable DT = IC.List("Error: No se pudo listar los productos");
            foreach (DataRow DR in DT.Rows)
            {
                Pedido Pedido = new Pedido();
                ISPD.MakeData(DR, Pedido);
                Pedidos.Add(Pedido);
            }
            return Pedidos;
        }

        string ISingletonGeneric<Pedido>.ListToJson(Pedido Data)
        {
            IC.CreateCommand("Pedidos_List");
            DataTable DT = IC.List("Error: No se pudo listar los pedidos");
            IID IID = Data;
            return IID.TableToJson(DT);
        }

        void ISingletonGeneric<Pedido>.MakeData(DataRow Dr, Pedido Data)
        {
            Data.ID = int.Parse(Dr["ID"].ToString());
            Data.IDCliente = int.Parse(Dr["IDCliente"].ToString());
            Data.Fecha = DateTime.Parse(Dr["Fecha"].ToString());
            Data.Estado = Dr["Estado"].ToString();
        }

        void ISingletonGeneric<Pedido>.Modify(Pedido Data)
        {
            IC.CreateCommand("Pedidos_Update");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddInt("IDCliente", Data.IDCliente);
            IC.ParameterAddDatetime("Fecha", Data.Fecha);
            IC.ParameterAddVarchar("Estado", 20, Data.Estado);
            IC.Update("Error: No se pudo modificar este pedido");
        }
    }
}
