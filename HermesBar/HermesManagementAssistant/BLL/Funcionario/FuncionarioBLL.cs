using DAO.Funcionario;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BLL.Funcionario
{
    public class FuncionarioBLL
    {
        private FuncionarioDAO _funcionarioDAO = null;
        public FuncionarioDAO FuncionarioDAO
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
            if (VerificaIdadeFuncionario(funcionario))
            {
                if(Validacoes.ValidaCPF(funcionario.Cpf))
                    return FuncionarioDAO.Salvar(funcionario);
            }
            return false;
        }

        public bool Excluir(FuncionarioModel funcionario)
        {
            return FuncionarioDAO.Excluir(funcionario);
        }
        public List<FuncionarioModel> Pesquisa(FuncionarioModel funcionario)
        {
            return FuncionarioDAO.Pesquisa(funcionario).DataTableToList<FuncionarioModel>();
        }
        private bool VerificaIdadeFuncionario(FuncionarioModel funcionario)
        {
            if ((funcionario.DataNascimento.Year - DateTime.Now.Year) < 18)
                return false;
            return true;
        }
    }
}
