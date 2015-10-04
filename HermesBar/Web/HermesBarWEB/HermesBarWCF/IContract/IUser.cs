﻿using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace HermesBarWCF.IContract
{
    [ServiceContract]
    public interface IUser
    {
        List<UsuarioModel> Get(UsuarioModel user);
    }
}