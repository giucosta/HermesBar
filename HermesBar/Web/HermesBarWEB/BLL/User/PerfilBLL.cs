using DAO.User;
using ENTITY.User;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
using HELPER;

namespace BLL.User
{
    public class PerfilBLL
    {
        #region Singleton
        private ProfileDAO PerfilDAO = Singleton<ProfileDAO>.Instance();
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
