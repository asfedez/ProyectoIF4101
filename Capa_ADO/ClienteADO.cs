using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Capa_Logica;

namespace Capa_ADO
{
    public class ClienteADO
    {
        #region "Variables"
        private string strConexion;
        private SqlConnection sqlConexion;
        private SqlCommand sqlComando;
        #endregion

        #region "Constructor"
        public ClienteADO(string pStringConexion)
        {
            this.StringConexion = pStringConexion;
        }
        #endregion

        #region "Propiedades"
        public string StringConexion
        {
            get
            {
                return this.strConexion;
            }
            set
            {
                this.strConexion = value.Trim();
            }
        }
        #endregion

        #region Metodos
        public void RegistrarCliente(Cliente cliente)
        {
            try
            {
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "InsCliente";
                this.sqlComando.Parameters.AddWithValue("@cedula", cliente.Cedula);
                this.sqlComando.Parameters.AddWithValue("@nombre", cliente.Nombre);
                this.sqlComando.Parameters.AddWithValue("@telefono", cliente.Telefono);
                this.sqlComando.Parameters.AddWithValue("@direccion", cliente.Direccion);
                this.sqlComando.ExecuteNonQuery();

                this.sqlConexion.Close();
                this.sqlComando.Dispose();
                this.sqlConexion.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public Cliente ConsultarCliente(int cedula)
        {
            try
            {
                Cliente cliente = null;
                SqlDataReader lectura;
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();
                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "ConsCliente";
                this.sqlComando.Parameters.AddWithValue("@cedula", cedula);

                lectura = this.sqlComando.ExecuteReader();
                if (lectura.Read())
                {
                    cliente = new Cliente( Convert.ToInt32(lectura.GetValue(0)), (string)lectura.GetValue(1),
                                            (string)lectura.GetValue(2), (string)lectura.GetValue(3));
                }
                return cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ModificarCliente(Cliente cliente)
        {
            try
            {
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "ModCliente";
                this.sqlComando.Parameters.AddWithValue("@cedula", cliente.Cedula);
                this.sqlComando.Parameters.AddWithValue("@nombre", cliente.Nombre);
                this.sqlComando.Parameters.AddWithValue("@telefono", cliente.Telefono);
                this.sqlComando.Parameters.AddWithValue("@direccion", cliente.Direccion);
                this.sqlComando.ExecuteNonQuery();

                this.sqlConexion.Close();
                this.sqlComando.Dispose();
                this.sqlConexion.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Boolean EliminarCliente(Cliente cliente)
        {
            try
            {
                Boolean eliminado = false;
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "DelCliente";
                this.sqlComando.Parameters.AddWithValue("@cedula", cliente.Cedula);
                this.sqlComando.ExecuteNonQuery();

                this.sqlConexion.Close();
                this.sqlComando.Dispose();
                this.sqlConexion.Dispose();

                eliminado = true;

                return eliminado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet ListaClientes()
        {
            try
            {
                DataSet datos;
                SqlDataAdapter adaptador = new SqlDataAdapter();
                datos = new DataSet("Clientes");

                this.sqlConexion = new SqlConnection(this.strConexion);
                this.sqlComando = new SqlCommand();
                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;


                this.sqlComando.CommandText = "select * from cliente";
                this.sqlComando.CommandType = CommandType.Text;
                adaptador.SelectCommand = this.sqlComando;
                adaptador.Fill(datos);


                this.sqlConexion.Close();
                this.sqlComando.Dispose();
                this.sqlConexion.Dispose();
                adaptador.Dispose();



                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion

    }
}
