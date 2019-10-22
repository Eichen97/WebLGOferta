using System;
using System.Collections.Generic;
using System.Data;
using Basic;

namespace Business
{
    partial class Singleton : ISingletonGeneric<Factura>
    {
        void ISingletonGeneric<Factura>.Add(Factura Data)
        {
            IC.CreateCommand("Facturas_Insert");
            IC.ParameterAddInt("NumeroFactura", Data.NumeroFactura);
            IC.ParameterAddDatetime("FechaFactura", Data.FechaFactura);
            IC.ParameterAddInt("IDPedido", Data.Pedido.ID);
            Data.ID = IC.Insert("Error: No se pudo insertar factura");
        }

        string ISingletonGeneric<Factura>.Erase(Factura Data)
        {
            IC.CreateCommand("Facturas_Delete");
            IC.ParameterAddInt("ID", Data.ID);
            IC.Update("Error: No se pudo eliminar la Factura");
            return Data.ListToJson();
        }

        string ISingletonGeneric<Factura>.Find(Factura Data)
        {
            IC.CreateCommand("Facturas_Find");
            IC.ParameterAddInt("ID", Data.ID);
            DataRow DR = IC.Find("Error: No se pudo encontrar la Factura");
            ISGF.MakeData(DR, Data);
            return Data.RowToJson(DR);
        }

        List<Factura> ISingletonGeneric<Factura>.List(Factura Data)
        {
            List<Factura> Facturas = new List<Factura>();
            IC.CreateCommand("Facturas_List");
            DataTable DT = IC.List("Error: No se pudo listar Facturas");
            foreach (DataRow DR in DT.Rows)
            {
                Factura F = new Factura();
                ISGF.MakeData(DR, F);
                Facturas.Add(F);
            }
            return Facturas;
        }

        string ISingletonGeneric<Factura>.ListToJson(Factura Data)
        {
            IC.CreateCommand("Facturas_List");
            DataTable DT = IC.List("Error: No se pudo listar Facturas");
            return Data.TableToJson(DT);
        }

        void ISingletonGeneric<Factura>.MakeData(DataRow Dr, Factura Data)
        {
            Data.ID = int.Parse(Dr["ID"].ToString());
            Data.NumeroFactura = int.Parse(Dr["NumeroFactura"].ToString());
            Data.FechaFactura = DateTime.Parse(Dr["FechaFactura"].ToString());
            Data.Pedido = new Pedido();
            Data.Pedido.ID = int.Parse(Dr["IDPedido"].ToString());
            Data.Pedido.Find();
        }

        void ISingletonGeneric<Factura>.Modify(Factura Data)
        {
            throw new NotImplementedException();
        }
    }
}
