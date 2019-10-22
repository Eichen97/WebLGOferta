using System;
using System.Collections.Generic;
using Basic;

namespace Business
{
    public class Producto : ClassID<Producto>, IProducto
    {
        ISingletonProducto ISP = Singleton.GetInstance;
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Url { get; set; }
        public int Stock { get; set; }
        public int Reserva { get; set; }
        public int Cant { get; set; }
        public Categoria Categoria { get; set; }
        public bool NameExists()
        {
            return ISP.NameExists(this);
        }
        public void UpdateReserva()
        {
            ISP.UpdateReserva(this);
        }
        public List<Producto> ListAvailable()
        {
            return ISP.ListAvailable(this);
        }
        public string ListAvailableToJson()
        {
            return ISP.ListAvailableToJson(this);
        }

        //CONSTRUCTOR
        public Producto()
        {
            IID IID = this;
            IID.Directory = "Productos";
            IID.Prefix = "Producto";
        }
        public override string Add()
        {
            ISP.NameExists(this);
            ISP.Add(this);
            IID IID = this;
            IID.SaveImage();
            return ListToJson();
        }

        public override string Erase()
        {
            IID IID = this;
            IID.DeleteImage();
            return ISP.Erase(this);
        }

        public override string Find()
        {
            return ISP.Find(this);
        }

        public override List<Producto> List()
        {
            return ISP.List(this);
        }

        public override string ListToJson()
        {
            return ISP.ListToJson(this);
        }

        public override string Modify()
        {
            ISP.NameExists(this);
            ISP.Modify(this);
            IID IID = this;
            IID.SaveImage();
            return ListToJson();
        }
        
    }
}
