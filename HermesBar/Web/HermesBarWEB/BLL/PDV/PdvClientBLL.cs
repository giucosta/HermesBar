using DAO.PDV;
using ENTITY.PDV;
using MODEL.PDV.Client;
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
    public class PdvClientBLL
    {
        #region Singleton
        private Pdv_ClientDAO ClientDAO = Singleton<Pdv_ClientDAO>.Instance();
        #endregion

        public int Insert(PdvClientModel client, UsuarioModel user)
        {
            return Convert.ToInt32(ClientDAO.Insert(ConvertModelToEntity(client, user)).Rows[0]["SUCCESS"]);
        }
        public string GetCard(PdvClientModel client, UsuarioModel user)
        {
            try
            {
                return ClientDAO.GetCard(ConvertModelToEntity(client, user)).Rows[0]["NOM"].ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<PdvFechamentoClientModel> Fechamento(PdvClientModel client)
        {
            return ClientDAO.Close(new HMA_PDV_CLI() { _ID_CAI = client.IdCaixa, NUM_CAR = client.NumeroCartao }).DataTableToList<PdvFechamentoClientModel>();
        }
        public bool CloseCommands(PdvClientModel client, UsuarioModel user)
        {
            try
            {
                return ClientDAO.CloseCommands(ConvertModelToEntity(client, user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private HMA_PDV_CLI ConvertModelToEntity(PdvClientModel cli, UsuarioModel user)
        {
            try
            {
                var model = new HMA_PDV_CLI();
                model._ID = cli.Id;
                model._USR = user.Id;
                model._ID_CAI = cli.IdCaixa;
                model._ID_CLI = cli.IdCliente;
                model.CONS_TOT = cli.ConsumoTotal;
                model.HOR_ENT = cli.Entrada;
                model.HOR_SAI = cli.Saida;
                model.NUM_CAR = cli.NumeroCartao;
                model.FRM_PAG = cli.FormaPagamento;
                model.VLR_REC = cli.ValorRecebido;
                model.TRC = cli.Troco;
                
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
