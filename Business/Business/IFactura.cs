using System;
using System.Collections.Generic;
using Basic;


namespace Business
{
    public interface IFactura
    {
        int NumeroFactura { get; set; }
        DateTime FechaFactura { get; set; }
        Pedido Pedido { get; set; }
    }
}