using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogos.Core
{
    public class Catalogo
    {
        public int id { get; set; }
        public string valor { get; set; }
        public string descripcion { get; set; }
        public int grupo { get; set; }
        public string grupodescripcion { get; set; }
    }
}
