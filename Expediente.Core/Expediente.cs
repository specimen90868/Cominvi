using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expediente.Core
{
    public class Expediente
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public bool contrato { get; set; }
        public bool solicitud { get; set; }
        public bool altaimss { get; set; }
        public bool credencial { get; set; }
        public bool actanacimiento { get; set; }
        public bool ife { get; set; }
        public bool curp { get; set; }
        public bool cdomicilio { get; set; }
        public bool nss { get; set; }
        public bool rfc { get; set; }
        public bool infonavit { get; set; }
        public bool afore { get; set; }
        public bool fotografias { get; set; }
        public bool autorizacion { get; set; }
        public bool estatus { get; set; }
        public bool observacion { get; set; }
    }
}
