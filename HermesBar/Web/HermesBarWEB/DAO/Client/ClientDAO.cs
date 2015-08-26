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
                InserParameter("RG", SqlDbType.VarChar, cli.RG);

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
        public DataSet Get(HMA_CLI cli)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_CLI.GET);
                InserParameter("ID", SqlDbType.Int, cli._ID);
                InserParameter("RG", SqlDbType.VarChar, cli.RG);

                return GetResultAsDataSet();
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
        public DataTable Inactive(HMA_CLI cli)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_CLI.INACTIVE);
                InserParameter("ID", SqlDbType.Int, cli._ID);
                InserParameter("USR", SqlDbType.Int, cli._USR);

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
        public DataTable Active(HMA_CLI cli)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_CLI.ACTIVE);
                InserParameter("ID", SqlDbType.Int, cli._ID);
                InserParameter("USR", SqlDbType.Int, cli._USR);

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
