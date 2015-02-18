using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Connections;
using System.Data.SqlClient;
using System.Data;
using MODEL;
using System.Diagnostics;
using DAO;
using DAO.Abstract;
using DAO.Log;
using DAO.Utils;

namespace DAO.Login
{
    public class LoginDAO
    {
        public bool EfetuaLogin(LoginModel login)
        {
            try
            {
                AccessObject<LoginModel> AO = new AccessObject<LoginModel>();
                AO.CreateSelectAll();
                AO.InsertParameter(ConstantesDAO.WHERE, "Login");
                AO.InsertParameter(ConstantesDAO.EQUAL, "@Login");
                
                var comando = new SqlCommand(AO.ReturnQuery(), Connection.GetConnection());
                comando.Parameters.AddWithValue("@Login",login.Login);

                if (login.Senha.Equals(Encript.EncriptMd5.Descriptografar(Connection.getDataTable(comando).Rows[0]["Senha"].ToString())))
                {
                    AlteraUltimaDataAcesso(login);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("EfetuaLogin", "LoginDAO",e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return false;
            }
        }

        public LoginModel RecuperaLogin(LoginModel login)
        {
            try
            {
                AccessObject<LoginModel> AO = new AccessObject<LoginModel>();
                AO.CreateSelectAll();
                AO.InsertParameter(ConstantesDAO.WHERE, "Login");
                AO.InsertParameter(ConstantesDAO.EQUAL, "@Login");
                
                var comando = new SqlCommand(AO.ReturnQuery(), Connection.GetConnection());
                comando.Parameters.AddWithValue("@Login",login.Login);

                var dataTable = Connection.getDataTable(comando);

                if (dataTable.Rows.Count == 0)
                    return null;

                login.Id = (int)dataTable.Rows[0]["Id_Login"];
                login.Senha = dataTable.Rows[0]["Senha"].ToString();

                var data = dataTable.Rows[0]["DataUltimoLogin"].ToString();
                if (string.IsNullOrWhiteSpace(data))
                    login.UltimoLogin = DateTime.Now;
                else
                    login.UltimoLogin = DateTime.Parse(data);

                return login;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaLogin", "LoginDAO",e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }

        public void AlteraUltimaDataAcesso(LoginModel login)
        {
            try
            {
                AccessObject<LoginModel> AO = new AccessObject<LoginModel>();
                AO.InsertParameter(ConstantesDAO.UPDATE,"login");
                AO.InsertParameter(ConstantesDAO.SET, "DataUltimoLogin");
                AO.InsertParameter(ConstantesDAO.EQUAL, "@UltimoLogin");
                AO.InsertParameter(ConstantesDAO.WHERE, "Login");
                AO.InsertParameter(ConstantesDAO.EQUAL, "@Login");

                var comando = new SqlCommand(AO.ReturnQuery(), Connection.GetConnection());
                comando.Parameters.Add(new SqlParameter("@UltimoLogin", DateTime.Now));
                comando.Parameters.Add(new SqlParameter("@Login", login.Login));

                Connection.ExecutarComando(comando);
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("AlteraUltimaDataAcesso", "LoginDAO",e.StackTrace, Constantes.ATipoMetodo.UPDATE);
                throw;
            }
        }

        public LoginModel Salvar(LoginModel login)
        {
            try
            {
                AccessObject<LoginModel> AO = new AccessObject<LoginModel>();
                AO.CreateDataInsert();
//                var sql = @"INSERT INTO Login VALUES(
//                                @Login,
//                                @Senha,
//                                null
//                            )";
                var comando = new SqlCommand(AO.ReturnQuery(), Connection.GetConnection());
                comando.Parameters.AddWithValue("@Login", login.Login);
                comando.Parameters.AddWithValue("@Senha",Encript.EncriptMd5.Criptografar(login.Senha));
                
                Connection.ExecutarComando(comando);
                return RecuperaLogin(login);
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("GravarLogin", "LoginDAO", e.StackTrace, Constantes.ATipoMetodo.INSERT);
                return null;
            }
        }

        public bool Excluir(LoginModel login)
        {
            try
            {
                AccessObject<LoginModel> AO = new AccessObject<LoginModel>();
                AO.DeleteFromId();
                var comando = new SqlCommand(AO.ReturnQuery(),Connection.GetConnection());
                comando.Parameters.AddWithValue("@IdLogin",login.Id);

                Connection.ExecutarComando(comando);
                return true;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir","LoginDAO",e.StackTrace,Constantes.ATipoMetodo.DELETE);
                return false;
            }
        }

    }
}
