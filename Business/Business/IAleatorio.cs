using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IAleatorio
    {
        void Add();
        void Find();
        void Delete();
    }
    interface ISingletonAleatorio
    {
        void Add(Aleatorios Data);
        void Find(Aleatorios Data);
        void Delete(Aleatorios Data);
    }
}
