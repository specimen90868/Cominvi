using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoNomina.Core
{
    public class Nomina
    {
        public int idtrabajador { get; set; }
        public int dias { get; set; }
        public decimal salariominimo { get; set; }
        public int antiguedadmod { get; set; }
        public decimal sdi { get; set; }
        public decimal sd { get; set; }
        public int id { get; set; }
        public int noconcepto { get; set; }
        public string concepto { get; set; }
        public string tipoconcepto { get; set; }
        public string formula { get; set; }
        public string formulaexento { get; set; }
        public bool modificado { get; set; }
    }

    public class DatosEmpleado
    {
        public int idtrabajador { get; set; }
        public int iddepartamento { get; set; }
        public int idpuesto { get; set; }
        public string noempleado { get; set; }
        public string nombres { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public decimal sueldo { get; set; }
        public decimal despensa { get; set; }
        public decimal asistencia { get; set; }
        public decimal puntualidad { get; set; }
        public decimal horas { get; set; }
    }

    public class tmpPagoNomina
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public int idconcepto { get; set; }
        public int noconcepto { get; set; }
        public string tipoconcepto { get; set; }
        public decimal exento { get; set; }
        public decimal gravado { get; set; }
        public decimal cantidad { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public int diaslaborados { get; set; }
        public bool guardada { get; set; }
        public int tiponomina { get; set; }
        public bool modificado { get; set; }
        public bool obracivil { get; set; }
        public int periodo { get; set; }
        public int iddepartamento { get; set; }
        public int idpuesto { get; set; }
        public int anio { get; set; }
    }

    public class DatosFaltaIncapacidad
    {
        public int idtrabajador { get; set; }
        public int iddepartamento { get; set; }
        public int idpuesto { get; set; }
        public string noempleado { get; set; }
        public string nombres { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
    }

    public class NetosNegativos
    {
        public int idtrabajador { get; set; }
        public string noempleado { get; set; }
        public string nombrecompleto { get; set; }
        public int noconcepto { get; set; }
        public string tipoconcepto { get; set; }
        public string concepto { get; set; }
        public decimal cantidad { get; set; }
    }

    public class CodigoBidimensional
    {
        public string re { get; set; }
        public int idtrabajador { get; set; }
        public string rr { get; set; }
        public decimal tt { get; set; }
        public string uuid { get; set; }
    }

    public class XmlCabecera
    {
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public string xml { get; set; }
    }

    public class PagoNomina
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public int idconcepto { get; set; }
        public int noconcepto { get; set; }
        public string tipoconcepto { get; set; }
        public decimal exento { get; set; }
        public decimal gravado { get; set; }
        public decimal cantidad { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public int noperiodo { get; set; }
        public int diaslaborados { get; set; }
        public int idusuario { get; set; }
        public int tiponomina { get; set; }
        public DateTime fechapago { get; set; }
        public int iddepartamento { get; set; }
        public int idpuesto { get; set; }
        public int anio { get; set; }
    }

    public class NominaTabular
    {
        public string noempleado { get; set; }
        public string concepto { get; set; }
        public decimal cantidad { get; set; }
    }
}
