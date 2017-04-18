using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Core
{
    public class IncidenciasHelper : Data.Obj.DataObj
    {
        public List<Incidencias> obtenerIndicencias(Incidencias i)
        {
            List<Incidencias> lstIncidencias = new List<Incidencias>();
            DataTable dtIncidencias = new DataTable();
            Command.CommandText = "select idtrabajador, idempresa, certificado, periodoinicio, periodofin, SUM(dias) as dias, inicioincapacidad, finincapacidad from Incidencias " +
                "where idempresa = @idempresa group by idtrabajador, idempresa, certificado, periodoinicio, periodofin, inicioincapacidad, finincapacidad";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", i.idempresa);
            dtIncidencias = SelectData(Command);
            for (int j = 0; j < dtIncidencias.Rows.Count; j++)
            {
                Incidencias incidencia = new Incidencias();
                incidencia.idtrabajador = int.Parse(dtIncidencias.Rows[j]["idtrabajador"].ToString());
                incidencia.idempresa = int.Parse(dtIncidencias.Rows[j]["idempresa"].ToString());
                incidencia.certificado = dtIncidencias.Rows[j]["certificado"].ToString();
                incidencia.periodoinicio = DateTime.Parse(dtIncidencias.Rows[j]["periodoinicio"].ToString());
                incidencia.periodofin = DateTime.Parse(dtIncidencias.Rows[j]["periodofin"].ToString());
                incidencia.dias = int.Parse(dtIncidencias.Rows[j]["dias"].ToString());
                incidencia.inicioincapacidad = DateTime.Parse(dtIncidencias.Rows[j]["inicioincapacidad"].ToString());
                incidencia.finincapacidad = DateTime.Parse(dtIncidencias.Rows[j]["finincapacidad"].ToString());
                lstIncidencias.Add(incidencia);
            }
            return lstIncidencias;
        }

        public List<Incidencias> obtenerIndicenciasTrabajador(Incidencias i)
        {
            List<Incidencias> lstIncidencias = new List<Incidencias>();
            DataTable dtIncidencias = new DataTable();
            Command.CommandText = @"select distinct certificado from Incidencias where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            dtIncidencias = SelectData(Command);
            for (int j = 0; j < dtIncidencias.Rows.Count; j++)
            {
                Incidencias incidencia = new Incidencias();
                incidencia.certificado = dtIncidencias.Rows[j]["certificado"].ToString();
                lstIncidencias.Add(incidencia);
            }
            return lstIncidencias;
        }

        public List<Incidencias> obtenerIndicenciaTrabajador(Incidencias i)
        {
            List<Incidencias> lstIncidencias = new List<Incidencias>();
            DataTable dtIncidencias = new DataTable();
            Command.CommandText = @"select certificado, sum(dias) as dias, inicioincapacidad, finincapacidad from Incidencias where idtrabajador = @idtrabajador 
                                    and certificado = @certificado group by certificado, inicioincapacidad, finincapacidad";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            Command.Parameters.AddWithValue("inicio", i.fechainicio);
            Command.Parameters.AddWithValue("fin", i.fechafin);
            Command.Parameters.AddWithValue("certificado", i.certificado);
            dtIncidencias = SelectData(Command);
            for (int j = 0; j < dtIncidencias.Rows.Count; j++)
            {
                Incidencias incidencia = new Incidencias();
                incidencia.certificado = dtIncidencias.Rows[j]["certificado"].ToString();
                incidencia.dias = int.Parse(dtIncidencias.Rows[j]["dias"].ToString());
                incidencia.inicioincapacidad = DateTime.Parse(dtIncidencias.Rows[j]["inicioincapacidad"].ToString());
                incidencia.finincapacidad = DateTime.Parse(dtIncidencias.Rows[j]["finincapacidad"].ToString());
                lstIncidencias.Add(incidencia);
            }
            return lstIncidencias;
        }

        public object existeIncidencia(Incidencias i)
        {
            Command.CommandText = "select coalesce(count(id),0) from incidencias where idtrabajador = @idtrabajador and fechainicio between @inicio and @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            Command.Parameters.AddWithValue("inicio", i.periodoinicio);
            Command.Parameters.AddWithValue("fin", i.periodofin);
            object dato = Select(Command);
            return dato;
        }

        public object existeIncidenciaBaja(Incidencias i)
        {
            Command.CommandText = "select coalesce(count(id),0) from incidencias where idtrabajador = @idtrabajador and fechainicio between @inicio and @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            Command.Parameters.AddWithValue("inicio", i.fechainicio);
            Command.Parameters.AddWithValue("fin", i.fechafin);
            object dato = Select(Command);
            return dato;
        }

        public object fechaInicio(Incidencias i)
        {
            Command.CommandText = "select fechainicio from incidencias where idtrabajador = @idtrabajador and fechainicio between @inicio and @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            Command.Parameters.AddWithValue("inicio", i.fechainicio);
            Command.Parameters.AddWithValue("fin", i.fechafin);
            object dato = Select(Command);
            return dato;
        }

        public object fechaFin(Incidencias i)
        {
            Command.CommandText = "select fechafin from incidencias where idtrabajador = @idtrabajador and fechainicio between @inicio and @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            Command.Parameters.AddWithValue("inicio", i.fechainicio);
            Command.Parameters.AddWithValue("fin", i.fechafin);
            object dato = Select(Command);
            return dato;
        }

        public List<Incidencias> finIncapacidad(int idtrabajador)
        {
            List<Incidencias> lstIncidencias = new List<Incidencias>();
            DataTable dt = new DataTable();
            Command.CommandText = @"select top 10 inicioincapacidad, finincapacidad from incidencias where idempresa = 2 and idtrabajador = @idtrabajador
                    group by inicioincapacidad, finincapacidad
                    order by inicioincapacidad desc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            dt = SelectData(Command);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Incidencias incidencia = new Incidencias();
                incidencia.inicioincapacidad = DateTime.Parse(dt.Rows[i]["inicioincapacidad"].ToString());
                incidencia.finincapacidad = DateTime.Parse(dt.Rows[i]["finincapacidad"].ToString());
                lstIncidencias.Add(incidencia);
            }
            return lstIncidencias;
        }

        public object diasIncidencia(Incidencias i)
        {
            Command.CommandText = "select isnull(sum(dias),0) as dias from incidencias where idtrabajador = @idtrabajador and fechainicio between @inicio and @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            Command.Parameters.AddWithValue("inicio", i.periodoinicio);
            Command.Parameters.AddWithValue("fin", i.periodofin);
            object dato = Select(Command);
            return dato;
        }

        public object diasIncidencia(int idtrabajador, DateTime inicio, DateTime fin)
        {
            Command.CommandText = "select coalesce(sum(dias),0) as dias from incidencias where idtrabajador = @idtrabajador and fechainicio = @inicio and fechafin = @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            Command.Parameters.AddWithValue("inicio", inicio);
            Command.Parameters.AddWithValue("fin", fin);
            object dato = Select(Command);
            return dato;
        }

        public object existeIncidenciaEnFalta(int id, DateTime fecha)
        {
            Command.CommandText = "select count(*) from incidencias where idtrabajador = @idtrabajador and fechafin >= @fecha and fechainicio <= @fecha";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", id);
            Command.Parameters.AddWithValue("fecha", fecha);
            object dato = Select(Command);
            return dato;
        }

        public object existeCertificado(Incidencias i)
        {
            Command.CommandText = "select coalesce(count(id),0) from incidencias where certificado = @certificado";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("certificado", i.certificado);
            object dato = Select(Command);
            return dato;
        }

        public object eliminaIncidencia(Incidencias i)
        {
            Command.CommandText = "delete from incidencias where idtrabajador = @idtrabajador and certificado = @certificado";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            Command.Parameters.AddWithValue("certificado", i.certificado);
            object dato = Select(Command);
            return dato;
        }

        public void bulkIncidencia(DataTable dt, string tabla)
        {
            bulkCommand.DestinationTableName = tabla;
            bulkCommand.WriteToServer(dt);
            dt.Clear();
        }

        public int stpIncidencia(DateTime inicio, DateTime fin)
        {
            Command.CommandText = "exec stp_InsertaIncidencias @inicio, @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("inicio", inicio);
            Command.Parameters.AddWithValue("fin", fin);
            return Command.ExecuteNonQuery();
        }
    }
}
