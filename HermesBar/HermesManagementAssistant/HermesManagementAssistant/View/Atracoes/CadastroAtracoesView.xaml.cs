﻿using BLL.Atracoes;
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
using DAO;

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
            CarregaComboEstilos();
        }
        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            var obrigatorios = ValidarCampos();
            if (obrigatorios.Count == 0)
            {
                var contato = CarregaContato();
                var atracoes = CarregaAtracoes();
                atracoes.Contato = contato;

                if (new AtracoesBLL().Salvar(atracoes))
                    Mensagens.GeraMensagens("Salvo com sucesso", MENSAGEM.ATRACOES_CADASTRO_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                else
                    Mensagens.GeraMensagens("Erro ao salvar", MENSAGEM.ATRACOES_CADASTRO_ERRO, null, TIPOS_MENSAGENS.ERRO);
            }
            else
                Mensagens.GeraMensagens("Campos Obrigatórios", MENSAGEM.CAMPOS_OBRIGATORIOS, obrigatorios, TIPOS_MENSAGENS.ALERTA);
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
            atracoes.Tempo_Show = tbTempo.Text;
            atracoes.Ultimo_Valor_Cobrado = Double.Parse(tbValor.Text);
            atracoes.Estilo = cbEstilo.SelectionBoxItem.ToString();

            return atracoes;
        }
        private List<String> ValidarCampos()
        {
            var camposObrigatorios = new List<String>();
            if (string.IsNullOrWhiteSpace(tbAtracao.Text))
                camposObrigatorios.Add("ATRACAO");
            if (string.IsNullOrWhiteSpace(tbValor.Text))
                camposObrigatorios.Add("VALOR");
            if (string.IsNullOrWhiteSpace(tbTempo.Text))
                camposObrigatorios.Add("TEMPO");
            if (string.IsNullOrWhiteSpace(tbNome.Text))
                camposObrigatorios.Add("NOME");
            if (string.IsNullOrWhiteSpace(tbTelefone.Text))
                camposObrigatorios.Add("TELEFONE");

            return camposObrigatorios;
        }
        private void CarregaComboEstilos()
        {
            cbEstilo.ItemsSource = new AtracoesBLL().RecuperaEstilos();
            cbEstilo.SelectedIndex = 0;
        }
    }

}
