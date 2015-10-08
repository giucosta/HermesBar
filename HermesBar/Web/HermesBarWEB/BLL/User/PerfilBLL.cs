using DAO.User;
using ENTITY.User;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;

namespace BLL.User
{
    public class PerfilBLL
    {
        #region Singleton
        private ProfileDAO _perfilDAO = null;
        private ProfileDAO PerfilDAO
        {
            get
            {
                if (_perfilDAO == null)
                    _perfilDAO = new ProfileDAO();
                return _perfilDAO;
            }
        }
        #endregion

        public List<PerfilModel> Get()
        {
            try
            {
                var list = new List<PerfilModel>();
                var result = PerfilDAO.Get().DataTableToList<HMA_PER>();
                foreach (var item in result)
                    list.Add(ConvertEntityToModel(item));

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private PerfilModel ConvertEntityToModel(HMA_PER per)
        {
            var model = new PerfilModel();
            model.Id = per._ID;
            model.StatusSelected = per._ATV.ToString();
            model.Sigla = per.SIG;
            model.Descricao = per.DSC;

            return model;
        }
        #endregion
    }
}
