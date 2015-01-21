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
            if(VerificaIdadeFuncionario(funcionario))
                return DAO.Salvar(funcionario);
            return false;
        }

        public bool Excluir(FuncionarioModel funcionario)
        {
            return DAO.Excluir(funcionario);
        }

        public DataTable Pesquisa(FuncionarioModel funcionario)
        {
            return DAO.Pesquisa(funcionario);
        }
        private bool VerificaIdadeFuncionario(FuncionarioModel funcionario)
        {
            if ((funcionario.DataNascimento.Year - DateTime.Now.Year) < 18)
                return false;
            return true;
        }

    }
}
