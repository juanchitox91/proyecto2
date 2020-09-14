using SGEA.Filters;
using System.Web.Mvc;
using SGEA.Repository;
using System.Collections.Generic;
using SGEA.Models;
using System.Linq;
using System;
using Helper;

namespace SGEA.Areas.Administrador.Controllers
{
    [Autenticado]
    public class RolController : Controller
    {
        [Permiso(permiso = "verRoles")]
        public ActionResult Index()
        {
            List<Rol> Roles = RolRepository.getRoles(HttpContext.Session["institucion"].ToString());
            Session["roles"] = Roles;
            ViewBag.Roles = Roles;
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "crearRol")]
        public ActionResult Crear()
        {
            return View();

        }

        [HttpPost]
        [Permiso(permiso = "crearRol")]
        public ActionResult Crear(Rol rol)
        {
            rol.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = RolRepository.createRol(rol);
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
        [Permiso(permiso = "editarRol")]
        public ActionResult Editar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Rol> roles = (List<Rol>)Session["roles"];
            Rol rol = roles.Where(x => x.ID == longid).SingleOrDefault();
            return View(rol);
        }

        [HttpPost]
        [Permiso(permiso = "editarRol")]
        public ActionResult Editar(Rol rol)
        {
            rol.InstitucionID = Convert.ToInt64(HttpContext.Session["institucion"].ToString());
            var mensaje = RolRepository.updateRol(rol);
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

        [Permiso(permiso = "editarRol")]
        public ActionResult VerDetalle(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Rol> roles = (List<Rol>)Session["roles"];
            Rol rol = roles.Where(x => x.ID == longid).SingleOrDefault();
            return View(rol);
        }

        [Permiso(permiso = "eliminarRol")]
        public ActionResult Eliminar(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Rol> roles = (List<Rol>)Session["roles"];
            Rol rol = roles.Where(x => x.ID == longid).SingleOrDefault();
            return View(rol);
        }

        [HttpPost]
        [Permiso(permiso = "eliminarRol")]
        public ActionResult Eliminar(Rol rol)
        {
            var mensaje = UsuarioRepository.deleteUsuario(rol.ID);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "El rol se eliminó exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }

        [Permiso(permiso = "asigarAccion")]
        public ActionResult AsignarAccion(string id)
        {
            var longid = Convert.ToInt64(id);
            List<Rol> roles = (List<Rol>)Session["roles"];
            Rol rol = roles.Where(x => x.ID == longid).SingleOrDefault();
            Session["rol_accion"] = rol;

            List<Accion> acciones = RolRepository.getAccionesPorRoles(rol.ID);
            Session["acciones"] = acciones;
            ViewBag.acciones = acciones;
            ViewBag.rol = rol;

            return View();
        }

        [Permiso(permiso = "asigarAccion")]
        public ActionResult AgregarAccionAlRol(string nombre)
        {
            /*List<Rol> roles = (List<Rol>)Session["roles"];
            Rol rol = roles.Where(x => x.ID == longid).SingleOrDefault();*/
            Dictionary<string,string> respuesta = new Dictionary<string, string>();

            List<Accion> acciones = (List<Accion>)Session["acciones"];
            var accion = acciones.Where(x => x.NombreAccion == nombre).SingleOrDefault();
            acciones[acciones.FindIndex(x => x.NombreAccion == nombre)].Activo = !accion.Activo;

            //aca si agregamos el primer permiso para un grupo de acciones del mismo tipo, se debe habilitar por defecto 
            //la opcion de "ver Entidad"
            if (accion.Activo)
            {
                if( acciones.Select(x => x.ID).ToList().Contains(accion.Parent_id))
                {
                    //acciones.Where(x => x.ID == accion.Parent_id).SingleOrDefault().Activo = true;
                    if (!acciones.Where(x => x.ID == accion.Parent_id).SingleOrDefault().Activo)
                    {
                        acciones.Where(x => x.ID == accion.Parent_id).SingleOrDefault().Activo = true;
                        respuesta.Add(acciones.Where(x => x.ID == accion.Parent_id).SingleOrDefault().NombreAccion, "true"); 
                            //$"agregarAccion(\"{acciones.Where(x => x.ID == accion.Parent_id).SingleOrDefault().NombreAccion}\");";
                    }
                }
            }
            else
            {
                if (acciones.Select(x => x.Parent_id).ToList().Distinct().Contains(accion.ID))
                {
                    foreach (var item in acciones.Where(x => x.Parent_id == accion.ID))
                    {
                        if (item.Activo)
                        {
                            acciones.Where(x => x.ID == item.ID).SingleOrDefault().Activo = false;
                            respuesta.Add(acciones.Where(x => x.ID == item.ID).SingleOrDefault().NombreAccion, "false");
                            //respuesta += $" agregarAccion(\"{acciones.Where(x => x.ID == item.ID).SingleOrDefault().NombreAccion}\");";
                        }
                    }
                }
            }

            Session["acciones"] = acciones;

            return Json(new { resultado = respuesta });
        }

        [HttpPost]
        [Permiso(permiso = "asigarAccion")]
        public ActionResult AgregarAccion()
        {
            List<Accion> acciones = (List<Accion>)Session["acciones"];
            Rol rolaccion = (Rol)Session["rol_accion"];
            string mensaje = RolRepository.GuardarPermisos(acciones, rolaccion.ID.ToString());

           
            return RedirectToAction("Index", "Rol");
        }
    }
}