using BLL.User;
using BLL.UTIL;
using HELPER;
using MODEL.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class Email
    {
        public LayoutModel Get()
        {
            return GetEmail.Get();
        }
    }
}