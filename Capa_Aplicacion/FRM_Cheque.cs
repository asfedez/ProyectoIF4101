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
    public partial class FRM_Cheque : Form
    {
        Cheque cheque = null; 
        public FRM_Cheque()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            cheque = new Cheque(Convert.ToInt32(txtNumCheque.Text), txtBanco.Text);
            this.Close();
        }

        public Cheque ChequeLleno()
        {
            return cheque;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
