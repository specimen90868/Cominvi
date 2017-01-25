using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bajas.Core
{
    public class BajasHelper : Data.Obj.DataObj
    {
        public List<Bajas> obtenerBajas(Bajas a)
        {
            List<Bajas> lstBaja = new List<Bajas>();
            DataTable dtBajas = new DataTable();
            Command.CommandText = "select * from suaBajas where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa",a.idempresa);
            dtBajas = SelectData(Command);
            for (int i = 0; i < dtBajas.Rows.Count; i++)
            {
                Bajas baja = new Bajas();
                baja.id = int.Parse(dtBajas.Rows[i]["id"].ToString());
                baja.idtrabajador = int.Parse(dtBajas.Rows[i]["idtrabajador"].ToString());
                baja.idempresa = int.Parse(dtBajas.Rows[i]["idempresa"].ToString());
                baja.registropatronal = dtBajas.Rows[i]["registropatronal"].ToString();
                baja.nss = dtBajas.Rows[i]["nss"].ToString();
                baja.motivo = int.Parse(dtBajas.Rows[i]["motivo"].ToString());
                baja.fecha = DateTime.Parse(dtBajas.Rows[i]["fecha"].ToString());
                baja.observaciones = dtBajas.Rows[i]["observaciones"].ToString();
                lstBaja.Add(baja);
            }
            return lstBaja;
        }

        public List<Bajas> obtenerBaja(Bajas a)
        {
            List<Bajas> lstBaja = new List<Bajas>();
            DataTable dtBajas = new DataTable();
            Command.CommandText = @"select fecha from suaBajas where idempresa = @idempresa and idtrabajador = @idtrabajador
                and periodoinicio = @periodoinicio and periodofin = @periodofin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", a.idempresa);
            Command.Parameters.AddWithValue("idtrabajador", a.idtrabajador);
            Command.Parameters.AddWithValue("periodoinicio", a.periodoinicio);
            Command.Parameters.AddWithValue("periodofin", a.periodofin);
            dtBajas = SelectData(Command);
            for (int i = 0; i < dtBajas.Rows.Count; i++)
            {
                Bajas baja = new Bajas();
                baja.fecha = DateTime.Parse(dtBajas.Rows[i]["fecha"].ToString());
                lstBaja.Add(baja);
            }
            return lstBaja;
        }

        public List<Bajas> obtenerBaja(int id)
        {
            List<Bajas> lstBaja = new List<Bajas>();
            DataTable dtBajas = new DataTable();
            Command.CommandText = @"select * from suaBajas where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", id);
            dtBajas = SelectData(Command);
            for (int i = 0; i < dtBajas.Rows.Count; i++)
            {
                Bajas baja = new Bajas();
                baja.id = int.Parse(dtBajas.Rows[i]["id"].ToString());
                baja.idtrabajador = int.Parse(dtBajas.Rows[i]["idtrabajador"].ToString());
                baja.idempresa = int.Parse(dtBajas.Rows[i]["idempresa"].ToString());
                baja.registropatronal = dtBajas.Rows[i]["registropatronal"].ToString();
                baja.nss = dtBajas.Rows[i]["nss"].ToString();
                baja.motivo = int.Parse(dtBajas.Rows[i]["motivo"].ToString());
                baja.fecha = DateTime.Parse(dtBajas.Rows[i]["fecha"].ToString());
                baja.observaciones = dtBajas.Rows[i]["observaciones"].ToString();
                baja.periodoinicio = DateTime.Parse(dtBajas.Rows[i]["periodoinicio"].ToString());
                baja.periodofin = DateTime.Parse(dtBajas.Rows[i]["periodofin"].ToString());
                lstBaja.Add(baja);
            }
            return lstBaja;
        }

        public string obtenerObservaciones(int id)
        {
            Command.CommandText = "select observaciones from suaBajas where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", id);
            return Select(Command).ToString();
        }

        public object existeBaja(Bajas b)
        {
            Command.CommandText = "select count(*) from suaBajas where idtrabajador = @idtrabajador and periodoinicio = @periodoinicio and periodofin = @periodofin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", b.idtrabajador);
            Command.Parameters.AddWithValue("periodoinicio", b.periodoinicio);
            Command.Parameters.AddWithValue("periodofin", b.periodofin);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerFechaBaja(Bajas b)
        { 
            Command.CommandText = @"select top 1 fecha from suaBajas where idempresa = @idempresa and idtrabajador = @idtrabajador order by fecha desc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", b.idempresa);
            Command.Parameters.AddWithValue("idtrabajador", b.idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public object diasProporcionales(Bajas b)
        {
            Command.CommandText = "select sum(diasproporcionales) as diasproporcionales from suaBajas where idtrabajador = @idtrabajador and periodoinicio = @periodoinicio and periodofin = @periodofin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", b.idtrabajador);
            Command.Parameters.AddWithValue("periodoinicio", b.periodoinicio);
            Command.Parameters.AddWithValue("periodofin", b.periodofin);
            object dato = Select(Command);
            return dato;
        }

        public int insertaBaja(Bajas a)
        {
            Command.CommandText = "insert into suaBajas (idtrabajador, idempresa, registropatronal, nss, motivo, fecha, diasproporcionales, periodoinicio, periodofin, observaciones, registro) " +
                "values (@idtrabajador, @idempresa, @registropatronal, @nss, @motivo, @fecha, @dias, @inicio, @fin, @observaciones, @registro)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador",a.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", a.idempresa);
            Command.Parameters.AddWithValue("registropatronal", a.registropatronal);
            Command.Parameters.AddWithValue("nss", a.nss);
            Command.Parameters.AddWithValue("motivo", a.motivo);
            Command.Parameters.AddWithValue("fecha", a.fecha);
            Command.Parameters.AddWithValue("dias", a.diasproporcionales);
            Command.Parameters.AddWithValue("inicio", a.periodoinicio);
            Command.Parameters.AddWithValue("fin", a.periodofin);
            Command.Parameters.AddWithValue("observaciones", a.observaciones);
            Command.Parameters.AddWithValue("registro", a.registro);
            return Command.ExecuteNonQuery();
        }

        public int bajaEmpleado(Bajas b)
        {
            Command.CommandText = "update trabajadores set estatus = 0 where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", b.idtrabajador);
            return Command.ExecuteNonQuery();
        }

        public int eliminaBaja(Bajas b)
        {
            Command.CommandText = "delete from suaBajas where idtrabajador = @idtrabajador and idempresa = @idempresa and fecha = @fecha";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", b.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", b.idempresa);
            Command.Parameters.AddWithValue("fecha", b.fecha);
            return Command.ExecuteNonQuery();
        }

        public object fechaBaja(Bajas b)
        {
            Command.CommandText = "select fecha from suaBajas where idtrabajador = @idtrabajador and periodoinicio = @inicio and periodofin = @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", b.idtrabajador);
            Command.Parameters.AddWithValue("inicio", b.periodoinicio);
            Command.Parameters.AddWithValue("fin", b.periodofin);
            object dato = Select(Command);
            return dato;
        }
    }
}
