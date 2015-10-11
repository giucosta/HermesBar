using MODEL.Commom;
using System;
using System.ServiceModel;

namespace HermesBarWCF.IContract
{
    [ServiceContract]
    public interface IEmail
    {
        LayoutModel Get();
    }
}