using BLL.Cliente;
using DAO.Caixa;
using DAO.Cliente;
using DAO.Utils;
using MODEL.Caixa;
using MODEL.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

namespace BLL.Caixa
{
    public class CartaoBLL
    {
        private CartaoDAO _cartaoDAO = null;
        public CartaoDAO CartaoDAO
        {
            get
            {
                if (_cartaoDAO == null)
                    _cartaoDAO = new CartaoDAO();
                return _cartaoDAO;
            }
        }
        private ClienteBLL _clienteBLL = null;
        public ClienteBLL ClienteBLL
        {
            get
            {
                if (_clienteBLL == null)
                    _clienteBLL = new ClienteBLL();
                return _clienteBLL;
            }
        }

        public bool Salvar(CartaoModel cartao, bool clienteExistente)
        {
            try
            {
                AccessObject<CartaoModel> AO = new AccessObject<CartaoModel>();
                AO.GetTransaction();

                if (!clienteExistente)
                {
                    if(ClienteBLL.Salvar(cartao.Cliente))
                        cartao.Cliente = ClienteBLL.RecuperaUltimoCliente();
                    else
                    {
                        AO.Rollback();
                        return false;
                    }
                }
                cartao.Data = DateTime.Now;
                cartao.HoraEntrada = DateTime.Now;
                cartao.FormaPagamento = "";
                cartao.HoraSaida = DateTime.Now;
                if (CartaoDAO.Salvar(cartao))
                {
                    AO.Commit();
                    return true;
                }
                else
                    AO.Rollback();

                return false;
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return false;
            }
        }
        public int RecuperaUltimoCartao()
        {
            try
            {
                return int.Parse(CartaoDAO.UltimoNumeroCartao().Rows[0]["NumeroCartao"].ToString()) + 1;
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return 0;
            }
        }
        public List<CartaoModel> Pesquisar(CartaoModel cartao)
        {
            try
            {
                cartao.Data = DateTime.Now;
                var cartoes = CartaoDAO.Pesquisar(cartao).DataTableToList<CartaoModel>();
                foreach (var item in cartoes)
                    item.Cliente = RecuperaCliente(item);

                return cartoes;
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
        public ClienteModel RecuperaCliente(CartaoModel cartao)
        {
            try
            {
                ClienteModel cliente = new ClienteModel();
                cliente.Id = Convert.ToInt16(CartaoDAO.RetornaIdCliente(cartao).Rows[0]["Id_Cliente"]);
                return ClienteBLL.Pesquisar(cliente).First();
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
    }
}
