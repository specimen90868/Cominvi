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
    public partial class frmListaCargaIncapacidades : Form
    {
        public frmListaCargaIncapacidades()
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
        Incapacidad.Core.IncapacidadHelper ih;
        #endregion

        private void toolCargar_Click(object sender, EventArgs e)
        {
            string conStr, sheetName;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleccionar Excel";
            ofd.RestoreDirectory = false;
            ofd.InitialDirectory = @"C:\";
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
                            sheetName = dtExcelSchema.Rows[4]["TABLE_NAME"].ToString();
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

                                for (int i = 2; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][0].ToString() != "")
                                    {
                                        dgvCargaIncapacidades.Rows.Add(
                                        dt.Rows[i][0].ToString(), //NO EMPLEADO
                                        dt.Rows[i][1].ToString(), //NOMBRE
                                        dt.Rows[i][2].ToString(), //PATERNO
                                        dt.Rows[i][3].ToString(), //MATERNO
                                        dt.Rows[i][4].ToString(), //DIAS INCAPACIDAD
                                        dt.Rows[i][5].ToString(), //FECHA INICIO
                                        dt.Rows[i][6].ToString(), //INICIO PERIODO
                                        dt.Rows[i][7].ToString()); //FIN PERIODO
                                    }
                                }

                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    dgvCargaIncapacidades.AutoResizeColumn(i);
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

        private void toolLimpiar_Click(object sender, EventArgs e)
        {
            dgvCargaIncapacidades.Rows.Clear();
        }

        private void frmListaCargaIncapacidades_Load(object sender, EventArgs e)
        {
            dgvCargaIncapacidades.RowHeadersVisible = false;
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            frmIncapacidad i = new frmIncapacidad();
            i.OnIncapacidad += i_OnIncapacidad;
            i._tipoForma = 1; //SE AÑADE DIRECTO AL DATAGRIDVIEW
            i.ShowDialog();
        }

        void i_OnIncapacidad()
        {
            //dgvCargaIncapacidades.Rows.Add(noempleado, nombre, paterno, materno, dias, fechainicio, inicio, fin);
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            txtBuscar.Font = new Font("Arial", 9);
            txtBuscar.ForeColor = System.Drawing.Color.Black;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                for (int i = 0; i < dgvCargaIncapacidades.Rows.Count; i++)
                {
                    if (txtBuscar.Text.Trim() == dgvCargaIncapacidades.Rows[i].Cells["noempleado"].Value.ToString())
                        dgvCargaIncapacidades.Rows[i].Selected = true;
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar no. empleado...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void toolAplicar_Click(object sender, EventArgs e)
        {
            if (dgvCargaIncapacidades.Rows.Count == 0)
            {
                MessageBox.Show("No se puede aplicar verifique.", "Error");
                return;
            }

            int i = 1;
            double diastomados = 0, diasrestantes = 0, diasapagar = 0, diasincapacidad = 0;
            DateTime fechaInicioIncapacidad, fechaFinIncapacidad, finPeriodo;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            bulk = new SqlBulkCopy(cnx);
            cmd.Connection = cnx;
            eh = new Empresas.Core.EmpresasHelper();
            eh.Command = cmd;

            //emph = new Empleados.Core.EmpleadosHelper();
            //emph.Command = cmd;

            try
            {
                cnx.Open();
                idEmpresa = eh.obtenerIdEmpresa(nombreEmpresa);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            if (idEmpresa != GLOBALES.IDEMPRESA)
            {
                MessageBox.Show("Intenta aplicar las incapacidades en un empresa diferente. \r\n \r\n La ventana se cerrará.", "Error");
                this.Dispose();
            }

            DataTable dt = new DataTable();
            DataRow dtFila;
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("idtrabajador", typeof(Int32));
            dt.Columns.Add("idempresa", typeof(Int32));
            dt.Columns.Add("diasincapacidad", typeof(Int32));
            dt.Columns.Add("diastomados", typeof(Int32));
            dt.Columns.Add("diasrestantes", typeof(Int32));
            dt.Columns.Add("diasapagar", typeof(Int32));
            dt.Columns.Add("tipo", typeof(Int32));
            dt.Columns.Add("aplicada", typeof(Int32));
            dt.Columns.Add("consecutiva", typeof(Int32));
            dt.Columns.Add("fechainicio", typeof(DateTime));
            dt.Columns.Add("fechafin", typeof(DateTime));

            foreach (DataGridViewRow fila in dgvCargaIncapacidades.Rows)
            {
                diasincapacidad = double.Parse(fila.Cells["diasincapacidad"].Value.ToString());
                fechaInicioIncapacidad = DateTime.Parse(fila.Cells["fechainicio"].Value.ToString());
                fechaFinIncapacidad = fechaInicioIncapacidad.AddDays(diasincapacidad - 1);
                finPeriodo = DateTime.Parse(fila.Cells["finperiodo"].Value.ToString());
                
                if (fechaFinIncapacidad <= finPeriodo)
                    diastomados = diasincapacidad;
                else
                {
                    diastomados = (finPeriodo - fechaInicioIncapacidad).TotalDays + 1;
                    diasrestantes = diasincapacidad - diastomados;
                }

                if (diasincapacidad > 3)
                {
                    if (diastomados <= 3)
                        diasapagar = 3 - diastomados;
                    else
                        diasapagar = 0;
                }
                else
                {
                    diasapagar = 3 - diastomados;
                }

                dtFila = dt.NewRow();
                dtFila["id"] = i;
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                emph = new Empleados.Core.EmpleadosHelper();
                emph.Command = cmd;
                try
                {
                    cnx.Open();
                    dtFila["idtrabajador"] = emph.obtenerIdTrabajador(fila.Cells["noempleado"].Value.ToString(), idEmpresa);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }
                dtFila["idempresa"] = idEmpresa;
                dtFila["diasincapacidad"] = diasincapacidad;
                dtFila["diastomados"] = diastomados;
                dtFila["diasrestantes"] = diasrestantes;
                dtFila["diasapagar"] = diasapagar;
                dtFila["tipo"] = 0;
                dtFila["aplicada"] = 1;
                dtFila["consecutiva"] = 1;
                dtFila["fechainicio"] = fechaInicioIncapacidad;
                dtFila["fechafin"] = fechaInicioIncapacidad.AddDays(diastomados - 1); 
                dt.Rows.Add(dtFila);
                i++;
            }

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            bulk = new SqlBulkCopy(cnx);
            cmd.Connection = cnx;

            ih = new Incapacidad.Core.IncapacidadHelper();
            ih.bulkCommand = bulk;
            ih.Command = cmd;

            try
            {
                cnx.Open();
                ih.bulkIncapacidad(dt, "tmpIncapacidades");
                ih.stpIncapacidad(DateTime.Parse(dgvCargaIncapacidades.Rows[0].Cells["inicioperiodo"].Value.ToString()),
                    DateTime.Parse(dgvCargaIncapacidades.Rows[0].Cells["finperiodo"].Value.ToString()));
                cnx.Close();
                cnx.Dispose();

                MessageBox.Show("Incapacidades aplicadas correctamente.", "Confirmación");
                dgvCargaIncapacidades.Rows.Clear();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
        }
    }
}
