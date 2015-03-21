using DAO.Produtos;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace BLL.Produtos
{
    public class MarcaProdutoBLL
    {
        private MarcaProdutoDAO _marcaProdutoDAO = null;
        public MarcaProdutoDAO MarcaProdutoDAO
        {
            get
            {
                if (_marcaProdutoDAO == null)
                    _marcaProdutoDAO = new MarcaProdutoDAO();
                return _marcaProdutoDAO;
            }
        }

        public bool Salvar(MarcaProdutoModel marca)
        {
            return MarcaProdutoDAO.Salvar(marca);
        }
        public List<MarcaProdutoModel> RetonaMarca()
        {
            return MarcaProdutoDAO.RetornaMarcas().DataTableToList<MarcaProdutoModel>();
        }
    }
}
