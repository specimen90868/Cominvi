using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salario.Core
{
    public class Salarios
    {
        public int idsalario { get; set; }
        public DateTime periodo { get; set; }
        public decimal valor { get; set; }
        public string zona { get; set; }
    }
}
