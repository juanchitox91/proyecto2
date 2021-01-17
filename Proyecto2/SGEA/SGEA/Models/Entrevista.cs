
using System.ComponentModel;

namespace SGEA.Models
{
    public class Entrevista
    {
        [DisplayName("Entrevistas")]
        public long ID { get; set; }
        [DisplayName("Cedula")]
        public long Cedula { get; set; }
        [DisplayName("Alumno")]
        public long AlumnoID { get; set; }
        [DisplayName("Alumno")]
        public string NombreAlumno { get; set; }
        [DisplayName("Motivo")]
        public string Motivo { get; set; }
        [DisplayName("Acuerdo")]
        public string Acuerdo { get; set; }
        [DisplayName("Sugerencia")]
        public string Sugerencia { get; set; }
        [DisplayName("Fecha Entrevista")]
        public string FechaEntrevistaString { get; set; }
    }
}