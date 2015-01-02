using DAO.Connections;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var comando = new SqlCommand(sql, Connection.getConnection());
                comando.Parameters.Add(new SqlParameter("@Login", login.Login));

                var dataTable = Connection.getDataTable(comando);

                if (dataTable.Rows.Count == 0)
                    return null;

                return new PerfilModel()
                {
                    IdPerfil = (int)dataTable.Rows[0]["Id_Perfil"],
                    Perfil = dataTable.Rows[0]["Perfil"].ToString()
                }; ;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
