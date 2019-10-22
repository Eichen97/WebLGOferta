using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;

namespace Business
{
    public class ItemPedido : ClassID<ItemPedido>, IItemPedido
    {
        ISingletonItemPedido ISGIT = Singleton.GetInstance;
        public Pedido Pedido { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public string NombreProducto { get { if (Producto != null) return Producto.Nombre; else return null; } }
        public double Precio { get; set; }
        public string Url { get { if (Producto != null) return Producto.Url; else return null; } }
        public int Cant { get; set; }

        public override string Add()
        {
            ISGIT.Add(this);
            return ListToJson();
        }

        public override string Erase()
        {
            return ISGIT.Erase(this);
        }

        public override string Find()
        {
            return ISGIT.Find(this);
        }

        public override List<ItemPedido> List()
        {
            return ISGIT.List(this);
        }

        public override string ListToJson()
        {
            return ISGIT.ListToJson(this);
        }

        public void ModificarCantidad()
        {
            ISGIT.ModificarCantidad(this);
        }

        public override string Modify()
        {
            ISGIT.Modify(this);
            return ListToJson();
        }
    }
}
