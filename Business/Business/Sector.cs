using System;
using System.Collections.Generic;
using Basic;

namespace Business
{
    public class Sector : ClassID<Sector>, ISector
    {
        ISingletonSector ISS = Singleton.GetInstance;
        public string Nombre { get ; set ; }

        public override string Add()
        {
            ISS.NameExists(this);
            ISS.Add(this);
            return ListToJson();
        }

        public override string Erase()
        {
           return ISS.Erase(this);
        }

        public bool Exists()
        {
            return ISS.Exists(this);
        }

        public override string Find()
        {
            return ISS.Find(this);
        }

        public override List<Sector> List()
        {
            return ISS.List(this);
        }

        public override string ListToJson()
        {
            return ISS.ListToJson(this);
        }

        public override string Modify()
        {
            ISS.NameExists(this);
            ISS.Modify(this);
            return ListToJson();
        }

        public bool NameExists()
        {
            return ISS.NameExists(this);
        }
    }
}
