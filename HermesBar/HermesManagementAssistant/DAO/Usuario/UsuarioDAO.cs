using DAO.Connections;
using MODEL;
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
        /// <summary>
        /// Recupera no banco o email do usuario
        /// </summary>
        /// <param name="login">LoginModel</param>
        /// <returns>Email</returns>
        public string RecuperaEmailUsuario(LoginModel login)
        {
            try
            {
                string sql = @"
                            SELECT 
                                Email 
                            FROM Usuario U
                            INNER JOIN Login L ON L.Id_Login = U.Id_Login 
                            WHERE L.Login = @Login";

                var comando = new SqlCommand(sql, Connection.getConnection());
                comando.Parameters.Add(new SqlParameter("@Login", login.Login));

                return Connection.getDataTable(comando).Rows[0]["Email"].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Grava a nova senha do usuario no banco
        /// </summary>
        /// <param name="login">LoginModel login</param>
        /// <returns>boolean</returns>
        public bool GravaNovaSenha(LoginModel login)
        {
            try
            {
                string sql = "UPDATE Login SET Senha = @Senha WHERE Login = @Login";
                var comando = new SqlCommand(sql, Connection.getConnection());
                comando.Parameters.Add(new SqlParameter("@Senha", Encript.EncriptMd5.Criptografar(login.Senha)));
                comando.Parameters.Add(new SqlParameter("@Login", login.Login));

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
