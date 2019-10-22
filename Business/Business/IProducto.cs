using System.Collections.Generic;

namespace Business
{
    public interface IProducto
    {
        string Nombre { get; set; }
        double Precio { get; set; }
        string Url { get; set; }
        int Stock { get; set; }
        int Reserva { get; set; }
        int Cant { get; set; }
        Categoria Categoria { get; set; }
        bool NameExists();
        void UpdateReserva();
        List<Producto> ListAvailable();
        string ListAvailableToJson();
    }
}