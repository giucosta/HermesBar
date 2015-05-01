using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMAPedidos
{
    public class Pedidos : DataAttribute
    {
        [Column(Storage = "Id")]
        public int Id { get; set; }
        [Column(Storage = "Nome")]
        public string Nome { get; set; }
        [Column(Storage = "Id_Teste")]
        public Teste Teste { get; set; }
    }
}
