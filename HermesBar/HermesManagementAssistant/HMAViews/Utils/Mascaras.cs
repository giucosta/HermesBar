using HMAViews.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HMAViews.Mascara
{
    public class Mascaras : Window
    {
        public static TextBox CnpjMasked(TextBox textBox, KeyEventArgs e){
            if (Verificadores.VerificaNumero(textBox.Text))
            {
                if (textBox.Text.Length == 14)
                {
                    char[] caracter = textBox.Text.ToCharArray();
                    var cnpj = "";
                    for (int i = 0; i < 2; i++)
                        cnpj += caracter[i];
                    cnpj += ".";
                    for (int i = 2; i < 5; i++)
                        cnpj += caracter[i];
                    cnpj += ".";
                    for (int i = 5; i < 8; i++)
                        cnpj += caracter[i];
                    cnpj += "/";
                    for (int i = 8; i < 12; i++)
                        cnpj += caracter[i];
                    cnpj += "-";
                    for (int i = 12; i < 14; i++)
                        cnpj += caracter[i];

                    textBox.Text = cnpj;
                    return textBox;
                }
                else
                    return textBox;
            }
            textBox.Text = "";
            return textBox;
        }
        public static TextBox CpfMasked(TextBox textBox, KeyEventArgs e){
            if (Verificadores.VerificaNumero(textBox.Text))
            {
                if (textBox.Text.Length == 11)
                {
                    char[] caracter = textBox.Text.ToCharArray();
                    var cpf = "";
                    for (int i = 0; i < 3; i++)
                        cpf += caracter[i];
                    cpf += ".";
                    for (int i = 3; i < 6; i++)
                        cpf += caracter[i];
                    cpf += ".";
                    for (int i = 6; i < 9; i++)
                        cpf += caracter[i];
                    cpf += "-";
                    for (int i = 9; i < 11; i++)
                        cpf += caracter[i];

                    textBox.Text = cpf;
                    return textBox;
                }
                else
                    return textBox;
            }
            textBox.Text = "";
            return textBox;
        }
        public static TextBox PhoneMasked(TextBox textBox, KeyEventArgs e){
            if (Verificadores.VerificaNumero(textBox.Text))
            {
                if (textBox.Text.Length == 10)
                {
                    char[] caracter = textBox.Text.ToCharArray();
                    var telefone = "(";
                    for (int i = 0; i < 2; i++)
                        telefone += caracter[i];
                    telefone += ")";
                    for (int i = 2; i < 6; i++)
                        telefone += caracter[i];
                    telefone += "-";
                    for (int i = 6; i < 10; i++)
                        telefone += caracter[i];

                    textBox.Text = telefone;
                }
                return textBox;
            }
            textBox.Text = "";
            return textBox;
        }
        public static TextBox CnpjCpfMasked(TextBox textBox, KeyEventArgs e)
        {
            if (textBox.Text.Length == 11)
                return CpfMasked(textBox, e);

            return CnpjMasked(RetiraElementos(textBox), e);
        }
        private static TextBox RetiraElementos(TextBox textBox)
        {
            var elementos = textBox.Text.Replace(".", "");
            elementos = elementos.Replace("-", "");
            textBox.Text = elementos;
            return textBox;
        }
        public static void SomenteNumeros(TextBox textBox, KeyEventArgs e)
        {
            KeyConverter key = new KeyConverter();
            if (e.Key==Key.Tab) 
                return;
            if ((char.IsNumber((string)key.ConvertTo(e.Key,typeof(string)),0)==false))
                e.Handled = true;
        }
    }
}
