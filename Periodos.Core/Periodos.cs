using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periodos.Core
{
    public class Periodos
    {
        public int idperiodo { get; set; }
        public int idcliente { get; set; }
        public string pago { get; set; }
        public int dias { get; set; }
        public string inicio { get; set; }
        public string termino { get; set; }
        public int estatus { get; set; }
    }
}
