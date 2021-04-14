
using System.ComponentModel;

namespace SGEA.Models
{
    public class Planilla
    {
        [DisplayName("Planilla")]
        public long ID { get; set; }
        [DisplayName("Curso")]
        public long CursoID { get; set; }
        [DisplayName("Curso")]
        public string NombreCurso { get; set; }
        [DisplayName("Materia")]
        public long MateriaID { get; set; }
        [DisplayName("Materia")]
        public string NombreMateria { get; set; }
        [DisplayName("Docente")]
        public long DocenteID { get; set; }
        [DisplayName("Docente")]
        public string NombreDocente { get; set; }
        [DisplayName("Institución")]
        public long InstitucionID { get; set; }
        [DisplayName("Estado")]
        public string Estado { get; set; }
        [DisplayName("Año Lectivo")]
        public string AnhoLectivo { get; set; }
        [DisplayName("Título")]
        public string Titulo { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
    }

    public class ProgramaEstudio
    {
        [DisplayName("Planilla")]
        public long PlanillaID { get; set; }
        [DisplayName("Planilla")]
        public string TituloPlanilla { get; set; }
        [DisplayName("Planilla")]
        public string DescripcionPlanilla { get; set; }
        [DisplayName("Curso")]
        public long CursoID { get; set; }
        [DisplayName("Curso")]
        public string NombreCurso { get; set; }
        [DisplayName("Unidad")]
        public long UnidadID { get; set; }
        [DisplayName("Unidad")]
        public string NombreUnidad { get; set; }
        [DisplayName("SubUnidad")]
        public long SubUnidadID { get; set; }
        [DisplayName("SubUnidad")]
        public string NombreSubUnidad { get; set; }
        [DisplayName("Item")]
        public long ItemID { get; set; }
        [DisplayName("Item")]
        public string NombreItem { get; set; }
        [DisplayName("Item")]
        public string DescripcionItem { get; set; }
        [DisplayName("Puntaje Maximo")]
        public long PuntajeMaximo { get; set; }
    }
}