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
    }
}
