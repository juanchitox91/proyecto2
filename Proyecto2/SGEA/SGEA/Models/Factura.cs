using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SGEA.Models
{
    public class Factura
    {
        [DisplayName("Factura")]
        public long ID { get; set; }
        [DisplayName("Razon Social")]
        public string RazonSocial { get; set; }
        [DisplayName("NroFactura")]
        public string NroFactura { get; set; }
        [DisplayName("Fecha Factura")]
        public string FechaPagoFactura { get; set; }
        [DisplayName("NroDocumento")]
        public string NroDocumento { get; set; }
        [DisplayName("Tipo Documento")]
        public long TipoDctoID { get; set; }
        [DisplayName("Tipo Documento")]
        public string TipoDocumentoDescr { get; set; }
        [DisplayName("Tipo Pago")]
        public long TipoPagoID { get; set; }
        [DisplayName("Tipo Pago")]
        public string TipoPagoDesc { get; set; }
        [DisplayName("Monto Factura")]
        public string MontoTotal { get; set; }
        [DisplayName("Monto Factura")]
        public decimal MontoTotalDecimal { get; set; }

        public List<FacturaDetalle> FacturaDetalle = new List<FacturaDetalle>();
    }

    public class FacturaDetalle
    {
        [DisplayName("ID")]
        public long ID { get; set; }
        [DisplayName("Cabecera Factura")]
        public long FacturaCabeceraID { get; set; }
        [DisplayName("Pagare")]
        public long PagareID { get; set; }
        [DisplayName("Monto")]
        public string Monto { get; set; }
        [DisplayName("Monto")]
        public decimal MontoDecimal { get; set; }
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; } 
    }

    public class ExtractoPago
    {
        [DisplayName("Apellido")]
        public string Apellido { get; set; }
        [DisplayName("Nombre")]
        public string Nombre{ get; set; }
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; }
        [DisplayName("Tipo Pagare")]
        public string TipoPagare { get; set; }
        [DisplayName("Estado")]
        public string Estado { get; set; }
        [DisplayName("Nro Factura")]
        public string NroFactura { get; set; }
        [DisplayName("Fecha")]
        public string Fecha { get; set; }
    }
}