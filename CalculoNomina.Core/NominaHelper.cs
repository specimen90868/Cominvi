using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoNomina.Core
{
    public class NominaHelper : Data.Obj.DataObj
    {
        public List<DatosEmpleado> obtenerDatosEmpleado(int idEmpresa, int estatus, bool obracivil, DateTime inicio, DateTime fin)
        {
            List<DatosEmpleado> lstDatosEmpleados = new List<DatosEmpleado>();
            DataTable dtDatosEmpleados = new DataTable();
             Command.CommandText = @"select idtrabajador, iddepartamento, idpuesto, noempleado, nombres, paterno, materno, 0 as sueldo, 0 as despensa,
                0 as asistencia, 0 as puntualidad, 0 as horas from Trabajadores where idempresa = @idempresa and estatus = @estatus 
                and obracivil = @obracivil and idtrabajador not in (
                select idtrabajador from suaAltas 
                where idempresa = @idempresa and periodoinicio = @inicio and periodofin = @fin
                union
                select idtrabajador from suaReingresos 
                where idempresa = @idempresa and periodoinicio = @inicio and periodofin = @fin
                ) order by noempleado asc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("estatus", estatus);
            Command.Parameters.AddWithValue("obracivil", obracivil);
            Command.Parameters.AddWithValue("inicio", inicio);
            Command.Parameters.AddWithValue("fin", fin);
            dtDatosEmpleados = SelectData(Command);
            for (int i = 0; i < dtDatosEmpleados.Rows.Count; i++)
            {
                DatosEmpleado de = new DatosEmpleado();
                de.idtrabajador = int.Parse(dtDatosEmpleados.Rows[i]["idtrabajador"].ToString());
                de.iddepartamento = int.Parse(dtDatosEmpleados.Rows[i]["iddepartamento"].ToString());
                de.idpuesto = int.Parse(dtDatosEmpleados.Rows[i]["idpuesto"].ToString());
                de.noempleado = dtDatosEmpleados.Rows[i]["noempleado"].ToString();
                de.nombres = dtDatosEmpleados.Rows[i]["nombres"].ToString();
                de.paterno = dtDatosEmpleados.Rows[i]["paterno"].ToString();
                de.materno = dtDatosEmpleados.Rows[i]["materno"].ToString();
                de.sueldo = decimal.Parse(dtDatosEmpleados.Rows[i]["sueldo"].ToString());
                de.despensa = decimal.Parse(dtDatosEmpleados.Rows[i]["despensa"].ToString());
                de.asistencia = decimal.Parse(dtDatosEmpleados.Rows[i]["asistencia"].ToString());
                de.puntualidad = decimal.Parse(dtDatosEmpleados.Rows[i]["puntualidad"].ToString());
                de.horas = decimal.Parse(dtDatosEmpleados.Rows[i]["horas"].ToString());
                lstDatosEmpleados.Add(de);
            }
            return lstDatosEmpleados;
        }

        public List<DatosEmpleado> obtenerDatosEmpleado(int idEmpresa, string idtrabajadores)
        {
            string[] noEmp = idtrabajadores.Split(',');
            string commandText = "select idtrabajador, iddepartamento, idpuesto, noempleado, nombres, paterno, materno, 0 as sueldo, 0 as despensa, " +
                    "0 as asistencia, 0 as puntualidad, 0 as horas from Trabajadores where idempresa = @idempresa and idtrabajador in ({0}) order by noempleado asc";
            string[] paramNombre = noEmp.Select((s, i) => "@idtrabajador" + i.ToString()).ToArray();
            string inClausula = string.Join(",", paramNombre);

            List<DatosEmpleado> lstDatosEmpleados = new List<DatosEmpleado>();
            DataTable dtDatosEmpleados = new DataTable();
            Command.CommandText = string.Format(commandText, inClausula);
            Command.Parameters.Clear();
            for (int i = 0; i < paramNombre.Length; i++)
            {
                Command.Parameters.AddWithValue(paramNombre[i], noEmp[i]);
            }
            Command.Parameters.AddWithValue("idEmpresa", idEmpresa);

            dtDatosEmpleados = SelectData(Command);
            for (int i = 0; i < dtDatosEmpleados.Rows.Count; i++)
            {
                DatosEmpleado de = new DatosEmpleado();
                de.idtrabajador = int.Parse(dtDatosEmpleados.Rows[i]["idtrabajador"].ToString());
                de.iddepartamento = int.Parse(dtDatosEmpleados.Rows[i]["iddepartamento"].ToString());
                de.idpuesto = int.Parse(dtDatosEmpleados.Rows[i]["idpuesto"].ToString());
                de.noempleado = dtDatosEmpleados.Rows[i]["noempleado"].ToString();
                de.nombres = dtDatosEmpleados.Rows[i]["nombres"].ToString();
                de.paterno = dtDatosEmpleados.Rows[i]["paterno"].ToString();
                de.materno = dtDatosEmpleados.Rows[i]["materno"].ToString();
                de.sueldo = decimal.Parse(dtDatosEmpleados.Rows[i]["sueldo"].ToString());
                de.despensa = decimal.Parse(dtDatosEmpleados.Rows[i]["despensa"].ToString());
                de.asistencia = decimal.Parse(dtDatosEmpleados.Rows[i]["asistencia"].ToString());
                de.puntualidad = decimal.Parse(dtDatosEmpleados.Rows[i]["puntualidad"].ToString());
                de.horas = decimal.Parse(dtDatosEmpleados.Rows[i]["horas"].ToString());
                lstDatosEmpleados.Add(de);
            }
            return lstDatosEmpleados;
        }

        public List<DatosFaltaIncapacidad> obtenerDatosFaltaInc(int idEmpresa, int estatus, bool obracivil, DateTime inicio, DateTime fin)
        {
            List<DatosFaltaIncapacidad> lstDatosEmpleados = new List<DatosFaltaIncapacidad>();
            DataTable dtDatosEmpleados = new DataTable();
            Command.CommandText = @"select idtrabajador, iddepartamento, idpuesto, noempleado, nombres, paterno, materno 
                from Trabajadores where idempresa = @idempresa and estatus = @estatus and obracivil = @obracivil 
                and idtrabajador not in (
                select idtrabajador from suaAltas 
                where idempresa = @idempresa and periodoinicio = @inicio and periodofin = @fin
                union
                select idtrabajador from suaReingresos 
                where idempresa = @idempresa and periodoinicio = @inicio and periodofin = @fin
                ) order by noempleado asc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("estatus", estatus);
            Command.Parameters.AddWithValue("obracivil", obracivil);
            Command.Parameters.AddWithValue("inicio", inicio);
            Command.Parameters.AddWithValue("fin", fin);
            dtDatosEmpleados = SelectData(Command);
            for (int i = 0; i < dtDatosEmpleados.Rows.Count; i++)
            {
                DatosFaltaIncapacidad de = new DatosFaltaIncapacidad();
                de.idtrabajador = int.Parse(dtDatosEmpleados.Rows[i]["idtrabajador"].ToString());
                de.iddepartamento = int.Parse(dtDatosEmpleados.Rows[i]["iddepartamento"].ToString());
                de.idpuesto = int.Parse(dtDatosEmpleados.Rows[i]["idpuesto"].ToString());
                de.noempleado = dtDatosEmpleados.Rows[i]["noempleado"].ToString();
                de.nombres = dtDatosEmpleados.Rows[i]["nombres"].ToString();
                de.paterno = dtDatosEmpleados.Rows[i]["paterno"].ToString();
                de.materno = dtDatosEmpleados.Rows[i]["materno"].ToString();
                lstDatosEmpleados.Add(de);
            }
            return lstDatosEmpleados;
        }

        public List<tmpPagoNomina> obtenerDatosRecibo(tmpPagoNomina pn)
        {
            List<tmpPagoNomina> lstRecibo = new List<tmpPagoNomina>();
            DataTable dtRecibo = new DataTable();
            Command.CommandText = @"select idtrabajador, idconcepto, noconcepto, sum(cantidad) as cantidad 
                                    from tmpPagoNomina where tipoconcepto = @tipoconcepto and fechainicio = @fechainicio and fechafin = @fechafin
                                    and idtrabajador = @idtrabajador
                                    group by idtrabajador, idconcepto, noconcepto";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", pn.idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("tipoconcepto", pn.tipoconcepto);
            dtRecibo = SelectData(Command);
            for (int i = 0; i < dtRecibo.Rows.Count; i++)
            {
                tmpPagoNomina pago = new tmpPagoNomina();
                pago.idtrabajador = int.Parse(dtRecibo.Rows[i]["idtrabajador"].ToString());
                pago.idconcepto = int.Parse(dtRecibo.Rows[i]["idconcepto"].ToString());
                pago.noconcepto = int.Parse(dtRecibo.Rows[i]["noconcepto"].ToString());
                pago.cantidad = decimal.Parse(dtRecibo.Rows[i]["cantidad"].ToString());
                lstRecibo.Add(pago);
            }
            return lstRecibo;
        } 

        public List<tmpPagoNomina> obtenerPreNomina(tmpPagoNomina pn)
        {
            List<tmpPagoNomina> lstPreNomina = new List<tmpPagoNomina>();
            DataTable dtRecibo = new DataTable();
            Command.CommandText = "select * from tmpPagoNomina where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin and obracivil = @obracivil";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("obracivil", pn.obracivil);
            dtRecibo = SelectData(Command);
            for (int i = 0; i < dtRecibo.Rows.Count; i++)
            {
                tmpPagoNomina pago = new tmpPagoNomina();
                pago.id = int.Parse(dtRecibo.Rows[i]["id"].ToString());
                pago.idtrabajador = int.Parse(dtRecibo.Rows[i]["idtrabajador"].ToString());
                pago.idempresa = int.Parse(dtRecibo.Rows[i]["idempresa"].ToString());
                pago.idconcepto = int.Parse(dtRecibo.Rows[i]["idconcepto"].ToString());
                pago.noconcepto = int.Parse(dtRecibo.Rows[i]["noconcepto"].ToString());
                pago.tipoconcepto = dtRecibo.Rows[i]["tipoconcepto"].ToString();
                pago.exento = decimal.Parse(dtRecibo.Rows[i]["exento"].ToString());
                pago.gravado = decimal.Parse(dtRecibo.Rows[i]["gravado"].ToString());
                pago.cantidad = decimal.Parse(dtRecibo.Rows[i]["cantidad"].ToString());
                pago.fechainicio = DateTime.Parse(dtRecibo.Rows[i]["fechainicio"].ToString());
                pago.fechafin = DateTime.Parse(dtRecibo.Rows[i]["fechafin"].ToString());
                lstPreNomina.Add(pago);
            }
            return lstPreNomina;
        }

        public List<tmpPagoNomina> obtenerFechasPreNomina(tmpPagoNomina pn)
        {
            List<tmpPagoNomina> lstPreNomina = new List<tmpPagoNomina>();
            DataTable dtPreNomina = new DataTable();
            Command.CommandText = "select distinct fechainicio, fechafin from tmpPagoNomina where idempresa = @idempresa and guardada = 1";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            dtPreNomina = SelectData(Command);
            for (int i = 0; i < dtPreNomina.Rows.Count; i++)
            {
                tmpPagoNomina pago = new tmpPagoNomina();
                pago.fechainicio = DateTime.Parse(dtPreNomina.Rows[i]["fechainicio"].ToString());
                pago.fechafin = DateTime.Parse(dtPreNomina.Rows[i]["fechafin"].ToString());
                lstPreNomina.Add(pago);
            }
            return lstPreNomina;
        }

        public List<NetosNegativos> obtenerNetosNegativos(int idempresa, DateTime inicio, DateTime fin, int idtrabajador)
        {
            List<NetosNegativos> lstNetos = new List<NetosNegativos>();
            DataTable dtNetos = new DataTable();
            Command.CommandText = "exec stp_NetosNegativos @idempresa, @inicio, @fin, @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("inicio", inicio);
            Command.Parameters.AddWithValue("fin", fin);
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            dtNetos = SelectData(Command);
            for (int i = 0; i < dtNetos.Rows.Count; i++)
            {
                NetosNegativos neto = new NetosNegativos();
                neto.idtrabajador = int.Parse(dtNetos.Rows[i]["idtrabajador"].ToString());
                neto.noempleado = dtNetos.Rows[i]["noempleado"].ToString();
                neto.nombrecompleto = dtNetos.Rows[i]["nombrecompleto"].ToString();
                neto.noconcepto = int.Parse(dtNetos.Rows[i]["noconcepto"].ToString());
                neto.tipoconcepto = dtNetos.Rows[i]["tipoconcepto"].ToString();
                neto.concepto = dtNetos.Rows[i]["concepto"].ToString();
                neto.cantidad = decimal.Parse(dtNetos.Rows[i]["cantidad"].ToString());
                lstNetos.Add(neto);
            }
            return lstNetos;
        }

        public List<tmpPagoNomina> obtenerUltimaNomina(int idEmpresa, int tipoNomina)
        {
            List<tmpPagoNomina> lstPagoNomina = new List<tmpPagoNomina>();
            DataTable dtPagoNomina = new DataTable();
            Command.CommandText = @"select distinct top 1 fechainicio, fechafin from PagoNomina where idempresa = @idempresa and tiponomina = @tiponomina 
                                    order by fechainicio desc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("tiponomina", tipoNomina);
            dtPagoNomina = SelectData(Command);
            for (int i = 0; i < dtPagoNomina.Rows.Count; i++)
            {
                tmpPagoNomina pn = new tmpPagoNomina();
                pn.fechainicio = DateTime.Parse(dtPagoNomina.Rows[i]["fechainicio"].ToString());
                pn.fechafin = DateTime.Parse(dtPagoNomina.Rows[i]["fechafin"].ToString());
                lstPagoNomina.Add(pn);
            }
            return lstPagoNomina;
        }

        public List<tmpPagoNomina> obtenerUltimaNomina(int idEmpresa, bool obraCivil)
        {
            List<tmpPagoNomina> lstPagoNomina = new List<tmpPagoNomina>();
            DataTable dtPagoNomina = new DataTable();
            Command.CommandText = @"select distinct top 1 fechainicio, fechafin, guardada from tmpPagoNomina where idempresa = @idempresa 
                                    and obracivil = @obracivil order by fechainicio desc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("obracivil", obraCivil);
            dtPagoNomina = SelectData(Command);
            for (int i = 0; i < dtPagoNomina.Rows.Count; i++)
            {
                tmpPagoNomina pn = new tmpPagoNomina();
                pn.fechainicio = DateTime.Parse(dtPagoNomina.Rows[i]["fechainicio"].ToString());
                pn.fechafin = DateTime.Parse(dtPagoNomina.Rows[i]["fechafin"].ToString());
                pn.guardada = bool.Parse(dtPagoNomina.Rows[i]["guardada"].ToString());
                lstPagoNomina.Add(pn);
            }
            return lstPagoNomina;
        }

        public DataTable obtenerPreNominaTabular(tmpPagoNomina pn, string netocero, string order)
        {
            DataTable dtPagoNomina = new DataTable();
            Command.CommandText = "exec stp_rptPreNominaTabular @tiponomina, @fechainicio, @fechafin, @idempresa, @netocero, @order";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("tiponomina", pn.tiponomina);
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("netocero", netocero);
            Command.Parameters.AddWithValue("order", order);
            dtPagoNomina = SelectData(Command);
            return dtPagoNomina;
        }

        public DataTable obtenerNominaTabular(tmpPagoNomina pn, int deptoInicial, int deptoFinal, int empleadoInicial, int empleadoFinal, int tiponomina, string neto, string order)
        {
            DataTable dtPagoNomina = new DataTable();
            Command.CommandText = "exec stp_rptNominaTabular @fechainicio, @fechafin, @idempresa, @deptoInicial, @deptoFinal, @empleadoInicial, @empleadoFinal, @tiponomina, @neto, @order";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("deptoInicial", deptoInicial);
            Command.Parameters.AddWithValue("deptoFinal", deptoFinal);
            Command.Parameters.AddWithValue("empleadoInicial", empleadoInicial);
            Command.Parameters.AddWithValue("empleadoFinal", empleadoFinal);
            Command.Parameters.AddWithValue("tiponomina", tiponomina);
            Command.Parameters.AddWithValue("neto", neto);
            Command.Parameters.AddWithValue("order", order);
            dtPagoNomina = SelectData(Command);
            return dtPagoNomina;
        }

        public DataTable obtenerGravadosExentos(tmpPagoNomina pn)
        {
            DataTable dtPagoNomina = new DataTable();
            Command.CommandText = "exec stp_rptGravadosExentos @idempresa, @fechainicio, @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            dtPagoNomina = SelectData(Command);
            return dtPagoNomina;
        }

        public DataTable obtenerPreGravadosExentos(tmpPagoNomina pn)
        {
            DataTable dtPagoNomina = new DataTable();
            Command.CommandText = "exec stp_rptPreGravadosExentos @idempresa, @fechainicio, @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            dtPagoNomina = SelectData(Command);
            return dtPagoNomina;
        }

        public object existeNomina(int idempresa, DateTime inicio, DateTime fin)
        {
            Command.CommandText = "select count(*) from PagoNomina where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("fechainicio", inicio);
            Command.Parameters.AddWithValue("fechafin", fin);
            object dato = Select(Command);
            return dato;
        }

        public void bulkNomina(DataTable dt, string tabla)
        {
            bulkCommand.DestinationTableName = tabla;
            bulkCommand.WriteToServer(dt);
            dt.Clear();
        }

        public int eliminaCalculo(tmpPagoNomina pn)
        {
            Command.CommandText = "delete from tmpPagoNomina where idempresa = @idempresa and idtrabajador = @idtrabajador and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("idtrabajador", pn.idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            return Command.ExecuteNonQuery();
        }

        public int guardaPreNomina(tmpPagoNomina pn)
        {
            Command.CommandText = "update tmpPagoNomina set guardada = @guardada, obracivil = @obracivil where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin and obracivil = @obracivil";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("guardada", pn.guardada);
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("obracivil", pn.obracivil);
            return Command.ExecuteNonQuery();
        }

        public int aplicaBajaObraCivil(tmpPagoNomina pn)
        {
            Command.CommandText = @"exec stp_AplicaBajasObraCivil @idempresa, @fechainicio, @fechafin, @tiponomina, @obracivil";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("tiponomina", pn.tiponomina);
            Command.Parameters.AddWithValue("obracivil", pn.obracivil);
            return Command.ExecuteNonQuery();
        }

        public int cargaPreNomina(tmpPagoNomina pn)
        {
            Command.CommandText = "update tmpPagoNomina set guardada = 0 where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("guardada", pn.guardada);
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            return Command.ExecuteNonQuery();
        }
        
        public int actualizaHorasExtrasDespensa(tmpPagoNomina pn)
        {
            Command.CommandText = "update tmpPagoNomina set cantidad = @cantidad, exento = @exento, gravado = @gravado, modificado = @modificado where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin and " +
                "idtrabajador = @idtrabajador and noconcepto = @noconcepto";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("cantidad", pn.cantidad);
            Command.Parameters.AddWithValue("gravado", pn.gravado);
            Command.Parameters.AddWithValue("exento", pn.exento);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("idtrabajador", pn.idtrabajador);
            Command.Parameters.AddWithValue("noconcepto", pn.noconcepto);
            Command.Parameters.AddWithValue("modificado", pn.modificado);
            return Command.ExecuteNonQuery();
        }

        public int actualizaConcepto(tmpPagoNomina pn)
        {
            Command.CommandText = @"update tmpPagoNomina set exento = @exento, gravado = @gravado, cantidad = @cantidad 
            where idtrabajador = @idtrabajador and noconcepto = @noconcepto and fechainicio = @fechainicio and fechafin = @fechafin
            and idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("exento", pn.exento);
            Command.Parameters.AddWithValue("gravado", pn.gravado);
            Command.Parameters.AddWithValue("cantidad", pn.cantidad);
            Command.Parameters.AddWithValue("idtrabajador", pn.idtrabajador);
            //Command.Parameters.AddWithValue("id", pn.id);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("noconcepto", pn.noconcepto);
            return Command.ExecuteNonQuery();
        }

        public int actualizaConceptoModificado(tmpPagoNomina pn)
        {
            Command.CommandText = @"update tmpPagoNomina set exento = @exento, gravado = @gravado, cantidad = @cantidad, modificado = @modificado 
            where idtrabajador = @idtrabajador and noconcepto = @noconcepto and fechainicio = @fechainicio and fechafin = @fechafin
            and idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("exento", pn.exento);
            Command.Parameters.AddWithValue("gravado", pn.gravado);
            Command.Parameters.AddWithValue("cantidad", pn.cantidad);
            Command.Parameters.AddWithValue("idtrabajador", pn.idtrabajador);
            //Command.Parameters.AddWithValue("id", pn.id);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("noconcepto", pn.noconcepto);
            Command.Parameters.AddWithValue("modificado", pn.modificado);
            return Command.ExecuteNonQuery();
        }

        public int eliminaPreNomina(tmpPagoNomina pn)
        {
            Command.CommandText = "delete from tmpPagoNomina where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin and guardada = @guardada";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("guardada", pn.guardada);
            return Command.ExecuteNonQuery();
        }

        public int eliminaPreNomina(int idEmpresa, DateTime inicio, DateTime fin, bool modificado, bool obracivil)
        {
            Command.CommandText = @"delete from tmpPagoNomina where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin and modificado = @modificado
                and obracivil = @obracivil";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("fechainicio", inicio);
            Command.Parameters.AddWithValue("fechafin", fin);
            Command.Parameters.AddWithValue("modificado", modificado);
            Command.Parameters.AddWithValue("obracivil", obracivil);
            return Command.ExecuteNonQuery();
        }

        public int eliminaPreNomina(int idtrabajador)
        {
            Command.CommandText = "delete from tmpPagoNomina where idtrabajador = @idtrabajador and guardada = 0";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            return Command.ExecuteNonQuery();
        }

        public object existeConcepto(tmpPagoNomina pn)
        {
            Command.CommandText = @"select count(*) from tmpPagoNomina where idempresa = @idempresa and idtrabajador = @idtrabajador and 
                                    fechainicio = @fechainicio and fechafin = @fechafin and noconcepto = @noconcepto";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("idtrabajador", pn.idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("noconcepto", pn.noconcepto);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerImporteConcepto(tmpPagoNomina pn)
        {
            Command.CommandText = @"select cantidad from tmpPagoNomina where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin and
            idtrabajador = @idtrabajador and noconcepto = @noconcepto";
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("idtrabajador", pn.idtrabajador);
            Command.Parameters.AddWithValue("noconcepto", pn.noconcepto);
            object dato = Select(Command);
            return dato;
        }

        public int stpAutorizaNomina(int idempresa, DateTime inicio, DateTime fin, int idusuario, int tiponomina)
        {
            Command.CommandText = "exec stp_AutorizaNomina @idempresa, @fechainicio, @fechafin, @idusuario, @tiponomina";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("fechainicio", inicio);
            Command.Parameters.AddWithValue("fechafin", fin);
            Command.Parameters.AddWithValue("idusuario", idusuario);
            Command.Parameters.AddWithValue("tiponomina", tiponomina);
            return Command.ExecuteNonQuery();
        }

        public int actualizaDiasFechaPago(tmpPagoNomina pn)
        {
            Command.CommandText = @"update tmpPagoNomina set diaslaborados = @diaslaborados 
                                    where idtrabajador = @idtrabajador and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("diaslaborados", pn.diaslaborados);
            Command.Parameters.AddWithValue("idtrabajador", pn.idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            return Command.ExecuteNonQuery();
        }

        public int actualizaDiasFechaPagoExtraordinaria(tmpPagoNomina pn, DateTime fechapago)
        {
            Command.CommandText = @"update tmpPagoNomina set diaslaborados = @diaslaborados, fechapago = @fechapago 
                                    where fechainicio = @fechainicio and fechafin = @fechafin and tiponomina = @tiponomina";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("diaslaborados", pn.diaslaborados);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("fechapago", fechapago);
            Command.Parameters.AddWithValue("tiponomina", pn.tiponomina);
            return Command.ExecuteNonQuery();
        }

        public object obtenerNoPeriodo(int periodo, DateTime fecha)
        {
            Command.CommandText = "exec stp_NumeroPeriodo @periodo, @fecha";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("periodo", periodo);
            Command.Parameters.AddWithValue("fecha", fecha);
            object _periodo = Select(Command);
            return _periodo;
        }

        public object obtenerNoPeriodoExtraordinario(int idempresa, int tiponomina)
        {
            Command.CommandText = @"select top 1 noperiodo from PagoNomina where idempresa = @idempresa and tiponomina = @tiponomina
                    order by noperiodo desc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("tiponomina", tiponomina);
            object _periodo = Select(Command);
            return _periodo;
        }

        public int actualizarNoPeriodo(int idEmpresa, DateTime inicio, DateTime fin, int noPeriodo)
        {
            Command.CommandText = "update tmpPagoNomina set noperiodo = @noperiodo where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("fechainicio", inicio);
            Command.Parameters.AddWithValue("fechafin", fin);
            Command.Parameters.AddWithValue("noperiodo", noPeriodo);
            return Command.ExecuteNonQuery();
        }

        public List<tmpPagoNomina> obtenerPeriodosNomina(int idEmpresa, int tipoNomina)
        {
            DataTable dtPeriodos = new DataTable();
            List<tmpPagoNomina> lstPeriodos = new List<tmpPagoNomina>();
            Command.CommandText = @"select distinct fechainicio, fechafin from PagoNomina where idempresa = @idempresa and tiponomina = @tiponomina
                order by fechainicio";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("tiponomina", tipoNomina);
            dtPeriodos = SelectData(Command);
            for (int i = 0; i < dtPeriodos.Rows.Count; i++)
            {
                tmpPagoNomina pn = new tmpPagoNomina();
                pn.fechainicio = DateTime.Parse(dtPeriodos.Rows[i]["fechainicio"].ToString());
                pn.fechafin = DateTime.Parse(dtPeriodos.Rows[i]["fechafin"].ToString());
                lstPeriodos.Add(pn);
            }
            return lstPeriodos;
        }

        public object existeXMLTrabajador(int idempresa, int idtrabajador, DateTime inicio)
        {
            Command.CommandText = @"select count(*) from xmlCabecera where idempresa = @idempresa and idtrabajador = @idtrabajador and
                    periodoinicio = @inicio";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            Command.Parameters.AddWithValue("inicio", inicio);
            object dato = Select(Command);
            return dato;
        }

        public List<XmlCabecera> obtenerXmlTrabajador(int idempresa, int idtrabajador, DateTime inicio)
        {
            DataTable dtXml = new DataTable();
            List<XmlCabecera> lstXml = new List<XmlCabecera>();
            Command.CommandText = @"select idtrabajador, xml from xmlCabecera where idempresa = @idempresa and periodoinicio = @inicio 
                    and idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            Command.Parameters.AddWithValue("inicio", inicio);
            dtXml = SelectData(Command);
            for (int i = 0; i < dtXml.Rows.Count; i++)
            {
                XmlCabecera xml = new XmlCabecera();
                xml.idtrabajador = int.Parse(dtXml.Rows[i]["idtrabajador"].ToString());
                xml.xml = dtXml.Rows[i]["xml"].ToString();
                lstXml.Add(xml);
            }
            return lstXml;
        }

        public List<tmpPagoNomina> fechaPreNominaObraCivil(int idempresa, int idtrabajador, DateTime inicio, DateTime fin)
        {
            List<tmpPagoNomina> lstPreNomina = new List<tmpPagoNomina>();
            Command.CommandText = @"select distinct fechainicio, fechafin from tmpPagoNomina where idempresa = @idempresa and 
                obracivil = 1 and idtrabajador = @idtrabajador and guardada = 1 and fechainicio = @fechainicio and 
                fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", inicio);
            Command.Parameters.AddWithValue("fechafin", fin);
            DataTable dtPreNomina = new DataTable();
            dtPreNomina = SelectData(Command);
            for (int i = 0; i < dtPreNomina.Rows.Count; i++)
            {
                tmpPagoNomina tmp = new tmpPagoNomina();
                tmp.fechainicio = DateTime.Parse(dtPreNomina.Rows[i]["fechainicio"].ToString());
                tmp.fechafin = DateTime.Parse(dtPreNomina.Rows[i]["fechafin"].ToString());
                lstPreNomina.Add(tmp);
            }
            return lstPreNomina;
        }

        public int eliminaNominaTrabajador(int idtrabajador, DateTime inicio, DateTime fin, int tiponomina)
        {
            Command.CommandText = "delete from tmpPagoNomina where idtrabajador = @idtrabajador and fechainicio = @fechainicio and fechafin = @fechafin and tiponomina = @tiponomina and modificado = 0";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", inicio);
            Command.Parameters.AddWithValue("fechafin", fin);
            Command.Parameters.AddWithValue("tiponomina", tiponomina);
            return Command.ExecuteNonQuery();
        }

        public List<tmpPagoNomina> obtenerIdTrabajadoresTempNomina(int idempresa, int tiponomina, DateTime inicio, DateTime fin)
        {
            DataTable dtTrabajadores = new DataTable();
            List<tmpPagoNomina> lstPeriodos = new List<tmpPagoNomina>();
            Command.CommandText = @"select distinct idtrabajador from tmpPagoNomina where idempresa = @idempresa
                and fechainicio = @fechainicio and fechafin = @fechafin and tiponomina = @tiponomina
                order by idtrabajador asc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("tiponomina", tiponomina);
            Command.Parameters.AddWithValue("fechainicio", inicio);
            Command.Parameters.AddWithValue("fechafin", fin);
            dtTrabajadores = SelectData(Command);
            for (int i = 0; i < dtTrabajadores.Rows.Count; i++)
            {
                tmpPagoNomina pn = new tmpPagoNomina();
                pn.idtrabajador = int.Parse(dtTrabajadores.Rows[i]["idtrabajador"].ToString());
                lstPeriodos.Add(pn);
            }
            return lstPeriodos;
        }

        #region DATOS NOMINA POR TRABAJADOR
        public List<Nomina> conceptosNominaTrabajador(int idEmpresa, string tipoConcepto, int idTrabajador, DateTime inicio, DateTime fin)
        {
            Command.CommandText = "exec stp_DatosNominaTrabajador @idempresa, @tipoconcepto, @idtrabajador, @inicio, @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("tipoconcepto", tipoConcepto);
            Command.Parameters.AddWithValue("idtrabajador", idTrabajador);
            Command.Parameters.AddWithValue("inicio", inicio);
            Command.Parameters.AddWithValue("fin", fin);
            List<Nomina> lstConceptosNomina = new List<Nomina>();
            DataTable dtConceptosNomina = new DataTable();
            dtConceptosNomina = SelectData(Command);
            for (int i = 0; i < dtConceptosNomina.Rows.Count; i++)
            {
                Nomina n = new Nomina();
                n.idtrabajador = int.Parse(dtConceptosNomina.Rows[i]["idtrabajador"].ToString());
                n.id = int.Parse(dtConceptosNomina.Rows[i]["id"].ToString());
                n.noconcepto = int.Parse(dtConceptosNomina.Rows[i]["noconcepto"].ToString());
                n.concepto = dtConceptosNomina.Rows[i]["concepto"].ToString();
                n.tipoconcepto = dtConceptosNomina.Rows[i]["tipoconcepto"].ToString();
                n.formula = dtConceptosNomina.Rows[i]["formula"].ToString();
                n.formulaexento = dtConceptosNomina.Rows[i]["formulaexento"].ToString();
                lstConceptosNomina.Add(n);
            }
            return lstConceptosNomina;
        }

        public List<Nomina> conceptosNominaTrabajador(int idEmpresa, string tipoConcepto, int idTrabajador, int tipoNomina, DateTime inicio, DateTime fin)
        {
            Command.CommandText = "exec stp_DatosNominaRecalculoTrabajador @idempresa, @tiponomina, @inicio, @fin, @tipoconcepto, @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("tipoconcepto", tipoConcepto);
            Command.Parameters.AddWithValue("idtrabajador", idTrabajador);
            Command.Parameters.AddWithValue("tiponomina", tipoNomina);
            Command.Parameters.AddWithValue("inicio", inicio);
            Command.Parameters.AddWithValue("fin", fin);
            List<Nomina> lstConceptosNomina = new List<Nomina>();
            DataTable dtConceptosNomina = new DataTable();
            dtConceptosNomina = SelectData(Command);
            for (int i = 0; i < dtConceptosNomina.Rows.Count; i++)
            {
                Nomina n = new Nomina();
                n.idtrabajador = int.Parse(dtConceptosNomina.Rows[i]["idtrabajador"].ToString());
                n.id = int.Parse(dtConceptosNomina.Rows[i]["idconcepto"].ToString());
                n.noconcepto = int.Parse(dtConceptosNomina.Rows[i]["noconcepto"].ToString());
                n.tipoconcepto = dtConceptosNomina.Rows[i]["tipoconcepto"].ToString();
                n.formula = dtConceptosNomina.Rows[i]["formula"].ToString();
                n.formulaexento = dtConceptosNomina.Rows[i]["formulaexento"].ToString();
                n.modificado = bool.Parse(dtConceptosNomina.Rows[i]["modificado"].ToString());
                lstConceptosNomina.Add(n);
            }
            return lstConceptosNomina;
        }

        public List<tmpPagoNomina> obtenerPercepcionesTrabajador(tmpPagoNomina pn)
        {
            List<tmpPagoNomina> lstPreNomina = new List<tmpPagoNomina>();
            DataTable dtRecibo = new DataTable();
            Command.CommandText = @"select * from tmpPagoNomina where idempresa = @idempresa and fechainicio = @fechainicio 
                                    and fechafin = @fechafin and tipoconcepto = @tipoconcepto and tiponomina = @tiponomina
                                    and idtrabajador = @idtrabajador order by idtrabajador, noconcepto asc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("tipoconcepto", pn.tipoconcepto);
            Command.Parameters.AddWithValue("tiponomina", pn.tiponomina);
            Command.Parameters.AddWithValue("idtrabajador", pn.idtrabajador);
            dtRecibo = SelectData(Command);
            for (int i = 0; i < dtRecibo.Rows.Count; i++)
            {
                tmpPagoNomina pago = new tmpPagoNomina();
                pago.id = int.Parse(dtRecibo.Rows[i]["id"].ToString());
                pago.idtrabajador = int.Parse(dtRecibo.Rows[i]["idtrabajador"].ToString());
                pago.idempresa = int.Parse(dtRecibo.Rows[i]["idempresa"].ToString());
                pago.idconcepto = int.Parse(dtRecibo.Rows[i]["idconcepto"].ToString());
                pago.noconcepto = int.Parse(dtRecibo.Rows[i]["noconcepto"].ToString());
                pago.tipoconcepto = dtRecibo.Rows[i]["tipoconcepto"].ToString();
                pago.exento = decimal.Parse(dtRecibo.Rows[i]["exento"].ToString());
                pago.gravado = decimal.Parse(dtRecibo.Rows[i]["gravado"].ToString());
                pago.cantidad = decimal.Parse(dtRecibo.Rows[i]["cantidad"].ToString());
                pago.fechainicio = DateTime.Parse(dtRecibo.Rows[i]["fechainicio"].ToString());
                pago.fechafin = DateTime.Parse(dtRecibo.Rows[i]["fechafin"].ToString());
                lstPreNomina.Add(pago);
            }
            return lstPreNomina;
        }

        public List<tmpPagoNomina> obtenerConceptosGuardados(tmpPagoNomina pn)
        {
            List<tmpPagoNomina> lstPreNomina = new List<tmpPagoNomina>();
            DataTable dtRecibo = new DataTable();
            Command.CommandText = @"select * from tmpPagoNomina where idtrabajador = @idtrabajador and fechainicio = @inicio
                                    and fechafin = @fin and guardada = 1 and idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("inicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fin", pn.fechafin);
            Command.Parameters.AddWithValue("idtrabajador", pn.idtrabajador);
            dtRecibo = SelectData(Command);
            for (int i = 0; i < dtRecibo.Rows.Count; i++)
            {
                tmpPagoNomina pago = new tmpPagoNomina();
                pago.id = int.Parse(dtRecibo.Rows[i]["id"].ToString());
                pago.idtrabajador = int.Parse(dtRecibo.Rows[i]["idtrabajador"].ToString());
                pago.idempresa = int.Parse(dtRecibo.Rows[i]["idempresa"].ToString());
                pago.idconcepto = int.Parse(dtRecibo.Rows[i]["idconcepto"].ToString());
                pago.noconcepto = int.Parse(dtRecibo.Rows[i]["noconcepto"].ToString());
                pago.tipoconcepto = dtRecibo.Rows[i]["tipoconcepto"].ToString();
                pago.exento = decimal.Parse(dtRecibo.Rows[i]["exento"].ToString());
                pago.gravado = decimal.Parse(dtRecibo.Rows[i]["gravado"].ToString());
                pago.cantidad = decimal.Parse(dtRecibo.Rows[i]["cantidad"].ToString());
                pago.fechainicio = DateTime.Parse(dtRecibo.Rows[i]["fechainicio"].ToString());
                pago.fechafin = DateTime.Parse(dtRecibo.Rows[i]["fechafin"].ToString());
                pago.guardada = bool.Parse(dtRecibo.Rows[i]["guardada"].ToString());
                lstPreNomina.Add(pago);
            }
            return lstPreNomina;
        }

        public object exentoConceptoTrabajador(tmpPagoNomina pn)
        {
            Command.CommandText = @"select exento from tmpPagoNomina where idtrabajador = @idtrabajador and fechainicio = @inicio 
                                    and fechafin = @fin and noconcepto = @noconcepto and guardada = @guardada";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", pn.idtrabajador);
            Command.Parameters.AddWithValue("inicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fin", pn.fechafin);
            Command.Parameters.AddWithValue("noconcepto", pn.noconcepto);
            Command.Parameters.AddWithValue("guardada", pn.guardada);
            object dato = Select(Command);
            return dato;
        }
        #endregion

        #region DATOS PARA QRCODE
        public List<CodigoBidimensional> obtenerListaQr(int idempresa, DateTime inicio, DateTime fin)
        {
            DataTable dt = new DataTable();
            List<CodigoBidimensional> lstDatos = new List<CodigoBidimensional>();
            Command.CommandText = @"select e.rfc as Empresa, t.idtrabajador, t.rfc as Trabajador, xml.total, xml.uuid from 
                                    xmlCabecera xml
                                    inner join trabajadores t
                                    on xml.idtrabajador = t.idtrabajador 
                                    inner join Empresas e
                                    on t.idempresa = e.idempresa
                                    where xml.idempresa = @idempresa and 
                                    xml.periodoinicio = @fechainicio and xml.periodofin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("fechainicio", inicio);
            Command.Parameters.AddWithValue("fechafin", fin);
            dt = SelectData(Command);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CodigoBidimensional qr = new CodigoBidimensional();
                qr.re = dt.Rows[i]["Empresa"].ToString();
                qr.idtrabajador = int.Parse(dt.Rows[i]["idtrabajador"].ToString());
                qr.rr = dt.Rows[i]["Trabajador"].ToString();
                qr.tt = decimal.Parse(dt.Rows[i]["total"].ToString());
                qr.uuid = dt.Rows[i]["uuid"].ToString();
                lstDatos.Add(qr);
            }
            return lstDatos;
        }

        public int actualizaXml(int idempresa, DateTime inicio, DateTime fin, int idtrabajador, Byte[] qr)
        {
            Command.CommandText = @"update xmlCabecera set codeqr = @codeqr where idempresa = @idempresa
                and idtrabajador = @idtrabajador and periodoinicio = @inicio and periodofin = @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("codeqr", qr);
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            Command.Parameters.AddWithValue("inicio", inicio);
            Command.Parameters.AddWithValue("fin", fin);
            return Command.ExecuteNonQuery();
        }

        public int existeNullQR(int idempresa, DateTime inicio, DateTime fin)
        {
            Command.CommandText = @"select count(*) from xmlcabecera where idempresa = @idempresa and periodoinicio = @inicio
                and periodofin = @fin and codeqr is null";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("inicio", inicio);
            Command.Parameters.AddWithValue("fin", fin);
            Command.Parameters.AddWithValue("idempresa", idempresa);
            int dato = (int)Select(Command);
            return dato;
        }
        #endregion

        #region DATOS PARA REPORTE TABULAR

        public List<Nomina> obtenerConceptosReporteTabular(int idEmpresa, DateTime inicio, string tipoConcepto)
        {
            DataTable dtConceptos = new DataTable();
            List<Nomina> lstConcepto = new List<Nomina>();
            Command.CommandText = @"select pn.noconcepto, c.concepto
                from PagoNomina pn  inner join Conceptos c
                on pn.idconcepto = c.id
                where pn.idempresa = @idempresa and pn.fechainicio = @fechainicio
                and pn.tipoconcepto = @tipoconcepto
                group by pn.noconcepto, c.concepto
                order by pn.noconcepto asc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("fechainicio", inicio);
            Command.Parameters.AddWithValue("tipoconcepto", tipoConcepto);
            dtConceptos = SelectData(Command);
            for (int i = 0; i < dtConceptos.Rows.Count; i++)
            {
                Nomina n = new Nomina();
                n.noconcepto = int.Parse(dtConceptos.Rows[i]["noconcepto"].ToString());
                n.concepto = dtConceptos.Rows[i]["noconcepto"].ToString();
                lstConcepto.Add(n);
            }

            return lstConcepto;
        }

        #endregion
    }
}
