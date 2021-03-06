﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SGEA.Models
{
    public class Inscripcion
    {
        [DisplayName("Inscripcion")]
        public long ID { get; set; }
        [DisplayName("Alumno")]
        public string NombreAlumno { get; set; }
        [DisplayName("Alumno")]
        public long AlumnoID { get; set; }
        [DisplayName("Cedula")]
        public long Cedula { get; set; }
        [DisplayName("Curso")]
        public string NombreCurso { get; set; }
        [DisplayName("Curso")]
        public long CursoID { get; set; }
        [DisplayName("Arancel")]
        public long ArancelID { get; set; }
        [DisplayName("Arancel")]
        public string NombreArancel { get; set; }
        [DisplayName("Anho")]
        public string Anho { get; set; }
        [DisplayName("Mes desde")]
        public int MesDesde { get; set; }
        [DisplayName("Mes hasta")]
        public int MesHasta { get; set; }
        [DisplayName("Fecha Inscripcion")]
        public string FechaInscripcionString { get; set; }
        [DisplayName("Estado")]
        public string Estado { get; set; }
        [DisplayName("Cantidad de Cuotas")]
        public int CantidadCuotas { get; set; }
        [DisplayName("Institucion")]
        public long InstitucionID { get; set; }
        [DisplayName("Nro Comprobante")]
        public string NroComprobante { get; set; }
        [DisplayName("Confirmado")]
        public bool Confirmado { get; set; }
        [DisplayName("Motivo")]
        public string MotivoAnulacion { get; set; }
        public List<PagareViewModel> listaPagares = new List<PagareViewModel>();
    }

    public class PagareViewModel
    {
        [DisplayName("Pagare")]
        public long ID { get; set; }
        [DisplayName("Inscripcion")]
        public long InscripcionID { get; set; }
        [DisplayName("Tipo Pagare")]
        public string TipoPagare { get; set; } //que carajos esto era (era Inscripcion y matricula?)
        [DisplayName("Fecha Vencimiento")]
        public string FechaVencimientoString { get; set; } 
        [DisplayName("Estado")]
        public string Estado { get; set; } 
        [DisplayName("Monto")]
        public string Monto { get; set; }
        [DisplayName("Monto")]
        public decimal MontoDecimal { get; set; }
        [DisplayName("FechaPago")]
        public string FechaPagoString { get; set; } 
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; } 
    }

    public class SituacionFinanciera
    {
        [DisplayName("Cedula")]
        public string Cedula { get; set; }
        [DisplayName("Nombres")]
        public string Nombres { get; set; }
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; }
        [DisplayName("Tipo Pagare")]
        public string TipoPagare { get; set; }
        [DisplayName("Estado")]
        public string Estado { get; set; }
        [DisplayName("Nro Factura")]
        public string NroFactura { get; set; }
        [DisplayName("Monto")]
        public decimal MontoDecimal { get; set; }
        [DisplayName("Monto")]
        public string MontoString { get; set; }
        [DisplayName("FechaPago")]
        public string FechaPagoString { get; set; }
        public long PagareID { get; set; }
    }

    public class ExtractoPago
    {
        public long AlumnoID { get; set; }
        [DisplayName("Cedula")]
        public string Cedula { get; set; }
        [DisplayName("Nombres")]
        public string Nombres { get; set; }
        [DisplayName("Monto Pagare")]
        public decimal MontoDecimalPagare { get; set; }
        [DisplayName("Monto Pagare")]
        public string MontoStringPagare { get; set; }
        [DisplayName("Monto Pagado")]
        public decimal MontoDecimalPagado { get; set; }
        [DisplayName("Monto Pagado")]
        public string MontoStringPagado { get; set; }
        [DisplayName("Curso")]
        public string NombreCurso { get; set; }
    }
}