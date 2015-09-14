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
        public bool Insert(PdvClientModel client, UsuarioModel user)
        {
            return ClientDAO.Insert(ConvertModelToEntity(client, user)).GetResults();
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
                
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
