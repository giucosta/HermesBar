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
        public List<FormaPagamentoModel> RetornaFormas()
        {
            var list = new List<FormaPagamentoModel>();
            list.Add(new FormaPagamentoModel() { FormaPagamento = Constantes.ATipoPagamento.Boleto });
            list.Add(new FormaPagamentoModel() { FormaPagamento = Constantes.ATipoPagamento.CartaoCredito });
            list.Add(new FormaPagamentoModel() { FormaPagamento = Constantes.ATipoPagamento.CartaoDebito });
            list.Add(new FormaPagamentoModel() { FormaPagamento = Constantes.ATipoPagamento.Cheque });
            list.Add(new FormaPagamentoModel() { FormaPagamento = Constantes.ATipoPagamento.Dinheiro });

            return list;
        }
    }
}
