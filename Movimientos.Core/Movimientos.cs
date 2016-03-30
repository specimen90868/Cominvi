using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimientos.Core
{
    public class Movimientos
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public int idconcepto { get; set; }
        public decimal cantidad { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
    }
}
