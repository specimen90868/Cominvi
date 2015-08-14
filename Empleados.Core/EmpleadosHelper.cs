using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Empleados.Core
{
    public class EmpleadosHelper : Data.Obj.DataObj
    {
        public List<Empleados> obtenerEmpleados(Empleados e)
        {
            DataTable dtEmpleados = new DataTable();
            List<Empleados> lstEmpleados = new List<Empleados>();
            Command.CommandText = "select idtrabajador, nombrecompleto, fechaingreso, antiguedad, sdi, sd, sueldo from trabajadores where idempresa = @idempresa and estatus = @estatus";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            Command.Parameters.AddWithValue("estatus", e.estatus);
            dtEmpleados = SelectData(Command);

            for (int i = 0; i < dtEmpleados.Rows.Count; i++)
            {
                Empleados empleado = new Empleados();
                empleado.idtrabajador = int.Parse(dtEmpleados.Rows[i]["idtrabajador"].ToString());
                empleado.nombrecompleto = dtEmpleados.Rows[i]["nombrecompleto"].ToString();
                empleado.fechaingreso = DateTime.Parse(dtEmpleados.Rows[i]["fechaingreso"].ToString());
                empleado.antiguedad = int.Parse(dtEmpleados.Rows[i]["antiguedad"].ToString());
                empleado.sdi = double.Parse(dtEmpleados.Rows[i]["sdi"].ToString());
                empleado.sd = double.Parse(dtEmpleados.Rows[i]["sd"].ToString());
                empleado.sueldo = double.Parse(dtEmpleados.Rows[i]["sueldo"].ToString());
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
                incremento.id = int.Parse(dtIncremento.Rows[i]["id"].ToString());
                incremento.idtrabajador = int.Parse(dtIncremento.Rows[i]["idtrabajador"].ToString());
                incremento.nombre = dtIncremento.Rows[i]["nombre"].ToString();
                incremento.sdivigente = double.Parse(dtIncremento.Rows[i]["sdivigente"].ToString());
                incremento.sdinuevo = double.Parse(dtIncremento.Rows[i]["sdinuevo"].ToString());
                lstEmpleadosIncremento.Add(incremento);
            }

            return lstEmpleadosIncremento;
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

        public object obtenerSalarioDiarioIntegrado(Empleados e)
        {
            Command.CommandText = "select sdi from trabajadores where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador",e.idtrabajador);
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
            Command.CommandText = "insert into trabajadores (noempleado,nombres,paterno,materno,nombrecompleto,idempresa,idperiodo,iddepartamento,idpuesto,fechaingreso,antiguedad," + 
                "fechaantiguedad,antiguedadmod,fechanacimiento,edad,rfc,curp,nss,digitoverificador,tiposalario,sdi,sd,sueldo,estatus,idse,idusuario) " +
                "values (@noempleado,@nombres,@paterno,@materno,@nombrecompleto,@idempresa,@idperiodo,@iddepartamento,@idpuesto,@fechaingreso,@antiguedad,@fechaantiguedad,@antiguedadmod," + 
                "@fechanacimiento,@edad,@rfc,@curp,@nss,@digitoverificador,@tiposalario,@sdi,@sd,@sueldo,@estatus,@idse,@idusuario)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("noempleado", e.noempleado);
            Command.Parameters.AddWithValue("nombres",e.nombres);
            Command.Parameters.AddWithValue("paterno", e.paterno);
            Command.Parameters.AddWithValue("materno", e.materno);
            Command.Parameters.AddWithValue("nombrecompleto", e.nombrecompleto);
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            Command.Parameters.AddWithValue("idperiodo", e.idperiodo);
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
            Command.Parameters.AddWithValue("idse", e.idse);
            Command.Parameters.AddWithValue("idusuario", e.idusuario);

            return Command.ExecuteNonQuery();
        }

        public int actualizaEmpleado(Empleados e)
        {
            Command.CommandText = "update trabajadores set noempleado = @noempleado, nombres = @nombres, paterno = @paterno, materno = @materno, nombrecompleto = @nombrecompleto," +
                "idperiodo = @idperiodo, iddepartamento = @iddepartamento, idpuesto = @idpuesto, fechaingreso = @fechaingreso, antiguedad = @antiguedad, fechaantiguedad = @fechaantiguedad," + 
                "antiguedadmod = @antiguedadmod, fechanacimiento = @fechanacimiento, edad= @edad, rfc = @rfc, curp = @curp, nss = @nss, digitoverificador = @digitoverificador, " + 
                "tiposalario = @tiposalario, sdi = @sdi, sd = @sd, sueldo = @sueldo where idtrabajador = @idtrabajador";
                
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.idtrabajador);
            Command.Parameters.AddWithValue("noempleado", e.noempleado);
            Command.Parameters.AddWithValue("nombres", e.nombres);
            Command.Parameters.AddWithValue("paterno", e.paterno);
            Command.Parameters.AddWithValue("materno", e.materno);
            Command.Parameters.AddWithValue("nombrecompleto", e.nombrecompleto);
            Command.Parameters.AddWithValue("idperiodo", e.idperiodo);
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
                "iddepartamento = @iddepartamento, idpuesto = @idpuesto, idperiodo = @idperiodo, sueldo = @sueldo, sd = @sd, sdi = @sdi, estatus = @estatus, idusuario = @idusuario where idtrabajador = @idtrabajador";
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
    }
}
