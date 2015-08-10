using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ICommom
{
    public interface IConnection
    {
        void OpenConnection();
        void CloseConnection();
        void CreateDataAdapter(string spName);
        void InserParameter(string parameter, SqlDbType type, object value);
        DataTable GetResult();
        DataSet GetResult(DataSet data);
        DataSet GetResultAsDataSet();
    }
}
