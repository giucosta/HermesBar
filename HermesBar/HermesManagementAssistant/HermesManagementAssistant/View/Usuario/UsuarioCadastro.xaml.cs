using BLL.Login;
using BLL.Perfil;
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
using DAO;

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
            CarregaComboPerfil();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            var obrigatorios = VerificaCamposObrigatorios();
            if (obrigatorios.Count == 0)
            {
                if (VerificaSenhas())
                {
                    if (!VerificaUsuarioExistente(tbNome.Text)){
                        if (GravarUsuario())
                            Mensagens.GeraMensagens("Cadastro OK!", MENSAGEM.USUARIO_CADASTRO_SUCESSO, null, TIPOS_MENSAGENS.SUCESSO);
                        else
                            Mensagens.GeraMensagens("Erro ao cadastrar", MENSAGEM.USUARIO_CADASTRO_ERRO, null, TIPOS_MENSAGENS.ERRO);
                    }
                        
                    else
                        Mensagens.GeraMensagens(MENSAGEM.USUARIO_CADASTRADO, MENSAGEM.USUARIO_CADASTRADO, null, TIPOS_MENSAGENS.ALERTA);
                }
                else
                    Mensagens.GeraMensagens(MENSAGEM.SENHA_IDENTICA, MENSAGEM.SENHA_IDENTICA, null, TIPOS_MENSAGENS.ALERTA);
            }
            else
                Mensagens.GeraMensagens("Campos Obrigatórios",MENSAGEM.CAMPOS_OBRIGATORIOS + " - ",obrigatorios,TIPOS_MENSAGENS.ALERTA);
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
        private bool GravarUsuario()
        {
            var login = new LoginBLL().GravarLogin(CarregaLoginCadastro());
            if (login == null)
            {
                Mensagens.GeraMensagens(MENSAGEM.ERRO_GRAVAR_LOGIN, MENSAGEM.ERRO_GRAVAR_LOGIN, null, TIPOS_MENSAGENS.ERRO);
                return false;
            }

            if (new UsuarioBLL().GravarUsuario(CarregaUsuarioCadastro(login)))
                return true;

            return false;
        }
        private void CarregaComboPerfil()
        {
            cbPerfil.ItemsSource = new PerfilBLL().RecuperaTodosPerfil();
            cbPerfil.SelectedIndex = 0; //Administrador
        }
        private PerfilModel CarregaPerfilCadastro()
        {
            var perfil = new PerfilModel();
            perfil.Perfil = cbPerfil.SelectionBoxItem.ToString();
            perfil.IdPerfil = new PerfilBLL().RecuperaIdPerfil(perfil);

            return perfil;
        }
        private LoginModel CarregaLoginCadastro()
        {
            var login = new LoginModel();
            login.Login = tbNome.Text;
            login.Senha = tbSenha.Password;
            return login;
        }
        private UsuarioModel CarregaUsuarioCadastro(LoginModel login)
        {
            var user = new UsuarioModel();
            user.Email = tbEmail.Text;
            user.Nome = tbNome.Text;
            user.Status = "A";
            user.Login = login;
            user.Perfil = CarregaPerfilCadastro();

            return user;
        }
    }
}
