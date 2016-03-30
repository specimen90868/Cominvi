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
        public decimal sd { get; set; }
        public int diasderecho { get; set; }
        public int diasapagar { get; set; }
        public int diaspendientes { get; set; }
        public decimal pv { get; set; }
        public decimal pexenta { get; set; }
        public decimal pgravada { get; set; }
        public decimal isrgravada { get; set; }
        public decimal pagovacaciones { get; set; }
        public decimal totalprima { get; set; }
        public decimal total { get; set; }
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

    public class VacacionesPrima
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public DateTime periodoinicio { get; set; }
        public DateTime periodofin { get; set; }
        public int diasderecho { get; set; }
        public int diaspago { get; set; }
        public int diaspendientes { get; set; }
        public DateTime fechapago { get; set; }
        public string vacacionesprima { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
    }
}
