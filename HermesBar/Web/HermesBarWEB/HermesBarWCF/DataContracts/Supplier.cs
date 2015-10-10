using BLL.Supplier;
using HELPER;
using MODEL.Supplier;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class Supplier
    {
        private SupplierBLL SuplierBLL;
        public Supplier()
        {
            this.SuplierBLL = Singleton<SupplierBLL>.Instance();
        }
        public bool Insert(FornecedorModel supplier, UsuarioModel user)
        {
            try
            {
                return SuplierBLL.Insert(supplier, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<FornecedorModel> Get(FornecedorModel model, UsuarioModel user)
        {
            try
            {
                return SuplierBLL.Get(model, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(FornecedorModel supplier, UsuarioModel user)
        {
            try
            {
                return SuplierBLL.Update(supplier, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}