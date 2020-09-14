using System;
using System.ComponentModel;

namespace SGEA.Models
{
    public class ItemPlanilla
    {
        [DisplayName("Item Planilla")]
        public long ID { get; set; }
        [DisplayName("Tipo Item")]
        public string NombreTipoItem { get; set; }
        [DisplayName("Tipo Item")]
        public long TipoItemID { get; set; }
        [DisplayName("Sub Unidad")]
        public string NombreSubUnidad { get; set; }
        [DisplayName("Sub Unidad")]
        public long SubUnidadID { get; set; }
        [DisplayName("Título")]
        public string Titulo { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [DisplayName("Puntaje Máximo")]
        public int PuntajeMaximo { get; set; }
        [DisplayName("Fecha Evaluación")]
        public DateTime FechaEvaluacion { get; set; }
        [DisplayName("Fecha Evaluación")]
        public string FechaEvaluacionString { get; set; }
        [DisplayName("Institución")]
        public long InstitucionID { get; set; }
    }
}