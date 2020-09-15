using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SGEA.Models
{
    public class Evaluacion
    {
        [DisplayName("Cedula")]
        public string Cedula { get; set; }
        [DisplayName("Alumno")]
        public string NombreAlumno { get; set; }
        [DisplayName("Alumno")]
        public long AumnoID { get; set; }
        [DisplayName("Item")]
        public string NombreTipoItem { get; set; }
        [DisplayName("Item")]
        public long ItemID { get; set; }
        [DisplayName("Puntaje Alcanzado")]
        public long PuntajeAlcanzado { get; set; }
    }

}