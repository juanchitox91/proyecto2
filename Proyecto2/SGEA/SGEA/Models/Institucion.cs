using System.Collections.Generic;
using System.ComponentModel;

namespace SGEA.Models
{
    public class Institucion
    {
        [DisplayName("Institución")]
        public long ID { get; set; }
        [DisplayName("Institución")]
        public string NombreInstitucion { get; set; }
        [DisplayName("Dirección")]
        public string Direccion { get; set; }
        [DisplayName("Ciudad")]
        public string Ciudad { get; set; }
        [DisplayName("Departamento")]
        public string Departamento { get; set; }
        [DisplayName("Teléfono")]
        public string Telefono { get; set; }
    }
}