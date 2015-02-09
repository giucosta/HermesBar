using DAO.Funcionario;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Funcionario
{
    public class TipoFuncionarioBLL
    {
        private TipoFuncionarioDAO _tipoFuncionarioDAO = null;
        public TipoFuncionarioDAO DAO
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
                return DAO.Salvar(tipoFuncionario);
            else
                return DAO.RetornaTipo(tipoFuncionario);
        }
        public bool VerificaTipoExistente(TipoFuncionarioModel tipoFuncionario)
        {
            if (DAO.RetornaTipo(tipoFuncionario) != null)
                return true;
            else
                return false;
        }
        public List<String> RetornaTipos()
        {
            return DAO.TiposFuncionarios();
        }
        public TipoFuncionarioModel RetornaTipo(TipoFuncionarioModel tipo)
        {
            return DAO.RetornaTipo(tipo);
        }
    }
}
