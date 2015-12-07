using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPORTMODEL.Funcionarios
{
    public class ReciboFuncionarioReportModel
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime Data
        {
            get
            {
                return DateTime.Now;
            }
        }
        public double Valor { get; set; }
        public string Descricao { get; set; }
    }
}
