using SGEA.Filters;
using System.Web.Mvc;
using SGEA.Repository;
using System.Collections.Generic;
using SGEA.Models;
using System.Linq;
using System;

namespace SGEA.Areas.Administrador.Controllers
{
    [Autenticado]
    public class UsuarioController : Controller
    {
        [Permiso(permiso = "verUsuarios")]
        public ActionResult Index()
        {
            List<Usuario> user = UsuarioRepository.getUsuarios(HttpContext.Session["institucion"].ToString());
            Session["usuarios"] = user;
            ViewBag.Usuarios = user;
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "crearUsuario")]
        public ActionResult Crear()
        {
            ViewBag.roles = ObtenerRolesSelect("0");
            return View();

        }

        [HttpPost]
        [Permiso(permiso = "crearUsuario")]
        public ActionResult Crear(Usuario user)
        {
            user.IDInstitucion = Convert.ToInt64( HttpContext.Session["institucion"].ToString());
            var mensaje = UsuarioRepository.createUsuario(user);
            if(mensaje == "OK")
            {
                ViewBag.mensaje = "La carga se realizó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }
            ViewBag.roles = ObtenerRolesSelect(user.IDRol.ToString());

            return View();
        }

        [HttpGet]
        [Permiso(permiso = "editarUsuario")]
        public ActionResult Editar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Usuario> usuarios = (List<Usuario>)Session["usuarios"];
            Usuario user = usuarios.Where(x => x.ID == longid).SingleOrDefault();
            ViewBag.roles = ObtenerRolesSelect(user.IDRol.ToString());
            return View(user);
        }

        [HttpPost]
        [Permiso(permiso = "editarUsuario")]
        public ActionResult Editar(Usuario user)
        {
            user.IDInstitucion = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = UsuarioRepository.updateUsuario(user);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La carga se realizó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }

        [Permiso(permiso = "detalleUsuario")]
        public ActionResult VerDetalle(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Usuario> usuarios = (List<Usuario>)Session["usuarios"];
            Usuario user = usuarios.Where(x => x.ID == longid ).SingleOrDefault();
            ViewBag.roles = ObtenerRolesSelect(user.IDRol.ToString());
            return View(user);
        }

        [Permiso(permiso = "eliminarUsuario")]
        public ActionResult Eliminar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Usuario> usuarios = (List<Usuario>)Session["usuarios"];
            Usuario user = usuarios.Where(x => x.ID == longid).SingleOrDefault();
            ViewBag.roles = ObtenerRolesSelect(user.IDRol.ToString());
            return View(user);
        }

        [HttpPost]
        [Permiso(permiso = "eliminarUsuario")]
        public ActionResult Eliminar(Usuario user)
        {
            user.IDInstitucion = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = UsuarioRepository.deleteUsuario(user.ID);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La carga se realizó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }
            ViewBag.roles = ObtenerRolesSelect("0");

            return RedirectToAction("Index");
        }

        public List<SelectListItem> ObtenerRolesSelect(string id)

        {
            var result = RolRepository.getRolesSelect2(HttpContext.Session["institucion"].ToString(), id);
            return result;
        }


    }
}