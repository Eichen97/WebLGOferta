using System;
using System.Collections.Generic;
using Basic;

namespace Business
{
    public class Categoria : ClassID<Categoria>, ICategoria
    {
        ISingletonCategoria ISC = Singleton.GetInstance;
        public string Nombre { get ; set ; }

        public Sector Sector { get; set; }

        public override string Add()
        {
            ISC.NameExists(this);
            ISC.Add(this);
            return ListToJson();
        }

        public override string Erase()
        {
            return ISC.Erase(this);
        }

        public bool Exists()
        {
            return ISC.Exists(this);
        }

        public override string Find()
        {
            return ISC.Find(this);
        }

        public override List<Categoria> List()
        {
            return ISC.List(this);
        }

        public override string ListToJson()
        {
            return ISC.ListToJson(this);
        }

        public override string Modify()
        {
            ISC.NameExists(this);
            ISC.Modify(this);
            return ListToJson();
        }

        public bool NameExists()
        {
            return ISC.NameExists(this);
        }
    }
}
