using MODEL.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Utils;

namespace DAO.Banco
{
    public class ContasReceberDAO
    {
        public bool InserirContasReceber(ContasReceberModel contaReceber)
        {
            try
            {
                AccessObject<ContasReceberModel> AO = new AccessObject<ContasReceberModel>();
                AO.CreateDataInsert();
                AO.GetCommand();

                AO.InsertParameter("DataEmissao", contaReceber.DataEmissao);
                AO.InsertParameter("DataVencimento", contaReceber.DataVencimento);
                AO.InsertParameter("Cliente", contaReceber.Cliente.Id);
                AO.InsertParameter("CentroCusto", contaReceber.CentroCusto.Id);
                AO.InsertParameter("Referente", contaReceber.Referente);
                AO.InsertParameter("FormaPagamento", contaReceber.FormaPagamento);
                AO.InsertParameter("Parcelas", contaReceber.Parcelas);
                AO.InsertParameter("Valor", contaReceber.Valor);
                AO.InsertParameter("ValorPago", contaReceber.ValorPago);
                AO.InsertParameter("Observacao", contaReceber.Observacao);
                AO.InsertParameter("DataCadastro", contaReceber.DataCadastro);
                AO.InsertParameter("Status", contaReceber.Status);

                return AO.ExecuteCommand();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
