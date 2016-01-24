using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;

namespace Empleados.Core
{
    public class EmpleadosHelper : Data.Obj.DataObj
    {
        public List<Empleados> obtenerEmpleados(Empleados e)
        {
            DataTable dtEmpleados = new DataTable();
            List<Empleados> lstEmpleados = new List<Empleados>();
            Command.CommandText = "select idtrabajador, noempleado, paterno, materno, nombres, nombrecompleto, curp, fechaingreso, antiguedad, sdi, sd, sueldo, cuenta, clabe, idbancario from trabajadores where idempresa = @idempresa and estatus = @estatus";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            Command.Parameters.AddWithValue("estatus", e.estatus);
            dtEmpleados = SelectData(Command);

            for (int i = 0; i < dtEmpleados.Rows.Count; i++)
            {
                Empleados empleado = new Empleados();
                empleado.idtrabajador = int.Parse(dtEmpleados.Rows[i]["idtrabajador"].ToString());
                empleado.noempleado = dtEmpleados.Rows[i]["noempleado"].ToString();
                empleado.paterno = dtEmpleados.Rows[i]["paterno"].ToString();
                empleado.materno = dtEmpleados.Rows[i]["materno"].ToString();
                empleado.nombres = dtEmpleados.Rows[i]["nombres"].ToString();
                empleado.nombrecompleto = dtEmpleados.Rows[i]["nombrecompleto"].ToString();
                empleado.curp = dtEmpleados.Rows[i]["curp"].ToString();
                empleado.fechaingreso = DateTime.Parse(dtEmpleados.Rows[i]["fechaingreso"].ToString());
                empleado.antiguedad = int.Parse(dtEmpleados.Rows[i]["antiguedad"].ToString());
                empleado.sdi = double.Parse(dtEmpleados.Rows[i]["sdi"].ToString());
                empleado.sd = double.Parse(dtEmpleados.Rows[i]["sd"].ToString());
                empleado.sueldo = double.Parse(dtEmpleados.Rows[i]["sueldo"].ToString());
                empleado.cuenta = dtEmpleados.Rows[i]["cuenta"].ToString();
                empleado.clabe = dtEmpleados.Rows[i]["clabe"].ToString();
                empleado.idbancario = dtEmpleados.Rows[i]["idbancario"].ToString();
                lstEmpleados.Add(empleado);
            }

            return lstEmpleados;
        }

        public List<Empleados> obtenerEmpleados(int idEmpresa)
        {
            DataTable dtEmpleados = new DataTable();
            List<Empleados> lstEmpleados = new List<Empleados>();
            Command.CommandText = "select idtrabajador, noempleado, paterno, materno, nombres, nombrecompleto, curp, fechaingreso, antiguedad, sdi, sd, sueldo, cuenta, clabe, idbancario, estatus from trabajadores where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            dtEmpleados = SelectData(Command);

            for (int i = 0; i < dtEmpleados.Rows.Count; i++)
            {
                Empleados empleado = new Empleados();
                empleado.idtrabajador = int.Parse(dtEmpleados.Rows[i]["idtrabajador"].ToString());
                empleado.noempleado = dtEmpleados.Rows[i]["noempleado"].ToString();
                empleado.paterno = dtEmpleados.Rows[i]["paterno"].ToString();
                empleado.materno = dtEmpleados.Rows[i]["materno"].ToString();
                empleado.nombres = dtEmpleados.Rows[i]["nombres"].ToString();
                empleado.nombrecompleto = dtEmpleados.Rows[i]["nombrecompleto"].ToString();
                empleado.curp = dtEmpleados.Rows[i]["curp"].ToString();
                empleado.fechaingreso = DateTime.Parse(dtEmpleados.Rows[i]["fechaingreso"].ToString());
                empleado.antiguedad = int.Parse(dtEmpleados.Rows[i]["antiguedad"].ToString());
                empleado.sdi = double.Parse(dtEmpleados.Rows[i]["sdi"].ToString());
                empleado.sd = double.Parse(dtEmpleados.Rows[i]["sd"].ToString());
                empleado.sueldo = double.Parse(dtEmpleados.Rows[i]["sueldo"].ToString());
                empleado.cuenta = dtEmpleados.Rows[i]["cuenta"].ToString();
                empleado.clabe = dtEmpleados.Rows[i]["clabe"].ToString();
                empleado.idbancario = dtEmpleados.Rows[i]["idbancario"].ToString();
                empleado.estatus = int.Parse(dtEmpleados.Rows[i]["estatus"].ToString());
                lstEmpleados.Add(empleado);
            }

            return lstEmpleados;
        }

        public List<Empleados> obtenerEmpleado(Empleados e)
        {
            DataTable dtEmpleados = new DataTable();
            List<Empleados> lstEmpleados = new List<Empleados>();
            
            Command.CommandText = "select * from trabajadores where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            
            dtEmpleados = SelectData(Command);

            for (int i = 0; i < dtEmpleados.Rows.Count; i++)
            {
                Empleados empleado = new Empleados();
                empleado.idtrabajador = int.Parse(dtEmpleados.Rows[i]["idtrabajador"].ToString());
                empleado.noempleado = dtEmpleados.Rows[i]["noempleado"].ToString();
                empleado.nombres = dtEmpleados.Rows[i]["nombres"].ToString();
                empleado.paterno = dtEmpleados.Rows[i]["paterno"].ToString();
                empleado.materno = dtEmpleados.Rows[i]["materno"].ToString();
                empleado.nombrecompleto = dtEmpleados.Rows[i]["nombrecompleto"].ToString();
                empleado.idempresa = int.Parse(dtEmpleados.Rows[i]["idempresa"].ToString());
                empleado.idperiodo = int.Parse(dtEmpleados.Rows[i]["idperiodo"].ToString());
                empleado.idsalario = int.Parse(dtEmpleados.Rows[i]["idsalario"].ToString());
                empleado.iddepartamento = int.Parse(dtEmpleados.Rows[i]["iddepartamento"].ToString());
                empleado.idpuesto = int.Parse(dtEmpleados.Rows[i]["idpuesto"].ToString());
                empleado.fechaingreso = DateTime.Parse(dtEmpleados.Rows[i]["fechaingreso"].ToString());
                empleado.antiguedad = int.Parse(dtEmpleados.Rows[i]["antiguedad"].ToString());
                empleado.fechaantiguedad = DateTime.Parse(dtEmpleados.Rows[i]["fechaantiguedad"].ToString());
                empleado.antiguedadmod = int.Parse(dtEmpleados.Rows[i]["antiguedadmod"].ToString());
                empleado.fechanacimiento = DateTime.Parse(dtEmpleados.Rows[i]["fechanacimiento"].ToString());
                empleado.edad = int.Parse(dtEmpleados.Rows[i]["edad"].ToString());
                empleado.rfc = dtEmpleados.Rows[i]["rfc"].ToString();
                empleado.curp = dtEmpleados.Rows[i]["curp"].ToString();
                empleado.nss = dtEmpleados.Rows[i]["nss"].ToString();
                empleado.digitoverificador = int.Parse(dtEmpleados.Rows[i]["digitoverificador"].ToString());
                empleado.tiposalario = int.Parse(dtEmpleados.Rows[i]["tiposalario"].ToString());
                empleado.sdi = double.Parse(dtEmpleados.Rows[i]["sdi"].ToString());
                empleado.sd = double.Parse(dtEmpleados.Rows[i]["sd"].ToString());
                empleado.sueldo = double.Parse(dtEmpleados.Rows[i]["sueldo"].ToString());
                empleado.cuenta = dtEmpleados.Rows[i]["cuenta"].ToString();
                empleado.clabe = dtEmpleados.Rows[i]["clabe"].ToString();
                empleado.idbancario = dtEmpleados.Rows[i]["idbancario"].ToString();
                empleado.metodopago = dtEmpleados.Rows[i]["metodopago"].ToString();
                empleado.tiporegimen = int.Parse(dtEmpleados.Rows[i]["tiporegimen"].ToString());

                lstEmpleados.Add(empleado);
            }

            return lstEmpleados;
        }

