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
            Command.CommandText = "insert into suaReingresos (idtrabajador, idempresa, registropatronal, nss, fechaingreso, diasproporcionales, sdi, periodoinicio, periodofin) " +
                "values (@idtrabajador, @idempresa, @registropatronal, @nss, @fechaingreso, @diasproporcionales, @sdi, @periodoinicio, @periodofin)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador",r.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", r.idempresa);
            Command.Parameters.AddWithValue("registropatronal", r.registropatronal);
            Command.Parameters.AddWithValue("nss", r.nss);
            Command.Parameters.AddWithValue("fechaingreso", r.fechaingreso);
            Command.Parameters.AddWithValue("diasproporcionales", r.diasproporcionales);
            Command.Parameters.AddWithValue("sdi", r.sdi);
            Command.Parameters.AddWithValue("periodoinicio", r.periodoinicio);
            Command.Parameters.AddWithValue("periodofin", r.periodofin);
            return Command.ExecuteNonQuery();
        }

        public object existeReingreso(Reingresos r)
        {
            Command.CommandText = "select count(*) from suaReingresos where idtrabajador = @idtrabajador and periodoinicio = @periodoinicio and periodofin = @periodofin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", r.idtrabajador);
            Command.Parameters.AddWithValue("periodoinicio", r.periodoinicio);
            Command.Parameters.AddWithValue("periodofin", r.periodofin);
            object dato = Select(Command);
            return dato;
        }

        public object diasProporcionales(Reingresos r)
        {
            Command.CommandText = "select diasproporcionales from suaReingresos where idtrabajador = @idtrabajador and periodoinicio = @periodoinicio and periodofin = @periodofin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", r.idtrabajador);
            Command.Parameters.AddWithValue("periodoinicio", r.periodoinicio);
            Command.Parameters.AddWithValue("periodofin", r.periodofin);
            object dato = Select(Command);
            return dato;
        }

        public object fechaReingreso(Reingresos r)
        {
            Command.CommandText = "select fechaingreso from suaReingresos where idtrabajador = @idtrabajador and periodoinicio = @inicio and periodofin = @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", r.idtrabajador);
            Command.Parameters.AddWithValue("inicio", r.periodoinicio);
            Command.Parameters.AddWithValue("fin", r.periodofin);
            object dato = Select(Command);
            return dato;
        }
    }
}
