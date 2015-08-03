using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Core
{
    public class Empresas
    {
        public int idempresa { get; set; }
        public string nombre { get; set; }
        public string rfc { get; set; }
        public string registro { get; set; }
        public int digitoverificador { get; set; }
        public int sindicato { get; set; }
        public string representante { get; set; }
        public int activo { get; set; }
    }
}
