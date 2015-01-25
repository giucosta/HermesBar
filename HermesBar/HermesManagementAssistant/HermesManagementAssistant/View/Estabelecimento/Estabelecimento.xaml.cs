using BLL.Comum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HermesManagementAssistant.View.Estabelecimento
{
    /// <summary>
    /// Interaction logic for Estabelecimento.xaml
    /// </summary>
    public partial class Estabelecimento : Window
    {
        public Estabelecimento()
        {
            InitializeComponent();
            cbEstado.ItemsSource = new EnderecoBLL().CarregaEstados();
        }

        private void ConsultaCep_Click(object sender, RoutedEventArgs e)
        {
            List<ConsultaCep.Endereco> enderecos = null;
            enderecos = ConsultaCep.ConsultaCep.GetEnderecos(tbCep.Text);
            if (enderecos.Count != 0)
            {
                foreach (ConsultaCep.Endereco x in enderecos)
                {
                    tbRua.Text = x.Logradouro;
                    tbCidade.Text = x.Localidade;
                    tbBairro.Text = x.Bairro;
                    var i = 0;
                    foreach (var s in cbEstado.ItemsSource)
                    {
                        if (x.uf == s.ToString())
                            cbEstado.SelectedIndex = i;
                        else
                            i++;
                    }
                }
            }
            else
                lbCepNaoExistente.Visibility = System.Windows.Visibility.Visible;
        }

        private void GravarEstabelecimento(object sender, RoutedEventArgs e)
        {

        }
    }
}
