﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramacionConcepto.Core
{
    public class ProgramacionConcepto
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public int idconcepto { get; set; }
        public string concepto { get; set; }
        public double cantidad { get; set; }
        public DateTime fechafin { get; set; }
    }
}
