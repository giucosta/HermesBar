using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Caixa;
using MODEL.Produto;

namespace MODEL.Pedido
{
    public class PedidoModel : IModel
    {
        public CartaoModel NumeroCartao { get; set; }
        public FuncionarioModel CodigoFuncionario { get; set; }
        public ProdutoModel CodigoProduto{ get; set; }
        public string Quantidade { get; set; }
        public string Observacao { get; set; }
        public DateTime Data { get; set; }
    }
}
