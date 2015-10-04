using MODEL.PDV.Client;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace HermesBarWCF.IContract
{
    [ServiceContract]
    public interface IPdvClient
    {
        int Insert(PdvClientModel client, UsuarioModel user);
        string GetCar(PdvClientModel client, UsuarioModel user);
        bool Pedido(string cartaoCliente, string codigoAtendente, string nomeProduto, string quantidade, UsuarioModel user, int idCaixa);
        List<PdvFechamentoClientModel> Fechamento(PdvClientModel client);
        bool FecharComanda(PdvClientModel client, UsuarioModel user);
    }
}