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
        public long AumnoID { get; set; }
        [DisplayName("Planilla")]
        public string NombrePlanilla { get; set; }
        [DisplayName("Planilla")]
        public long PlanillaID { get; set; }
        [DisplayName("Presente")]
        public bool Presente { get; set; }
    }

}