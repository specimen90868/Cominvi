using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faltas.Core
{
    public class Faltas
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public int periodo { get; set; }
        public int faltas { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public DateTime fecha { get; set; }
    }
}
