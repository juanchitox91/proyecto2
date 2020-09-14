using SGEA.Filters;
using System.Web.Mvc;
using SGEA.Repository;
using System.Collections.Generic;

namespace SGEA.Controllers
{
    [Autenticado]
    public class _MenuLateralController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            Dictionary<string, string> permisos = new Dictionary<string, string> { { "nombrePermiso", "SI" } };
            var user = Helper.SessionHelper.GetUser();
            ViewBag.Nombre = user.Nombre;
            return PartialView("~/views/shared/_MenuLateral.cshtml", permisos);
        }
    }
}