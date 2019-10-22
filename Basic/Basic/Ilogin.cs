using System;
using System.Collections.Generic;


namespace Basic
{
   public interface Ilogin<T>:IAbstract<T>
    {
        string Login();
        string FindByMail();

    }
}
