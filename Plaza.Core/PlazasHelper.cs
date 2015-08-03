using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaza.Core
{
    public class PlazasHelper : Data.Obj.DataObj
    {
        public List<Plazas> obtenerPlazas()
        {
            DataTable dtPlazas = new DataTable();
            List<Plazas> lstPlazas = new List<Plazas>();
            Command.CommandText = "select id, nombre from plazas where activo = 1";
            Command.Parameters.Clear();
            dtPlazas = SelectData(Command);
            for (int i = 0; i < dtPlazas.Rows.Count; i++)
            {
                Plazas p = new Plazas();
                p.id = int.Parse(dtPlazas.Rows[i]["id"].ToString());
                p.nombre = dtPlazas.Rows[i]["nombre"].ToString();
                lstPlazas.Add(p);
            }
            return lstPlazas;
        }

        public List<Plazas> obtenerPlaza(Plazas p)
        {
            DataTable dtPlaza = new DataTable();
            List<Plazas> lstPlaza = new List<Plazas>();
            Command.CommandText = "select id, nombre from plazas where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", p.id);
            dtPlaza = SelectData(Command);
            for (int i = 0; i < dtPlaza.Rows.Count; i++)
            {
                Plazas plaza = new Plazas();
                plaza.id = int.Parse(dtPlaza.Rows[i]["id"].ToString());
                plaza.nombre = dtPlaza.Rows[i]["nombre"].ToString();
                lstPlaza.Add(plaza);
            }
            return lstPlaza;
        }

        public int insertaPlaza(Plazas p)
        {
            Command.CommandText = "insert into plazas (nombre, activo) values (@nombre, @activo)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("nombre",p.nombre);
            Command.Parameters.AddWithValue("activo", p.activo);
            return Command.ExecuteNonQuery();
        }

        public int actualizaPlaza(Plazas p)
        {
            Command.CommandText = "update plazas set nombre = @nombre where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id",p.id);
            Command.Parameters.AddWithValue("nombre",p.nombre);
            return Command.ExecuteNonQuery();
        }

        public int bajaPlaza(Plazas p)
        {
            Command.CommandText = "update plazas set activo = @activo where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", p.id);
            Command.Parameters.AddWithValue("activo", p.activo);
            return Command.ExecuteNonQuery();
        }
        
    }
}
