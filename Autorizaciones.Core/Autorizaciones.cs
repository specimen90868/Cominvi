using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizaciones.Core
{
    public class Autorizaciones
    {
        public int idusuario { get; set; }
        public string usuario { get; set; }
        public int idperfil { get; set; }
        public string nombre { get; set; }
        public string modulo { get; set; }
        public bool acceso { get; set; }
    }

    public class Menus
    {
        public string nombre { get; set; }
        public bool ver { get; set; }
    }

    public class Ediciones
    {
        public string nombre { get; set; }
        public bool crear { get; set; }
        public bool consulta { get; set; }
        public bool modificar { get; set; }
        public bool baja { get; set; }
    }

    public class Autorizacion
    {
        public int id { get; set; }
        public int idacceso { get; set; }
        public int idperfil { get; set; }
        public int acceso { get; set; }
    }
}
