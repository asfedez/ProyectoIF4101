using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_ADO;
using Capa_Logica;
using System.Configuration;
using System.Transactions;

namespace Capa_Aplicacion
{
    public partial class FRM_Motos : Form
    {
        public FRM_Motos()
        {
            InitializeComponent();
            EstadoInicial();
        }

        #region Eventos
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                MotoADO motoADO = new MotoADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                Moto moto = motoADO.ConsultarMoto(Convert.ToInt32(txtIDMoto.Text.Trim()));
                if (moto != null)
                {
                    txtNombre.Text = moto.Nombre;
                    txtPorcentajeFlete.Text = "" + moto.ProcentajeFlete * 100;
                    txtPrecio.Text = "" + moto.Precio;
                    txtExistencias.Text = "" + moto.Cantidad;
                    HabilitarModificarEliminar();
                }
                else
                {
                    DialogResult respuesta = MessageBox.Show("La moto no se encuentra registrada\n¿Desea agregarla?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (respuesta == DialogResult.Yes)
                    {
                        HabilitarAgregar();
                    }//fin if dialogo
                    else
                    {
                        this.EstadoInicial();
                    }//fin de else dialogo

                }

            }
            catch (Exception ex)
            {
                EstadoInicial();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            try
            {
                Moto moto = new Moto(Convert.ToInt32(txtIDMoto.Text), txtNombre.Text, Convert.ToDouble(txtPrecio.Text), Convert.ToDouble(txtPorcentajeFlete.Text) / 100, Convert.ToInt32(txtExistencias.Text));
                MotoADO motoADO = new MotoADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                motoADO.RegistrarMoto(moto);

                MessageBox.Show("Moto registrada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EstadoInicial();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                
                Moto moto = new Moto(Convert.ToInt32(txtIDMoto.Text), txtNombre.Text, Convert.ToDouble(txtPrecio.Text), (Convert.ToDouble(txtPorcentajeFlete.Text)/100), Convert.ToInt32(txtExistencias.Value));
                MotoADO motoADO = new MotoADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                motoADO.ModificarMoto(moto);

                MessageBox.Show("Moto modificada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EstadoInicial();
            }
            catch (Exception ex)
            {
                EstadoInicial();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Desea eliminar la moto", "Eliminar Moto", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Moto moto = new Moto(Convert.ToInt32(txtIDMoto.Text), txtNombre.Text, Convert.ToDouble(txtPrecio.Text), Convert.ToDouble(txtPorcentajeFlete.Text) / 100, Convert.ToInt32(txtExistencias.Text));
                    MotoADO motoADO = new MotoADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);

                    if (motoADO.EliminarMoto(moto))
                    {

                        EstadoInicial();
                        MessageBox.Show("Moto " + moto.Nombre + " eliminada", "Eliminar Moto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        EstadoInicial();
                    }
                }
                else
                    EstadoInicial();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
        #endregion

        #region Metodos
        private void EstadoInicial()
        {
            btnAgregar.Enabled = false;
            btnConsultar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            txtIDMoto.Text = "";
            txtNombre.Text = "";
            txtPorcentajeFlete.Text = "";
            txtPrecio.Text = "";
            txtExistencias.Text = "";

            txtIDMoto.Enabled = true;
            txtNombre.Enabled = false;
            txtPorcentajeFlete.Enabled = false;
            txtPrecio.Enabled = false;
            txtExistencias.Enabled = false;
        }

        private void HabilitarAgregar()
        {
            btnAgregar.Enabled = true;
            btnConsultar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;


            txtIDMoto.Enabled = false;
            txtNombre.Enabled = true;
            txtPorcentajeFlete.Enabled = true;
            txtPrecio.Enabled = true;
            txtExistencias.Enabled = true;

        }

        private void HabilitarModificarEliminar()
        {
            btnAgregar.Enabled = false;
            btnConsultar.Enabled = false;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;

            txtIDMoto.Enabled = false;
            txtNombre.Enabled = true;
            txtPorcentajeFlete.Enabled = true;
            txtPrecio.Enabled = true;
            txtExistencias.Enabled = true;


        }



        #endregion

        private void FRM_Motos_Load(object sender, EventArgs e)
        {

        }
    }
}
