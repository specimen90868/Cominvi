using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacaciones.Core
{
    public class Vacaciones
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public DateTime fechaingreso { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fin { get; set; }
        public double sd { get; set; }
        public int diasderecho { get; set; }
        public int diasapagar { get; set; }
        public int diaspendientes { get; set; }
        public double pv { get; set; }
        public double pgravada { get; set; }
        public double isrgravada { get; set; }
        public double pagovacaciones { get; set; }
        public double totalprima { get; set; }
        public double total { get; set; }
        public DateTime fechapago { get; set; }
        public bool pagada { get; set; }
        public bool pvpagada { get; set; }
    }

    public class DiasDerecho 
    {
        public int id { get; set; }
        public int anio { get; set; }
        public int dias { get; set; }
    }
}
