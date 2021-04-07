using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SGEA.Models
{
    public class Asistencia
    {
        [DisplayName("Cedula")]
        public string Cedula { get; set; }
        [DisplayName("Alumno")]
        public string NombreAlumno { get; set; }
        [DisplayName("Alumno")]
        public long AlumnoID { get; set; }
        [DisplayName("Planilla")]
        public string NombrePlanilla { get; set; }
        [DisplayName("Planilla")]
        public long PlanillaID { get; set; }
        [DisplayName("Fecha")]
        public string Fecha { get; set; }
        [DisplayName("Presente")]
        public bool Presente { get; set; }
    }

}