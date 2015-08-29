using MODEL.Client;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace HermesBarWCF.IContract
{
    [ServiceContract]
    public interface IClient
    {
        List<ClientModel> Get(ClientModel client, UsuarioModel user);
        bool Insert(ClientModel client, UsuarioModel user, bool cadastroRapido);
        bool Inactive(ClientModel client, UsuarioModel user);
        bool Active(ClientModel client, UsuarioModel user);
        bool Update(ClientModel client, UsuarioModel user);
    }
}