using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTILS
{
    public static class Constantes
    {
        public static class APerfil
        {
            public const int ADMINISTRADOR = 1;
            public const int GERENTE = 2;
            public const int GARCOM = 3;
            public const int BARMAN = 4;
            public const int MANUTENCAO = 5;
        }
        public static class ATipoMetodo
        {
            public const string UPDATE = "Update";
            public const string INSERT = "Insert";
            public const string SELECT = "Pesquisa";
            public const string DELETE = "Exclusão";
        }
    }
}
