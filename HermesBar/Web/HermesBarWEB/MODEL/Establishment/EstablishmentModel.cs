using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MODEL.Establishment
{
    public class EstablishmentModel
    {
        public int Id { get; set; }
        [DisplayName("Nome Fantasia")]
        public string NomeFantasia { get; set; }
        [DisplayName("Razão Social")]
        public string RazaoSocial { get; set; }
        [DisplayName("Status")]
        public List<SelectListItem> Status { get; set; }
        public string StatusSelected { get; set; }
        [DisplayName("CNPJ")]
        public string Cnpj { get; set; }
        public Contact.ContatoModel Contato { get; set; }
        public Address.EnderecoModel Endereco { get; set; }
        [DisplayName("Quantidade de Mesas")]
        public int QuantidadeMesas { get; set; }
        [DisplayName("Lotação Máxima")]
        public int QuantidadeMaximoPessoas { get; set; }
        [DisplayName("Inscrição Estadual")]
        public string InscricaoEstadual { get; set; }
        [DisplayName("Inscrição Municipal")]
        public string InscricaoMunicipal { get; set; }

        public string MatrizSelected { get; set; }
        public List<SelectListItem> Matriz { get; set; }
    }
}
