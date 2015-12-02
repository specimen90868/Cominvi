﻿using System;
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
        public double salariominimo { get; set; }
        public int antiguedadmod { get; set; }
        public double sdi { get; set; }
        public double sd { get; set; }
        public int id { get; set; }
        public int noconcepto { get; set; }
        public string concepto { get; set; }
        public string tipoconcepto { get; set; }
        public string formula { get; set; }
        public string formulaexento { get; set; }
    }

    public class DatosEmpleado
    {
        public bool chk { get; set; }
        public int idtrabajador { get; set; }
        public int iddepartamento { get; set; }
        public int idpuesto { get; set; }
        public string noempleado { get; set; }
        public string nombres { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public double sueldo { get; set; }
        public double despensa { get; set; }
        public double asistencia { get; set; }
        public double puntualidad { get; set; }
        public double horas { get; set; }
    }

    public class tmpPagoNomina
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public int idconcepto { get; set; }
        public int noconcepto { get; set; }
        public string tipoconcepto { get; set; }
        public double exento { get; set; }
        public double gravado { get; set; }
        public double cantidad { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public bool guardada { get; set; }
        public int tiponomina { get; set; }
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
        public int falta { get; set; }
        public int incapacidad { get; set; }
    }
}
