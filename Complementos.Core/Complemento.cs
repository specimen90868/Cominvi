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
        public int contrato { get; set; }
        public int jornada { get; set; }
        public int estadocivil { get; set; }
        public int sexo { get; set; }
        public int escolaridad { get; set; }
        public string clinica { get; set; }
        public string nacionalidad { get; set; }
        public string observaciones { get; set; }
    }
}
