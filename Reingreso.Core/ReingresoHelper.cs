using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reingreso.Core
{
    public class ReingresoHelper : Data.Obj.DataObj
    {
        public List<Reingresos> obtenerReingresos(Reingresos r)
        {
            List<Reingresos> lstReingresos = new List<Reingresos>();
            DataTable dtReingresos = new DataTable();
            Command.CommandText = "select * from suaReingresos where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", r.idempresa);
            dtReingresos = SelectData(Command);
            for (int i = 0; i < dtReingresos.Rows.Count; i++)
            {
                Reingresos reingreso = new Reingresos();
                reingreso.id = int.Parse(dtReingresos.Rows[i]["id"].ToString());
                reingreso.idtrabajador = int.Parse(dtReingresos.Rows[i]["idtrabajador"].ToString());
                reingreso.idempresa = int.Parse(dtReingresos.Rows[i]["idempresa"].ToString());
                reingreso.registropatronal = dtReingresos.Rows[i]["registropatronal"].ToString();
                reingreso.nss = dtReingresos.Rows[i]["nss"].ToString();
                reingreso.fechaingreso = DateTime.Parse(dtReingresos.Rows[i]["fechaingreso"].ToString());
                reingreso.sdi = double.Parse(dtReingresos.Rows[i]["sdi"].ToString());
                lstReingresos.Add(reingreso);
            }
            return lstReingresos;
        }

        public int insertaReingreso(Reingresos r)
        {
            Command.CommandText = "insert into suaReingresos (idtrabajador, idempresa, registropatronal, nss, fechaingreso, sdi) " +
                "values (@idtrabajador, @idempresa, @registropatronal, @nss, @fechaingreso, @sdi)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador",r.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", r.idempresa);
            Command.Parameters.AddWithValue("registropatronal", r.registropatronal);
            Command.Parameters.AddWithValue("nss", r.nss);
            Command.Parameters.AddWithValue("fechaingreso", r.fechaingreso);
            Command.Parameters.AddWithValue("sdi", r.sdi);
            return Command.ExecuteNonQuery();
        }
    }
}
