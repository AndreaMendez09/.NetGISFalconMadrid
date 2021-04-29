using System;
using System.Collections.Generic;

#nullable disable

namespace Net_Gis_Falcon
{
    public partial class Peticion
    {
        public Peticion()
        {
            HistoricoEstados = new HashSet<HistoricoEstado>();
            PeticionOperadores = new HashSet<PeticionOperadore>();
        }

        public int IdPeticion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string LocalizacionPeticion { get; set; }
        public int Usuario { get; set; }

        public virtual Usuario UsuarioNavigation { get; set; }
        public virtual ICollection<HistoricoEstado> HistoricoEstados { get; set; }
        public virtual ICollection<PeticionOperadore> PeticionOperadores { get; set; }
    }
}
