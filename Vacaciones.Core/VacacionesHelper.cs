using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacaciones.Core
{
    public class VacacionesHelper : Data.Obj.DataObj
    {
        public List<Vacaciones> obtenerVacaciones(Vacaciones v)
        {
            List<Vacaciones> lstVacaciones = new List<Vacaciones>();
            DataTable dtVacaciones = new DataTable();
            Command.CommandText = "select * from PagoVacaciones where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", v.idempresa);
            dtVacaciones = SelectData(Command);
            for (int i = 0; i < dtVacaciones.Rows.Count; i++)
            {
                Vacaciones vacacion = new Vacaciones();
                vacacion.id = int.Parse(dtVacaciones.Rows[i]["id"].ToString());
                vacacion.idtrabajador = int.Parse(dtVacaciones.Rows[i]["idtrabajador"].ToString());
                vacacion.idempresa = int.Parse(dtVacaciones.Rows[i]["idempresa"].ToString());
                vacacion.fechaingreso = DateTime.Parse(dtVacaciones.Rows[i]["fechaingreso"].ToString());
                vacacion.inicio = DateTime.Parse(dtVacaciones.Rows[i]["inicio"].ToString());
                vacacion.fin = DateTime.Parse(dtVacaciones.Rows[i]["fin"].ToString());
                vacacion.sd = decimal.Parse(dtVacaciones.Rows[i]["sd"].ToString());
                vacacion.diasderecho = int.Parse(dtVacaciones.Rows[i]["diasderecho"].ToString());
                vacacion.diasapagar = int.Parse(dtVacaciones.Rows[i]["diasapagar"].ToString());
                vacacion.diaspendientes = int.Parse(dtVacaciones.Rows[i]["diaspendientes"].ToString());
                vacacion.pv = decimal.Parse(dtVacaciones.Rows[i]["pv"].ToString());
                vacacion.pexenta = decimal.Parse(dtVacaciones.Rows[i]["pexenta"].ToString());
                vacacion.pgravada = decimal.Parse(dtVacaciones.Rows[i]["pgravada"].ToString());
                vacacion.isrgravada = decimal.Parse(dtVacaciones.Rows[i]["isrgravada"].ToString());
                vacacion.pagovacaciones = decimal.Parse(dtVacaciones.Rows[i]["pagovacaciones"].ToString());
                vacacion.totalprima = decimal.Parse(dtVacaciones.Rows[i]["totalprima"].ToString());
                vacacion.total = decimal.Parse(dtVacaciones.Rows[i]["total"].ToString());
                vacacion.fechapago = DateTime.Parse(dtVacaciones.Rows[i]["fechapago"].ToString());
                vacacion.pagada = bool.Parse(dtVacaciones.Rows[i]["pagada"].ToString());
                vacacion.pvpagada = bool.Parse(dtVacaciones.Rows[i]["pvpagada"].ToString());
                lstVacaciones.Add(vacacion);
            }
            return lstVacaciones;
        }

        public List<Vacaciones> obtenerVacacion(Vacaciones f)
        {
            List<Vacaciones> lstVacacion = new List<Vacaciones>();
            DataTable dtVacacion = new DataTable();
            Command.CommandText = "select * from PagoVacaciones where idempresa = @idempresa and id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", f.idempresa);
            Command.Parameters.AddWithValue("id", f.id);
            dtVacacion = SelectData(Command);
            for (int i = 0; i < dtVacacion.Rows.Count; i++)
            {
                Vacaciones vacacion = new Vacaciones();
                vacacion.id = int.Parse(dtVacacion.Rows[i]["id"].ToString());
                vacacion.idtrabajador = int.Parse(dtVacacion.Rows[i]["idtrabajador"].ToString());
                vacacion.idempresa = int.Parse(dtVacacion.Rows[i]["idempresa"].ToString());
                vacacion.fechaingreso = DateTime.Parse(dtVacacion.Rows[i]["fechaingreso"].ToString());
                vacacion.inicio = DateTime.Parse(dtVacacion.Rows[i]["inicio"].ToString());
                vacacion.fin = DateTime.Parse(dtVacacion.Rows[i]["fin"].ToString());
                vacacion.sd = decimal.Parse(dtVacacion.Rows[i]["sd"].ToString());
                vacacion.diasderecho = int.Parse(dtVacacion.Rows[i]["diasderecho"].ToString());
                vacacion.diasapagar = int.Parse(dtVacacion.Rows[i]["diasapagar"].ToString());
                vacacion.diaspendientes = int.Parse(dtVacacion.Rows[i]["diaspendientes"].ToString());
                vacacion.pv = decimal.Parse(dtVacacion.Rows[i]["pv"].ToString());
                vacacion.pexenta = decimal.Parse(dtVacacion.Rows[i]["pexenta"].ToString());
                vacacion.pgravada = decimal.Parse(dtVacacion.Rows[i]["pgravada"].ToString());
                vacacion.isrgravada = decimal.Parse(dtVacacion.Rows[i]["isrgravada"].ToString());
                vacacion.pagovacaciones = decimal.Parse(dtVacacion.Rows[i]["pagovacaciones"].ToString());
                vacacion.totalprima = decimal.Parse(dtVacacion.Rows[i]["totalprima"].ToString());
                vacacion.total = decimal.Parse(dtVacacion.Rows[i]["total"].ToString());
                vacacion.fechapago = DateTime.Parse(dtVacacion.Rows[i]["fechapago"].ToString());
                vacacion.pagada = bool.Parse(dtVacacion.Rows[i]["pagada"].ToString());
                vacacion.pvpagada = bool.Parse(dtVacacion.Rows[i]["pvpagada"].ToString());
                lstVacacion.Add(vacacion);
            }
            return lstVacacion;
        }

        public object vacacionesPagadas(Vacaciones v)
        {
            Command.CommandText = "select isnull(sum(pagovacaciones),0) as pagovacaciones from PagoVacaciones " +
                "where idtrabajador = @idtrabajador and inicio = @fechainicio and fin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", v.idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", v.inicio);
            Command.Parameters.AddWithValue("fechafin", v.fin);
            object dato = Select(Command);
            return dato;
        }

        public List<Vacaciones> primaVacacional(Vacaciones v)
        {
            List<Vacaciones> lstPrima = new List<Vacaciones>();
            DataTable dtPrima = new DataTable();
            Command.CommandText = "select isnull(sum(pv),0) as pv, isnull(sum(pexenta),0) as pexenta, isnull(sum(pgravada),0) as pgravada from PagoVacaciones " +
                "where idtrabajador = @idtrabajador and inicio = @fechainicio and fin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", v.idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", v.inicio);
            Command.Parameters.AddWithValue("fechafin", v.fin);
            dtPrima = SelectData(Command);
            for (int i = 0; i < dtPrima.Rows.Count; i++)
            {
                Vacaciones vacacion = new Vacaciones();
                vacacion.pv = decimal.Parse(dtPrima.Rows[i]["pv"].ToString());
                vacacion.pexenta = decimal.Parse(dtPrima.Rows[i]["pexenta"].ToString());
                vacacion.pgravada = decimal.Parse(dtPrima.Rows[i]["pgravada"].ToString());
                lstPrima.Add(vacacion);
            }
            return lstPrima;
        }

        public List<VacacionesPrima> obtenerVacacionesPrimaTrabajador(VacacionesPrima v)
        {
            List<VacacionesPrima> lstPrima = new List<VacacionesPrima>();
            DataTable dtPrima = new DataTable();
            Command.CommandText = "select * from VacacionesPrima where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", v.idtrabajador);
            dtPrima = SelectData(Command);
            for (int i = 0; i < dtPrima.Rows.Count; i++)
            {
                VacacionesPrima vp = new VacacionesPrima();
                vp.id = int.Parse(dtPrima.Rows[i]["id"].ToString());
                vp.idtrabajador = int.Parse(dtPrima.Rows[i]["idtrabajador"].ToString());
                vp.idempresa = int.Parse(dtPrima.Rows[i]["idempresa"].ToString());
                vp.periodoinicio = DateTime.Parse(dtPrima.Rows[i]["periodoinicio"].ToString());
                vp.periodofin = DateTime.Parse(dtPrima.Rows[i]["periodofin"].ToString());
                vp.diasderecho = int.Parse(dtPrima.Rows[i]["diasderecho"].ToString());
                vp.diaspago = int.Parse(dtPrima.Rows[i]["diaspago"].ToString());
                vp.diaspendientes = int.Parse(dtPrima.Rows[i]["diaspendientes"].ToString());
                vp.fechapago = DateTime.Parse(dtPrima.Rows[i]["fechapago"].ToString());
                vp.vacacionesprima = dtPrima.Rows[i]["vacacionesprima"].ToString();
                lstPrima.Add(vp);
            }
            return lstPrima;
        }

        public List<VacacionesPrima> obtenerVacacionesPrimaTrabajador(int id, int idTrabajador, DateTime inicio, DateTime fin)
        {
            List<VacacionesPrima> lstPrima = new List<VacacionesPrima>();
            DataTable dtPrima = new DataTable();
            Command.CommandText = "select * from VacacionesPrima where id = @id and idtrabajador = @idtrabajador and periodoinicio = @periodoinicio and periodofin = @periodofin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", id);
            Command.Parameters.AddWithValue("idtrabajador", idTrabajador);
            Command.Parameters.AddWithValue("periodoinicio", inicio);
            Command.Parameters.AddWithValue("periodofin", fin);
            dtPrima = SelectData(Command);
            for (int i = 0; i < dtPrima.Rows.Count; i++)
            {
                VacacionesPrima vp = new VacacionesPrima();
                vp.id = int.Parse(dtPrima.Rows[i]["id"].ToString());
                vp.idtrabajador = int.Parse(dtPrima.Rows[i]["idtrabajador"].ToString());
                vp.idempresa = int.Parse(dtPrima.Rows[i]["idempresa"].ToString());
                vp.periodoinicio = DateTime.Parse(dtPrima.Rows[i]["periodoinicio"].ToString());
                vp.periodofin = DateTime.Parse(dtPrima.Rows[i]["periodofin"].ToString());
                vp.diasderecho = int.Parse(dtPrima.Rows[i]["diasderecho"].ToString());
                vp.diaspago = int.Parse(dtPrima.Rows[i]["diaspago"].ToString());
                vp.diaspendientes = int.Parse(dtPrima.Rows[i]["diaspendientes"].ToString());
                vp.fechapago = DateTime.Parse(dtPrima.Rows[i]["fechapago"].ToString());
                vp.vacacionesprima = dtPrima.Rows[i]["vacacionesprima"].ToString();
                vp.fechainicio = DateTime.Parse(dtPrima.Rows[i]["fechainicio"].ToString());
                lstPrima.Add(vp);
            }
            return lstPrima;
        }

        public object pagoVacacionesPrima(VacacionesPrima vp)
        {
            Command.CommandText = @"select isnull(sum(diaspago),0) from VacacionesPrima where idtrabajador = @idtrabajador
                                    and idempresa = @idempresa
                                    and periodoinicio = @inicio and periodofin = @fin
                                    and vacacionesprima = @vacacionprima";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", vp.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", vp.idempresa);
            Command.Parameters.AddWithValue("inicio", vp.periodoinicio);
            Command.Parameters.AddWithValue("fin", vp.periodofin);
            Command.Parameters.AddWithValue("vacacionprima", vp.vacacionesprima);
            object dato = Select(Command);
            return dato;
        }

        public object existeVacacionesPrima(VacacionesPrima vp)
        {
            Command.CommandText = @"select count(id) from VacacionesPrima where idtrabajador = @idtrabajador
                                    and periodoinicio = @inicio and periodofin = @fin and vacacionesprima = @vacacionesprima";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", vp.idtrabajador);
            Command.Parameters.AddWithValue("inicio", vp.periodoinicio);
            Command.Parameters.AddWithValue("fin", vp.periodofin);
            Command.Parameters.AddWithValue("vacacionesprima", vp.vacacionesprima);
            object dato = Select(Command);
            return dato;
        }

        public object fechaInicio(VacacionesPrima vp)
        {
            Command.CommandText = @"select fechainicio from VacacionesPrima where idtrabajador = @idtrabajador
                                    and periodoinicio = @inicio and periodofin = @fin and vacacionesprima = @vacacionesprima";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", vp.idtrabajador);
            Command.Parameters.AddWithValue("inicio", vp.periodoinicio);
            Command.Parameters.AddWithValue("fin", vp.periodofin);
            Command.Parameters.AddWithValue("vacacionesprima", vp.vacacionesprima);
            object dato = Select(Command);
            return dato;
        }

        public object fechaFin(VacacionesPrima vp)
        {
            Command.CommandText = @"select fechafin from VacacionesPrima where idtrabajador = @idtrabajador
                                    and periodoinicio = @inicio and periodofin = @fin and vacacionesprima = @vacacionesprima";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", vp.idtrabajador);
            Command.Parameters.AddWithValue("inicio", vp.periodoinicio);
            Command.Parameters.AddWithValue("fin", vp.periodofin);
            Command.Parameters.AddWithValue("vacacionesprima", vp.vacacionesprima);
            object dato = Select(Command);
            return dato;
        }

        public object existeVacacionEnFalta(int id, DateTime fecha)
        {
            Command.CommandText = @"select count(*) from VacacionesPrima where idtrabajador = @idtrabajador and fechafin >= @fecha and 
                                    fechainicio <= @fecha and vacacionesprima = 'V'";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", id);
            Command.Parameters.AddWithValue("fecha", fecha);
            object dato = Select(Command);
            return dato;
        }

        public int insertaVacacion(VacacionesPrima vp)
        {
            Command.CommandText = @"insert into VacacionesPrima (idtrabajador,idempresa,periodoinicio,periodofin,diasderecho,diaspago,diaspendientes,fechapago,vacacionesprima,fechainicio,fechafin) 
                                    values (@idtrabajador,@idempresa,@periodoinicio,@periodofin,@diasderecho,@diaspago,@diaspendientes,@fechapago,@vacacionesprima,@fechainicio,@fechafin)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", vp.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", vp.idempresa);
            Command.Parameters.AddWithValue("periodoinicio", vp.periodoinicio);
            Command.Parameters.AddWithValue("periodofin", vp.periodofin);
            Command.Parameters.AddWithValue("diasderecho", vp.diasderecho);
            Command.Parameters.AddWithValue("diaspago", vp.diaspago);
            Command.Parameters.AddWithValue("diaspendientes", vp.diaspendientes);
            Command.Parameters.AddWithValue("fechapago", vp.fechapago);
            Command.Parameters.AddWithValue("vacacionesprima", vp.vacacionesprima);
            Command.Parameters.AddWithValue("fechainicio", vp.fechainicio);
            Command.Parameters.AddWithValue("fechafin", vp.fechafin);
            return Command.ExecuteNonQuery();
        }

        public int eliminaVacacion(VacacionesPrima vp)
        {
            Command.CommandText = "delete from VacacionesPrima where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", vp.id);
            return Command.ExecuteNonQuery();
        }

        public int insertaVacacion(Vacaciones v)
        {
            Command.CommandText = "insert into PagoVacaciones (idtrabajador, idempresa, fechaingreso, inicio, fin, sd, diasderecho, diasapagar, diaspendientes, pv, pexenta, pgravada, isrgravada, pagovacaciones, totalprima, total, fechapago, pagada, pvpagada) " +
                " values (@idtrabajador, @idempresa, @fechaingreso, @inicio, @fin, @sd, @diasderecho, @diasapagar, @diaspendientes, @pv, @pexenta, @pgravada, @isrgravada, @pagovacaciones, @totalprima, @total, @fechapago, @pagada, @pvpagada)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", v.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", v.idempresa);
            Command.Parameters.AddWithValue("fechaingreso", v.fechapago);
            Command.Parameters.AddWithValue("inicio", v.inicio);
            Command.Parameters.AddWithValue("fin", v.fin);
            Command.Parameters.AddWithValue("sd", v.sd);
            Command.Parameters.AddWithValue("diasderecho", v.diasderecho);
            Command.Parameters.AddWithValue("diasapagar", v.diasapagar);
            Command.Parameters.AddWithValue("diaspendientes", v.diaspendientes);
            Command.Parameters.AddWithValue("pv", v.pv);
            Command.Parameters.AddWithValue("pexenta", v.pexenta);
            Command.Parameters.AddWithValue("pgravada", v.pgravada);
            Command.Parameters.AddWithValue("isrgravada", v.isrgravada);
            Command.Parameters.AddWithValue("pagovacaciones", v.pagovacaciones);
            Command.Parameters.AddWithValue("totalprima", v.totalprima);
            Command.Parameters.AddWithValue("total", v.total);
            Command.Parameters.AddWithValue("fechapago", v.fechapago);
            Command.Parameters.AddWithValue("pagada", v.pagada);
            Command.Parameters.AddWithValue("pvpagada", v.pvpagada);
            return Command.ExecuteNonQuery();
        }

        public int eliminaVacacion(Vacaciones v)
        {
            Command.CommandText = "delete from PagoVacaciones where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", v.id);
            return Command.ExecuteNonQuery();
        }

        public void bulkVacaciones(DataTable dt, string tabla)
        {
            bulkCommand.DestinationTableName = tabla;
            bulkCommand.WriteToServer(dt);
            dt.Clear();
        }

        public int stpVacaciones()
        {
            Command.CommandText = "exec stp_InsertaVacacionesPrima";
            Command.Parameters.Clear();
            return Command.ExecuteNonQuery();
        }

        #region METODOS DIAS X DERECHO

        public object diasDerecho(DiasDerecho dd)
        {
            Command.CommandText = "select top 1 dias from vacaciones where anio <= @anio order by anio desc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("anio", dd.anio);
            object dato = Select(Command);
            return dato;
        }

        #endregion
    }
}
