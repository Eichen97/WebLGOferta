using System;
using System.Collections.Generic;
using Basic;

namespace Business
{
    public class Factura : ClassID<Factura>, IFactura
    {
        ISingletonGeneric<Factura> ISGF = Singleton.GetInstance;
        public int NumeroFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public Pedido Pedido { get; set; }

        public override string Add()
        {
            ISGF.Add(this);
            return ListToJson();
        }

        public override string Erase()
        {
            return ISGF.Erase(this);
        }

        public override string Find()
        {
            return ISGF.Find(this);
        }

        public override List<Factura> List()
        {
            return ISGF.List(this);
        }

        public override string ListToJson()
        {
            return ISGF.ListToJson(this);
        }

        public override string Modify()
        {
            ISGF.Modify(this);
            return ListToJson();
        }
    }
}
