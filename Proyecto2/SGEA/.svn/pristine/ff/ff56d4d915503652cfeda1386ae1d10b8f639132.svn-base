using SGEA.Filters;
using System.Web.Mvc;
using SGEA.Repository;
using System.Collections.Generic;
using SGEA.Models;
using System.Linq;
using System;

namespace SGEA.Areas.Educativo.Controllers
{
    [Autenticado]
    public class ItemPlanillaController : Controller
    {
        [Permiso(permiso = "verItemPlanillas")]
        public ActionResult Index()
        {
            List<ItemPlanilla> items = ItemPlanillaRepository.getItemPlanillas(HttpContext.Session["institucion"].ToString());
            Session["items"] = items;
            ViewBag.ItemPlanillas = items;
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "crearItemPlanilla")]
        public ActionResult Crear()
        {
            ViewBag.tipositem = ObtenerTiposItemSelect("0");
            ViewBag.subunidades = ObtenerSubUnidadesSelect("0");

            return View();
        }

        [HttpPost]
        [Permiso(permiso = "crearItemPlanilla")]
        public ActionResult Crear(ItemPlanilla unidad)
        {
            unidad.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = ItemPlanillaRepository.createItemPlanilla(unidad);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La carga se realizó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            ViewBag.tipositem = ObtenerTiposItemSelect("0");
            ViewBag.subunidades = ObtenerSubUnidadesSelect("0");

            return View();
        }

        [HttpGet]
        [Permiso(permiso = "editarItemPlanilla")]
        public ActionResult Editar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<ItemPlanilla> items = (List<ItemPlanilla>)Session["items"];
            ItemPlanilla item = items.Where(x => x.ID == longid).SingleOrDefault();

            ViewBag.tipositem = ObtenerTiposItemSelect(item.TipoItemID.ToString());
            ViewBag.subunidades = ObtenerSubUnidadesSelect(item.SubUnidadID.ToString());

            return View(item);
        }

        [HttpPost]
        [Permiso(permiso = "editarItemPlanilla")]
        public ActionResult Editar(ItemPlanilla unidad)
        {
            unidad.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = ItemPlanillaRepository.updateItemPlanilla(unidad);
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

        [Permiso(permiso = "verDetalleItemPlanilla")]
        public ActionResult VerDetalle(string id)
        {
            var longid = Convert.ToInt64(id);
            List<ItemPlanilla> items = (List<ItemPlanilla>)Session["items"];
            ItemPlanilla item = items.Where(x => x.ID == longid).SingleOrDefault();
            return View(item);
        }

        [Permiso(permiso = "eliminarItemPlanilla")]
        public ActionResult Eliminar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<ItemPlanilla> items = (List<ItemPlanilla>)Session["items"];
            ItemPlanilla item = items.Where(x => x.ID == longid).SingleOrDefault();
            return View(item);
        }

        [HttpPost]
        [Permiso(permiso = "eliminarItemPlanilla")]
        public ActionResult Eliminar(ItemPlanilla item)
        {
            var mensaje = ItemPlanillaRepository.deleteItemPlanilla(item.ID);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La unidad se eliminó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }

        public List<SelectListItem> ObtenerTiposItemSelect(string id)
        {
            var result = ItemPlanillaRepository.getTiposItemSelect2(HttpContext.Session["institucion"].ToString(), id);
            return result;
        }

        public List<SelectListItem> ObtenerSubUnidadesSelect(string id)
        {
            var result = SubUnidadRepository.getSubUnidadesSelect2(HttpContext.Session["institucion"].ToString(), id);
            return result;
        }
    }
}