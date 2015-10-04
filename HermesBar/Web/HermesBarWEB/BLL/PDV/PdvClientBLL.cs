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

namespace BLL.PDV
{
    public class PdvClientBLL
    {
        private Pdv_ClientDAO _clientDAO = null;
        private Pdv_ClientDAO ClientDAO
        {
            get
            {
                if (_clientDAO == null)
                    _clientDAO = new Pdv_ClientDAO();
                return _clientDAO;
            }
        }

        public int Insert(PdvClientModel client, UsuarioModel user)
        {
            return Convert.ToInt32(ClientDAO.Insert(ConvertModelToEntity(client, user)).Rows[0]["SUCCESS"]);
        }
        public string GetCar(PdvClientModel client, UsuarioModel user)
        {
            try
            {
                return ClientDAO.GetCar(ConvertModelToEntity(client, user)).Rows[0]["NOM"].ToString();
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
        public bool FecharComanda(PdvClientModel client, UsuarioModel user)
        {
            try
            {
                return ClientDAO.CloseCommand(ConvertModelToEntity(client, user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }

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
    }
}
