using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Utils
{
    public static class Mensagens
    {
        public static void GeraMensagens(string titulo, string mensagemErro, List<String> erros, string tipo)
        {
            var msg = "";
            if (erros == null)
                erros = new List<string>();
            foreach (string x in erros)
                msg += x + " - ";

            if (tipo == TIPOS_MENSAGENS.SUCESSO)
                MessageBox.Show(mensagemErro + msg, titulo, MessageBoxButton.OK, MessageBoxImage.Information);
            if (tipo == TIPOS_MENSAGENS.ALERTA)
                MessageBox.Show(mensagemErro + msg, titulo, MessageBoxButton.OK, MessageBoxImage.Warning);
            if (tipo == TIPOS_MENSAGENS.ERRO)
                MessageBox.Show(mensagemErro + msg, titulo, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    public static class TIPOS_MENSAGENS
    {
        public const string ERRO = "Erro";
        public const string ALERTA = "Alerta";
        public const string SUCESSO = "Sucesso";
    }

    public static class MENSAGEM
    {
        public const string USUARIO_CADASTRADO = "Usuário já cadastrado";
        public const string SENHA_IDENTICA = "A senha e a confirmação devem ser identicas";
        public const string CAMPOS_OBRIGATORIOS = "Campos obrigatórios não preenchidos";
        public const string LOGIN_INVALIDO = "Login e/ou senha inválido";
        public const string NOVA_SENHA_EMAIL = "Sua nova senha foi enviada para o email cadastrado!";
        public const string ERRO_ENVIA_EMAIL = "Ocorreu um erro ao enviar email, favor consultar o administrador do sistema";
        public const string PREENCHE_CAMPO_LOGIN = "Favor preencher o campo LOGIN";

        //Login
        public const string ERRO_GRAVAR_LOGIN = "Erro ao gravar o login";

        //CADASTRO USUARIO
        public const string USUARIO_CADASTRO_SUCESSO = "Usuário cadastrado com sucesso!";
        public const string USUARIO_CADASTRO_ERRO = "Erro ao cadastrar usuário!";
    }
}
