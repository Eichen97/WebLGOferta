using System;
using System.Collections.Generic;
using Basic;


namespace Business
{
    interface ISingletonProducto:ISingletonGeneric<Producto>
    {
        bool NameExists(Producto Data);
        void UpdateReserva(Producto Data);
        List<Producto> ListAvailable(Producto Data);
        string ListAvailableToJson(Producto Data);
    }
}
