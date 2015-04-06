using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPORTMODEL.Funcionarios
{
    public class ListarFuncionarioReportModel
    {
        public string Nome { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public static List<ListarFuncionarioReportModel> GetList()
        {
            List<ListarFuncionarioReportModel> list = new List<ListarFuncionarioReportModel>();

            var report1 = new ListarFuncionarioReportModel()
            {
                Nome = "Nome1",
                Rg = "Rg1",
                Cpf = "Cpf1"
            };

            var report2 = new ListarFuncionarioReportModel()
            {
                Nome = "Nome2",
                Rg = "Rg2",
                Cpf = "Cpf2"
            };
            var report3 = new ListarFuncionarioReportModel()
            {
                Nome = "Nome3",
                Rg = "Rg3",
                Cpf = "Cpf3"
            };
            list.Add(report1);
            list.Add(report2);
            list.Add(report3);

            return list;
        }
    }
}