        public List<IncrementoSalarial> obtenerIncremento(Empleados e)
        {
            DataTable dtIncremento = new DataTable();
            List<IncrementoSalarial> lstEmpleadosIncremento = new List<IncrementoSalarial>();

            Command.CommandText = "exec stp_IncrementoSalarioAnual @idempresa, @estatus";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            Command.Parameters.AddWithValue("estatus", e.estatus);

            dtIncremento = SelectData(Command);

            for (int i = 0; i < dtIncremento.Rows.Count; i++)
            {
                IncrementoSalarial incremento = new IncrementoSalarial();
                incremento.chk = int.Parse(dtIncremento.Rows[i]["chk"].ToString());
                incremento.idtrabajador = int.Parse(dtIncremento.Rows[i]["idtrabajador"].ToString());
                incremento.noempleado = int.Parse(dtIncremento.Rows[i]["noempleado"].ToString());
                incremento.nombre = dtIncremento.Rows[i]["nombre"].ToString();
                incremento.sdivigente = double.Parse(dtIncremento.Rows[i]["sdivigente"].ToString());
                incremento.sdinuevo = double.Parse(dtIncremento.Rows[i]["sdinuevo"].ToString());
                incremento.antiguedad = int.Parse(dtIncremento.Rows[i]["antiguedad"].ToString());
                incremento.antiguedadmod = int.Parse(dtIncremento.Rows[i]["antiguedadmod"].ToString());
                incremento.fechaimss = DateTime.Parse(dtIncremento.Rows[i]["fechaimss"].ToString());
                lstEmpleadosIncremento.Add(incremento);
            }

            return lstEmpleadosIncremento;
        }

        public List<Empleados> obtenerAntiguedades(string noempleados)
        {
            string[] noEmp = noempleados.Split(',');
            string commandText = "select idtrabajador, idsalario, idperiodo, antiguedadmod, sdi, sd, fechaantiguedad, sueldo from trabajadores " +
                    "where noempleado in ({0})";
            string[] paramNombre = noEmp.Select((s, i) => "@noempleado" + i.ToString()).ToArray();
            string inClausula = string.Join(",", paramNombre);

            DataTable dtEmpleados = new DataTable();
            List<Empleados> lstEmpleados = new List<Empleados>();
            Command.CommandText = string.Format(commandText, inClausula);
            Command.Parameters.Clear();
            
            for (int i = 0; i < paramNombre.Length; i++)
            {
                Command.Parameters.AddWithValue(paramNombre[i], noEmp[i]);
            }

            dtEmpleados = SelectData(Command);

            for (int i = 0; i < dtEmpleados.Rows.Count; i++)
            {
                Empleados empleado = new Empleados();
                empleado.idtrabajador = int.Parse(dtEmpleados.Rows[i]["idtrabajador"].ToString());
                empleado.idsalario = int.Parse(dtEmpleados.Rows[i]["idsalario"].ToString());
                empleado.idperiodo = int.Parse(dtEmpleados.Rows[i]["idperiodo"].ToString());
                empleado.antiguedadmod = int.Parse(dtEmpleados.Rows[i]["antiguedadmod"].ToString());
                empleado.sdi = double.Parse(dtEmpleados.Rows[i]["sdi"].ToString());
                empleado.sd = double.Parse(dtEmpleados.Rows[i]["sd"].ToString());
                empleado.sueldo = double.Parse(dtEmpleados.Rows[i]["sueldo"].ToString());
                empleado.fechaantiguedad = DateTime.Parse(dtEmpleados.Rows[i]["fechaantiguedad"].ToString());
                lstEmpleados.Add(empleado);
            }

            return lstEmpleados;
        }

        public List<Empleados> obtenerSalarioDiario(string noempleados)
        {
            string[] noEmp = noempleados.Split(',');
            string commandText = "select idtrabajador, sd from trabajadores where noempleado in ({0})";
            string[] paramNombre = noEmp.Select((s, i) => "@noempleado" + i.ToString()).ToArray();
            string inClausula = string.Join(",", paramNombre);

            DataTable dtEmpleados = new DataTable();
            List<Empleados> lstEmpleados = new List<Empleados>();
            Command.CommandText = string.Format(commandText, inClausula);
            Command.Parameters.Clear();

            for (int i = 0; i < paramNombre.Length; i++)
            {
                Command.Parameters.AddWithValue(paramNombre[i], noEmp[i]);
            }

            dtEmpleados = SelectData(Command);

            for (int i = 0; i < dtEmpleados.Rows.Count; i++)
            {
                Empleados empleado = new Empleados();
                empleado.idtrabajador = int.Parse(dtEmpleados.Rows[i]["idtrabajador"].ToString());
                empleado.sd = double.Parse(dtEmpleados.Rows[i]["sd"].ToString());
                lstEmpleados.Add(empleado);
            }

            return lstEmpleados;
        }

        public List<Empleados> obtenerFechaAntiguedad(Empleados e)
        {
            DataTable dtFechas = new DataTable();
            List<Empleados> lstFechas = new List<Empleados>();

            Command.CommandText = "select idtrabajador, fechaingreso, fechaantiguedad, antiguedad, antiguedadmod, idperiodo from trabajadores where idempresa = @idempresa and estatus = 1";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);

            dtFechas = SelectData(Command);

            for (int i = 0; i < dtFechas.Rows.Count; i++)
            {
                Empleados empleado = new Empleados();
                empleado.idtrabajador = int.Parse(dtFechas.Rows[i]["idtrabajador"].ToString());
                empleado.fechaingreso = DateTime.Parse(dtFechas.Rows[i]["fechaingreso"].ToString());
                empleado.fechaantiguedad = DateTime.Parse(dtFechas.Rows[i]["fechaantiguedad"].ToString());
                empleado.idperiodo = int.Parse(dtFechas.Rows[i]["idperiodo"].ToString());
                empleado.antiguedad = int.Parse(dtFechas.Rows[i]["antiguedad"].ToString());
                empleado.antiguedadmod = int.Parse(dtFechas.Rows[i]["antiguedadmod"].ToString());
                lstFechas.Add(empleado);
            }

            return lstFechas;
        }

        public object obtenerSalarioDiario(Empleados e)
        {
            Command.CommandText = "select sd from trabajadores where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerSalarioDiarioIntegrado(Empleados e)
        {
            Command.CommandText = "select sdi from trabajadores where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerEstatus(Empleados e)
        {
            Command.CommandText = "select estatus from trabajadores where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador",e.idtrabajador);
            object estatus = Select(Command);
            return estatus;
        }

