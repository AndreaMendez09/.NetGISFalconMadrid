using System;
using System.Collections.Generic;

#nullable disable

namespace Net_Gis_Falcon
{
    public partial class OperadoresZona
    {
        public int Operador { get; set; }
        public int Zona { get; set; }

        public virtual Usuario OperadorNavigation { get; set; }
        public virtual Zona ZonaNavigation { get; set; }
    }
}
