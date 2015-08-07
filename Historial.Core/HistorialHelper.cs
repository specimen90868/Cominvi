using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Historial.Core
{
    public class HistorialHelper : Data.Obj.DataObj
    {
        public List<Historial> obtenerHistoriales(Historial h)
        {
            Command.CommandText = "select * from MovimientoTrabajador where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", h.idtrabajador);
            DataTable dtHistorial = new DataTable();
            List<Historial> lstHistorial = new List<Historial>();
            dtHistorial = SelectData(Command);
            for (int i = 0; i < dtHistorial.Rows.Count; i++)
            {
                Historial historial = new Historial();
                historial.id = int.Parse(dtHistorial.Rows[i]["id"].ToString());
                historial.idtrabajador = int.Parse(dtHistorial.Rows[i]["idtrabajador"].ToString());
                historial.tipomovimiento = int.Parse(dtHistorial.Rows[i]["tipomovimiento"].ToString());
                historial.valor = double.Parse(dtHistorial.Rows[i]["valor"].ToString());
                historial.fecha_imss = DateTime.Parse(dtHistorial.Rows[i]["fecha_imss"].ToString());
                historial.fecha_sistema = DateTime.Parse(dtHistorial.Rows[i]["fecha_sistema"].ToString());
                historial.motivobaja = int.Parse(dtHistorial.Rows[i]["motivobaja"].ToString());
                lstHistorial.Add(historial);
            }
            return lstHistorial;
        }

        public int insertarHistorial(Historial h)
        {
            Command.CommandText = "insert into MovimientoTrabajador (idtrabajador, tipomovimiento, valor, fecha_imss, fecha_sistema, idempresa, motivobaja) values " +
                "(@idtrabajador, @tipomovimiento, @valor, @fecha_imss, @fecha_sistema, @idempresa, @motivobaja)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", h.idtrabajador);
            Command.Parameters.AddWithValue("tipomovimiento", h.tipomovimiento);
            Command.Parameters.AddWithValue("valor", h.valor);
            Command.Parameters.AddWithValue("fecha_imss", h.fecha_imss);
            Command.Parameters.AddWithValue("fecha_sistema", h.fecha_sistema);
            Command.Parameters.AddWithValue("idempresa", h.idempresa);
            Command.Parameters.AddWithValue("motivobaja", h.motivobaja);
            return Command.ExecuteNonQuery();
        }
    }
}
