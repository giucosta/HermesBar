using BLL.Login;
using BLL.Usuario;
using MODEL;
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
using Utils;
using UTILS;

namespace HermesManagementAssistant.View.Usuario
{
    /// <summary>
    /// Interaction logic for UsuarioCadastro.xaml
    /// </summary>
    public partial class UsuarioCadastro : Window
    {
        public UsuarioCadastro()
        {
            InitializeComponent();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            var obrigatorios = VerificaCamposObrigatorios();
            if (obrigatorios.Count == 0)
            {
                if (VerificaSenhas())
                {
                    if (!VerificaUsuarioExistente(tbNome.Text))
                        GravarUsuario();
                    else
                        Mensagens.GeraMensagens("Usuário já cadastrado", "Usuário já cadastrado", null,TIPOS_MENSAGENS.ALERTA);

                        #warning terminar isso aqui

                }
                else
                    Mensagens.GeraMensagens("A senha e a confirmação devem ser identicas", "Senhas não identicas", null,TIPOS_MENSAGENS.ALERTA);
            }
            else
                Mensagens.GeraMensagens("Campos Obrigatórios","Campos obrigatórios não preenchidos: ",obrigatorios,TIPOS_MENSAGENS.ALERTA);
        }

        private List<String> VerificaCamposObrigatorios()
        {
            var campos = new List<String>();
            if (string.IsNullOrWhiteSpace(tbNome.Text))
                campos.Add("NOME");
            if (string.IsNullOrWhiteSpace(tbEmail.Text))
                campos.Add("EMAIL");
            if (string.IsNullOrWhiteSpace(tbSenha.Password))
                campos.Add("SENHA");
            if (string.IsNullOrWhiteSpace(tbConfirmaSenha.Password))
                campos.Add("CONFIRMA SENHA");
            if (cbPerfil.SelectedItem == null)
                campos.Add("PERFIL");

            return campos;
        }
        private bool VerificaUsuarioExistente(string nome)
        {
            return new UsuarioBLL().RetornaUsuarioExistente(nome);
        }

        private bool VerificaSenhas()
        {
            if (tbSenha.Password == tbConfirmaSenha.Password)
                return true;

            return false;
        }

        private void GravarUsuario()
        {
            var login = new LoginModel();
            login.Login = tbNome.Text;
            login.Senha = tbSenha.Password;

            if (!new LoginBLL().GravarLogin(login))
                Mensagens.GeraMensagens("Erro ao gravar o login", "Erro ao gravar login", null,TIPOS_MENSAGENS.ERRO);

            var perfil = new PerfilModel() { Perfil = cbPerfil.SelectedItem.ToString() };

            var user = new UsuarioModel();
            user.Email = tbEmail.Text;
            user.Nome = tbNome.Text;
            user.Status = "A";
            user.Login = login;
            user.Perfil = perfil;



        }
    }
}
