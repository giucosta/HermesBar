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
        private PdvPedidoBLL _pedidoBLL = null;
        private PdvPedidoBLL PedidoBLL
        {
            get
            {
                if (_pedidoBLL == null)
                    _pedidoBLL = new PdvPedidoBLL();
                return _pedidoBLL;
            }
        }
        public bool Insert(PdvClientModel client, UsuarioModel user)
        {
            return ClientBLL.Insert(client, user);
        }
        public string GetCar(PdvClientModel client, UsuarioModel user)
        {
            return ClientBLL.GetCar(client, user);
        }
        public bool Pedido(string cartaoCliente, string codigoAtendente, string nomeProduto, string quantidade, UsuarioModel user)
        {
            return PedidoBLL.Insert(cartaoCliente, codigoAtendente, nomeProduto, quantidade, user);
        }
    }
}