using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SGEA.Models
{
    public class Curso
    {
        [DisplayName("Curso")]
        public long ID { get; set; }
        [DisplayName("Curso")]
        public int _Curso { get; set; }
        [DisplayName("Seccíón")]
        public string Seccion { get; set; }
        public enum Turnos
        {
            Mañana = 0,
            Tarde = 1,
            Noche = 2,
            DobleTurno= 3
        }
        [DisplayName("Turno")]
        public Turnos Turno { get; set; }
        [DisplayName("Nombre Curso")]
        public string NombreCurso { get; set; }
        [DisplayName("Observación")]
        public string Observacion { get; set; }
        [DisplayName("Fecha Alta")]
        public string FechaAlta { get; set; }
        [DisplayName("Institución")]
        public long InstitucionID { get; set; }
    }
}