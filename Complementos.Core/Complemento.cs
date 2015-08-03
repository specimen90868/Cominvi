using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complementos.Core
{
    public class Complemento
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public string contrato { get; set; }
        public string jornada { get; set; }
        public int iddepartamento { get; set; }
        public int idpuesto { get; set; }
        public string estadocivil { get; set; }
        public string sexo { get; set; }
        public string escolaridad { get; set; }
        public string horario { get; set; }
        public string nocontrol { get; set; }
        public string clinica { get; set; }
        public string nacionalidad { get; set; }
        public string funciones { get; set; }
    }
}
