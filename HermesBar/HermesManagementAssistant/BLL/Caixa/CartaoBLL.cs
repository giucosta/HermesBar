using DAO.Caixa;
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

        public bool Salvar(CartaoModel cartao)
        {
            try
            {
                return CartaoDAO.Salvar(cartao);
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return false;
            }
        }
    }
}
