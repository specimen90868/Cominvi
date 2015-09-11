using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulas.Core
{
    public class Formulas
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int padre { get; set; }
        public string tabla { get; set; }
        public string campo { get; set; }
        public string clausula { get; set; }
    }
}
