using MODEL.PDV.PayBox;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace HermesBarWCF.IContract
{
    [ServiceContract]
    public interface IPayBox
    {
        bool Open(PayBoxModel payBox, UsuarioModel user);
        PayBoxModel VerifyPayBox();
        bool Close(PayBoxModel payBox, UsuarioModel user);
        bool Reinforcement(PayBoxModel payBox, UsuarioModel user);
    }
}