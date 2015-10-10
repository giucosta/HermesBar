using DAO.PDV;
using ENTITY.PDV;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
using HELPER;

namespace BLL.PDV
{
    public class PdvPedidoBLL
    {
        #region Singleton
        private Pdv_OrderDAO Pdv_OrderDAO = Singleton<Pdv_OrderDAO>.Instance();
        #endregion

        public bool Insert(string cartaoCliente, string codigoAtendente, string nomeProduto, string quantidade, UsuarioModel user, int idCaixa)
        {
            return Pdv_OrderDAO.Insert(
                LoadModel(cartaoCliente, codigoAtendente, nomeProduto, quantidade, user, idCaixa))
                .GetResults();
        }

        #region Private Methods
        private HMA_PDV_PED_CLI LoadModel(string cartaoCliente, string codigoAtendente, string nomeProduto, string quantidade, UsuarioModel user, int idCaixa)
        {
            var model = new HMA_PDV_PED_CLI()
            {
                _USR = user.Id
                ,
                _ID_CLI = Convert.ToInt32(cartaoCliente)
                ,
                _ID_FUNC = Convert.ToInt32(codigoAtendente)
                ,
                _ID_PROD = nomeProduto
                ,
                QTD = Convert.ToInt32(quantidade)
                ,
                CAI = idCaixa
            };
            return model;
        }
        #endregion
    }
}
