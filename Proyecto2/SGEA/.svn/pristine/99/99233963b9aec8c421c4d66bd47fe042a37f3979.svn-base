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
    public class MateriaController : Controller
    {
        [Permiso(permiso = "verMaterias")]
        public ActionResult Index()
        {
            List<Materia> materias = MateriaRepository.getMaterias(HttpContext.Session["institucion"].ToString());
            Session["materias"] = materias;
            ViewBag.Materias = materias;
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "crearMateria")]
        public ActionResult Crear()
        {
            return View();

        }

        [HttpPost]
        [Permiso(permiso = "crearMateria")]
        public ActionResult Crear(Materia materia)
        {
            materia.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = MateriaRepository.createMateria(materia);
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
        [Permiso(permiso = "editarMateria")]
        public ActionResult Editar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Materia> materias = (List<Materia>)Session["materias"];
            Materia materia = materias.Where(x => x.ID == longid).SingleOrDefault();
            return View(materia);
        }

        [HttpPost]
        [Permiso(permiso = "editarMateria")]
        public ActionResult Editar(Materia materia)
        {
            materia.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = MateriaRepository.updateMateria(materia);
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

        [Permiso(permiso = "verDetalleMateria")]
        public ActionResult VerDetalle(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Materia> materias = (List<Materia>)Session["materias"];
            Materia materia = materias.Where(x => x.ID == longid).SingleOrDefault();
            return View(materia);
        }

        [Permiso(permiso = "eliminarMateria")]
        public ActionResult Eliminar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Materia> materias = (List<Materia>)Session["materias"];
            Materia materia = materias.Where(x => x.ID == longid).SingleOrDefault();
            return View(materia);
        }

        [HttpPost]
        [Permiso(permiso = "eliminarMateria")]
        public ActionResult Eliminar(Materia materia)
        {
            var mensaje = MateriaRepository.deleteMateria(materia.ID);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La materia se eliminó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }       
    }
}