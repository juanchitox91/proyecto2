using SGEA.Filters;
using System.Web.Mvc;
using SGEA.Repository;
using System.Collections.Generic;
using SGEA.Models;
using System.Linq;
using System;
using static SGEA.Models.DataTable;

namespace SGEA.Areas.Educativo.Controllers
{
    [Autenticado]
    public class EvaluacionController : Controller
    {
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
            
            foreach(var item in planillas)
            {
                respuesta.Add(item.Value, item.Text);
            }

            return Json(new { resultado = respuesta.OrderBy(x => x.Key) });
        }

        public JsonResult AgregarPuntajeMaximo(string idItemPlanilla)
        {
            int puntajemaximo = Convert.ToInt32(EvaluacionRepository.getPuntajeMaximo(idItemPlanilla))
                ;
            return Json(new { puntajemaximo = puntajemaximo });
        }

        /*public JsonResult CustomServerSideSearchAction(DataTableAjaxPostModel model)
        {
            int filteredResultsCount;
            int totalResultsCount;


            MainDBDataContext db = Models.DataMethods.GetDataContext();

            IQueryable<Entities> allEntities = db.Entities.Where(...);
            totalResultsCount = allEntities.Count();
            filteredResultsCount = allEntities.Count();

            var result = new List<YourCustomSearchClass>();

            foreach (var en in allEntities)
            {
                result.Add(new YourCustomSearchClass
                {
                    Name = en.Name
                });
            }

            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
            });
        }*/

        /*public ActionResult PoblarGrilla(string iditem)
        {
            List<Evaluacion> lista = new List<Evaluacion>();
            try
            {
                lista = EvaluacionRepository.getAlumnosEvaluar(iditem);
            }
            catch { }

            return Json(new { data = lista}, JsonRequestBehavior.AllowGet);
        }*/
    }
}