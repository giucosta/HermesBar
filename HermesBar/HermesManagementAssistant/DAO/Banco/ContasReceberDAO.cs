using MODEL.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Utils;
using UTIL;
using System.Data;

namespace DAO.Banco
{
    public class ContasReceberDAO
    {
        public bool Salvar(ContasReceberModel contasReceber)
        {
            try
            {
                AccessObject<ContasReceberModel> AO = new AccessObject<ContasReceberModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("DataEmissao",contasReceber.DataEmissao);
                AO.InsertParameter("DataVencimento",contasReceber.DataVencimento);
                AO.InsertParameter("Fornecedor",contasReceber.Fornecedor.Id);
                AO.InsertParameter("CentroCusto", contasReceber.CentroCusto.Id);
                AO.InsertParameter("Referente", contasReceber.Referente);
                AO.InsertParameter("FormaPagamento", contasReceber.FormaPagamento);
                AO.InsertParameter("Parcelas", contasReceber.Parcelas);
                AO.InsertParameter("Valor", contasReceber.Valor);
                AO.InsertParameter("ValorRecebido", contasReceber.ValorRecebido);
                AO.InsertParameter("Observacao", contasReceber.Observacao);
                AO.InsertParameter("DataCadastor", contasReceber.DataCadastro);
                AO.InsertParameter("Status", contasReceber.Status);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "ContasReceberDAO", e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
            }
        }
        public bool Excluir(ContasReceberModel contasReceber)
        {
            try
            {
                AccessObject<ContasReceberModel> AO = new AccessObject<ContasReceberModel>();
                AO.DeleteFromId();
                AO.GetCommand();

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir", "ContasReceberDAO", e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
            }
        }
        public bool Editar(ContasReceberModel contasReceber)
        {
            try
            {
                AccessObject<ContasReceberModel> AO = new AccessObject<ContasReceberModel>();
                AO.CreateSpecificQuery(@"UPDATE ContasReceber SET DataEmissao = @DataEmissao, DataVencimento = @DataVencimento, Id_Fornecedor = @Fornecedor, Id_CentroCusto = @CentroCusto, Referente = @Referente, FormaPagamento = @FormaPagamento, Parcelas = @Parcelas, Valor = @Valor, ValorRecebido = @ValorRecebido, Observacao = @Observacao, Status = @Status");
                AO.GetCommand();
                AO.InsertParameter("DataEmissao", contasReceber.DataEmissao);
                AO.InsertParameter("DataVencimento", contasReceber.DataVencimento);
                AO.InsertParameter("Fornecedor", contasReceber.Fornecedor.Id);
                AO.InsertParameter("CentroCusto", contasReceber.CentroCusto.Id);
                AO.InsertParameter("Referente", contasReceber.Referente);
                AO.InsertParameter("FormaPagamento", contasReceber.FormaPagamento);
                AO.InsertParameter("Parcelas", contasReceber.Parcelas);
                AO.InsertParameter("Valor", contasReceber.Valor);
                AO.InsertParameter("ValorRecebido", contasReceber.ValorRecebido);
                AO.InsertParameter("Observacao", contasReceber.Observacao);
                AO.InsertParameter("Status", contasReceber.Status);
                
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_ContasReceber", ConstantesDAO.EQUAL, contasReceber.Id);
                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Editar", "ContasReceberDAO", e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
            }
        }
        
    }
}
