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

namespace BLL.Supplier
{
    public class FornecedorBLL
    {
        #region Singleton
        private FornecedorDAO _fornecedorDAO = null;
        private FornecedorDAO FornecedorDAO
        {
            get
            {
                if (_fornecedorDAO == null)
                    _fornecedorDAO = new FornecedorDAO();
                return _fornecedorDAO;
            }
        }
        private EnderecoBLL _enderecoBLL = null;
        private EnderecoBLL EnderecoBLL
        {
            get
            {
                if (_enderecoBLL == null)
                    _enderecoBLL = new EnderecoBLL();
                return _enderecoBLL;
            }
        }
        private ContatoBLL _contatoBLL = null;
        private ContatoBLL ContatoBLL
        {
            get
            {
                if (_contatoBLL == null)
                    _contatoBLL = new ContatoBLL();
                return _contatoBLL;
            }
        }
        #endregion

        public bool Insert(FornecedorModel fornecedor, UsuarioModel usuario)
        {
            try
            {
                var fornecedorEntity = ConvertModelToEntity(fornecedor, usuario);
                if (string.IsNullOrWhiteSpace(fornecedorEntity.INSMUN))
                    fornecedorEntity.INSMUN = "ISENTO";
                if (string.IsNullOrWhiteSpace(fornecedorEntity.INSEST))
                    fornecedorEntity.INSEST = "ISENTO";
                var enderecoEntity = EnderecoBLL.ConvertModelToEntity(fornecedor.Endereco, usuario);
                var contatoEntity = ContatoBLL.ConvertModelToEntity(fornecedor.Contato, usuario);
                return Convert.ToInt32(FornecedorDAO.Insert(fornecedorEntity, enderecoEntity, contatoEntity).Rows[0]["SUCCESS"]) != 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar fornecedor " +ex.Message);
            }
        }
        public List<FornecedorModel> Get(FornecedorModel model, UsuarioModel usuario)
        {
            try
            {
                var result = FornecedorDAO.Get(ConvertModelToEntity(model, usuario));
                var supplier = result.Tables[0].DataTableToList<HMA_FOR>();
                var address = result.Tables[1].DataTableToList<HMA_END>();
                var contact = result.Tables[2].DataTableToList<HMA_CON>();

                var list = new List<FornecedorModel>();
                foreach (var item in supplier)
                {
                    foreach (var a in address)
                    {
                        foreach (var c in contact)
                        {
                            list.Add(ConvertEntityToModel(item, a, c));
                        }
                    }
                }
                
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(FornecedorModel fornecedor, UsuarioModel usuario)
        {
            var supplier = ConvertModelToEntity(fornecedor, usuario);
            var address = EnderecoBLL.ConvertModelToEntity(fornecedor.Endereco, usuario);
            var contact = ContatoBLL.ConvertModelToEntity(fornecedor.Contato, usuario);
            return Convert.ToInt32(FornecedorDAO.Update(supplier, address, contact).Rows[0]["SUCCESS"]) != 0;
        }

        #region Private Methods
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
            catch (Exception ex)
            {
                throw ex;
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

                model.Endereco = EnderecoBLL.ConvertEntityToModel(end);
                model.Contato = ContatoBLL.ConvertEntityToModel(con);

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
