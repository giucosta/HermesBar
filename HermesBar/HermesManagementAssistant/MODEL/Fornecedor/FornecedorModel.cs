using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Fornecedor
{
    public class FornecedorModel : IModel
    {
        public string RazaoSocial { get; set; }
        public string Cpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public ContatoModel Contato { get; set; }
        public EnderecoModel Endereco { get; set; }
    }
}
