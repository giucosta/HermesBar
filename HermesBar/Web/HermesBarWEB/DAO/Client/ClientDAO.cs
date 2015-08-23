using ENTITY.Client;
using ENTITY.Commom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Client
{
    public class ClientDAO : Connection.Connection
    {
        public DataTable Insert(HMA_CLI cli, HMA_CON con)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_CLI.INSERT);
                InserParameter("USR", SqlDbType.Int, cli._USR);
                InserParameter("ATV", SqlDbType.Int, cli._ATV);
                InserParameter("NASC", SqlDbType.DateTime, cli.NASC);

                InserParameter("NOM", SqlDbType.VarChar, con.NOM);
                InserParameter("TEL", SqlDbType.VarChar, con.TEL);
                InserParameter("CEL", SqlDbType.VarChar, con.CEL);
                InserParameter("EMA", SqlDbType.VarChar, con.EMA);

                return GetResult();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
