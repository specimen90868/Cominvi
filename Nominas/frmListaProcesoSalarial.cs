using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nominas
{
    public partial class frmListaProcesoSalarial : Form
    {
        public frmListaProcesoSalarial()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Empleados.Core.IncrementoSalarial> lstEmpleadosIncremento;
        #endregion

        private void frmListaProcesoSalarial_Load(object sender, EventArgs e)
        {
            dgvEmpleados.RowHeadersVisible = false;
            ListaEmpleados();
        }

        private void ListaEmpleados()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.estatus = GLOBALES.ACTIVO;

            #region DISEÑO DEL GRIDVIEW

            dgvEmpleados.Columns["seleccion"].DataPropertyName = "chk";
            dgvEmpleados.Columns["idtrabajador"].DataPropertyName = "idtrabajador";
            dgvEmpleados.Columns["noempleado"].DataPropertyName = "noempleado";
            dgvEmpleados.Columns["nombre"].DataPropertyName = "nombre";
            dgvEmpleados.Columns["sdivigente"].DataPropertyName = "sdivigente";
            dgvEmpleados.Columns["sdinuevo"].DataPropertyName = "sdinuevo";
            dgvEmpleados.Columns["antiguedad"].DataPropertyName = "antiguedad";
            dgvEmpleados.Columns["antiguedadmod"].DataPropertyName = "antiguedadmod";
            dgvEmpleados.Columns["fechaimss"].DataPropertyName = "fechaimss";

            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvEmpleados.Columns[4].DefaultCellStyle = estilo;
            dgvEmpleados.Columns[5].DefaultCellStyle = estilo;

            dgvEmpleados.Columns["idtrabajador"].Visible = false;
            dgvEmpleados.Columns["noempleado"].ReadOnly = true;
            dgvEmpleados.Columns["nombre"].ReadOnly = true;
            dgvEmpleados.Columns["sdivigente"].ReadOnly = true;
            dgvEmpleados.Columns["antiguedad"].Visible = false;
            dgvEmpleados.Columns["antiguedadmod"].Visible = false;
            dgvEmpleados.Columns["sdinuevo"].ReadOnly = true;
            #endregion

            try
            {
                cnx.Open();
                lstEmpleadosIncremento = eh.obtenerIncremento(empleado);
                cnx.Close();
                cnx.Dispose();

                dgvEmpleados.DataSource = lstEmpleadosIncremento.ToList();

                for (int i = 0; i < dgvEmpleados.Columns.Count; i++)
                {
                    dgvEmpleados.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void toolAplicar_Click(object sender, EventArgs e)
        {

            int i = 1;

            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            SqlBulkCopy bulk = new SqlBulkCopy(cnx);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Historial.Core.HistorialHelper hh = new Historial.Core.HistorialHelper();
            hh.bulkCommand = bulk;
            hh.Command = cmd;

            DataTable dt = new DataTable();
            DataRow dtFila;
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("idtrabajador", typeof(Int32));
            dt.Columns.Add("tipomovimiento", typeof(Int32));
            dt.Columns.Add("valor", typeof(Double));
            dt.Columns.Add("fecha_imss", typeof(DateTime));
            dt.Columns.Add("fecha_sistema", typeof(DateTime));
            dt.Columns.Add("idempresa", typeof(Int32));
            dt.Columns.Add("motivobaja", typeof(Int32));

            DataTable dtAntiguedad = new DataTable();
            DataRow dtFilaAntiguedad;
            dtAntiguedad.Columns.Add("id", typeof(Int32));
            dtAntiguedad.Columns.Add("idtrabajador", typeof(Int32));
            dtAntiguedad.Columns.Add("sdi", typeof(Double));
            dtAntiguedad.Columns.Add("antiguedad", typeof(Int32));
            dtAntiguedad.Columns.Add("antiguedadmod", typeof(Int32));

            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                if ((int)fila.Cells["seleccion"].Value == 1)
                {
                    dtFila = dt.NewRow();
                    dtFila["id"] = i;
                    dtFila["idtrabajador"] = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                    dtFila["tipomovimiento"] = GLOBALES.mMODIFICACIONSALARIO;
                    dtFila["valor"] = double.Parse(fila.Cells["sdinuevo"].Value.ToString());
                    dtFila["fecha_imss"] = DateTime.Parse(fila.Cells["fechaimss"].Value.ToString());
                    dtFila["fecha_sistema"] = DateTime.Now;
                    dtFila["idempresa"] = GLOBALES.IDEMPRESA;
                    dtFila["motivobaja"] = 0;
                    dt.Rows.Add(dtFila);

                    dtFilaAntiguedad = dtAntiguedad.NewRow();
                    dtFilaAntiguedad["id"] = i;
                    dtFilaAntiguedad["idtrabajador"] = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                    dtFilaAntiguedad["sdi"] = double.Parse(fila.Cells["sdinuevo"].Value.ToString());
                    dtFilaAntiguedad["antiguedad"] = int.Parse(fila.Cells["antiguedad"].Value.ToString());
                    dtFilaAntiguedad["antiguedadmod"] = int.Parse(fila.Cells["antiguedadmod"].Value.ToString());
                    dtAntiguedad.Rows.Add(dtFilaAntiguedad);
                    i++;
                }
            }

            try
            {
                cnx.Open();
                hh.bulkMovimientos(dt, "tmpMovimientoTrabajador");
                hh.bulkMovimientos(dtAntiguedad, "tmpSalarioAntiguedad");
                hh.stpAntiguedadHistorial();
                cnx.Close();
                cnx.Dispose();

                MessageBox.Show("Incremento aplicado.", "Confirmación");
                ListaEmpleados();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
        }

        private void dgvEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvEmpleados.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }
}