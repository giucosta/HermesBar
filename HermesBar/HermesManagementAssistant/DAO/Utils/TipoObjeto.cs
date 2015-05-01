using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Utils
{
    public class TipoObjeto : Attribute
    {
        internal String Referencia;

        public TipoObjeto(String referencia)
        {
            Referencia = referencia;
        }

        public enum Tipo
        {
            [TipoObjeto("DAO.Sql.Pedidos.sql")]
            Pedidos
        }
        internal static String GetSQL(String pClasse)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string result = null;

            using (Stream stream = assembly.GetManifestResourceStream(pClasse))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        internal static T GetAttribute<T>(Object pObj)
        {
            Type typ = typeof(T);

            System.Reflection.FieldInfo fie = pObj.GetType().GetField(pObj.ToString());
            Object[] obj = fie.GetCustomAttributes(typeof(T), false);

            foreach (Object objeto in obj)
                return objeto.GetType() == typ ? (T)Convert.ChangeType(objeto, typ) : default(T);

            return default(T);
        }

        internal static Type GetReferencia(string typeName)
        {
            var typ = Type.GetType(typeName);
            if (typ != null) return typ;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                typ = a.GetType(typeName);
                if (typ != null)
                    return typ;
            }
            return null;
        }
    }
}
