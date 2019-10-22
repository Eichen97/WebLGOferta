using System;
using System.Collections.Generic;
using Basic;

namespace Business
{
    public class Aleatorios : IAleatorio
    {
        ISingletonAleatorio ISGA = Singleton.GetInstance;
        public Usuario Usuario { get; set; }
        public string Aleatorio { get; set; }

        public void Add()
        {
            ISGA.Add(this);
        }

        public void Delete()
        {
            ISGA.Delete(this);
        }

        public void Find()
        {
            ISGA.Find(this);
        }
    }
}
