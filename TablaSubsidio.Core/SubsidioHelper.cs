using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TablaSubsidio.Core
{
    public class SubsidioHelper : Data.Obj.DataObj
    {
        public List<TablaSubsidio> obtenerTablaSubsidio()
        {
            List<TablaSubsidio> lstSubsidio = new List<TablaSubsidio>();
            DataTable dtSubsidio = new DataTable();
            Command.CommandText = "select * from tablaSubsidio";
            dtSubsidio = SelectData(Command);
            for (int i = 0; i < dtSubsidio.Rows.Count; i++)
            {
                TablaSubsidio subsidio = new TablaSubsidio();
                subsidio.id = int.Parse(dtSubsidio.Rows[i]["id"].ToString());
                subsidio.desde = decimal.Parse(dtSubsidio.Rows[i]["desde"].ToString());
                subsidio.cantidad = decimal.Parse(dtSubsidio.Rows[i]["cantidad"].ToString());
                subsidio.periodo = int.Parse(dtSubsidio.Rows[i]["periodo"].ToString());
                subsidio.anio = int.Parse(dtSubsidio.Rows[i]["anio"].ToString());
                lstSubsidio.Add(subsidio);
            }
            return lstSubsidio;
        }

        public List<TablaSubsidio> obtieneSubsidio(TablaSubsidio s)
        {
            List<TablaSubsidio> lstSubsidio = new List<TablaSubsidio>();
            DataTable dtSubsidio = new DataTable();
            Command.CommandText = "select * from tablaSubsidio where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id",s.id);
            dtSubsidio = SelectData(Command);
            for (int i = 0; i < dtSubsidio.Rows.Count; i++)
            {
                TablaSubsidio subsidio = new TablaSubsidio();
                subsidio.id = int.Parse(dtSubsidio.Rows[i]["id"].ToString());
                subsidio.desde = decimal.Parse(dtSubsidio.Rows[i]["desde"].ToString());
                subsidio.cantidad = decimal.Parse(dtSubsidio.Rows[i]["cantidad"].ToString());
                subsidio.periodo = int.Parse(dtSubsidio.Rows[i]["periodo"].ToString());
                subsidio.anio = int.Parse(dtSubsidio.Rows[i]["anio"].ToString());
                lstSubsidio.Add(subsidio);
            }
            return lstSubsidio;
        }

        public object obtenerCantidadSubsidio(TablaSubsidio ts)
        {
            Command.CommandText = "select top 1 cantidad from tablasubsidio where desde <= @desde order by desde desc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("desde", ts.desde);
            object dato = Select(Command);
            return dato;
        }

        public int insertaSubsidio(TablaSubsidio ts)
        {
            Command.CommandText = "insert into tablaSubsidio (desde, cantidad, periodo, anio) " +
                "values (@desde, @cantidad, @periodo, @anio)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("desde", ts.desde);
            Command.Parameters.AddWithValue("cantidad", ts.cantidad);
            Command.Parameters.AddWithValue("periodo", ts.periodo);
            Command.Parameters.AddWithValue("anio", ts.anio);
            return Command.ExecuteNonQuery();
        }

        public int actualizaSubsidio(TablaSubsidio ts)
        {
            Command.CommandText = "update tablaSubsidio set desde = @desde, cantidad = @cantidad, periodo = @periodo, anio = @anio where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", ts.id);
            Command.Parameters.AddWithValue("desde", ts.desde);
            Command.Parameters.AddWithValue("cantidad", ts.cantidad);
            Command.Parameters.AddWithValue("periodo", ts.periodo);
            Command.Parameters.AddWithValue("anio", ts.anio);
            return Command.ExecuteNonQuery();
        }

        public int eliminaSubsidio(TablaSubsidio ts)
        {
            Command.CommandText = "delete from tablaSubsidio where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", ts.id);
            return Command.ExecuteNonQuery();
        }
    }
}
