using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfonavitProporcional.Core
{
    public class InfonavitProporcional
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public int dias { get; set; }
        public DateTime periodoinicio { get; set; }
        public DateTime periodofin { get; set; }
        public int idsuainfonavit { get; set; }
        public int idinfonavit { get; set; }
    }
}
