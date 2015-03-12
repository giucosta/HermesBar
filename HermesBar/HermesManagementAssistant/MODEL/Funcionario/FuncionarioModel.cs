using MODEL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class FuncionarioModel : IModel
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CarteiraTrabalho { get; set; }
        public string Serie { get; set; }
        public EnderecoModel Endereco { get; set; }
        public TipoFuncionarioModel Tipo { get; set; }
        public ContatoModel Contato { get; set; }
        public DateTime DataAdmissao { get; set; }
    }
}
