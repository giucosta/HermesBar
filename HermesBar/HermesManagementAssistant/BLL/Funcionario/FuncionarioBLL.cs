using BLL.Comum;
using DAO.Comum;
using DAO.Funcionario;
using DAO.Utils;
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
    public class FuncionarioBLL
    {
        #region AccessMethod
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
        #endregion

        public bool Salvar(FuncionarioModel funcionario)
        {
            try
            {
                if (VerificaIdadeFuncionario(funcionario))
                {
                    if (Validacoes.ValidaCPF(funcionario.Cpf))
                    {
                        AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
                        AO.GetTransaction();
                        var endereco = EnderecoBLL.Salvar(funcionario.Endereco);
                        if (endereco != null)
                        {
                            funcionario.Endereco = endereco;
                            var contato = ContatoBLL.Salvar(funcionario.Contato);
                            if (contato != null)
                                funcionario.Contato = contato;
                            else
                                AO.Rollback();
                            if (FuncionarioDAO.Salvar(funcionario))
                            {
                                AO.Commit();
                                return true;
                            }
                        }
                        else
                            AO.Rollback();
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return false;
            }
        }
        public bool Excluir(FuncionarioModel funcionario)
        {
            AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
            AO.GetTransaction();
            if (FuncionarioDAO.Excluir(funcionario))
            {
                if (ContatoBLL.Excluir(funcionario.Contato))
                {
                    if (EnderecoBLL.Excluir(funcionario.Endereco))
                    {
                        AO.Commit();
                        return true;
                    }
                    else
                        AO.Rollback();
                }
                else
                    AO.Rollback();
            }
            else
                AO.Rollback();
            return false;   
        }
        public bool Editar(FuncionarioModel funcionario)
        {
            return FuncionarioDAO.Editar(funcionario);
        }
        public List<FuncionarioModel> Pesquisa(FuncionarioModel func)
        {
            try
            {
                return FuncionarioDAO.Pesquisa(func).DataTableToList<FuncionarioModel>();
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
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
            if ((DateTime.Now.Year - funcionario.DataNascimento.Year) < 18)
                return false;
            return true;
        }
        public FuncionarioModel PesquisaFuncionarioCpf(FuncionarioModel func)
        {
            var funcionario = FuncionarioDAO.PesquisaFuncionarioCpf(func.Cpf).DataTableToSimpleObject<FuncionarioModel>();
            funcionario.Endereco = EnderecoBLL.RecuperaEnderecoId(FuncionarioDAO.RetornaIdEndereco(funcionario));
            funcionario.Contato = ContatoBLL.RecuperaContatoId(FuncionarioDAO.RetornaIdContato(funcionario));

            return funcionario;
        }
    }
}
