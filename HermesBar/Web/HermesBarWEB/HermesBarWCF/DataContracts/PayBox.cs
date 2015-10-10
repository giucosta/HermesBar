using BLL.PDV;
using HELPER;
using MODEL.PDV.PayBox;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class PayBox
    {
        private PdvPayBoxBLL PayBoxBLL;
        public PayBox()
        {
            this.PayBoxBLL = Singleton<PdvPayBoxBLL>.Instance();
        }

        public bool Open(PayBoxModel payBox, UsuarioModel user)
        {
            return PayBoxBLL.Open(payBox, user);
        }
        public PayBoxModel VerifyPayBox()
        {
            return PayBoxBLL.VerifyPayBox();
        }
        public bool Close(PayBoxModel payBox, UsuarioModel user)
        {
            return PayBoxBLL.Close(payBox, user);
        }
        public bool Reinforcement(PayBoxModel payBox, UsuarioModel user)
        {
            return PayBoxBLL.Reinforcement(payBox, user);
        }
        public bool Depletion(PayBoxModel payBox, UsuarioModel user)
        {
            return PayBoxBLL.Depletion(payBox, user);
        }
    }
}