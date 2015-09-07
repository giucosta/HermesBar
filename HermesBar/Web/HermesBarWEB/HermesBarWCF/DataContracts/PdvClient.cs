using BLL.PDV;
using MODEL.PDV.Client;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class PdvClient
    {
        private PdvClientBLL _clientBLL = null;
        private PdvClientBLL ClientBLL 
        { 
            get 
            { 
                if(_clientBLL == null) 
                    _clientBLL = new PdvClientBLL(); 
                return _clientBLL;
            } 
        }
        public bool Insert(PdvClientModel client, UsuarioModel user)
        {
            return ClientBLL.Insert(client, user);
        }
    }
}