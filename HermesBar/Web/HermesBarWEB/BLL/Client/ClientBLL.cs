using BLL.Commom;
using DAO.Client;
using ENTITY.Client;
using MODEL.Client;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Client
{
    public class ClientBLL
    {
        private ClientDAO _clientDAO = null;
        private ClientDAO ClientDAO
        {
            get
            {
                if (_clientDAO == null)
                    _clientDAO = new ClientDAO();
                return _clientDAO;
            }
        }
        private ContatoBLL _contatoBLL = null;
        private ContatoBLL ContatoBLL
        {
            get
            {
                if (_contatoBLL == null)
                    _contatoBLL = new ContatoBLL();
                return _contatoBLL;
            }
        }
        public bool Insert(ClientModel client, UsuarioModel user)
        {
            var cli = ConvertModelToEntity(client, user);
            var con = ContatoBLL.ConvertModelToEntity(client.Contato, user);

            return Convert.ToInt32(ClientDAO.Insert(cli, con).Rows[0]["SUCCESS"]) != 0;
        }

        #region Private Methods
        private HMA_CLI ConvertModelToEntity(ClientModel client, UsuarioModel user)
        {
            try
            {
                var entity = new HMA_CLI();
                entity._ID = client.Id;
                entity.NASC = client.DataNascimento;
                entity._ATV = Convert.ToInt32(client.StatusSelected);
                entity._USR = user.Id;

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
