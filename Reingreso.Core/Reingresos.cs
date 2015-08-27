using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reingreso.Core
{
    public class Reingresos
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public string registropatronal { get; set; }
        public string nss { get; set; }
        public DateTime fechaingreso { get; set; }
        public double sdi { get; set; }
    }
}
