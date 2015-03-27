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
        public static class ADadosEmail
        {
            public const int PORTA_EMAIL = 587;
            public const string HOST_EMAIL = "smtp.gmail.com";
            public const string ENDERECO_EMAIL = "hermesmanagementassistant@gmail.com";
            public const string IDENTIFICACAO_EMAIL = "Hermes Management Assistant";
            public const string SENHA_EMAIL = "hermesBarSistema";
            public const string TITULO_EMAIL = "Hermes Management Assistant";
            public const string CORPO_INICIO_EMAIL = "Olá, segue sua nova senha de acesso ao sistema Hermes Management Assistant";
            public const string CORPO_MEIO_EMAIL = "Acreditamos que seja interessante efetuar a troca da senha, acessando o módulo Gestão/Configurações/Senhas";
            public const string CORPO_FINAL_EMAIL = "Email enviando automaticamente, caso necessite entrar em contato envie email para: giulianocosta@outlook.com";
        }
        public static class ATipoEndereco
        {
            public const string COBRANCA = "Cobrança";
            public const string ENTREGA = "Entrega";
            public const string FILIAL = "Filial";
            public const string MATRIZ = "Matriz";
            public const string PESSOAL = "Pessoal";
            public const string RECADO = "Recado";
        }

        public static class ATipoSistema
        {
            public const string MESA = "M";
            public const string COMANDA = "C";
        }
        public static class AUnidadeProduto
        {
            public const string LITRO = "LT";
            public const string KILO = "KG";
            public const string GRAMA = "GR";
            public const string ML = "ML";
            public const string LATA = "LATA";
            public const string UNIDADE = "UNIDADE";
            public const string PORCAO = "PORÇÃO";
            public const string GARRAFA = "GARRAFA";
            public const string FARDO = "FARDO";
            public const string FRASCO = "FRASCO";
        }

        public static class AConexaoBanco
        {
            public const string SERVER = @"server = GIULIANOCOSTA\SQLEXPRESS;";
            public const string DATABASE = @"Database = HermesBar;";
            public const string SEGURANCA = @"integrated security = true;";
        }
        public static class AInscricaoEstadual
        {
            public const string ISENTO = "Isento";
        }
    }
}
