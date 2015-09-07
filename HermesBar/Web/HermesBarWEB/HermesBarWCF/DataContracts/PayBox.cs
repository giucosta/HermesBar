using BLL.PDV;
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
        private PdvPayBoxBLL _payBoxBLL = null;
        private PdvPayBoxBLL PayBoxBLL
        {
            get
            {
                if (_payBoxBLL == null)
                    _payBoxBLL = new PdvPayBoxBLL();
                return _payBoxBLL;
            }
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
    }
}