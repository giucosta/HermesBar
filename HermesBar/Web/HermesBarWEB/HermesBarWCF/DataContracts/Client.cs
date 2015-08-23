using BLL.Client;
using MODEL.Client;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class Client
    {
        private ClientBLL _clientBLL = null;
        private ClientBLL ClientBLL
        {
            get
            {
                if (_clientBLL == null)
                    _clientBLL = new ClientBLL();
                return _clientBLL;
            }
        }

        public List<ClientModel> Get(ClientModel client, UsuarioModel user)
        {
            return ClientBLL.Get(client, user);
        }
        public bool Insert(ClientModel client, UsuarioModel user)
        {
            return ClientBLL.Insert(client, user);
        }
    }
}