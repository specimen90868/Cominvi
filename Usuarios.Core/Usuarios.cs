using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios.Core
{
    public class Usuarios
    {
        public int idusuario { get; set; }
        public string usuario { get; set; }
        public string nombre { get; set; }
        public string password { get; set; }
        public int activo { get; set; }
        public int plaza { get; set; }
    }
}
