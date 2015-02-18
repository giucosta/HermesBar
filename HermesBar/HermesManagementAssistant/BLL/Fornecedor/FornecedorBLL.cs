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

        public List<FornecedorModel> Pesquisar(FornecedorModel fornecedor)
        {
            return FornecedorDAO.Pesquisar(fornecedor).DataTableToList<FornecedorModel>();
        }
    }
}
