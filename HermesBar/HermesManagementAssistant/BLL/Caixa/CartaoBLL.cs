using BLL.Cliente;
using DAO.Caixa;
using DAO.Cliente;
using DAO.Utils;
using MODEL.Caixa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool Salvar(CartaoModel cartao)
        {
            try
            {
                AccessObject<CartaoModel> AO = new AccessObject<CartaoModel>();
                AO.GetTransaction();

                if (ClienteBLL.Salvar(cartao.Cliente))
                {
                    cartao.Cliente = ClienteBLL.RecuperaUltimoCliente();
                    cartao.Data = DateTime.Now;
                    cartao.HoraEntrada = DateTime.Now;
                    cartao.FormaPagamento = "";
                    cartao.HoraSaida = DateTime.Now;
                    if (CartaoDAO.Salvar(cartao))
                    {
                        AO.Commit();
                        return true;
                    }
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
    }
}
