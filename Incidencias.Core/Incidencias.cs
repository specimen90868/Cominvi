using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Core
{
    public class Incidencias
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public int dias { get; set; }
        public string certificado { get; set; }
        public DateTime inicioincapacidad { get; set; }
        public DateTime finincapacidad { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public DateTime periodoinicio { get; set; }
        public DateTime periodofin { get; set; }
        public int idcontrol { get; set; }
        public int idincapacidad { get; set; }
    }
}
