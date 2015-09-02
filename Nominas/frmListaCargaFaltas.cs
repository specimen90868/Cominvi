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
                            sheetName = dtExcelSchema.Rows[2]["TABLE_NAME"].ToString();
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
                                    dgvCargaFaltas.Rows.Add(
                                        dt.Rows[i][0].ToString(), //NO EMPLEADO
                                        dt.Rows[i][1].ToString(), //NOMBRE
                                        dt.Rows[i][2].ToString(), //PATERNO
                                        dt.Rows[i][3].ToString(), //MATERNO
                                        dt.Rows[i][4].ToString(), //FALTAS
                                        dt.Rows[i][5].ToString(), //FECHA INICIO
                                        dt.Rows[i][6].ToString()); //FECHA FIN
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

            int i = 1;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            bulk = new SqlBulkCopy(cnx);
            cmd.Connection = cnx;
            eh = new Empresas.Core.EmpresasHelper();
            eh.Command = cmd;

            fh = new Faltas.Core.FaltasHelper();
            fh.bulkCommand = bulk;
            fh.Command = cmd;

            emph = new Empleados.Core.EmpleadosHelper();
            emph.Command = cmd;

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

            DataTable dt = new DataTable();
            DataRow dtFila;
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("idtrabajador", typeof(Int32));
            dt.Columns.Add("idempresa", typeof(Int32));
            dt.Columns.Add("idperiodo", typeof(Int32));
            dt.Columns.Add("faltas", typeof(Int32));
            dt.Columns.Add("fechainicio", typeof(DateTime));
            dt.Columns.Add("fechafin", typeof(DateTime));

            foreach (DataGridViewRow fila in dgvCargaFaltas.Rows)
            {
                dtFila = dt.NewRow();
                dtFila["id"] = i;
                try { 
                    cnx.Open();
                    dtFila["idtrabajador"] = emph.obtenerIdTrabajador(fila.Cells["noempleado"].Value.ToString());
                    dtFila["idperiodo"] = emph.obtenerIdPeriodo(fila.Cells["noempleado"].Value.ToString());
                    cnx.Close();
                } catch (Exception error) { 
                    MessageBox.Show("Error: \r\n \r\n" + error.Message,"Error"); 
                }
                dtFila["idempresa"] = idEmpresa;
                dtFila["faltas"] = fila.Cells["nofaltas"].Value;
                dtFila["fechainicio"] = fila.Cells["fechainicio"].Value;
                dtFila["fechafin"] = fila.Cells["fechafin"].Value;
                dt.Rows.Add(dtFila);
                i++;
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

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            txtBuscar.Font = new Font("Arial", 9);
            txtBuscar.ForeColor = System.Drawing.Color.Black;
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar no. empleado...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                for (int i = 0; i < dgvCargaFaltas.Rows.Count; i++)
                {
                    if (txtBuscar.Text.Trim() == dgvCargaFaltas.Rows[i].Cells["noempleado"].Value.ToString())
                        dgvCargaFaltas.Rows[i].Selected = true;
                }
            }
        }
    }
}
