using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SGEA.Models
{
    public class Usuario
    {
        [DisplayName("Usuario")]
        public long ID { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Campo Requerido.")]
        public string Nombre { get; set; }

        [DisplayName("Apellido")]
        [Required(ErrorMessage = "Campo Requerido.")]
        public string Apellido { get; set; }

        [DisplayName("Rol")]
        public string NombreRol { get; set; }

        [DisplayName("Rol")]
        [Required(ErrorMessage = "Campo Requerido.")]
        public long IDRol { get; set; }

        [DisplayName("Institución")]
        public long IDInstitucion { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Campo Requerido.")]
        [EmailAddress(ErrorMessage ="El mail ingresado no es válido.")]
        public string Email { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "Campo Requerido.")]
        public string Password { get; set; }


    }
}