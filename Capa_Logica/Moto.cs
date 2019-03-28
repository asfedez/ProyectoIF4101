using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Logica
{
    public class Moto
    {

        #region Atributos
        private int idMoto;
        private String nombre;
        private double precio;
        private double procentajeFlete;
        private int cantidad;
        #endregion

        #region Propiedades
        public int IDMoto
        {
            get => idMoto;
            set
            {
                if (value.ToString().Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (Idmoto)");
                }
                else
                {
                    this.idMoto = Convert.ToInt32(value.ToString().Trim());
                }
            }
        }
        public string Nombre
        {
            get => nombre;
            set
            {
                if (value.Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (Nombre)");
                }
                else
                {
                    this.nombre = value.Trim();
                }
            } 
        }

        public double Precio
        {
            get => precio;
            set
            {
                if (value.ToString().Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (Precio)");
                }
                else
                {
                    this.precio = Convert.ToDouble(value.ToString().Trim());
                }
            } 
        }
        public double ProcentajeFlete
        {
            get => procentajeFlete;
            set
            {
                if (value.ToString().Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (Porcentaje)");
                }
                else
                {
                    this.procentajeFlete = Convert.ToDouble(value.ToString().Trim());
                }
            }  
        }
        public int Cantidad
        {
            get => cantidad;
            set
            {
                if (value.ToString().Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (Cantidad)");
                }
                else
                {
                    this.cantidad = Convert.ToInt32(value.ToString().Trim());
                }
            }
            
        }
        #endregion


        #region Constructor
        public Moto(int iDMoto, string nombre, double precio, double procentajeFlete, int cantidad)
        {
            IDMoto = iDMoto;
            Nombre = nombre;
            Precio = precio;
            ProcentajeFlete = procentajeFlete;
            Cantidad = cantidad;
        } 
        #endregion


    }
}
