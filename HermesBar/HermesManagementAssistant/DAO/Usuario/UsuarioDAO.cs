using DAO.Connections;
using DAO.Login;
using DAO.Perfil;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
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

        /// <summary>
        /// Carrega todos os dados do usuario
        /// Metodo utilizado principalmente pra carregar a Session
        /// </summary>
        /// <param name="login">LoginModel login</param>
        /// <returns>UsuarioModel</returns>
        public UsuarioModel RetornaUsuario(LoginModel login)
        {
            try
            {
                string sql = @"SELECT * FROM Usuario U INNER JOIN Login L ON L.Id_Login = U.Id_Login WHERE L.Login = @Login ";
                var comando = new SqlCommand(sql, Connection.getConnection());
                comando.Parameters.Add(new SqlParameter("@Login", login.Login));

                var dataTable = Connection.getDataTable(comando);

                return new UsuarioModel()
                {
                    IdUsuario = (int)dataTable.Rows[0]["Id_Usuario"],
                    Email = dataTable.Rows[0]["Email"].ToString(),
                    Nome = dataTable.Rows[0]["Nome"].ToString(),
                    Status = dataTable.Rows[0]["Status"].ToString(),
                    Login = new LoginDAO().RecuperaLogin(login),
                    Perfil = new PerfilDAO().RecuperaPerfil(login)
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UsuarioModel> PesquisaUsuario(UsuarioModel usuario)
        {
            try
            {
                string sql = @"
                                SELECT 
                                    Id_Usuario,
	                                Nome, 
	                                Email,
	                                CASE Status
		                                WHEN 'A' THEN 'Ativo'
		                                WHEN 'C' THEN 'Cancelado'
	                                END Status
                                FROM Usuario
                                INNER JOIN Perfil ON Perfil.Id_Perfil = Usuario.Id_Perfil
                                WHERE 1=1
                                AND Nome LIKE @Nome
                                AND Email LIKE @Email";

                var comando = new SqlCommand(sql, Connection.getConnection());
                comando.Parameters.Add(new SqlParameter("@Nome", "%" + usuario.Nome + "%"));
                comando.Parameters.Add(new SqlParameter("@Email", "%" + usuario.Email + "%"));

                var dataTable = Connection.getDataTable(comando);

                if (dataTable.Rows.Count == 0)
                    return null;

                var modelList = new List<UsuarioModel>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    var user = new UsuarioModel();
                    user.Email = dataTable.Rows[i]["Email"].ToString();
                    user.Nome = dataTable.Rows[i]["Nome"].ToString();
                    user.Status = dataTable.Rows[i]["Status"].ToString();
                    user.IdUsuario = (int)dataTable.Rows[i]["Id_Usuario"];
                    modelList.Add(user);
                }

                return modelList;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
