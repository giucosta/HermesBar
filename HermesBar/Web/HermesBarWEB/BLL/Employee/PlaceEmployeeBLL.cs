using DAO.Employee;
using MODEL.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
using ENTITY.Employee;

namespace BLL.Employee
{
    public class PlaceEmployeeBLL
    {
        #region Singleton
        private PlaceEmployeeDAO _placeDAO = null;
        private PlaceEmployeeDAO PlaceEmployeeDAO
        {
            get
            {
                if (_placeDAO == null)
                    _placeDAO = new PlaceEmployeeDAO();
                return _placeDAO;
            }
        }
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
