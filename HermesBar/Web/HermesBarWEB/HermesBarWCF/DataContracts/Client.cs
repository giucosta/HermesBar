using BLL.Client;
using HELPER;
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
        private ClientBLL ClientBLL;
        public Client()
        {
            this.ClientBLL = Singleton<ClientBLL>.Instance();
        }

        public List<ClientModel> Get(ClientModel client, UsuarioModel user)
        {
            try
            {
                return ClientBLL.Get(client, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Insert(ClientModel client, UsuarioModel user, bool cadastroRapido)
        {
            try
            {
                return ClientBLL.Insert(client, user, cadastroRapido);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(ClientModel client, UsuarioModel user)
        {
            try
            {
                return ClientBLL.Update(client, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Inactive(ClientModel client, UsuarioModel user)
        {
            try
            {
                return ClientBLL.Inactive(client, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Active(ClientModel client, UsuarioModel user)
        {
            try
            {
                return ClientBLL.Active(client, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}