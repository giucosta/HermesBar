﻿using BLL.Caixa;
using BLL.Funcionario;
using BLL.Produtos;
using DAO.Pedido;
using MODEL;
using MODEL.Caixa;
using MODEL.Pedido;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

namespace BLL.Pedido
{
    public class PedidoBLL
    {
        private PedidoDAO _pedidoDAO = null;
        public PedidoDAO PedidoDAO
        {
            get
            {
                if (_pedidoDAO == null)
                    _pedidoDAO = new PedidoDAO();
                return _pedidoDAO;
            }
        }
        private CartaoBLL _cartaoBLL = null;
        public CartaoBLL CartaoBLL
        {
            get
            {
                if (_cartaoBLL == null)
                    _cartaoBLL = new CartaoBLL();
                return _cartaoBLL;
            }
        }
        private ProdutoBLL _produtoBLL = null;
        public ProdutoBLL ProdutoBLL
        {
            get
            {
                if (_produtoBLL == null)
                    _produtoBLL = new ProdutoBLL();
                return _produtoBLL;
            }
        }
        private FuncionarioBLL _funcionarioBLL = null;
        public FuncionarioBLL FuncionarioBLL
        {
            get
            {
                if (_funcionarioBLL == null)
                    _funcionarioBLL = new FuncionarioBLL();
                return _funcionarioBLL;
            }
        }

        public bool Salvar(PedidoModel pedido)
        {
            try
            {
                pedido.Data = DateTime.Now;
                return PedidoDAO.Salvar(pedido);
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return false;
            }
        }
        public List<PedidoModel> Pesquisar(PedidoModel pedido)
        {
            try
            {
                pedido.Data = DateTime.Now;
                var list = PedidoDAO.Pesquisar(pedido).DataTableToList<PedidoModel>();
                foreach (var item in list)
                {
                    item.NumeroCartao = RecuperaCartao(item);
                    item.CodigoProduto = RecuperaProduto(item);
                    item.CodigoFuncionario = RecuperaFuncionario(item);
                }
                return list;
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
        private CartaoModel RecuperaCartao(PedidoModel pedido)
        {
            try
            {
                pedido.NumeroCartao = new CartaoModel();
                pedido.NumeroCartao.NumeroCartao = PedidoDAO.RecuperaNumeroCartao(pedido).DataTableToString();
                pedido.NumeroCartao = CartaoBLL.Pesquisar(pedido.NumeroCartao).FirstOrDefault();

                return pedido.NumeroCartao;
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
        private ProdutoModel RecuperaProduto(PedidoModel pedido)
        {
            try
            {
                pedido.CodigoProduto = new ProdutoModel();
                pedido.CodigoProduto.CodigoOriginal = PedidoDAO.RecuperaNumeroProduto(pedido).DataTableToString();
                pedido.CodigoProduto = ProdutoBLL.PesquisaProdutoCodigo(pedido.CodigoProduto).FirstOrDefault();

                return pedido.CodigoProduto;
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
        private FuncionarioModel RecuperaFuncionario(PedidoModel pedido)
        {
            try
            {
                pedido.CodigoFuncionario = new FuncionarioModel();
                pedido.CodigoFuncionario.Id = Convert.ToInt16(PedidoDAO.RecuperaCodigoFuncionario(pedido).Rows[0]["CodigoFuncionario"]);
                pedido.CodigoFuncionario = FuncionarioBLL.PesquisaFuncionarioId(pedido.CodigoFuncionario);

                return pedido.CodigoFuncionario;
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
    }
}
