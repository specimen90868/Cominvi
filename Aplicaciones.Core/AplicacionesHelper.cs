﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicaciones.Core
{
    public class AplicacionesHelper : Data.Obj.DataObj
    {
        public int insertaAplicacion(Aplicaciones a)
        {
            Command.CommandText = @"insert into Aplicaciones (idtrabajador, idempresa, iddeptopuesto, deptopuesto, fecha, registro, idusuario, periodoinicio, periodofin) values (
                                    @idtrabajador,@idempresa,@iddeptopuesto,@deptopuesto,@fecha,@registro,@idusuario, @periodoinicio, @periodofin)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", a.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", a.idempresa);
            Command.Parameters.AddWithValue("iddeptopuesto", a.iddeptopuesto);
            Command.Parameters.AddWithValue("deptopuesto", a.deptopuesto);
            Command.Parameters.AddWithValue("fecha", a.fecha);
            Command.Parameters.AddWithValue("registro", a.registro);
            Command.Parameters.AddWithValue("idusuario", a.idusuario);
            Command.Parameters.AddWithValue("periodoinicio", a.periodoinicio);
            Command.Parameters.AddWithValue("periodofin", a.periodofin);
            return Command.ExecuteNonQuery();
        }

        public List<Aplicaciones> obtenerFechasDeAplicacion()
        {
            DataTable dt = new DataTable();
            Command.CommandText = @"select * from Aplicaciones";
            dt = SelectData(Command);
            List<Aplicaciones> lstAplicaciones = new List<Aplicaciones>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Aplicaciones ap = new Aplicaciones();
                ap.id = int.Parse(dt.Rows[i]["id"].ToString());
                ap.idtrabajador = int.Parse(dt.Rows[i]["idtrabajador"].ToString());
                ap.idempresa = int.Parse(dt.Rows[i]["idempresa"].ToString());
                ap.iddeptopuesto = int.Parse(dt.Rows[i]["iddeptopuesto"].ToString());
                ap.deptopuesto = dt.Rows[i]["deptopuesto"].ToString();
                ap.fecha = DateTime.Parse(dt.Rows[i]["fecha"].ToString());
                ap.registro = DateTime.Parse(dt.Rows[i]["registro"].ToString());
                ap.idusuario = int.Parse(dt.Rows[i]["idusuario"].ToString());
                ap.periodoinicio = DateTime.Parse(dt.Rows[i]["periodoinicio"].ToString());
                ap.periodofin = DateTime.Parse(dt.Rows[i]["periodofin"].ToString());
                lstAplicaciones.Add(ap);
            }
            return lstAplicaciones;
        }

        public int eliminaAplicacion(int id)
        {
            Command.CommandText = @"delete from Aplicaciones where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", id);
            return Command.ExecuteNonQuery();
        }

    }
}
