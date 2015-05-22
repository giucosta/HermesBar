using DAO.Utils;
using MODEL.Caixa;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

namespace DAO.Caixa
{
    public class CartaoDAO
    {
        public bool Salvar(CartaoModel cartao)
        {
            try
            {
                AccessObject<CartaoModel> AO = new AccessObject<CartaoModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("NumeroCartao", cartao.NumeroCartao);
                AO.InsertParameter("Cliente", cartao.Cliente.Id);
                AO.InsertParameter("Data", cartao.Data.ToShortDateString());
                AO.InsertParameter("ValorTotal", cartao.ValorTotal);
                AO.InsertParameter("FormaPagamento", cartao.FormaPagamento);
                AO.InsertParameter("HoraEntrada", cartao.HoraEntrada);
                AO.InsertParameter("HoraSaida", cartao.HoraSaida);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable UltimoNumeroCartao()
        {
            try
            {
                AccessObject<CartaoModel> AO = new AccessObject<CartaoModel>();
                AO.CreateSpecificQuery("SELECT MAX(NumeroCartao) AS NumeroCartao FROM Cartao");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Data", ConstantesDAO.EQUAL, DateTime.Now.ToShortDateString());

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable Pesquisar(CartaoModel cartao)
        {
            try
            {
                AccessObject<CartaoModel> AO = new AccessObject<CartaoModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertComparisionAttribute();
                if(!string.IsNullOrEmpty(cartao.NumeroCartao))
                    AO.InsertParameter(ConstantesDAO.AND, "NumeroCartao", ConstantesDAO.EQUAL, cartao.NumeroCartao);
                AO.InsertParameter(ConstantesDAO.AND, "Data", ConstantesDAO.EQUAL, cartao.Data.ToShortDateString());
                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisar", "CartaoDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw;
            }
        }
        public DataTable RetornaIdCliente(CartaoModel cartao)
        {
            try
            {
                AccessObject<CartaoModel> AO = new AccessObject<CartaoModel>();
                AO.CreateSelectWithSimpleParameter("Id_Cliente");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Cartao", ConstantesDAO.EQUAL, cartao.Id);

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaIdCliente", "CartaoDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }

        public bool FecharCartao(CartaoModel cartao)
        {
            try
            {
                AccessObject<CartaoModel> AO = new AccessObject<CartaoModel>();
                AO.CreateSpecificQuery(@"UPDATE Cartao SET ValorTotal = @ValorTotal, FormaPagamento = @FormaPagamento, HoraSaida = (SELECT GETDATE()) WHERE Id_Cartao = @Id");
                AO.GetCommand();
                AO.InsertParameter("ValorTotal", cartao.ValorTotal);
                AO.InsertParameter("FormaPagamento", cartao.FormaPagamento);
                AO.InsertParameter("Id", cartao.Id);

                return AO.ExecuteCommand();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
