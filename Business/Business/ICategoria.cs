namespace Business
{
    public interface ICategoria
    {
        string Nombre { get; set; }
        bool NameExists();
        bool Exists();
        Sector Sector { get; set; }
    }
}