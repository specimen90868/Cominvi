using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Periodos.Core
{
    public class PeriodosHelper : Data.Obj.DataObj
    {
        public List<Periodos> obtenerPeriodos()
        {
            DataTable dtPeriodos = new DataTable();
            List<Periodos> lstPeriodos = new List<Periodos>();
            Command.CommandText = "select idperiodo,idcliente,pago,dias,inicio,termino from periodos";
            Command.Parameters.Clear();
            dtPeriodos = SelectData(Command);
            for (int i = 0; i < dtPeriodos.Rows.Count; i++)
            {
                Periodos p = new Periodos();
                p.idperiodo = int.Parse(dtPeriodos.Rows[i]["idperiodo"].ToString());
                p.idcliente = int.Parse(dtPeriodos.Rows[i]["idcliente"].ToString());
                p.pago = dtPeriodos.Rows[i]["pago"].ToString();
                p.dias = int.Parse(dtPeriodos.Rows[i]["dias"].ToString());
                p.inicio = dtPeriodos.Rows[i]["inicio"].ToString();
                p.termino = dtPeriodos.Rows[i]["termino"].ToString();
                lstPeriodos.Add(p);
            }
            return lstPeriodos;
        }

        public List<Periodos> obtenerPeriodo(Periodos p)
        {
            DataTable dtPeriodos = new DataTable();
            List<Periodos> lstPeriodos = new List<Periodos>();
            Command.CommandText = "select idperiodo, idcliente, pago, dias, inicio, termino from periodos where idperiodo = @idperiodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperiodo", p.idperiodo);
            dtPeriodos = SelectData(Command);
            for (int i = 0; i < dtPeriodos.Rows.Count; i++)
            {
                Periodos periodo = new Periodos();
                periodo.idperiodo = int.Parse(dtPeriodos.Rows[i]["idperiodo"].ToString());
                periodo.idcliente = int.Parse(dtPeriodos.Rows[i]["idcliente"].ToString());
                periodo.pago = dtPeriodos.Rows[i]["pago"].ToString();
                periodo.dias = int.Parse(dtPeriodos.Rows[i]["dias"].ToString());
                periodo.inicio = dtPeriodos.Rows[i]["inicio"].ToString();
                periodo.termino = dtPeriodos.Rows[i]["termino"].ToString();
                lstPeriodos.Add(periodo);
            }
            return lstPeriodos;
        }

        public object DiasDePago(Periodos p)
        {
            Command.CommandText = "select dias from periodos where idperiodo = @idperiodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperiodo", p.idperiodo);
            object dato = Select(Command);
            return dato;
        }

        public List<Periodos> obtenerPeriodoCliente(Periodos p)
        {
            DataTable dtPeriodos = new DataTable();
            List<Periodos> lstPeriodos = new List<Periodos>();
            Command.CommandText = "select idperiodo, pago from periodos where idcliente = @idcliente";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idcliente",p.idcliente);
            dtPeriodos = SelectData(Command);
            for (int i = 0; i < dtPeriodos.Rows.Count; i++)
            {
                Periodos periodo = new Periodos();
                periodo.idperiodo = int.Parse(dtPeriodos.Rows[i]["idperiodo"].ToString());
                periodo.pago = dtPeriodos.Rows[i]["pago"].ToString();
                lstPeriodos.Add(periodo);
            }
            return lstPeriodos;
        }

        public int insertaPeriodo(Periodos p)
        {
            Command.CommandText = "insert into periodos (idcliente, pago, dias, inicio, termino, estatus) values (@idcliente, @pago, @dias, @inicio, @termino, @estatus)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idcliente",p.idcliente);
            Command.Parameters.AddWithValue("pago", p.pago);
            Command.Parameters.AddWithValue("dias", p.dias);
            Command.Parameters.AddWithValue("inicio", p.inicio);
            Command.Parameters.AddWithValue("termino", p.termino);
            Command.Parameters.AddWithValue("estatus", p.estatus);
            return Command.ExecuteNonQuery();
        }

        public int actualizaPeriodo(Periodos p)
        {
            Command.CommandText = "update periodos set idcliente = @idcliente, pago = @pago, dias = @dias, inicio = @inicio, termino = @termino where idperiodo = @idperiodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperiodo", p.idperiodo);
            Command.Parameters.AddWithValue("idcliente", p.idcliente);
            Command.Parameters.AddWithValue("pago", p.pago);
            Command.Parameters.AddWithValue("dias", p.dias);
            Command.Parameters.AddWithValue("inicio", p.inicio);
            Command.Parameters.AddWithValue("termino", p.termino);
            return Command.ExecuteNonQuery();
        }

        public int bajaPeriodo(Periodos p)
        {
            Command.CommandText = "update periodos set estatus = @estatus where idperiodo = @idperiodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperiodo", p.idperiodo);
            Command.Parameters.AddWithValue("estatus", p.estatus);
            return Command.ExecuteNonQuery();
        }
    }
}
