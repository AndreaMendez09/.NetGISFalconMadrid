using System;
using System.Collections.Generic;

#nullable disable

namespace Net_Gis_Falcon
{
    public partial class Direccion
    {
        public int IdDireccion { get; set; }
        public string TipoVia { get; set; }
        public string NombreVia { get; set; }
        public string Ciudad { get; set; }
        public string Piso { get; set; }
        public string Escalera { get; set; }
        public string Numero { get; set; }
        public string CodPostal { get; set; }
        public string Pais { get; set; }
        public string Notas { get; set; }
    }
}
