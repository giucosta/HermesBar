using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Utils;
using MODEL.Caixa;
using System.Data;

namespace DAO.Caixa
{
    public class CaixaAbertoDAO
    {
        public DataTable RetornaMovimentacoes()
        {
            try
            {
                AccessObject<CaixaModel> AO = new AccessObject<CaixaModel>();
                AO.CreateSelectAll();
                AO.GetCommand();

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool AbrirCaixa(CaixaModel caixa)
        {
            try
            {
                AccessObject<CaixaModel> AO = new AccessObject<CaixaModel>();
                AO.CreateSpecificQuery("INSERT INTO Caixa(ValorInicial, ObservacaoAbertura, DataAbertura) VALUES (@ValorInicial, @ObservacaoAbertura, @DataAbertura)");
                AO.GetCommand();
                AO.InsertParameter("ValorInicial", caixa.ValorInicial);
                AO.InsertParameter("ObservacaoAbertura", caixa.ObservacaoAbertura);
                AO.InsertParameter("DataAbertura", caixa.DataAbertura);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
