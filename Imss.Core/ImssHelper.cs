using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imss.Core
{
    public class ImssHelper : Data.Obj.DataObj
    {
        public List<Imss> ObtenerImss()
        {
            List<Imss> lstImss = new List<Imss>();
            DataTable dtImss = new DataTable();
            Command.CommandText = "select * from tablaImss";
            dtImss = SelectData(Command);
            for (int i = 0; i < dtImss.Rows.Count; i++)
            {
                Imss imss = new Imss();
                imss.id = int.Parse(dtImss.Rows[i]["id"].ToString());
                imss.prestacion = dtImss.Rows[i]["prestacion"].ToString();
                imss.porcentaje = double.Parse(dtImss.Rows[i]["porcentaje"].ToString());
                lstImss.Add(imss);
            }
            return lstImss;
        }

        public List<Imss> ObtenerImss(Imss i)
        {
            List<Imss> lstImss = new List<Imss>();
            DataTable dtImss = new DataTable();
            Command.CommandText = "select * from tablaImss where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", i.id);
            dtImss = SelectData(Command);
            for (int j = 0; j < dtImss.Rows.Count; j++)
            {
                Imss imss = new Imss();
                imss.id = int.Parse(dtImss.Rows[j]["id"].ToString());
                imss.prestacion = dtImss.Rows[j]["prestacion"].ToString();
                imss.porcentaje = double.Parse(dtImss.Rows[j]["porcentaje"].ToString());
                lstImss.Add(imss);
            }
            return lstImss;
        }

        public int insertaImss(Imss i)
        {
            Command.CommandText = "insert into tablaImss (prestacion, porcentaje) values (@prestacion, @porcentaje)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("prestacion", i.prestacion);
            Command.Parameters.AddWithValue("porcentaje", i.porcentaje);
            return Command.ExecuteNonQuery();
        }

        public int actualizaImss(Imss i)
        {
            Command.CommandText = "update tablaImss set prestacion = @prestacion, porcentaje = @porcentaje where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", i.id);
            Command.Parameters.AddWithValue("prestacion", i.prestacion);
            Command.Parameters.AddWithValue("porcentaje", i.porcentaje);
            return Command.ExecuteNonQuery();
        }

        public int eliminarImss(Imss i)
        {
            Command.CommandText = "delete from tablaImss where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", i.id);
            return Command.ExecuteNonQuery();
        }
    }
}
