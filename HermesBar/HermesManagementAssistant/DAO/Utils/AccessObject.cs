using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Utils
{
    public class AccessObject<T>
    {
        private static string sql;
        public AccessObject()
        {
            sql = string.Empty;
        }
        public void CreateDataInsert()
        {
            Type myType = typeof(T);
            var nomeClasse = RetornaNomeClasse();
            var stb = new StringBuilder();
            stb.Append("INSERT INTO " + nomeClasse);
            stb.Append(" VALUES");
            stb.Append(" (");

            var elemento = myType.GetProperties().Length;
            int i = 1;

            foreach (PropertyInfo property in myType.GetProperties())
            {
                if (elemento != i)
                {
                    if (i != 1)
                        stb.Append(", ");
                    if (!property.Name.Equals("Id"))
                    {
                        stb.Append("@");
                        stb.Append(property.Name);
                        i++;
                    }
                }
                else
                {
                    if (!property.Name.Equals("Id"))
                    {
                        stb.Append("@");
                        stb.Append(property.Name);
                    }
                }
            }
            stb.Append(" )");
            sql += FormatSql(stb.ToString());
        }
        public void CreateSelectAll()
        {
            var nomeClasse = RetornaNomeClasse();
            var stb = new StringBuilder();
            stb.Append("SELECT * FROM " + nomeClasse);

            sql += FormatSql(stb.ToString());
        }
        public void DeleteFromId()
        {
            var nomeClasse = RetornaNomeClasse();
            var stb = new StringBuilder();
            stb.Append("DELETE FROM " + nomeClasse);
            stb.Append("WHERE ");
            stb.Append("Id_" + nomeClasse);
            stb.Append("=");
            stb.Append("@Id_" + nomeClasse);
            sql += FormatSql(stb.ToString());
        }
        private String RetornaNomeClasse()
        {
            Type type = typeof(T);
            return type.Name.ToString().Replace("Model","");
        }
        public void InsertParameter(String sqlCommand,String attribute, String condition, String comparisionAttribute )
        {
            var stb = new StringBuilder();
            stb.Append(" " + sqlCommand);
            stb.Append(" " + attribute);
            stb.Append(" " + condition);
            stb.Append(" " + comparisionAttribute);
            sql += FormatSql(stb.ToString());
        }
        /// <summary>
        /// Utilizar quando necessitar criar uma Query específica, passando 2 parametros de cada vez
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="parameter"></param>
        public void CreateSpecificQuery(String query)
        {
            sql += query;
        }
        public void CreateSelectWithSimpleParameter(String parameter)
        {
            var stb = new StringBuilder();
            stb.Append(ConstantesDAO.SELECT);
            stb.Append(" " + parameter + " ");
            stb.Append(ConstantesDAO.FROM);
            stb.Append(" " + RetornaNomeClasse());
            sql+= FormatSql(stb.ToString());
        }

        public void CreateUpdate(String attribute, String parameter)
        {
            var stb = new StringBuilder();
            var table = RetornaNomeClasse();
            stb.Append(ConstantesDAO.UPDATE);
            stb.Append(" " + table);
            stb.Append(" " + ConstantesDAO.SET);
            stb.Append(" " + attribute);
            stb.Append(" " + ConstantesDAO.EQUAL);
            stb.Append(" " + "@" + attribute);
            stb.Append(" " + ConstantesDAO.WHERE);
            stb.Append(" " + parameter);
            stb.Append(" " + ConstantesDAO.EQUAL);
            stb.Append(" " + "@" + parameter);
            sql += FormatSql(stb.ToString());
        }
        private string FormatSql(string command)
        {
            return sql = " " + command;
        }
        public String ReturnQuery()
        {
            return sql;
        }
    }
}
