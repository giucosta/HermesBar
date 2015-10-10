using DAO.Employee;
using MODEL.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
using ENTITY.Employee;
using HELPER;

namespace BLL.Employee
{
    public class PlaceEmployeeBLL
    {
        #region Singleton
        private PlaceEmployeeDAO PlaceEmployeeDAO = Singleton<PlaceEmployeeDAO>.Instance();
        #endregion

        public List<PlaceEmployeeModel> Get()
        {
            try
            {
                var list = new List<PlaceEmployeeModel>();
                foreach (var item in PlaceEmployeeDAO.Get().DataTableToList<HMA_CAR_FUNC>())
                    list.Add(ConvertEntityToModel(item));

                return list;

            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private PlaceEmployeeModel ConvertEntityToModel(HMA_CAR_FUNC car)
        {
            try
            {
                var model = new PlaceEmployeeModel();
                model.Cargo = car.CAR;
                model.Descricao = car.DESCR;
                model.Id = car._ID;

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
