using MODEL.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Banco;
using UTIL;
using BLL.Fornecedor;
using MODEL.Fornecedor;

namespace BLL.Banco
{
    public class ContasPagarBLL
    {
        private ContasPagarDAO _contasPagarDAO = null;
        public ContasPagarDAO ContasPagarDAO
        {
            get
            {
                if (_contasPagarDAO == null)
                    _contasPagarDAO = new ContasPagarDAO();
                return _contasPagarDAO;
            }
        }
        private FornecedorBLL _fornecedorBLL = null;
        public FornecedorBLL FornecedorBLL
        {
            get
            {
                if (_fornecedorBLL == null)
                    _fornecedorBLL = new FornecedorBLL();
                return _fornecedorBLL;
            }
        }
        private CentroCustoBLL _centroCustoBLL = null;
        public CentroCustoBLL CentroCustoBLL
        {
            get
            {
                if (_centroCustoBLL == null)
                    _centroCustoBLL = new CentroCustoBLL();
                return _centroCustoBLL;
            }
        }
        public bool Salvar(ContasPagarModel contasPagar)
        {
            try
            {
                return ContasPagarDAO.Salvar(contasPagar);
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return false;
            }
        }
        public List<ContasPagarGridModel> Pesquisar(ContasPagarModel contasPagar, DateTime dataFinal)
        {
            var contas = ContasPagarDAO.Pesquisar(contasPagar, DateTime.Now).DataTableToList<ContasPagarModel>();
            var grid = new List<ContasPagarGridModel>();
            foreach (var item in contas)
            {
                item.Fornecedor = RecuperaFornecedor(item);
                item.CentroCusto = RecuperaCentroCusto(item);
            }
            
            foreach (var item in contas)
            {
                TimeSpan atraso;
                if (item.DataVencimento < DateTime.Now)
                    atraso = DateTime.Now - item.DataVencimento;
                else
                    atraso = item.DataVencimento - DateTime.Now;

                grid.Add(new ContasPagarGridModel()
                {
                    Fornecedor = item.Fornecedor.RazaoSocial,
                    DataVencimento = item.DataVencimento.ToShortDateString(),
                    FormaPagamento = item.FormaPagamento,
                    Parcelas = item.Parcelas,
                    Status = item.Status,
                    Valor = item.Valor,
                    Referente = item.Referente,
                    DiasAtraso = atraso.Days
                });
            }
            
            return grid;
        }
        public FornecedorModel RecuperaFornecedor(ContasPagarModel contas)
        {
            return FornecedorBLL.PesquisaFornecedorPorId(Convert.ToInt16(ContasPagarDAO.RetornaFornecedorId(contas).Rows[0]["Id_Fornecedor"]));
        }
        public CentroCustoModel RecuperaCentroCusto(ContasPagarModel contas)
        {
            return CentroCustoBLL.RecuperaCentroCusto(contas.CentroCusto);
        }
    }
}
