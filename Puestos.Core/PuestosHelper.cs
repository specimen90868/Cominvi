using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Puestos.Core
{
    public class PuestosHelper : Data.Obj.DataObj
    {
        public List<Puestos> obtenerPuestos()
        {
            DataTable dtPuestos = new DataTable();
            List<Puestos> lstPuestos = new List<Puestos>();
            Command.CommandText = "select id, descripcion from puestos";
            Command.Parameters.Clear();
            dtPuestos = SelectData(Command);
            for (int i = 0; i < dtPuestos.Rows.Count; i++)
            {
                Puestos p = new Puestos();
                p.id = int.Parse(dtPuestos.Rows[i]["id"].ToString());
                p.descripcion = dtPuestos.Rows[i]["descripcion"].ToString();
                lstPuestos.Add(p);
            }
            return lstPuestos;
        }

        public List<Puestos> obtenerPuesto(Puestos p)
        {
            DataTable dtPuestos = new DataTable();
            List<Puestos> lstPuestos = new List<Puestos>();
            Command.CommandText = "select id, descripcion from puestos where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id",p.id);
            dtPuestos = SelectData(Command);
            for (int i = 0; i < dtPuestos.Rows.Count; i++)
            {
                Puestos puesto = new Puestos();
                puesto.id = int.Parse(dtPuestos.Rows[i]["id"].ToString());
                puesto.descripcion = dtPuestos.Rows[i]["descripcion"].ToString();
                lstPuestos.Add(puesto);
            }
            return lstPuestos;
        }

        public int insertaPuesto(Puestos p)
        {
            Command.CommandText = "insert into puestos (descripcion, estatus) values (@descripcion,@estatus)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("descripcion", p.descripcion);
            Command.Parameters.AddWithValue("estatus",p.estatus);
            return Command.ExecuteNonQuery();
        }

        public int actualizaPuesto(Puestos p)
        {
            Command.CommandText = "update puestos set descripcion = @descripcion where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id",p.id);
            Command.Parameters.AddWithValue("descripcion", p.descripcion);
            return Command.ExecuteNonQuery();
        }

        public int bajaPuesto(Puestos p)
        {
            Command.CommandText = "update puestos set estatus = @estatus where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", p.id);
            Command.Parameters.AddWithValue("estatus", p.descripcion);
            return Command.ExecuteNonQuery();
        }
    }
}
