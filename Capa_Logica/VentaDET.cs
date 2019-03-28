using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Logica
{
    public class VentaDET
    {
        #region Atributos
        int idventa;
        int idmoto;
        int cantidad;
        double montoFleteEnvio;
        double montoImpuestoAduana;
        double montoGanancia;
        double montoIVA;
        double subTotal;
        double total;


        #endregion



        #region Propiedades
        public int Idventa { get => idventa; set => idventa = value; }
        public int Idmoto { get => idmoto; set => idmoto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public double MontoFleteEnvio { get => montoFleteEnvio; set => montoFleteEnvio = value; }
        public double MontoImpuestoAduana { get => montoImpuestoAduana; set => montoImpuestoAduana = value; }
        public double MontoGanancia { get => montoGanancia; set => montoGanancia = value; }
        public double MontoIVA { get => montoIVA; set => montoIVA = value; }
        public double SubTotal { get => subTotal; set => subTotal = value; }
        public double Total { get => total; set => total = value; }
        #endregion


        #region Constructor
       

        public VentaDET(int idventa, int idmoto, int cantidad)
        {
            Idventa = idventa;
            Idmoto = idmoto;
            Cantidad = cantidad;
        }

        public VentaDET(int idventa, int idmoto, int cantidad, double montoFleteEnvio, double montoImpuestoAduana, double montoGanancia, double montoIVA, double subTotal, double total)
        {
            Idventa = idventa;
            Idmoto = idmoto;
            Cantidad = cantidad;
            MontoFleteEnvio = montoFleteEnvio;
            MontoImpuestoAduana = montoImpuestoAduana;
            MontoGanancia = montoGanancia;
            MontoIVA = montoIVA;
            SubTotal = subTotal;
            Total = total;
        }

        
        #endregion


        public VentaDET CalcularMontos(Moto moto, VentaDET pVentaDET)
        {
            try
            {
                if(pVentaDET.Cantidad<=moto.Cantidad)
                {
                    VentaDET ventaDET = pVentaDET;
                    double subtotal = Math.Round(moto.Precio, 3);
                    double montoFlete = Math.Round(moto.Precio * moto.ProcentajeFlete, 3);
                    subtotal += Math.Round(montoFlete, 3);
                    double montoImpAduana = Math.Round(subtotal * 0.15, 3);
                    subtotal += Math.Round(montoImpAduana,3);
                    double montoGanancia = Math.Round(subtotal * 0.15,3);
                    subtotal += Math.Round(montoGanancia,3);
                    double montoImpIVA = Math.Round(subtotal * 0.13, 3);
                    subtotal += Math.Round(montoImpIVA,3);
                    double total = Math.Round(subtotal * ventaDET.Cantidad, 3);


                    ventaDET.MontoFleteEnvio = montoFlete;
                    ventaDET.MontoImpuestoAduana = montoImpAduana;
                    ventaDET.MontoGanancia = montoGanancia;
                    ventaDET.MontoIVA = montoImpIVA;
                    ventaDET.SubTotal = subtotal;
                    ventaDET.Total = total;

                    return ventaDET;
                }
                else
                {
                    throw new Exception("Existencias de moto insuficiente");
                }
    
            }
            catch(Exception ex)
            {
                throw ex;
            }

            
        }
    }
}
