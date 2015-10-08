using BLL.User;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class Profile
    {
        private PerfilBLL _profileBLL = null;
        private PerfilBLL ProfileBLL
        {
            get
            {
                if (_profileBLL == null)
                    _profileBLL = new PerfilBLL();
                return _profileBLL;
            }
        }

        public List<PerfilModel> Get()
        {
            return ProfileBLL.Get();
        }
    }
}