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
    public partial class FRM_Principal : Form
    {
        public FRM_Principal()
        {
            InitializeComponent();
        }

        private void motosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_Motos frm = new FRM_Motos();
            frm.ShowDialog();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_Cliente frm = new FRM_Cliente();
            frm.ShowDialog();
        }

        private void facturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void facturasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FRM_Venta frm = new FRM_Venta();
            frm.ShowDialog();
        }

        private void reproteFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_Reportes frm = new FRM_Reportes();
            frm.ShowDialog();
        }
    }
}
