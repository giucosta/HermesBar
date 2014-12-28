using MODEL.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Connections;
using System.Data.SqlClient;
using System.Data;

namespace DAO.Login
{
    public class LoginDAO
    {
        public bool efetuaLogin(LoginModel login)
        {
            try
            {
                string sql = "SELECT * FROM Usuarios WHERE Nome = @Nome";
                
                var comando = new SqlCommand(sql, Connection.getConnection());
                comando.Parameters.Add(new SqlParameter("@Nome", login.Usuario.Nome));

                if (login.Usuario.Senha.Equals(Connection.getDataTable(comando).Rows[0]["Senha"].ToString()))
                    return true;

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
