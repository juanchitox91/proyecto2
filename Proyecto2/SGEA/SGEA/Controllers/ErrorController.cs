using SGEA.Filters;
using System.Web.Mvc;

namespace SGEA.Controllers
{
    [Autenticado]
    public class ErrorController : Controller
    {
        public ActionResult AccesoDenegado()
        {
            ViewBag.NroError = "403";
            ViewBag.Titulo = "Acceso Denegado";
            ViewBag.Texto = "Lo sentimos, no tiene permiso para acceder a esta opcion";
            return View("Index");
        }

      
    }
}