using ENTITY.Commom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Commom
{
    public class EnderecoDAO : Connection.Connection
    {
        public DataTable Insert(HMA_END end)
        {
            try
            {
                OpenConnection();
                var data = new DataTable();

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
