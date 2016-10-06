using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicaciones.Core
{
    public class Aplicaciones
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public int iddeptopuesto { get; set; }
        public string deptopuesto { get; set; }
        public DateTime fecha { get; set; }
        public DateTime registro { get; set; }
        public int idusuario { get; set; }
        public DateTime periodoinicio { get; set; }
        public DateTime periodofin { get; set; }

    }
}
