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
    public class TutorController : Controller
    {
        [Permiso(permiso = "verTutores")]
        public ActionResult Index()
        {
            List<Tutor> Tutores = TutorRepository.getTutores(HttpContext.Session["institucion"].ToString());
            Session["Tutores"] = Tutores;
            ViewBag.Tutores = Tutores;
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "crearTutor")]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [Permiso(permiso = "crearTutor")]
        public ActionResult Crear(Tutor Tutor)
        {
            Tutor.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = TutorRepository.createTutor(Tutor);
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
        [Permiso(permiso = "editarTutor")]
        public ActionResult Editar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Tutor> Tutores = (List<Tutor>)Session["Tutores"];
            Tutor Tutor = Tutores.Where(x => x.ID == longid).SingleOrDefault();
            return View(Tutor);
        }

        [HttpPost]
        [Permiso(permiso = "editarTutor")]
        public ActionResult Editar(Tutor Tutor)
        {
            Tutor.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = TutorRepository.updateTutor(Tutor);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "El tutor se editó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }

        [Permiso(permiso = "detalleTutor")]
        public ActionResult VerDetalle(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Tutor> Tutores = (List<Tutor>)Session["Tutores"];
            Tutor Tutor = Tutores.Where(x => x.ID == longid).SingleOrDefault();
            return View(Tutor);
        }

        [Permiso(permiso = "eliminarTutor")]
        public ActionResult Eliminar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Tutor> Tutores = (List<Tutor>)Session["Tutores"];
            Tutor Tutor = Tutores.Where(x => x.ID == longid).SingleOrDefault();
            return View(Tutor);
        }

        [HttpPost]
        [Permiso(permiso = "eliminarTutor")]
        public ActionResult Eliminar(Tutor Tutor)
        {
            var mensaje = TutorRepository.deleteTutor(Tutor.ID);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "El Tutor se eliminó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }       
    }
}