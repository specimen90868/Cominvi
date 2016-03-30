using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modificaciones.Core
{
    public class Modificaciones
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public string registropatronal { get; set; }
        public string nss { get; set; }
        public DateTime fecha { get; set; }
        public decimal sdi { get; set; }
    }
}
