using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguinaldo.Core
{
    public class Aguinaldo
    {
        public int id { get; set; }
        public int idempresa { get; set; }
        public int idtrabajador { get; set; }
        public int diaslaborados { get; set; }
        public int diasaguinaldo { get; set; }
        public decimal proporcinal { get; set; }
        public decimal gravado { get; set; }
        public decimal excento { get; set; }
        public decimal isr { get; set; }
        public decimal totalisr { get; set; }
        public decimal total { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public DateTime fechapago { get; set; }
    }
}
