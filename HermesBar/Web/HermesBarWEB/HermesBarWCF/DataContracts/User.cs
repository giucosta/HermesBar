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
    public class User
    {
        private UserBLL UserBLL;

        public User()
        {
            this.UserBLL = Singleton<UserBLL>.Instance();
        }
        public List<UsuarioModel> Get(UsuarioModel user)
        {
            return UserBLL.Get(user);
        }
        public bool Insert(UsuarioModel user)
        {
            return UserBLL.Insert(user);
        }
        public bool Update(UsuarioModel user)
        {
            return UserBLL.Update(user);
        }
        public bool Active(UsuarioModel user)
        {
            return UserBLL.Active(user);
        }
        public bool Inactive(UsuarioModel user)
        {
            return UserBLL.Inactive(user);
        }
    }
}