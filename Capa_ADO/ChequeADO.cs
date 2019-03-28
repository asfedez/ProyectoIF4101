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
    public class ChequeADO
    {

        #region "Variables"
        private string strConexion;
        private SqlConnection sqlConexion;
        private SqlCommand sqlComando;
        #endregion

        #region "Constructor"
        public ChequeADO(string pStringConexion)
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


        public void RegistrarCheque(Cheque cheque)
        {
            try
            {
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Ins_Cheque";
                this.sqlComando.Parameters.AddWithValue("@idVenta", cheque.IdVenta);
                this.sqlComando.Parameters.AddWithValue("@numeroCheque", cheque.NumeroCheque);
                this.sqlComando.Parameters.AddWithValue("@nombreBanco", cheque.NombreBanco);
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

        public Boolean EliminarCheque(int idVenta)
        {
            try
            {
                Boolean eliminado = false;
                this.sqlConexion = new SqlConnection(this.StringConexion);
                this.sqlComando = new SqlCommand();

                this.sqlConexion.Open();
                this.sqlComando.Connection = this.sqlConexion;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.sqlComando.CommandText = "Sp_Del_Cheque";
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
