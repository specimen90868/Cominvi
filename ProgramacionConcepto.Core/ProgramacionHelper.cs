using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramacionConcepto.Core
{
    public class ProgramacionHelper : Data.Obj.DataObj
    {
        public List<ProgramacionConcepto> obtenerProgramaciones(ProgramacionConcepto pc)
        {
            List<ProgramacionConcepto> lstProgramacion = new List<ProgramacionConcepto>();
            DataTable dtProgramacion = new DataTable();
            Command.CommandText = "select * from ProgramacionConcepto where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pc.idempresa);
            dtProgramacion = SelectData(Command);
            for (int i = 0; i < dtProgramacion.Rows.Count; i++)
            {
                ProgramacionConcepto programacion = new ProgramacionConcepto();
                programacion.idprogramacion = int.Parse(dtProgramacion.Rows[i]["id"].ToString());
                programacion.idtrabajador = int.Parse(dtProgramacion.Rows[i]["idtrabajador"].ToString());
                programacion.idempresa = int.Parse(dtProgramacion.Rows[i]["idempresa"].ToString());
                programacion.idconcepto = int.Parse(dtProgramacion.Rows[i]["idconcepto"].ToString());
                programacion.cantidad = decimal.Parse(dtProgramacion.Rows[i]["cantidad"].ToString());
                programacion.fechafin = DateTime.Parse(dtProgramacion.Rows[i]["fechafin"].ToString());
                lstProgramacion.Add(programacion);
            }
            return lstProgramacion;
        }

        public List<ProgramacionConcepto> obtenerProgramacion(ProgramacionConcepto pc)
        {
            List<ProgramacionConcepto> lstProgramacion = new List<ProgramacionConcepto>();
            DataTable dtProgramacion = new DataTable();
            Command.CommandText = "select * from ProgramacionConcepto where idtrabajador = @idtrabajador order by idconcepto asc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", pc.idtrabajador);
            dtProgramacion = SelectData(Command);
            for (int i = 0; i < dtProgramacion.Rows.Count; i++)
            {
                ProgramacionConcepto programacion = new ProgramacionConcepto();
                programacion.idprogramacion = int.Parse(dtProgramacion.Rows[i]["id"].ToString());
                programacion.idtrabajador = int.Parse(dtProgramacion.Rows[i]["idtrabajador"].ToString());
                programacion.idempresa = int.Parse(dtProgramacion.Rows[i]["idempresa"].ToString());
                programacion.idconcepto = int.Parse(dtProgramacion.Rows[i]["idconcepto"].ToString());
                programacion.cantidad = decimal.Parse(dtProgramacion.Rows[i]["cantidad"].ToString());
                programacion.fechafin = DateTime.Parse(dtProgramacion.Rows[i]["fechafin"].ToString());
                lstProgramacion.Add(programacion);
            }
            return lstProgramacion;
        }

        public List<ProgramacionConcepto> obtenerProgramacion(int id)
        {
            List<ProgramacionConcepto> lstProgramacion = new List<ProgramacionConcepto>();
            DataTable dtProgramacion = new DataTable();
            Command.CommandText = "select * from ProgramacionConcepto where id = @id order by idconcepto asc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", id);
            dtProgramacion = SelectData(Command);
            for (int i = 0; i < dtProgramacion.Rows.Count; i++)
            {
                ProgramacionConcepto programacion = new ProgramacionConcepto();
                programacion.idprogramacion = int.Parse(dtProgramacion.Rows[i]["id"].ToString());
                programacion.idtrabajador = int.Parse(dtProgramacion.Rows[i]["idtrabajador"].ToString());
                programacion.idempresa = int.Parse(dtProgramacion.Rows[i]["idempresa"].ToString());
                programacion.idconcepto = int.Parse(dtProgramacion.Rows[i]["idconcepto"].ToString());
                programacion.cantidad = decimal.Parse(dtProgramacion.Rows[i]["cantidad"].ToString());
                programacion.fechafin = DateTime.Parse(dtProgramacion.Rows[i]["fechafin"].ToString());
                lstProgramacion.Add(programacion);
            }
            return lstProgramacion;
        }

        public object existeProgramacion(ProgramacionConcepto pc)
        {
            Command.CommandText = "select count(idtrabajador) as existe from programacionconcepto where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", pc.idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public int insertaProgramacion(ProgramacionConcepto pc)
        {
            Command.CommandText = "insert into ProgramacionConcepto (idtrabajador, idempresa, idconcepto, cantidad, fechafin) " +
                "values (@idtrabajador, @idempresa, @idconcepto, @cantidad, @fechafin)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", pc.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", pc.idempresa);
            Command.Parameters.AddWithValue("idconcepto", pc.idconcepto);
            Command.Parameters.AddWithValue("cantidad", pc.cantidad);
            Command.Parameters.AddWithValue("fechafin", pc.fechafin);
            return Command.ExecuteNonQuery();
        }

        public int actualizaProgramacion(ProgramacionConcepto pc)
        {
            Command.CommandText = "update ProgramacionConcepto set idconcepto = @idconcepto, cantidad = @cantidad, fechafin = @fechafin " +
                "where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", pc.idprogramacion);
            Command.Parameters.AddWithValue("idconcepto", pc.idconcepto);
            Command.Parameters.AddWithValue("cantidad", pc.cantidad);
            Command.Parameters.AddWithValue("fechafin", pc.fechafin);
            return Command.ExecuteNonQuery();
        }

        public int eliminaProgramacion(ProgramacionConcepto pc)
        {
            Command.CommandText = "delete from ProgramacionConcepto where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", pc.idprogramacion);
            return Command.ExecuteNonQuery();
        }
    }
}
