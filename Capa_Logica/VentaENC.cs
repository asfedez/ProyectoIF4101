using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Logica
{
    public class VentaENC
    {
        #region Atributos
        int iDVenta;
        int cedula;
        DateTime fecha;
        String tipoPago;
        double montoDescuento;
        double subtotal;
        double totalDolares;
        double total;
        #endregion



        #region Propiedades
        public int IDVenta
        {
            get => iDVenta;
            set
            {
                if (value.ToString().Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (IdVenta)");
                }
                else
                {
                    this.iDVenta = Convert.ToInt32(value.ToString().Trim());
                }
            } 
        }
        public int Cedula
        {
            get => cedula;

            set
            {
                if (value.ToString().Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (Cedula)");
                }
                else
                {
                    this.cedula = Convert.ToInt32(value.ToString().Trim());
                }
            }
        }
        public DateTime Fecha { get => fecha; set => fecha = value; }

        public string TipoPago { get => tipoPago; set => tipoPago = value; }


        public double MontoDescuento
        {
            get => montoDescuento;
            set
            {
                if (value.ToString().Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (Descuento)");
                }
                else
                {
                    this.montoDescuento = Convert.ToDouble(value.ToString().Trim());
                }
            }
               
        }

        public double Subtotal
        {
            get => subtotal;
            set
            {
                if (value.ToString().Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (Subtotal)");
                }
                else
                {
                    this.subtotal = Convert.ToDouble(value.ToString().Trim());
                }
            } 
        }


        public double TotalDolares
        {
            get => totalDolares;
            set
            {
                if (value.ToString().Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (Total Dolares)");
                }
                else
                {
                    this.totalDolares = Convert.ToDouble(value.ToString().Trim());
                }
            }
        }

        public double Total
        {
            get => total;
            set
            {
                if (value.ToString().Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (Total)");
                }
                else
                {
                    this.total = Convert.ToDouble(value.ToString().Trim());
                }
            }
            

        }
        #endregion


        #region Constructor
        public VentaENC(int iDVenta, int cedula, DateTime fecha, string tipoPago, double montoDescuento, double subtotal, double totalDolares, double total)
        {
            IDVenta = iDVenta;
            Cedula = cedula;
            Fecha = fecha;
            TipoPago = tipoPago;
            MontoDescuento = montoDescuento;
            Subtotal = subtotal;
            TotalDolares = totalDolares;
            Total = total;
        } 
        #endregion
    }
}
