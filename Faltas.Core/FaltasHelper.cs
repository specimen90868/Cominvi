using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faltas.Core
{
    public class FaltasHelper : Data.Obj.DataObj
    {
        public List<Faltas> obtenerFaltas(Faltas f)
        {
            List<Faltas> lstFaltas = new List<Faltas>();
            DataTable dtFaltas = new DataTable();
            Command.CommandText = "select * from faltas where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", f.idempresa);
            Command.Parameters.AddWithValue("fechainicio", f.fechainicio);
            Command.Parameters.AddWithValue("fechafin", f.fechafin);
            dtFaltas = SelectData(Command);
            for (int i = 0; i < dtFaltas.Rows.Count; i++)
            {
                Faltas falta = new Faltas();
                falta.id = int.Parse(dtFaltas.Rows[i]["id"].ToString());
                falta.idtrabajador = int.Parse(dtFaltas.Rows[i]["idtrabajador"].ToString());
                falta.idempresa = int.Parse(dtFaltas.Rows[i]["idempresa"].ToString());
                falta.periodo = int.Parse(dtFaltas.Rows[i]["periodo"].ToString());
                falta.faltas = int.Parse(dtFaltas.Rows[i]["faltas"].ToString());
                falta.fechainicio = DateTime.Parse(dtFaltas.Rows[i]["fechainicio"].ToString());
                falta.fechafin = DateTime.Parse(dtFaltas.Rows[i]["fechafin"].ToString());
                falta.fecha = DateTime.Parse(dtFaltas.Rows[i]["fecha"].ToString());
                lstFaltas.Add(falta);
            }
            return lstFaltas;
        }

        public List<Faltas> obtenerFaltaTrabajador(Faltas f)
        {
            List<Faltas> lstFaltas = new List<Faltas>();
            DataTable dtFaltas = new DataTable();
            Command.CommandText = @"select * from faltas where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin
                                    and idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", f.idempresa);
            Command.Parameters.AddWithValue("fechainicio", f.fechainicio);
            Command.Parameters.AddWithValue("fechafin", f.fechafin);
            Command.Parameters.AddWithValue("idtrabajador", f.idtrabajador);
            dtFaltas = SelectData(Command);
            for (int i = 0; i < dtFaltas.Rows.Count; i++)
            {
                Faltas falta = new Faltas();
                falta.id = int.Parse(dtFaltas.Rows[i]["id"].ToString());
                falta.idtrabajador = int.Parse(dtFaltas.Rows[i]["idtrabajador"].ToString());
                falta.idempresa = int.Parse(dtFaltas.Rows[i]["idempresa"].ToString());
                falta.periodo = int.Parse(dtFaltas.Rows[i]["periodo"].ToString());
                falta.faltas = int.Parse(dtFaltas.Rows[i]["faltas"].ToString());
                falta.fechainicio = DateTime.Parse(dtFaltas.Rows[i]["fechainicio"].ToString());
                falta.fechafin = DateTime.Parse(dtFaltas.Rows[i]["fechafin"].ToString());
                falta.fecha = DateTime.Parse(dtFaltas.Rows[i]["fecha"].ToString());
                lstFaltas.Add(falta);
            }
            return lstFaltas;
        }

        public List<Faltas> obtenerFalta(int idTrabajador, int idEmpresa, DateTime fecha)
        {
            List<Faltas> lstFaltas = new List<Faltas>();
            DataTable dtFaltas = new DataTable();
            Command.CommandText = @"select * from faltas where idempresa = @idempresa and fecha = @fecha and idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("fecha", fecha);
            Command.Parameters.AddWithValue("idtrabajador", idTrabajador);
            dtFaltas = SelectData(Command);
            for (int i = 0; i < dtFaltas.Rows.Count; i++)
            {
                Faltas falta = new Faltas();
                falta.id = int.Parse(dtFaltas.Rows[i]["id"].ToString());
                falta.idtrabajador = int.Parse(dtFaltas.Rows[i]["idtrabajador"].ToString());
                falta.idempresa = int.Parse(dtFaltas.Rows[i]["idempresa"].ToString());
                falta.periodo = int.Parse(dtFaltas.Rows[i]["periodo"].ToString());
                falta.faltas = int.Parse(dtFaltas.Rows[i]["faltas"].ToString());
                falta.fechainicio = DateTime.Parse(dtFaltas.Rows[i]["fechainicio"].ToString());
                falta.fechafin = DateTime.Parse(dtFaltas.Rows[i]["fechafin"].ToString());
                falta.fecha = DateTime.Parse(dtFaltas.Rows[i]["fecha"].ToString());
                lstFaltas.Add(falta);
            }
            return lstFaltas;
        }

        public List<Faltas> obtenerFaltas(int idempresa)
        {
            List<Faltas> lstFaltas = new List<Faltas>();
            DataTable dtFaltas = new DataTable();
            Command.CommandText = "select * from faltas where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            dtFaltas = SelectData(Command);
            for (int i = 0; i < dtFaltas.Rows.Count; i++)
            {
                Faltas falta = new Faltas();
                falta.id = int.Parse(dtFaltas.Rows[i]["id"].ToString());
                falta.idtrabajador = int.Parse(dtFaltas.Rows[i]["idtrabajador"].ToString());
                falta.idempresa = int.Parse(dtFaltas.Rows[i]["idempresa"].ToString());
                falta.periodo = int.Parse(dtFaltas.Rows[i]["periodo"].ToString());
                falta.faltas = int.Parse(dtFaltas.Rows[i]["faltas"].ToString());
                falta.fechainicio = DateTime.Parse(dtFaltas.Rows[i]["fechainicio"].ToString());
                falta.fechafin = DateTime.Parse(dtFaltas.Rows[i]["fechafin"].ToString());
                falta.fecha = DateTime.Parse(dtFaltas.Rows[i]["fecha"].ToString());
                lstFaltas.Add(falta);
            }
            return lstFaltas;
        }

        public List<Faltas> obtenerFalta(Faltas f)
        {
            List<Faltas> lstFaltas = new List<Faltas>();
            DataTable dtFaltas = new DataTable();
            Command.CommandText = "select * from faltas where idempresa = @idempresa and id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", f.idempresa);
            Command.Parameters.AddWithValue("id", f.id);
            dtFaltas = SelectData(Command);
            for (int i = 0; i < dtFaltas.Rows.Count; i++)
            {
                Faltas falta = new Faltas();
                falta.id = int.Parse(dtFaltas.Rows[i]["id"].ToString());
                falta.idtrabajador = int.Parse(dtFaltas.Rows[i]["idtrabajador"].ToString());
                falta.idempresa = int.Parse(dtFaltas.Rows[i]["idempresa"].ToString());
                falta.periodo = int.Parse(dtFaltas.Rows[i]["periodo"].ToString());
                falta.faltas = int.Parse(dtFaltas.Rows[i]["faltas"].ToString());
                falta.fechainicio = DateTime.Parse(dtFaltas.Rows[i]["fechainicio"].ToString());
                falta.fechafin = DateTime.Parse(dtFaltas.Rows[i]["fechafin"].ToString());
                lstFaltas.Add(falta);
            }
            return lstFaltas;
        }

        public object existeFalta(Faltas f)
        {
            Command.CommandText = "select coalesce(sum(faltas),0) as faltas from faltas where idtrabajador = @idtrabajador and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", f.idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", f.fechainicio);
            Command.Parameters.AddWithValue("fechafin", f.fechafin);
            object dato = Select(Command);
            return dato;
        }

        public object existeFalta(int idTrabajador, DateTime fecha)
        {
            Command.CommandText = "select coalesce(sum(faltas),0) as faltas from faltas where idtrabajador = @idtrabajador and fecha = @fecha";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idTrabajador);
            Command.Parameters.AddWithValue("fecha", fecha);
            object dato = Select(Command);
            return dato;
        }

        public int insertaFalta(Faltas f)
        {
            Command.CommandText = "insert into faltas (idtrabajador, idempresa, periodo, faltas, fechainicio, fechafin, fecha) " +
                "values (@idtrabajador, @idempresa, @idperiodo, @faltas, @fechainicio, @fechafin, @fecha)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador",f.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", f.idempresa);
            Command.Parameters.AddWithValue("idperiodo", f.periodo);
            Command.Parameters.AddWithValue("faltas", f.faltas);
            Command.Parameters.AddWithValue("fechainicio", f.fechainicio);
            Command.Parameters.AddWithValue("fechafin", f.fechafin);
            Command.Parameters.AddWithValue("fecha", f.fecha);
            return Command.ExecuteNonQuery();
        }

        public int actualizaFalta(Faltas f)
        {
            Command.CommandText = "update faltas set periodo = @periodo, faltas = @faltas, fechainicio = @fechainicio, fechafin = @fechafin where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", f.id);
            Command.Parameters.AddWithValue("periodo", f.periodo);
            Command.Parameters.AddWithValue("faltas", f.faltas);
            Command.Parameters.AddWithValue("fechainicio", f.fechainicio);
            Command.Parameters.AddWithValue("fechafin", f.fechafin);
            return Command.ExecuteNonQuery();
        }

        public int eliminaFalta(Faltas f)
        {
            Command.CommandText = "delete from faltas where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", f.id);
            return Command.ExecuteNonQuery();
        }

        public int eliminaFalta(int idTrabajador, DateTime fecha)
        {
            Command.CommandText = "delete from faltas where idtrabajador = @idtrabajador and fecha = @fecha";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idTrabajador);
            Command.Parameters.AddWithValue("fecha", fecha);
            return Command.ExecuteNonQuery();
        }

        public int eliminaFaltaExistente(Faltas f)
        {
            Command.CommandText = "delete from faltas where idtrabajador = @idtrabajador and fechainicio = @fechainicio and fechafin = @fechafin and fecha = @fecha";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", f.idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", f.fechainicio);
            Command.Parameters.AddWithValue("fechafin", f.fechafin);
            Command.Parameters.AddWithValue("fecha", f.fecha);
            return Command.ExecuteNonQuery();
        }

        public void bulkFaltas(DataTable dt, string tabla)
        {
            bulkCommand.DestinationTableName = tabla;
            bulkCommand.WriteToServer(dt);
            dt.Clear();
        }

        public int stpFaltas()
        {
            Command.CommandText = "exec stp_InsertaFaltas";
            Command.Parameters.Clear();
            return Command.ExecuteNonQuery();
        }
    }
}

