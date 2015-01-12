using BLL.Atracoes;
using BLL.Comum;
using MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HermesManagementAssistant.View.Atracoes
{
    /// <summary>
    /// Interaction logic for CadastroAtracoesView.xaml
    /// </summary>
    public partial class CadastroAtracoesView : Window
    {
        public CadastroAtracoesView()
        {
            InitializeComponent();
            cbEstilo.ItemsSource = new AtracoesBLL().RecuperaEstilos();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            var contato = CarregaContato();
            var atracoes = CarregaAtracoes();
            atracoes.Contato = contato;

            if (new AtracoesBLL().Salvar(atracoes))
                MessageBox.Show("Ok");
            else
                MessageBox.Show("Não");
        }

        private ContatoModel CarregaContato()
        {
            var contato = new ContatoModel();
            contato.Nome = tbNome.Text;
            contato.Telefone = tbTelefone.Text;
            contato.Celular = tbCelular.Text;
            contato.Email = tbEmail.Text;
            contato.Site = tbSite.Text;

            return contato;
        }
        private ContatoModel SalvaContato(ContatoModel contato)
        {
            return new ContatoBLL().Salvar(contato);
        }
        private AtracoesModel CarregaAtracoes()
        {
            var atracoes = new AtracoesModel();
            atracoes.Nome = tbAtracao.Text;
            atracoes.TempoApresentacao = tbTempo.Text;
            atracoes.UltimoValorCobrado = Double.Parse(tbValor.Text);
            atracoes.EstiloPredominante = cbEstilo.SelectionBoxItem.ToString();

            return atracoes;
        }
    }
}
