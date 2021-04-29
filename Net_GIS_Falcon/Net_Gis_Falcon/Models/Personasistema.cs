using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Net_Gis_Falcon
{
    public partial class Personasistema
    {
        public Personasistema()
        {
            HistoricoEstados = new HashSet<HistoricoEstado>();
            OperadoresZonas = new HashSet<OperadoresZona>();
            PeticionOperadores = new HashSet<PeticionOperadore>();
        }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
        public string Idioma { get; set; }
        public string Contraseña { get; set; }
        public string Foto { get; set; }

        [Key]
        public int IdPersonasistema { get; set; }
        public int? Rol { get; set; }
        public string Zona { get; set; }

        public virtual ICollection<HistoricoEstado> HistoricoEstados { get; set; }
        public virtual ICollection<OperadoresZona> OperadoresZonas { get; set; }
        public virtual ICollection<PeticionOperadore> PeticionOperadores { get; set; }
    }
}
