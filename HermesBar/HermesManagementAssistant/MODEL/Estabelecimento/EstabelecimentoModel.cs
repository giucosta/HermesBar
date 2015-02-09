using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Estabelecimento
{
    public class EstabelecimentoModel : IModel
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string InscEstadual { get; set; }
        public EnderecoModel Endereco { get; set; }
        public ContatoModel Contato { get; set; }
        public ConfigEstabelecimentoModel ConfigEstabelecimento { get; set; }
    }
}
