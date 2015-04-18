namespace REPORTS
{
    partial class ListarFuncionarios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.FuncionarioModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.HermesBarDataSet = new REPORTS.HermesBarDataSet();
            this.FuncionarioBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FuncionarioTableAdapter = new REPORTS.HermesBarDataSetTableAdapters.FuncionarioTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.FuncionarioModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HermesBarDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FuncionarioBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // FuncionarioModelBindingSource
            // 
            this.FuncionarioModelBindingSource.DataSource = typeof(MODEL.FuncionarioModel);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ReciboFuncionario";
            reportDataSource1.Value = this.FuncionarioBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "REPORTS.ReciboFuncionario.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(682, 386);
            this.reportViewer1.TabIndex = 0;
            // 
            // HermesBarDataSet
            // 
            this.HermesBarDataSet.DataSetName = "HermesBarDataSet";
            this.HermesBarDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // FuncionarioBindingSource
            // 
            this.FuncionarioBindingSource.DataMember = "Funcionario";
            this.FuncionarioBindingSource.DataSource = this.HermesBarDataSet;
            // 
            // FuncionarioTableAdapter
            // 
            this.FuncionarioTableAdapter.ClearBeforeFill = true;
            // 
            // ListarFuncionarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 386);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ListarFuncionarios";
            this.Text = "RPG - Hermes Management Assistant";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FuncionarioModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HermesBarDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FuncionarioBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource FuncionarioModelBindingSource;
        private System.Windows.Forms.BindingSource FuncionarioBindingSource;
        private HermesBarDataSet HermesBarDataSet;
        private HermesBarDataSetTableAdapters.FuncionarioTableAdapter FuncionarioTableAdapter;
    }
}

