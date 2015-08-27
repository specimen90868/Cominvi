using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Estados.Core
{
    public class EstadosHelper : Data.Obj.DataObj
    {
        public List<Estados> obtenerEstados()
        {
            List<Estados> lstEstados = new List<Estados>();
            DataTable dtEstados = new DataTable();
            Command.CommandText = "select idestado, nombre from estados";
            dtEstados = SelectData(Command);
            for (int i = 0; i < dtEstados.Rows.Count; i++)
            {
                Estados edo = new Estados();
                edo.idestado = int.Parse(dtEstados.Rows[i]["idestado"].ToString());
                edo.nombre = dtEstados.Rows[i]["nombre"].ToString();
                lstEstados.Add(edo);
            }
            return lstEstados;
        }

        public object obtenerIdEstado(Estados e)
        {
            Command.CommandText = "seletc idestado from estados where estado like '%@estado%'";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("estado", e.nombre);
            return Command.ExecuteNonQuery();
        }
    }
}
