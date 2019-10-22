using System;
using System.Collections.Generic;


namespace Basic
{
    public abstract class ClassIDLogin<T> : ClassID<T>, Ilogin<T>
    {
        public string FindByMail()
        {
            throw new NotImplementedException();
        }

        public abstract string Login();
    }
}
