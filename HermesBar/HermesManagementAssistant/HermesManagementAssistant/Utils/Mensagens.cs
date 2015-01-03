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
}
