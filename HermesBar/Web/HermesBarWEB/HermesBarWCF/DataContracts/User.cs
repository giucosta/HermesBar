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
    public class User
    {
        private UserBLL _userBLL = null;
        private UserBLL UserBLL
        {
            get
            {
                if (_userBLL == null)
                    _userBLL = new UserBLL();
                return _userBLL;
            }
        }

        public List<UsuarioModel> Get(UsuarioModel user)
        {
            return UserBLL.Get(user);
        }
    }
}