using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Security;
using System.Net.Http;
using System.Web.Security;
using SGEA.Models;

namespace Helper
{
    public class SessionHelper
    {

        public static bool ExistUserInSession()
        {
            return (HttpContext.Current.Session["usuario"] != null) ? true : false;
        }

        public static void DestroyUserSession()
        {
            HttpContext.Current.Session.Abandon();
        }

        public static Usuario GetUser()
        {
            Usuario user = new Usuario();
            if (HttpContext.Current.Session["usuario"] != null)
            {
                user = (Usuario)HttpContext.Current.Session["usuario"];
            }
            return user;
        }

        public static long GetInstitucion()
        {
            long institucion_id = 0;
            if (HttpContext.Current.Session["usuario"] != null)
            {
                var usuario = GetUser();
                institucion_id = usuario.IDInstitucion;
            }
            return institucion_id;
        }

        public static void AddUserToSession(Usuario usuario, string idInstitucion)
        {
            HttpContext.Current.Session["usuario"] = usuario;
            HttpContext.Current.Session["institucion"] = idInstitucion;
        }
    }
}