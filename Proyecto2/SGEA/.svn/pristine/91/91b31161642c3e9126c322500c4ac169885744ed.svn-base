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
    public class DocenteController : Controller
    {
        [Permiso(permiso = "verDocentes")]
        public ActionResult Index()
        {
            List<Docente> docentes = DocenteRepository.getDocentes(HttpContext.Session["institucion"].ToString());
            Session["docentes"] = docentes;
            ViewBag.Docentes = docentes;
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "crearDocente")]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [Permiso(permiso = "crearDocente")]
        public ActionResult Crear(Docente docente)
        {
            docente.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = DocenteRepository.createDocente(docente);
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
        [Permiso(permiso = "editarDocente")]
        public ActionResult Editar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Docente> docentes = (List<Docente>)Session["docentes"];
            Docente docente = docentes.Where(x => x.ID == longid).SingleOrDefault();
            return View(docente);
        }

        [HttpPost]
        [Permiso(permiso = "editarDocente")]
        public ActionResult Editar(Docente docente)
        {
            docente.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = DocenteRepository.updateDocente(docente);
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

        [Permiso(permiso = "verDetalleDocente")]
        public ActionResult VerDetalle(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Docente> docentes = (List<Docente>)Session["docentes"];
            Docente docente = docentes.Where(x => x.ID == longid).SingleOrDefault();
            return View(docente);
        }

        [Permiso(permiso = "eliminarDocente")]
        public ActionResult Eliminar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Docente> docentes = (List<Docente>)Session["docentes"];
            Docente docente = docentes.Where(x => x.ID == longid).SingleOrDefault();
            return View(docente);
        }

        [HttpPost]
        [Permiso(permiso = "eliminarDocente")]
        public ActionResult Eliminar(Docente docente)
        {
            var mensaje = DocenteRepository.deleteDocente(docente.ID);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "El docente se eliminó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }       
    }
}