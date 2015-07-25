using ENTITY.Commom;
using MODEL.Address;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commom
{
    public class EnderecoBLL
    {
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
                entity.UF = model.Uf;

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
                model.Uf = end.UF;

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
