using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatCatalogos.Core
{
    public class satEstados
    {
        public int idestado { get; set; }
        public string estado { get; set; }
        public string pais { get; set; }
        public string nombreestado { get; set; }
    }

    public class satMetodoPago
    {
        public int idmetodopago { get; set; }
        public string clave { get; set; }
        public string descripcion { get; set; }
    }

    public class satRegimenFiscal
    {
        public int id { get; set; }
        public string regimenfiscal { get; set; }
        public string descripcion { get; set; }
    }

    public class satRiesgoPuesto
    {
        public int id { get; set; }
        public string riesgopuesto { get; set; }
        public string descripcion { get; set; }
    }

    public class satTipoContrato
    {
        public int id { get; set; }
        public string tipocontrato { get; set; }
        public string descripcion { get; set; }
    }

    public class satTipoRegimen
    {
        public int idregimen { get; set; }
        public string tiporegimen { get; set; }
        public string descripcion { get; set; }
    }

    public class satTipoDeduccion 
    {
        public int id { get; set; }
        public string tipodeduccion { get; set; }
        public string descripcion { get; set; }
    }

    public class satTipoPercepcion
    {
        public int id { get; set; }
        public string tipopercepcion { get; set; }
        public string descripcion { get; set; }
    }
}
