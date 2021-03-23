using SGEA.Filters;
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
    public class EntrevistaController : Controller
    {
        [Permiso(permiso = "verEntrevistas")]
        public ActionResult Index()
        {
            ViewBag.cursos = ObtenerCursosSelect("0");

            return View();
        }

        [Permiso(permiso = "verEntrevistas")]
        public ActionResult Reporte()
        {
            ViewBag.cursos = ObtenerCursosSelect("0");
            
            return View();
        }

        [Permiso(permiso = "verEntrevistas")]
        public ActionResult ReportePDF(string idAlumno)
        {
            List<Entrevista> lista = EntrevistaRepository.getEntrevistasReporte(idAlumno);

            EntrevistasReport rpt = new EntrevistasReport();
            rpt.Load();
            rpt.SetDataSource(lista);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);


            return File(s, "application/pdf");
        }


        [HttpPost]
        public JsonResult PoblarGrilla(DataTableAjaxPostModel model)
        {
            List<Entrevista> lista = new List<Entrevista>();

            try
            {
                lista = EntrevistaRepository.getEntrevistas(model.param1);
            }
            catch { }

            Session["entrevistas"] = lista;
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = 100,
                recordsFiltered = 100,
                data = lista
            });
        }

        [HttpPost]
        public JsonResult PoblarGrillaEntrevista(DataTableAjaxPostModel model)
        {
            List<Alumno> lista = new List<Alumno>();

            try
            {
                lista = EntrevistaRepository.getAlumnosReporteEntrevistas(model.param1);
            }
            catch { }

            Session["alumnosEntrevista"] = lista;
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = 100,
                recordsFiltered = 100,
                data = lista
            });
        }

        public ActionResult CargarAlumnos(string idcurso)
        {
            Dictionary<string, string> respuesta = new Dictionary<string, string>();

            List<SelectListItem> alumnos = EntrevistaRepository.getAlumnosSelect2(HttpContext.Session["institucion"].ToString(), idcurso, "0");

            foreach (var item in alumnos)
            {
                respuesta.Add(item.Value, item.Text);
            }

            return Json(new { resultado = respuesta.OrderBy(x => x.Key) });
        }


        [HttpGet]
        [Permiso(permiso = "crearEntrevista")]
        public ActionResult Crear()
        {
            ViewBag.Cursos = ObtenerCursosSelect("0");
            ViewBag.Alumnos = new List<SelectListItem>();

            return View();
        }

        [HttpPost]
        [Permiso(permiso = "crearEntrevista")]
        public ActionResult Crear(Entrevista entrevista)
        {
            //entrevista.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = EntrevistaRepository.createEntrevista(entrevista);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La carga se realizó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            ViewBag.Cursos = ObtenerCursosSelect(entrevista.CursoID.ToString());
            ViewBag.Alumnos = EntrevistaRepository.getAlumnosSelect2(HttpContext.Session["institucion"].ToString(), entrevista.CursoID.ToString(), entrevista.InscripcionID.ToString());
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "editarEntrevista")]
        public ActionResult Editar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Entrevista> entrevistas = (List<Entrevista>)Session["entrevistas"];
            Entrevista entrevista = entrevistas.Where(x => x.ID == longid).SingleOrDefault();

            //ViewBag.materias = ObtenerMateriasSelect(planilla.MateriaID.ToString());
            //ViewBag.docentes = ObtenerDocentesSelect(planilla.DocenteID.ToString());
            ViewBag.cursos = ObtenerCursosSelect(entrevista.CursoID.ToString());
            ViewBag.Alumnos = EntrevistaRepository.getAlumnosSelect2(HttpContext.Session["institucion"].ToString(), entrevista.CursoID.ToString(), entrevista.InscripcionID.ToString());

            return View(entrevista);
        }

        [HttpPost]
        [Permiso(permiso = "editarEntrevista")]
        public ActionResult Editar(Entrevista entrevista)
        {
            //entrevista. = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = EntrevistaRepository.updateEntrevista(entrevista);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La entrevista se editó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }

        [Permiso(permiso = "verDetalleEntrevista")]
        public ActionResult VerDetalle(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Entrevista> entrevistas = (List<Entrevista>)Session["entrevistas"];
            Entrevista entrevista = entrevistas.Where(x => x.ID == longid).SingleOrDefault();

            //ViewBag.materias = ObtenerMateriasSelect(planilla.MateriaID.ToString());
            //ViewBag.docentes = ObtenerDocentesSelect(planilla.DocenteID.ToString());
            ViewBag.cursos = ObtenerCursosSelect(entrevista.CursoID.ToString());
            ViewBag.Alumnos = EntrevistaRepository.getAlumnosSelect2(HttpContext.Session["institucion"].ToString(), entrevista.CursoID.ToString(), entrevista.InscripcionID.ToString());

            return View(entrevista);
        }

        public List<SelectListItem> ObtenerCursosSelect(string id)

        {
            var result = CursoRepository.getCursosSelect2(HttpContext.Session["institucion"].ToString(), id);
            return result;
        }
      
    }
}