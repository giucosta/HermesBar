using MODEL.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

namespace BLL.Banco
{
    public class FormaPagamentoBLL
    {
        public List<string> RetornaFormas()
        {
            var list = new List<string>();
            list.Add(Constantes.ATipoPagamento.Boleto);
            list.Add(Constantes.ATipoPagamento.CartaoCredito);
            list.Add(Constantes.ATipoPagamento.CartaoDebito);
            list.Add(Constantes.ATipoPagamento.Cheque);
            list.Add(Constantes.ATipoPagamento.Dinheiro);

            return list;
        }
    }
}
