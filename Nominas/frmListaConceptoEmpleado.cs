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
    public partial class frmListaConceptoEmpleado : Form
    {
        public frmListaConceptoEmpleado()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        List<Empleados.Core.Empleados> lstEmpleados;
        #endregion

        private void ListaEmpleados()
        {

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.estatus = GLOBALES.ACTIVO;

            try
            {
                cnx.Open();
                lstEmpleados = eh.obtenerEmpleados(empleado);
                cnx.Close();
                cnx.Dispose();

                var em = from e in lstEmpleados
                         select new
                         {
                             IdTrabajador = e.idtrabajador,
                             NoEmpleado = e.noempleado,
                             Nombre = e.nombrecompleto
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

            dgvEmpleados.Columns["IdTrabajador"].Visible = false;
        }

        private void frmListaConceptoEmpleado_Load(object sender, EventArgs e)
        {
            dgvEmpleados.RowHeadersVisible = false;
            ListaEmpleados();
            CargaPerfil();
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Concepto - Empleado");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Consultar":
                        toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                }
            }
        }

        private void toolConsultar_Click(object sender, EventArgs e)
        {
            int fila = 0;
            frmConceptoEmpleado ce = new frmConceptoEmpleado();
            fila = dgvEmpleados.CurrentCell.RowIndex;
            ce._idEmpleado = int.Parse(dgvEmpleados.Rows[fila].Cells[0].Value.ToString());
            ce._nombreEmpleado = dgvEmpleados.Rows[fila].Cells[2].Value.ToString();
            ce.MdiParent = this.MdiParent;
            ce.Show();
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
                if (string.IsNullOrEmpty(txtBuscar.Text) || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    var em = from emp in lstEmpleados
                             select new
                             {
                                 IdTrabajador = emp.idtrabajador,
                                 NoEmpleado = emp.noempleado,
                                 Nombre = emp.nombrecompleto
                             };
                    dgvEmpleados.DataSource = em.ToList();
                }
                else
                {
                    var busqueda = from b in lstEmpleados
                                   where b.nombrecompleto.Contains(txtBuscar.Text.ToUpper()) || b.noempleado.Contains(txtBuscar.Text)
                                   select new
                                   {
                                       IdTrabajador = b.idtrabajador,
                                       NoEmpleado = b.noempleado,
                                       Nombre = b.nombrecompleto
                                   };
                    dgvEmpleados.DataSource = busqueda.ToList();
                }
                dgvEmpleados.Columns["IdTrabajador"].Visible = false;
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar empleado...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }
    }
}
