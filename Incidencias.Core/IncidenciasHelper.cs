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
            Command.CommandText = "select idtrabajador, idempresa, certificado, periodoinicio, periodofin from Incidencias " +
                "where idempresa = @idempresa group by idtrabajador, idempresa, certificado, periodoinicio, periodofin";
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

        public object diasIncidencia(Incidencias i)
        {
            Command.CommandText = "select dias from incidencias where idtrabajador = @idtrabajador and fechainicio between @inicio and @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", i.idtrabajador);
            Command.Parameters.AddWithValue("inicio", i.periodoinicio);
            Command.Parameters.AddWithValue("fin", i.periodofin);
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

        public int stpIncidencia()
        {
            Command.CommandText = "exec stp_InsertaIncidencias";
            Command.Parameters.Clear();
            return Command.ExecuteNonQuery();
        }
    }
}
