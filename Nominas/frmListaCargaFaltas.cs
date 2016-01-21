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
    public partial class frmListaCargaFaltas : Form
    {
        public frmListaCargaFaltas()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        SqlBulkCopy bulk;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        string ruta, nombreEmpresa;
        string ExcelConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;'";
        int idEmpresa;
        Empresas.Core.EmpresasHelper eh;
        Empleados.Core.EmpleadosHelper emph;
        Faltas.Core.FaltasHelper fh;
        DateTime inicio, fin;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoNomina;
        public DateTime _inicioPeriodo;
        public DateTime _finPeriodo;
        #endregion

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
                            sheetName = dtExcelSchema.Rows[3]["TABLE_NAME"].ToString();
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
                                    if (dt.Rows[i][0].ToString() != "")
                                        dgvCargaFaltas.Rows.Add(
                                            dt.Rows[i][0].ToString(), //NO EMPLEADO
                                            dt.Rows[i][1].ToString(), //NOMBRE
                                            dt.Rows[i][2].ToString(), //PATERNO
                                            dt.Rows[i][3].ToString(), //MATERNO
                                            dt.Rows[i][4].ToString(), //NO. FALTAS
                                            dt.Rows[1][1].ToString(), //FECHA INICIO
                                            dt.Rows[2][1].ToString()); //FECHA FIN
                                }

                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    dgvCargaFaltas.AutoResizeColumn(i);
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
            if (dgvCargaFaltas.Rows.Count == 0)
            {
                MessageBox.Show("No se puede aplicar verifique.", "Error");
                return;
            }

            int idEmpleado = 0;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            bulk = new SqlBulkCopy(cnx);
            cmd.Connection = cnx;
            
            fh = new Faltas.Core.FaltasHelper();
            fh.Command = cmd;

            emph = new Empleados.Core.EmpleadosHelper();
            emph.Command = cmd;

            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            List<Faltas.Core.Faltas> lstMovimientos = new List<Faltas.Core.Faltas>();

            foreach (DataGridViewRow fila in dgvCargaFaltas.Rows)
            {
                try
                {
                    cnx.Open();
                    idEmpleado = (int)emph.obtenerIdTrabajador(fila.Cells["noempleado"].Value.ToString(), idEmpresa);
                    cnx.Close();

                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Obtener ID del empleado. \r\n \r\n" + error.Message, "Error");
                    return;
                }

                int idperiodo = 0;
                try
                {
                    cnx.Open();
                    idperiodo = (int)emph.obtenerIdPeriodo(idEmpleado);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: al obtener el Id del Periodo.", "Error");
                    cnx.Dispose();
                    return;
                }

                Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                p.idperiodo = idperiodo;

                int periodo = 0;
                try
                {
                    cnx.Open();
                    periodo = (int)ph.DiasDePago(p);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: al obtener los dias de pago.", "Error");
                    cnx.Dispose();
                    return;
                }

                int falta = int.Parse(fila.Cells["faltas"].Value.ToString());
                DateTime fecha = DateTime.Parse(fila.Cells["fechainicio"].Value.ToString());

                if (falta > 15)
                    falta = 15;

                for (int i = 0; i < falta; i++)
                {
                    int existe = 0;
                    int existeVacacion = 0;
                    try
                    {
                        cnx.Open();
                        existe = (int)fh.existeFalta(idEmpleado, fecha.AddDays(i).Date);
                        cnx.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Error: Al verificar existencia de falta.", "Error");
                        cnx.Dispose();
                        return;
                    }

                    if (existe == 0)
                    {
                        Incidencias.Core.IncidenciasHelper ih = new Incidencias.Core.IncidenciasHelper();
                        ih.Command = cmd;

                        Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
                        vh.Command = cmd;

                        try
                        {
                            cnx.Open();
                            existe = (int)ih.existeIncidenciaEnFalta(idEmpleado, fecha.AddDays(i).Date);
                            existeVacacion = (int)vh.existeVacacionEnFalta(idEmpleado, fecha.AddDays(i).Date);
                            cnx.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Error: Al guardar la falta.", "Error");
                            cnx.Dispose();
                            return;
                        }

                        if (existe == 0 && existeVacacion == 0)
                            try
                            {
                                Faltas.Core.Faltas f = new Faltas.Core.Faltas();
                                f.idempresa = GLOBALES.IDEMPRESA;
                                f.idtrabajador = idEmpleado;
                                f.periodo = periodo;
                                f.faltas = 1;
                                f.fechainicio = DateTime.Parse(dgvCargaFaltas.Rows[0].Cells["fechainicio"].Value.ToString());
                                f.fechafin = DateTime.Parse(dgvCargaFaltas.Rows[0].Cells["fechafin"].Value.ToString());
                                f.fecha = fecha.AddDays(i).Date;
                                lstMovimientos.Add(f);
                            }
                            catch
                            {
                                MessageBox.Show("Error: Al guardar la falta.", "Error");
                                cnx.Dispose();
                            }
                        else
                            MessageBox.Show("La falta ingresada, se empalma con una incapacidad y/o dia de vacación del trabajador.", "Error");
                    }
                }
            }

            fh.bulkCommand = bulk;
            DataTable dt = new DataTable();
            DataRow dtFila;
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("idtrabajador", typeof(Int32));
            dt.Columns.Add("idempresa", typeof(Int32));
            dt.Columns.Add("periodo", typeof(Int32));
            dt.Columns.Add("faltas", typeof(Int32));
            dt.Columns.Add("fechainicio", typeof(DateTime));
            dt.Columns.Add("fechafin", typeof(DateTime));
            dt.Columns.Add("fecha", typeof(DateTime));


            int index = 1;
            for (int i = 0; i < lstMovimientos.Count; i++)
            {
                dtFila = dt.NewRow();
                dtFila["id"] = index;
                dtFila["idtrabajador"] = lstMovimientos[i].idtrabajador;
                dtFila["periodo"] = lstMovimientos[i].periodo;
                dtFila["idempresa"] = lstMovimientos[i].idempresa;
                dtFila["faltas"] = lstMovimientos[i].faltas;
                dtFila["fechainicio"] = lstMovimientos[i].fechainicio;
                dtFila["fechafin"] = lstMovimientos[i].fechafin;
                dtFila["fecha"] = lstMovimientos[i].fecha;
                dt.Rows.Add(dtFila);
                index++;
            }

            try
            {
                cnx.Open();
                fh.bulkFaltas(dt, "tmpFaltas");
                fh.stpFaltas();
                cnx.Close();
                cnx.Dispose();

                MessageBox.Show("Faltas aplicadas correctamente.", "Confirmación");
                dgvCargaFaltas.Rows.Clear();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
        }

        private void toolLimpiar_Click(object sender, EventArgs e)
        {
            dgvCargaFaltas.Rows.Clear();
        }

        private void frmListaCargaFaltas_Load(object sender, EventArgs e)
        {
            dgvCargaFaltas.RowHeadersVisible = false;
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            frmFaltas f = new frmFaltas();
            f.OnFaltas += f_OnFaltas;
            f._tipoForma = 1; //SE AÑADE DIRECTO AL DATAGRIDVIEW
            f.ShowDialog();
        }

        void f_OnFaltas(string noempleado, string nombre, string paterno, string materno, int faltas, DateTime fechainicio, DateTime fechafin)
        {
            dgvCargaFaltas.Rows.Add(noempleado, nombre, paterno, materno, faltas, fechainicio, fechafin);
        }

    }
}
