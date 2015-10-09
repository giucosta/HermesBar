﻿using BLL.User;
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