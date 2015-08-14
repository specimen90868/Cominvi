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
            //dgvEmpleados.RowHeadersVisible = false;
            CalendarioColumn col = new CalendarioColumn();
            this.dgvEmpleados.Columns.Add(col);
            this.dgvEmpleados.RowCount = 5;
            foreach (DataGridViewRow row in this.dgvEmpleados.Rows)
            {
                row.Cells[0].Value = DateTime.Now;
            }
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
            DataGridViewCheckBoxColumn chkCelda = new DataGridViewCheckBoxColumn();
            CalendarioColumn colCalendario = new CalendarioColumn();

            dgvEmpleados.Columns.Add(chkCelda);
            dgvEmpleados.Columns.Add("idtrabajador", "IdTrabajador");
            dgvEmpleados.Columns.Add("nombre", "Nombre");
            dgvEmpleados.Columns.Add("sdivigente", "SDI Vigente");
            dgvEmpleados.Columns.Add("sdinuevo", "SDI Nuevo");
            dgvEmpleados.Columns.Add(colCalendario);

            dgvEmpleados.Columns["idtrabajador"].Visible = false;
            dgvEmpleados.Columns["idtrabajador"].DataPropertyName = "idtrabajador";
            dgvEmpleados.Columns["nombre"].DataPropertyName = "nombre";
            dgvEmpleados.Columns["sdivigente"].DataPropertyName = "sdivigente";
            dgvEmpleados.Columns["sdinuevo"].DataPropertyName = "sdinuevo";

            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvEmpleados.Columns[1].ReadOnly = true;
            dgvEmpleados.Columns[2].ReadOnly = true;
            dgvEmpleados.Columns[3].ReadOnly = true;
            dgvEmpleados.Columns[4].ReadOnly = true;

            dgvEmpleados.Columns[3].DefaultCellStyle = estilo;
            dgvEmpleados.Columns[4].DefaultCellStyle = estilo;
            #endregion

            try
            {
                cnx.Open();
                lstEmpleadosIncremento = eh.obtenerIncremento(empleado);
                cnx.Close();
                cnx.Dispose();

                var em = from e in lstEmpleadosIncremento
                         select new
                         {
                             e.idtrabajador,
                             e.nombre,
                             e.sdivigente,
                             e.sdinuevo
                         };
                dgvEmpleados.DataSource = em.ToList();

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
    }
}
