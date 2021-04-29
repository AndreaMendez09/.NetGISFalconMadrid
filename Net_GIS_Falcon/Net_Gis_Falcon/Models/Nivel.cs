using System;
using System.Collections.Generic;

#nullable disable

namespace Net_Gis_Falcon
{
    public partial class Nivel
    {
        public Nivel()
        {
            Respuesta = new HashSet<Respuestum>();
        }

        public int IdPregunta { get; set; }
        public string DescripcionPregunta { get; set; }
        public short Nivel1 { get; set; }

        public virtual ICollection<Respuestum> Respuesta { get; set; }
    }
}
