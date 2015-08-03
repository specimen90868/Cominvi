using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagen.Core
{
    public class Imagenes
    {
        public int idimagen { get; set; }
        public int idpersona { get; set; }
        public byte[] imagen { get; set; }
        public int tipopersona { get; set; }
    }
}
