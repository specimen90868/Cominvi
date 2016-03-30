using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimientos.Core
{
    public class MovimientosHelper : Data.Obj.DataObj
    {
        public List<Movimientos> obtenerMovimientos(Movimientos m)
        {
            Command.CommandText = "select * from movimientos where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", m.idempresa);
            DataTable dtMovimientos = new DataTable();
            List<Movimientos> lstMovimientos = new List<Movimientos>();
            dtMovimientos = SelectData(Command);
            for (int i = 0; i < dtMovimientos.Rows.Count; i++)
            {
                Movimientos mov = new Movimientos();
                mov.id = int.Parse(dtMovimientos.Rows[i]["id"].ToString());
                mov.idtrabajador = int.Parse(dtMovimientos.Rows[i]["idtrabajador"].ToString());
                mov.idempresa = int.Parse(dtMovimientos.Rows[i]["idempresa"].ToString());
                mov.idconcepto = int.Parse(dtMovimientos.Rows[i]["idconcepto"].ToString());
                mov.cantidad = decimal.Parse(dtMovimientos.Rows[i]["cantidad"].ToString());
                mov.fechainicio = DateTime.Parse(dtMovimientos.Rows[i]["fechainicio"].ToString());
                mov.fechafin = DateTime.Parse(dtMovimientos.Rows[i]["fechafin"].ToString());
                lstMovimientos.Add(mov);
            }
            return lstMovimientos;
        }

        public List<Movimientos> obtenerMovimiento(Movimientos m)
        {
            Command.CommandText = "select * from movimientos where idtrabajador = @idtrabajador and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", m.idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", m.fechainicio);
            Command.Parameters.AddWithValue("fechafin", m.fechafin);
            DataTable dtMovimientos = new DataTable();
            List<Movimientos> lstMovimientos = new List<Movimientos>();
            dtMovimientos = SelectData(Command);
            for (int i = 0; i < dtMovimientos.Rows.Count; i++)
            {
                Movimientos mov = new Movimientos();
                mov.id = int.Parse(dtMovimientos.Rows[i]["id"].ToString());
                mov.idtrabajador = int.Parse(dtMovimientos.Rows[i]["idtrabajador"].ToString());
                mov.idempresa = int.Parse(dtMovimientos.Rows[i]["idempresa"].ToString());
                mov.idconcepto = int.Parse(dtMovimientos.Rows[i]["idconcepto"].ToString());
                mov.cantidad = decimal.Parse(dtMovimientos.Rows[i]["cantidad"].ToString());
                mov.fechainicio = DateTime.Parse(dtMovimientos.Rows[i]["fechainicio"].ToString());
                mov.fechafin = DateTime.Parse(dtMovimientos.Rows[i]["fechafin"].ToString());
                lstMovimientos.Add(mov);
            }
            return lstMovimientos;
        }

        public List<Movimientos> obtenerMovimientosTrabajador(Movimientos m)
        {
            Command.CommandText = "select * from movimientos where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", m.idtrabajador);
            DataTable dtMovimientos = new DataTable();
            List<Movimientos> lstMovimientos = new List<Movimientos>();
            dtMovimientos = SelectData(Command);
            for (int i = 0; i < dtMovimientos.Rows.Count; i++)
            {
                Movimientos mov = new Movimientos();
                mov.id = int.Parse(dtMovimientos.Rows[i]["id"].ToString());
                mov.idtrabajador = int.Parse(dtMovimientos.Rows[i]["idtrabajador"].ToString());
                mov.idempresa = int.Parse(dtMovimientos.Rows[i]["idempresa"].ToString());
                mov.idconcepto = int.Parse(dtMovimientos.Rows[i]["idconcepto"].ToString());
                mov.cantidad = decimal.Parse(dtMovimientos.Rows[i]["cantidad"].ToString());
                mov.fechainicio = DateTime.Parse(dtMovimientos.Rows[i]["fechainicio"].ToString());
                mov.fechafin = DateTime.Parse(dtMovimientos.Rows[i]["fechafin"].ToString());
                lstMovimientos.Add(mov);
            }
            return lstMovimientos;
        }

        public int insertaMovimiento(Movimientos m)
        {
            Command.CommandText = "insert into movimientos (idtrabajador, idempresa, idconcepto, cantidad, fechainicio, fechafin) values " +
                "(@idtrabajador, @idempresa, @idconcepto, @cantidad, @fechainicio, @fechafin)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", m.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", m.idempresa);
            Command.Parameters.AddWithValue("idconcepto", m.idconcepto);
            Command.Parameters.AddWithValue("cantidad", m.cantidad);
            Command.Parameters.AddWithValue("fechainicio", m.fechainicio);
            Command.Parameters.AddWithValue("fechafin", m.fechafin);
            return Command.ExecuteNonQuery();
        }

        public int actualizaMovimiento(Movimientos m)
        {
            Command.CommandText = @"update movimientos set cantidad = @cantidad 
                where idtrabajador = @idtrabajador and idempresa = @idempresa and fechainicio = @fechainicio 
                and fechafin = @fechafin and idconcepto = @idconcepto";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", m.idtrabajador);
            Command.Parameters.AddWithValue("idconcepto", m.idconcepto);
            Command.Parameters.AddWithValue("cantidad", m.cantidad);
            Command.Parameters.AddWithValue("fechainicio", m.fechainicio);
            Command.Parameters.AddWithValue("fechafin", m.fechafin);
            Command.Parameters.AddWithValue("idempresa", m.idempresa);
            return Command.ExecuteNonQuery();
        }

        public int eliminaMovimiento(Movimientos m)
        {
            Command.CommandText = "delete from movimientos where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", m.id);
            return Command.ExecuteNonQuery();
        }

        public object existeMovimiento(Movimientos m)
        {
            Command.CommandText = "select count(*) from movimientos where idtrabajador = @idtrabajador and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", m.idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", m.fechainicio);
            Command.Parameters.AddWithValue("fechafin", m.fechafin);
            object dato = Select(Command);
            return dato;
        }

        public object existeMovimientoConcepto(Movimientos m)
        {
            Command.CommandText = "select count(*) from movimientos where idtrabajador = @idtrabajador and fechainicio = @fechainicio and fechafin = @fechafin and idconcepto = @idconcepto";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", m.idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", m.fechainicio);
            Command.Parameters.AddWithValue("fechafin", m.fechafin);
            Command.Parameters.AddWithValue("idconcepto", m.idconcepto);
            object dato = Select(Command);
            return dato;
        }

        public void bulkMovimientos(DataTable dt, string tabla)
        {
            bulkCommand.DestinationTableName = tabla;
            bulkCommand.WriteToServer(dt);
            dt.Clear();
        }

        public int stpMovimientos()
        {
            Command.CommandText = "exec stp_InsertaMovimientos";
            Command.Parameters.Clear();
            return Command.ExecuteNonQuery();
        }
    }
}
