using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Logica
{
    public class Cliente
    {
        #region Atributos
        int cedula;
        string nombre;
        string telefono;
        string direccion;
        #endregion

        #region Propiedades
        public int Cedula
        {
            get
            {
                return this.cedula;
            }
            set
            {
                if(value.ToString().Length < 9 || value.ToString().Length > 9)
                {
                    throw new Exception("Formato de cédula invalido");
                }
                else
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
        }

        public string Nombre
        {
            get
            {
                return this.nombre;
            }
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

        public string Telefono
        {
            get
            {
                return this.telefono;
            }
            set
            {
                if (value.Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (Telefono)");
                }
                else
                {
                    this.telefono = value.Trim();
                }
            }
        }

        public string Direccion
        {
            get
            {
                return this.direccion;
            }
            set
            {
                if (value.Trim().Equals(""))
                {
                    throw new Exception("No se permiten campos vacíos (Direccion)");
                }
                else
                {
                    this.direccion = value.Trim();
                }
            }
        }
        #endregion

        #region Constructores
        public Cliente(int pCedula, string pNombre, string pTelefono, string pDireccion)
        {
            this.Cedula = pCedula;
            this.Nombre = pNombre;
            this.Telefono = pTelefono;
            this.Direccion = pDireccion;
        }
        #endregion


    }
}
