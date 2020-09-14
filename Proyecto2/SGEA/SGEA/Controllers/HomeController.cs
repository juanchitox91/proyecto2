using SGEA.Filters;
using System.Web.Mvc;
using SGEA.Repository;

namespace SGEA.Controllers
{
    [Autenticado]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var user = HomeRepository.getUsuario(HttpContext.Session["usuario"].ToString());
            //ViewBag.Nombre = user.Nombre;
            return View();
        }
    }
}