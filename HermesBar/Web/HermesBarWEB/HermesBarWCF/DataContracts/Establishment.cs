using BLL.Establishment;
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
        private EstablishmentBLL _estabelecimentoBLL = null;
        private EstablishmentBLL EstabelecimentoBLL
        {
            get
            {
                if (_estabelecimentoBLL == null)
                    _estabelecimentoBLL = new EstablishmentBLL();
                return _estabelecimentoBLL;
            }
        }

        public List<EstablishmentModel> Get(EstablishmentModel estabelecimento, UsuarioModel usuario)
        {
            return EstabelecimentoBLL.Get(estabelecimento, usuario);
        }
        public bool Insert(EstablishmentModel estabelecimento, UsuarioModel usuario)
        {
            return EstabelecimentoBLL.Insert(estabelecimento, usuario);
        }
        public bool Update(EstablishmentModel estabelecimento, UsuarioModel usuario)
        {
            return EstabelecimentoBLL.Update(estabelecimento, usuario);
        }
    }
}