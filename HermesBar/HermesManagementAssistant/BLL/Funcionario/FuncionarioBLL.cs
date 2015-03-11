using BLL.Comum;
using DAO.Comum;
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
        private EnderecoDAO _enderecoDAO = null;
        public EnderecoDAO EnderecoDAO
        {
            get
            {
                if (_enderecoDAO == null)
                    _enderecoDAO = new EnderecoDAO();
                return _enderecoDAO;
            }
        }
        private EnderecoBLL _enderecoBLL = null;
        public EnderecoBLL EnderecoBLL
        {
            get
            {
                if (_enderecoBLL == null)
                    _enderecoBLL = new EnderecoBLL();
                return _enderecoBLL;
            }
        }
        private ContatoBLL _contatoBLL = null;
        public ContatoBLL ContatoBLL
        {
            get
            {
                if (_contatoBLL == null)
                    _contatoBLL = new ContatoBLL();
                return _contatoBLL;
                       
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
        public List<FuncionarioModel> Pesquisa()
        {
            return FuncionarioDAO.Pesquisa().DataTableToList<FuncionarioModel>();
        }
        public FuncionarioModel PesquisaFuncionarioId(FuncionarioModel func)
        {
            var funcionario = FuncionarioDAO.PesquisaPorId(func.Id).DataTableToSimpleObject<FuncionarioModel>();
            funcionario.Endereco = EnderecoBLL.RecuperaEnderecoId(FuncionarioDAO.RetornaIdEndereco(funcionario));
            funcionario.Contato = ContatoBLL.RecuperaContatoId(FuncionarioDAO.RetornaIdContato(funcionario));
            
            return funcionario;
        }
        private bool VerificaIdadeFuncionario(FuncionarioModel funcionario)
        {
            if ((funcionario.DataNascimento.Year - DateTime.Now.Year) < 18)
                return false;
            return true;
        }
    }
}
