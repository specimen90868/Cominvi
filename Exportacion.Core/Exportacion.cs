using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exportacion.Core
{
    public class Exportacion
    {
        public int id { get; set; }
        public string formulario { get; set; }
        public string campo { get; set; }
        public bool activo { get; set; }
        public int orden { get; set; }
    }
}
