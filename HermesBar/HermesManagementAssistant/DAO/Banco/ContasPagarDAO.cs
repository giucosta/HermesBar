using DAO.Utils;
using MODEL.Banco;
using System;
using System.Collections.Generic;
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
                AO.InsertParameter("Fornecedor", contasPagar.Fornecedor.Id);
                AO.InsertParameter("Referente", contasPagar.Referente);
                AO.InsertParameter("FormaPagamento", contasPagar.FormaPagamento);
                AO.InsertParameter("Parcelas", contasPagar.Parcelas);
                AO.InsertParameter("Valor", contasPagar.Valor);
                AO.InsertParameter("NumeroNota", contasPagar.NumeroNota);
                AO.InsertParameter("Observacao", contasPagar.Observacao);
                AO.InsertParameter("DataCadastro", contasPagar.DataCadastro);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "ContasPagarDAO", e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
            }
        }
    }
}
