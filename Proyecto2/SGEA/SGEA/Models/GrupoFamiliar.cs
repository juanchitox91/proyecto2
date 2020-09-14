using System.ComponentModel;

namespace SGEA.Models
{
    public class GrupoFamiliar
    {
        [DisplayName("Grupo Familiar")]
        public long ID { get; set; }
        [DisplayName("Apellidos")]
        public string Apellidos { get; set; }
        [DisplayName("Observación")]
        public string Observacion { get; set; }
        [DisplayName("Fecha Alta")]
        public string FechaAlta { get; set; }
        [DisplayName("Institución")]
        public long InstitucionID { get; set; }
    }
}