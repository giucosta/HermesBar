using DAO.Abstract;
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
using DAO;

namespace DAO.Usuario
{
    public class UsuarioDAO : IUsuario
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

                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Login",login.Login);
                
                return Connection.getDataTable(comando).Rows[0]["Email"].ToString();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaEmailUsuario", "UsuarioDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
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
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Senha",Encript.EncriptMd5.Criptografar(login.Senha));
                comando.Parameters.AddWithValue("@Login",login.Login);

                Connection.ExecutarComando(comando);
                return true;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("GravaNovaSenha","UsuarioDAO",e.StackTrace,Constantes.ATipoMetodo.UPDATE);
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
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Login",login.Login);
                
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
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaUsuario", "UsuarioDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }

        public DataTable Pesquisar(UsuarioModel usuario)
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

                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Nome", "%" + usuario.Nome + "%");
                comando.Parameters.AddWithValue("@Email", "%" + usuario.Email + "%");
                
                var dataTable = Connection.getDataTable(comando);

                if (dataTable.Rows.Count == 0)
                    return null;

                return dataTable;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("PesquisaUsuario", "UsuarioDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }

        public bool PesquisaUsuarioExistente(string nome)
        {
            try
            {
                var sql = "SELECT Count(Nome) AS Quantidade FROM Usuario WHERE Nome = @Nome";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Nome", nome);
                
                if ((int)Connection.getDataTable(comando).Rows[0]["Quantidade"] > 0)
                    return true;

                return false;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("PesquisaUsuarioExistente", "UsuarioDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return false;
            }
        }

        public bool Salvar(UsuarioModel usuario)
        {
            try
            {
                var sql = @"INSERT INTO Usuario VALUES ( @IdLogin, @IdPerfil, @Nome, @Status, @Email )";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("IdLogin", usuario.Login.Id);
                comando.Parameters.AddWithValue("@IdPerfil", usuario.Perfil.IdPerfil);
                comando.Parameters.AddWithValue("@Nome", usuario.Nome);
                comando.Parameters.AddWithValue("@Status", usuario.Status);
                comando.Parameters.AddWithValue("@Email", usuario.Email);

                Connection.ExecutarComando(comando);
                return true;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("GravaUsuario", "UsuarioDAO", e.StackTrace, Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }

        public bool Excluir(UsuarioModel usuario)
        {
            try
            {
                var sql = @"DELETE FROM Usuario WHERE Id_Usuario = @IdUsuario";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);

                Connection.ExecutarComando(comando);
                return true;

            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir","UsuarioDAO", e.StackTrace,Constantes.ATipoMetodo.DELETE);
                return false;
            }
        }
    }
}
