using System;
using AccesoADatos;

public partial class _Default : System.Web.UI.Page
{
    IConexion IC = new Conexion();

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void BTInsert_Click(object sender, EventArgs e)
    {
        LBError.Text = "";
        IC.CreateCommand("Departamentos_Insert");
        IC.ParameterAddVarchar("Nombre", 40, TBNombre.Text);
        IC.ParameterAddVarchar("Descripcion", 140, TBDescripcion.Text);
        try
        {
            IC.Insert("Error, clave repetida");
            GVDepartamentos.DataBind();
        }
        catch (Exception Err)
        {
            LBError.Text = Err.Message;
        }
    }
}