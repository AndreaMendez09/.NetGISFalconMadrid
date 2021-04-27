using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Net_Gis_Falcon
{
    public partial class Usuario
    {
        [Key]
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
        public string Idioma { get; set; }
        public string Contraseña { get; set; }
        public string Foto { get; set; }
        public string Municipio { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }
}
