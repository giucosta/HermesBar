using DAO.Commom;
using ENTITY.Commom;
using MODEL.Address;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL.Commom
{
    public class EnderecoBLL
    {
        private EnderecoDAO _enderecoDAO = null;
        private EnderecoDAO EnderecoDAO
        {
            get
            {
                if (_enderecoDAO == null)
                    _enderecoDAO = new EnderecoDAO();
                return _enderecoDAO;
            }
        }
        /// <summary>
        /// Performs the model´s conversion to entity
        /// </summary>
        /// <param name="EnderecoModel"></param>
        /// <param name="UsuarioModel"></param>
        /// <returns>HMA_END</returns>
        public HMA_END ConvertModelToEntity(EnderecoModel model, UsuarioModel usuario)
        {
            try
            {
                var entity = new HMA_END();
                entity._ID = model.Id;
                entity._USR = usuario.Id;
                entity.BAI = model.Bairro;
                entity.CEP = model.Cep;
                entity.CID = model.Cidade;
                entity.COMP = model.Complemento;
                entity.NUM = model.Numero;
                entity.RUA = model.Rua;
                entity.UF = Convert.ToInt32(model.UfSelected);

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Performs the entity's conversion to model
        /// </summary>
        /// <param name="HMA_END"></param>
        /// <returns>EnderecoModel</returns>
        public EnderecoModel ConvertEntityToModel(HMA_END end)
        {
            try
            {
                var model = new EnderecoModel();
                model.Bairro = end.BAI;
                model.Cep = end.CEP;
                model.Cidade = end.CID;
                model.Complemento = end.COMP;
                model.Id = end._ID;
                model.Numero = end.NUM;
                model.Rua = end.RUA;
                model.UfSelected = end.UF.ToString();

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EnderecoModel GetStates(EnderecoModel model)
        {
            try
            {
                var result = EnderecoDAO.GetStates();

                model.UfList = new List<SelectListItem>();
                for (int i = 0; i < result.Rows.Count; i++)
                    model.UfList.Add(new SelectListItem() { Text = result.Rows[i]["SIG"].ToString(), Value = result.Rows[i]["_ID"].ToString() });

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
