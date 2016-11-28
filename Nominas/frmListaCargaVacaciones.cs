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
    public partial class frmListaCargaVacaciones : Form
    {
        public frmListaCargaVacaciones()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        SqlBulkCopy bulk;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        string ruta, nombreEmpresa = "";
        string ExcelConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;'";
        int idEmpresa;
        Empleados.Core.EmpleadosHelper emph;
        Vacaciones.Core.VacacionesHelper vh;
        Periodos.Core.PeriodosHelper ph;
        #endregion

        #region VARIABLES PUBLICA
        public int _tipoNomina;
        public DateTime _inicioPeriodo;
        public DateTime _finPeriodo;
        #endregion

        private void toolCargar_Click(object sender, EventArgs e)
        {
            string conStr, sheetName;
            DateTime inicio, fin;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleccionar Excel";
            ofd.RestoreDirectory = false;
            ofd.InitialDirectory = @"C:\";
            ofd.Filter = "Documentos de Excel|*.xls;*.xlsx";
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
                            sheetName = dtExcelSchema.Rows[5]["TABLE_NAME"].ToString();
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
                                inicio = DateTime.Parse(dt.Rows[1][1].ToString());
                                fin = DateTime.Parse(dt.Rows[2][1].ToString());

                                if (GLOBALES.IDEMPRESA != idEmpresa)
                                {
                                    MessageBox.Show("Los datos a ingresar pertenecen a otra empresa. Verifique. \r\n \r\n La ventana se cerrara.", "Error");
                                    this.Dispose();
                                }

                                if (inicio != _inicioPeriodo && fin != _finPeriodo)
                                {
                                    MessageBox.Show("Los datos a ingresar pertenecen a otro periodo. Verifique. \r\n \r\n La ventana se cerrara.", "Error");
                                    this.Dispose();
                                }

                                for (int i = 5; i < dt.Rows.Count; i++)
                                {
                                    dgvCargaVacaciones.Rows.Add(
                                        dt.Rows[i][0].ToString(), //NO EMPLEADO
                                        dt.Rows[i][1].ToString(), //NOMBRE
                                        dt.Rows[i][2].ToString(), //PATERNO
                                        dt.Rows[i][3].ToString(), //MATERNO
                                        dt.Rows[i][4].ToString(), //CONCEPTO
                                        dt.Rows[i][5].ToString(), //DIAS
                                        dt.Rows[1][1].ToString(), //FECHA INICIO
                                        dt.Rows[2][1].ToString(), //FECHA FIN
                                        dt.Rows[i][6].ToString()); //FECHA INICIO VACACIONES
                                }

                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    dgvCargaVacaciones.AutoResizeColumn(i);
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
            if (dgvCargaVacaciones.Rows.Count == 0)
            {
                MessageBox.Show("No se puede aplicar verifique.", "Error");
                return;
            }

            workVacaciones.RunWorkerAsync();
            
        }

        private void toolLimpiar_Click(object sender, EventArgs e)
        {
            dgvCargaVacaciones.Rows.Clear();
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            //frmVacaciones v = new frmVacaciones();
            //v.OnVacacion += v_OnVacacion;
            //v._tipoNomina = _tipoNomina;
            //v._ventana = "Carga";
            //v.MdiParent = this.MdiParent;
            //v.Show();
        }

        void v_OnVacacion(string noempleado, string nombre, string paterno, string materno, bool prima, bool pago, int diaspagopv, bool vacacion, int diaspago, DateTime fechainicio, DateTime fechafin)
        {
            dgvCargaVacaciones.Rows.Add(noempleado,nombre,paterno,materno,prima,pago,diaspagopv,vacacion,diaspago,fechainicio,fechafin);
        }

        private void workVacaciones_DoWork(object sender, DoWorkEventArgs e)
        {
            int idEmpleado = 0;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            List<Vacaciones.Core.VacacionesPrima> lstMovimientos = new List<Vacaciones.Core.VacacionesPrima>();

            vh = new Vacaciones.Core.VacacionesHelper();
            vh.Command = cmd;

            emph = new Empleados.Core.EmpleadosHelper();
            emph.Command = cmd;

            ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            foreach (DataGridViewRow fila in dgvCargaVacaciones.Rows)
            {
                try
                {
                    cnx.Open();
                    idEmpleado = (int)emph.obtenerIdTrabajador(fila.Cells["noempleado"].Value.ToString(), idEmpresa);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Obtener ID del concepto. \r\n \r\n" + error.Message, "Error");
                    cnx.Dispose();
                    this.Dispose();
                }

                Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
                empleado.idtrabajador = idEmpleado;

                List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();

                try
                {
                    cnx.Open();
                    lstEmpleado = emph.obtenerEmpleado(empleado);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al obtener la antigüedad del empleado.", "Error");
                    cnx.Dispose();
                    this.Dispose();
                }

                Vacaciones.Core.DiasDerecho dd = new Vacaciones.Core.DiasDerecho();
                dd.anio = lstEmpleado[0].antiguedadmod;

                int dias = 0;
                try
                {
                    cnx.Open();
                    dias = (int)vh.diasDerecho(dd);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al obtener los dias por derecho del empleado.", "Error");
                    cnx.Dispose();
                    return;
                }

                Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
                fh.Command = cmd;

                Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
                falta.idempresa = GLOBALES.IDEMPRESA;
                falta.idtrabajador = idEmpleado;
                falta.fechainicio = DateTime.Parse(dgvCargaVacaciones.Rows[0].Cells["inicio"].Value.ToString());
                falta.fechafin = DateTime.Parse(dgvCargaVacaciones.Rows[0].Cells["fin"].Value.ToString());

                int existeFaltas = 0;
                try
                {
                    cnx.Open();
                    existeFaltas = (int)fh.existeFalta(falta);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Al obtener las faltas del trabajador. \r\n" + error.Message, "Error");
                    cnx.Dispose();
                    return;
                }

                int idperiodo = 0;
                try
                {
                    cnx.Open();
                    idperiodo = (int)emph.obtenerIdPeriodo(idEmpleado);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Al obtener el id del periodo. \r\n" + error.Message, "Error");
                    cnx.Dispose();
                    return;
                }
                Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                p.idperiodo = idperiodo;

                int diasPeriodo = 0;
                try
                {
                    cnx.Open();
                    diasPeriodo = (int)ph.DiasDePago(p);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Al obtener los dias del periodo. \r\n" + error.Message, "Error");
                    cnx.Dispose();
                    return;
                }

                int diasPagoReales = int.Parse(fila.Cells["diaspago"].Value.ToString()) + existeFaltas;
                if (diasPagoReales >= diasPeriodo)
                {
                    diasPagoReales = diasPeriodo - existeFaltas;
                    MessageBox.Show("Existen faltas del trabajador, se ajustarán las vacaciones.", "Información");
                }
                else
                {
                    diasPagoReales = int.Parse(fila.Cells["diaspago"].Value.ToString());
                }

                Vacaciones.Core.VacacionesPrima vp = new Vacaciones.Core.VacacionesPrima();
                vp.idtrabajador = idEmpleado;
                vp.idempresa = GLOBALES.IDEMPRESA;
                vp.periodoinicio = DateTime.Parse(dgvCargaVacaciones.Rows[0].Cells["inicio"].Value.ToString());
                vp.periodofin = DateTime.Parse(dgvCargaVacaciones.Rows[0].Cells["fin"].Value.ToString());
                vp.diasderecho = dias;
                vp.fechapago = DateTime.Now.Date;
                vp.vacacionesprima = fila.Cells["concepto"].Value.ToString() == "Prima Vacacional" ? "P" : "V";

                if (fila.Cells["concepto"].Value.ToString() == "Prima Vacacional")
                {
                    vp.diaspago = diasPagoReales;
                    vp.diaspendientes = dias - diasPagoReales;
                    vp.fechainicio = DateTime.Now.Date;
                    vp.fechafin = DateTime.Now.Date;
                }
                else
                {
                    vp.diaspago = diasPagoReales;
                    vp.diaspendientes = dias - diasPagoReales;
                    vp.fechainicio = DateTime.Parse(fila.Cells["fechaaplicacion"].Value.ToString());
                    vp.fechafin = DateTime.Parse(fila.Cells["fechaaplicacion"].Value.ToString()).AddDays(diasPagoReales - 1);
                }
                
                lstMovimientos.Add(vp);
            }

            bulk = new SqlBulkCopy(cnx);
            vh.bulkCommand = bulk;

            DataTable dt = new DataTable();
            DataRow dtFila;
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("idtrabajador", typeof(Int32));
            dt.Columns.Add("idempresa", typeof(Int32));
            dt.Columns.Add("periodoinicio", typeof(DateTime));
            dt.Columns.Add("periodofin", typeof(DateTime));
            dt.Columns.Add("diasderecho", typeof(Int32));
            dt.Columns.Add("diaspago", typeof(Int32));
            dt.Columns.Add("diaspendientes", typeof(Int32));
            dt.Columns.Add("fechapago", typeof(DateTime));
            dt.Columns.Add("vacacionesprima", typeof(String));
            dt.Columns.Add("fechainicio", typeof(DateTime));
            dt.Columns.Add("fechafin", typeof(DateTime));

            int index = 1;
            for (int i = 0; i < lstMovimientos.Count; i++)
            {
                dtFila = dt.NewRow();
                dtFila["id"] = i + 1;
                dtFila["idtrabajador"] = lstMovimientos[i].idtrabajador;
                dtFila["idempresa"] = lstMovimientos[i].idempresa;
                dtFila["periodoinicio"] = lstMovimientos[i].periodoinicio;
                dtFila["periodofin"] = lstMovimientos[i].periodofin;
                dtFila["diasderecho"] = lstMovimientos[i].diasderecho;
                dtFila["diaspago"] = lstMovimientos[i].diaspago;
                dtFila["diaspendientes"] = lstMovimientos[i].diaspendientes;
                dtFila["fechapago"] = lstMovimientos[i].fechapago;
                dtFila["vacacionesprima"] = lstMovimientos[i].vacacionesprima;
                dtFila["fechainicio"] = lstMovimientos[i].fechainicio;
                dtFila["fechafin"] = lstMovimientos[i].fechafin;
                dt.Rows.Add(dtFila);
                index++;
            }

            try
            {
                cnx.Open();
                vh.bulkVacaciones(dt, "tmpVacacionesPrima");
                vh.stpVacaciones();
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error (DataTable): \r\n \r\n" + error.Message, "Error");
            }

        }

        private void workVacaciones_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Vacación aplicada.", "Confirmación");
            dgvCargaVacaciones.Rows.Clear();
        }

        private void frmListaCargaVacaciones_Load(object sender, EventArgs e)
        {
            CargaPerfil();
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Carga Vacaciones");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Crear":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Cargar": toolCargar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Aplicar": toolAplicar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                }
            }
        }
    }
}
