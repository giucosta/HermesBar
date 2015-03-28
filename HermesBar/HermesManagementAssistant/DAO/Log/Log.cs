using DAO.Connections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using UTIL;
using DAO.Utils;

namespace DAO.Log
{
    public static class Log
    {
        public static void GravarLog(string Metodo, string Classe, string Erro, string Tipo)
        {
            try
            {
                AccessObject<Object> AO = new AccessObject<Object>();
                AO.CreateSpecificQuery(@"INSERT INTO Logs VALUES(@Metodo, @Classe, @Data, @Usuario, @Erro, @Tipo)");
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Metodo", Metodo);
                Connection.AddParameter("@Classe", Classe);
                Connection.AddParameter("@Data", DateTime.Now);
                Connection.AddParameter("@Usuario", Session.Usuario.Nome);
                Connection.AddParameter("@Erro", Erro);
                Connection.AddParameter("@Tipo", Tipo);
                
                Connection.ExecutarComando();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
