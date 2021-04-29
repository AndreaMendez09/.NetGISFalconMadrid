using System;
using System.Collections.Generic;

#nullable disable

namespace Net_Gis_Falcon
{
    public partial class Estado
    {
        public Estado()
        {
            HistoricoEstados = new HashSet<HistoricoEstado>();
        }

        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }
        public bool Esfinal { get; set; }
        public string ColorEstado { get; set; }

        public virtual ICollection<HistoricoEstado> HistoricoEstados { get; set; }
    }
}
