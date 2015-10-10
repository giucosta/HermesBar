using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HELPER
{
    public sealed class Singleton<T> where T : class, new()
    {
        private static T _instancia;
        public static T Instance()
        {
            lock (typeof(T))
                if (_instancia == null)
                    _instancia = new T();
            return _instancia;
        }
    }
}
