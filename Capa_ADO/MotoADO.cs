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
    public class MotoADO
    {

        #region "Variables"
        private string strConexion;
        private SqlConnection sqlConexion;
        private SqlCommand sqlComando;
        #endregion

        #region "Constructor"
        public MotoADO(string pStringConexion)
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
        public void RegistrarMoto(Moto moto)
        {
            try
            {
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Ins_motos";
                this.sqlComando.Parameters.AddWithValue("@idmoto", moto.IDMoto);
                this.sqlComando.Parameters.AddWithValue("@nombre", moto.Nombre);
                this.sqlComando.Parameters.AddWithValue("@precio", moto.Precio);
                this.sqlComando.Parameters.AddWithValue("@porcentajeFlete", moto.ProcentajeFlete);
                this.sqlComando.Parameters.AddWithValue("@existencias", moto.Cantidad);
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


        public Moto ConsultarMoto(int idMoto)
        {
            try
            {
                Moto moto = null;
                SqlDataReader lectura;
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();
                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Cns_motos";
                this.sqlComando.Parameters.AddWithValue("@idmoto", idMoto);

                lectura = this.sqlComando.ExecuteReader();
                if (lectura.Read())
                {
                    moto = new Moto(Convert.ToInt32(lectura.GetValue(0)), ""+lectura.GetValue(1),
                                            Convert.ToDouble(lectura.GetValue(2)), Convert.ToDouble(lectura.GetValue(3)),
                                            Convert.ToInt32(lectura.GetValue(4)));
                }
                return moto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ModificarMoto(Moto moto)
        {
            try
            {
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Upd_motos";
                this.sqlComando.Parameters.AddWithValue("@idmoto", moto.IDMoto);
                this.sqlComando.Parameters.AddWithValue("@nombre", moto.Nombre);
                this.sqlComando.Parameters.AddWithValue("@precio", moto.Precio);
                this.sqlComando.Parameters.AddWithValue("@porcentajeFlete", moto.ProcentajeFlete);
                this.sqlComando.Parameters.AddWithValue("@existencias", moto.Cantidad);
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

        public Boolean EliminarMoto(Moto moto)
        {
            try
            {
                Boolean eliminado = false;
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Del_motos";
                this.sqlComando.Parameters.AddWithValue("@idmoto", moto.IDMoto);
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
        public DataSet ListarMotos()
        {
            try
            {
                DataSet datos;
                SqlDataAdapter adaptador = new SqlDataAdapter();
                datos = new DataSet("Motos");

                this.sqlConexion = new SqlConnection(this.strConexion);
                this.sqlComando = new SqlCommand();
                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;


                this.sqlComando.CommandText = "Select * from motos";
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
