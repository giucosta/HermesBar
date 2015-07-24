using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Address
{
    public class EnderecoModel
    {
        public int Id { get; set; }
        [DisplayName("Rua")]
        public string Rua { get; set; }
        [DisplayName("Número")]
        public string Numero { get; set; }
        [DisplayName("Bairro")]
        public string Bairro { get; set; }
        [DisplayName("Complemento")]
        public string Complemento { get; set; }
        [DisplayName("CEP")]
        public string Cep { get; set; }
        [DisplayName("Cidade")]
        public string Cidade { get; set; }
        [DisplayName("UF")]
        public int Uf { get; set; }
    }
}
