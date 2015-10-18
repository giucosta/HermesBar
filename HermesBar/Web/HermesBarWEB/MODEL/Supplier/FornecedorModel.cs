using MODEL.Address;
using MODEL.Contact;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MODEL.Supplier
{
    public class FornecedorModel
    {
        public int Id { get; set; }
        [DisplayName("Razão Social")]
        public string RazaoSocial { get; set; }
        [DisplayName("Nome Fantasia")]
        public string NomeFantasia { get; set; }
        [DisplayName("CNPJ")]
        public string Cnpj { get; set; }
        [DisplayName("Inscrição Municipal")]
        public string InscricaoMunicipal { get; set; }
        [DisplayName("Inscrição Estadual")]
        public string InscricaoEstadual { get; set; }
        public EnderecoModel Endereco { get; set; }
        public ContatoModel Contato { get; set; }
        [DisplayName("Status")]
        public List<SelectListItem> Status { get; set; }
        public string StatusSelected { get; set; }
        public string MatrizSelected { get; set; }
        public List<SelectListItem> Matriz { get; set; }
        
    }
}
