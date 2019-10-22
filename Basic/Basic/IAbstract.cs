using System;
using System.Collections.Generic;


namespace Basic
{
   public interface IAbstract<T>
    {
        string Add();
        string Erase();
        string Modify();
        string Find();
        List<T> List();
        string ListToJson();

    }
}
