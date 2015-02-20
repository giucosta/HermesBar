using BLL.Comum;
using DAO.Fornecedor;
using MODEL;
using MODEL.Fornecedor;
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
            var enderecoSalvo = EnderecoBLL.Salvar(fornecedor.Endereco);
            if (enderecoSalvo != null)
            {
                var contatoSalvo = ContatoBLL.Salvar(fornecedor.Contato);
                if (contatoSalvo != null)
                {
                    fornecedor.Contato = contatoSalvo;
                    fornecedor.Endereco = enderecoSalvo;
                    return FornecedorDAO.Salvar(fornecedor);
                }
            }
            return false;
        }
        public List<FornecedorModel> Pesquisar(FornecedorModel fornecedor)
        {
            return FornecedorDAO.Pesquisar(fornecedor).DataTableToList<FornecedorModel>();
        }
    }
}
