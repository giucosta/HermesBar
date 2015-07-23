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
        public LoginDAO() { }
        public DataTable Login(HMA_USR USR)
        {
            try
            {
                OpenConnection();
                var data = new DataTable();
                CreateDataAdapter("[dbo].[SP_HMA_LOG_ON]");
                InserParameter("EMA", SqlDbType.VarChar, USR.EMA);
                InserParameter("PAS", SqlDbType.VarChar, USR.PAS);

                return GetResult(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
