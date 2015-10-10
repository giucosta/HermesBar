using BLL.PDV;
using HELPER;
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
        private PdvClientBLL ClientBLL;
        private PdvOrderBLL OrderBLL;
        public PdvClient()
        {
            this.ClientBLL = Singleton<PdvClientBLL>.Instance();
            this.OrderBLL = Singleton<PdvOrderBLL>.Instance();
        }

        public int Insert(PdvClientModel client, UsuarioModel user)
        {
            return ClientBLL.Insert(client, user);
        }
        public string GetCard(PdvClientModel client, UsuarioModel user)
        {
            return ClientBLL.GetCard(client, user);
        }
        public bool Order(string cartaoCliente, string codigoAtendente, string nomeProduto, string quantidade, UsuarioModel user, int idCaixa)
        {
            return OrderBLL.Insert(cartaoCliente, codigoAtendente, nomeProduto, quantidade, user, idCaixa);
        }
        public List<PdvFechamentoClientModel> Close(PdvClientModel client)
        {
            return ClientBLL.Fechamento(client);
        }
        public bool CloseCommands(PdvClientModel client, UsuarioModel user)
        {
            return ClientBLL.CloseCommands(client, user);
        }
    }
}