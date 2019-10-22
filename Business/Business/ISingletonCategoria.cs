using System;
using System.Collections.Generic;
using Basic;

namespace Business
{
    interface ISingletonCategoria:ISingletonGeneric<Categoria>
    {
        bool NameExists(Categoria Data);
        bool Exists(Categoria Data);
    }
}
