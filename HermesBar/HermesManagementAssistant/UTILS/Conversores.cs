using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UTILS
{
    public static class Conversores
    {
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                var list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    var obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return null;
            }
        }
        public static List<String> DataTableToListString(this DataTable table, string attribute)
        {
            var lista = new List<String>();
            for (int i = 0; i < table.Rows.Count; i++)
                lista.Add(table.Rows[i][attribute].ToString());
            return lista;
        } 
    }
}
