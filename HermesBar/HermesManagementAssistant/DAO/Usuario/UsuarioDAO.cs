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
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Login", login.Login);
                
                return Connection.getDataTable().Rows[0]["Email"].ToString();
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
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Senha", Encript.EncriptMd5.Criptografar(login.Senha));
                Connection.AddParameter("@Login", login.Login);

                return Connection.ExecutarComando();
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
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Login", login.Login);
                
                var dataTable = Connection.getDataTable();

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

                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Nome", "%" + usuario.Nome + "%");
                Connection.AddParameter("@Email", "%" + usuario.Email + "%");
                var dataTable = Connection.getDataTable();

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
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Nome", nome);
                
                if ((int)Connection.getDataTable().Rows[0]["Quantidade"] > 0)
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
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("IdLogin", usuario.Login.Id);
                Connection.AddParameter("@IdPerfil", usuario.Perfil.IdPerfil);
                Connection.AddParameter("@Nome", usuario.Nome);
                Connection.AddParameter("@Status", usuario.Status);
                Connection.AddParameter("@Email", usuario.Email);

                return Connection.ExecutarComando();
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
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Id_Usuario", usuario.IdUsuario);

                return Connection.ExecutarComando();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir","UsuarioDAO", e.StackTrace,Constantes.ATipoMetodo.DELETE);
                return false;
            }
        }
    }
}
