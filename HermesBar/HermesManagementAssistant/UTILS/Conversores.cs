using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

                    Type type = typeof(T);
                    var nameModel = type.Name.ToString().Replace("Model", "");

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            if (propertyInfo.Name.Equals("Id"))
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name + "_" + nameModel], propertyInfo.PropertyType), null);
                            else
                            {
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                            }
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
        public static T DataTableToSimpleObject<T>(this DataTable table) where T : class, new()
        {
            try
            {
                var obj = new T();
                foreach (var row in table.AsEnumerable())
                {
                    Type type = typeof(T);
                    var nameModel = type.Name.ToString().Replace("Model", "");

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            if (propertyInfo.Name.Equals("Id"))
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name + "_" + nameModel], propertyInfo.PropertyType), null);
                            else 
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                return obj;
            }
            catch
            {
                return null;
            }
        }
        public static int DateTimeToInt(DateTime theDate)
        {
            return (int)(theDate.Date - new DateTime(1900, 1, 1)).TotalDays + 2;
        }
        public static DateTime IntToDateTime(int intDate)
        {
            return new DateTime(1900, 1, 1).AddDays(intDate - 2);
        }
        public static string DataTableToString(this DataTable dataTable)
        {
            var retorno = new StringBuilder();
            
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                    retorno.Append(row[i].ToString());
            }
            return retorno.ToString();
        }
        public static string DataTableToJSON(this DataTable dataTable)
        {
            string[] StrDc = new string[dataTable.Columns.Count];
            string HeadStr = string.Empty;
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                StrDc[i] = dataTable.Columns[i].Caption;
                HeadStr += "\"" + StrDc[i] + "\":\"" + StrDc[i] + i.ToString() + "¾" + "\",";
            }
            HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
            StringBuilder Sb = new StringBuilder();
            Sb.Append("[");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string TempStr = HeadStr;
                for (int j = 0; j < dataTable.Columns.Count; j++)
                    TempStr = TempStr.Replace(dataTable.Columns[j] + j.ToString() + "¾", dataTable.Rows[i][j].ToString().Trim());
                Sb.Append("{" + TempStr + "},");
            }
            Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
            if (Sb.ToString().Length > 0)
                Sb.Append("]");

            return StripControlChars(Sb.ToString());
        }
        public static string StripControlChars(string s)
        {
            return Regex.Replace(s, @"[^\x20-\x7F]", "");
        }
    }
}
