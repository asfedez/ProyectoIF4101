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
    public class VentaENCADO
    {
        #region "Variables"
        private string strConexion;
        private SqlConnection sqlConexion;
        private SqlCommand sqlComando;
        #endregion

        #region "Constructor"
        public VentaENCADO(string pStringConexion)
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
        public void RegistrarEncabezado(VentaENC ventaENC)
        {
            try
            {
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Ins_ventaEnc";
                this.sqlComando.Parameters.AddWithValue("@idVenta", ventaENC.IDVenta);
                this.sqlComando.Parameters.AddWithValue("@cedula", ventaENC.Cedula);
                this.sqlComando.Parameters.AddWithValue("@fecha", ventaENC.Fecha);
                this.sqlComando.Parameters.AddWithValue("@tipoPago", ventaENC.TipoPago);
                this.sqlComando.Parameters.AddWithValue("@montoDescuento", ventaENC.MontoDescuento);
                this.sqlComando.Parameters.AddWithValue("@subtotal", ventaENC.Subtotal);
                this.sqlComando.Parameters.AddWithValue("@totalDolares", ventaENC.TotalDolares);
                this.sqlComando.Parameters.AddWithValue("@total", ventaENC.Total);
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

        public VentaENC ConsultarEncabezado(int idventa)
        {
            try
            {
                VentaENC ventaEnc = null;
                SqlDataReader lectura;
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();
                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Cns_ventaEnc";
                this.sqlComando.Parameters.AddWithValue("@idVenta", idventa);

                lectura = this.sqlComando.ExecuteReader();
                if (lectura.Read())
                {
                    ventaEnc = new VentaENC(Convert.ToInt32(lectura.GetValue(0)), Convert.ToInt32(lectura.GetValue(1)),
                                            Convert.ToDateTime(lectura.GetValue(2)), (string)lectura.GetValue(3),
                                            Convert.ToDouble(lectura.GetValue(4)), Convert.ToDouble(lectura.GetValue(5)),
                                            Convert.ToDouble(lectura.GetValue(6)), Convert.ToDouble(lectura.GetValue(7)));
                }
                return ventaEnc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Boolean EliminarEncabezado(int idVenta)
        {
            try
            {
                Boolean eliminado = false;
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Del_ventaEnc";
                this.sqlComando.Parameters.AddWithValue("@idVenta", idVenta);
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



        public int Codigo()
        {
           
            try
            { 

                 int codigo = 0;
                SqlDataReader lectura;
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();
                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandText = "select max(idventa)+1 from ventaENC"; 
                this.sqlComando.CommandType = CommandType.Text;
                lectura = this.sqlComando.ExecuteReader();
                if (lectura.Read())
                {
                    if (lectura.GetValue(0) == DBNull.Value)
                    {
                        codigo = 1;
                    }
                    else
                    {
                        codigo = Convert.ToInt32(lectura.GetValue(0));

                    }
                        
                }

             
                

                return codigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
