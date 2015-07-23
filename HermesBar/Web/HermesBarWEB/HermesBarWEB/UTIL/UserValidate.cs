using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.UTIL
{

    public class HmaAuthorize : AuthorizeAttribute
    {
        public int[] Perfil { get; set; }
        public bool NoValidation { get; set; }

        public HmaAuthorize(params int[] Perfil)
        {
            this.Perfil = Perfil;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!NoValidation)
            {
                if (VerifySession())
                    return false;

                var authorized = base.AuthorizeCore(httpContext);
                if (!authorized)
                    return false;

                string previlege;

                previlege = string.Join("", GetUserRights());
                foreach (var p in Perfil)
                    if (previlege.Equals(p.ToString()))
                        return true;

                return false;
            }
            return true;
        }
        private string GetUserRights()
        {
            return ((UsuarioModel)HttpContext.Current.Session["USR"]).PerfilId.ToString();
        }
        private bool VerifySession()
        {
            return HttpContext.Current.Session["USR"] == null;
        }
    }
    public class PerfilAuthorize
    {
        public enum Perfil
        {
            Administrador = 1
        }
    }
}