using System;
using System.Collections.Generic;

#nullable disable

namespace Net_Gis_Falcon
{
    public partial class HistoricoEstado
    {
        public DateTime FechaModificacion { get; set; }
        public int Estado { get; set; }
        public int Peticion { get; set; }
        public int Operador { get; set; }

        public virtual Estado EstadoNavigation { get; set; }
        public virtual Usuario OperadorNavigation { get; set; }
        public virtual Peticion PeticionNavigation { get; set; }
    }
}
