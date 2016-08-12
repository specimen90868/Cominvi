using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bajas.Core
{
    public class Bajas
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public string registropatronal { get; set; }
        public string nss { get; set; }
        public int motivo { get; set; }
        public DateTime fecha { get; set; }
        public int diasproporcionales { get; set; }
        public DateTime periodoinicio { get; set; }
        public DateTime periodofin { get; set; }
        public string observaciones { get; set; }
        public DateTime registro { get; set; }
    }
}
