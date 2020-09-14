using Helper;
using SGEA.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SGEA.Filters
{
    // Si no estamos logueado, regresamos al login
    public class AutenticadoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!SessionHelper.ExistUserInSession())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "Index"
                }));
            }
        }
    }

    // Si estamos logueados ya no podemos acceder a la página de Login
    public class NoLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (SessionHelper.ExistUserInSession())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
            }
        }
    }


    // Si estamos logueados ya no podemos acceder a la página de Login
    public class PermisoAttribute : ActionFilterAttribute
    {
        public string permiso { get; set; } //aca se guarda el valor que se le envia desde la llamada al controlador
        public string idusuario { get; set; } 

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            idusuario = SessionHelper.GetUser().ID.ToString();

            if (HomeRepository.getPermiso(idusuario, permiso))
            {
                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                //{
                //    controller = "Home",
                //    action = "Index"
                //}));
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Error",
                    action = "AccesoDenegado"
                }));
            }
        }
    }
}