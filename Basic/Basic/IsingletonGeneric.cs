using System;
using System.Collections.Generic;
using System.Data;

namespace Basic
{
    public interface ISingletonGeneric<T>
    {
        void Add(T Data);
        void Modify(T Data);
        string Erase(T Data);
        List<T> List(T Data);
        string Find(T Data);
        string ListToJson(T Data);
        void MakeData(DataRow Dr, T Data);
    }
}
