using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMAPedidos
{
    public class Teste : DataAttribute
    {
        [Column(Storage="Id")]
        public int Id { get; set; }
        [Column(Storage = "Teste")]
        public string Testes { get; set; }
    }
}
