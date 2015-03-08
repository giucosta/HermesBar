using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Fornecedor;

namespace MODEL.Produto
{
    public class MarcaProdutoModel : IModel
    {
        public string Marca { get; set; }
        public FornecedorModel Fornecedor { get; set; }
    }
}
