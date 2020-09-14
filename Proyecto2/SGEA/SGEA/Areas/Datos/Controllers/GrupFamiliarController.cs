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
    public class GrupoFamiliarController : Controller
    {
        [Permiso(permiso = "verGrupoFamiliar")]
        public ActionResult Index()
        {
            List<GrupoFamiliar> grupos = GrupoFamiliarRepository.getGruposFamiliares(HttpContext.Session["institucion"].ToString());
            Session["grupos"] = grupos;
            ViewBag.Grupos = grupos;
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "crearGrupoFamiliar")]
        public ActionResult Crear()
        {
            return View();

        }

        [HttpPost]
        [Permiso(permiso = "crearGrupoFamiliar")]
        public ActionResult Crear(GrupoFamiliar grupo)
        {
            grupo.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = GrupoFamiliarRepository.createGrupoFamiliar(grupo);
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
        [Permiso(permiso = "editarGrupoFamiliar")]
        public ActionResult Editar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<GrupoFamiliar> grupos = (List<GrupoFamiliar>)Session["grupos"];
            GrupoFamiliar grupo = grupos.Where(x => x.ID == longid).SingleOrDefault();
            return View(grupo);
        }

        [HttpPost]
        [Permiso(permiso = "editarGrupoFamiliar")]
        public ActionResult Editar(GrupoFamiliar grupo)
        {
            grupo.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = GrupoFamiliarRepository.updateGrupoFamiliar(grupo);
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

        [Permiso(permiso = "verDetalleGrupoFamiliar")]
        public ActionResult VerDetalle(string id)
        {
            var longid = Convert.ToInt64(id);
            List<GrupoFamiliar> grupos = (List<GrupoFamiliar>)Session["grupos"];
            GrupoFamiliar grupo = grupos.Where(x => x.ID == longid).SingleOrDefault();
            return View(grupo);
        }

        [Permiso(permiso = "eliminarGrupoFamiliar")]
        public ActionResult Eliminar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<GrupoFamiliar> grupos = (List<GrupoFamiliar>)Session["grupos"];
            GrupoFamiliar grupo = grupos.Where(x => x.ID == longid).SingleOrDefault();
            return View(grupo);
        }

        [HttpPost]
        [Permiso(permiso = "eliminarGrupoFamiliar")]
        public ActionResult Eliminar(GrupoFamiliar grupo)
        {
            var mensaje = GrupoFamiliarRepository.deleteGrupoFamiliar(grupo.ID);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "El Grupo Familiar se eliminó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }       
    }
}