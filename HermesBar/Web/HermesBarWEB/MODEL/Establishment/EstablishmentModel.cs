using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MODEL.Establishment
{
    public class EstablishmentModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public List<SelectListItem> Status { get; set; }
        public string StatusSelected { get; set; }
        public string Cnpj { get; set; }
        public Contact.ContatoModel Contato { get; set; }
        public Address.EnderecoModel Endereco { get; set; }
        public int QuantidadeMesas { get; set; }
        public int QuantidadeMaximoPessoas { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
    }
}
