using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ausentismo.Core
{
    public class AusentismoHelper : Data.Obj.DataObj
    {
        public List<Ausentismo> obtenerAusentimos(Ausentismo a)
        {
            List<Ausentismo> lstAusentismo = new List<Ausentismo>();
            DataTable dtBajas = new DataTable();
            Command.CommandText = "select * from suaAusentismos where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", a.idempresa);
            dtBajas = SelectData(Command);
            for (int i = 0; i < dtBajas.Rows.Count; i++)
            {
                Ausentismo ausentimo = new Ausentismo();
                ausentimo.id = int.Parse(dtBajas.Rows[i]["id"].ToString());
                ausentimo.idtrabajador = int.Parse(dtBajas.Rows[i]["idtrabajador"].ToString());
                ausentimo.idempresa = int.Parse(dtBajas.Rows[i]["idempresa"].ToString());
                ausentimo.registropatronal = dtBajas.Rows[i]["registropatronal"].ToString();
                ausentimo.nss = dtBajas.Rows[i]["nss"].ToString();
                ausentimo.fecha_imss = DateTime.Parse(dtBajas.Rows[i]["fecha_imss"].ToString());
                ausentimo.dias = int.Parse(dtBajas.Rows[i]["dias"].ToString());
                lstAusentismo.Add(ausentimo);
            }
            return lstAusentismo;
        }

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
