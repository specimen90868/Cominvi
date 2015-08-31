using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TablaSubsidio.Core
{
    public class TablaSubsidio
    {
        public int id { get; set; }
        public double desde { get; set; }
        public double cantidad { get; set; }
        public int periodo { get; set; }
        public int anio { get; set; }
    }
}
