using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Abstract
{
    public interface IDAO<T>
    {
        bool Salvar(T objeto);
        bool Excluir(T objeto);
    }
}
