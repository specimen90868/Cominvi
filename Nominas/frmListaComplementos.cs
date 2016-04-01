using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nominas
{
    public partial class frmListaComplementos : Form
    {
        public frmListaComplementos()
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
                lstEmpleados = eh.obtenerEmpleadosBaja(empleado);
                cnx.Close();
                cnx.Dispose();

                var em = from e in lstEmpleados
                         select new
                         {
                             IdTrabajador = e.idtrabajador,
                             NoEmpleado = e.noempleado,
                             Nombre = e.nombrecompleto
                         };
                dgvComplementos.DataSource = em.ToList();

                for (int i = 0; i < dgvComplementos.Columns.Count; i++)
                {
                    dgvComplementos.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
            dgvComplementos.Columns["IdTrabajador"].Visible = false;
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Complementos");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Crear":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Consultar": toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); 
                        break;
                    case "Editar": toolEditar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); 
                        break;
                }
            }
        }

        private void Seleccion(int edicion)
        {
            int fila = 0;
            frmComplementos c = new frmComplementos();
            c.MdiParent = this.MdiParent;
            fila = dgvComplementos.CurrentCell.RowIndex;
            c._idEmpleado = int.Parse(dgvComplementos.Rows[fila].Cells[0].Value.ToString());
            c._nombreEmpleado = dgvComplementos.Rows[fila].Cells[2].Value.ToString();
            c._tipoOperacion = edicion;
            c.Show();
        }

        private void frmListaComplementos_Load(object sender, EventArgs e)
        {
            dgvComplementos.RowHeadersVisible = false;
            CargaPerfil();
            ListaEmpleados();
        }

        private void dgvComplementos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = 0;
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Complementos.Core.ComplementoHelper ch = new Complementos.Core.ComplementoHelper();
            ch.Command = cmd;

            fila = dgvComplementos.CurrentCell.RowIndex;
            Complementos.Core.Complemento c = new Complementos.Core.Complemento();
            c.idtrabajador = int.Parse(dgvComplementos.Rows[fila].Cells[0].Value.ToString());

            try
            {
                cnx.Open();
                int existe = (int)ch.existeComplemento(c);
                cnx.Close();
                cnx.Dispose();

                if (!existe.Equals(0))
                    toolNuevo.Enabled = false;
                else
                    toolNuevo.Enabled = true;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
        }

        private void toolConsultar_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.CONSULTAR);
        }

        private void toolEditar_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.MODIFICAR);
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.NUEVO);
        }

        private void dgvComplementos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Seleccion(GLOBALES.CONSULTAR);
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
                    dgvComplementos.DataSource = em.ToList();
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
                    dgvComplementos.DataSource = busqueda.ToList();
                }
                dgvComplementos.Columns["IdTrabajador"].Visible = false;
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
