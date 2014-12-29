using DAO.Connections;
using MODEL.Login;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Usuario
{
    public class UsuarioDAO
    {
        public string RecuperaEmailUsuario(LoginModel login)
        {
            try
            {
                string sql = "SELECT Email FROM Usuarios where Nome = @Nome";

                var comando = new SqlCommand(sql, Connection.getConnection());
                comando.Parameters.Add(new SqlParameter("@Nome", login.Usuario.Nome));

                return Connection.getDataTable(comando).Rows[0]["Email"].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool GravaNovaSenha(LoginModel login)
        {
            try
            {
                string sql = "UPDATE Usuarios SET Senha = @Senha where Nome = @Nome";
                var comando = new SqlCommand(sql, Connection.getConnection());
                comando.Parameters.Add(new SqlParameter("@Senha", Encript.EncriptMd5.Criptografar(login.Usuario.Senha)));
                comando.Parameters.Add(new SqlParameter("@Nome", login.Usuario.Nome));

                Connection.ExecutarComando(comando);
                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
