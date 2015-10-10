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
    public class ProductType
    {
        private TypeBLL TypeBLL = Singleton<TypeBLL>.Instance();

        public List<TipoModel> Get()
        {
            try
            {
                return TypeBLL.Get();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TipoModel GetId(TipoModel tipo, UsuarioModel user)
        {
            try
            {
                return TypeBLL.GetId(tipo, user);   
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Insert(TipoModel tipo, UsuarioModel user)
        {
            try
            {
                return TypeBLL.Insert(tipo, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(TipoModel tipo, UsuarioModel user)
        {
            try
            {
                return TypeBLL.Update(tipo, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}