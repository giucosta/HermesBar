using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MODEL.Product
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        [DisplayName("Nome")]
        public string Nome { get; set; }
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [DisplayName("Valor de Compra")]
        public double ValorCompra { get; set; }
        [DisplayName("Valor de Venda")]
        public double ValorVenda { get; set; }
        public string TipoSelected { get; set; }
        [DisplayName("Tipos")]
        public List<SelectListItem> Tipos { get; set; }
        [DisplayName("Código de Venda")]
        public string CodigoVenda { get; set; }
        public string UnidadeMedidaSelected { get; set; }
        [DisplayName("Unidade de Medida")]
        public List<SelectListItem> UnidadesMedida { get; set; }
        [DisplayName("Quant. Mínima para Aviso")]
        public int QuantidadeMinimaAviso{ get; set; }
        [DisplayName("Quantidade Ideal")]
        public double QuantidadeAtual { get; set; }
        public string CategoriaSelected { get; set; }
        [DisplayName("Categoria")]
        public List<SelectListItem> Categorias { get; set; }
    }
}
