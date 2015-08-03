using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empleados.Core
{
    public class Empleados
    {
        public int idtrabajador { get; set; }
        public string nombres { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string nombrecompleto { get; set; }
        public DateTime fechanacimiento { get; set; }
        public int edad { get; set; }
        public int idempresa { get; set; }
        public int idcliente { get; set; }
        public DateTime fechaingreso { get; set; }
        public int antiguedad { get; set; }
        public int idperiodo { get; set; }
        public string localforaneo { get; set; }
        public string sua { get; set; }
        public string rfc { get; set; }
        public string curp { get; set; }
        public string nss { get; set; }
        public int digitoverificador { get; set; }
        public string tiposalario { get; set; }
        public decimal sdi { get; set; }
        public decimal sd { get; set; }
        public decimal sueldo { get; set; }
        public int idinfonavit { get; set; }
        public int estatus { get; set; }
        public int idse { get; set; }
        public int modsalario { get; set; }
        public int idplaza { get; set; }      
    }
}
