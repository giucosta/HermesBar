using BLL.Commom;
using DAO.Employee;
using ENTITY.Commom;
using ENTITY.Employee;
using MODEL.Employee;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;

namespace BLL.Employee
{
    public class EmployeeBLL
    {
        #region Singleton
        private EmployeeDAO _employeeDAO = null;
        private EmployeeDAO EmployeeDAO
        {
            get
            {
                if (_employeeDAO == null)
                    _employeeDAO = new EmployeeDAO();
                return _employeeDAO;
            }
        }
        private ContatoBLL _contatoBLL = null;
        private ContatoBLL ContatoBLL
        {
            get
            {
                if (_contatoBLL == null)
                    _contatoBLL = new ContatoBLL();
                return _contatoBLL;
            }
        }
        private EnderecoBLL _enderecoBLL = null;
        private EnderecoBLL EnderecoBLL
        {
            get
            {
                if (_enderecoBLL == null)
                    _enderecoBLL = new EnderecoBLL();
                return _enderecoBLL;
            }
        }
        #endregion

        public List<EmployeeModel> Get(EmployeeModel model, UsuarioModel user)
        {
            try
            {
                var result = EmployeeDAO.Get(ConvertModelToEntity(model, user));
                var employee = result.Tables[0].DataTableToList<HMA_FUNC>();
                var contact = result.Tables[1].DataTableToList<HMA_CON>();
                var address = result.Tables[2].DataTableToList<HMA_END>();

                var list = new List<EmployeeModel>();
                for (int i = 0; i < employee.Count; i++)
                    list.Add(ConvertEntityToModel(employee[i], contact[i], address[i]));

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private EmployeeModel ConvertEntityToModel(HMA_FUNC func, HMA_CON con, HMA_END end)
        {
            try
            {
                var model = new EmployeeModel();
                model.CargoSelected = func._ID_CAR.ToString();
                model.Contato = ContatoBLL.ConvertEntityToModel(con);
                model.Endereco = EnderecoBLL.ConvertEntityToModel(end);
                model.Funcao = func.FUN;
                model.Id = func._ID;
                model.Rg = func.RG;
                model.Cpf = func.CPF;
                model.Ctps = func.CTPS;
                model.DataAdmissao = func.DT_ADM;
                model.DataDemissao = func.DT_DEM;
                model.SexoSelected = func.SEX;
                model.TipoSelected = func.TIP.ToString();

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private HMA_FUNC ConvertModelToEntity(EmployeeModel model, UsuarioModel user)
        {
            try
            {
                var entity = new HMA_FUNC();
                entity._ATV = Convert.ToInt32(model.StatusSelected);
                entity._ID = model.Id;
                entity._ID_CAR = Convert.ToInt32(model.CargoSelected);
                entity._USR = user.Id;
                entity.CPF = model.Cpf;
                entity.CTPS = model.Ctps;
                entity.DT_ADM = model.DataAdmissao;
                entity.DT_DEM = model.DataDemissao;
                entity.FUN = model.Funcao;
                entity.RG = model.Rg;
                entity.SEX = model.SexoSelected;
                entity.TIP = Convert.ToInt32(model.TipoSelected);

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
