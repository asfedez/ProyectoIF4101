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
    public class FacilidadPagoADO
    {
        #region "Variables"
        private string strConexion;
        private SqlConnection sqlConexion;
        private SqlCommand sqlComando;
        #endregion

        #region "Constructor"
        public FacilidadPagoADO(string pStringConexion)
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
        public void RegistrarFacilidadPago(FacilidadPago pago)
        {
            try
            {
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Ins_FacilidadPago";
                this.sqlComando.Parameters.AddWithValue("@cedula", pago.Cedula);
                this.sqlComando.Parameters.AddWithValue("@efectivo", pago.Efectivo);
                this.sqlComando.Parameters.AddWithValue("@tarjeta", pago.Tarjeta);
                this.sqlComando.Parameters.AddWithValue("@cheque", pago.Cheque);
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


        public FacilidadPago ConsultarFacilidadPago(int cedula)
        {
            try
            {
                FacilidadPago pago = null;
                SqlDataReader lectura;
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();
                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Cns_FacilidadPago";
                this.sqlComando.Parameters.AddWithValue("@cedula", cedula);

                lectura = this.sqlComando.ExecuteReader();
                if (lectura.Read())
                {
                    pago = new FacilidadPago(Convert.ToInt32(lectura.GetValue(0)), (int)lectura.GetValue(1),
                                            (int)lectura.GetValue(2), (int)lectura.GetValue(3));
                }
                else
                {
                    return null;
                }
                return pago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ModificarFacilidadPago(FacilidadPago pago)
        {
            try
            {
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Upd_FacilidadPago";
                this.sqlComando.Parameters.AddWithValue("@cedula", pago.Cedula);
                this.sqlComando.Parameters.AddWithValue("@efectivo", pago.Efectivo);
                this.sqlComando.Parameters.AddWithValue("@tarjeta", pago.Tarjeta);
                this.sqlComando.Parameters.AddWithValue("@cheque", pago.Cheque);
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

        public Boolean EliminarFacilidadPago(FacilidadPago pago)
        {
            try
            {
                Boolean eliminado = false;
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Del_FacilidadPago";
                this.sqlComando.Parameters.AddWithValue("@cedula", pago.Cedula);
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet ListarPagos()
        {
            try
            {
                DataSet datos;
                SqlDataAdapter adaptador = new SqlDataAdapter();
                datos = new DataSet("Pagos");

                this.sqlConexion = new SqlConnection(this.strConexion);
                this.sqlComando = new SqlCommand();
                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;


                this.sqlComando.CommandText = "Select * from FacilidadPago";
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
