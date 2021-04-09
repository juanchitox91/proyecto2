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
    public class EvaluacionController : Controller
    {
        #region Evaluaciones
        [Permiso(permiso = "verEvaluaciones")]
        public ActionResult CargarEvaluacion()
        {
            List<SelectListItem> planillas = EvaluacionRepository.getPlanillasSelect2(HttpContext.Session["institucion"].ToString(), "0");
            ViewBag.Planillas = planillas;
            ViewBag.Items = new List<SelectListItem>();
            return View();
        }


        public ActionResult CargarItems(string idplanilla)
        {
            Dictionary<string, string> respuesta = new Dictionary<string, string>();

            List<SelectListItem> planillas = EvaluacionRepository.getItemsSelect2(HttpContext.Session["institucion"].ToString(), idplanilla, "0");

            foreach (var item in planillas)
            {
                respuesta.Add(item.Value, item.Text);
            }

            return Json(new { resultado = respuesta.OrderBy(x => x.Key) });
        }

        public JsonResult AgregarPuntajeMaximo(string idItemPlanilla)
        {
            Session["idItemEvaluacion"] = idItemPlanilla;
            int puntajemaximo = Convert.ToInt32(EvaluacionRepository.getPuntajeMaximo(idItemPlanilla)) ;
            Session["puntajeMaximo"] = puntajemaximo;
            return Json(new { puntajemaximo = puntajemaximo });
        }

        public JsonResult AgregarPuntaje(string puntaje, string cedula)
        {
            int puntajemaximo = (int)Session["puntajeMaximo"];
            try
            {
                if(puntajemaximo < Convert.ToInt32(puntaje))
                {
                    return Json(new { mensaje = "ERROR-PUNTAJE" });
                }
                string idItem = (string)Session["idItemEvaluacion"];

                EvaluacionRepository.insertPuntaje(idItem, cedula, puntaje);
                return Json(new { mensaje = "OK" });
            }
            catch(Exception ex)
            {
                string idItem = (string)Session["idItemEvaluacion"];

                EvaluacionRepository.insertPuntaje(idItem, cedula, puntaje);
                return Json(new { mensaje = "ERROR" });
            }
           
        }
        
        [HttpPost]
        public JsonResult PoblarGrilla(DataTableAjaxPostModel model)
        {
            List<Evaluacion> lista = new List<Evaluacion>();

            try
            {
                lista = EvaluacionRepository.getAlumnosEvaluar (model.param1);
            }
            catch { }


            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = 100,
                recordsFiltered = 100,
                data = lista
            });
        }

        #endregion

        #region Asistencias

        [Permiso(permiso = "verAsistencias")]
        public ActionResult CargarAsistencia()
        {
            Session["PlanillaAsistencia"] = null;
            List<SelectListItem> planillas = EvaluacionRepository.getPlanillasSelect2(HttpContext.Session["institucion"].ToString(), "0");
            ViewBag.Planillas = planillas;
            return View();
        }

        [HttpPost]
        public JsonResult PoblarGrillaAsistencia(DataTableAjaxPostModel model)
        {
            List<Asistencia> lista = new List<Asistencia>();

            try
            {
                lista = EvaluacionRepository.getAlumnosAsistencia(model.param1, model.param2);
                Session["PlanillaAsistencia"] = lista;
            }
            catch { }


            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = 100,
                recordsFiltered = 100,
                data = lista
            });
        }

        public JsonResult AgregarAsistencia(string idalumno, string presente)
        {
            List<Asistencia> lista = new List<Asistencia>(); 
            try
            {
                if (Session["PlanillaAsistencia"] != null)
                {
                    lista = (List<Asistencia>)Session["PlanillaAsistencia"];
                    lista.Where(x => x.AlumnoID.ToString() == idalumno).SingleOrDefault().Presente = presente.Equals("P");
                    Session["PlanillaAsistencia"] = lista;
                    return Json(new { mensaje = "OK" });
                }
            }
            catch (Exception ex)
            {
            }
            return Json(new { mensaje = "ERROR" });
        }

        public JsonResult ConfirmarAsistencia()
        {
            List<Asistencia> lista = new List<Asistencia>();
            try
            {
                if (Session["PlanillaAsistencia"] != null)
                {
                    lista = (List<Asistencia>)Session["PlanillaAsistencia"];
                    string mensaje = EvaluacionRepository.insertAsistencia(lista, HttpContext.Session["institucion"].ToString());
                    return Json(new { mensaje });
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { mensaje = "ERROR" });
        }
        #endregion

        #region Reportes

        [Permiso(permiso = "verAsistencias")]
        public ActionResult Reporte()
        {
            List<SelectListItem> planillas = EvaluacionRepository.getPlanillasSelect2(HttpContext.Session["institucion"].ToString(), "0");
            ViewBag.Planillas = planillas;

            return View();
        }

        [Permiso(permiso = "verAsistencias")]
        public ActionResult ReportePDF(string idPlanilla, string Fecha)
        {
            List<Asistencia> lista = EvaluacionRepository.getAlumnosAsistencia(idPlanilla, Fecha); ;

            AsistenciaReport rpt = new AsistenciaReport();
            rpt.Load();
            rpt.SetDataSource(lista);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);


            return File(s, "application/pdf");
        }
        #endregion
    }
}