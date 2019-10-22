using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AccesoADatos
{
    public class Conexion : IConexion
    {
        SqlConnection MiConexion = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString);
        IConexion IC = null;
        SqlCommand MiComando = null;
        
        public Conexion ()
        {
            IC = this;
        }

        void IConexion.CreateCommand(string StoreProcedureName)
        {
            MiComando = new SqlCommand(StoreProcedureName, MiConexion);
            MiComando.CommandType = CommandType.StoredProcedure;
        }

        int IConexion.Insert(string ErrMessage)
        {
            IC.OpenConnection();            
            try
            {
                int i = int.Parse(MiComando.ExecuteScalar().ToString());
                return i;
            }
            catch (Exception)
            {

                throw new Exception(ErrMessage);
            }
            finally
            {
                MiConexion.Close();
            }
        }

        DataRow IConexion.Find(string ErrMessage)
        {
            IC.OpenConnection();
            DataTable MiTabla = new DataTable();

            try
            {
                MiTabla.Load(MiComando.ExecuteReader());
                return MiTabla.Rows[0];
            }
            catch (Exception err)
            {

                throw new Exception(ErrMessage);
            }
            finally
            {
                MiConexion.Close();
            }
        }
        
        DataTable IConexion.List(string ErrMessage)
        {
            IC.OpenConnection();
            DataTable MiTabla = new DataTable();

            try
            {
                MiTabla.Load(MiComando.ExecuteReader());
                return MiTabla;
            }
            catch (Exception)
            {

                throw new Exception(ErrMessage);
            }
            finally
            {
                MiConexion.Close();
            }
        }

        void IConexion.OpenConnection()
        {
            if (MiConexion.State != ConnectionState.Open)
            {
                try
                {
                    MiConexion.Open();
                }
                catch (Exception)
                {
                    throw new Exception("ERROR: No se pudo abrir la conexion.");
                }
            }
        }

        void IConexion.ParameterAddBool(string Name, bool Value)
        {
            MiComando.Parameters.Add("@" + Name, SqlDbType.Bit).Value = Value;
        }

        void IConexion.ParameterAddDatetime(string Name, DateTime Value)
        {
            MiComando.Parameters.Add("@" + Name, SqlDbType.SmallDateTime).Value = Value.ToString();
        }

        void IConexion.ParameterAddFloat(string Name, double Value)
        {
            MiComando.Parameters.Add("@" + Name, SqlDbType.Float).Value = Value;
        }

        void IConexion.ParameterAddInt(string Name, int Value)
        {
            MiComando.Parameters.Add("@" + Name, SqlDbType.Int).Value = Value;
        }

        void IConexion.ParameterAddText(string Name, string Value)
        {
            MiComando.Parameters.Add("@" + Name, SqlDbType.Text).Value = Value;
        }

        void IConexion.ParameterAddVarchar(string Name, int Length, string Value)
        {
            MiComando.Parameters.Add("@" + Name, SqlDbType.VarChar, Length).Value = Value;
        }

        void IConexion.Update(string ErrMessage)
        {
            IC.OpenConnection();

            try
            {
                MiComando.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw new Exception(ErrMessage);
            }
            finally
            {
                MiConexion.Close();
            }
        }

        bool IConexion.Exists(string ErrMessage)
        {
            IC.OpenConnection();
            try
            {
                DataTable DT = new DataTable();
                DT.Load(MiComando.ExecuteReader());
                if (DT.Rows.Count == 0)
                    return false;
                else
                    throw new Exception();
            }
            catch (Exception err)
            {

                throw new Exception(ErrMessage);
            }
            finally
            {
                MiConexion.Close();
            }
        }
    }
}
