using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Nominas
{
    public partial class frmListaEmpleados : Form
    {
        public frmListaEmpleados()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        MySqlConnection cnx;
        MySqlCommand cmd;
        List<Empleados.Core.Empleados> lstEmpleados;
        #endregion

       
        private void frmListaEmpleados_Load(object sender, EventArgs e)
        {
            dgvEmpleados.RowHeadersVisible = false;
            CargaPerfil();
            ListaEmpleados();
        }

        private void ListaEmpleados()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new MySqlConnection(cdn);
            cmd = new MySqlCommand();
            cmd.Connection = cnx;
            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.idplaza = GLOBALES.IDPLAZA;
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
                             Nombre = e.nombrecompleto,
                             Ingreso = e.fechaingreso,
                             Antiguedad = e.antiguedad + " AÑOS",
                             SDI = e.sdi,
                             SD = e.sd,
                             Sueldo = e.sueldo
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

            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEmpleados.Columns[4].DefaultCellStyle = estilo;
            dgvEmpleados.Columns[5].DefaultCellStyle = estilo;
            dgvEmpleados.Columns[6].DefaultCellStyle = estilo;
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Trabajadores");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].nombre.ToString())
                {
                    case "Trabajadores":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].crear);
                        toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].consulta);
                        toolEditar.Enabled = Convert.ToBoolean(lstEdiciones[i].modificar);
                        toolBaja.Enabled = Convert.ToBoolean(lstEdiciones[i].baja);
                        break;
                }
            }
        }

        private void Seleccion(int edicion)
        {
            int fila = 0;
            frmEmpleados e = new frmEmpleados();
            e.MdiParent = this.MdiParent;
            e.OnNuevoEmpleado += e_OnNuevoEmpleado;
            if (!edicion.Equals(GLOBALES.NUEVO))
            {
                fila = dgvEmpleados.CurrentCell.RowIndex;
                e._idempleado = int.Parse(dgvEmpleados.Rows[fila].Cells[0].Value.ToString());
            }
            e._tipoOperacion = edicion;
            e.Show();
        }

        void e_OnNuevoEmpleado(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaEmpleados();
        }

        private void dgvEmpleados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Seleccion(GLOBALES.CONSULTAR);
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.NUEVO);
        }

        private void toolConsultar_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.CONSULTAR);
        }

        private void toolEditar_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.MODIFICAR);
        }

        private void toolBaja_Click(object sender, EventArgs e)
        {
            int estatus = 0;
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            int fila = dgvEmpleados.CurrentCell.RowIndex;
            int idempleado = int.Parse(dgvEmpleados.Rows[fila].Cells[0].Value.ToString());

            cnx = new MySqlConnection(cdn);
            cmd = new MySqlCommand();
            cmd.Connection = cnx;

            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = idempleado;

            LayoutMovimientos.Core.LayoutHelper lh = new LayoutMovimientos.Core.LayoutHelper();
            lh.Command = cmd;
            LayoutMovimientos.Core.LayoutMovimientos lm = new LayoutMovimientos.Core.LayoutMovimientos();
            lm.idtrabajador = idempleado;

            try
            {
                cnx.Open();
                estatus = (int)eh.obtenerEstatus(empleado);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }

            if (estatus.Equals(0))
            {
                DialogResult respuesta = MessageBox.Show("¿Quiere eliminar la trabajador?", "Confirmación", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    //eh = new Empleados.Core.EmpleadosHelper();
                    //eh.Command = cmd;
                    
                    try
                    {
                        cnx.Open();
                        eh.bajaEmpleado(empleado);
                        lh.bajaLayoutMovimiento(lm);
                        cnx.Close();
                        cnx.Dispose();
                        ListaEmpleados();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("El empleado no puede ser eliminado. Ya tiene movimientos registrados.", "Confirmación");
            }
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
                    var empleado = from em in lstEmpleados
                             select new
                             {
                                 IdTrabajador = em.idtrabajador,
                                 Nombre = em.nombrecompleto,
                                 Ingreso = em.fechaingreso,
                                 Antiguedad = em.antiguedad + " AÑOS",
                                 SDI = em.sdi,
                                 SD = em.sd,
                                 Sueldo = em.sueldo
                             };
                    dgvEmpleados.DataSource = empleado.ToList();
                }
                else
                {
                    var busqueda = from b in lstEmpleados
                                   where b.nombrecompleto.Contains(txtBuscar.Text.ToUpper())
                                   select new
                                   {
                                       IdTrabajador = b.idtrabajador,
                                       Nombre = b.nombrecompleto,
                                       Ingreso = b.fechaingreso,
                                       Antiguedad = b.antiguedad + " AÑOS",
                                       SDI = b.sdi,
                                       SD = b.sd,
                                       Sueldo = b.sueldo
                                   };
                    dgvEmpleados.DataSource = busqueda.ToList();
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar empleado...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void toolModificarSalario_Click(object sender, EventArgs e)
        {
            int fila = dgvEmpleados.CurrentCell.RowIndex;
            frmModificaSalarioImss msi = new frmModificaSalarioImss();
            msi.MdiParent = this.MdiParent;
            msi._idempleado = int.Parse(dgvEmpleados.Rows[fila].Cells[0].Value.ToString());
            msi._nombreCompleto = dgvEmpleados.Rows[fila].Cells[1].Value.ToString();
            msi.Show();
        }

        private void frmListaEmpleados_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
