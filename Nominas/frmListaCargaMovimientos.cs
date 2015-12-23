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
    public partial class frmListaCargaMovimientos : Form
    {
        public frmListaCargaMovimientos()
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
        Movimientos.Core.MovimientosHelper mh;
        Conceptos.Core.ConceptosHelper ch;
        #endregion

        #region VARIABLES PUBLICA
        public int _tipoNomina;
        #endregion

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            frmMovimientos m = new frmMovimientos();
            m._tipoNomina = _tipoNomina;
            m._ventana = "Carga";
            m.OnMovimiento += m_OnMovimiento;
            m.MdiParent = this.MdiParent;
            m.Show();
        }

        void m_OnMovimiento(int idEmpleado, string concepto, double cantidad, DateTime inicio, DateTime fin)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            emph = new Empleados.Core.EmpleadosHelper();
            emph.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = idEmpleado;
            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();
            try
            {
                cnx.Open();
                lstEmpleado = emph.obtenerEmpleado(empleado);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Obtener No. de Empleado. \r\n \r\n" + error.Message, "Error");
                return;
            }

            dgvMovimientos.Rows.Add(lstEmpleado[0].noempleado, concepto, cantidad.ToString(), inicio.ToShortDateString(), fin.ToShortDateString());
        }

        private void toolCargar_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            string conStr, sheetName;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleccionar Excel";
            ofd.RestoreDirectory = false;
            ofd.InitialDirectory = @"C:\";
            ofd.Filter = "Documento de Excel|*.xls;*.xlsx";
            if (DialogResult.OK == ofd.ShowDialog())
            {
                ruta = ofd.FileName;
                conStr = string.Empty;
                conStr = string.Format(ExcelConString, ruta);

                try
                {

                    using (OleDbConnection con = new OleDbConnection(conStr))
                    {
                        using (OleDbCommand cmdOle = new OleDbCommand())
                        {
                            cmdOle.Connection = con;
                            con.Open();
                            DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            sheetName = dtExcelSchema.Rows[6]["TABLE_NAME"].ToString();
                            con.Close();
                        }
                    }

                    using (OleDbConnection con = new OleDbConnection(conStr))
                    {
                        using (OleDbCommand cmdOle = new OleDbCommand())
                        {
                            using (OleDbDataAdapter oda = new OleDbDataAdapter())
                            {
                                DataTable dt = new DataTable();
                                cmdOle.CommandText = "SELECT * From [" + sheetName + "]";
                                cmdOle.Connection = con;
                                con.Open();
                                oda.SelectCommand = cmdOle;
                                oda.Fill(dt);
                                con.Close();

                                nombreEmpresa = dt.Columns[1].ColumnName;
                                idEmpresa = int.Parse(dt.Columns[3].ColumnName.ToString());

                                if (GLOBALES.IDEMPRESA != idEmpresa)
                                {
                                    MessageBox.Show("Los datos a ingresar pertenecen a otra empresa. Verifique. \r\n \r\n La ventana se cerrara." , "Error");
                                    this.Dispose();
                                }

                                for (int i = 6; i < dt.Rows.Count; i++)
                                {
                                    for (int j = 1; j < 5; j++)
                                    {
                                        if (dt.Rows[i][j].ToString() != "")
                                        {
                                            dgvMovimientos.Rows.Add(
                                                dt.Rows[i][0].ToString(), //no empleado
                                                dt.Rows[i][1].ToString(), //cantidad
                                                dt.Rows[4][j].ToString(), //concepto
                                                dt.Rows[1][1].ToString(), //fecha inicio
                                                dt.Rows[2][1].ToString()); //fecha fin
                                        }
                                    }
                                }

                                for (int i = 0; i < dgvMovimientos.Columns.Count; i++)
                                {
                                    dgvMovimientos.AutoResizeColumn(i);
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
            dgvMovimientos.Rows.Clear();
        }

        private void toolAplicar_Click(object sender, EventArgs e)
        {
            if (dgvMovimientos.Rows.Count == 0)
            {
                MessageBox.Show("No se puede aplicar verifique.", "Error");
                return;
            }

            workMovimientos.RunWorkerAsync();
        }

        private void workMovimientos_DoWork(object sender, DoWorkEventArgs e)
        {
            int idConcepto = 0, idEmpleado = 0;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            List<Movimientos.Core.Movimientos> lstMovimientos = new List<Movimientos.Core.Movimientos>();

            mh = new Movimientos.Core.MovimientosHelper();
            mh.Command = cmd;

            ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            emph = new Empleados.Core.EmpleadosHelper();
            emph.Command = cmd;

            foreach (DataGridViewRow fila in dgvMovimientos.Rows)
            {
                try
                {
                    cnx.Open();
                    idConcepto = (int)ch.obtenerIdConcepto(fila.Cells["concepto"].Value.ToString(), idEmpresa);
                    idEmpleado = (int)emph.obtenerIdTrabajador(fila.Cells["noempleado"].Value.ToString(), idEmpresa);
                    cnx.Close();

                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Obtener ID del concepto. \r\n \r\n" + error.Message, "Error");
                    return;
                }

                Movimientos.Core.Movimientos mov = new Movimientos.Core.Movimientos();
                mov.idtrabajador = idEmpleado;
                mov.idempresa = idEmpresa;
                mov.idconcepto = idConcepto;
                mov.cantidad = double.Parse(fila.Cells["cantidad"].Value.ToString());
                mov.fechainicio = DateTime.Parse(fila.Cells["inicio"].Value.ToString());
                mov.fechafin = DateTime.Parse(fila.Cells["fin"].Value.ToString());
                lstMovimientos.Add(mov);
            }

            bulk = new SqlBulkCopy(cnx);
            mh.bulkCommand = bulk;

            DataTable dt = new DataTable();
            DataRow dtFila;
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("idtrabajador", typeof(Int32));
            dt.Columns.Add("idempresa", typeof(Int32));
            dt.Columns.Add("idconcepto", typeof(Int32));
            dt.Columns.Add("cantidad", typeof(Double));
            dt.Columns.Add("fechainicio", typeof(DateTime));
            dt.Columns.Add("fechafin", typeof(DateTime));
            
            int index = 1;
            for (int i = 0; i < lstMovimientos.Count; i++)
            {
                dtFila = dt.NewRow();
                dtFila["id"] = index;
                dtFila["idtrabajador"] = lstMovimientos[i].idtrabajador;
                dtFila["idempresa"] = lstMovimientos[i].idempresa;
                dtFila["idconcepto"] = lstMovimientos[i].idconcepto;
                dtFila["cantidad"] = lstMovimientos[i].cantidad;
                dtFila["fechainicio"] = lstMovimientos[i].fechainicio;
                dtFila["fechafin"] = lstMovimientos[i].fechafin;
                dt.Rows.Add(dtFila);
                index++;
            }

            try
            {
                cnx.Open();
                mh.bulkMovimientos(dt, "tmpMovimientos");
                mh.stpMovimientos();
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error (DataTable): \r\n \r\n" + error.Message, "Error");
            }

        }

        private void workMovimientos_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void workMovimientos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Movimientos importados", "Confirmacón");
            dgvMovimientos.Rows.Clear();
        }

        private void frmListaCargaMovimientos_Load(object sender, EventArgs e)
        {
            CargaPerfil();
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Carga movimientos");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Crear":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Cargar": toolCargar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Aplicar": toolAplicar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;

                }
            }
        }
    }
}