        public object obtenerIdTrabajador(Empleados e)
        {
            Command.CommandText = "select idtrabajador from trabajadores where rfc = @rfc";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("rfc", e.rfc);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerIdSalarioMinimo(int idTrabajador)
        {
            Command.CommandText = "select idsalario from trabajadores where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idTrabajador);
            object dato = Select(Command);
            return dato;
        }
        
        public int obtenerIdTrabajador(string noempleado, int idEmpresa)
        {
            Command.CommandText = "select idtrabajador from trabajadores where noempleado = @noempleado and idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("noempleado", noempleado);
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            object dato = Select(Command);
            return (int)dato;
        }
        
        public object obtenerIdPeriodo(string noempleado)
        {
            Command.CommandText = "select idperiodo from trabajadores where noempleado = @noempleado";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("noempleado", noempleado);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerIdPeriodo(int idTrabajador)
        {
            Command.CommandText = "select idperiodo from trabajadores where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idTrabajador);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerDiasPeriodo(int idtrabajador)
        {
            Command.CommandText = "select dias from dbo.Periodos where idperiodo = (select idperiodo from Trabajadores where idtrabajador = @idtrabajador)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerNss(Empleados e)
        {
            Command.CommandText = "select nss + convert(char(1),digitoverificador) as nss from trabajadores where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public object obtenerIdEmpresa(Empleados e)
        {
            Command.CommandText = "select idempresa from trabajadores where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public int insertaEmpleado(Empleados e)
        {
            Command.CommandText = "insert into trabajadores (noempleado,nombres,paterno,materno,nombrecompleto,idempresa,idperiodo,idsalario,iddepartamento,idpuesto,fechaingreso,antiguedad," + 
                "fechaantiguedad,antiguedadmod,fechanacimiento,edad,rfc,curp,nss,digitoverificador,tiposalario,sdi,sd,sueldo,estatus,idusuario,cuenta,clabe,idbancario,metodopago, tiporegimen) " +
                "values (@noempleado,@nombres,@paterno,@materno,@nombrecompleto,@idempresa,@idperiodo,@idsalario,@iddepartamento,@idpuesto,@fechaingreso,@antiguedad,@fechaantiguedad,@antiguedadmod," +
                "@fechanacimiento,@edad,@rfc,@curp,@nss,@digitoverificador,@tiposalario,@sdi,@sd,@sueldo,@estatus,@idusuario,@cuenta,@clabe,@idbancario, @metodopago, @tiporegimen)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("noempleado", e.noempleado);
            Command.Parameters.AddWithValue("nombres",e.nombres);
            Command.Parameters.AddWithValue("paterno", e.paterno);
            Command.Parameters.AddWithValue("materno", e.materno);
            Command.Parameters.AddWithValue("nombrecompleto", e.nombrecompleto);
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            Command.Parameters.AddWithValue("idperiodo", e.idperiodo);
            Command.Parameters.AddWithValue("idsalario", e.idsalario);
            Command.Parameters.AddWithValue("iddepartamento", e.iddepartamento);
            Command.Parameters.AddWithValue("idpuesto", e.idpuesto);
            Command.Parameters.AddWithValue("fechaingreso", e.fechaingreso);
            Command.Parameters.AddWithValue("antiguedad", e.antiguedad);
            Command.Parameters.AddWithValue("fechaantiguedad", e.fechaantiguedad);
            Command.Parameters.AddWithValue("antiguedadmod", e.antiguedadmod);
            Command.Parameters.AddWithValue("fechanacimiento", e.fechanacimiento);
            Command.Parameters.AddWithValue("edad", e.edad);
            Command.Parameters.AddWithValue("rfc", e.rfc);
            Command.Parameters.AddWithValue("curp", e.curp);
            Command.Parameters.AddWithValue("nss", e.nss);
            Command.Parameters.AddWithValue("digitoverificador", e.digitoverificador);
            Command.Parameters.AddWithValue("tiposalario", e.tiposalario);
            Command.Parameters.AddWithValue("sdi", e.sdi);
            Command.Parameters.AddWithValue("sd", e.sd);
            Command.Parameters.AddWithValue("sueldo", e.sueldo);
            Command.Parameters.AddWithValue("estatus", e.estatus);
            Command.Parameters.AddWithValue("idusuario", e.idusuario);
            Command.Parameters.AddWithValue("cuenta", e.cuenta);
            Command.Parameters.AddWithValue("clabe", e.clabe);
            Command.Parameters.AddWithValue("idbancario", e.idbancario);
            Command.Parameters.AddWithValue("metodopago", e.metodopago);
            Command.Parameters.AddWithValue("tiporegimen", e.tiporegimen);
            return Command.ExecuteNonQuery();
        }

        public int actualizaEmpleado(Empleados e)
        {
            Command.CommandText = "update trabajadores set noempleado = @noempleado, nombres = @nombres, paterno = @paterno, materno = @materno, nombrecompleto = @nombrecompleto," +
                "idperiodo = @idperiodo, idsalario = @idsalario, iddepartamento = @iddepartamento, idpuesto = @idpuesto, fechaingreso = @fechaingreso, antiguedad = @antiguedad, fechaantiguedad = @fechaantiguedad," + 
                "antiguedadmod = @antiguedadmod, fechanacimiento = @fechanacimiento, edad= @edad, rfc = @rfc, curp = @curp, nss = @nss, digitoverificador = @digitoverificador, " + 
                "tiposalario = @tiposalario, sdi = @sdi, sd = @sd, sueldo = @sueldo, cuenta = @cuenta, clabe = @clabe, idbancario = @idbancario, metodopago = @metodopago, tiporegimen = @tiporegimen where idtrabajador = @idtrabajador";
                
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            Command.Parameters.AddWithValue("noempleado", e.noempleado);
            Command.Parameters.AddWithValue("nombres", e.nombres);
            Command.Parameters.AddWithValue("paterno", e.paterno);
            Command.Parameters.AddWithValue("materno", e.materno);
            Command.Parameters.AddWithValue("nombrecompleto", e.nombrecompleto);
            Command.Parameters.AddWithValue("idperiodo", e.idperiodo);
            Command.Parameters.AddWithValue("idsalario", e.idsalario);
            Command.Parameters.AddWithValue("iddepartamento", e.iddepartamento);
            Command.Parameters.AddWithValue("idpuesto", e.idpuesto);
            Command.Parameters.AddWithValue("fechaingreso", e.fechaingreso);
            Command.Parameters.AddWithValue("antiguedad", e.antiguedad);
            Command.Parameters.AddWithValue("fechaantiguedad", e.fechaantiguedad);
            Command.Parameters.AddWithValue("antiguedadmod", e.antiguedadmod);
            Command.Parameters.AddWithValue("fechanacimiento", e.fechanacimiento);
            Command.Parameters.AddWithValue("edad", e.edad);
            Command.Parameters.AddWithValue("rfc", e.rfc);
            Command.Parameters.AddWithValue("curp", e.curp);
            Command.Parameters.AddWithValue("nss", e.nss);
            Command.Parameters.AddWithValue("digitoverificador", e.digitoverificador);
            Command.Parameters.AddWithValue("tiposalario", e.tiposalario);
            Command.Parameters.AddWithValue("sdi", e.sdi);
            Command.Parameters.AddWithValue("sd", e.sd);
            Command.Parameters.AddWithValue("sueldo", e.sueldo);
            Command.Parameters.AddWithValue("cuenta", e.cuenta);
            Command.Parameters.AddWithValue("clabe", e.clabe);
            Command.Parameters.AddWithValue("idbancario", e.idbancario);
            Command.Parameters.AddWithValue("metodopago", e.metodopago);
            Command.Parameters.AddWithValue("tiporegimen", e.tiporegimen);
            return Command.ExecuteNonQuery();
        }

        public int eliminarEmpleado(Empleados e)
        {
            Command.CommandText = "delete from trabajadores where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador",e.idtrabajador);
            return Command.ExecuteNonQuery();
        }

        public int bajaEmpleado(Empleados e)
        {
            Command.CommandText = "update trabajadores set estatus = @estatus where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("estatus",e.estatus);
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            return Command.ExecuteNonQuery();
        }

        public int reingreso(Empleados e)
        {
            Command.CommandText = "update trabajadores set idempresa = @idempresa, fechaingreso = @fechaingreso, fechaantiguedad = @fechaantiguedad, antiguedad = @antiguedad, antiguedadmod = @antiguedadmod," + 
                "iddepartamento = @iddepartamento, idpuesto = @idpuesto, idperiodo = @idperiodo, sueldo = @sueldo, sd = @sd, sdi = @sdi, estatus = @estatus, idusuario = @idusuario, cuenta = @cuenta, clabe = @clabe, idbancario = @idbancario, metodopago = @metodopago where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            Command.Parameters.AddWithValue("fechaingreso", e.fechaingreso);
            Command.Parameters.AddWithValue("fechaantiguedad", e.fechaantiguedad);
            Command.Parameters.AddWithValue("antiguedad", e.antiguedad);
            Command.Parameters.AddWithValue("antiguedadmod", e.antiguedadmod);
            Command.Parameters.AddWithValue("iddepartamento", e.iddepartamento);
            Command.Parameters.AddWithValue("idpuesto", e.idpuesto);
            Command.Parameters.AddWithValue("idperiodo", e.idperiodo);
            Command.Parameters.AddWithValue("sueldo", e.sueldo);
            Command.Parameters.AddWithValue("sd", e.sd);
            Command.Parameters.AddWithValue("sdi", e.sdi);
            Command.Parameters.AddWithValue("estatus", e.estatus);
            Command.Parameters.AddWithValue("idusuario", e.idusuario);
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            Command.Parameters.AddWithValue("cuenta", e.cuenta);
            Command.Parameters.AddWithValue("clabe", e.clabe);
            Command.Parameters.AddWithValue("idbancario", e.idbancario);
            Command.Parameters.AddWithValue("metodopago", e.idbancario);
            return Command.ExecuteNonQuery();
        }

        public int actualizaSueldo(Empleados e)
        {
            Command.CommandText = "update trabajadores set sueldo = @sueldo, sd = @sd, sdi = @sdi where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("sueldo", e.sueldo);
            Command.Parameters.AddWithValue("sd", e.sd);
            Command.Parameters.AddWithValue("sdi", e.sdi);
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            return Command.ExecuteNonQuery();
        }

        public int actualizaAntiguedad(int idEmpresa)
        {
            Command.CommandText = "exec stp_ActualizaAntiguedad @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idEmpresa);
            return Command.ExecuteNonQuery();
        }

        public object existeEmpleado(Empleados e)
        {
            Command.CommandText = "select count(idtrabajador) from trabajadores where nss = @nss and digitoverificador = @digito";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("nss", e.nss);
            Command.Parameters.AddWithValue("digito", e.digitoverificador);
            object dato = Select(Command);
            return dato;
        }

    }

    public class RFC
    {
        public string ObtieneRFC(string _paterno, string _materno, string _nombre)
        {
            string rfc = "";
            string paterno = PalabrasNoUtilizadas(_paterno.TrimStart().TrimEnd()).TrimEnd();
            string materno = PalabrasNoUtilizadas(_materno.TrimStart().TrimEnd()).TrimEnd();
            string nombre = PalabrasNoUtilizadas(_nombre.TrimStart().TrimEnd()).TrimEnd();
            nombre = PalabrasComunes(nombre);

            int cuentaPaterno = 0, cuentaMaterno = 0;
            cuentaPaterno = paterno.Length;
            cuentaMaterno = materno.Length;

            if (cuentaPaterno == 0)
            {
                materno = materno.Substring(0, 2);
                nombre = nombre.Substring(0, 2);
                rfc = materno + nombre;
            }
            if (cuentaMaterno == 0)
            {
                paterno = paterno.Substring(0, 2);
                nombre = nombre.Substring(0, 2);
                rfc = paterno + nombre;
            }

            if (cuentaPaterno == 1 || cuentaPaterno == 2)
            {
                paterno = paterno.Substring(0, 1);
                materno = materno.Substring(0, 1);
                nombre = nombre.Substring(0, 2);
                rfc = paterno + materno + nombre;
            }

            if (cuentaPaterno > 2 && cuentaMaterno > 2)
            {
                bool esVocal = false;
                string primerLetraPaterno = paterno.Substring(0, 1);
                string primerVocal = "";
                string primerLetraMaterno = materno.Substring(0, 1);
                string primerLetraNombre = nombre.Substring(0, 1);
                char[] letras = paterno.ToCharArray();
                for (int i = 1; i < letras.Length; i++)
                {
                    esVocal = EsVocal(letras[i]);
                    if (esVocal)
                    {
                        primerVocal = letras[i].ToString();
                        break;
                    }
                }
                rfc = primerLetraPaterno + primerVocal + primerLetraMaterno + primerLetraNombre;
                rfc = PalabrasInconvenientes(rfc);
            }

            return rfc;
        }

        public string PalabrasNoUtilizadas(string valor)
        {
            string[] valores = valor.Split(' ');
            string _valor = "";
            for (int i = 0; i < valores.Length; i++)
            {
                switch (valores[i])
                {
                    case "DE": valores[i] = valores[i].Replace("DE", ""); break;
                    case "LA": valores[i] = valores[i].Replace("LA", ""); break;
                    case "LAS": valores[i] = valores[i].Replace("LAS", ""); break;
                    case "MC": valores[i] = valores[i].Replace("MC", ""); break;
                    case "VON": valores[i] = valores[i].Replace("VON", ""); break;
                    case "DEL": valores[i] = valores[i].Replace("DEL", ""); break;
                    case "LOS": valores[i] = valores[i].Replace("LOS", ""); break;
                    case "Y": valores[i] = valores[i].Replace("Y", ""); break;
                    case "MAC": valores[i] = valores[i].Replace("MAC", ""); break;
                    case "VAN": valores[i] = valores[i].Replace("VAN", ""); break;
                    case "MI": valores[i] = valores[i].Replace("MI", ""); break;
                }
            }

            for (int i = 0; i < valores.Length; i++)
            {
                if (valores[i] != "")
                    _valor += valores[i] + " ";
            }
            return _valor;
        }

        public string PalabrasComunes(string valor)
        {
            string[] valores = valor.Split(' ');
            string _valor = "";

            if (valores.Length == 1)
            {
                _valor = valor;
            }

            if (valores.Length == 2)
            {
                bool flag1 = false, flag2 = false;
                if (valores[0] == "MARIA" || valores[0] == "JOSE" || valores[0] == "MARÍA" || valores[0] == "JOSÉ")
                    flag1 = true;
                if (valores[1] == "MARIA" || valores[1] == "JOSE" || valores[1] == "MARÍA" || valores[1] == "JOSÉ")
                    flag2 = true;
                if (flag1 && flag2)
                    _valor = valores[1];
                else
                {
                    for (int i = 0; i < valores.Length; i++)
                    {
                        switch (valores[i])
                        {
                            case "MARIA": valores[i] = valores[i].Replace("MARIA", ""); break;
                            case "MARÍA": valores[i] = valores[i].Replace("MARÍA", ""); break;
                            case "JOSE": valores[i] = valores[i].Replace("JOSE", ""); ; break;
                            case "JOSÉ": valores[i] = valores[i].Replace("JOSÉ", ""); break;
                            case "J": valores[i] = valores[i].Replace("J", ""); break;
                            case "J.": valores[i] = valores[i].Replace("J.", ""); break;
                            case "MA": valores[i] = valores[i].Replace("J.", ""); break;
                            case "MA.": valores[i] = valores[i].Replace("J.", ""); break;
                        }
                    }
                    for (int i = 0; i < valores.Length; i++)
                    {
                        if (valores[i] != "")
                            _valor += valores[i] + " ";
                    }
                }
            }

            if (valores.Length > 2)
            {
                for (int i = 0; i < valores.Length; i++)
                {
                    switch (valores[i])
                    {
                        case "MARIA": valores[i] = valores[i].Replace("MARIA", ""); break;
                        case "MARÍA": valores[i] = valores[i].Replace("MARÍA", ""); break;
                        case "JOSE": valores[i] = valores[i].Replace("JOSE", ""); ; break;
                        case "JOSÉ": valores[i] = valores[i].Replace("JOSÉ", ""); break;
                        case "J": valores[i] = valores[i].Replace("J", ""); break;
                        case "J.": valores[i] = valores[i].Replace("J.", ""); break;
                        case "MA": valores[i] = valores[i].Replace("J.", ""); break;
                        case "MA.": valores[i] = valores[i].Replace("J.", ""); break;
                    }
                }
                for (int i = 0; i < valores.Length; i++)
                {
                    if (valores[i] != "")
                        _valor += valores[i] + " ";
                }
            }
            return _valor;
        }

        public bool EsVocal(char c)
        {
            bool vocal = false;
            switch (c)
            {
                case 'a': vocal = true; break;
                case 'e': vocal = true; break;
                case 'i': vocal = true; break;
                case 'o': vocal = true; break;
                case 'u': vocal = true; break;

                case 'A': vocal = true; break;
                case 'E': vocal = true; break;
                case 'I': vocal = true; break;
                case 'O': vocal = true; break;
                case 'U': vocal = true; break;

                case 'Á': vocal = true; break;
                case 'É': vocal = true; break;
                case 'Í': vocal = true; break;
                case 'Ó': vocal = true; break;
                case 'Ú': vocal = true; break;
                default:
                    vocal = false; break;
            }
            return vocal;
        }

        public string PalabrasInconvenientes(string rfc)
        {
            switch (rfc)
            {
                case "BUEI": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "BUEY": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "CACA": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "CACO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "CAGA": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "CAGO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "CAKA": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "CAKO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "COGE": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "COJA": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "COJE": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "COJI": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "COJO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "CULO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "FETO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "GUEY": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "JOTO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "KACA": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "KACO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "KAGA": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "KAGO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "KOGE": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "KOJO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "KAKA": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "KULO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "MAME": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "MAMO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "MEAR": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "MEAS": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "MEON": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "MION": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "MOCO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "MULA": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "PEDA": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "PEDO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "PENE": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "PUTA": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "PUTO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "PITO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "QULO": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "RATA": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
                case "RUIN": rfc = rfc.Replace(rfc.Substring(rfc.Length - 1, 1), "X"); break;
            }
            return rfc;
        }

        public string ClaveHomonimia(string _paterno, string _materno, string _nombre)
        {
            string paterno = _paterno.TrimStart().TrimEnd();
            string materno = _materno.TrimStart().TrimEnd();
            string nombre = _nombre.TrimStart().TrimEnd();
            string nombreCompleto = "";

            int cuentaPaterno = 0, cuentaMaterno = 0;
            cuentaPaterno = paterno.Length;
            cuentaMaterno = materno.Length;

            if (cuentaPaterno == 0)
                nombreCompleto = materno + " " + nombre;

            if (cuentaMaterno == 0)
                nombreCompleto = paterno + " " + nombre;

            if (cuentaPaterno != 0 && cuentaMaterno != 0)
                nombreCompleto = paterno + " " + materno + " " + nombre;

            char[] valoresLetras = nombreCompleto.ToCharArray();
            string[] valores = new string[nombreCompleto.Length];
            for (int i = 0; i < valoresLetras.Length; i++)
            {
                switch (valoresLetras[i])
                {
                    case ' ': valores[i] = "00"; break;
                    case '0': valores[i] = "00"; break;
                    case '1': valores[i] = "01"; break;
                    case '2': valores[i] = "02"; break;
                    case '3': valores[i] = "03"; break;
                    case '4': valores[i] = "04"; break;
                    case '5': valores[i] = "05"; break;
                    case '6': valores[i] = "06"; break;
                    case '7': valores[i] = "07"; break;
                    case '8': valores[i] = "08"; break;
                    case '9': valores[i] = "09"; break;
                    case '&': valores[i] = "10"; break;
                    case 'A': valores[i] = "11"; break;
                    case 'B': valores[i] = "12"; break;
                    case 'C': valores[i] = "13"; break;
                    case 'D': valores[i] = "14"; break;
                    case 'E': valores[i] = "15"; break;
                    case 'F': valores[i] = "16"; break;
                    case 'G': valores[i] = "17"; break;
                    case 'H': valores[i] = "18"; break;
                    case 'I': valores[i] = "19"; break;
                    case 'J': valores[i] = "21"; break;
                    case 'K': valores[i] = "22"; break;
                    case 'L': valores[i] = "23"; break;
                    case 'M': valores[i] = "24"; break;
                    case 'N': valores[i] = "25"; break;
                    case 'O': valores[i] = "26"; break;
                    case 'P': valores[i] = "27"; break;
                    case 'Q': valores[i] = "28"; break;
                    case 'R': valores[i] = "29"; break;
                    case 'S': valores[i] = "32"; break;
                    case 'T': valores[i] = "33"; break;
                    case 'U': valores[i] = "34"; break;
                    case 'V': valores[i] = "35"; break;
                    case 'W': valores[i] = "36"; break;
                    case 'X': valores[i] = "37"; break;
                    case 'Y': valores[i] = "38"; break;
                    case 'Z': valores[i] = "39"; break;
                    case 'Ñ': valores[i] = "40"; break;
                }
            }
            string numeros = "0";
            for (int i = 0; i < valores.Length; i++)
            {
                numeros += valores[i];
            }

            double suma = 0;
            char[] numeros2 = numeros.ToCharArray();
            for (int i = 0; i < numeros2.Length; i++)
            {
                if (i <= (numeros2.Length - 2))
                    suma += double.Parse(numeros2[i].ToString() + "" + numeros2[i + 1].ToString()) * double.Parse(numeros2[i + 1].ToString());
            }

            suma = double.Parse(suma.ToString().Substring(suma.ToString().Length - 3, 3));
            int cociente = (int)Math.Truncate(suma / 34);
            int residuo = (int)(suma % 34);

            string clave = TablaHomonimio(cociente) + TablaHomonimio(residuo);
            return clave;
        }

        public string DigitoVerificador(string rfc12posiciones)
        {
            char[] valoresLetras = rfc12posiciones.ToCharArray();
            string[] valores = new string[rfc12posiciones.Length];
            for (int i = 0; i < valoresLetras.Length; i++)
            {
                switch (valoresLetras[i])
                {
                    case '0': valores[i] = "00"; break;
                    case '1': valores[i] = "01"; break;
                    case '2': valores[i] = "02"; break;
                    case '3': valores[i] = "03"; break;
                    case '4': valores[i] = "04"; break;
                    case '5': valores[i] = "05"; break;
                    case '6': valores[i] = "06"; break;
                    case '7': valores[i] = "07"; break;
                    case '8': valores[i] = "08"; break;
                    case '9': valores[i] = "09"; break;
                    case 'A': valores[i] = "10"; break;
                    case 'B': valores[i] = "11"; break;
                    case 'C': valores[i] = "12"; break;
                    case 'D': valores[i] = "13"; break;
                    case 'E': valores[i] = "14"; break;
                    case 'F': valores[i] = "15"; break;
                    case 'G': valores[i] = "16"; break;
                    case 'H': valores[i] = "17"; break;
                    case 'I': valores[i] = "18"; break;
                    case 'J': valores[i] = "19"; break;
                    case 'K': valores[i] = "20"; break;
                    case 'L': valores[i] = "21"; break;
                    case 'M': valores[i] = "22"; break;
                    case 'N': valores[i] = "23"; break;
                    case '&': valores[i] = "24"; break;
                    case 'O': valores[i] = "25"; break;
                    case 'P': valores[i] = "26"; break;
                    case 'Q': valores[i] = "27"; break;
                    case 'R': valores[i] = "28"; break;
                    case 'S': valores[i] = "29"; break;
                    case 'T': valores[i] = "30"; break;
                    case 'U': valores[i] = "31"; break;
                    case 'V': valores[i] = "32"; break;
                    case 'W': valores[i] = "33"; break;
                    case 'X': valores[i] = "34"; break;
                    case 'Y': valores[i] = "35"; break;
                    case 'Z': valores[i] = "36"; break;
                    case ' ': valores[i] = "37"; break;
                    case 'Ñ': valores[i] = "38"; break;
                    default: valores[i] = "00"; break;
                }
            }
            double suma = 0;
            double posicion = 12;
            for (int i = 0; i < valores.Length; i++)
            {
                suma += double.Parse(valores[i].ToString()) * (posicion + 1);
                posicion--;
            }

            int residuo = (int)(suma % 11);
                string dv = "";

            if (residuo == 0)
                dv = "0";

            if (residuo > 10)
                dv = "0";
            
            if (residuo < 10 && residuo > 0)
            {
                dv = (11 - residuo).ToString();
                if (dv == "10")
                    dv = "A";
            }
               
            if (residuo == 10)
                dv = "A";

            return dv;
        }

        private string TablaHomonimio(int valor)
        {
            string homo = "";
            switch (valor)
            {
                case 0: homo = "1"; break;
                case 1: homo = "2"; break;
                case 2: homo = "3"; break;
                case 3: homo = "4"; break;
                case 4: homo = "5"; break;
                case 5: homo = "6"; break;
                case 6: homo = "7"; break;
                case 7: homo = "8"; break;
                case 8: homo = "9"; break;
                case 9: homo = "A"; break;
                case 10: homo = "B"; break;
                case 11: homo = "C"; break;
                case 12: homo = "D"; break;
                case 13: homo = "E"; break;
                case 14: homo = "F"; break;
                case 15: homo = "G"; break;
                case 16: homo = "H"; break;
                case 17: homo = "I"; break;
                case 18: homo = "J"; break;
                case 19: homo = "K"; break;
                case 20: homo = "L"; break;
                case 21: homo = "M"; break;
                case 22: homo = "N"; break;
                case 23: homo = "P"; break;
                case 24: homo = "Q"; break;
                case 25: homo = "R"; break;
                case 26: homo = "S"; break;
                case 27: homo = "T"; break;
                case 28: homo = "U"; break;
                case 29: homo = "V"; break;
                case 30: homo = "W"; break;
                case 31: homo = "X"; break;
                case 32: homo = "Y"; break;
                case 33: homo = "Z"; break;
            }
            return homo;
        }

    }

    public class CURP
    {
        public string ObtieneRFC_CURP(string _paterno, string _materno, string _nombre)
        {
            string rfc = "";
            string paterno = PalabrasNoUtilizadas(_paterno.TrimStart().TrimEnd()).TrimEnd();
            string materno = PalabrasNoUtilizadas(_materno.TrimStart().TrimEnd()).TrimEnd();
            string nombre = PalabrasNoUtilizadas(_nombre.TrimStart().TrimEnd()).TrimEnd();
            paterno = dieresis(paterno);
            materno = dieresis(materno);
            nombre = PalabrasComunes(nombre);

            int cuentaPaterno = 0, cuentaMaterno = 0;
            cuentaPaterno = paterno.Length;
            cuentaMaterno = materno.Length;

            if (cuentaPaterno == 0)
            {
                materno = materno.Substring(0, 2);
                nombre = nombre.Substring(0, 1);
                rfc = materno + "X" + nombre;
            }
            if (cuentaMaterno == 0)
            {
                paterno = paterno.Substring(0, 2);
                nombre = nombre.Substring(0, 1);
                rfc = paterno + "X" + nombre;
            }

            if (cuentaPaterno == 1 || cuentaPaterno == 2)
            {
                paterno = paterno.Substring(0, 1);
                materno = materno.Substring(0, 1);
                nombre = nombre.Substring(0, 2);
                rfc = paterno + materno + nombre;
            }

            if (cuentaPaterno > 2 && cuentaMaterno > 2)
            {
                bool esVocal = false;
                string primerLetraPaterno = paterno.Substring(0, 1);
                string primerVocal = "";
                string primerLetraMaterno = materno.Substring(0, 1);
                string primerLetraNombre = nombre.Substring(0, 1);
                char[] letras = paterno.ToCharArray();
                for (int i = 1; i < letras.Length; i++)
                {
                    esVocal = EsVocal(letras[i]);
                    if (esVocal)
                    {
                        primerVocal = letras[i].ToString();
                        break;
                    }
                    else
                    {
                        primerVocal = "X";
                        break;
                    }
                }
                if (primerLetraPaterno == "Ñ")
                    primerLetraPaterno = "X";

                rfc = primerLetraPaterno + primerVocal + primerLetraMaterno + primerLetraNombre;
                rfc = PalabrasInconvenientes(rfc);
            }

            return rfc;
        }

        public string ConsonantesRFC(string _paterno, string _materno, string _nombre)
        {
            string _valor = "";
            string paterno = PalabrasNoUtilizadas(_paterno.TrimStart().TrimEnd()).TrimEnd();
            string materno = PalabrasNoUtilizadas(_materno.TrimStart().TrimEnd()).TrimEnd();
            string nombre = PalabrasNoUtilizadas(_nombre.TrimStart().TrimEnd()).TrimEnd();
            paterno = dieresis(paterno);
            materno = dieresis(materno);
            nombre = PalabrasComunes(nombre);

            int cuentaPaterno = 0, cuentaMaterno = 0;
            cuentaPaterno = paterno.Length;
            cuentaMaterno = materno.Length;

            if (cuentaPaterno == 0)
            {
                bool consonante = true;
                char[] cmaterno = materno.ToCharArray();
                for (int i = 1; i < cmaterno.Length; i++)
                {
                    consonante = EsVocal(cmaterno[i]);
                    if (!consonante)
                    {
                        if (cmaterno[i] == 'Ñ')
                            _valor += "X";
                        else
                            _valor += cmaterno[i].ToString();
                        break;
                    }
                }
                _valor += "X";
                consonante = true;
                char[] cnombre = nombre.ToCharArray();
                for (int i = 1; i < cnombre.Length; i++)
                {
                    consonante = EsVocal(cnombre[i]);
                    if (!consonante)
                    {
                        if (cnombre[i] == 'Ñ')
                            _valor += "X";
                        else
                            _valor += cnombre[i].ToString();
                        break;
                    }
                }
            }

            if (cuentaMaterno == 0)
            {
                bool consonante = true;
                char[] cpaterno = paterno.ToCharArray();
                for (int i = 1; i < cpaterno.Length; i++)
                {
                    consonante = EsVocal(cpaterno[i]);
                    if (!consonante)
                    {
                        if (cpaterno[i] == 'Ñ')
                            _valor += "X";
                        else
                            _valor += cpaterno[i].ToString();
                        break;
                    }
                }
                _valor += "X";
                consonante = true;
                char[] cnombre = nombre.ToCharArray();
                for (int i = 1; i < cnombre.Length; i++)
                {
                    consonante = EsVocal(cnombre[i]);
                    if (!consonante)
                    {
                        if (cnombre[i] == 'Ñ')
                            _valor += "X";
                        else
                            _valor += cnombre[i].ToString();
                        break;
                    }
                }
            }

            if (cuentaPaterno >= 2 && cuentaMaterno >= 2)
            {
                bool consonante = true;
                string consonantePaterno = "";
                string consonanteMaterno = "";
                string consonanteNombre = "";

                char[] cpaterno = paterno.ToCharArray();
                for (int i = 1; i < cpaterno.Length; i++)
                {
                    consonante = EsVocal(cpaterno[i]);
                    if (!consonante)
                    {
                        if (cpaterno[i] == 'Ñ')
                            consonantePaterno += "X";
                        else
                            consonantePaterno += cpaterno[i].ToString();
                        break;
                    }
                }

                if (consonantePaterno == "")
                    consonantePaterno = "X";

                consonante = true;
                char[] cmaterno = materno.ToCharArray();
                for (int i = 1; i < cmaterno.Length; i++)
                {
                    consonante = EsVocal(cmaterno[i]);
                    if (!consonante)
                    {
                        if (cmaterno[i] == 'Ñ')
                            consonanteMaterno += "X";
                        else
                            consonanteMaterno += cmaterno[i].ToString();
                        break;
                    }
                }

                if (consonanteMaterno == "")
                    consonanteMaterno = "X";

                consonante = true;
                char[] cnombre = nombre.ToCharArray();
                for (int i = 1; i < _nombre.Length; i++)
                {
                    consonante = EsVocal(cnombre[i]);
                    if (!consonante)
                    {
                        if (cnombre[i] == 'Ñ')
                            consonanteNombre += "X";
                        else
                            consonanteNombre += cnombre[i].ToString();
                        break;
                    }
                }

                if (consonanteNombre == "")
                    consonanteNombre = "X";

                _valor = consonantePaterno + consonanteMaterno + consonanteNombre;
            }
            return _valor;
        }

        public string dieresis(string valor)
        {
            char[] valores = valor.ToCharArray();
            for (int i = 0; i < valores.Length; i++)
            {
                switch (valores[i])
                {
                    case 'Ä': valores[i] = 'A'; break;
                    case 'Ë': valores[i] = 'E'; break;
                    case 'Ï': valores[i] = 'I'; break;
                    case 'Ö': valores[i] = 'O'; break;
                    case 'Ü': valores[i] = 'U'; break;
                }
            }
            string _valor = "";
            for (int i = 0; i < valores.Length; i++)
            {
                _valor += valores[i].ToString();
            }
            return _valor;
        }

        public string PalabrasNoUtilizadas(string valor)
        {
            string[] valores = valor.Split(' ');
            string _valor = "";
            for (int i = 0; i < valores.Length; i++)
            {
                switch (valores[i])
                {
                    case "DE": valores[i] = valores[i].Replace("DE", ""); break;
                    case "LA": valores[i] = valores[i].Replace("LA", ""); break;
                    case "LAS": valores[i] = valores[i].Replace("LAS", ""); break;
                    case "MC": valores[i] = valores[i].Replace("MC", ""); break;
                    case "VON": valores[i] = valores[i].Replace("VON", ""); break;
                    case "DEL": valores[i] = valores[i].Replace("DEL", ""); break;
                    case "LOS": valores[i] = valores[i].Replace("LOS", ""); break;
                    case "Y": valores[i] = valores[i].Replace("Y", ""); break;
                    case "MAC": valores[i] = valores[i].Replace("MAC", ""); break;
                    case "VAN": valores[i] = valores[i].Replace("VAN", ""); break;
                    case "MI": valores[i] = valores[i].Replace("MI", ""); break;
                }
            }

            for (int i = 0; i < valores.Length; i++)
            {
                if (valores[i] != "")
                    _valor += valores[i] + " ";
            }
            return _valor;
        }

        public string PalabrasComunes(string valor)
        {
            string[] valores = valor.Split(' ');
            string _valor = "";

            if (valores.Length == 1)
            {
                _valor = valor;
            }

            if (valores.Length == 2)
            {
                bool flag1 = false, flag2 = false;
                if (valores[0] == "MARIA" || valores[0] == "JOSE" || valores[0] == "MARÍA" || valores[0] == "JOSÉ")
                    flag1 = true;
                if (valores[1] == "MARIA" || valores[1] == "JOSE" || valores[1] == "MARÍA" || valores[1] == "JOSÉ")
                    flag2 = true;
                if (flag1 && flag2)
                    _valor = valores[1];
                else
                {
                    for (int i = 0; i < valores.Length; i++)
                    {
                        switch (valores[i])
                        {
                            case "MARIA": valores[i] = valores[i].Replace("MARIA", ""); break;
                            case "MARÍA": valores[i] = valores[i].Replace("MARÍA", ""); break;
                            case "JOSE": valores[i] = valores[i].Replace("JOSE", ""); ; break;
                            case "JOSÉ": valores[i] = valores[i].Replace("JOSÉ", ""); break;
                            case "J": valores[i] = valores[i].Replace("J", ""); break;
                            case "J.": valores[i] = valores[i].Replace("J.", ""); break;
                            case "MA": valores[i] = valores[i].Replace("J.", ""); break;
                            case "MA.": valores[i] = valores[i].Replace("J.", ""); break;
                        }
                    }
                    for (int i = 0; i < valores.Length; i++)
                    {
                        if (valores[i] != "")
                            _valor += valores[i] + " ";
                    }
                }
            }

            if (valores.Length > 2)
            {
                for (int i = 0; i < valores.Length; i++)
                {
                    switch (valores[i])
                    {
                        case "MARIA": valores[i] = valores[i].Replace("MARIA", ""); break;
                        case "MARÍA": valores[i] = valores[i].Replace("MARÍA", ""); break;
                        case "JOSE": valores[i] = valores[i].Replace("JOSE", ""); ; break;
                        case "JOSÉ": valores[i] = valores[i].Replace("JOSÉ", ""); break;
                        case "J": valores[i] = valores[i].Replace("J", ""); break;
                        case "J.": valores[i] = valores[i].Replace("J.", ""); break;
                        case "MA": valores[i] = valores[i].Replace("J.", ""); break;
                        case "MA.": valores[i] = valores[i].Replace("J.", ""); break;
                    }
                }
                for (int i = 0; i < valores.Length; i++)
                {
                    if (valores[i] != "")
                        _valor += valores[i] + " ";
                }
            }
            return _valor;
        }

        public bool EsVocal(char c)
        {
            bool vocal = false;
            switch (c)
            {
                case 'a': vocal = true; break;
                case 'e': vocal = true; break;
                case 'i': vocal = true; break;
                case 'o': vocal = true; break;
                case 'u': vocal = true; break;

                case 'A': vocal = true; break;
                case 'E': vocal = true; break;
                case 'I': vocal = true; break;
                case 'O': vocal = true; break;
                case 'U': vocal = true; break;

                case 'Á': vocal = true; break;
                case 'É': vocal = true; break;
                case 'Í': vocal = true; break;
                case 'Ó': vocal = true; break;
                case 'Ú': vocal = true; break;
                default:
                    vocal = false; break;
            }
            return vocal;
        }

        public string PalabrasInconvenientes(string rfc)
        {
            switch (rfc)
            {
                case "BACA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "BAKA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "BUEI": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "BUEY": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "CACA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "CACO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "CAGA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "CAGO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "CAKA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "CAKO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "COGE": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "COGI": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "COJA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "COJE": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "COJI": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "COJO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "COLA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "CULO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "FALO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "FETO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "GETA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "GUEI": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "GUEY": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "JETA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "JOTO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "KACA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "KACO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "KAGA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "KAGO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "KAKA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "KAKO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "KOGE": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "KOGI": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "KOJA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "KOJI": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "KOJO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "KULO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "LILO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "LOCA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "LOCO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "LOKA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "LOKO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "MAME": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "MAMO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "MEAR": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "MEAS": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "MEON": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "MIAR": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "MION": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "MOCO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "MOKO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "MULA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "MULO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "NACA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "NACO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "PEDA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "PEDO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "PENE": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "PIPI": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "POPO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "PUTA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "PUTO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "PITO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "QULO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "RATA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "RUIN": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "ROBA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "ROBE": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "ROBO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "SENO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "TETA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "VACA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "VAGA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "VAGO": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "VAKA": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "VUEI": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "VUEY": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "WUEI": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
                case "WUEY": rfc = rfc.Replace(rfc.Substring(1, 1), "X"); break;
            }
            return rfc;
        }

        public string DigitoVerificador(string curp17posiciones)
        {
            curp17posiciones = curp17posiciones + "0";
            char[] valoresLetras = curp17posiciones.ToCharArray();
            string[] valores = new string[curp17posiciones.Length];
            for (int i = 0; i < valoresLetras.Length; i++)
            {
                switch (valoresLetras[i])
                {
                    case '0': valores[i] = "00"; break;
                    case '1': valores[i] = "01"; break;
                    case '2': valores[i] = "02"; break;
                    case '3': valores[i] = "03"; break;
                    case '4': valores[i] = "04"; break;
                    case '5': valores[i] = "05"; break;
                    case '6': valores[i] = "06"; break;
                    case '7': valores[i] = "07"; break;
                    case '8': valores[i] = "08"; break;
                    case '9': valores[i] = "09"; break;
                    case 'A': valores[i] = "10"; break;
                    case 'B': valores[i] = "11"; break;
                    case 'C': valores[i] = "12"; break;
                    case 'D': valores[i] = "13"; break;
                    case 'E': valores[i] = "14"; break;
                    case 'F': valores[i] = "15"; break;
                    case 'G': valores[i] = "16"; break;
                    case 'H': valores[i] = "17"; break;
                    case 'I': valores[i] = "18"; break;
                    case 'J': valores[i] = "19"; break;
                    case 'K': valores[i] = "20"; break;
                    case 'L': valores[i] = "21"; break;
                    case 'M': valores[i] = "22"; break;
                    case 'N': valores[i] = "23"; break;
                    case 'Ñ': valores[i] = "24"; break;
                    case 'O': valores[i] = "25"; break;
                    case 'P': valores[i] = "26"; break;
                    case 'Q': valores[i] = "27"; break;
                    case 'R': valores[i] = "28"; break;
                    case 'S': valores[i] = "29"; break;
                    case 'T': valores[i] = "30"; break;
                    case 'U': valores[i] = "31"; break;
                    case 'V': valores[i] = "32"; break;
                    case 'W': valores[i] = "33"; break;
                    case 'X': valores[i] = "34"; break;
                    case 'Y': valores[i] = "35"; break;
                    case 'Z': valores[i] = "36"; break;
                    case ' ': valores[i] = "37"; break;
                }
            }
            double suma = 0;
            double posicion = 18;
            for (int i = 0; i < valores.Length; i++)
            {
                suma += double.Parse(valores[i].ToString()) * (posicion);
                posicion--;
            }

            int residuo = (int)(suma % 10);
            residuo = Math.Abs(residuo - 10);
            string dv = "";

            if (residuo == 10)
                dv = "0";
            else
                dv = residuo.ToString();

            return dv;
        }

        public string TablaEstados(string valor)
        {
            string estado = "";
            switch (valor)
            {
                case "AGUASCALIENTES": estado = "AS"; break;
                case "BAJA CALIFORNIA": estado = "BC"; break;
                case "BAJA CALIFORNIA SUR": estado = "BS"; break;
                case "CAMPECHE": estado = "CC"; break;
                case "COAHUILA": estado = "CL"; break;
                case "COLIMA": estado = "CM"; break;
                case "CHIAPAS": estado = "CS"; break;
                case "CHIHUAHA": estado = "CH"; break;
                case "DISTRITO FEDERAL": estado = "DF"; break;
                case "DURANGO": estado = "DG"; break;
                case "GUANAJUATO": estado = "GT"; break;
                case "GUERRERO": estado = "GR"; break;
                case "HIDALGO": estado = "HG"; break;
                case "JALISCO": estado = "JC"; break;
                case "MEXICO": estado = "MC"; break;
                case "MICHOACAN": estado = "MN"; break;
                case "MORELOS": estado = "MS"; break;
                case "NAYARIT": estado = "NT"; break;
                case "NUEVO LEON": estado = "NL"; break;
                case "OAXACA": estado = "OC"; break;
                case "PUEBLA": estado = "PL"; break;
                case "QUERETARO": estado = "QT"; break;
                case "QUINTANA ROO": estado = "QR"; break;
                case "SAN LUIS POTOSI": estado = "SP"; break;
                case "SINALOA": estado = "SL"; break;
                case "SONORA": estado = "SR"; break;
                case "TABASCO": estado = "TC"; break;
                case "TAMAULIPAS": estado = "TS"; break;
                case "TLAXCALA": estado = "TL"; break;
                case "VERACRUZ": estado = "VZ"; break;
                case "YUCATAN": estado = "YN"; break;
                case "ZACATECAS": estado = "ZS"; break;
            }
            return estado;
        }
    }
}
