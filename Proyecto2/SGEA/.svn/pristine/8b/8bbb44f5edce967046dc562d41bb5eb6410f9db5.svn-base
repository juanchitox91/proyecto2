using System.Net;
using System.Web.Mvc;
using Helper;
using SGEA.Repository;
using SGEA.Filters;
using SGEA.Models;
using System.Collections.Generic;

namespace SGEA.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login/Acceder
        [HttpPost]
        public ActionResult Acceder(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string md5pass = HashHelper.MD5(password);

            Usuario usuario = LoginRepository.Acceder(email, password);
                //db.Usuarios.Where(x => x.Email == email && x.Password == md5pass).SingleOrDefault();
            
            if (usuario == null)
            {
                return HttpNotFound();
            }
            //aca agregamos la visualización de los menús
            List<Accion> acciones = RolRepository.getAccionesPorRoles(usuario.IDRol);
            Session["accionesMenu"] = acciones;
            Dictionary<string, string> permisoOperaciones = RolRepository.GetPermisosOperaciones(usuario.IDRol.ToString());
            Session["permisoOperaciones"] = permisoOperaciones;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();
            return Redirect("~/");
        }


        //// GET: Login/Create
        //public ActionResult Create()
        //{
        //    ViewBag.RolID = new SelectList(db.Roles, "ID", "NombreRol");
        //    return View();
        //}

        //// POST: Login/Create
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        //// más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Nombre,Apellido,RolID,Email,Password")] Usuario usuario)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Usuarios.Add(usuario);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.RolID = new SelectList(db.Roles, "ID", "NombreRol", usuario.RolID);
        //    return View(usuario);
        //}

        //// GET: Login/Edit/5
        //public ActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Usuario usuario = db.Usuarios.Find(id);
        //    if (usuario == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.RolID = new SelectList(db.Roles, "ID", "NombreRol", usuario.RolID);
        //    return View(usuario);
        //}

        //// POST: Login/Edit/5
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        //// más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Nombre,Apellido,RolID,Email,Password")] Usuario usuario)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(usuario).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.RolID = new SelectList(db.Roles, "ID", "NombreRol", usuario.RolID);
        //    return View(usuario);
        //}

        //// GET: Login/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Usuario usuario = db.Usuarios.Find(id);
        //    if (usuario == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(usuario);
        //}

        //// POST: Login/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    Usuario usuario = db.Usuarios.Find(id);
        //    db.Usuarios.Remove(usuario);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
