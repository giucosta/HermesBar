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
                AO.GetCommand();
                AO.InsertParameter("Metodo", Metodo);
                AO.InsertParameter("Classe", Classe);
                AO.InsertParameter("Data", DateTime.Now);
                AO.InsertParameter("Usuario", Session.Usuario.Nome);
                AO.InsertParameter("Erro", Erro);
                AO.InsertParameter("Tipo", Tipo);

                AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
