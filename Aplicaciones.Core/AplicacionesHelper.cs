using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicaciones.Core
{
    public class AplicacionesHelper : Data.Obj.DataObj
    {
        public int insertaAplicacion(Aplicaciones a)
        {
            Command.CommandText = @"insert into Aplicaciones (idtrabajador, idempresa, iddeptopuesto, deptopuesto, fecha, registro, idusuario, periodoinicio, periodofin) values (
                                    @idtrabajador,@idempresa,@iddeptopuesto,@deptopuesto,@fecha,@registro,@idusuario, @periodoinicio, @periodofin)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", a.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", a.idempresa);
            Command.Parameters.AddWithValue("iddeptopuesto", a.iddeptopuesto);
            Command.Parameters.AddWithValue("deptopuesto", a.deptopuesto);
            Command.Parameters.AddWithValue("fecha", a.fecha);
            Command.Parameters.AddWithValue("registro", a.registro);
            Command.Parameters.AddWithValue("idusuario", a.idusuario);
            Command.Parameters.AddWithValue("periodoinicio", a.periodoinicio);
            Command.Parameters.AddWithValue("periodofin", a.periodofin);
            return Command.ExecuteNonQuery();
        }

        public List<Aplicaciones> obtenerFechasDeAplicacion(Aplicaciones a)
        {
            DataTable dt = new DataTable();
            Command.CommandText = @"select * from Aplicaciones where idtrabajador = @idtrabajador and periodoinicio = @periodoinicio and periodofin = @periodofin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("periodofin", a.periodofin);
            Command.Parameters.AddWithValue("periodoinicio", a.periodoinicio);
            Command.Parameters.AddWithValue("idtrabajador", a.idtrabajador);
            dt = SelectData(Command);
            List<Aplicaciones> lstAplicaciones = new List<Aplicaciones>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Aplicaciones ap = new Aplicaciones();
                ap.id = int.Parse(dt.Rows[i]["id"].ToString());
                ap.idtrabajador = int.Parse(dt.Rows[i]["idtrabajador"].ToString());
                ap.idempresa = int.Parse(dt.Rows[i]["idempresa"].ToString());
                ap.iddeptopuesto = int.Parse(dt.Rows[i]["iddeptopuesto"].ToString());
                ap.deptopuesto = dt.Rows[i]["deptopuesto"].ToString();
                ap.fecha = DateTime.Parse(dt.Rows[i]["fecha"].ToString());
                ap.registro = DateTime.Parse(dt.Rows[i]["registro"].ToString());
                ap.idusuario = int.Parse(dt.Rows[i]["idusuario"].ToString());
                ap.periodoinicio = DateTime.Parse(dt.Rows[i]["periodoinicio"].ToString());
                ap.periodofin = DateTime.Parse(dt.Rows[i]["periodofin"].ToString());
                lstAplicaciones.Add(ap);
            }
            return lstAplicaciones;
        }

        public int eliminaAplicacion(int id)
        {
            Command.CommandText = @"delete from Aplicaciones where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", id);
            return Command.ExecuteNonQuery();
        }

        public int aplica(int idtrabajador, DateTime inicio, DateTime fin, int periodo, string tipo, int valor)
        {
            if (tipo == "D")
            {
                Command.CommandText = @"update tmpPagoNomina set iddepartamento = @valor where idtrabajador = @idtrabajador and fechainicio = @fechainicio and fechafin = @fechafin";
            }
            else if (tipo == "P")
            {
                Command.CommandText = @"update tmpPagoNomina set idpuesto = @valor where idtrabajador = @idtrabajador and fechainicio = @fechainicio and fechafin = @fechafin";
            }

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("valor", valor);
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", inicio);
            Command.Parameters.AddWithValue("fechafin", fin);
            return Command.ExecuteNonQuery();
        }

        public int existeAplicacion(Aplicaciones a)
        {
            Command.CommandText = @"select count(*) from aplicaciones where idtrabajador = @idtrabajador and deptopuesto = @deptopuesto and periodoinicio = @periodoinicio
                                  and periodofin = @periodofin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", a.idtrabajador);
            Command.Parameters.AddWithValue("deptopuesto", a.deptopuesto);
            Command.Parameters.AddWithValue("periodoinicio", a.periodoinicio);
            Command.Parameters.AddWithValue("periodofin", a.periodofin);
            int dato = int.Parse(Select(Command).ToString());
            return dato;
        }
    }
}
