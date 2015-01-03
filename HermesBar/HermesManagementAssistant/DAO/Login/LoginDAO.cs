using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Connections;
using System.Data.SqlClient;
using System.Data;
using MODEL;
using DAO.Log;
using System.Diagnostics;

namespace DAO.Login
{
    public class LoginDAO
    {
        /// <summary>
        /// Recebe login como parametro, efetua a decriptacao da senha do banco e compara com a digitada pelo usuario
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool EfetuaLogin(LoginModel login)
        {
            try
            {
                string sql = "SELECT * FROM Login WHERE Login = @Login";
                
                var comando = new SqlCommand(sql, Connection.getConnection());
                comando.Parameters.Add(new SqlParameter("@Login", login.Login));

                if (login.Senha.Equals(Encript.EncriptMd5.Descriptografar(Connection.getDataTable(comando).Rows[0]["Senha"].ToString())))
                {
                    AlteraUltimaDataAcesso(login);
                    return true;
                }
                    

                return false;
            }
            catch (Exception)
            {
                Log.Log.GravarLog("EfetuaLogin", "LoginDAO");
                return false;
            }
        }

        /// <summary>
        /// Retorna um LoginModel carregado, recebendo como parametro pra pesquisa um LoginModel
        /// </summary>
        /// <param name="login">LoginModel login</param>
        /// <returns></returns>
        public LoginModel RecuperaLogin(LoginModel login)
        {
            try
            {
                string sql = "SELECT * FROM Login WHERE Login = @Login";

                var comando = new SqlCommand(sql, Connection.getConnection());
                comando.Parameters.Add(new SqlParameter("@Login", login.Login));

                var dataTable = Connection.getDataTable(comando);

                if (dataTable.Rows.Count == 0)
                    return null;

                login.IdLogin = (int)dataTable.Rows[0]["Id_Login"];
                login.Senha = dataTable.Rows[0]["Senha"].ToString();
                login.UltimoLogin = DateTime.Parse(dataTable.Rows[0]["DataUltimoLogin"].ToString());

                return login;
            }
            catch (Exception)
            {
                Log.Log.GravarLog("RecuperaLogin", "LoginDAO");
                throw;
            }
        }

        public void AlteraUltimaDataAcesso(LoginModel login)
        {
            try
            {
                var sql = "UPDATE Login SET DataUltimoLogin = @UltimoLogin WHERE Login = @Login";
                var comando = new SqlCommand(sql, Connection.getConnection());
                comando.Parameters.Add(new SqlParameter("@UltimoLogin", DateTime.Now));
                comando.Parameters.Add(new SqlParameter("@Login", login.Login));

                Connection.ExecutarComando(comando);
            }
            catch (Exception)
            {
                Log.Log.GravarLog("AlteraUltimaDataAcesso", "LoginDAO");
                throw;
            }
        }

        public bool GravaLogin(LoginModel login)
        {
            try
            {
                var sql = @"INSERT INTO Login VALUES(
                                @Login,
                                @Senha,
                                null
                            )";
                var comando = new SqlCommand(sql, Connection.getConnection());
                comando.Parameters.Add(new SqlParameter("@Login",login.Login));
                comando.Parameters.Add(new SqlParameter("@Senha",Encript.EncriptMd5.Criptografar(login.Senha)));

                Connection.ExecutarComando(comando);
                return true;
            }
            catch (Exception)
            {
                Log.Log.GravarLog("GravarLogin", "LoginDAO");
                return false;
            }
        }
    }
}
