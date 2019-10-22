using System;
using System.Collections.Generic;
using Basic;

namespace Business
{
    interface ISingletonSector:ISingletonGeneric<Sector>
    {
        bool NameExists(Sector Data);
        bool Exists(Sector Data);
    }
}
