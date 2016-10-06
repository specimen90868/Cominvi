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
        public int diasproporcionales { get; set; }
        public decimal sdi { get; set; }
        public DateTime periodoinicio { get; set; }
        public DateTime periodofin { get; set; }
        public DateTime registro { get; set; }
    }
}
