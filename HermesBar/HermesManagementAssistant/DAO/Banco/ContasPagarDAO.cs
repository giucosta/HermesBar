using DAO.Utils;
using MODEL.Banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

namespace DAO.Banco
{
    public class ContasPagarDAO
    {
        public bool Salvar(ContasPagarModel contasPagar)
        {
            try
            {
                AccessObject<ContasPagarModel> AO = new AccessObject<ContasPagarModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("DataEmissao", contasPagar.DataEmissao);
                AO.InsertParameter("DataVencimento", contasPagar.DataVencimento);
                AO.InsertParameter("Fornecedor", contasPagar.Fornecedor.Id);
                AO.InsertParameter("CentroCusto", contasPagar.CentroCusto.Id);
                AO.InsertParameter("Referente", contasPagar.Referente);
                AO.InsertParameter("FormaPagamento", contasPagar.FormaPagamento);
                AO.InsertParameter("Parcelas", contasPagar.Parcelas);
                AO.InsertParameter("Valor", contasPagar.Valor);
                AO.InsertParameter("ValorPago", contasPagar.ValorPago);
                AO.InsertParameter("NumeroNota", contasPagar.NumeroNota);
                AO.InsertParameter("Observacao", contasPagar.Observacao);
                AO.InsertParameter("DataCadastro", contasPagar.DataCadastro);
                AO.InsertParameter("Status", contasPagar.Status);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "ContasPagarDAO", e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
            }
        }
        public DataTable Pesquisar(ContasPagarModel contasPagar, DateTime? dataFinal)
        {
            try
            {
                AccessObject<ContasPagarModel> AO = new AccessObject<ContasPagarModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertComparisionAttribute();
                if (!string.IsNullOrEmpty(contasPagar.Status))
                    AO.InsertParameter(ConstantesDAO.AND, "Status", ConstantesDAO.EQUAL, contasPagar.Status);
                if (dataFinal != null && contasPagar.DataEmissao != DateTime.MinValue)
                    AO.InsertParameter(ConstantesDAO.AND, "DataEmissao", ConstantesDAO.BETWEEN, contasPagar.DataEmissao, ConstantesDAO.AND, dataFinal);
                if (contasPagar.CentroCusto.Id != 0)
                    AO.InsertParameter(ConstantesDAO.AND, "Id_CentroCusto", ConstantesDAO.EQUAL, contasPagar.CentroCusto.Id);

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisar", "ContasPagarDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public DataTable RetornaFornecedorId(ContasPagarModel contasPagar)
        {
            try
            {
                AccessObject<ContasPagarModel> AO = new AccessObject<ContasPagarModel>();
                AO.CreateSelectWithSimpleParameter("Id_Fornecedor");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_ContasPagar", ConstantesDAO.EQUAL, contasPagar.Id);

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaFornecedorId", "ContasPagarDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public DataTable RetornaCentroCustoId(ContasPagarModel contasPagar)
        {
            try
            {
                AccessObject<ContasPagarModel> AO = new AccessObject<ContasPagarModel>();
                AO.CreateSelectWithSimpleParameter("Id_CentroCusto");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_ContasPagar", ConstantesDAO.EQUAL, contasPagar.Id);

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaCentroCustoId", "ContasPagarDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
    }
}
