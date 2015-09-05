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
        public bool Insert(EmployeeModel employee, UsuarioModel user)
        {
            try
            {
                ProcessDataForInsert(ref employee);

                var employeeEntity = ConvertModelToEntity(employee, user);
                var addressEntity = EnderecoBLL.ConvertModelToEntity(employee.Endereco, user);
                var contatcEntity = ContatoBLL.ConvertModelToEntity(employee.Contato, user);

                return Convert.ToInt32(EmployeeDAO.Insert(employeeEntity, contatcEntity, addressEntity).Rows[0]["SUCCESS"]) != 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void ProcessDataForInsert(ref EmployeeModel emp)
        {
            try
            {
                emp.Rg = emp.Rg.Replace(".", "").Replace("-", "");
                emp.Cpf = emp.Cpf.Replace(".", "").Replace("-", "");
                emp.Contato.Telefone = emp.Contato.Telefone.Replace("(", "").Replace(")", "").Replace("-", "");
                emp.Contato.Celular = emp.Contato.Telefone.Replace("(", "").Replace(")", "").Replace("-", "");
                emp.Endereco.Cep = emp.Endereco.Cep.Replace("-", "");
            }
            catch (Exception)
            {
                throw;
            }
        }
        #region Private Methods
        private EmployeeModel ConvertEntityToModel(HMA_FUNC emp, HMA_CON con, HMA_END end)
        {
            try
            {
                var model = new EmployeeModel();
                model.CargoSelected = emp._ID_CAR.ToString();
                model.Contato = ContatoBLL.ConvertEntityToModel(con);
                model.Endereco = EnderecoBLL.ConvertEntityToModel(end);
                model.Funcao = emp.FUN;
                model.Id = emp._ID;
                model.Rg = emp.RG;
                model.Cpf = emp.CPF;
                model.Ctps = emp.CTPS;
                model.DataAdmissao = emp.DT_ADM;
                model.DataDemissao = emp.DT_DEM;
                model.SexoSelected = emp.SEX;
                model.TipoSelected = emp.TIP.ToString();

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
