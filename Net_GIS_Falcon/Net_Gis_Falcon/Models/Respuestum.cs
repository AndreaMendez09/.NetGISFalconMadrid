using System;
using System.Collections.Generic;

#nullable disable

namespace Net_Gis_Falcon
{
    public partial class Respuestum
    {
        public int IdRespuesta { get; set; }
        public string CuerpoRespuesta { get; set; }
        public bool Principal { get; set; }
        public int Nivel { get; set; }

        public virtual Nivel NivelNavigation { get; set; }
    }
}
