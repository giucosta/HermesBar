using DAO.Contexto;
using Model.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Usuarios
{
    public class UsuariosDAO
    {
        private Context ctx;

        public UsuariosDAO() { }
        public bool CadastrarUsuario(Usuario usuario)
        {
            try
            {
                Context.GetInstance().Usuarios.Add(usuario);
                Context.GetInstance().SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
