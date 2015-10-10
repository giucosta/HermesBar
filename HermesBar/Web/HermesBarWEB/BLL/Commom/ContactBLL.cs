using ENTITY.Commom;
using MODEL.Contact;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commom
{
    public class ContactBLL
    {
        /// <summary>
        /// Performs the model´s conversion to entity
        /// </summary>
        /// <param name="ContatoModel"></param>
        /// <param name="UsuarioModel"></param>
        /// <returns>HMA_CON</returns>
        public HMA_CON ConvertModelToEntity(ContatoModel model, UsuarioModel usuario)
        {
            try
            {
                var entity = new HMA_CON();
                entity._ID = model.Id;
                entity._USR = usuario.Id;
                entity.CEL = model.Celular;
                entity.EMA = model.Email;
                entity.NOM = model.Nome;
                entity.TEL = model.Telefone;

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
        /// <param name="HMA_CON"></param>
        /// <returns>ContatoModel</returns>
        public ContatoModel ConvertEntityToModel(HMA_CON con)
        {
            try
            {
                var model = new ContatoModel();
                model.Celular = con.CEL;
                model.Email = con.EMA;
                model.Id = con._ID;
                model.Nome = con.NOM;
                model.Telefone = con.TEL;

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
