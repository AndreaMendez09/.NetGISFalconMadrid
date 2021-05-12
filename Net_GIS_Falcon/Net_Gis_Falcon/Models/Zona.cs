using System;
using System.Collections.Generic;
using NpgsqlTypes;

#nullable disable

namespace Net_Gis_Falcon
{
    public partial class Zona
    {
        public Zona()
        {
            OperadoresZonas = new HashSet<OperadoresZona>();
        }

        public int IdZona { get; set; }
        public string NombreZona { get; set; }
        public string DescripcionZona { get; set; }
        public NpgsqlPolygon GeometriaZona { get; set; }
        public string Coordenadas { get; set; }

        public virtual ICollection<OperadoresZona> OperadoresZonas { get; set; }
    }
}
