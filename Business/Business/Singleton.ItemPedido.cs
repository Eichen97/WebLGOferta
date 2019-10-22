using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;

namespace Business
{
    partial class Singleton : ISingletonItemPedido

    {
        void ISingletonGeneric<ItemPedido>.Add(ItemPedido Data)
        {
            IC.CreateCommand("ItemsPedidos_Insert");
            IC.ParameterAddInt("IDPedido", Data.Pedido.ID);
            IC.ParameterAddInt("IDProducto", Data.Producto.ID);
            IC.ParameterAddInt("Cantidad", Data.Cantidad);
            Data.ID = IC.Insert("Error: No se puede insertar el Item Pedido");
        }

        string ISingletonGeneric<ItemPedido>.Erase(ItemPedido Data)
        {
            IC.CreateCommand("ItemsPedidos_Delete");
            IC.ParameterAddInt("ID", Data.ID);
            IC.Update("Error: No se pudo eliminar el Item Pedido");
            return Data.ListToJson();
        }

        string ISingletonGeneric<ItemPedido>.Find(ItemPedido Data)
        {
            IC.CreateCommand("ItemsPedidos_Find");
            IC.ParameterAddInt("ID", Data.ID);
            DataRow DR = IC.Find("Error: No se pudo encontrar el Item Pedido");
            IID IID = Data;
            ISGIT.MakeData(DR, Data);
            return IID.RowToJson(DR);
        }

        List<ItemPedido> ISingletonGeneric<ItemPedido>.List(ItemPedido Data)
        {
            List<ItemPedido> ItemsPedidos = new List<ItemPedido>();
            IC.CreateCommand("ItemsPedidos_List");
            IC.ParameterAddInt("IDPedido", Data.Pedido.ID);
            DataTable DT = IC.List("Error: No se pudo listar Items Pedidos");
            foreach (DataRow DR in DT.Rows)
            {
                ItemPedido IT = new ItemPedido();
                ISGIT.MakeData(DR, IT);
                ItemsPedidos.Add(IT);
            }
            return ItemsPedidos;
        }

        string ISingletonGeneric<ItemPedido>.ListToJson(ItemPedido Data)
        {
            IC.CreateCommand("ItemsPedidos_List");
            DataTable DT = IC.List("Error: No se pudo listar Items Pedidos");
            IID IID = Data;
            return IID.TableToJson(DT);
        }

        void ISingletonGeneric<ItemPedido>.MakeData(DataRow Dr, ItemPedido Data)
        {
            Data.ID = int.Parse(Dr["ID"].ToString());
            Data.Cantidad = int.Parse(Dr["Cantidad"].ToString());
            Data.Pedido = new Pedido();
            Data.Pedido.ID = int.Parse(Dr["IDPedido"].ToString());
            Data.Pedido.Find();
            Data.Producto = new Producto();
            Data.Producto.ID = int.Parse(Dr["IDProducto"].ToString());
            Data.Producto.Find();
            Data.Precio = Data.Producto.Precio;
        }

        void ISingletonItemPedido.ModificarCantidad(ItemPedido data)
        {
            IC.CreateCommand("ItemsPedidos_ModificarCantidad");
            IC.ParameterAddInt("IDPedido", data.Pedido.ID);
            IC.ParameterAddInt("IDProducto", data.Producto.ID);
            IC.ParameterAddInt("Cant", data.Cant);
            IC.Update("Error: No se pudo actualizar la cantidad");
        }

        void ISingletonGeneric<ItemPedido>.Modify(ItemPedido Data)
        {
            IC.CreateCommand("ItemsPedidos_Update");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddInt("IDPedido", Data.Pedido.ID);
            IC.ParameterAddInt("IDProducto", Data.Producto.ID);
            IC.ParameterAddInt("Cantidad", Data.Cantidad);
            IC.Update("Error: No se pudo modificar el Item");
        }
    }
}
