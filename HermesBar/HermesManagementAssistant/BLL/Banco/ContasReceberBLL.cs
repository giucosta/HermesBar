using DAO.Banco;
using MODEL.Banco;
using MODEL.Caixa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Banco
{
    public class ContasReceberBLL
    {
        private ContasReceberDAO _contasReceberDAO = null;
        public ContasReceberDAO ContasReceberDAO
        {
            get
            {
                if (_contasReceberDAO == null)
                    _contasReceberDAO = new ContasReceberDAO();
                return _contasReceberDAO;
            }
        }

        public bool InserirContasReceber(CartaoModel cartao)
        {
            try
            {
                var contasReceber = new ContasReceberModel();
                contasReceber.Cliente = cartao.Cliente;
                contasReceber.CentroCusto = new CentroCustoModel() { Id = 1 };
                contasReceber.DataCadastro = DateTime.Now;
                contasReceber.DataEmissao = DateTime.Now;
                contasReceber.DataVencimento = DateTime.Now;
                contasReceber.FormaPagamento = cartao.FormaPagamento;
                contasReceber.Observacao = "Pagamento comanda";
                contasReceber.Referente = "Pagamento comanda";
                contasReceber.Valor = cartao.ValorTotal;
                contasReceber.ValorPago = cartao.ValorTotal;
                contasReceber.Status = "F";
                contasReceber.Parcelas = 0;

                return ContasReceberDAO.InserirContasReceber(contasReceber);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
