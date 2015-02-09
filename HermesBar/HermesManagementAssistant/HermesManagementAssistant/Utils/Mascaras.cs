using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HermesManagementAssistant.Utils
{
    class Mascaras : Window
    {
        public static TextBox VerificaMascaraCnpj(TextBox textBox, KeyEventArgs e){
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

        public static TextBox AplicaMascaraTelefone(TextBox textBox, KeyEventArgs e){
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
    }
}
