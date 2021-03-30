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
    public class AlumnoController : Controller
    {
        [Permiso(permiso = "verAlumnos")]
        public ActionResult Index()
        {
            List<Alumno> alumnos = AlumnoRepository.getAlumnos(HttpContext.Session["institucion"].ToString());
            Session["alumnos"] = alumnos;
            ViewBag.Alumnos = alumnos;
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "crearAlumno")]
        public ActionResult Crear()
        {
            return View();

        }

        [HttpPost]
        [Permiso(permiso = "crearAlumno")]
        public ActionResult Crear(Alumno alumno)
        {
            alumno.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = AlumnoRepository.createAlumno(alumno);
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
        [Permiso(permiso = "editarAlumno")]
        public ActionResult Editar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Alumno> alumnos = (List<Alumno>)Session["alumnos"];
            Alumno alumno = alumnos.Where(x => x.ID == longid).SingleOrDefault();
            return View(alumno);
        }

        [HttpPost]
        [Permiso(permiso = "editarAlumno")]
        public ActionResult Editar(Alumno alumno)
        {
            alumno.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = AlumnoRepository.updateAlumno(alumno);
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

        [Permiso(permiso = "detalleAlumno")]
        public ActionResult VerDetalle(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Alumno> alumnos = (List<Alumno>)Session["alumnos"];
            Alumno alumno = alumnos.Where(x => x.ID == longid).SingleOrDefault();
            return View(alumno);
        }

        [Permiso(permiso = "eliminarAlumno")]
        public ActionResult Eliminar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Alumno> alumnos = (List<Alumno>)Session["alumnos"];
            Alumno alumno = alumnos.Where(x => x.ID == longid).SingleOrDefault();
            return View(alumno);
        }

        [HttpPost]
        [Permiso(permiso = "eliminarAlumno")]
        public ActionResult Eliminar(Alumno alumno)
        {
            var mensaje = AlumnoRepository.deleteAlumno(alumno.ID);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "El alumno se eliminó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }

    }
}