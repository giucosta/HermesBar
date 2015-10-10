using BLL.Commom;
using MODEL.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class Address
    {
        private AddressBLL _enderecoBLL = null;
        private AddressBLL EnderecoBLL
        {
            get
            {
                if (_enderecoBLL == null)
                    _enderecoBLL = new AddressBLL();
                return _enderecoBLL;
            }
        }
        public EnderecoModel GetStates(EnderecoModel endereco)
        {
            return EnderecoBLL.GetStates(endereco);
        }
    }
}