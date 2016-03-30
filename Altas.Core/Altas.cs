using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altas.Core
{
    public class Altas
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public string registropatronal { get; set; }
        public string nss { get; set; }
        public string rfc { get; set; }
        public string curp { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string nombre { get; set; }
        public int contrato { get; set; }
        public int jornada { get; set; }
        public DateTime fechaingreso { get; set; }
        public int diasproporcionales { get; set; }
        public decimal sdi { get; set; }
        public string cp { get; set; }
        public DateTime fechanacimiento { get; set; }
        public string estado { get; set; }
        public int noestado { get; set; }
        public string clinica { get; set; }
        public string sexo { get; set; }
        public DateTime periodoInicio { get; set; }
        public DateTime periodoFin { get; set; }
    }
}
