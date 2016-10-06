using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonavit.Core
{
    public class Infonavit
    {
        public int idinfonavit { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public string credito { get; set; }
        public int descuento { get; set; }
        public decimal valordescuento { get; set; }
        public bool activo { get; set; }
        public string descripcion { get; set; }
        public int dias { get; set; }
        public DateTime fecha { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fin { get; set; }
        public DateTime registro { get; set; }
        public int idusuario { get; set; }
        public int estatus { get; set; }
    }

    public class suaInfonavit
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public string registropatronal { get; set; }
        public string nss { get; set; }
        public string credito { get; set; }
        public int modificacion { get; set; }
        public DateTime fecha { get; set; }
        public int descuento { get; set; }
        public decimal valor { get; set; }
    }
}
