using BLL.User;
using HELPER;
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
        private ProfileBLL ProfileBLL;
        public Profile()
        {
            this.ProfileBLL = Singleton<ProfileBLL>.Instance();
        }

        public List<PerfilModel> Get()
        {
            return ProfileBLL.Get();
        }
    }
}