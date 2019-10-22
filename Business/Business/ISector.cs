namespace Business
{
    public interface ISector
    {
        string Nombre { get; set; }
        bool Exists();
        bool NameExists();
    }
}