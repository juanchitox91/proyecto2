﻿using SGEA.Filters;
using System.Web.Mvc;
using SGEA.Repository;
using System.Collections.Generic;
using SGEA.Models;
using System.Linq;
using System;
using SGEA.Reportes;
using System.IO;

namespace SGEA.Areas.Educativo.Controllers
{
    [Autenticado]
    public class PlanillaController : Controller
    {
        [Permiso(permiso = "verPlanillas")]
        public ActionResult Index()
        {
            List<Planilla> planillas = PlanillaRepository.getPlanillas(HttpContext.Session["institucion"].ToString());
            Session["planillas"] = planillas;
            ViewBag.Planillas = planillas;
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "crearPlanilla")]
        public ActionResult Crear()
        {
            ViewBag.materias = ObtenerMateriasSelect("0");
            ViewBag.docentes = ObtenerDocentesSelect("0");
            ViewBag.cursos = ObtenerCursosSelect("0");

            return View();
        }

        [HttpPost]
        [Permiso(permiso = "crearPlanilla")]
        public ActionResult Crear(Planilla planilla)
        {
            planilla.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = PlanillaRepository.createPlanilla(planilla);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La carga se realizó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            ViewBag.materias = ObtenerMateriasSelect(planilla.MateriaID.ToString());
            ViewBag.docentes = ObtenerDocentesSelect(planilla.DocenteID.ToString());
            ViewBag.cursos = ObtenerCursosSelect(planilla.CursoID.ToString());

            return View();
        }

        [HttpGet]
        [Permiso(permiso = "editarPlanilla")]
        public ActionResult Editar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Planilla> planillas = (List<Planilla>)Session["planillas"];
            Planilla planilla = planillas.Where(x => x.ID == longid).SingleOrDefault();

            ViewBag.materias = ObtenerMateriasSelect(planilla.MateriaID.ToString());
            ViewBag.docentes = ObtenerDocentesSelect(planilla.DocenteID.ToString());
            ViewBag.cursos = ObtenerCursosSelect(planilla.CursoID.ToString());

            return View(planilla);
        }

        [HttpPost]
        [Permiso(permiso = "editarPlanilla")]
        public ActionResult Editar(Planilla planilla)
        {
            planilla.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = PlanillaRepository.updatePlanilla(planilla);
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

        [Permiso(permiso = "verDetallePlanilla")]
        public ActionResult VerDetalle(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Planilla> planillas = (List<Planilla>)Session["planillas"];
            Planilla planilla = planillas.Where(x => x.ID == longid).SingleOrDefault();
            return View(planilla);
        }

        [Permiso(permiso = "eliminarPlanilla")]
        public ActionResult Eliminar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Planilla> planillas = (List<Planilla>)Session["planillas"];
            Planilla planilla = planillas.Where(x => x.ID == longid).SingleOrDefault();
            return View(planilla);
        }

        [HttpPost]
        [Permiso(permiso = "eliminarPlanilla")]
        public ActionResult Eliminar(Planilla planilla)
        {
            var mensaje = PlanillaRepository.deletePlanilla(planilla.ID);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La" +
                    " planilla se eliminó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }

        public List<SelectListItem> ObtenerCursosSelect(string id)

        {
            var result = CursoRepository.getCursosSelect2(HttpContext.Session["institucion"].ToString(), id);
            return result;
        }

        public List<SelectListItem> ObtenerMateriasSelect(string id)

        {
            var result = MateriaRepository.getMateriaSelect2(HttpContext.Session["institucion"].ToString(), id);
            return result;
        }

        public List<SelectListItem> ObtenerDocentesSelect(string id)

        {
            var result = DocenteRepository.getDocentesSelect2(HttpContext.Session["institucion"].ToString(), id);
            return result;
        }

        [Permiso(permiso = "verProgramaEstudio")]
        public ActionResult Reporte()
        {
            List<Planilla> planillas = PlanillaRepository.getPlanillas(HttpContext.Session["institucion"].ToString());
            ViewBag.Planillas = planillas;
            return View();
        }

        [Permiso(permiso = "verProgramaEstudio")]
        public ActionResult ReportePDF(string idPlanilla)
        {
            List<ProgramaEstudio> lista = PlanillaRepository.getProgramaEstudio(idPlanilla);

            ProgramaEstudioReport rpt = new ProgramaEstudioReport();
            rpt.Load();
            rpt.SetDataSource(lista);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);


            return File(s, "application/pdf");
        }

    }
}