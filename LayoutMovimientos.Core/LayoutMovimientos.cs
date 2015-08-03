using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayoutMovimientos.Core
{
    public class LayoutMovimientos
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public int idcliente { get; set; }
        public int movimiento { get; set; }
        public string nombres { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public decimal sdi { get; set; }
        public decimal sdinuevo { get; set; }
        public string nss { get; set; }
        public int digitonss { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public DateTime fecha_sistema { get; set; }
        public string curp { get; set; }
        public string registro { get; set; }
        public int digitoregistro { get; set; }
        public int motivobaja { get; set; }
        public int generado { get; set; }
    }
}
