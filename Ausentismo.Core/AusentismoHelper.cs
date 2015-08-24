using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ausentismo.Core
{
    public class AusentismoHelper : Data.Obj.DataObj
    {
        public int insertaAusentismo(Ausentismo a)
        {
            Command.CommandText = "insert into suaAusentismos (idtrabajador, idempresa, registropatronal, nss, fecha_imss, dias) values " +
                "(@idtrabajador,@idempresa,@registropatronal,@nss,@fecha_imss,@dias)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", a.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", a.idempresa);
            Command.Parameters.AddWithValue("registropatronal", a.registropatronal);
            Command.Parameters.AddWithValue("nss", a.nss);
            Command.Parameters.AddWithValue("fecha_imss",a.fecha_imss);
            Command.Parameters.AddWithValue("dias", a.dias);
            return Command.ExecuteNonQuery();
        }
    }
}
