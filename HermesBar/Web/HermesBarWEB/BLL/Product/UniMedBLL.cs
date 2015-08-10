using DAO.Product;
using ENTITY.Product;
using MODEL.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
using MODEL.User;

namespace BLL.Product
{
    public class UniMedBLL
    {
        private UniMedDAO _unimedDAO = null;
        private UniMedDAO UniMedDAO
        {
            get
            {
                if (_unimedDAO == null)
                    _unimedDAO = new UniMedDAO();
                return _unimedDAO;
            }
        }

        public List<UnidadeMedidaModel> Get()
        {
            try
            {
                var result = UniMedDAO.Get().DataTableToList<HMA_UNI_MED>();
                var list = new List<UnidadeMedidaModel>();
                foreach (var item in result)
                    list.Add(ConvertEntityToModel(item));

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Insert(UnidadeMedidaModel model, UsuarioModel user)
        {
            try
            {
                return Convert.ToInt32(UniMedDAO.Insert(ConvertModelToEntity(model, user)).Rows[0]["SUCCESS"]) == 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private UnidadeMedidaModel ConvertEntityToModel(HMA_UNI_MED entity)
        {
            var model = new UnidadeMedidaModel();
            model.Nome = entity.NOM;
            model.Descricao = entity.DSC;
            model.Id = entity._ID;
            model.StatusSelected = entity._ATV.ToString();

            return model;
        }
        private HMA_UNI_MED ConvertModelToEntity(UnidadeMedidaModel model, UsuarioModel user)
        {
            var entity = new HMA_UNI_MED();
            entity._ATV = Convert.ToInt32(model.StatusSelected);
            entity._ID = model.Id;
            entity._USR = user.Id;
            entity.DSC = model.Descricao;
            entity.NOM = model.Nome;

            return entity;
        }
    }
}
