using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Pedido;
using UTIL;
using DAO.Utils;
using System.Data;

namespace DAO.Pedido
{
    public class PedidoDAO
    {
        public bool Salvar(PedidoModel pedido)
        {
            try
            {
                AccessObject<PedidoModel> AO = new AccessObject<PedidoModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("NumeroCartao", pedido.NumeroCartao.NumeroCartao);
                AO.InsertParameter("CodigoFuncionario", pedido.CodigoFuncionario.Id);
                AO.InsertParameter("CodigoProduto", pedido.CodigoProduto.CodigoOriginal);
                AO.InsertParameter("Quantidade", pedido.Quantidade);
                AO.InsertParameter("Observacao", pedido.Observacao);
                AO.InsertParameter("Data", pedido.Data.ToShortDateString());

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "PedidoDAO", e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
            }
        }
        public DataTable Pesquisar(PedidoModel pedido)
        {
            try
            {
                AccessObject<PedidoModel> AO = new AccessObject<PedidoModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertComparisionAttribute();
                if (!string.IsNullOrEmpty(pedido.NumeroCartao.NumeroCartao))
                    AO.InsertParameter(ConstantesDAO.AND, "NumeroCartao", ConstantesDAO.EQUAL, pedido.NumeroCartao.NumeroCartao);
                AO.InsertParameter(ConstantesDAO.AND, "Data", ConstantesDAO.EQUAL, pedido.Data.ToShortDateString());

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisar", "PedidoDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public DataTable RecuperaNumeroCartao(PedidoModel pedido)
        {
            try
            {
                AccessObject<PedidoModel> AO = new AccessObject<PedidoModel>();
                AO.CreateSelectWithSimpleParameter("NumeroCartao");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Pedido", ConstantesDAO.EQUAL, pedido.Id);

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaNumeroCartao", "PedidoDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public DataTable RecuperaNumeroProduto(PedidoModel pedido)
        {
            try
            {
                AccessObject<PedidoModel> AO = new AccessObject<PedidoModel>();
                AO.CreateSelectWithSimpleParameter("CodigoProduto");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Pedido", ConstantesDAO.EQUAL, pedido.Id);

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaNumeroProduto", "PedidoDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public DataTable RecuperaCodigoFuncionario(PedidoModel pedido)
        {
            try
            {
                AccessObject<PedidoModel> AO = new AccessObject<PedidoModel>();
                AO.CreateSelectWithSimpleParameter("CodigoFuncionario");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Pedido", ConstantesDAO.EQUAL, pedido.Id);

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaCodigoFuncionario", "PedidoDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
    }
}
