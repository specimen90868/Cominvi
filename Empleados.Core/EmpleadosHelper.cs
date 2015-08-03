using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Empleados.Core
{
    public class EmpleadosHelper : Data.Obj.DataObj
    {
        public List<Empleados> obtenerEmpleados(Empleados e)
        {
            DataTable dtEmpleados = new DataTable();
            List<Empleados> lstEmpleados = new List<Empleados>();
            Command.CommandText = "select idtrabajador, nombrecompleto, fechaingreso, antiguedad, sdi, sd, sueldo from trabajadores where idempresa = @idempresa and idplaza = @idplaza";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            Command.Parameters.AddWithValue("idplaza", e.idplaza);
            
            dtEmpleados = SelectData(Command);

            for (int i = 0; i < dtEmpleados.Rows.Count; i++)
            {
                Empleados empleado = new Empleados();
                empleado.idtrabajador = int.Parse(dtEmpleados.Rows[i]["idtrabajador"].ToString());
                empleado.nombrecompleto = dtEmpleados.Rows[i]["nombrecompleto"].ToString();
                empleado.fechaingreso = DateTime.Parse(dtEmpleados.Rows[i]["fechaingreso"].ToString());
                empleado.antiguedad = int.Parse(dtEmpleados.Rows[i]["antiguedad"].ToString());
                empleado.sdi = decimal.Parse(dtEmpleados.Rows[i]["sdi"].ToString());
                empleado.sd = decimal.Parse(dtEmpleados.Rows[i]["sd"].ToString());
                empleado.sueldo = decimal.Parse(dtEmpleados.Rows[i]["sueldo"].ToString());
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
                empleado.nombres = dtEmpleados.Rows[i]["nombres"].ToString();
                empleado.paterno = dtEmpleados.Rows[i]["paterno"].ToString();
                empleado.materno = dtEmpleados.Rows[i]["materno"].ToString();
                empleado.nombrecompleto = dtEmpleados.Rows[i]["nombrecompleto"].ToString();
                empleado.fechanacimiento = DateTime.Parse(dtEmpleados.Rows[i]["fechanacimiento"].ToString());
                empleado.edad = int.Parse(dtEmpleados.Rows[i]["edad"].ToString());
                empleado.idempresa = int.Parse(dtEmpleados.Rows[i]["idempresa"].ToString());
                empleado.idcliente = int.Parse(dtEmpleados.Rows[i]["idcliente"].ToString());
                empleado.fechaingreso = DateTime.Parse(dtEmpleados.Rows[i]["fechaingreso"].ToString());
                empleado.antiguedad = int.Parse(dtEmpleados.Rows[i]["antiguedad"].ToString());
                empleado.idperiodo = int.Parse(dtEmpleados.Rows[i]["idperiodo"].ToString());
                empleado.localforaneo = dtEmpleados.Rows[i]["localforaneo"].ToString();
                empleado.sua = dtEmpleados.Rows[i]["sua"].ToString();
                empleado.rfc = dtEmpleados.Rows[i]["rfc"].ToString();
                empleado.curp = dtEmpleados.Rows[i]["curp"].ToString();
                empleado.nss = dtEmpleados.Rows[i]["nss"].ToString();
                empleado.digitoverificador = int.Parse(dtEmpleados.Rows[i]["digitoverificador"].ToString());
                empleado.tiposalario = dtEmpleados.Rows[i]["antiguedad"].ToString();
                empleado.sdi = decimal.Parse(dtEmpleados.Rows[i]["sdi"].ToString());
                empleado.sd = decimal.Parse(dtEmpleados.Rows[i]["sd"].ToString());
                empleado.sueldo = decimal.Parse(dtEmpleados.Rows[i]["sueldo"].ToString());
                lstEmpleados.Add(empleado);
            }

            return lstEmpleados;
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

        public int insertaEmpleado(Empleados e)
        {
            Command.CommandText = "insert into trabajadores (nombres,paterno,materno,nombrecompleto,fechanacimiento,edad,idempresa,idcliente,fechaingreso,antiguedad,idperiodo,localforaneo,sua,rfc,curp,nss,digitoverificador,tiposalario,sdi,sd,sueldo,estatus,idse,modsalario,idplaza)" +
                "values (@nombres,@paterno,@materno,@nombrecompleto,@fechanacimiento,@edad,@idempresa,@idcliente,@fechaingreso,@antiguedad,@idperiodo,@localforaneo,@sua,@rfc,@curp,@nss,@digitoverificador,@tiposalario,@sdi,@sd,@sueldo,@estatus,@idse,@modsalario,@idplaza)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("nombres",e.nombres);
            Command.Parameters.AddWithValue("paterno", e.paterno);
            Command.Parameters.AddWithValue("materno", e.materno);
            Command.Parameters.AddWithValue("nombrecompleto", e.nombrecompleto);
            Command.Parameters.AddWithValue("fechanacimiento", e.fechanacimiento);
            Command.Parameters.AddWithValue("edad", e.edad);
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            Command.Parameters.AddWithValue("idcliente", e.idcliente);
            Command.Parameters.AddWithValue("fechaingreso", e.fechaingreso);
            Command.Parameters.AddWithValue("antiguedad", e.antiguedad);
            Command.Parameters.AddWithValue("idperiodo", e.idperiodo);
            Command.Parameters.AddWithValue("localforaneo", e.localforaneo);
            Command.Parameters.AddWithValue("sua", e.sua);
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
            Command.Parameters.AddWithValue("modsalario", e.modsalario);
            Command.Parameters.AddWithValue("idplaza", e.idplaza);

            return Command.ExecuteNonQuery();
        }

        public int actualizaEmpleado(Empleados e)
        {
            Command.CommandText = "update trabajadores set nombres = @nombres, paterno = @paterno, materno = @materno, nombrecompleto = @nombrecompleto, fechanacimiento = @fechanacimiento, edad= @edad, idcliente = @idcliente, fechaingreso = @fechaingreso, antiguedad = @antiguedad, idperiodo = @idperiodo, " +
                "localforaneo = @localforaneo, sua = @sua, rfc = @rfc, curp = @curp, nss = @nss, digitoverificador = @digitoverificador, tiposalario = @tiposalario, sdi = @sdi, sd = @sd, sueldo = @sueldo where idtrabajador = @idtrabajador";
                
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", e.nombres);
            Command.Parameters.AddWithValue("nombres", e.nombres);
            Command.Parameters.AddWithValue("paterno", e.paterno);
            Command.Parameters.AddWithValue("materno", e.materno);
            Command.Parameters.AddWithValue("nombrecompleto", e.nombrecompleto);
            Command.Parameters.AddWithValue("fechanacimiento", e.fechanacimiento);
            Command.Parameters.AddWithValue("edad", e.edad);
            Command.Parameters.AddWithValue("idcliente", e.idcliente);
            Command.Parameters.AddWithValue("fechaingreso", e.fechaingreso);
            Command.Parameters.AddWithValue("antiguedad", e.antiguedad);
            Command.Parameters.AddWithValue("idperiodo", e.idperiodo);
            Command.Parameters.AddWithValue("localforaneo", e.localforaneo);
            Command.Parameters.AddWithValue("sua", e.sua);
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

        public int bajaEmpleado(Empleados e)
        {
            Command.CommandText = "delete from trabajadores where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador",e.idtrabajador);
            return Command.ExecuteNonQuery();
        }

        public int actualizaClientePeriodo(Empleados e)
        {
            Command.CommandText = "update trabajadores set idcliente = @idcliente, idperiodo = @idperiodo where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@idcliente", e.idcliente);
            Command.Parameters.AddWithValue("@idperiodo", e.idperiodo);
            Command.Parameters.AddWithValue("@idtrabajador", e.idtrabajador);
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
