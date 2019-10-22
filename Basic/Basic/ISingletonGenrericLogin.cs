using System;
using System.Collections.Generic;


namespace Basic
{
    public interface ISingletonGenrericLogin<T>:ISingletonGeneric<T>
    {
        string Login(T Data);
        void MailExists(T Data);
        void DNIExists(T Data);
        string FindByMail(T Data);
    }
}
