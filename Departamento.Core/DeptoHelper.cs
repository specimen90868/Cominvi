using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departamento.Core
{
    public class DeptoHelper : Data.Obj.DataObj
    {
        public List<Depto> obtenerDepartamentos()
        {
            DataTable dtDeptos = new DataTable();
            List<Depto> lstDeptos = new List<Depto>();
            Command.CommandText = "select id, descripcion from departamentos where estatus = 1";
            Command.Parameters.Clear();
            dtDeptos = SelectData(Command);
            for (int i = 0; i < dtDeptos.Rows.Count; i++)
            {
                Depto d = new Depto();
                d.id = int.Parse(dtDeptos.Rows[i]["id"].ToString());
                d.descripcion = dtDeptos.Rows[i]["descripcion"].ToString();
                lstDeptos.Add(d);
            }

            return lstDeptos;
        }

        public List<Depto> obtenerDepartamento(Depto d)
        {
            DataTable dtDepto = new DataTable();
            List<Depto> lstDepto = new List<Depto>();
            Command.CommandText = "select id, descripcion from departamentos where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", d.id);
            dtDepto = SelectData(Command);
            for (int i = 0; i < dtDepto.Rows.Count; i++)
            {
                Depto depto = new Depto();
                depto.id = int.Parse(dtDepto.Rows[i]["id"].ToString());
                depto.descripcion = dtDepto.Rows[i]["descripcion"].ToString();
                lstDepto.Add(depto);
            }

            return lstDepto;
        }

        public int insertaDepartamento(Depto d)
        {
            Command.CommandText = "insert into departamentos (descripcion, estatus) values (@descripcion, @estatus)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("descripcion", d.descripcion);
            Command.Parameters.AddWithValue("estatus", d.estatus);
            return Command.ExecuteNonQuery();
        }

        public int actualizaDepartamento(Depto d)
        {
            Command.CommandText = "update departamentos set descripcion = @descripcion where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", d.id);
            Command.Parameters.AddWithValue("descripcion", d.descripcion);
            return Command.ExecuteNonQuery();
        }

        public int bajaDepartamento(Depto d)
        {
            Command.CommandText = "update departamentos set estatus = @estatus where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", d.id);
            Command.Parameters.AddWithValue("estatus", d.estatus);
            return Command.ExecuteNonQuery();
        }
    }
}
