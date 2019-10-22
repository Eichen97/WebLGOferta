using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;

namespace Business
{
    public interface IItemPedido:IAbstract<ItemPedido>
    {
        void ModificarCantidad();
        string Url { get; }
    }
}
