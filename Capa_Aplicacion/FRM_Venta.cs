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
    public partial class FRM_Venta : Form
    {

        bool clienteEncontrado = false;
        bool calcular = false;
        int cantidadMotos = 0;
        Cheque chequeGlobal = null;
        FRM_Cheque frmCheque = new FRM_Cheque();

        public FRM_Venta()
        {
            InitializeComponent();
        }

        

        private void FRM_Venta_Load(object sender, EventArgs e)
        {
            cantidadMotos = 0;
            EstadoInicial();
        }



        

        

        

        


        #region Eventos
        /// <summary>
        /// Este evento consulta el cliente en la base de datos
        /// si el cliente no consulta, no se habilitará el agregar detalles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteADO clienteADO = new ClienteADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                Cliente cliente = clienteADO.ConsultarCliente(Convert.ToInt32(txtCedula.Text.Trim()));

                if (cliente != null)
                {

                    txtNombre.Text = cliente.Nombre;
                    ListarPagos(cliente);
                    clienteEncontrado = true;

                    if (clienteEncontrado)
                    {
                        groupBox2.Enabled = true;
                    }

                }
                else
                {
                    throw new Exception("Cliente no se encuentra registrado\nPrimeramente debe registrarlo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                bool existe = VerificarMotosDetalle();

                if (existe)
                {
                    MessageBox.Show("No puede ingresar la misma moto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MotoADO motoADO = new MotoADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                    Moto moto = motoADO.ConsultarMoto(Convert.ToInt32(cbxMotos.SelectedValue));

                    VentaDET ventaDet = new VentaDET(Convert.ToInt32(txtCodigo.Text), moto.IDMoto, Convert.ToInt32(txtCantidad.Value));

                    VentaDET venta = ventaDet.CalcularMontos(moto, ventaDet);

                    dtgDetalle.Rows.Add("" + moto.IDMoto, "" + moto.Nombre, "" + venta.Cantidad, "" + venta.MontoFleteEnvio, "" + venta.MontoImpuestoAduana,
                        "" + venta.MontoGanancia, "" + venta.MontoIVA, "" + venta.SubTotal, "" + venta.Total);

                    //Actualizar subtotal automaticamente
                    cantidadMotos += venta.Cantidad;
                    double subtotal = Convert.ToDouble(txtSubtotal.Text);
                    subtotal += Math.Round(venta.Total, 3);
                    txtSubtotal.Text = "" + subtotal;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        /// <summary>
        /// Este evento elimina la fila seleccionada por el usuario para una posible modificacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgDetalle_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            MotoADO motoADO = new MotoADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
            Moto moto = motoADO.ConsultarMoto(Convert.ToInt32(dtgDetalle.Rows[e.RowIndex].Cells[0].Value));

            VentaDET ventaDet = new VentaDET(Convert.ToInt32(txtCodigo.Text), moto.IDMoto, Convert.ToInt32(dtgDetalle.Rows[e.RowIndex].Cells[2].Value));



            VentaDET venta = ventaDet.CalcularMontos(moto, ventaDet);


            //Esto es para restar la fila selecciona y recalcular el subtotal
            double subtotal = Convert.ToDouble(txtSubtotal.Text);
            subtotal = subtotal - Math.Round(venta.Total, 3);
            txtSubtotal.Text = "" + subtotal;
            cantidadMotos = cantidadMotos - venta.Cantidad;

            //se selecciona la moto y la cantidad seleccionada 
            cbxMotos.SelectedValue = dtgDetalle.Rows[e.RowIndex].Cells[0].Value;
            txtCantidad.Value = Convert.ToDecimal(dtgDetalle.Rows[e.RowIndex].Cells[2].Value);
            dtgDetalle.Rows.RemoveAt(e.RowIndex);
        }

        /// <summary>
        /// Este evento despliega la ventana de cheques
        /// en caso de que se seleccione este tipo de pago
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxTipoPago_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int indexCheque = cbxTipoPago.Items.IndexOf("Cheque");



            if (cbxTipoPago.SelectedIndex == indexCheque)
            {

                frmCheque.ShowDialog();
                chequeGlobal = frmCheque.ChequeLleno();

            }

        }


        /// <summary>
        /// Este evento nos despliega los montos calculados
        /// obligatoriamente debe seleccionar el tipo de pago
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            calcular = true;
            if (cbxTipoPago.SelectedItem == null)
            {
                MessageBox.Show("Elija un metodo de pago", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                CalcularMontos();
            }

        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
           
            try
            {
                
                int numFila = dtgDetalle.RowCount - 1;
                
                if (calcular && numFila>0)
                {

                    int idMoto = 0;
                    int cantidad = 0;
                    double montoFlete = 0;
                    double impAduana = 0;
                    double ganancia = 0;
                    double iva = 0;
                    double subtotal = 0;
                    double total = 0;

                    VentaENC ventaENC = new VentaENC(Convert.ToInt32(txtCodigo.Text), Convert.ToInt32(txtCedula.Text), Convert.ToDateTime(dtpFecha.Text),
                        "" + cbxTipoPago.SelectedItem, Convert.ToDouble(txtDescuento.Text), Convert.ToDouble(txtSubtotal.Text),
                        Convert.ToDouble(txtTotalDolares.Text), Convert.ToDouble(txtTotal.Text));
                    VentaENCADO ventaEncabezadoADO = new VentaENCADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                    ventaEncabezadoADO.RegistrarEncabezado(ventaENC);


                    VentaDetalleADO ventaDetalleAdo = new VentaDetalleADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                    VentaDET ventaDet = null;
                    //Agregar los detalles
                    numFila = dtgDetalle.RowCount - 1;
                    for (int i = 0; i < numFila; i++)
                    {

                        idMoto = Convert.ToInt32(dtgDetalle.Rows[i].Cells[0].Value);
                        cantidad = Convert.ToInt32(dtgDetalle.Rows[i].Cells[2].Value);
                        montoFlete = Convert.ToDouble(dtgDetalle.Rows[i].Cells[3].Value);
                        impAduana = Convert.ToDouble(dtgDetalle.Rows[i].Cells[4].Value);
                        ganancia = Convert.ToDouble(dtgDetalle.Rows[i].Cells[5].Value);
                        iva = Convert.ToDouble(dtgDetalle.Rows[i].Cells[6].Value);
                        subtotal = Convert.ToDouble(dtgDetalle.Rows[i].Cells[7].Value);
                        total = Convert.ToDouble(dtgDetalle.Rows[i].Cells[8].Value);

                        ventaDet = new VentaDET(ventaENC.IDVenta, idMoto, cantidad, montoFlete, impAduana,
                            ganancia, iva, subtotal, total);

                        ventaDetalleAdo.RegistrarDetalle(ventaDet);

                    }

                    if (cbxTipoPago.SelectedItem.Equals("Cheque"))
                    {
                        ChequeADO chequeADO = new ChequeADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                        chequeGlobal.IdVenta = ventaENC.IDVenta;

                        chequeADO.RegistrarCheque(chequeGlobal);
                    }



                    EstadoInicial();

                    MessageBox.Show("Factura Registrada correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new Exception("Debe calcular los montos o no hay detalles incluidos");
                }

                    
            }
            catch (Exception ex)
            {
                    
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }


        private void btnConsultar_Click(object sender, EventArgs e)
        {

            try
            {
                VentaENCADO ventaEncabezadoADO = new VentaENCADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                VentaENC ventaENC = ventaEncabezadoADO.ConsultarEncabezado(Convert.ToInt32(txtCodigo.Text));

                if(ventaENC!=null)
                {
                    Cliente cliente = new ClienteADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString).ConsultarCliente(ventaENC.Cedula);


                    ListarPagos(cliente);
                    txtCedula.Text = "" + ventaENC.Cedula;
                    txtNombre.Text = cliente.Nombre;
                    cbxTipoPago.SelectedItem = "" + ventaENC.TipoPago;
                    txtDescuento.Text = "" + ventaENC.MontoDescuento;
                    txtTotal.Text = "" + ventaENC.Total;
                    txtSubtotal.Text = "" + ventaENC.Subtotal;
                    txtTotalDolares.Text = "" + ventaENC.TotalDolares;


                    VentaDetalleADO ventaDetalleAdo = new VentaDetalleADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);

                    dataGridView1.DataSource = ventaDetalleAdo.ListaDetalles(ventaENC.IDVenta).Tables[0];
                    dataGridView1.Visible = true;

                    HabilitarModEli();
                }
                else
                {
                    DialogResult respuesta = MessageBox.Show("La venta no se encuentra registrada\n¿Desea agregarla?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (MessageBox.Show("Desea eliminar la factura", "Eliminar Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (cbxTipoPago.SelectedItem.Equals("Cheque"))
                    {
                        ChequeADO chequeADO = new ChequeADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                        chequeADO.EliminarCheque(Convert.ToInt32(txtCodigo.Text));
                    }

                    VentaENCADO ventaEncabezadoADO = new VentaENCADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                    VentaDetalleADO ventaDetalleAdo = new VentaDetalleADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);

                    ventaDetalleAdo.EliminarDetalles(Convert.ToInt32(txtCodigo.Text));
                    ventaEncabezadoADO.EliminarEncabezado(Convert.ToInt32(txtCodigo.Text));
                    MessageBox.Show("Factura Eliminada Correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    EstadoInicial();

                }
                else
                {
                    EstadoInicial();
                }
                   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void label11_Click(object sender, EventArgs e)
        {

            FRM_ListaClientes frmLista = new FRM_ListaClientes();
            frmLista.ShowDialog();
            txtCedula.Text = frmLista.Cedula();
            cbxTipoPago.Items.Clear();
            button1_Click(sender, e);

        }

        #endregion










        #region Metodos

        /// <summary>
        /// Este metodo verifica si existe la misma moto que se dea ingresar
        /// para evitar que se puedan ingresar la misma moto varias veces
        /// </summary>
        /// <returns></returns>
        public bool VerificarMotosDetalle()
        {
            int numFilas = dtgDetalle.RowCount - 1;
            bool existe = false;
            for (int i = 0; i < numFilas; i++)
            {
                if (Convert.ToInt32(dtgDetalle.Rows[i].Cells[0].Value) == Convert.ToInt32(cbxMotos.SelectedValue))
                {
                    existe = true;
                }

            }
            return existe;
        }

        /// <summary>
        /// Este metodo llena el combobox con las motos disponibles 
        /// </summary>
        public void ListarMotos()
        {
            try
            {
                MotoADO motoADO = new MotoADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                cbxMotos.DataSource = motoADO.ListarMotos().Tables[0];
                this.cbxMotos.ValueMember = "idmoto";
                this.cbxMotos.DisplayMember = "nombre";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Este metodo llena el combobox con los pagos disponibles para el cliente buscado
        /// </summary>
        /// <param name="cliente"></param>
        public void ListarPagos(Cliente cliente)
        {
            try
            {
                FacilidadPagoADO facilidadADO = new FacilidadPagoADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);
                FacilidadPago facilidad = facilidadADO.ConsultarFacilidadPago(cliente.Cedula);

                if (facilidad.Efectivo == 1)
                    cbxTipoPago.Items.Add("Efectivo");

                if (facilidad.Tarjeta == 1)
                    cbxTipoPago.Items.Add("Tarjeta");

                if (facilidad.Cheque == 1)
                    cbxTipoPago.Items.Add("Cheque");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Este metodo nos permite calcular los montos, en base 
        /// al tipo de pago
        /// cantidad de motos
        /// </summary>
        public void CalcularMontos()
        {
            double subtotal = Convert.ToDouble(txtSubtotal.Text);
            double descuento = 0;
            double total = 0;
            double totalDolares = 0;
            double descuentoCantidad = 0;
            double descuentoEfectivo = 0;


            if (cantidadMotos >= 3)
            {
                descuentoCantidad = Math.Round(subtotal * 0.10, 3);
            }

            if (cbxTipoPago.SelectedItem.Equals("Efectivo"))
            {
                descuentoEfectivo = Math.Round(subtotal * 0.02, 3);
            }

            descuento = Math.Round(descuentoCantidad + descuentoEfectivo, 3);

            total = Math.Round(subtotal - descuento, 3);
            totalDolares = Math.Round(total / 515, 3);

            txtDescuento.Text = "" + descuento;
            txtTotal.Text = "" + total;
            txtTotalDolares.Text = "" + totalDolares;

        }


        public void EstadoInicial()
        {
            txtCodigo.Enabled = true;
            calcular = false;

            dataGridView1.Visible = false;

            txtCodigo.Text = "" + new VentaENCADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString).Codigo();
            ListarMotos();
            groupBox2.Enabled = false;
            clienteEncontrado = false;
            lblLista.Enabled = false;

            cbxTipoPago.Items.Clear();

            txtCedula.Text = "";
            txtCedula.Enabled = false;
            btnBuscarCliente.Enabled = false;
            txtNombre.Text = "";
            txtDescuento.Text = "0";
            txtSubtotal.Text = "0";
            txtTotalDolares.Text = "0";
            txtTotal.Text = "0";

            btnAgregar.Enabled = false;
            btnConsultar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            btnCalcular.Enabled = false;

            int numFila = dtgDetalle.RowCount - 1;
            for (int i = 0; i < numFila; i++)
            {
                dtgDetalle.Rows.RemoveAt(0);
            }

        }

        public void HabilitarAgregar()
        {
            txtCedula.Enabled = true;
            btnBuscarCliente.Enabled = true;
            lblLista.Enabled = true;
            txtCodigo.Enabled = false;

            btnAgregar.Enabled = true;
            btnConsultar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            btnCalcular.Enabled = true;
        }

        public void HabilitarModEli()
        {
            lblLista.Enabled = false;
            txtCedula.Enabled = false;
            btnBuscarCliente.Enabled = false;


            groupBox2.Enabled = false;

            txtCodigo.Enabled = false;

            btnAgregar.Enabled = false;
            btnConsultar.Enabled = false;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;

            btnCalcular.Enabled = false;
        }



        #endregion







        

        

       
    }
}
