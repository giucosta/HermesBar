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
    public class FuncionarioBLL
    {
        private FuncionarioDAO _funcionarioDAO = null;
        public FuncionarioDAO DAO
        {
            get
            {
                if (_funcionarioDAO == null)
                    _funcionarioDAO = new FuncionarioDAO();
                return _funcionarioDAO;
            }
        }

        public bool Salvar(FuncionarioModel funcionario)
        {
            return DAO.Salvar(funcionario);
        }

        public bool Excluir(FuncionarioModel funcionario)
        {
            return DAO.Excluir(funcionario);
        }

        public DataTable Pesquisa(FuncionarioModel funcionario)
        {
            return DAO.Pesquisa(funcionario);
        }
    }
}
