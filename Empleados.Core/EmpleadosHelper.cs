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
                "fechaantiguedad,antiguedadmod,fechanacimiento,edad,rfc,curp,nss,digitoverificador,tiposalario,sdi,sd,sueldo,estatus,idusuario,cuenta,clabe,idbancario) " +
                "values (@noempleado,@nombres,@paterno,@materno,@nombrecompleto,@idempresa,@idperiodo,@idsalario,@iddepartamento,@idpuesto,@fechaingreso,@antiguedad,@fechaantiguedad,@antiguedadmod," +
                "@fechanacimiento,@edad,@rfc,@curp,@nss,@digitoverificador,@tiposalario,@sdi,@sd,@sueldo,@estatus,@idusuario,@cuenta,@clabe,@idbancario)";
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
            return Command.ExecuteNonQuery();
        }

        public int actualizaEmpleado(Empleados e)
        {
            Command.CommandText = "update trabajadores set noempleado = @noempleado, nombres = @nombres, paterno = @paterno, materno = @materno, nombrecompleto = @nombrecompleto," +
                "idperiodo = @idperiodo, idsalario = @idsalario, iddepartamento = @iddepartamento, idpuesto = @idpuesto, fechaingreso = @fechaingreso, antiguedad = @antiguedad, fechaantiguedad = @fechaantiguedad," + 
                "antiguedadmod = @antiguedadmod, fechanacimiento = @fechanacimiento, edad= @edad, rfc = @rfc, curp = @curp, nss = @nss, digitoverificador = @digitoverificador, " + 
                "tiposalario = @tiposalario, sdi = @sdi, sd = @sd, sueldo = @sueldo, cuenta = @cuenta, clabe = @clabe, idbancario = @idbancario where idtrabajador = @idtrabajador";
                
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
                "iddepartamento = @iddepartamento, idpuesto = @idpuesto, idperiodo = @idperiodo, sueldo = @sueldo, sd = @sd, sdi = @sdi, estatus = @estatus, idusuario = @idusuario, cuenta = @cuenta, clabe = @clabe, idbancario = @idbancario where idtrabajador = @idtrabajador";
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

    public class RFC
    {

        private ArrayList ExtraeApellidos(string nombre)
        {
            string[] strArray = new string[13] { "Y", "DEL", "EL", "LA", "LOS", "DE", "PARA", "DE", "LA", "MC", "VON", "MAC", "VAN" };
            char[] chArray = nombre.ToCharArray();
            int index = 0;
            ArrayList apellido = new ArrayList();
            string str = "";
            for (; index != nombre.Length; ++index)
            {
                if (chArray[index].ToString().Equals(" "))
                {
                    apellido.Add((object)str);
                    str = "";
                }
                else
                    str += chArray[index].ToString();
            }
            for (int i = 0; i != apellido.Count; ++i)
            {
                if (Enumerable.Count<string>(Enumerable.Where<string>((IEnumerable<string>)strArray, (Func<string, bool>)(u => u == apellido[i].ToString()))) > 0)
                {
                    apellido.RemoveRange(i, 1);
                    --i;
                }
            }
            return apellido;
        }

        private string ExtraeLetrasApellidos(ArrayList apellidos)
        {
            char[] cadena = apellidos[0].ToString().ToCharArray();
            int cont = 0;
            bool flag = false;
            string[] strArray = new string[5] { "A", "E", "I", "O", "U" };
            string str;
            if (cadena.Length > 1)
            {
                if ((cadena[0].ToString() + cadena[1].ToString()).Equals("CH") || (cadena[0].ToString() + cadena[1].ToString()).Equals("LL"))
                {
                    str = cadena[0].ToString();
                    cont = 2;
                }
                else
                {
                    str = cadena[0].ToString();
                    ++cont;
                }
                for (; !flag && cont <= cadena.Length - 1; ++cont)
                {
                    if (Enumerable.Count<string>(Enumerable.Where<string>((IEnumerable<string>)strArray, (Func<string, bool>)(u => u == cadena[cont].ToString()))) > 0)
                    {
                        str += cadena[cont].ToString();
                        flag = true;
                    }
                }
            }
            else
                str = cadena.Length <= 0 ? "" : cadena[0].ToString();
            return str;
        }

        private string ExtraeNombre(string nombr, string rfc)
        {
            ArrayList arrayList = this.ExtraeApellidos(nombr);
            if (arrayList.Count == 3)
            {
                if (arrayList[0].Equals((object)"MARIA") || arrayList[0].Equals((object)"JOSE"))
                    arrayList.RemoveAt(0);
            }
            
            char[] chArray = arrayList[0].ToString().ToCharArray();
            return !(chArray[0].ToString() + chArray[1].ToString()).Equals("CH") && !(chArray[0].ToString() + chArray[1].ToString()).Equals("LL") ? 
                (rfc.Length <= 2 ? chArray[0].ToString() + chArray[1].ToString() : 
                chArray[0].ToString()) : 
                (rfc.Length <= 2 ? chArray[0].ToString() + chArray[2].ToString() : 
                chArray[0].ToString());
        }

        private string QuitaPalabrasMalas(string rfc)
        {
            if (Enumerable.Count<string>(Enumerable.Where<string>((IEnumerable<string>)new string[45]
              {
                "BUEI",
                "BUEY",
                "CACA",
                "CACO",
                "CAGA",
                "CAGO",
                "CAKA",
                "CAKO",
                "COGE",
                "COJA",
                "COJE",
                "COJI",
                "COJO",
                "CULO",
                "FETO",
                "GUEY",
                "JOTO",
                "KACA",
                "KACO",
                "KAGA",
                "KAGO",
                "KOGE",
                "KOJO",
                "KAKA",
                "KULO",
                "LOCA",
                "LOCO",
                "LOKA",
                "LOKO",
                "MAME",
                "MAMO",
                "MEAR",
                "MEAS",
                "MEON",
                "MION",
                "MOCO",
                "MULA",
                "PEDA",
                "PEDO",
                "PENE",
                "PUTA",
                "PUTO",
                "QULO",
                "RATA",
                "RUIN"}, (Func<string, bool>)(u => u == rfc))) > 0)
                return rfc.Substring(0, rfc.Length - 1) + "X";
            return rfc;
        }

        private string RFC10DIGITOS(string appat, string apmat, string nombre, string fecha)
        {
            fecha = fecha.Replace("/", "");
            string str = this.ExtraeLetrasApellidos(this.ExtraeApellidos(appat + " "));
            string rfc = !this.ExtraeLetrasApellidos(this.ExtraeApellidos(apmat + " ")).Equals("") ? str + this.ExtraeLetrasApellidos(this.ExtraeApellidos(apmat + " ")).Substring(0, 1) : str + this.ExtraeLetrasApellidos(this.ExtraeApellidos(apmat + " "));
            return this.QuitaPalabrasMalas(rfc + this.ExtraeNombre(nombre + " ", rfc)) + "-" + fecha.Trim();
        }

        public string ObtieneHomonimia(string appat, string apmat, string nombre)
        {
            string[,] strArray1 = new string[38, 2]
              {
                {
                  " ",
                  "00"
                },
                {
                  "0",
                  "00"
                },
                {
                  "1",
                  "01"
                },
                {
                  "2",
                  "02"
                },
                {
                  "3",
                  "03"
                },
                {
                  "4",
                  "04"
                },
                {
                  "5",
                  "05"
                },
                {
                  "6",
                  "06"
                },
                {
                  "7",
                  "07"
                },
                {
                  "8",
                  "08"
                },
                {
                  "9",
                  "09"
                },
                {
                  "&",
                  "10"
                },
                {
                  "A",
                  "11"
                },
                {
                  "B",
                  "12"
                },
                {
                  "C",
                  "13"
                },
                {
                  "D",
                  "14"
                },
                {
                  "E",
                  "15"
                },
                {
                  "F",
                  "16"
                },
                {
                  "G",
                  "17"
                },
                {
                  "H",
                  "18"
                },
                {
                  "I",
                  "19"
                },
                {
                  "J",
                  "21"
                },
                {
                  "K",
                  "22"
                },
                {
                  "L",
                  "23"
                },
                {
                  "M",
                  "24"
                },
                {
                  "N",
                  "25"
                },
                {
                  "O",
                  "26"
                },
                {
                  "P",
                  "27"
                },
                {
                  "Q",
                  "28"
                },
                {
                  "R",
                  "29"
                },
                {
                  "S",
                  "32"
                },
                {
                  "T",
                  "33"
                },
                {
                  "U",
                  "34"
                },
                {
                  "V",
                  "35"
                },
                {
                  "W",
                  "36"
                },
                {
                  "X",
                  "37"
                },
                {
                  "Y",
                  "38"
                },
                {
                  "Z",
                  "39"
                }
              };
            string[,] strArray2 = new string[35, 2]
              {
                {
                  "0",
                  "1"
                },
                {
                  "1",
                  "2"
                },
                {
                  "2",
                  "3"
                },
                {
                  "3",
                  "4"
                },
                {
                  "4",
                  "5"
                },
                {
                  "5",
                  "6"
                },
                {
                  "6",
                  "7"
                },
                {
                  "7",
                  "8"
                },
                {
                  "8",
                  "9"
                },
                {
                  "9",
                  "A"
                },
                {
                  "10",
                  "B"
                },
                {
                  "11",
                  "C"
                },
                {
                  "12",
                  "D"
                },
                {
                  "13",
                  "E"
                },
                {
                  "14",
                  "F"
                },
                {
                  "15",
                  "G"
                },
                {
                  "16",
                  "H"
                },
                {
                  "17",
                  "I"
                },
                {
                  "18",
                  "J"
                },
                {
                  "19",
                  "K"
                },
                {
                  "20",
                  "L"
                },
                {
                  "21",
                  "M"
                },
                {
                  "22",
                  "N"
                },
                {
                  "0",
                  "O"
                },
                {
                  "23",
                  "P"
                },
                {
                  "24",
                  "Q"
                },
                {
                  "25",
                  "R"
                },
                {
                  "26",
                  "S"
                },
                {
                  "27",
                  "T"
                },
                {
                  "28",
                  "U"
                },
                {
                  "29",
                  "V"
                },
                {
                  "30",
                  "W"
                },
                {
                  "31",
                  "X"
                },
                {
                  "32",
                  "Y"
                },
                {
                  "33",
                  "Z"
                }
              };
            string str1;
            if (apmat.Equals(""))
                str1 = appat + " " + nombre;
            else
                str1 = appat + " " + apmat + " " + nombre;
            string str2 = str1.Replace("Ñ", "&").Trim();
            string str3 = (string)null;
            char[] chArray = str2.ToCharArray();
            int index1 = 0;
            double num1 = 0.0;
            string str4 = "0";
            for (; index1 != chArray.Length; ++index1)
            {
                int index2 = 0;
                bool flag = false;
                while (!flag)
                {
                    if (strArray1[index2, 0].Equals(chArray[index1].ToString()))
                    {
                        str4 += strArray1[index2, 1];
                        flag = true;
                    }
                    ++index2;
                }
            }
            for (int startIndex = 0; startIndex < str4.Length - 1; ++startIndex)
            {
                string str5 = str4.Substring(startIndex, 2) + "X" + str4.Substring(startIndex + 1, 1);
                num1 += double.Parse(str4.Substring(startIndex, 2)) * double.Parse(str4.Substring(startIndex + 1, 1));
            }
            if (num1.ToString().Length > 3)
                num1 = (double)int.Parse(num1.ToString().Substring(1, 3));
            int result = 0;
            Math.DivRem((int)num1, 34, out result);
            int num2 = (int)num1 / 34;
            bool flag1 = false;
            int index3 = 0;
            while (!flag1)
            {
                if (strArray2[index3, 0].Equals(num2.ToString()))
                {
                    str3 += strArray2[index3, 1];
                    flag1 = true;
                }
                ++index3;
            }
            bool flag2 = false;
            int index4 = 0;
            while (!flag2)
            {
                if (strArray2[index4, 0].Equals(result.ToString()))
                {
                    str3 += strArray2[index4, 1];
                    flag2 = true;
                }
                ++index4;
            }
            return str3;
        }

        public string ObtieneDigitoVerificador(string rfc12pocisiones)
        {
            rfc12pocisiones = rfc12pocisiones.Replace("-", "");
            if (rfc12pocisiones.Length < 12)
                rfc12pocisiones = " " + rfc12pocisiones;
            string[,] strArray = new string[38, 2]
              {
                {
                  "0",
                  "00"
                },
                {
                  "1",
                  "01"
                },
                {
                  "2",
                  "02"
                },
                {
                  "3",
                  "03"
                },
                {
                  "4",
                  "04"
                },
                {
                  "5",
                  "05"
                },
                {
                  "6",
                  "06"
                },
                {
                  "7",
                  "07"
                },
                {
                  "8",
                  "08"
                },
                {
                  "9",
                  "09"
                },
                {
                  "A",
                  "10"
                },
                {
                  "B",
                  "11"
                },
                {
                  "C",
                  "12"
                },
                {
                  "D",
                  "13"
                },
                {
                  "E",
                  "14"
                },
                {
                  "F",
                  "15"
                },
                {
                  "G",
                  "16"
                },
                {
                  "H",
                  "17"
                },
                {
                  "I",
                  "18"
                },
                {
                  "J",
                  "19"
                },
                {
                  "K",
                  "20"
                },
                {
                  "L",
                  "21"
                },
                {
                  "M",
                  "22"
                },
                {
                  "N",
                  "23"
                },
                {
                  "Ñ",
                  "24"
                },
                {
                  "O",
                  "25"
                },
                {
                  "P",
                  "26"
                },
                {
                  "Q",
                  "27"
                },
                {
                  "R",
                  "28"
                },
                {
                  "S",
                  "29"
                },
                {
                  "T",
                  "30"
                },
                {
                  "U",
                  "31"
                },
                {
                  "V",
                  "32"
                },
                {
                  "W",
                  "33"
                },
                {
                  "X",
                  "34"
                },
                {
                  "Y",
                  "35"
                },
                {
                  "Z",
                  "36"
                },
                {
                  " ",
                  "37"
                }
              };
            string str1 = (string)null;
            int index1 = 0;
            int result = 0;
            for (; index1 != rfc12pocisiones.ToCharArray().Length; ++index1)
            {
                int index2 = 0;
                bool flag = false;
                while (!flag)
                {
                    if (strArray[index2, 0].Equals(rfc12pocisiones.ToCharArray()[index1].ToString()))
                    {
                        str1 += strArray[index2, 1];
                        flag = true;
                    }
                    ++index2;
                }
            }
            int startIndex = 0;
            int num = 0;
            while (startIndex < str1.Length - 1)
            {
                result += int.Parse(str1.Substring(startIndex, 2)) * (13 - num);
                startIndex += 2;
                ++num;
            }
            Math.DivRem(result, 11, out result);
            string str2 = "";
            if (result > 0)
            {
                result = 11 - result;
                str2 = result.ToString();
            }
            switch (result)
            {
                case 0:
                    str2 = "0";
                    break;
                case 10:
                    str2 = "A";
                    break;
            }
            return str2;
        }

        public string RFC13Pocisiones(string Appat, string Apmat, string Nombre, string FechaNac)
        {
            string str1 = this.RFC10DIGITOS(Appat + " ", Apmat + " ", Nombre + " ", FechaNac);
            string str2 = this.ObtieneHomonimia(Appat, Apmat, Nombre);
            return str1 + str2 + this.ObtieneDigitoVerificador(str1 + str2);
        }

    }

    public class CURP
    {
        private string SegConsonantes = (string)null;

        private ArrayList ExtraeApellidos(string nombre)
        {
            string[] strArray = new string[13]
      {
        "Y",
        "DEL",
        "EL",
        "LA",
        "LOS",
        "DE",
        "PARA",
        "DE",
        "LA",
        "MC",
        "VON",
        "MAC",
        "VAN"
      };
            char[] chArray = nombre.ToCharArray();
            int index = 0;
            ArrayList apellido = new ArrayList();
            string str = "";
            for (; index != nombre.Length; ++index)
            {
                if (chArray[index].ToString().Equals(" "))
                {
                    apellido.Add((object)str);
                    str = "";
                }
                else
                    str += chArray[index].ToString();
            }
            for (int i = 0; i != apellido.Count; ++i)
            {
                if (Enumerable.Count<string>(Enumerable.Where<string>((IEnumerable<string>)strArray, (Func<string, bool>)(u => u == apellido[i].ToString()))) > 0)
                {
                    apellido.RemoveRange(i, 1);
                    --i;
                }
            }
            return apellido;
        }

        private string ExtraeLetrasApellidos(ArrayList apellidos)
        {
            char[] cadena = apellidos[0].ToString().ToCharArray();
            bool flag1 = false;
            int cont = 0;
            bool flag2 = false;
            string[] strArray = new string[5]
              {
                "A",
                "E",
                "I",
                "O",
                "U"
              };
            string str;
            if (cadena.Length > 1)
            {
                if ((cadena[0].ToString() + cadena[1].ToString()).Equals("CH") || (cadena[0].ToString() + cadena[1].ToString()).Equals("LL"))
                {
                    str = cadena[0].ToString();
                    cont = 2;
                }
                else
                {
                    str = cadena[0].ToString();
                    ++cont;
                }
                for (; !flag2 && cont <= (cadena.Length - 1) || !flag1; ++cont)
                {
                    if (Enumerable.Count<string>(Enumerable.Where<string>((IEnumerable<string>)strArray, (Func<string, bool>)(u => u == cadena[cont].ToString()))) > 0)
                    {
                        str += cadena[cont].ToString();
                        flag2 = true;
                    }
                    else if (!flag1)
                    {
                        this.SegConsonantes += cadena[cont].ToString();
                        flag1 = true;
                    }
                }
            }
            else
                str = cadena.Length <= 0 ? "" : cadena[0].ToString();
            return str;
        }

        private string ExtraeNombre(string nombr, string DigInicialesApellidos)
        {
            ArrayList arrayList = this.ExtraeApellidos(nombr);
            char[] chArray = new char[5]
              {
                'A',
                'E',
                'I',
                'O',
                'U'
              };
            if (arrayList.Count == 3)
                if (arrayList[0].Equals((object)"MARIA") || arrayList[0].Equals((object)"JOSE"))
                    arrayList.RemoveAt(0);
            char[] nombrearray = arrayList[0].ToString().ToCharArray();
            string str = !(nombrearray[0].ToString() + nombrearray[1].ToString()).Equals("CH") && !(nombrearray[0].ToString() + nombrearray[1].ToString()).Equals("LL") ? (DigInicialesApellidos.Length <= 2 ? nombrearray[0].ToString() + nombrearray[1].ToString() : nombrearray[0].ToString()) : (DigInicialesApellidos.Length <= 2 ? nombrearray[0].ToString() + nombrearray[2].ToString() : nombrearray[0].ToString());
            bool flag = false;
            int x = 1;
            while (!flag)
            {
                if (Enumerable.Count<char>(Enumerable.Where<char>((IEnumerable<char>)chArray, (Func<char, bool>)(u => (int)u == (int)nombrearray[x]))) == 0)
                {
                    this.SegConsonantes += nombrearray[x].ToString();
                    flag = true;
                }
                ++x;
            }
            return str;
        }

        private string QuitaPalabrasMalas(string rfc)
        {
            if (Enumerable.Count<string>(Enumerable.Where<string>((IEnumerable<string>)new string[45]
          {
            "BUEI",
            "BUEY",
            "CACA",
            "CACO",
            "CAGA",
            "CAGO",
            "CAKA",
            "CAKO",
            "COGE",
            "COJA",
            "COJE",
            "COJI",
            "COJO",
            "CULO",
            "FETO",
            "GUEY",
            "JOTO",
            "KACA",
            "KACO",
            "KAGA",
            "KAGO",
            "KOGE",
            "KOJO",
            "KAKA",
            "KULO",
            "LOCA",
            "LOCO",
            "LOKA",
            "LOKO",
            "MAME",
            "MAMO",
            "MEAR",
            "MEAS",
            "MEON",
            "MION",
            "MOCO",
            "MULA",
            "PEDA",
            "PEDO",
            "PENE",
            "PUTA",
            "PUTO",
            "QULO",
            "RATA",
            "RUIN"
          }, (Func<string, bool>)(u => u == rfc))) > 0)
                    return rfc.Substring(0, rfc.Length - 1) + "X";
            return rfc;
        }

        private string CURP10DIGITOS(string appat, string apmat, string nombre, string fecha)
        {
            fecha = fecha.Replace("/", "");
            string DigInicialesApellidos = this.ExtraeLetrasApellidos(this.ExtraeApellidos(appat + " "));
            if (!string.IsNullOrEmpty(apmat.Trim()))
                DigInicialesApellidos += this.ExtraeLetrasApellidos(this.ExtraeApellidos(apmat + " ")).Substring(0, 1);
            return this.QuitaPalabrasMalas(DigInicialesApellidos + this.ExtraeNombre(nombre + " ", DigInicialesApellidos)) + fecha.Trim();
        }

        private string ObtieneDigitoVerificador(string CURP17Pos)
        {
            CURP17Pos = CURP17Pos.Replace("-", "");
            if (CURP17Pos.Length < 12)
                CURP17Pos = " " + CURP17Pos;
            string[,] strArray = new string[38, 2]
              {
                {
                  "0",
                  "00"
                },
                {
                  "1",
                  "01"
                },
                {
                  "2",
                  "02"
                },
                {
                  "3",
                  "03"
                },
                {
                  "4",
                  "04"
                },
                {
                  "5",
                  "05"
                },
                {
                  "6",
                  "06"
                },
                {
                  "7",
                  "07"
                },
                {
                  "8",
                  "08"
                },
                {
                  "9",
                  "09"
                },
                {
                  "A",
                  "10"
                },
                {
                  "B",
                  "11"
                },
                {
                  "C",
                  "12"
                },
                {
                  "D",
                  "13"
                },
                {
                  "E",
                  "14"
                },
                {
                  "F",
                  "15"
                },
                {
                  "G",
                  "16"
                },
                {
                  "H",
                  "17"
                },
                {
                  "I",
                  "18"
                },
                {
                  "J",
                  "19"
                },
                {
                  "K",
                  "20"
                },
                {
                  "L",
                  "21"
                },
                {
                  "M",
                  "22"
                },
                {
                  "N",
                  "23"
                },
                {
                  "Ñ",
                  "24"
                },
                {
                  "O",
                  "25"
                },
                {
                  "P",
                  "26"
                },
                {
                  "Q",
                  "27"
                },
                {
                  "R",
                  "28"
                },
                {
                  "S",
                  "29"
                },
                {
                  "T",
                  "30"
                },
                {
                  "U",
                  "31"
                },
                {
                  "V",
                  "32"
                },
                {
                  "W",
                  "33"
                },
                {
                  "X",
                  "34"
                },
                {
                  "Y",
                  "35"
                },
                {
                  "Z",
                  "36"
                },
                {
                  " ",
                  "37"
                }
              };
            string str1 = (string)null;
            int index1 = 0;
            int result = 0;
            for (; index1 != CURP17Pos.ToCharArray().Length; ++index1)
            {
                int index2 = 0;
                bool flag = false;
                while (!flag)
                {
                    if (strArray[index2, 0].Equals(CURP17Pos.ToCharArray()[index1].ToString()))
                    {
                        str1 += strArray[index2, 1];
                        flag = true;
                    }
                    ++index2;
                }
            }
            int startIndex = 0;
            int num = 0;
            while (startIndex < str1.Length - 1)
            {
                result += int.Parse(str1.Substring(startIndex, 2)) * (18 - num);
                startIndex += 2;
                ++num;
            }
            Math.DivRem(result, 10, out result);
            string str2 = "";
            if (result > 0)
            {
                result = 10 - result;
                str2 = result.ToString();
            }
            switch (result)
            {
                case 0:
                    str2 = "0";
                    break;
                case 10:
                    str2 = "A";
                    break;
            }
            return str2;
        }

        public string CURPCompleta(string Appat, string Apmat, string Nombre, string FechaNac, string Sexo, string CodEsta)
        {
            string str = this.CURP10DIGITOS(Appat + " ", Apmat + " ", Nombre + " ", FechaNac.Replace("/", ""));
            return str + Sexo + CodEsta + this.SegConsonantes + "0" + this.ObtieneDigitoVerificador(str + Sexo + CodEsta + this.SegConsonantes + "0");
        }
    }
}
