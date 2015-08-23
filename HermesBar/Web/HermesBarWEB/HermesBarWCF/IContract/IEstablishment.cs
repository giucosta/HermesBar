using MODEL.Establishment;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace HermesBarWCF.IContract
{
    [ServiceContract]
    public interface IEstablishment
    {
        List<EstablishmentModel> Get(EstablishmentModel estabelecimento, UsuarioModel usuario);
        bool Insert(EstablishmentModel estabelecimento, UsuarioModel usuario);
        bool Update(EstablishmentModel estabelecimento, UsuarioModel usuario);
        bool Inactive(EstablishmentModel estabelecimento, UsuarioModel usuario);
        bool Active(EstablishmentModel estabelecimento, UsuarioModel usuario);
    }
}