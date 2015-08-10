using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ICommom
{
    public interface IDao<T>
    {
        DataTable Get(T? model);
        DataTable Insert(T model);
        DataTable Update(T model);
    }
    public interface IDao<T, Z, Y>
    {
        DataSet Get(T model);
        DataTable Insert(T t, Z z, Y y);
        DataTable Update(T t, Z z, Y y);
    }
}
