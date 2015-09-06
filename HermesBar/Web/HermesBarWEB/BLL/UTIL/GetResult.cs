using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UTIL
{
    public static class GetResult
    {
        public static bool GetResults(this DataTable data)
        {
            try
            {
                return Convert.ToInt32(data.Rows[0]["SUCCESS"]) != 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
