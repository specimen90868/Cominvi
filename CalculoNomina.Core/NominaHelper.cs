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
        public List<Nomina> obtenerDatosNomina(int idEmpresa, int estatus, string idTrabajadorLista = "")
        {
            List<Nomina> lstDatosNomina = new List<Nomina>();
            DataTable dtDatosNomina = new DataTable();
            Command.CommandText = "exec stp_DatosNomina @idempresa, @estatus";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("estatus", estatus);
            //Command.Parameters.AddWithValue("lista", idTrabajadorLista);
            dtDatosNomina = SelectData(Command);
            for (int i = 0; i < dtDatosNomina.Rows.Count; i++)
            {
                Nomina nom = new Nomina();
                nom.idtrabajador = int.Parse(dtDatosNomina.Rows[i]["idtrabajador"].ToString());
                nom.dias = int.Parse(dtDatosNomina.Rows[i]["dias"].ToString());
                nom.salariominimo = double.Parse(dtDatosNomina.Rows[i]["salariominimo"].ToString());
                nom.antiguedadmod = int.Parse(dtDatosNomina.Rows[i]["antiguedadmod"].ToString());
                nom.sdi = double.Parse(dtDatosNomina.Rows[i]["sdi"].ToString());
                nom.sd = double.Parse(dtDatosNomina.Rows[i]["sd"].ToString());
                nom.id = int.Parse(dtDatosNomina.Rows[i]["id"].ToString());
                nom.noconcepto = int.Parse(dtDatosNomina.Rows[i]["noconcepto"].ToString());
                nom.concepto = dtDatosNomina.Rows[i]["concepto"].ToString();
                nom.tipoconcepto = dtDatosNomina.Rows[i]["tipoconcepto"].ToString();
                nom.formula = dtDatosNomina.Rows[i]["formula"].ToString();
                nom.formulaexento = dtDatosNomina.Rows[i]["formulaexento"].ToString();
                lstDatosNomina.Add(nom);
            }
            return lstDatosNomina;
        }

        public List<NominaRecalculo> obtenerDatosNominaRecalculo(int idEmpresa, DateTime inicio, DateTime fin)
        {
            List<NominaRecalculo> lstDatosNomina = new List<NominaRecalculo>();
            DataTable dtDatosNomina = new DataTable();
            Command.CommandText = "exec stp_DatosNominaRecalculo @idempresa, @inicio, @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("inicio", inicio);
            Command.Parameters.AddWithValue("fin", fin);
            dtDatosNomina = SelectData(Command);
            for (int i = 0; i < dtDatosNomina.Rows.Count; i++)
            {
                NominaRecalculo nom = new NominaRecalculo();
                nom.id = int.Parse(dtDatosNomina.Rows[i]["id"].ToString());
                nom.idtrabajador = int.Parse(dtDatosNomina.Rows[i]["idtrabajador"].ToString());
                nom.dias = int.Parse(dtDatosNomina.Rows[i]["dias"].ToString());
                nom.salariominimo = double.Parse(dtDatosNomina.Rows[i]["salariominimo"].ToString());
                nom.antiguedadmod = int.Parse(dtDatosNomina.Rows[i]["antiguedadmod"].ToString());
                nom.sdi = double.Parse(dtDatosNomina.Rows[i]["sdi"].ToString());
                nom.sd = double.Parse(dtDatosNomina.Rows[i]["sd"].ToString());
                nom.idconcepto = int.Parse(dtDatosNomina.Rows[i]["idconcepto"].ToString());
                nom.noconcepto = int.Parse(dtDatosNomina.Rows[i]["noconcepto"].ToString());
                nom.tipoconcepto = dtDatosNomina.Rows[i]["tipoconcepto"].ToString();
                nom.formula = dtDatosNomina.Rows[i]["formula"].ToString();
                nom.formulaexento = dtDatosNomina.Rows[i]["formulaexento"].ToString();
                nom.modificado = bool.Parse(dtDatosNomina.Rows[i]["modificado"].ToString());
                lstDatosNomina.Add(nom);
            }
            return lstDatosNomina;
        }

        public List<DatosEmpleado> obtenerDatosEmpleado(int idEmpresa, int estatus)
        {
            List<DatosEmpleado> lstDatosEmpleados = new List<DatosEmpleado>();
            DataTable dtDatosEmpleados = new DataTable();
            Command.CommandText = "select cast(0 as bit) as chk, idtrabajador, iddepartamento, idpuesto, noempleado, nombres, paterno, materno, 0 as sueldo, 0 as despensa," +
                "0 as asistencia, 0 as puntualidad, 0 as horas from Trabajadores where idempresa = @idempresa and estatus = @estatus order by noempleado asc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("estatus", estatus);
            dtDatosEmpleados = SelectData(Command);
            for (int i = 0; i < dtDatosEmpleados.Rows.Count; i++)
            {
                DatosEmpleado de = new DatosEmpleado();
                de.chk = bool.Parse(dtDatosEmpleados.Rows[i]["chk"].ToString());
                de.idtrabajador = int.Parse(dtDatosEmpleados.Rows[i]["idtrabajador"].ToString());
                de.iddepartamento = int.Parse(dtDatosEmpleados.Rows[i]["iddepartamento"].ToString());
                de.idpuesto = int.Parse(dtDatosEmpleados.Rows[i]["idpuesto"].ToString());
                de.noempleado = dtDatosEmpleados.Rows[i]["noempleado"].ToString();
                de.nombres = dtDatosEmpleados.Rows[i]["nombres"].ToString();
                de.paterno = dtDatosEmpleados.Rows[i]["paterno"].ToString();
                de.materno = dtDatosEmpleados.Rows[i]["materno"].ToString();
                de.sueldo = double.Parse(dtDatosEmpleados.Rows[i]["sueldo"].ToString());
                de.despensa = double.Parse(dtDatosEmpleados.Rows[i]["despensa"].ToString());
                de.asistencia = double.Parse(dtDatosEmpleados.Rows[i]["asistencia"].ToString());
                de.puntualidad = double.Parse(dtDatosEmpleados.Rows[i]["puntualidad"].ToString());
                de.horas = double.Parse(dtDatosEmpleados.Rows[i]["horas"].ToString());
                lstDatosEmpleados.Add(de);
            }
            return lstDatosEmpleados;
        }

        public List<DatosFaltaIncapacidad> obtenerDatosFaltaInc(int idEmpresa, int estatus)
        {
            List<DatosFaltaIncapacidad> lstDatosEmpleados = new List<DatosFaltaIncapacidad>();
            DataTable dtDatosEmpleados = new DataTable();
            Command.CommandText = "select idtrabajador, iddepartamento, idpuesto, noempleado, nombres, paterno, materno " +
                "from Trabajadores where idempresa = @idempresa and estatus = @estatus order by noempleado asc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            Command.Parameters.AddWithValue("estatus", estatus);
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

        public List<tmpPagoNomina> obtenerPagoNomina(tmpPagoNomina pn)
        {
            List<tmpPagoNomina> lstPagoNomina = new List<tmpPagoNomina>();
            DataTable dtPagoNomina = new DataTable();
            Command.CommandText = "select * from tmpPagoNomina where idempresa = @idempresa and idconcepto in (1,2,3,5,6) and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            dtPagoNomina = SelectData(Command);

            for (int i = 0; i < dtPagoNomina.Rows.Count; i++)
            {
                tmpPagoNomina pago = new tmpPagoNomina();
                pago.id = int.Parse(dtPagoNomina.Rows[i]["id"].ToString());
                pago.idtrabajador = int.Parse(dtPagoNomina.Rows[i]["idtrabajador"].ToString());
                pago.idempresa = int.Parse(dtPagoNomina.Rows[i]["idempresa"].ToString());
                pago.idconcepto = int.Parse(dtPagoNomina.Rows[i]["idconcepto"].ToString());
                pago.tipoconcepto = dtPagoNomina.Rows[i]["tipoconcepto"].ToString();
                pago.exento = double.Parse(dtPagoNomina.Rows[i]["exento"].ToString());
                pago.gravado = double.Parse(dtPagoNomina.Rows[i]["gravado"].ToString());
                pago.cantidad = double.Parse(dtPagoNomina.Rows[i]["cantidad"].ToString());
                pago.fechainicio = DateTime.Parse(dtPagoNomina.Rows[i]["fechainicio"].ToString());
                pago.fechafin = DateTime.Parse(dtPagoNomina.Rows[i]["fechafin"].ToString());
                pago.guardada = bool.Parse(dtPagoNomina.Rows[i]["guardada"].ToString());
                lstPagoNomina.Add(pago);
            }
            return lstPagoNomina;
        }

        public List<tmpPagoNomina> obtenerDatosRecibo(tmpPagoNomina pn)
        {
            List<tmpPagoNomina> lstRecibo = new List<tmpPagoNomina>();
            DataTable dtRecibo = new DataTable();
            Command.CommandText = "select * from tmpPagoNomina where idtrabajador = @idtrabajador and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", pn.idtrabajador);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            dtRecibo = SelectData(Command);
            for (int i = 0; i < dtRecibo.Rows.Count; i++)
            {
                tmpPagoNomina pago = new tmpPagoNomina();
                pago.id = int.Parse(dtRecibo.Rows[i]["id"].ToString());
                pago.idtrabajador = int.Parse(dtRecibo.Rows[i]["idtrabajador"].ToString());
                pago.idempresa = int.Parse(dtRecibo.Rows[i]["idempresa"].ToString());
                pago.idconcepto = int.Parse(dtRecibo.Rows[i]["idconcepto"].ToString());
                pago.tipoconcepto = dtRecibo.Rows[i]["tipoconcepto"].ToString();
                pago.exento = double.Parse(dtRecibo.Rows[i]["exento"].ToString());
                pago.gravado = double.Parse(dtRecibo.Rows[i]["gravado"].ToString());
                pago.cantidad = double.Parse(dtRecibo.Rows[i]["cantidad"].ToString());
                pago.fechainicio = DateTime.Parse(dtRecibo.Rows[i]["fechainicio"].ToString());
                pago.fechafin = DateTime.Parse(dtRecibo.Rows[i]["fechafin"].ToString());
                lstRecibo.Add(pago);
            }
            return lstRecibo;
        }

        public List<tmpPagoNomina> obtenerPreNomina(tmpPagoNomina pn)
        {
            List<tmpPagoNomina> lstPreNomina = new List<tmpPagoNomina>();
            DataTable dtRecibo = new DataTable();
            Command.CommandText = "select * from tmpPagoNomina where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
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
                pago.exento = double.Parse(dtRecibo.Rows[i]["exento"].ToString());
                pago.gravado = double.Parse(dtRecibo.Rows[i]["gravado"].ToString());
                pago.cantidad = double.Parse(dtRecibo.Rows[i]["cantidad"].ToString());
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

        public List<NetosNegativos> obtenerNetosNegativos(int idempresa, DateTime inicio, DateTime fin)
        {
            List<NetosNegativos> lstNetos = new List<NetosNegativos>();
            DataTable dtNetos = new DataTable();
            Command.CommandText = "exec stp_NetosNegativos @idempresa, @inicio, @fin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("inicio", inicio);
            Command.Parameters.AddWithValue("fin", fin);
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

        public DataTable obtenerPreNominaTabular(tmpPagoNomina pn, string netocero, string order)
        {
            DataTable dtPagoNomina = new DataTable();
            Command.CommandText = "exec stp_rptPreNominaTabular @fechainicio, @fechafin, @idempresa, @netocero, @order";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("netocero", netocero);
            Command.Parameters.AddWithValue("order", order);
            dtPagoNomina = SelectData(Command);
            return dtPagoNomina;
        }

        public DataTable obtenerNominaTabular(tmpPagoNomina pn, int deptoInicial, int deptoFinal, int empleadoInicial, int empleadoFinal, int tiponomina)
        {
            DataTable dtPagoNomina = new DataTable();
            Command.CommandText = "exec stp_rptNominaTabular @fechainicio, @fechafin, @idempresa, @deptoInicial, @deptoFinal, @empleadoInicial, @empleadoFinal, @tiponomina";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("deptoInicial", deptoInicial);
            Command.Parameters.AddWithValue("deptoFinal", deptoFinal);
            Command.Parameters.AddWithValue("empleadoInicial", empleadoInicial);
            Command.Parameters.AddWithValue("empleadoFinal", empleadoFinal);
            Command.Parameters.AddWithValue("tiponomina", tiponomina);
            dtPagoNomina = SelectData(Command);
            return dtPagoNomina;
        }

        public object existePreNomina(DateTime inicio, DateTime fin)
        {
            Command.CommandText = "select count(*) from tmpPagoNomina where fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("fechainicio",inicio);
            Command.Parameters.AddWithValue("fechafin", fin);
            object dato = Select(Command);
            return dato;
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

        public int actualizaPreNomina(tmpPagoNomina pn)
        {
            Command.CommandText = "update tmpPagoNomina set guardada = @guardada where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("guardada", pn.guardada);
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            return Command.ExecuteNonQuery();
        }
        
        public int actualizaHorasExtrasDespensa(tmpPagoNomina pn)
        {
            Command.CommandText = "update tmpPagoNomina set cantidad = @cantidad, gravado = @gravado, modificado = @modificado where idempresa = @idempresa and fechainicio = @fechainicio and fechafin = @fechafin and " +
                "idtrabajador = @idtrabajador and noconcepto = @noconcepto";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("cantidad", pn.cantidad);
            Command.Parameters.AddWithValue("gravado", pn.gravado);
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
            where idtrabajador = @idtrabajador and id = @id and fechainicio = @fechainicio and fechafin = @fechafin
            and idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("exento", pn.exento);
            Command.Parameters.AddWithValue("gravado", pn.gravado);
            Command.Parameters.AddWithValue("cantidad", pn.cantidad);
            Command.Parameters.AddWithValue("idtrabajador", pn.idtrabajador);
            Command.Parameters.AddWithValue("id", pn.id);
            Command.Parameters.AddWithValue("fechainicio", pn.fechainicio);
            Command.Parameters.AddWithValue("fechafin", pn.fechafin);
            Command.Parameters.AddWithValue("idempresa", pn.idempresa);
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

        public int stpAutorizaNomina(int idempresa, DateTime inicio, DateTime fin, int idusuario)
        {
            Command.CommandText = "exec stp_AutorizaNomina @idempresa, @fechainicio, @fechafin, @idusuario";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            Command.Parameters.AddWithValue("fechainicio", inicio);
            Command.Parameters.AddWithValue("fechafin", fin);
            Command.Parameters.AddWithValue("idusuario", idusuario);
            return Command.ExecuteNonQuery();
        }
    }
}
