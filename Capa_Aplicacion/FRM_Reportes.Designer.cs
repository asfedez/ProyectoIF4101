namespace Capa_Aplicacion
{
    partial class FRM_Reportes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Db_VehiculosDataSet = new Capa_Aplicacion.Db_VehiculosDataSet();
            this.ventaEncBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ventaEncTableAdapter = new Capa_Aplicacion.Db_VehiculosDataSetTableAdapters.ventaEncTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.Db_VehiculosDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ventaEncBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.ventaEncBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Capa_Aplicacion.rptClienteGen.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(896, 413);
            this.reportViewer1.TabIndex = 0;
            // 
            // Db_VehiculosDataSet
            // 
            this.Db_VehiculosDataSet.DataSetName = "Db_VehiculosDataSet";
            this.Db_VehiculosDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ventaEncBindingSource
            // 
            this.ventaEncBindingSource.DataMember = "ventaEnc";
            this.ventaEncBindingSource.DataSource = this.Db_VehiculosDataSet;
            // 
            // ventaEncTableAdapter
            // 
            this.ventaEncTableAdapter.ClearBeforeFill = true;
            // 
            // FRM_Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 413);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FRM_Reportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte Facturas";
            this.Load += new System.EventHandler(this.FRM_Reportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Db_VehiculosDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ventaEncBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ventaEncBindingSource;
        private Db_VehiculosDataSet Db_VehiculosDataSet;
        private Db_VehiculosDataSetTableAdapters.ventaEncTableAdapter ventaEncTableAdapter;
    }
}