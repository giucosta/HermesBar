using ENTITY.PDV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PDV
{
    public class Pdv_ClientDAO : Connection.Connection
    {
        public DataTable Insert(HMA_PDV_CLI cli)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_PDV_CLI.INSERT);
                InserParameter("USR", SqlDbType.Int, cli._USR);
                InserParameter("ID_CLI", SqlDbType.Int, cli._ID_CLI);
                InserParameter("ID_CAI", SqlDbType.Int, cli._ID_CAI);
                InserParameter("NUM_CAR", SqlDbType.Int, cli.NUM_CAR);

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
