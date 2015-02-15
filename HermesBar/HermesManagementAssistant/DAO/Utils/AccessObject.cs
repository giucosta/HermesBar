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
        public static String CreateDataInsert()
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

            return stb.ToString();
        }
        public static String CreateSelectAll()
        {
            var nomeClasse = RetornaNomeClasse();
            var stb = new StringBuilder();
            stb.Append("SELECT * FROM " + nomeClasse);

            return stb.ToString();
        }
        public static String DeleteFromId()
        {
            var nomeClasse = RetornaNomeClasse();
            var stb = new StringBuilder();
            stb.Append("DELETE FROM " + nomeClasse);
            stb.Append("WHERE ");
            stb.Append("Id_" + nomeClasse);
            stb.Append("=");
            stb.Append("@Id_" + nomeClasse);
            return stb.ToString();
        }
        private static String RetornaNomeClasse()
        {
            Type type = typeof(T);
            return type.Name.ToString().Replace("Model","");
        }
        public static String InsertParameter(String command, String parameter,String variavel)
        {
            var stb = new StringBuilder();
            stb.Append(command);
            stb.Append(" " + parameter);
            stb.Append(" " + variavel);

            return stb.ToString();
        }
        public static String InsertSimpleParameter(String parameter)
        {
            var stb = new StringBuilder();
            stb.Append(parameter);
            return stb.ToString();
        }
        public static String CreateSelectWithSimpleParameter(String parameter)
        {
            var stb = new StringBuilder();
            stb.Append(ConstantesDAO.SELECT);
            stb.Append(" " + parameter + " ");
            stb.Append(ConstantesDAO.FROM);
            stb.Append(" " + RetornaNomeClasse());
            return stb.ToString();
        }
    }
}
