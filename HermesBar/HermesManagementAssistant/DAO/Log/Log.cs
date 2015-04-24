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
using MODEL.Log;

namespace DAO.Log
{
    public static class Log
    {
        public static void GravarLog(string Metodo, string Classe, string Erro, string Tipo)
        {
            try
            {
                AccessObject<LogsModel> AO = new AccessObject<LogsModel>();
                AO.CreateDataInsert();
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
