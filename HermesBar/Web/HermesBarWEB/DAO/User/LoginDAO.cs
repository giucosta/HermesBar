using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.User;

namespace DAO.User
{
    public class LoginDAO : Connection.Connection
    {
        public DataTable Login(HMA_USR USR)
        {
            try
            {
                OpenConnection();
                var data = new DataTable();
                CreateDataAdapter(SQL.SP_USR.LOGON);
                InserParameter("NOM", SqlDbType.VarChar, USR.NOM);
                InserParameter("PAS", SqlDbType.VarChar, USR.PAS);

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
