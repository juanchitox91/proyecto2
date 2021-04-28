using SGEA.Filters;
using System.Web.Mvc;
using SGEA.Repository;
using System.Collections.Generic;
using SGEA.Models;
using System.Linq;
using System;
using SGEA.Reportes;
using System.IO;

namespace SGEA.Areas.Gestion.Controllers
{
    [Autenticado]
    public class InscripcionController : Controller
    {
        [Permiso(permiso = "verInscripciones")]
        public ActionResult Index()
        {
            string idinstitucion = HttpContext.Session["institucion"].ToString();
            List<Inscripcion> inscripciones = InscripcionRepository.getInscripciones(idinstitucion);
            ViewBag.Inscripciones = inscripciones;
            ViewBag.error = (Session["errorInscripcion"]??string.Empty).ToString();
            Session["inscripciones"] = inscripciones;
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "crearInscripcion")]
        public ActionResult Crear()
        {
            CargarDatosListas(new Inscripcion());
            return View();

        }

        [HttpPost]
        [Permiso(permiso = "crearInscripcion")]
        public ActionResult Crear(Inscripcion inscripcion)
        {
            inscripcion.InstitucionID = Convert.ToInt64(Session["institucion"].ToString());
            inscripcion.Estado = "A";
            inscripcion.listaPagares = new List<PagareViewModel>();

            var mensaje = InscripcionRepository.createInscripcion(inscripcion);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La carga se realizó exitosamente.";
                Session["inscripcion"] = null;
                CargarDatosListas(new Inscripcion());
                
                /*aquí recuperamos de nuevo las inscripciones actualizadas y cargamos ya los pagares */
                List<Inscripcion> inscripciones = InscripcionRepository.getInscripciones(Session["institucion"].ToString());
                Session["inscripciones"] = inscripciones;
                Inscripcion inscripcioncargada = inscripciones.Where(x => x.AlumnoID == inscripcion.AlumnoID &&
                x.ArancelID == inscripcion.ArancelID && x.CursoID == inscripcion.CursoID).FirstOrDefault();
                InscripcionRepository.createPagares(inscripcioncargada);
                return RedirectToAction("VerDetalle","Inscripcion", new { id = inscripcioncargada.ID.ToString() });

            }
            else
            {
                ViewBag.error = mensaje;
                return View();
            }
        }

        [HttpGet]
        [Permiso(permiso = "editarInscripcion")]
        public ActionResult Editar(string id)
        {
            string idinstitucion = HttpContext.Session["institucion"].ToString();
            List<Inscripcion> lista = (List<Inscripcion>)Session["inscripciones"];
            Inscripcion inscripcion = lista.Where(x => x.ID == Convert.ToInt64(id)).SingleOrDefault();

            if (inscripcion.Estado == "CONFIRMADO" || inscripcion.Estado == "INACTIVO")
            {
                Session["errorInscripcion"] = $"La inscripcion ya se encuentra en estado {inscripcion.Estado} y no puede ser modificada.";
                return RedirectToAction("Index");
            }
            else
            {
                CargarDatosListas(inscripcion);
            }

            return View(inscripcion);
        }

        [HttpPost]
        [Permiso(permiso = "editarInscripcion")]
        public ActionResult Editar(Inscripcion inscripcion)
        {
            var mensaje = InscripcionRepository.updateInscripcion(inscripcion);
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La edición se realizó exitosamente.";
                CargarDatosListas(inscripcion);
                inscripcion = InscripcionRepository.getInscripcion(inscripcion.ID.ToString());
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return View(inscripcion);
        }

        [HttpGet]
        [Permiso(permiso = "eliminarInscripcion")]
        public ActionResult Eliminar(string id)
        {
            string idinstitucion = HttpContext.Session["institucion"].ToString();
            List<Inscripcion> lista = (List<Inscripcion>)Session["inscripciones"];
            Inscripcion inscripcion = lista.Where(x => x.ID == Convert.ToInt64(id)).SingleOrDefault();

            if (inscripcion.Estado == "CONFIRMADO")
            {
                ViewBag.error = "La inscripcion ya se encuentra confirmada y no puede ser eliminada.";
                return RedirectToAction("Index");
            }
            else
            {
                CargarDatosListas(inscripcion);
            }

            return View(inscripcion);
        }

        [HttpPost]
        [Permiso(permiso = "eliminarInscripcion")]
        public ActionResult Eliminar(Inscripcion inscripcion)
        {

            var mensaje = InscripcionRepository.deleteInscripcion(inscripcion.ID);
           
            if (mensaje == "OK")
            {
                ViewBag.mensaje = "La inscripcion se elimino exitosamente.";
            }
            else
            {
                ViewBag.error = mensaje;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Permiso(permiso = "verDetalleInscripcion")]
        public ActionResult VerDetalle(string id)
        {
            string idinstitucion = HttpContext.Session["institucion"].ToString();
            List<Inscripcion> lista = (List<Inscripcion>)Session["inscripciones"];
            Inscripcion inscripcion = lista.Where(x => x.ID == Convert.ToInt64(id)).SingleOrDefault();
            inscripcion.listaPagares = InscripcionRepository.getPagares(inscripcion.ID.ToString());

            CargarDatosListas(inscripcion);


            return View(inscripcion);
        }

        public JsonResult GuardarFecha(string fecha, string id)
        {
            DateTime fechaelegida = DateTime.ParseExact(fecha, "dd/MM/yyyy", null);

            if (fechaelegida.Date < DateTime.Now.Date)
            {
                return Json("La fecha debe ser mayor o igual a la actual.");
            }

            InscripcionRepository.updatePagares(id, fecha);

            return Json("OK");
        }

        [HttpGet]
        [Permiso(permiso = "AnularInscripcion")]
        public ActionResult Anular(string id)
        {
            string idinstitucion = HttpContext.Session["institucion"].ToString();
            List<Inscripcion> lista = (List<Inscripcion>)Session["inscripciones"];
            Inscripcion inscripcion = lista.Where(x => x.ID == Convert.ToInt64(id)).SingleOrDefault();
            inscripcion.listaPagares = InscripcionRepository.getPagares(inscripcion.ID.ToString());

            CargarDatosListas(inscripcion);

            return View(inscripcion);
        }

        [HttpPost]
        [Permiso(permiso = "AnularInscripcion")]
        public JsonResult Anular(Inscripcion inscr)
        {
            string idinstitucion = HttpContext.Session["institucion"].ToString();
            List<Inscripcion> lista = (List<Inscripcion>)Session["inscripciones"];
            Inscripcion inscripcion = lista.Where(x => x.ID == Convert.ToInt64(inscr.ID)).SingleOrDefault();

            string mensaje = InscripcionRepository.AnularInscripcion(inscr.ID.ToString());
 
            return Json(mensaje);
        }

        [HttpGet]
        [Permiso(permiso = "ConfirmarInscripcion")]
        public ActionResult Confirmar(string id)
        {
            string idinstitucion = HttpContext.Session["institucion"].ToString();
            List<Inscripcion> lista = (List<Inscripcion>)Session["inscripciones"];
            Inscripcion inscripcion = lista.Where(x => x.ID == Convert.ToInt64(id)).SingleOrDefault();

            if(inscripcion.Estado == "CONFIRMADO" || inscripcion.Estado == "INACTIVO")
            {
                ViewBag.error = $"La inscripcion ya se encuentra en estado {inscripcion.Estado} y no puede ser modificada.";
                return RedirectToAction("Index");
            }

            inscripcion.listaPagares = InscripcionRepository.getPagares(inscripcion.ID.ToString());

            CargarDatosListas(inscripcion);

            return View(inscripcion);
        }

        [HttpPost]
        [Permiso(permiso = "ConfirmarInscripcion")]
        public ActionResult Confirmar(Inscripcion inscripcion)
        {
            string idinstitucion = HttpContext.Session["institucion"].ToString();

            List<Inscripcion> lista = (List<Inscripcion>)Session["inscripciones"];
            Inscripcion inscripcion1 = lista.Where(x => x.ID == Convert.ToInt64(inscripcion.ID)).SingleOrDefault();
            inscripcion1.listaPagares = InscripcionRepository.getPagares(inscripcion.ID.ToString());

            if (inscripcion1.Estado == "ACTIVO")
            {
                string mensaje = InscripcionRepository.validarPagares(inscripcion.ID.ToString());
                if (mensaje == "OK")
                {
                    InscripcionRepository.confirmarInscripcion(inscripcion.ID.ToString());
                    ViewBag.mensaje = "La inscripcion se confirmo exitosamente.";
                }
                else
                {
                    ViewBag.error = "Ha ocurrido un error inesperado, favor intente nuevamente mas tarde.";
                }
            }
            else
            {
                ViewBag.error = "Ha ocurrido un error inesperado, favor intente nuevamente mas tarde.";
            }
          
            CargarDatosListas(inscripcion1);

            return View(inscripcion1.ID);
        }


        #region Abonar Aranceles

        [HttpGet]
        [Permiso(permiso = "AbonarInscripcion")]
        public ActionResult Abonar()
        {
            string idinstitucion = HttpContext.Session["institucion"].ToString();
            List<Inscripcion> inscripciones = InscripcionRepository.getInscripciones(idinstitucion).Where(x => x.Estado == "CONFIRMADO").ToList();
            ViewBag.Inscripciones = inscripciones;
            Session["inscripciones"] = inscripciones;
            return View();
        }

        [HttpGet]
        [Permiso(permiso = "AbonarInscripcion")]
        public ActionResult AbonarInscripcion(string id)
        {
            string idinstitucion = HttpContext.Session["institucion"].ToString();
            List<Inscripcion> lista = (List<Inscripcion>)Session["inscripciones"];
            Inscripcion inscripcion = lista.Where(x => x.ID == Convert.ToInt64(id)).SingleOrDefault();
            inscripcion.listaPagares = InscripcionRepository.getPagares(inscripcion.ID.ToString());

            CargarDatosListas(inscripcion);


            return View(inscripcion);
        }

        [HttpPost]
        [Permiso(permiso = "AbonarInscripcion")]
        public JsonResult ConfirmarPago(List<string> IDs)
        {
            Factura factura = new Factura();
            factura.FacturaDetalle = new List<FacturaDetalle>();

            try
            {
                foreach (string id in IDs)
                {
                    PagareViewModel pagare = InscripcionRepository.getPagareById(id);
                    factura.FacturaDetalle.Add(new FacturaDetalle
                    {
                        PagareID = Convert.ToInt64(id),
                        Descripcion = pagare.Descripcion,
                        Monto = pagare.Monto,
                        MontoDecimal = pagare.MontoDecimal
                    });
                }

                factura.MontoTotalDecimal = factura.FacturaDetalle.Sum(x => x.MontoDecimal);
                factura.MontoTotal = factura.MontoTotalDecimal.ToString("#,###").Replace(",", ".");

                Session["FacturaConfirmar"] = factura;
            }
            catch (Exception ex)
            {
                return Json("ERROR");
            }

            return Json("OK");
        }

        [HttpGet]
        [Permiso(permiso = "AbonarInscripcion")]
        public ActionResult CargarFactura()
        {
            Factura factura = new Factura();

            try
            {
                factura = (Factura)Session["FacturaConfirmar"];

                string idinstitucion = HttpContext.Session["institucion"].ToString();
                ViewBag.tiposPago = InscripcionRepository.getTiposPagoSelect2(idinstitucion, "0");
                ViewBag.tiposDcto = InscripcionRepository.getTiposDctoSelect2(idinstitucion, "0");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Abonar");
            }

            return View(factura);
        }

        [HttpPost]
        [Permiso(permiso = "AbonarInscripcion")]
        public ActionResult CargarFactura(Factura facturaModel)
        {
            Factura factura = new Factura();

            try
            {
                factura = (Factura)Session["FacturaConfirmar"];

                factura.TipoDctoID = facturaModel.TipoDctoID;
                factura.TipoPagoID = facturaModel.TipoPagoID;
                factura.RazonSocial = facturaModel.RazonSocial;
                factura.NroDocumento = facturaModel.NroDocumento;
                factura.FechaPagoFactura = DateTime.Now.ToShortDateString();
                factura.NroFactura = facturaModel.NroFactura;

                string mensaje = InscripcionRepository.cargarFactura(factura);

                if (mensaje == "OK")
                {
                    ViewBag.mensaje = "La factura se cargó exitosamente.";
                }
                else
                {
                    ViewBag.error = "Ha ocurrido un error inesperado, favor intente nuevamente mas tarde.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = "Ha ocurrido un error inesperado, favor intente nuevamente mas tarde.";
            }

            string idinstitucion = HttpContext.Session["institucion"].ToString();
            ViewBag.tiposPago = InscripcionRepository.getTiposPagoSelect2(idinstitucion, factura.TipoPagoID.ToString());
            ViewBag.tiposDcto = InscripcionRepository.getTiposDctoSelect2(idinstitucion, factura.TipoDctoID.ToString());
            return View(factura);
        }

        #endregion

        private void CargarDatosListas(Inscripcion inscripcion)
        {
            string idinstitucion = HttpContext.Session["institucion"].ToString();

            ViewBag.cursos = CursoRepository.getCursosSelect2(idinstitucion, inscripcion.CursoID.ToString());
            ViewBag.alumnos = AlumnoRepository.getAlumnosSelect2(idinstitucion, inscripcion.AlumnoID.ToString() );
            List<SelectListItem> aranceles = ArancelRepository.getArancelSelect2(idinstitucion, inscripcion.ArancelID.ToString(), DateTime.Now.Year);
            aranceles.AddRange(ArancelRepository.getArancelSelect2(idinstitucion, inscripcion.ArancelID.ToString(), (DateTime.Now.Year + 1)));
            aranceles = aranceles.Distinct().ToList();
            ViewBag.aranceles = aranceles;

            ViewBag.mesDesde = GeneralRepository.getMeses(2, 11, inscripcion.MesDesde);
            ViewBag.mesHasta = GeneralRepository.getMeses(2, 11, inscripcion.MesHasta);
            ViewBag.cantidadcuotas = GeneralRepository.getCantidadCuotas(inscripcion.CantidadCuotas);
        }

        #region Reporte
        [Permiso(permiso = "verReporteSituacionFinanciera")]
        public ActionResult ReporteSituacionFinanciera()
        {
            List<SelectListItem> cursos = CursoRepository.getCursosSelect2(HttpContext.Session["institucion"].ToString(), "0");
            ViewBag.Cursos = cursos;
            return View();
        }

        [HttpPost]
        public JsonResult PoblarGrilla(DataTableAjaxPostModel model)
        {
            List<Alumno> lista = new List<Alumno>();

            try
            {
                lista = AlumnoRepository.getAlumnosByIdPlanilla(model.param1);
            }
            catch { }


            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = lista.Count,
                recordsFiltered = lista.Count,
                data = lista
            });

        }
        [Permiso(permiso = "verReporteSituacionFinanciera")]
        public ActionResult ReportePDF(string idAlumno)
        {
            List<SituacionFinanciera> lista = InscripcionRepository.getSitaucionFinanciera(idAlumno);
            lista = lista.OrderBy(x => x.PagareID).ToList();
            SituacionFinancieraReport rpt = new SituacionFinancieraReport();
            rpt.Load();
            rpt.SetDataSource(lista);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);


            return File(s, "application/pdf");
        }
        #endregion
    }
}