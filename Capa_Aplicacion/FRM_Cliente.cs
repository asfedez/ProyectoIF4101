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
    public partial class FRM_Cliente : Form
    {

        int vEfectivo = 0;
        int vTarjeta = 0;
        int vCheque = 0;

        public FRM_Cliente()
        {
            InitializeComponent();
            EstadoInicial();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Cliente cliente = new Cliente(Convert.ToInt32(txtCedula.Text.Trim()), txtNombre.Text.Trim(), txtTelefono.Text.Trim(), txtDireccion.Text.Trim());
                    ClienteADO clienteADO = new ClienteADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                    clienteADO.RegistrarCliente(cliente);

                    if(ValidarSeleccionPago())
                    {
                        FacilidadPago facilidad = new FacilidadPago(cliente.Cedula, vEfectivo, vTarjeta, vCheque);
                        FacilidadPagoADO facilidadADO = new FacilidadPagoADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                        facilidadADO.RegistrarFacilidadPago(facilidad);
                    }
                    else
                    {
                        throw new Exception("Debe elegir al menos una facilidad de pago para el cliente");
                    }

                    
                    scope.Complete();
                    MessageBox.Show("Cliente registrado correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EstadoInicial();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
                

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            
                try
                {
                    ClienteADO clienteADO = new ClienteADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                    Cliente cliente = clienteADO.ConsultarCliente(Convert.ToInt32(txtCedula.Text.Trim()));

                    FacilidadPagoADO facilidadADO = new FacilidadPagoADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                    FacilidadPago facilidad = facilidadADO.ConsultarFacilidadPago(Convert.ToInt32(txtCedula.Text.Trim()));

                    if (cliente != null )
                    {
                        txtNombre.Text = cliente.Nombre;
                        txtTelefono.Text = cliente.Telefono;
                        txtDireccion.Text = cliente.Direccion;
                        ValidarChecks(facilidad);

                        HabilitarModificarEliminar();
                    }
                    else
                    {
                        DialogResult respuesta = MessageBox.Show("El cliente no se encuentra registrado\n¿Desea agregarlo?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Cliente cliente = new Cliente(Convert.ToInt32(txtCedula.Text.Trim()), txtNombre.Text.Trim(), txtTelefono.Text.Trim(), txtDireccion.Text.Trim());
                    ClienteADO clienteADO = new ClienteADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                    clienteADO.ModificarCliente(cliente);


                    FacilidadPago facilidad = new FacilidadPago(cliente.Cedula, vEfectivo, vTarjeta, vCheque);
                    FacilidadPagoADO facilidadADO = new FacilidadPagoADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                    facilidadADO.ModificarFacilidadPago(facilidad);

                    scope.Complete();
                    MessageBox.Show("Cliente modificado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EstadoInicial();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    EstadoInicial();
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
                
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (MessageBox.Show("Desea eliminar el cliente", "Eliminar Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {


                        Cliente cliente = new Cliente(Convert.ToInt32(txtCedula.Text.Trim()), txtNombre.Text.Trim(), txtTelefono.Text.Trim(), txtDireccion.Text.Trim());
                        ClienteADO clienteADO = new ClienteADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);

                        FacilidadPago facilidad = new FacilidadPago(cliente.Cedula, vEfectivo, vTarjeta, vCheque);
                        FacilidadPagoADO facilidadADO = new FacilidadPagoADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);


                        if (facilidadADO.EliminarFacilidadPago(facilidad))
                        {
                            if (clienteADO.EliminarCliente(cliente))
                            {
                                scope.Complete();
                                EstadoInicial();
                                MessageBox.Show("Cliente " + cliente.Nombre + " eliminado", "Eliminar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                scope.Dispose();
                                EstadoInicial();
                            }
                        }
                        else
                        {
                            scope.Dispose();
                            EstadoInicial();
                            MessageBox.Show("Error al eliminar cliente", "Eliminar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        EstadoInicial();
                        scope.Dispose();
                    }

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
               
        }

        private void checkEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            if(checkEfectivo.Checked)
            {
                vEfectivo = 1;
            }
            else
            {
                vEfectivo = 0;
            }
        }

        private void checkTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTarjeta.Checked)
            {
                vTarjeta = 1;
            }
            else
            {
                vTarjeta = 0;
            }
        }

        private void checkCheque_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCheque.Checked)
            {
                vCheque = 1;
            }
            else
            {
                vCheque = 0;
            }
        }




        #region Metodos
        /// <summary>
        /// Este metodo muestra los tipos de pagos asociados al cliente consultado
        /// </summary>
        /// <param name="facilidad"></param>
        private void ValidarChecks(FacilidadPago facilidad)
        {
            if(facilidad.Efectivo==1)
                checkEfectivo.Checked = true;
            else
                checkEfectivo.Checked = false;

            if (facilidad.Tarjeta == 1)
                checkTarjeta.Checked = true;
            else
                checkTarjeta.Checked = false;

            if (facilidad.Cheque == 1)
                checkCheque.Checked = true;
            else
                checkCheque.Checked = false;

        }
        /// <summary>
        /// Este metodo verifica que el cliente tenga al menos una opcion de pago
        /// </summary>
        /// <returns></returns>
        private bool ValidarSeleccionPago()
        {
            if(checkEfectivo.Checked == false && checkTarjeta.Checked==false && checkCheque.Checked == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void EstadoInicial()
        {
            btnAgregar.Enabled = false;
            btnConsultar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            txtCedula.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";

            checkCheque.Checked = false;
            checkEfectivo.Checked = false;
            checkTarjeta.Checked = false;

            groupBox2.Enabled = false;

            txtCedula.Enabled = true;
            txtNombre.Enabled = false;
            txtTelefono.Enabled = false;
            txtDireccion.Enabled = false;

        }

        private void HabilitarAgregar()
        {
            btnAgregar.Enabled = true;
            btnConsultar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            txtCedula.Enabled = false;
            txtNombre.Enabled = true;
            txtTelefono.Enabled = true;
            txtDireccion.Enabled = true;

            groupBox2.Enabled = true;


        }

        private void HabilitarModificarEliminar()
        {
            btnAgregar.Enabled = false;
            btnConsultar.Enabled = false;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;

            txtCedula.Enabled = false;
            txtNombre.Enabled = true;
            txtTelefono.Enabled = true;
            txtDireccion.Enabled = true;

            groupBox2.Enabled = true;

        }



        #endregion

        private void FRM_Cliente_Load(object sender, EventArgs e)
        {

        }
    }
}
