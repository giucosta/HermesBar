using BLL.Commom;
using HELPER;
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
        private AddressBLL AddressBLL;
        public Address()
        {
            this.AddressBLL = Singleton<AddressBLL>.Instance();
        }
       
        public EnderecoModel GetStates(EnderecoModel address)
        {
            return AddressBLL.GetStates(address);
        }
    }
}