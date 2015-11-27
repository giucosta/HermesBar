using MODEL.User;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB2.UTIL
{
    public static class GetSession
    {
        public static void GetUserSession(ref UsuarioModel usuario)
        {
            usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["USR"];
        }
    }
}