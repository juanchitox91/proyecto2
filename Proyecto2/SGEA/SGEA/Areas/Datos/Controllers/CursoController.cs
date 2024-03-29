﻿using SGEA.Filters;
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
    public class CursoController : Controller
    {
        [Permiso(permiso = "verCursos")]
        public ActionResult Index()
        {
            List<Curso> cursos = CursoRepository.getCursos(HttpContext.Session["institucion"].ToString());
            Session["cursos"] = cursos;
            ViewBag.Cursos = cursos;
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "crearCurso")]
        public ActionResult Crear()
        {
            ViewBag.turnos = ObtenerTurnos("0");
            return View();

        }

        [HttpPost]
        [Permiso(permiso = "crearCurso")]
        public ActionResult Crear(Curso curso)
        {
            curso.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = CursoRepository.createCurso(curso);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La carga se realizó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            ViewBag.turnos = ObtenerTurnos(((int)curso.Turno).ToString());

            return View();
        }

        [HttpGet]
        [Permiso(permiso = "editarCurso")]
        public ActionResult Editar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Curso> cursos = (List<Curso>)Session["cursos"];
            Curso curso = cursos.Where(x => x.ID == longid).SingleOrDefault();
            ViewBag.turnos = ObtenerTurnos(((int)curso.Turno).ToString());
            return View(curso);
        }

        [HttpPost]
        [Permiso(permiso = "editarCurso")]
        public ActionResult Editar(Curso curso)
        {
            curso.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = CursoRepository.updateCurso(curso);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La carga se editó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            ViewBag.turnos = ObtenerTurnos(((int)curso.Turno).ToString());

            return RedirectToAction("Index");
        }

        [Permiso(permiso = "detalleCurso")]
        public ActionResult VerDetalle(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Curso> cursos = (List<Curso>)Session["cursos"];
            Curso curso = cursos.Where(x => x.ID == longid).SingleOrDefault();
            return View(curso);
        }

        [Permiso(permiso = "eliminarCurso")]
        public ActionResult Eliminar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Curso> cursos = (List<Curso>)Session["cursos"];
            Curso curso = cursos.Where(x => x.ID == longid).SingleOrDefault();
            return View(curso);
        }

        [HttpPost]
        [Permiso(permiso = "eliminarCurso")]
        public ActionResult Eliminar(Curso curso)
        {
            var mensaje = CursoRepository.deleteCurso(curso.ID);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "El curso se eliminó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }

        public List<SelectListItem> ObtenerTurnos(string id)
        {
            var result = CursoRepository.getTurnosSelect2(HttpContext.Session["institucion"].ToString(), id);
            return result;
        }
    }
}