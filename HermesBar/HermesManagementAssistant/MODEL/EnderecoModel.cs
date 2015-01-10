using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class EnderecoModel : IModel
    {
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public TipoEndereco Tipo { get; set; }
    }
    public enum TipoEndereco
    {
        Cobrança,
        Entrega,
        Filial,
        Matriz
    }
}
