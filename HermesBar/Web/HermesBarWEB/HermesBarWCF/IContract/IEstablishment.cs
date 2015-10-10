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
        List<EstablishmentModel> Get(EstablishmentModel establisment, UsuarioModel user);
        bool Insert(EstablishmentModel establisment, UsuarioModel user);
        bool Update(EstablishmentModel establisment, UsuarioModel user);
        bool Inactive(EstablishmentModel establisment, UsuarioModel user);
        bool Active(EstablishmentModel establisment, UsuarioModel user);
    }
}