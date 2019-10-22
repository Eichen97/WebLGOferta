using System;
using System.Collections.Generic;
using System.Data;
using Basic;

namespace Business
{
    partial class Singleton : ISingletonProducto
    {
        void ISingletonGeneric<Producto>.Add(Producto Data)
        {
            IC.CreateCommand("Productos_Insert");
            IC.ParameterAddVarchar("Nombre", 60, Data.Nombre);
            IC.ParameterAddFloat("Precio", Data.Precio);
            IC.ParameterAddInt("Stock", Data.Stock);
            IC.ParameterAddInt("Reserva", Data.Reserva);
            IC.ParameterAddInt("IDCategoria", Data.Categoria.ID);
            Data.ID = IC.Insert("Error: No se pudo agregar este producto");
        }

        string ISingletonGeneric<Producto>.Erase(Producto Data)
        {
            IC.CreateCommand("Productos_Delete");
            IC.ParameterAddInt("ID", Data.ID);
            IC.Update("Error: No se pudo eliminar este producto");
            return Data.ListToJson();
        }

        string ISingletonGeneric<Producto>.Find(Producto Data)
        {
            IC.CreateCommand("Productos_Find");
            IC.ParameterAddInt("ID", Data.ID);
            DataRow DR = IC.Find("Error: No se pudo encontrar este producto");
            IID IID = Data;
            ISingletonProducto ISP = this;
            ISP.MakeData(DR, Data);
            return IID.RowToJson(DR);
        }

        List<Producto> ISingletonGeneric<Producto>.List(Producto Data)
        {
            List<Producto> Productos = new List<Producto>();
            IC.CreateCommand("Productos_List");
            DataTable DT = IC.List("Error: No se pudo listar los productos");
            foreach (DataRow DR in DT.Rows)
            {
                Producto Producto = new Producto();
                ISingletonProducto ISP = this;
                ISP.MakeData(DR, Producto);
                Productos.Add(Producto);
                
            }
            return Productos;
        }


        List<Producto> ISingletonProducto.ListAvailable(Producto Data)
        {
            List<Producto> Productos = new List<Producto>();
            IC.CreateCommand("Productos_ListAvailable");
            DataTable DT = IC.List("Error: No se pudo listar los productos");
            foreach (DataRow DR in DT.Rows)
            {
                Producto Producto = new Producto();
                ISingletonProducto ISP = this;
                ISP.MakeData(DR, Producto);
                Productos.Add(Producto);

            }
            return Productos;
        }

        string ISingletonProducto.ListAvailableToJson(Producto Data)
        {
            IC.CreateCommand("Productos_ListAvailable");
            DataTable DT = IC.List("Error: No se pudo listar los productos");
            IID IID = Data;
            return IID.TableToJson(DT);
        }

        string ISingletonGeneric<Producto>.ListToJson(Producto Data)
        {
            IC.CreateCommand("Productos_List");
            DataTable DT = IC.List("Error: No se pudo listar los productos");
            IID IID = Data;
            return IID.TableToJson(DT);
        }

        void ISingletonGeneric<Producto>.MakeData(DataRow Dr, Producto Data)
        {
            Data.ID = int.Parse(Dr["ID"].ToString());
            Data.Nombre = Dr["Nombre"].ToString();
            Data.Precio = double.Parse(Dr["Precio"].ToString());
            Data.Stock = int.Parse(Dr["Stock"].ToString());
            Data.Reserva = int.Parse(Dr["Reserva"].ToString());
            Data.Categoria = new Categoria();
            Data.Categoria.ID = int.Parse(Dr["IDCategoria"].ToString());
            Data.Categoria.Find();

        }

        void ISingletonGeneric<Producto>.Modify(Producto Data)
        {
            IC.CreateCommand("Productos_Update");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddVarchar("Nombre", 60, Data.Nombre);
            IC.ParameterAddFloat("Precio", Data.Precio);
            IC.ParameterAddInt("Stock", Data.Stock);
            IC.ParameterAddInt("Reserva", Data.Reserva);
            IC.ParameterAddInt("IDCategoria", Data.Categoria.ID);
            IC.Update("Error: No se pudo modificar este producto");
        }

        bool ISingletonProducto.NameExists(Producto Data)
        {
            IC.CreateCommand("Productos_NameExists");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddVarchar("Nombre", 60, Data.Nombre);
            return IC.Exists("Error: Ya existe un producto con el mismo nombre");
        }

        void ISingletonProducto.UpdateReserva(Producto Data)
        {
            IC.CreateCommand("Productos_UpdateReserva");
            IC.ParameterAddInt("Cant", Data.Cant);
            IC.ParameterAddInt("IDProducto", Data.ID);
            IC.Update("Error: No se pudo modificar la reserva");
        }
    }
}
