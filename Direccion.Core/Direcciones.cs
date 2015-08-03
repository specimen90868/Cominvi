using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Direccion.Core
{
    public class Direcciones
    {
        public int iddireccion { get; set; }
        public int idpersona { get; set; }
        public string calle { get; set; }
        public string exterior { get; set; }
        public string interior { get; set; }
        public string colonia { get; set; }
        public string cp { get; set; }
        public string ciudad { get; set; }
        public string estado { get; set; }
        public string pais { get; set; }
        public int tipodireccion { get; set; }
        public int tipopersona { get; set; }
    }
}
