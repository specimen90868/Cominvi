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
        public string noempleado { get; set; }
        public string nombres { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string nombrecompleto { get; set; }
        public int idempresa { get; set; }
        public int idperiodo { get; set; }
        public int iddepartamento { get; set; }
        public int idpuesto { get; set; }
        public DateTime fechaingreso { get; set; }
        public int antiguedad { get; set; }
        public DateTime fechaantiguedad { get; set; }
        public int antiguedadmod { get; set; }
        public DateTime fechanacimiento { get; set; }
        public int edad { get; set; }
        public string rfc { get; set; }
        public string curp { get; set; }
        public string nss { get; set; }
        public int digitoverificador { get; set; }
        public int tiposalario { get; set; }
        public double sdi { get; set; }
        public double sd { get; set; }
        public double sueldo { get; set; }
        public int estatus { get; set; }
        public int idse { get; set; }
        public int idusuario { get; set; }      
    }

    public class IncrementoSalarial
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public string nombre { get; set; }
        public double sdivigente { get; set; }
        public double sdinuevo { get; set; }
    }
}
