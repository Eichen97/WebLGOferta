using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;

namespace Business
{
    interface ISingletonItemPedido:ISingletonGeneric<ItemPedido>
    {
        void ModificarCantidad(ItemPedido data);
    }
}
