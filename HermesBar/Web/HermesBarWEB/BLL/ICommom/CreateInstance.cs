using DAO.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ICommom
{
    public abstract class CreateInstance<T>
    {
        public static T Instance()
        {
            return Activator.CreateInstance<T>();
        }
    }
}
