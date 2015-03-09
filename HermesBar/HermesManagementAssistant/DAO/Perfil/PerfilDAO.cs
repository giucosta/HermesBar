using DAO.Connections;
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

namespace DAO.Perfil
{
    public class PerfilDAO
    {
        public PerfilModel RecuperaPerfil(LoginModel login)
        {
            try
            {
                AccessObject<LoginModel> AO = new AccessObject<LoginModel>();
                AO.CreateSpecificQuery(@"SELECT P.Id_Perfil, P.Perfil FROM Usuario U INNER JOIN Perfil P ON P.Id_Perfil = U.Id_Perfil INNER JOIN Login L ON L.Id_Login = U.Id_Login WHERE L.Login = @Login");
                AO.GetCommand();
                AO.InsertParameter("Login", login.Login);

                var dataTable = AO.GetDataTable();

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
                AccessObject<PerfilModel> AO = new AccessObject<PerfilModel>();
                AO.CreateSelectWithSimpleParameter("Perfil");
                AO.GetCommand();
                var dataTable = AO.GetDataTable();

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
                AccessObject<PerfilModel> AO = new AccessObject<PerfilModel>();
                AO.CreateSelectWithSimpleParameter("Id_Perfil");
                AO.InsertParameter(ConstantesDAO.WHERE, "Perfil",ConstantesDAO.EQUAL,perfil.Perfil);
                AO.GetCommand();

                var dataTable = AO.GetDataTable();
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
