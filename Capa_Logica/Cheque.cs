using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Logica
{
    public class Cheque
    {
        int idCheque;
        int idVenta;
        int numeroCheque;
        string nombreBanco;

       

        public int IdVenta { get => idVenta; set => idVenta = value; }
        public int IdCheque { get => idCheque; set => idCheque = value; }


        public int NumeroCheque
        {
            get => numeroCheque;

            set
            {
                if (value.ToString().Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (Numero de Cheque)");
                }
                else
                {
                    this.numeroCheque = Convert.ToInt32(value.ToString().Trim());
                }
            }
        }
        public string NombreBanco
        {
            get => nombreBanco;
            set
            {
                if (value.Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (Nombre)");
                }
                else
                {
                    this.nombreBanco = value.Trim();
                }
            }
        }


        public Cheque( int numeroCheque, string nombreBanco)
        {
            NumeroCheque = numeroCheque;
            NombreBanco = nombreBanco;
        }
    }
}
