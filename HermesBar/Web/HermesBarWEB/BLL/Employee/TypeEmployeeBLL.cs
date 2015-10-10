using DAO.Employee;
using ENTITY.Employee;
using MODEL.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
using HELPER;

namespace BLL.Employee
{
    public class TypeEmployeeBLL
    {
        #region Singleton
        private TypeEmployeeDAO TypeEmployeeDAO = Singleton<TypeEmployeeDAO>.Instance();
        #endregion

        public List<TypeEmployeeModel> Get()
        {
            try
            {
                var list = new List<TypeEmployeeModel>();
                foreach (var item in TypeEmployeeDAO.Get().DataTableToList<HMA_TIP_FUNC>())
                    list.Add(ConvertEntityToModel(item));

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private TypeEmployeeModel ConvertEntityToModel(HMA_TIP_FUNC tip)
        {
            try
            {
                var model = new TypeEmployeeModel();
                model.Id = tip._ID;
                model.Tipo = tip.TIP;
                model.Descricao = tip.DESCR;

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
