using System.Collections.Generic;
using System.ComponentModel;

namespace SGEA.Models
{
    public class Resultado
    {
        [DisplayName("Estado")]
        public Estado Estado { get; set; }
        [DisplayName("Mensaje")]
        public string Mensaje { get; set; }     
    }

    public enum Estado
    {
        OK,
        ERROR
    }
}