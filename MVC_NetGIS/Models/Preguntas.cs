using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_NetGIS.Models
{
    public class Preguntas
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public Preguntas()
        {

        }
    }
}
