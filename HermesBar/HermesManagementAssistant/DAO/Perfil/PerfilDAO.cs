using DAO.Connections;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace DAO.Perfil
{
    public class PerfilDAO
    {
        public PerfilModel RecuperaPerfil(LoginModel login)
        {
            try
            {
                var sql = @"SELECT 
                                P.Id_Perfil,
                                P.Perfil 
                            FROM Usuario U 
                            INNER JOIN Perfil P ON P.Id_Perfil = U.Id_Perfil
                            INNER JOIN Login L ON L.Id_Login = U.Id_Login
                            WHERE L.Login = @Login";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Login", login.Login);
                
                var dataTable = Connection.getDataTable(comando);

                if (dataTable.Rows.Count == 0)
                    return null;

                return new PerfilModel()
                {
                    IdPerfil = (int)dataTable.Rows[0]["Id_Perfil"],
                    Perfil = dataTable.Rows[0]["Perfil"].ToString()
                }; ;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaPerfil", "PerfilDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }

        public List<String> RecuperaTodosPerfil()
        {
            try
            {
                var sql = "SELECT Perfil FROM Perfil";
                var comando = new SqlCommand(sql, Connection.GetConnection());

                var dataTable = Connection.getDataTable(comando);

                var lista = new List<String>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                    lista.Add(dataTable.Rows[i]["Perfil"].ToString());

                    return lista;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaTodosPerfil","PerfilDAO",e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }

        public int RecuperaIdPerfil(PerfilModel perfil)
        {
            try
            {
                var sql = @"SELECT Id_Perfil FROM Perfil WHERE Perfil = @Perfil";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Perfil",perfil.Perfil);
                
                var dataTable = Connection.getDataTable(comando);
                return (int)dataTable.Rows[0]["Id_Perfil"];
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RecuperaIdPerfil","PerfilDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
    }
}
