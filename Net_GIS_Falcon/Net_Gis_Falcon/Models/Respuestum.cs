using System;
using System.Collections.Generic;

#nullable disable

namespace Net_Gis_Falcon
{
    public partial class Respuestum
    {
        public Respuestum()
        {
            InverseRespuestaPadreNavigation = new HashSet<Respuestum>();
        }

        public int IdRespuesta { get; set; }
        public string CuerpoRespuesta { get; set; }
        public bool Principal { get; set; }
        public int Nivel { get; set; }
        public int? RespuestaPadre { get; set; }

        public virtual Nivel NivelNavigation { get; set; }
        public virtual Respuestum RespuestaPadreNavigation { get; set; }
        public virtual ICollection<Respuestum> InverseRespuestaPadreNavigation { get; set; }
    }
}
