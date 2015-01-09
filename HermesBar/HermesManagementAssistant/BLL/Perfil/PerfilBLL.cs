using DAO.Perfil;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Perfil
{
    public class PerfilBLL
    {
        private PerfilDAO _perfilDAO = null;
        public PerfilDAO PerfilDAO
        {
            get
            {
                if (_perfilDAO == null)
                    _perfilDAO = new PerfilDAO();
                return _perfilDAO;
            }
        }

        public List<string> RecuperaTodosPerfil()
        {
            return PerfilDAO.RecuperaTodosPerfil();
        }

        public int RecuperaIdPerfil(PerfilModel perfil)
        {
            return PerfilDAO.RecuperaIdPerfil(perfil);
        }
    }
}
