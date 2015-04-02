using DAO.Funcionario;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

namespace BLL.Funcionario
{
    public class TipoFuncionarioBLL
    {
        private TipoFuncionarioDAO _tipoFuncionarioDAO = null;
        public TipoFuncionarioDAO TipoFuncionarioDAO
        {
            get
            {
                if (_tipoFuncionarioDAO == null)
                    _tipoFuncionarioDAO = new TipoFuncionarioDAO();
                return _tipoFuncionarioDAO;
            }
        }
        public TipoFuncionarioModel Salvar(TipoFuncionarioModel tipoFuncionario)
        {
            if (VerificaTipoExistente(tipoFuncionario))
                return TipoFuncionarioDAO.Salvar(tipoFuncionario);
            else
                return TipoFuncionarioDAO.RetornaTipo(tipoFuncionario);
        }
        public bool VerificaTipoExistente(TipoFuncionarioModel tipoFuncionario)
        {
            if (TipoFuncionarioDAO.RetornaTipo(tipoFuncionario) != null)
                return true;
            else
                return false;
        }
        public List<TipoFuncionarioModel> RetornaTipos()
        {
            return TipoFuncionarioDAO.TiposFuncionarios().DataTableToList<TipoFuncionarioModel>();
        }
        public TipoFuncionarioModel RetornaTipo(TipoFuncionarioModel tipo)
        {
            return TipoFuncionarioDAO.RetornaTipo(tipo);
        }
    }
}
