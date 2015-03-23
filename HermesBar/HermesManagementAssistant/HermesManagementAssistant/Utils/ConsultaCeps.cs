using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HermesManagementAssistant.Utils
{
    public class ConsultaCeps : Window
    {
        /// <summary>
        /// Passar como parametros os componentes de endereço da tela! Esses endereços deverão ser fixos, sempre com o mesmo nome padrão
        /// </summary>
        /// <param name="tbRua"></param>
        /// <param name="tbCidade"></param>
        /// <param name="tbBairro"></param>
        /// <param name="cbEstado"></param>
        /// <param name="tbCep"></param>
        public static bool ConsultarCep(TextBox rua, TextBox cidade, TextBox bairro, ComboBox uf, TextBox cep){
            List<ConsultaCep.Endereco> enderecos = null;
            enderecos = ConsultaCep.ConsultaCep.GetEnderecos(cep.Text);
            if (enderecos.Count != 0)
            {
                foreach (ConsultaCep.Endereco x in enderecos)
                {
                    rua.Text = x.Logradouro;
                    cidade.Text = x.Localidade;
                    bairro.Text = x.Bairro;
                    var i = 0;
                    foreach (var s in uf.ItemsSource)
                    {
                        if (x.uf == s.ToString())
                            uf.SelectedIndex = i;
                        else
                            i++;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
