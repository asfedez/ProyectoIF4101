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
    public class VentaDetalleADO
    {
        #region "Variables"
        private string strConexion;
        private SqlConnection sqlConexion;
        private SqlCommand sqlComando;
        #endregion

        #region "Constructor"
        public VentaDetalleADO(string pStringConexion)
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
        public void RegistrarDetalle(VentaDET ventaDET)
        {
            try
            {
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Ins_ventaDET";
                this.sqlComando.Parameters.AddWithValue("@idVenta", ventaDET.Idventa);
                this.sqlComando.Parameters.AddWithValue("@idMoto", ventaDET.Idmoto);
                this.sqlComando.Parameters.AddWithValue("@cantidad", ventaDET.Cantidad);
                this.sqlComando.Parameters.AddWithValue("@montoFleteEnvio", ventaDET.MontoFleteEnvio);
                this.sqlComando.Parameters.AddWithValue("@montoImpuestoAduana", ventaDET.MontoImpuestoAduana);
                this.sqlComando.Parameters.AddWithValue("@montoGanancia", ventaDET.MontoGanancia);
                this.sqlComando.Parameters.AddWithValue("@montoIVA", ventaDET.MontoIVA);
                this.sqlComando.Parameters.AddWithValue("@subTotal", ventaDET.SubTotal);
                this.sqlComando.Parameters.AddWithValue("@total", ventaDET.Total);
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

        public DataSet ListaDetalles(int idVenta)
        {
            try
            {
                DataSet datos;
                SqlDataAdapter adaptador = new SqlDataAdapter();
                datos = new DataSet("Detalles");

                this.sqlConexion = new SqlConnection(this.strConexion);
                this.sqlComando = new SqlCommand();
                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;


                this.sqlComando.CommandText = "select v.idmoto, m.nombre, v.cantidad, v.montoFleteEnvio, " +
                                                "v.montoImpuestoAduana, v.montoGanancia," +
                                                "v.montoIVA, v.subtotal, v.total from ventaDet v " +
                                                "join motos m on m.idmoto = v.idmoto" +
                                                " where v.idventa='"+idVenta+"'";
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

        /*
        public List<VentaDET> Detalles()
        {
            try
            {
                VentaDET ventaDet = null;
                List<VentaDET> lista=null;
                SqlDataReader lectura;
                
                this.sqlConexion = new SqlConnection(this.strConexion);
                this.sqlComando = new SqlCommand();
                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandText = "select v.idmoto, m.nombre, v.cantidad, v.montoFleteEnvio, v.montoImpuestoAduana, v.montoGanancia,"+
                    "v.montoIVA, v.subtotal, v.total from ventaDet v join motos m on m.idmoto = v.idmoto";
                this.sqlComando.CommandType = CommandType.Text;
                lectura = this.sqlComando.ExecuteReader();




                if (lectura.Read())
                {

                   ventaDet= new VentaDET(Convert.ToInt32(lectura.GetValue(0)), Convert.ToInt32(lectura.GetValue(1)),
                               Convert.ToInt32(lectura.GetValue(2)), Convert.ToDouble(lectura.GetValue(3)),
                               Convert.ToDouble(lectura.GetValue(4)), Convert.ToDouble(lectura.GetValue(5)),
                               Convert.ToDouble(lectura.GetValue(6)), Convert.ToDouble(lectura.GetValue(7)),
                               Convert.ToDouble(lectura.GetValue(8)));
                   lista.Add(ventaDet);
                }


                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */


        public Boolean EliminarDetalles(int idVenta)
        {
            try
            {
                Boolean eliminado = false;
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Del_ventaDET";
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


        



    }
}
