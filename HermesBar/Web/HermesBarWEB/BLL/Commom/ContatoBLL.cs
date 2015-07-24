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
    public class ContatoBLL
    {
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
    }
}
