using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapacidad.Core
{
    public class Incapacidades
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public int diasincapacidad { get; set; }
        public int diastomados { get; set; }
        public int diasrestantes { get; set; }
        public int diasapagar { get; set; }
        public int tipo { get; set; }
        public int aplicada { get; set; }
        public int consecutiva { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
    }
}
