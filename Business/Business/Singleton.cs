using System;
using System.Collections.Generic;
using AccesoADatos;
using Basic;

namespace Business
{
   partial class Singleton:Conexion
    {
        IConexion IC;
        ISingletonGenrericLogin<Usuario> ISGLU;
        ISingletonGeneric<Pedido> ISPD;
        ISingletonItemPedido ISGIT;
        ISingletonGeneric<Factura> ISGF;
        static Singleton Instance = new Singleton();
        private Singleton()
        {
            ISGLU = this;
            IC = this;
            ISPD = this;
            ISGIT = this;
            ISGF = this;
        }
        public static Singleton GetInstance { get { return Instance; } }


    }
}
