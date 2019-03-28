using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Logica
{
    public class FacilidadPago
    {
        #region Atributos
        int cedula;
        int efectivo;
        int tarjeta;
        int cheque;

        #endregion

        #region Propiedades

        public int Cedula { get => cedula; set => cedula = value; }
        public int Efectivo { get => efectivo; set => efectivo = value; }
        public int Tarjeta { get => tarjeta; set => tarjeta = value; }
        public int Cheque { get => cheque; set => cheque = value; }

        #endregion


        #region Constructor
        public FacilidadPago(int cedula, int efectivo, int tarjeta, int cheque)
        {
            Cedula = cedula;
            Efectivo = efectivo;
            Tarjeta = tarjeta;
            Cheque = cheque;
        }
        #endregion

    }
}
