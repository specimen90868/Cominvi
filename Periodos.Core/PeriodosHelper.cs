using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Periodos.Core
{
    public class PeriodosHelper : Data.Obj.DataObj
    {
        public List<Periodos> obtenerPeriodos(Periodos periodo)
        {
            DataTable dtPeriodos = new DataTable();
            List<Periodos> lstPeriodos = new List<Periodos>();
            Command.CommandText = "select idperiodo, pago, dias from periodos where estatus = 1 and idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", periodo.idempresa);
            dtPeriodos = SelectData(Command);
            for (int i = 0; i < dtPeriodos.Rows.Count; i++)
            {
                Periodos p = new Periodos();
                p.idperiodo = int.Parse(dtPeriodos.Rows[i]["idperiodo"].ToString());
                p.pago = dtPeriodos.Rows[i]["pago"].ToString();
                p.dias = int.Parse(dtPeriodos.Rows[i]["dias"].ToString());
                lstPeriodos.Add(p);
            }
            return lstPeriodos;
        }

        public List<Periodos> obtenerPeriodo(Periodos p)
        {
            DataTable dtPeriodos = new DataTable();
            List<Periodos> lstPeriodos = new List<Periodos>();
            Command.CommandText = "select idperiodo, pago, dias from periodos where idperiodo = @idperiodo and estatus = 1";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperiodo", p.idperiodo);
            dtPeriodos = SelectData(Command);
            for (int i = 0; i < dtPeriodos.Rows.Count; i++)
            {
                Periodos periodo = new Periodos();
                periodo.idperiodo = int.Parse(dtPeriodos.Rows[i]["idperiodo"].ToString());
                periodo.pago = dtPeriodos.Rows[i]["pago"].ToString();
                periodo.dias = int.Parse(dtPeriodos.Rows[i]["dias"].ToString());
                lstPeriodos.Add(periodo);
            }
            return lstPeriodos;
        }

        public object obtenerPeriodo(int idempresa, int dias)
        {
            Command.CommandText = "select count(*) from periodos where idempresa = @idempresa and dias = @dias";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("dias", dias);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerIdPeriodo(string pago, int idEmpresa)
        {
            Command.CommandText = "select idperiodo from periodos where idempresa = @idempresa and pago = @pago";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("pago", pago);
            object dato = Select(Command);
            return dato;
        }

        public object DiasDePago(Periodos p)
        {
            Command.CommandText = "select dias from periodos where idperiodo = @idperiodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperiodo", p.idperiodo);
            object dato = Select(Command);
            return dato;
        }

        public int insertaPeriodo(Periodos p)
        {
            Command.CommandText = "insert into periodos (pago, dias, estatus, idempresa) values (@pago, @dias, @estatus, @idempresa)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("pago", p.pago);
            Command.Parameters.AddWithValue("dias", p.dias);
            Command.Parameters.AddWithValue("estatus", p.estatus);
            Command.Parameters.AddWithValue("idempresa", p.idempresa);
            return Command.ExecuteNonQuery();
        }

        public int actualizaPeriodo(Periodos p)
        {
            Command.CommandText = "update periodos set pago = @pago, dias = @dias where idperiodo = @idperiodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperiodo", p.idperiodo);
            Command.Parameters.AddWithValue("pago", p.pago);
            Command.Parameters.AddWithValue("dias", p.dias);
            return Command.ExecuteNonQuery();
        }

        public int bajaPeriodo(Periodos p)
        {
            Command.CommandText = "update periodos set estatus = 0 where idperiodo = @idperiodo";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperiodo", p.idperiodo);
            return Command.ExecuteNonQuery();
        }
    }
}
