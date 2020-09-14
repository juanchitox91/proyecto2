using SGEA.Filters;
using System.Web.Mvc;
using SGEA.Repository;
using System.Collections.Generic;
using SGEA.Models;
using System.Linq;
using System;
using Helper;

namespace SGEA.Areas.Datos.Controllers
{
    [Autenticado]
    public class ArancelController : Controller
    {
        [Permiso(permiso = "verAranceles")]
        public ActionResult Index()
        {
            List<Arancel> aranceles = ArancelRepository.getAranceles(HttpContext.Session["institucion"].ToString());
            Session["aranceles"] = aranceles;
            ViewBag.Aranceles = aranceles;
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "crearArancel")]
        public ActionResult Crear()
        {
            return View();

        }

        [HttpPost]
        [Permiso(permiso = "crearArancel")]
        public ActionResult Crear(Arancel arancel)
        {
            arancel.InstitucionId = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = ArancelRepository.createArancel(arancel);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La carga se realizó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "editarArancel")]
        public ActionResult Editar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Arancel> aranceles = (List<Arancel>)Session["aranceles"];
            Arancel arancel = aranceles.Where(x => x.ID == longid).SingleOrDefault();
            return View(arancel);
        }

        [HttpPost]
        [Permiso(permiso = "editarArancel")]
        public ActionResult Editar(Arancel arancel)
        {
            arancel.InstitucionId = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = ArancelRepository.updateArancel(arancel);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La carga se editó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }

        [Permiso(permiso = "verDetalleArancel")]
        public ActionResult VerDetalle(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Arancel> aranceles = (List<Arancel>)Session["aranceles"];
            Arancel arancel = aranceles.Where(x => x.ID == longid).SingleOrDefault();
            return View(arancel);
        }

        [Permiso(permiso = "eliminarArancel")]
        public ActionResult Eliminar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Arancel> aranceles = (List<Arancel>)Session["aranceles"];
            Arancel arancel = aranceles.Where(x => x.ID == longid).SingleOrDefault();
            return View(arancel);
        }

        [HttpPost]
        [Permiso(permiso = "eliminarArancel")]
        public ActionResult Eliminar(Alumno alumno)
        {
            var mensaje = ArancelRepository.deleteArancel(alumno.ID);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "El arancel se eliminó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }       
    }
}