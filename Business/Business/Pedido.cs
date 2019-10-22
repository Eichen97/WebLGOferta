using System;
using System.Collections.Generic;
using Basic;

namespace Business
{
    public class Pedido : ClassID<Pedido>,IPedido
    {
        ISingletonGeneric<Pedido> ISPD = Singleton.GetInstance;
        public int IDCliente { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }

        public override string Add()
        {
            ISPD.Add(this);
            return ListToJson();
        }

        public override string Erase()
        {
            return ISPD.Erase(this);
        }

        public override string Find()
        {
            return ISPD.Find(this);
        }

        public void InicializarPedido()
        {
            List<Pedido> Pedidos = this.List();
            this.Estado = "Iniciado";
            foreach (Pedido P in Pedidos)
            {
                if (this.IDCliente == P.IDCliente)
                {
                    if (P.Estado == "Iniciado")
                    {
                        if (DateTime.Compare(P.Fecha.AddMinutes(30), DateTime.Now) < 0)
                        {
                            ItemPedido IP = new ItemPedido();
                            List<ItemPedido> ItemsAnulados = new List<ItemPedido>();
                            IP.Pedido = P;
                            ItemsAnulados = IP.List();
                            foreach (ItemPedido item in ItemsAnulados)
                            {
                                item.Producto.Reserva -= item.Cantidad;
                                item.Producto.Modify();
                            }
                            P.Estado = "Anulado";
                            P.Modify();
                            this.Add();
                            return;
                        }
                        else
                        {
                            this.ID = P.ID;
                            this.Find();
                            return;
                        }
                    }
                }
            }
            this.Add();
        }

        public override List<Pedido> List()
        {
            return ISPD.List(this);
        }

        public override string ListToJson()
        {
            return ISPD.ListToJson(this);
        }

        public override string Modify()
        {
            ISPD.Modify(this);
            return ListToJson();
        }
    }
}
