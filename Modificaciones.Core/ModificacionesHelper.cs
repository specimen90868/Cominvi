using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modificaciones.Core
{
    public class ModificacionesHelper : Data.Obj.DataObj
    {
        public List<Modificaciones> obtieneModificaciones(Modificaciones m)
        {
            List<Modificaciones> lstModificaciones = new List<Modificaciones>();
            DataTable dtMod = new DataTable();
            Command.CommandText = "select * from suaModificaciones where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", m.idempresa);
            dtMod = SelectData(Command);
            for (int i = 0; i < dtMod.Rows.Count; i++)
            {
                Modificaciones mod = new Modificaciones();
                mod.id = int.Parse(dtMod.Rows[i]["id"].ToString());
                mod.idtrabajador = int.Parse(dtMod.Rows[i]["idtrabajador"].ToString());
                mod.idempresa = int.Parse(dtMod.Rows[i]["idempresa"].ToString());
                mod.registropatronal = dtMod.Rows[i]["registropatronal"].ToString();
                mod.nss = dtMod.Rows[i]["nss"].ToString();
                mod.fecha = DateTime.Parse(dtMod.Rows[i]["fecha"].ToString());
                mod.sdi = decimal.Parse(dtMod.Rows[i]["sdi"].ToString());
                lstModificaciones.Add(mod);
            }
            return lstModificaciones;
        }

        public int insertaModificacion(Modificaciones m)
        {
            Command.CommandText = "insert into suaModificaciones (idtrabajador, idempresa, registropatronal, nss, fecha, sdi) " +
                "values (@idtrabajador, @idempresa, @registropatronal, @nss, @fecha, @sdi)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador",m.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", m.idempresa);
            Command.Parameters.AddWithValue("registropatronal", m.registropatronal);
            Command.Parameters.AddWithValue("nss", m.nss);
            Command.Parameters.AddWithValue("fecha", m.fecha);
            Command.Parameters.AddWithValue("sdi", m.sdi);
            return Command.ExecuteNonQuery();
        }
    }
}
