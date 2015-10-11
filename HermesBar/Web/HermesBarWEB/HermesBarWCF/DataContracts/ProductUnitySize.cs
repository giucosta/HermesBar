using BLL.Product;
using HELPER;
using MODEL.Product;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class ProductUnitySize
    {
        private UniMedBLL UniMedBLL;
        public ProductUnitySize()
        {
            this.UniMedBLL = Singleton<UniMedBLL>.Instance();
        }

        public List<UnidadeMedidaModel> Get()
        {
            try
            {
                return UniMedBLL.Get();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Insert(UnidadeMedidaModel model, UsuarioModel user)
        {
            try
            {
                return UniMedBLL.Insert(model, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}