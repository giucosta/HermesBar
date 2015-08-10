using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.ICommom;
using ENTITY.Product;

namespace DAO.Product
{
    public class UnitySizeDAO : IDao<HMA_UNI_MED>
    {
        private IConnection _connection;
        public UnitySizeDAO(IConnection c)
        {
            this._connection = c;
        }

        public DataTable Get(HMA_UNI_MED? entity)
        {
            _connection.OpenConnection();
            _connection.CreateDataAdapter("[dbo].[SP_HMA_UNI_MED_GET]");

            return _connection.GetResult();
        }
        public DataTable Insert(HMA_UNI_MED entity)
        {
            return null;
        }
        public DataTable Update(HMA_UNI_MED entity)
        {
            return null;
        }
    }
}
