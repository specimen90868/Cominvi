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
        public List<Depto> obtenerDepartamentos(Depto depto)
        {
            DataTable dtDeptos = new DataTable();
            List<Depto> lstDeptos = new List<Depto>();
            Command.CommandText = "select id, descripcion from departamentos where estatus = 1 and idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", depto.idempresa);
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

        public List<Depto> obtenerDepartamentos(int idEmpresa, DateTime fecha, int tipoNomina, bool timbrados)
        {
            DataTable dtDeptos = new DataTable();
            List<Depto> lstDeptos = new List<Depto>();
            if (timbrados)
                Command.CommandText = @"select distinct cm.iddepartamento as id, cm.departamento as descripcion from cfdimaster cm
                                    where cm.idempresa = @idempresa and cm.periodoinicio = @fecha and cm.tiponomina = @tiponomina order by cm.iddepartamento asc";
            else
                Command.CommandText = @"select distinct pn.iddepartamento as id, d.descripcion from pagonomina pn inner join departamentos d on pn.iddepartamento = d.id
                                    where pn.idempresa = @idempresa and pn.fechainicio = @fecha and pn.tiponomina = @tiponomina order by pn.iddepartamento asc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("fecha", fecha);
            Command.Parameters.AddWithValue("tipoNomina", tipoNomina);
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

        public object obtenerIdDepartamento(string descripcion, int idEmpresa)
        {
            Command.CommandText = "select id from departamentos where idempresa = @idempresa and descripcion = @descripcion";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("descripcion", descripcion);
            object dato = Select(Command);
            return dato;
        }

        public int insertaDepartamento(Depto d)
        {
            Command.CommandText = "insert into departamentos (descripcion, estatus, idempresa) values (@descripcion, @estatus, @idempresa)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("descripcion", d.descripcion);
            Command.Parameters.AddWithValue("estatus", d.estatus);
            Command.Parameters.AddWithValue("idempresa", d.idempresa);
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
            Command.CommandText = "update departamentos set estatus = 0 where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", d.id);
            return Command.ExecuteNonQuery();
        }
    }
}
