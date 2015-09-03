using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MODEL.Employee
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public Contact.ContatoModel Contato { get; set; }
        public Address.EnderecoModel Endereco { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public string Ctps { get; set; }
        public List<SelectListItem> Status { get; set; }
        public string StatusSelected { get; set; }
        public DateTime DataAdmissao { get; set; }
        public DateTime DataDemissao { get; set; }
        public List<SelectListItem> Tipo { get; set; }
        public string TipoSelected { get; set; }
        public string Funcao { get; set; }
        public List<SelectListItem> Sexo { get; set; }
        public string SexoSelected { get; set; }
        public List<SelectListItem> Cargo { get; set; }
        public string CargoSelected { get; set; }
    }
}
