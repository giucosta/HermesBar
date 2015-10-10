using DAO.PDV;
using ENTITY.PDV;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;

namespace BLL.PDV
{
    public class PdvPedidoBLL
    {
        private Pdv_OrderDAO _pedidoDAO = null;
        private Pdv_OrderDAO PedidoDAO
        {
            get
            {
                if (_pedidoDAO == null)
                    _pedidoDAO = new Pdv_OrderDAO();
                return _pedidoDAO;
            }
        }

        public bool Insert(string cartaoCliente, string codigoAtendente, string nomeProduto, string quantidade, UsuarioModel user, int idCaixa)
        {
            var model = new HMA_PDV_PED_CLI();
            model._USR = user.Id;
            model._ID_CLI = Convert.ToInt32(cartaoCliente);
            model._ID_FUNC = Convert.ToInt32(codigoAtendente);
            model._ID_PROD = nomeProduto;
            model.QTD = Convert.ToInt32(quantidade);
            model.CAI = idCaixa;
            return PedidoDAO.Insert(model).GetResults();
        }
    }
}
