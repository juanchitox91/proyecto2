using System.ComponentModel;

namespace SGEA.Models
{
    public class TipoItem
    {
        [DisplayName("Tipo Ítem")]
        public long ID { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [DisplayName("Institución")]
        public long InstitucionID { get; set; }
    }
}