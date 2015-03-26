using BLL.Comum;
using DAO.Connections;
using DAO.Fornecedor;
using MODEL;
using MODEL.Fornecedor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;


namespace BLL.Fornecedor
{
    public class FornecedorBLL
    {
        private FornecedorDAO _fornecedorDAO = null;
        public FornecedorDAO FornecedorDAO
        {
            get
            {
                if (_fornecedorDAO == null)
                    _fornecedorDAO = new FornecedorDAO();
                return _fornecedorDAO;
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
        public bool Salvar(FornecedorModel fornecedor)
        {
            if (Validacoes.ValidaCNPJ(fornecedor.Cpj))
            {
                Connection.GetTransaction();
                fornecedor.Endereco = SaveAddress(fornecedor.Endereco);
                if (fornecedor.Endereco != null)
                {
                    var contatoSalvo = ContatoBLL.Salvar(fornecedor.Contato);
                    if (contatoSalvo != null)
                    {
                        fornecedor.Contato = contatoSalvo;
                        FornecedorDAO.Salvar(fornecedor);
                        Connection.Commit();
                        return true;
                    }
                    else
                        Connection.Rollback();
                }
            }
            return false;
        }
        public List<FornecedorModel> Pesquisar(FornecedorModel fornecedor)
        {
            var listFornecedor = FornecedorDAO.Pesquisar(fornecedor).DataTableToList<FornecedorModel>();
            foreach (var item in listFornecedor)
            {
                item.Contato = ContatoBLL.RecuperaContatoId(RecuperaIdContato(item));
                item.Endereco = EnderecoBLL.RecuperaEnderecoId(RecuperaIdEndereco(item));
            }
            return listFornecedor;
        }
        public int RecuperaIdContato(FornecedorModel fornecedor)
        {
            return FornecedorDAO.RetornaIdContato(fornecedor);
        }
        public int RecuperaIdEndereco(FornecedorModel fornecedor)
        {
            return FornecedorDAO.RetornaIdEndereco(fornecedor);
        }

        #region EnderecoFornecedor
        private EnderecoModel SaveAddress(EnderecoModel endereco)
        {
            if (!string.IsNullOrWhiteSpace(endereco.Rua) && !string.IsNullOrWhiteSpace(endereco.Numero))
                return EnderecoBLL.Salvar(endereco);
            return null;
        }
        private bool DeleteAddress(EnderecoModel endereco){
            if (endereco.Id != 0)
                return EnderecoBLL.Excluir(endereco);
            return false;
        }
        #endregion
    }
}
