using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Usuarios;
using Model.Login;

namespace DAO.Contexto
{
    class Context : DbContext
    {
        private static Context ctx;

        public static Context GetInstance()
        {
            if (ctx == null)
                ctx = new Context();

            return ctx;
        }

        public Context(){ }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Login> Login { get; set; }
    }
}
