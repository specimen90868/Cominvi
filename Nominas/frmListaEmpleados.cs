using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
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
        SqlConnection cnx;
        SqlCommand cmd;
        List<Empleados.Core.CatalogoEmpleado> lstEmpleados;
        #endregion

        #region VARIABLES PUBLICAS
        public int _empleadoAltaBaja;
        #endregion

        private void frmListaEmpleados_Load(object sender, EventArgs e) 
        {
            dgvEmpleados.RowHeadersVisible = false;
            ListaEmpleados();

            if (_empleadoAltaBaja == GLOBALES.INACTIVO)
            {
                CargaPerfil(GLOBALES.INACTIVO, "Empleados en Baja");
            }
            else
            {
                CargaPerfil(GLOBALES.ACTIVO, "Empleados de nómina");
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

            try
            {
                cnx.Open();
                lstEmpleados = eh.obtenerEmpleados(GLOBALES.IDEMPRESA);
                cnx.Close();
                cnx.Dispose();

                var em = from e in lstEmpleados
                         select new
                         {
                             IdTrabajador = e.idtrabajador,
                             NoEmpleado = e.noempleado,
                             Nombre = e.nombrecompleto,
                             Ingreso = e.fechaingreso,
                             Antiguedad = e.antiguedad + " AÑOS",
                             SDI = e.sdi,
                             SD = e.sd,
                             Sueldo     = e.sueldo,
                             Cuenta = e.cuenta,
                             Estado = e.estatus,
                             Departamento = e.departamento,
                             Puesto = e.puesto,
                             FechaBaja = e.fechabaja
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
            dgvEmpleados.Columns[5].DefaultCellStyle = estilo;
            dgvEmpleados.Columns[6].DefaultCellStyle = estilo;
            dgvEmpleados.Columns[7].DefaultCellStyle = estilo;

            dgvEmpleados.Columns["IdTrabajador"].Visible = false;
        }

        private void CargaPerfil(int activo_inactivo, string nombre)
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES(nombre);

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Crear": toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Consular": toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Editar": toolEditar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Baja": toolBaja.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Historial": toolHistorial.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Reingreso": toolReingreso.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Eliminar": toolEliminar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Incrementar Salario": toolIncrementoSalario.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
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
                                       NoEmpleado = em.noempleado,
                                       Ingreso = em.fechaingreso,
                                       Antiguedad = em.antiguedad + " AÑOS",
                                       SDI = em.sdi,
                                       SD = em.sd,
                                       Sueldo = em.sueldo,
                                       Cuenta = em.cuenta,
                                       Estado = em.estatus,
                                       Departamento = em.departamento,
                                       Puesto = em.puesto,
                                       FechaBaja = em.fechabaja
                                   };
                    dgvEmpleados.DataSource = empleado.ToList();
                }
                else
                {
                    var busqueda = from b in lstEmpleados
                                   where b.nombrecompleto.Contains(txtBuscar.Text.ToUpper()) || b.noempleado.Contains(txtBuscar.Text)
                                   select new
                                   {
                                       IdTrabajador = b.idtrabajador,
                                       NoEmpleado = b.noempleado,
                                       Nombre = b.nombrecompleto,
                                       Ingreso = b.fechaingreso,
                                       Antiguedad = b.antiguedad + " AÑOS",
                                       SDI = b.sdi,
                                       SD = b.sd,
                                       Sueldo = b.sueldo,
                                       Cuenta = b.cuenta, 
                                       Estado = b.estatus,
                                       Departamento = b.departamento,
                                       Puesto = b.puesto,
                                       FechaBaja = b.fechabaja
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

        private void toolModificarSalario_Click(object sender, EventArgs e)
        {
            //int fila = dgvEmpleados.CurrentCell.RowIndex;
            //frmModificaSalarioImss msi = new frmModificaSalarioImss();
            //msi.MdiParent = this.MdiParent;
            //msi._idempleado = int.Parse(dgvEmpleados.Rows[fila].Cells[0].Value.ToString());
            //msi._nombreCompleto = dgvEmpleados.Rows[fila].Cells[1].Value.ToString();
            //msi.Show();
        }

        private void frmListaEmpleados_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void toolIncrementoSalario_Click(object sender, EventArgs e)
        {
            int fila = dgvEmpleados.CurrentCell.RowIndex;
            frmIncrementoSalarial isal = new frmIncrementoSalarial();
            isal.OnIncrementoSalarial += isal_OnIncrementoSalarial;
            isal.MdiParent = this.MdiParent;
            isal._nombreEmpleado = dgvEmpleados.Rows[fila].Cells[2].Value.ToString();
            isal._idempleado = int.Parse(dgvEmpleados.Rows[fila].Cells[0].Value.ToString());
            isal.Show();
        }

        void isal_OnIncrementoSalarial()
        {
            ListaEmpleados();
        }

        private void toolHistorial_Click(object sender, EventArgs e)
        {
            int fila = dgvEmpleados.CurrentCell.RowIndex;
            frmListaHistorial lh = new frmListaHistorial();
            lh.MdiParent = this.MdiParent;
            lh._idempleado = int.Parse(dgvEmpleados.Rows[fila].Cells[0].Value.ToString());
            lh.Show();
        }

        private void toolEliminar_Click(object sender, EventArgs e)
        {
            //int estatus = 0;
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            int fila = dgvEmpleados.CurrentCell.RowIndex;
            int idempleado = int.Parse(dgvEmpleados.Rows[fila].Cells[0].Value.ToString());

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = idempleado;

            //try
            //{
            //    cnx.Open();
            //    estatus = (int)eh.obtenerEstatus(empleado);
            //    cnx.Close();
            //    cnx.Dispose();
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            //}

            //if (estatus.Equals(1)) {}
            //else { MessageBox.Show("El empleado no puede ser eliminado. Ya tiene movimientos registrados.", "Confirmación"); }

            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar la trabajador?. \r\n \r\n CUIDADO. Esta acción eliminará permanentemente el Empleado.", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                //eh = new Empleados.Core.EmpleadosHelper();
                //eh.Command = cmd;

                try
                {
                    cnx.Open();
                    eh.eliminarEmpleado(empleado);
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

        private void toolBaja_Click(object sender, EventArgs e)
        {
            int fila = dgvEmpleados.CurrentCell.RowIndex;

            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = int.Parse(dgvEmpleados.Rows[fila].Cells[0].Value.ToString());

            int estatus = 0;
            try
            {
                cnx.Open();
                estatus = (int)eh.obtenerEstatus(empleado);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener el estatus de trabajador.", "Error");
                cnx.Dispose();
                return;
            }

            if (estatus == 1)
            {
                frmBaja b = new frmBaja();
                b.OnBajaEmpleado += b_OnBajaEmpleado;
                b.MdiParent = this.MdiParent;
                b._idempleado = int.Parse(dgvEmpleados.Rows[fila].Cells[0].Value.ToString());
                b._nombreEmpleado = dgvEmpleados.Rows[fila].Cells[2].Value.ToString();
                b.Show();
            }
            else
            {
                MessageBox.Show("El trabajador actualmente en baja.", "Información");
            }
        }

        void b_OnBajaEmpleado(int baja)
        {
            _empleadoAltaBaja = baja;
            ListaEmpleados();
        }

        private void toolReingreso_Click(object sender, EventArgs e)
        {
            int fila = dgvEmpleados.CurrentCell.RowIndex;
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = int.Parse(dgvEmpleados.Rows[fila].Cells[0].Value.ToString());

            int estatus = 0;
            try
            {
                cnx.Open();
                estatus = (int)eh.obtenerEstatus(empleado);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener el estatus de trabajador.", "Error");
                cnx.Dispose();
                return;
            }

            if (estatus == 0)
            {
                frmReingresoEmpleado r = new frmReingresoEmpleado();
                r.OnReingreso += r_OnReingreso;
                r._idempleado = int.Parse(dgvEmpleados.Rows[fila].Cells[0].Value.ToString());
                r._nombreEmpleado = dgvEmpleados.Rows[fila].Cells[2].Value.ToString();
                r.Show();
            }
            else
            {
                MessageBox.Show("El trabajador no puede ser reingresado. Estatus: Alta", "Información");
            }
        }

        void r_OnReingreso(int edicion)
        {
            if (edicion == GLOBALES.NUEVO)
                ListaEmpleados();
        }

        private void toolExportar_Click(object sender, EventArgs e)
        {
            frmExportarEmpleado ee = new frmExportarEmpleado();
            ee.Show();
        }

        private void toolActualizar_Click(object sender, EventArgs e)
        {
            dgvEmpleados.DataSource = null;
            dgvEmpleados.RowHeadersVisible = false;
            ListaEmpleados();
        }

    }
}
