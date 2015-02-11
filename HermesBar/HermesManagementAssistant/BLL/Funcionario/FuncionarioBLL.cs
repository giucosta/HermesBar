using DAO.Funcionario;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

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
            return ConvertDataTableToList(FuncionarioDAO.Pesquisa(funcionario));
        }
        private bool VerificaIdadeFuncionario(FuncionarioModel funcionario)
        {
            if ((funcionario.DataNascimento.Year - DateTime.Now.Year) < 18)
                return false;
            return true;
        }
        private List<FuncionarioModel> ConvertDataTableToList(DataTable func)
        {
            var lista = new List<FuncionarioModel>();
            for (int i = 0; i < func.Rows.Count; i++)
            {
                lista.Add(new FuncionarioModel()
                {
                    Id = (int)func.Rows[i]["Id_funcionario"],
                    Nome = func.Rows[i]["Nome"].ToString(),
                    Cpf = func.Rows[i]["Cpf"].ToString(),
                    Rg = func.Rows[i]["Rg"].ToString(),
                    DataAdmissao = DateTime.Parse(String.Format("{0:dd/MM/yyyy}", func.Rows[i]["DataAdmissao"]))
                });
            }
            return lista;
        }

    }
}
