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
        public string tempresa1 { get; set; }
        public string tempresa2 { get; set; }
        public string tempresa3 { get; set; }
        public string extension { get; set; }
        public string cempresa { get; set; }
        public string cpersonal { get; set; }
        public string email { get; set; }
        public string observaciones { get; set; }
    }
}
