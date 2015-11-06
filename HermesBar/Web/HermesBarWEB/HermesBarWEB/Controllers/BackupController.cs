using HELPER;
using HermesBarWCF;
using HermesBarWEB.UTIL;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HermesBarWEB.Controllers
{
    public class BackupController : Controller
    {
        private BackupService BackupService = Singleton<BackupService>.Instance();
        private UsuarioModel User;

        public BackupController()
        {
            GetSession.GetUserSession(ref this.User);
        }
        public ActionResult Get()
        {
            return View(BackupService.Get());
        }
        public ActionResult Restaurar(string Name)
        {
            try
            {
                BackupService.Restore(Name);
                ViewBag.BackupSuccess = true;
                return View("Get", BackupService.Get());
            }
            catch (Exception)
            {
                ViewBag.BackupError = true;
                return View("Get", BackupService.Get());
            }
        }
    }
}
