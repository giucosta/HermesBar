using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMAViews.View.Funcionario
{
    public partial class ReciboFuncionario : Form
    {
        public ReciboFuncionario()
        {
            InitializeComponent();
        }

        private void ReciboFuncionario_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
    }
}
