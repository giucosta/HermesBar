using BLL.Commom;
using DAO.Supplier;
using ENTITY.Commom;
using ENTITY.Supplier;
using MODEL.Supplier;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
using HELPER;

namespace BLL.Supplier
{
    public class SupplierBLL
    {
        #region Singleton
        private SupplierDAO SupplierDAO = Singleton<SupplierDAO>.Instance();
        private AddressBLL AddressBLL = Singleton<AddressBLL>.Instance();
        private ContactBLL ContactBLL = Singleton<ContactBLL>.Instance();
        #endregion

        public bool Insert(FornecedorModel supplier, UsuarioModel user)
        {
            try
            {
                ProcessDataForInsert(ref supplier);
                supplier.StatusSelected = "1";
                
                HMA_FOR supplierEntity;
                HMA_END addressEntity;
                HMA_CON contactEntity;
                LoadModels(supplier, user, out supplierEntity, out addressEntity, out contactEntity);

                return SupplierDAO.Insert(supplierEntity, addressEntity, contactEntity).GetResults();
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
                var result = SupplierDAO.Get(ConvertModelToEntity(model, user));
                var supplier = result.Tables[0].DataTableToList<HMA_FOR>();
                var address = result.Tables[1].DataTableToList<HMA_END>();
                var contact = result.Tables[2].DataTableToList<HMA_CON>();

                var list = new List<FornecedorModel>();

                for (int i = 0; i < supplier.Count; i++)
                    list.Add(ConvertEntityToModel(supplier[i], address[i], contact[i]));
                
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(FornecedorModel supplier, UsuarioModel user)
        {
            ProcessDataForInsert(ref supplier);

            HMA_FOR supplierEntity;
            HMA_END addressEntity;
            HMA_CON contactEntity;
            LoadModels(supplier, user, out supplierEntity, out addressEntity, out contactEntity);

            return SupplierDAO.Update(supplierEntity, addressEntity, contactEntity).GetResults();
        }

        #region Private Methods
        private void LoadModels(FornecedorModel supplier, UsuarioModel user, out HMA_FOR supplierEntity, out HMA_END addressEntity, out HMA_CON contactEntity)
        {
            supplierEntity = ConvertModelToEntity(supplier, user);
            addressEntity = AddressBLL.ConvertModelToEntity(supplier.Endereco, user);
            contactEntity = ContactBLL.ConvertModelToEntity(supplier.Contato, user);
        }
        private void ProcessDataForInsert(ref FornecedorModel forn)
        {
            try
            {
                forn.Cnpj = forn.Cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                forn.Contato.Telefone = forn.Contato.Telefone.Replace("(", "").Replace(")", "").Replace("-", "");
                forn.Contato.Celular = forn.Contato.Telefone.Replace("(", "").Replace(")", "").Replace("-", "");
                forn.Endereco.Cep = forn.Endereco.Cep.Replace("-", "");

                if (string.IsNullOrWhiteSpace(forn.InscricaoEstadual))
                    forn.InscricaoEstadual = "ISENTO";
                if (string.IsNullOrWhiteSpace(forn.InscricaoMunicipal))
                    forn.InscricaoMunicipal = "ISENTO";
            }
            catch (Exception)
            {
                throw;
            }
        }
        private HMA_FOR ConvertModelToEntity(FornecedorModel model, UsuarioModel usuario)
        {
            try
            {
                var entity = new HMA_FOR();
                entity._ATV = Convert.ToInt32(model.StatusSelected);
                entity._ID = model.Id;
                entity._USR = usuario.Id;
                entity.CNPJ = model.Cnpj;
                entity.INSEST = model.InscricaoEstadual;
                entity.INSMUN = model.InscricaoMunicipal;
                entity.FAN = model.NomeFantasia;
                entity.RAZ = model.RazaoSocial;
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private FornecedorModel ConvertEntityToModel(HMA_FOR forn, HMA_END end, HMA_CON con)
        {
            try
            {
                var model = new FornecedorModel();
                model.Cnpj = forn.CNPJ;
                model.Id = forn._ID;
                model.InscricaoEstadual = forn.INSEST;
                model.InscricaoMunicipal = forn.INSMUN;
                model.NomeFantasia = forn.FAN;
                model.RazaoSocial = forn.RAZ;
                model.StatusSelected = forn._ATV.ToString();

                model.Endereco = AddressBLL.ConvertEntityToModel(end);
                model.Contato = ContactBLL.ConvertEntityToModel(con);

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
