using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conceptos.Core
{
    public class Conceptos
    {
        public int id { get; set; }
        public int idempresa { get; set; }
        public int noconcepto { get; set; }
        public string concepto { get; set; }
        public string tipoconcepto { get; set; }
        public string formula { get; set; }
        public string formulaexento { get; set; }
        public bool gravado { get; set; }
        public bool exento { get; set; }
        public string gruposat { get; set; }
        public bool visible { get; set; }
        public int periodo { get; set; }
    }

    public class ConceptoTrabajador
    {
        public int id { get; set; }
        public int idempleado { get; set; }
        public int idconcepto { get; set; }
    }

    public class ConceptosEmpresa
    {
        public int id { get; set; }
        public int idempresa { get; set; }
        public int idconcepto { get; set; }
        public int noconcepto { get; set; }
        public bool asignacion { get; set; }
        public int periodo { get; set; }
    }
}
