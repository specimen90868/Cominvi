using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TablaIsr.Core
{
    public class IsrHelper : Data.Obj.DataObj
    {
        public List<TablaIsr> obtenerTablaIsr()
        {
            List<TablaIsr> lstIsr = new List<TablaIsr>();
            DataTable dtIsr = new DataTable();
            Command.CommandText = "select * from tablaIsr";
            dtIsr = SelectData(Command);
            for (int i = 0; i < dtIsr.Rows.Count; i++)
            {
                TablaIsr isr = new TablaIsr();
                isr.id = int.Parse(dtIsr.Rows[i]["id"].ToString());
                isr.inferior = decimal.Parse(dtIsr.Rows[i]["inferior"].ToString());
                isr.cuota = decimal.Parse(dtIsr.Rows[i]["cuota"].ToString());
                isr.porcentaje = decimal.Parse(dtIsr.Rows[i]["porcentaje"].ToString());
                isr.periodo = int.Parse(dtIsr.Rows[i]["periodo"].ToString());
                isr.anio = int.Parse(dtIsr.Rows[i]["anio"].ToString());
                lstIsr.Add(isr);
            }
            return lstIsr;
        }

        public List<TablaIsr> obtenerIsr(TablaIsr ti)
        {
            List<TablaIsr> lstIsr = new List<TablaIsr>();
            DataTable dtIsr = new DataTable();
            Command.CommandText = "select * from tablaIsr where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", ti.id);
            dtIsr = SelectData(Command);
            for (int i = 0; i < dtIsr.Rows.Count; i++)
            {
                TablaIsr isr = new TablaIsr();
                isr.id = int.Parse(dtIsr.Rows[i]["id"].ToString());
                isr.inferior = decimal.Parse(dtIsr.Rows[i]["inferior"].ToString());
                isr.cuota = decimal.Parse(dtIsr.Rows[i]["cuota"].ToString());
                isr.porcentaje = decimal.Parse(dtIsr.Rows[i]["porcentaje"].ToString());
                isr.periodo = int.Parse(dtIsr.Rows[i]["periodo"].ToString());
                isr.anio = int.Parse(dtIsr.Rows[i]["anio"].ToString());
                lstIsr.Add(isr);
            }
            return lstIsr;
        }

        public List<TablaIsr> isrCorrespondiente(TablaIsr ti)
        {
            List<TablaIsr> lstIsr = new List<TablaIsr>();
            DataTable dtIsr = new DataTable();
            Command.CommandText = "select top 1 * from tablaIsr where inferior <= @cantidad order by inferior desc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("cantidad", ti.inferior);
            dtIsr = SelectData(Command);
            for (int i = 0; i < dtIsr.Rows.Count; i++)
            {
                TablaIsr isr = new TablaIsr();
                isr.id = int.Parse(dtIsr.Rows[i]["id"].ToString());
                isr.inferior = decimal.Parse(dtIsr.Rows[i]["inferior"].ToString());
                isr.cuota = decimal.Parse(dtIsr.Rows[i]["cuota"].ToString());
                isr.porcentaje = decimal.Parse(dtIsr.Rows[i]["porcentaje"].ToString());
                isr.periodo = int.Parse(dtIsr.Rows[i]["periodo"].ToString());
                isr.anio = int.Parse(dtIsr.Rows[i]["anio"].ToString());
                lstIsr.Add(isr);
            }
            return lstIsr;
        }

        public int insertaIsr(TablaIsr ti)
        {
            Command.CommandText = "insert into tablaIsr (inferior, cuota, porcentaje, periodo, anio) " +
                "values (@inferior, @cuota, @porcentaje, @periodo, @anio)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("inferior",ti.inferior);
            Command.Parameters.AddWithValue("cuota", ti.cuota);
            Command.Parameters.AddWithValue("porcentaje", ti.porcentaje);
            Command.Parameters.AddWithValue("periodo", ti.periodo);
            Command.Parameters.AddWithValue("anio", ti.anio);
            return Command.ExecuteNonQuery();
        }

        public int actualizaIsr(TablaIsr ti)
        {
            Command.CommandText = "update tablaIsr set inferior = @inferior, cuota = @cuota, porcentaje = @porcentaje, periodo = @periodo, anio = @anio where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", ti.id);
            Command.Parameters.AddWithValue("inferior", ti.inferior);
            Command.Parameters.AddWithValue("cuota", ti.cuota);
            Command.Parameters.AddWithValue("porcentaje", ti.porcentaje);
            Command.Parameters.AddWithValue("periodo", ti.periodo);
            Command.Parameters.AddWithValue("anio", ti.anio);
            return Command.ExecuteNonQuery();
        }

        public int eliminaIsr(TablaIsr ti)
        {
            Command.CommandText = "delete from tablaIsr where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", ti.id);
            return Command.ExecuteNonQuery();
        }
    }
}
