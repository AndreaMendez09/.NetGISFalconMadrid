using System;
using System.Collections.Generic;

#nullable disable

namespace Net_Gis_Falcon
{
    public partial class PeticionOperadore
    {
        public int Operador { get; set; }
        public int Peticion { get; set; }

        public virtual Usuario OperadorNavigation { get; set; }
        public virtual Peticion PeticionNavigation { get; set; }
    }
}
