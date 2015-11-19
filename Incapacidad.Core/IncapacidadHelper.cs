using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapacidad.Core
{
    public class IncapacidadHelper : Data.Obj.DataObj
    {
        public List<Incapacidades> obtenerIncapacidades(Incapacidades inc)
        {
            List<Incapacidades> lstIncapacidades = new List<Incapacidades>();
            DataTable dtIncapacidad = new DataTable();
            Command.CommandText = "select * from Incapacidades where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", inc.idempresa);
            Command.Parameters.AddWithValue("fechainicio", inc.fechainicio);
            Command.Parameters.AddWithValue("fechafin", inc.fechafin);
            dtIncapacidad = SelectData(Command);
            for (int i = 0; i < dtIncapacidad.Rows.Count; i++)
            {
                Incapacidades incapacidad = new Incapacidades();
                incapacidad.id = int.Parse(dtIncapacidad.Rows[i]["id"].ToString());
                incapacidad.idtrabajador = int.Parse(dtIncapacidad.Rows[i]["idtrabajador"].ToString());
                incapacidad.idempresa = int.Parse(dtIncapacidad.Rows[i]["idempresa"].ToString());
                incapacidad.diasincapacidad = int.Parse(dtIncapacidad.Rows[i]["diasincapacidad"].ToString());
                incapacidad.diastomados = int.Parse(dtIncapacidad.Rows[i]["diastomados"].ToString());
                incapacidad.diasrestantes = int.Parse(dtIncapacidad.Rows[i]["diasrestantes"].ToString());
                incapacidad.diasapagar = int.Parse(dtIncapacidad.Rows[i]["diasapagar"].ToString());
                incapacidad.tipo = int.Parse(dtIncapacidad.Rows[i]["tipo"].ToString());
                incapacidad.aplicada = int.Parse(dtIncapacidad.Rows[i]["aplicada"].ToString());
                incapacidad.consecutiva = int.Parse(dtIncapacidad.Rows[i]["consecutiva"].ToString());
                incapacidad.fechainicio = DateTime.Parse(dtIncapacidad.Rows[i]["fechainicio"].ToString());
                incapacidad.fechafin = DateTime.Parse(dtIncapacidad.Rows[i]["fechafin"].ToString());
                lstIncapacidades.Add(incapacidad);
            }
            return lstIncapacidades;
        }

        public List<Incapacidades> obtenerIncapacidades(int idempresa)
        {
            List<Incapacidades> lstIncapacidades = new List<Incapacidades>();
            DataTable dtIncapacidad = new DataTable();
            Command.CommandText = "select * from Incapacidades where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            dtIncapacidad = SelectData(Command);
            for (int i = 0; i < dtIncapacidad.Rows.Count; i++)
            {
                Incapacidades incapacidad = new Incapacidades();
                incapacidad.id = int.Parse(dtIncapacidad.Rows[i]["id"].ToString());
                incapacidad.idtrabajador = int.Parse(dtIncapacidad.Rows[i]["idtrabajador"].ToString());
                incapacidad.idempresa = int.Parse(dtIncapacidad.Rows[i]["idempresa"].ToString());
                incapacidad.diasincapacidad = int.Parse(dtIncapacidad.Rows[i]["diasincapacidad"].ToString());
                incapacidad.diastomados = int.Parse(dtIncapacidad.Rows[i]["diastomados"].ToString());
                incapacidad.diasrestantes = int.Parse(dtIncapacidad.Rows[i]["diasrestantes"].ToString());
                incapacidad.diasapagar = int.Parse(dtIncapacidad.Rows[i]["diasapagar"].ToString());
                incapacidad.tipo = int.Parse(dtIncapacidad.Rows[i]["tipo"].ToString());
                incapacidad.aplicada = int.Parse(dtIncapacidad.Rows[i]["aplicada"].ToString());
                incapacidad.consecutiva = int.Parse(dtIncapacidad.Rows[i]["consecutiva"].ToString());
                incapacidad.fechainicio = DateTime.Parse(dtIncapacidad.Rows[i]["fechainicio"].ToString());
                incapacidad.fechafin = DateTime.Parse(dtIncapacidad.Rows[i]["fechafin"].ToString());
                lstIncapacidades.Add(incapacidad);
            }
            return lstIncapacidades;
        }

        public List<Incapacidades> obtenerIncapacidad(Incapacidades inc)
        {
            List<Incapacidades> lstIncapacidad = new List<Incapacidades>();
            DataTable dtIncapacidad = new DataTable();
            Command.CommandText = "select * from Incapacidades where idempresa = @idempresa and id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", inc.idempresa);
            Command.Parameters.AddWithValue("id", inc.id);
            dtIncapacidad = SelectData(Command);
            for (int i = 0; i < dtIncapacidad.Rows.Count; i++)
            {
                Incapacidades incapacidad = new Incapacidades();
                incapacidad.id = int.Parse(dtIncapacidad.Rows[i]["id"].ToString());
                incapacidad.idtrabajador = int.Parse(dtIncapacidad.Rows[i]["idtrabajador"].ToString());
                incapacidad.idempresa = int.Parse(dtIncapacidad.Rows[i]["idempresa"].ToString());
                incapacidad.diasincapacidad = int.Parse(dtIncapacidad.Rows[i]["diasincapacidad"].ToString());
                incapacidad.diastomados = int.Parse(dtIncapacidad.Rows[i]["diastomados"].ToString());
                incapacidad.diasrestantes = int.Parse(dtIncapacidad.Rows[i]["diasrestantes"].ToString());
                incapacidad.diasapagar = int.Parse(dtIncapacidad.Rows[i]["diasapagar"].ToString());
                incapacidad.tipo = int.Parse(dtIncapacidad.Rows[i]["tipo"].ToString());
                incapacidad.aplicada = int.Parse(dtIncapacidad.Rows[i]["aplicada"].ToString());
                incapacidad.consecutiva = int.Parse(dtIncapacidad.Rows[i]["consecutiva"].ToString());
                incapacidad.fechainicio = DateTime.Parse(dtIncapacidad.Rows[i]["fechainicio"].ToString());
                incapacidad.fechafin = DateTime.Parse(dtIncapacidad.Rows[i]["fechafin"].ToString());
                lstIncapacidad.Add(incapacidad);
            }
            return lstIncapacidad;
        }

        public object existeIncapacidad(Incapacidades inc)
        {
            Command.CommandText = "select coalesce(SUM(diastomados),0) as diastomados from incapacidades where idtrabajador = @idtrabajador and fechainicio = @fechainicio and  fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", inc.idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", inc.fechainicio);
            Command.Parameters.AddWithValue("fechafin", inc.fechafin);
            object dato = Select(Command);
            return dato;
        }

        public int insertaIncapacidad(Incapacidades inc)
        {
            Command.CommandText = "insert into Incapacidades (idtrabajador, idempresa, diasincapacidad, diastomados, diasrestantes, diasapagar, tipo, aplicada, consecutiva, fechainicio, fechafin) " +
                "values (@idtrabajador, @idempresa, @diasincapacidad, @diastomados, @diasrestantes, @diasapagar, @tipo, @aplicada, @consecutiva, @fechainicio, @fechafin)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", inc.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", inc.idempresa);
            Command.Parameters.AddWithValue("diasincapacidad", inc.diasincapacidad);
            Command.Parameters.AddWithValue("diastomados", inc.diastomados);
            Command.Parameters.AddWithValue("diasrestantes", inc.diasrestantes);
            Command.Parameters.AddWithValue("diasapagar", inc.diasapagar);
            Command.Parameters.AddWithValue("tipo", inc.tipo);
            Command.Parameters.AddWithValue("aplicada", inc.aplicada);
            Command.Parameters.AddWithValue("consecutiva", inc.consecutiva);
            Command.Parameters.AddWithValue("fechainicio", inc.fechainicio);
            Command.Parameters.AddWithValue("fechafin", inc.fechafin);
            return Command.ExecuteNonQuery();
        }

        public int actualizaIncapacidad(Incapacidades inc)
        {
            Command.CommandText = "update Incapacidades set diasincapacidad = @diasincapacidad, diastomados = @diastomados, diasrestantes = @diasrestantes, @diasapagar = @diasapagar, fechainicio = @fechainicio," +
                "fechafin = @fechafin where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", inc.id);
            Command.Parameters.AddWithValue("diasincapacidad", inc.diasincapacidad);
            Command.Parameters.AddWithValue("diastomados", inc.diastomados);
            Command.Parameters.AddWithValue("diasrestantes", inc.diasrestantes);
            Command.Parameters.AddWithValue("diasapagar", inc.diasapagar);
            Command.Parameters.AddWithValue("fechainicio", inc.fechainicio);
            Command.Parameters.AddWithValue("fechafin", inc.fechafin);
            return Command.ExecuteNonQuery();
        }

        public int eliminaIncapadidad(int idtrabajador, DateTime fechainicio, DateTime fechafin)
        {
            Command.CommandText = "delete from Incapacidades where idtrabajador = @idtrabajador and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", fechainicio);
            Command.Parameters.AddWithValue("fechafin", fechafin);
            return Command.ExecuteNonQuery();
        }

        public int eliminaIncapadidad(Incapacidades inc)
        {
            Command.CommandText = "delete from Incapacidades where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", inc.id);
            return Command.ExecuteNonQuery();
        }

        public void bulkIncapacidad(DataTable dt, string tabla)
        {
            bulkCommand.DestinationTableName = tabla;
            bulkCommand.WriteToServer(dt);
            dt.Clear();
        }

        public int stpIncapacidad(DateTime fechainicio, DateTime fechafin)
        {
            Command.CommandText = "exec stp_InsertaIncapacidades @fechainicio, @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("fechainicio", fechainicio);
            Command.Parameters.AddWithValue("fechafin", fechafin);
            return Command.ExecuteNonQuery();
        }
    }
}
