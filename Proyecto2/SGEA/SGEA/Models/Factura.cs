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
        [DisplayName("Monto Inscripción")]
        public string MontoTotal { get; set; }

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
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; } 
    }
}