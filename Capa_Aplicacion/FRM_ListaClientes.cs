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

namespace Capa_Aplicacion
{
    public partial class FRM_ListaClientes : Form
    {
        string cedulaGlobal="";
        public FRM_ListaClientes()
        {
            InitializeComponent();

            ClienteADO clienteADO = new ClienteADO(ConfigurationManager.ConnectionStrings["StringVehiculo"].ConnectionString);

            dataGridView1.DataSource = clienteADO.ListaClientes().Tables[0];

        }

        private void FRM_ListaClientes_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        public string Cedula()
        {
            return cedulaGlobal;
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            cedulaGlobal = "" + dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            this.Close();
        }
    }
}
