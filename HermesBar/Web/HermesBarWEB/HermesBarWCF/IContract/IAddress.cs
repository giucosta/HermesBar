using MODEL.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace HermesBarWCF.IContract
{
    [ServiceContract]
    public interface IAddress
    {
        EnderecoModel GetStates(EnderecoModel endereco);
    }
}