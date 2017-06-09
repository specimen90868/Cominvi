using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nominas
{
    public partial class frmListaCargaEmpleados : Form
    {
        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        string ruta, nombreEmpresa;
        string ExcelConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;'";
        int idEmpresa;
        //Empresas.Core.EmpresasHelper eh;
        Empleados.Core.EmpleadosHelper emph;
        Periodos.Core.PeriodosHelper ph;
        Departamento.Core.DeptoHelper dh;
        Puestos.Core.PuestosHelper puestoh;
        Factores.Core.FactoresHelper fh;
        //Historial.Core.HistorialHelper hh;
        //Altas.Core.AltasHelper ah;
        Estados.Core.EstadosHelper edoh;
        int idDepto, idPuesto, idPeriodo;
        //string registroPatronal;
        DateTime periodoInicio, periodoFin;
        #endregion

        public frmListaCargaEmpleados()
        {
            InitializeComponent();
        }

        private void toolCargar_Click(object sender, EventArgs e)
        {
            string conStr, sheetName;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleccionar Excel";
            ofd.RestoreDirectory = false;
            ofd.InitialDirectory = @"C:\";
            ofd.Filter = "Documentos de Excel|*.xls; *.xlsx";

            if (DialogResult.OK == ofd.ShowDialog())
            {
                ruta = ofd.FileName;
                conStr = string.Empty;
                conStr = string.Format(ExcelConString, ruta);

                try
                {

                    using (OleDbConnection con = new OleDbConnection(conStr))
                    {
                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            cmd.Connection = con;
                            con.Open();
                            DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            sheetName = dtExcelSchema.Rows[7]["TABLE_NAME"].ToString();
                            con.Close();
                        }
                    }

                    using (OleDbConnection con = new OleDbConnection(conStr))
                    {
                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            using (OleDbDataAdapter oda = new OleDbDataAdapter())
                            {
                                DataTable dt = new DataTable();
                                cmd.CommandText = "SELECT * From [" + sheetName + "]";
                                cmd.Connection = con;
                                con.Open();
                                oda.SelectCommand = cmd;
                                oda.Fill(dt);
                                con.Close();

                                nombreEmpresa = dt.Columns[1].ColumnName;
                                idEmpresa = int.Parse(dt.Columns[3].ColumnName.ToString());

                                if (GLOBALES.IDEMPRESA != idEmpresa)
                                {
                                    MessageBox.Show("Los datos a ingresar pertenecen a otra empresa. Verifique. \r\n \r\n La ventana se cerrara.", "Error");
                                    this.Dispose();
                                }

                                for (int i = 2; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][0].ToString() != "")
                                        dgvCargaEmpleados.Rows.Add(
                                            dt.Rows[i][0].ToString(), //NO EMPLEADO
                                            dt.Rows[i][1].ToString().ToUpper(), //PATERNO
                                            dt.Rows[i][2].ToString().ToUpper(), //MATERNO
                                            dt.Rows[i][3].ToString().ToUpper(), //NOMBRE
                                            dt.Rows[i][4].ToString(), //PERIODO
                                            dt.Rows[i][5].ToString(), //DEPARTAMENTO
                                            dt.Rows[i][6].ToString(), //PUESTO
                                            dt.Rows[i][7].ToString(), //FECHA INGRESO
                                            dt.Rows[i][8].ToString().ToUpper(), //CURP
                                            dt.Rows[i][9].ToString(), //NSS
                                            dt.Rows[i][10].ToString(), //DIGITO VERIFICADOR
                                            dt.Rows[i][11].ToString()); //SUELDO INTEGRADO
                                }

                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    dgvCargaEmpleados.AutoResizeColumn(i);
                                }
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n Verifique que el archivo este cerrado. \r\n \r\n Descripcion: " + error.Message);
                }
            }
        }

        private void toolAplicar_Click(object sender, EventArgs e)
        {
            
            if (dgvCargaEmpleados.Rows.Count == 0)
            {
                MessageBox.Show("No se puede aplicar verifique.", "Error");
                return;
            }

            workerImporta.RunWorkerAsync();
        }

        private DateTime ObtieneFecha(string curp)
        {
            int numero17 = 0;
            string posicion17 = curp.Substring(16, 1);
            string anio = curp.Substring(4, 2);
            string mes = curp.Substring(6, 2);
            string dia = curp.Substring(8, 2);
            DateTime fechaNacimiento;

            try
            {
                numero17 = int.Parse(posicion17);
                fechaNacimiento = new DateTime(int.Parse("19" + anio), int.Parse(mes), int.Parse(dia));
            }
            catch
            {
                fechaNacimiento = new DateTime(int.Parse("20" + anio), int.Parse(mes), int.Parse(dia));
            }
            return fechaNacimiento;
        }

        private int ObtieneEdad(DateTime fecha)
        {
            DateTime fechaNacimiento = fecha;
            int edad = (DateTime.Now.Subtract(fechaNacimiento).Days / 365);
            return edad;
        }

        private decimal ObtieneSD(decimal sdi)
        {
            int DiasDePago = 0;
            decimal FactorDePago = 0;
            decimal sd = 0;
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ph = new Periodos.Core.PeriodosHelper();
            Periodos.Core.Periodos p = new Periodos.Core.Periodos();
            
            fh = new Factores.Core.FactoresHelper();
            Factores.Core.Factores f = new Factores.Core.Factores();

            ph.Command = cmd;
            fh.Command = cmd;

            p.idperiodo = idPeriodo;
            f.anio = 0;

            try
            {
                cnx.Open();
                DiasDePago = (int)ph.DiasDePago(p);
                FactorDePago = decimal.Parse(fh.FactorDePago(f).ToString());
                cnx.Close();

                sd = (sdi / FactorDePago);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n Al obtener los dias de pago y/o factor de pago. \r\n" + error.Message, "Error");
                this.Dispose();
            }
            return sd;
        }

        private decimal ObtieneSueldo(decimal sd)
        {
            int DiasDePago = 0;
            decimal sueldo = 0;
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ph = new Periodos.Core.PeriodosHelper();
            Periodos.Core.Periodos p = new Periodos.Core.Periodos();

            ph.Command = cmd;
            
            p.idperiodo = idPeriodo;
            
            try
            {
                cnx.Open();
                DiasDePago = (int)ph.DiasDePago(p);
                cnx.Close();

                sueldo = sd * DiasDePago;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n Al obtener los dias de pago y/o factor de pago. \r\n" + error.Message, "Error");
                this.Dispose();
            }
            return sueldo;
        }

        private int ObtenerDiasProporcionales(DateTime fechaingreso)
        {
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos p = new Periodos.Core.Periodos();
            p.idperiodo = idPeriodo;
            int diasPago = 0;
            try { cnx.Open(); diasPago = (int)ph.DiasDePago(p); cnx.Close(); }
            catch { MessageBox.Show("Error: Al obtener los dias de pago.", "Error"); }

            DateTime dt = fechaingreso.Date;
            
            int diasProporcionales = 0;
            if (diasPago == 7)
            {
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                periodoInicio = dt;
                periodoFin = dt.AddDays(6);
                diasProporcionales = (int)(periodoFin.Date - fechaingreso.Date).TotalDays + 1;
            }
            else
            {
                if (dt.Day <= 15)
                {
                    periodoInicio = new DateTime(dt.Year, dt.Month, 1);
                    periodoFin = new DateTime(dt.Year, dt.Month, 15);
                    diasProporcionales = (int)(periodoFin.Date - fechaingreso.Date).TotalDays + 1;
                }
                else
                {
                    int diasMes = DateTime.DaysInMonth(dt.Year, dt.Month);
                    int diasNoLaborados = 0;
                    periodoInicio = new DateTime(dt.Year, dt.Month, 16);
                    periodoFin = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
                    diasNoLaborados = (int)(fechaingreso.Date - periodoInicio).TotalDays;
                    diasProporcionales = 15 - diasNoLaborados;
                }
            }
            return diasProporcionales;
        }

        private int ObtenerIdEstado(string estado)
        {
            int idEstado = 0;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            edoh = new Estados.Core.EstadosHelper();
            edoh.Command = cmd;

            Estados.Core.Estados edo = new Estados.Core.Estados();
            edo.nombre = estado;

            try
            {
                cnx.Open();
                idEstado = (int)edoh.obtenerIdEstado(edo);
                cnx.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener el ID del estado.", "Error");
                cnx.Dispose();
                return 0;
            }
            return idEstado;
        }

        private string ObtenerRfc(string paterno, string materno, string nombre, DateTime fechanacimiento)
        {
            return "";
        }

        private string ObtenerEstado(string curp)
        {
            string estado = curp.Substring(11, 2);
            switch (estado)
            {
                case "AS": estado = "AGUASCALIENTES"; break;
                case "BC": estado = "BAJA CALIFORNIA"; break;
                case "BS": estado = "BAJA CALIFORNIA SUR"; break;
                case "CC": estado = "CAMPECHE"; break;
                case "CL": estado = "COAHUILA"; break;
                case "CM": estado = "COLIMA"; break;
                case "CS": estado = "CHIAPAS"; break;
                case "CH": estado = "CHIHUAHUA"; break;
                case "DF": estado = "DISTRITO FEDERAL"; break;
                case "DG": estado = "DURANGO"; break;
                case "GT": estado = "GUANAJUATO"; break;
                case "GR": estado = "GERRERO"; break;
                case "HG": estado = "HIDALGO"; break;
                case "JC": estado = "JALISCO"; break;
                case "MC": estado = "MEXICO"; break;
                case "MN": estado = "MICHOACAN"; break;
                case "MS": estado = "MORELOS"; break;
                case "NT": estado = "NAYARIT"; break;
                case "NL": estado = "NUEVO LEON"; break;
                case "OC": estado = "OAXACA"; break;
                case "PL": estado = "PUEBLA"; break;
                case "QT": estado = "QUERETARO"; break;
                case "QR": estado = "QUINTANA ROO"; break;
                case "SP": estado = "SAN LUIS POTOSI"; break;
                case "SL": estado = "SINALOA"; break;
                case "SR": estado = "SONORA"; break;
                case "TC": estado = "TABASCO"; break;
                case "TS": estado = "TAMAULIPAS"; break;
                case "TL": estado = "TLAXCALA"; break;
                case "VZ": estado = "VERACRUZ"; break;
                case "YN": estado = "YUCATAN"; break;
                case "ZS": estado = "ZACATECAS"; break;
            }
            return estado;
        }

        private string ObtenerSexo(string curp)
        {
            return curp.Substring(10, 1);
        }

        private void workerImporta_DoWork(object sender, DoWorkEventArgs e)
        {
            int contador = dgvCargaEmpleados.Rows.Count;
            int progreso = 0;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            foreach (DataGridViewRow fila in dgvCargaEmpleados.Rows)
            {
                
                workerImporta.ReportProgress((progreso * 100) / contador);
                progreso++;

                dh = new Departamento.Core.DeptoHelper();
                dh.Command = cmd;

                puestoh = new Puestos.Core.PuestosHelper();
                puestoh.Command = cmd;

                ph = new Periodos.Core.PeriodosHelper();
                ph.Command = cmd;

                try
                {
                    cnx.Open();
                    idDepto = (int)dh.obtenerIdDepartamento(fila.Cells["departamento"].Value.ToString(), GLOBALES.IDEMPRESA);
                    idPuesto = (int)puestoh.obtenerIdPuesto(fila.Cells["puesto"].Value.ToString(), GLOBALES.IDEMPRESA);
                    idPeriodo = (int)ph.obtenerIdPeriodo(fila.Cells["periodo"].Value.ToString(), GLOBALES.IDEMPRESA);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al obtener los datos del Departamento, Puesto y/o Periodo.", "Error");
                    cnx.Dispose();
                    return;
                }

                Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
                empleado.noempleado = fila.Cells["noempleado"].Value.ToString();
                empleado.nombres = fila.Cells["nombre"].Value.ToString();
                empleado.paterno = fila.Cells["paterno"].Value.ToString();
                empleado.materno = fila.Cells["materno"].Value.ToString();
                empleado.nombrecompleto = String.Format("{0} {1} {2}", empleado.paterno, empleado.materno, empleado.nombres);
                empleado.idempresa = GLOBALES.IDEMPRESA;
                empleado.idperiodo = idPeriodo;
                empleado.idsalario = 1;
                empleado.iddepartamento = idDepto;
                empleado.idpuesto = idPuesto;
                empleado.fechaingreso = DateTime.Parse(fila.Cells["fechaingreso"].Value.ToString());
                empleado.antiguedad = 0;
                empleado.fechaantiguedad = DateTime.Parse(fila.Cells["fechaingreso"].Value.ToString());
                empleado.antiguedadmod = 0;
                empleado.fechanacimiento = ObtieneFecha(fila.Cells["curp"].Value.ToString());
                empleado.edad = ObtieneEdad(empleado.fechanacimiento);
                empleado.rfc = ObtenerRfc(empleado.paterno, empleado.materno, empleado.nombres, empleado.fechanacimiento);
                empleado.curp = fila.Cells["curp"].Value.ToString();
                empleado.nss = fila.Cells["nss"].Value.ToString();
                empleado.digitoverificador = int.Parse(fila.Cells["dv"].Value.ToString());
                empleado.tiposalario = 19;
                empleado.sdi = decimal.Parse(fila.Cells["sdi"].Value.ToString());
                empleado.sd = ObtieneSD(decimal.Parse(fila.Cells["sdi"].Value.ToString()));
                empleado.sueldo = ObtieneSueldo(empleado.sd);
                empleado.estatus = 1;
                empleado.idusuario = GLOBALES.IDUSUARIO;
                empleado.cuenta = "0000000000";
                empleado.clabe = "000000000000000000";
                empleado.idbancario = "0000";
                empleado.metodopago = "TRANSFERENCIA";
                empleado.tiporegimen = 60;

                //Empleados.Core.EmpleadosEstatus ee = new Empleados.Core.EmpleadosEstatus();
                //ee.estatus = GLOBALES.ACTIVO;
                //ee.idempresa = GLOBALES.IDEMPRESA;

                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                emph = new Empleados.Core.EmpleadosHelper();
                emph.Command = cmd;

                Empleados.Core.Empleados nss = new Empleados.Core.Empleados();
                nss.nss = empleado.nss;
                nss.digitoverificador = empleado.digitoverificador;
                int existeEmpleado = 0;
                try
                {
                    cnx.Open();
                    existeEmpleado = (int)emph.existeEmpleado(nss);
                    cnx.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al obtener la existencia del empleado.", "Error");
                    cnx.Dispose();
                    return;
                }

                if (existeEmpleado == 0)
                {
                    try
                    {
                        cnx.Open();
                        emph.insertaEmpleado(empleado);
                        //ee.idtrabajador = (int)emph.obtenerIdTrabajador(empleado);
                        //emph.insertaEmpleadoEstatus(ee);
                        cnx.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Al ingresar al empleado. No. empleado: " + fila.Cells["noempleado"].Value.ToString(), "Error");
                        cnx.Dispose();
                        return;
                    }

                    int idEmpleado = 0;
                    try
                    {
                        cnx.Open();
                        idEmpleado = (int)emph.obtenerIdTrabajador(fila.Cells["noempleado"].Value.ToString(), GLOBALES.IDEMPRESA);
                        cnx.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Al obtener el ID del empleado. No. empleado: " + fila.Cells["noempleado"].Value.ToString(), "Error");
                        cnx.Dispose();
                        return;
                    }

                    //cnx = new SqlConnection(cdn);
                    //cmd = new SqlCommand();
                    //cmd.Connection = cnx;

                    //hh = new Historial.Core.HistorialHelper();
                    //hh.Command = cmd;

                    //Historial.Core.Historial historial = new Historial.Core.Historial();
                    //historial.idtrabajador = idEmpleado;
                    //historial.tipomovimiento = GLOBALES.mALTA;
                    //historial.valor = empleado.sdi;
                    //historial.fecha_imss = DateTime.Parse(fila.Cells["fechaingreso"].Value.ToString());
                    //historial.fecha_sistema = DateTime.Now.Date;
                    //historial.idempresa = GLOBALES.IDEMPRESA;
                    //historial.motivobaja = 0;

                    //try
                    //{
                    //    cnx.Open();
                    //    hh.insertarHistorial(historial);
                    //    cnx.Close();
                    //}
                    //catch (Exception)
                    //{
                    //    MessageBox.Show("Error: Al ingresar historial del trabajador. No. empleado: " + fila.Cells["noempleado"].Value.ToString(), "Error");
                    //    cnx.Dispose();
                    //    return;
                    //}

                    //cnx = new SqlConnection(cdn);
                    //cmd = new SqlCommand();
                    //cmd.Connection = cnx;

                    //eh = new Empresas.Core.EmpresasHelper();
                    //eh.Command = cmd;

                    //Empresas.Core.Empresas empresa = new Empresas.Core.Empresas();
                    //empresa.idempresa = GLOBALES.IDEMPRESA;

                    //try
                    //{
                    //    cnx.Open();
                    //    registroPatronal = eh.obtenerRegistroPatronal(empresa).ToString();
                    //    cnx.Close();
                    //}
                    //catch (Exception)
                    //{
                    //    MessageBox.Show("Error: Al obtener el registro patronal. NoEmpleado: " + fila.Cells["noempleado"].Value.ToString() + ".", "Error");
                    //    cnx.Dispose();
                    //    return;
                    //}

                    //Altas.Core.Altas alta = new Altas.Core.Altas();
                    //alta.idtrabajador = idEmpleado;
                    //alta.idempresa = GLOBALES.IDEMPRESA;
                    //alta.registropatronal = registroPatronal;
                    //alta.nss = empleado.nss + empleado.digitoverificador.ToString();
                    //alta.rfc = empleado.rfc;
                    //alta.curp = empleado.curp;
                    //alta.paterno = empleado.paterno;
                    //alta.materno = empleado.materno;
                    //alta.nombre = empleado.nombres;
                    //alta.contrato = 4;
                    //alta.jornada = 12;
                    //alta.fechaingreso = empleado.fechaingreso;
                    //alta.diasproporcionales = ObtenerDiasProporcionales(empleado.fechaingreso);
                    //alta.sdi = empleado.sdi;
                    //alta.cp = "0";
                    //alta.fechanacimiento = empleado.fechanacimiento;
                    //alta.estado = ObtenerEstado(empleado.curp);
                    //alta.noestado = ObtenerIdEstado(alta.estado);
                    //alta.clinica = "0";
                    //alta.sexo = ObtenerSexo(empleado.curp);
                    //alta.periodoInicio = periodoInicio;
                    //alta.periodoFin = periodoFin;


                    //cnx = new SqlConnection(cdn);
                    //cmd = new SqlCommand();
                    //cmd.Connection = cnx;

                    //ah = new Altas.Core.AltasHelper();
                    //ah.Command = cmd;

                    //try
                    //{
                    //    cnx.Open();
                    //    ah.insertaAlta(alta);
                    //    cnx.Close();
                    //}
                    //catch (Exception)
                    //{
                    //    MessageBox.Show("Error: Al ingresar alta del trabajador. No. empleado: " + fila.Cells["noempleado"].Value.ToString(), "Error");
                    //    cnx.Dispose();
                    //    return;
                    //}
                }
            }
            cnx.Dispose();
        }

        private void workerImporta_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblPorcentaje.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void workerImporta_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblPorcentaje.Text = "Completada.";
            dgvCargaEmpleados.Rows.Clear();
        }

        private void toolLimpiar_Click(object sender, EventArgs e)
        {
            dgvCargaEmpleados.Rows.Clear();
        }

        private void frmListaCargaEmpleados_Load(object sender, EventArgs e)
        {
            CargaPerfil("Importar empleados");
        }

        private void CargaPerfil(string nombre)
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES(nombre);

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Cargar": toolCargar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Aplicar": toolAplicar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                }
            }
        }
    }
}
