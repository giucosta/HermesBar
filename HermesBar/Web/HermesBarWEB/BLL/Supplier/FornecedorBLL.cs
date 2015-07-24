using BLL.Commom;
using DAO.Supplier;
using ENTITY.Supplier;
using MODEL.Supplier;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Supplier
{
    public class FornecedorBLL
    {
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
        public bool Insert(FornecedorModel fornecedor, UsuarioModel usuario)
        {
            try
            {
                var fornecedorEntity = ConvertModelToEntity(fornecedor, usuario);
                var enderecoEntity = EnderecoBLL.ConvertModelToEntity(fornecedor.Endereco, usuario);
                var contatoEntity = ContatoBLL.ConvertModelToEntity(fornecedor.Contato, usuario);
                var result =  Convert.ToInt32(FornecedorDAO.Insert(fornecedorEntity, enderecoEntity, contatoEntity).Rows[0]["SUCCESS"]);

                return result != 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar fornecedor " +ex.Message);
            }
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
        #endregion
    }
}
