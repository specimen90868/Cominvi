﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Historial.Core
{
    public class Historial
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int tipomovimiento { get; set; }
        public double valor { get; set; }
        public DateTime fecha_imss { get; set; }
        public DateTime fecha_sistema { get; set; }
        public int idempresa { get; set; }
        public int motivobaja { get; set; }
    }
}
