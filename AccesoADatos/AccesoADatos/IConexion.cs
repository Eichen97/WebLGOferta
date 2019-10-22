using System;
using System.Data;

namespace AccesoADatos
{
    public interface IConexion
    {
        bool Exists(string ErrMessage);
        /// <summary>
        /// Inserta en la base de datos un elemento.
        /// </summary>
        /// <param name="ErrMessage">Mensaje de error que va a verse.</param>
        /// <returns>Devuelve el ID de el registro ingresado.</returns>
        int Insert(string ErrMessage);
        /// <summary>
        /// Modifica un registro en la base de datos.
        /// </summary>
        /// <param name="ErrMessage">Mensaje de error que va a verse.</param>
        void Update(string ErrMessage);
        /// <summary>
        /// Ejecuta un listado.
        /// </summary>
        /// <param name="ErrMessage">Mensaje de error que va a verse.</param>
        /// <returns>Retorna el listado en un DataTable</returns>
        DataTable List(string ErrMessage);
        /// <summary>
        /// Busca un registro.
        /// </summary>
        /// <param name="ErrMessage">Mensaje de error que va a verse.</param>
        /// <returns>Devuelve en un DataTable el listado del proyecto.</returns>
        DataRow Find(string ErrMessage);
        /// <summary>
        /// Genera un comando de tipo StoreProcedure.
        /// </summary>
        /// <param name="StoreProcedureName">Nombre del StoreProcedure</param>
        void CreateCommand(string StoreProcedureName);
        /// <summary>
        /// Agrega un parametro al comando de tipo int.
        /// </summary>
        /// <param name="Name">Nombre del parametro que esta en el store procedure sin @</param>
        /// <param name="Value">Valor que toma el parametro.</param>
        void ParameterAddInt(string Name, int Value);
        /// <summary>
        /// Agrega un parametro de tipo varchar.
        /// </summary>
        /// <param name="Name">Nombre del parametro que esta en el store procedure sin @</param>
        /// <param name="Lenght">Largo del varchar.</param>
        /// <param name="Value">Valor que toma el parametro.</param>
        void ParameterAddVarchar(string Name, int Lenght, string Value);
        /// <summary>
        /// Agrega un parametro de tipo booleano que esta en el StoreProcedure
        /// </summary>
        /// <param name="Name">Nombre del parametro que esta en el store procedure sin @</param>
        /// <param name="Value">Valor que toma el parametro.</param>
        void ParameterAddBool(string Name, bool Value);
        /// <summary>
        /// Agrega un parametro de tipo flotante.
        /// </summary>
        /// <param name="Name">Nombre del parametro que esta en el store procedure sin @</param>
        /// <param name="Value">Valor que toma el parametro.</param>
        void ParameterAddFloat(string Name, double Value);
        /// <summary>
        /// Parametro de tipo texto que se guarda como un archivo de texto en la base de datos.
        /// </summary>
        /// <param name="Name">Nombre del parametro que esta en el store procedure sin @</param>
        /// <param name="Value">Valor que toma el parametro.</param>
        void ParameterAddText(string Name, string Value);
        /// <summary>
        /// Agrega un valor de tipo fecha.
        /// </summary>
        /// <param name="Name">Nombre del parametro que esta en el store procedure sin @</param>
        /// <param name="Value">Valor que toma el parametro.</param>
        void ParameterAddDatetime(string Name, DateTime Value);
        /// <summary>
        /// Abre la conexion.
        /// </summary>
        void OpenConnection();

    }
}
