using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MODEL.Employee
{
    public class EmployeeModel
    {
        [DisplayName("Código")]
        public int Id { get; set; }
        public Contact.ContatoModel Contato { get; set; }
        public Address.EnderecoModel Endereco { get; set; }
        [DisplayName("RG")]
        public string Rg { get; set; }
        [DisplayName("CPF")]
        public string Cpf { get; set; }
        [DisplayName("CTPS")]
        public string Ctps { get; set; }
        public List<SelectListItem> Status { get; set; }
        public string StatusSelected { get; set; }
        [DisplayName("Data de Admissão")]
        public DateTime DataAdmissao { get; set; }
        [DisplayName("Data de Demissão")]
        public DateTime DataDemissao { get; set; }
        public List<SelectListItem> Tipo { get; set; }
        public string TipoSelected { get; set; }
        [DisplayName("Função")]
        public string Funcao { get; set; }
        public List<SelectListItem> Sexo { get; set; }
        public string SexoSelected { get; set; }
        public List<SelectListItem> Cargo { get; set; }
        public string CargoSelected { get; set; }
    }
}
