using BLL.Establishment;
using HELPER;
using MODEL.Establishment;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class Establishment
    {
        private EstablishmentBLL EstablishmentBLL;
        public Establishment()
        {
            this.EstablishmentBLL = Singleton<EstablishmentBLL>.Instance();
        }

        public List<EstablishmentModel> Get(EstablishmentModel establisment, UsuarioModel user)
        {
            return EstablishmentBLL.Get(establisment, user);
        }
        public bool Insert(EstablishmentModel establisment, UsuarioModel user)
        {
            return EstablishmentBLL.Insert(establisment, user);
        }
        public bool Update(EstablishmentModel establisment, UsuarioModel user)
        {
            return EstablishmentBLL.Update(establisment, user);
        }
        public bool Inactive(EstablishmentModel establisment, UsuarioModel user)
        {
            return EstablishmentBLL.Inactive(establisment, user);
        }
        public bool Active(EstablishmentModel establisment, UsuarioModel user)
        {
            return EstablishmentBLL.Active(establisment, user);
        }
    }
}