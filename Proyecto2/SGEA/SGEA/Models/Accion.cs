using System.Collections.Generic;
using System.ComponentModel;

namespace SGEA.Models
{
    public class Accion
    {
        [DisplayName("Acción")]
        public long ID { get; set; }
        [DisplayName("Nombre Acción")]
        public string NombreAccion { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [DisplayName("Acción Padre")]
        public long Parent_id { get; set; }
        [DisplayName("Activo")]
        public bool Activo { get; set; }     
    }
}