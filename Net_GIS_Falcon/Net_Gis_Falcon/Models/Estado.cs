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
            InversePadreNavigation = new HashSet<Estado>();
        }

        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }
        public bool Esfinal { get; set; }
        public string ColorEstado { get; set; }
        public int? Padre { get; set; }

        public virtual Estado PadreNavigation { get; set; }
        public virtual ICollection<HistoricoEstado> HistoricoEstados { get; set; }
        public virtual ICollection<Estado> InversePadreNavigation { get; set; }



    }
}
