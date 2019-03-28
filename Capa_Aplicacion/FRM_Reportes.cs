using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Aplicacion
{
    public partial class FRM_Reportes : Form
    {
        public FRM_Reportes()
        {
            InitializeComponent();
        }

        private void FRM_Reportes_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'Db_VehiculosDataSet.ventaEnc' Puede moverla o quitarla según sea necesario.
            this.ventaEncTableAdapter.Fill(this.Db_VehiculosDataSet.ventaEnc);

            this.reportViewer1.RefreshReport();
        }
    }
}
