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
using UTILS;
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
                AO.GetTransaction();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Login", ConstantesDAO.EQUAL, login.Login);

                var loginDataTable = AO.GetDataTable();
                if (loginDataTable.Rows.Count > 0)
                {
                    if (login.Senha.Equals(Encript.EncriptMd5.Descriptografar(loginDataTable.Rows[0]["Senha"].ToString())))
                    {
                        AlteraUltimaDataAcesso(login);
                        AO.Commit();
                        return true;
                    }
                }
                AO.Rollback();
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
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Login", ConstantesDAO.EQUAL, login.Login);
                
                var dataTable = AO.GetDataTable();

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
                AO.CreateUpdate("DataUltimoLogin", "Login");
                AO.GetCommand();
                AO.InsertParameter("DataUltimoLogin", DateTime.Now);
                AO.InsertParameter("Login", login.Login);
                AO.ExecuteCommand();
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
                AO.GetCommand();
                AO.InsertParameter("Login", login.Login);
                AO.InsertParameter("Senha", Encript.EncriptMd5.Criptografar(login.Senha));
                AO.ExecuteCommand();
                return RecuperaLogin(login);
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("GravarLogin", "LoginDAO", e.StackTrace, Constantes.ATipoMetodo.INSERT);
                Connection.Rollback();
                return null;
            }
        }

        public bool Excluir(LoginModel login)
        {
            try
            {
                AccessObject<LoginModel> AO = new AccessObject<LoginModel>();
                AO.DeleteFromId();
                AO.GetCommand();
                AO.InsertParameter("IdLogin", login.Id);
                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir","LoginDAO",e.StackTrace,Constantes.ATipoMetodo.DELETE);
                return false;
            }
        }
    }
}
