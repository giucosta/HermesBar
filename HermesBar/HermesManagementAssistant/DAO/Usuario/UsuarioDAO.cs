using DAO.Abstract;
using DAO.Connections;
using DAO.Login;
using DAO.Perfil;
using DAO.Utils;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace DAO.Usuario
{
    public class UsuarioDAO : IUsuario
    {
        public string RecuperaEmailUsuario(LoginModel login)
        {
            try
            {
                AccessObject<LoginModel> AO = new AccessObject<LoginModel>();
                AO.CreateSpecificQuery(@"SELECT Email FROM Usuario U INNER JOIN Login L ON L.Id_Login = U.Id_Login WHERE L.Login = @Login");
                AO.GetCommand();
                AO.InsertParameter("Login", login.Login);

                return AO.GetDataTable().Rows[0]["Email"].ToString();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaEmailUsuario", "UsuarioDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }

        public bool GravaNovaSenha(LoginModel login)
        {
            try
            {
                AccessObject<LoginModel> AO = new AccessObject<LoginModel>();
                AO.CreateUpdate("Senha", "Login");
                AO.GetCommand();
                AO.InsertParameter("Senha", Encript.EncriptMd5.Criptografar(login.Senha));
                AO.InsertParameter("Login", login.Login);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("GravaNovaSenha","UsuarioDAO",e.StackTrace,Constantes.ATipoMetodo.UPDATE);
                throw;
            }
        }

        public UsuarioModel RetornaUsuario(LoginModel login)
        {
            try
            {
                AccessObject<LoginModel> AO = new AccessObject<LoginModel>();
                AO.CreateSpecificQuery(@"SELECT * FROM Usuario U INNER JOIN Login L ON L.Id_Login = U.Id_Login WHERE L.Login = @Login ");
                AO.GetCommand();
                AO.InsertParameter("Login", login.Login);

                var dataTable = AO.GetDataTable();

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
                AccessObject<UsuarioModel> AO = new AccessObject<UsuarioModel>();
                AO.CreateSpecificQuery(@"SELECT 
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
                                        AND Email LIKE @Email");

                AO.GetCommand();
                AO.InsertParameter("Nome", "%" + usuario.Nome + "%");
                AO.InsertParameter("Email", "%" + usuario.Email + "%");
                var dataTable = AO.GetDataTable();

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
                AccessObject<UsuarioModel> AO = new AccessObject<UsuarioModel>();
                AO.CreateSpecificQuery(@"SELECT Count(Nome) AS Quantidade FROM Usuario WHERE Nome = @Nome");
                AO.GetCommand();
                AO.InsertParameter("Nome", nome);
                
                if ((int)AO.GetDataTable().Rows[0]["Quantidade"] > 0)
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
                AccessObject<UsuarioModel> AO = new AccessObject<UsuarioModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("IdLogin", usuario.Login.Id);
                AO.InsertParameter("IdPerfil", usuario.Perfil.IdPerfil);
                AO.InsertParameter("Nome", usuario.Nome);
                AO.InsertParameter("Status", usuario.Status);
                AO.InsertParameter("Email", usuario.Email);

                return AO.ExecuteCommand();
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
                AccessObject<UsuarioModel> AO = new AccessObject<UsuarioModel>();
                AO.DeleteFromId();
                AO.GetCommand();
                AO.InsertParameter("Id_Usuario", usuario.IdUsuario);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir","UsuarioDAO", e.StackTrace,Constantes.ATipoMetodo.DELETE);
                return false;
            }
        }
    }
}
