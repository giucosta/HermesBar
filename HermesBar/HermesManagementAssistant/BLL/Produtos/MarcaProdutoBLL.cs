using DAO.Produtos;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

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

        public bool Salvar(MarcaModel marca)
        {
            return MarcaProdutoDAO.Salvar(marca);
        }
        public List<MarcaModel> RetonaMarca()
        {
            return MarcaProdutoDAO.RetornaMarcas().DataTableToList<MarcaModel>();
        }
        public MarcaModel RecuperaMarcaid(int id)
        {
            try
            {
                return MarcaProdutoDAO.RecuperaMarcaId(id).DataTableToSimpleObject<MarcaModel>();
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                throw e;
            }
        }
    }
}
