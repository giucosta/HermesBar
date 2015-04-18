using DAO.Funcionario;
using Microsoft.Reporting.WinForms;
using REPORTMODEL.Funcionarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace REPORTS
{
    public partial class ListarFuncionarios : Form
    {
        public ListarFuncionarios()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'HermesBarDataSet.Funcionario' table. You can move, or remove it, as needed.
            this.FuncionarioTableAdapter.Fill(this.HermesBarDataSet.Funcionario);
            var list = ListarFuncionarioReportModel.GetList();
            
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportEmbeddedResource = "REPORTS.Reports.ListarFuncionarioReport.rdlc";

            Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("ListarFuncionarioDataSet", list);
            reportViewer1.LocalReport.DataSources.Add(dataset);
            dataset.Value = list;
            reportViewer1.LocalReport.Refresh();
            reportViewer1.RefreshReport();
        }
    }
}