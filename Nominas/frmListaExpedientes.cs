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
    public partial class frmListaExpedientes : Form
    {
        public frmListaExpedientes()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        List<Empleados.Core.Empleados> lstEmpleados;
        List<Expediente.Core.Expediente> lstExpediente;
        #endregion

        private void ListaEmpleados()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            Expediente.Core.ExpedienteHelper exph = new Expediente.Core.ExpedienteHelper();
            eh.Command = cmd;
            exph.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.estatus = GLOBALES.ACTIVO;

            Expediente.Core.Expediente expediente = new Expediente.Core.Expediente();
            expediente.idempresa = GLOBALES.IDEMPRESA;

            try
            {
                cnx.Open();
                lstEmpleados = eh.obtenerEmpleados(empleado);
                lstExpediente = exph.obtenerExpedientes(expediente);
                cnx.Close();
                cnx.Dispose();

                var em = from e in lstEmpleados join ex in lstExpediente on e.idtrabajador equals ex.idtrabajador
                         select new
                         {
                             IdTrabajador = e.idtrabajador,
                             NoEmpleado = e.noempleado,
                             Nombre = e.nombrecompleto,
                             Estatus = ((int)ex.estatus) == 0 ? "SIN EXPEDIENTE" :
                             ((int)ex.estatus > 0 && (int)ex.estatus < 14) ? "EXPEDIENTE SIN COMPLETAR" : "COMPLETO"
                         };

                dgvExpedientes.DataSource = em.ToList();

                for (int i = 0; i < dgvExpedientes.Columns.Count; i++)
                {
                    dgvExpedientes.AutoResizeColumn(i);
                }
                dgvExpedientes.Columns["IdTrabajador"].Visible = false;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Expedientes");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Crear":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Consultar": toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Editar": toolEditar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Eliminar": toolEliminar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;

                }
            }
        }

        private void Seleccion(int edicion)
        {
            int fila = 0;
            frmExpediente e = new frmExpediente();
            e.MdiParent = this.MdiParent;
            e.OnNuevoExpediente += e_OnNuevoExpediente;
            
            if (edicion != GLOBALES.NUEVO)
            {
                fila = dgvExpedientes.CurrentCell.RowIndex;
                e._idEmpleado = int.Parse(dgvExpedientes.Rows[fila].Cells[0].Value.ToString());
                e._nombreEmpleado = dgvExpedientes.Rows[fila].Cells[2].Value.ToString();
            }

            e._tipoOperacion = edicion;
            e.Show();
        }

        void e_OnNuevoExpediente(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaEmpleados();
        }

        private void frmListaExpedientes_Load(object sender, EventArgs e)
        {
            dgvExpedientes.RowHeadersVisible = false;
            ListaEmpleados();
            CargaPerfil();
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

        private void toolEliminar_Click(object sender, EventArgs e)
        {
            int fila = 0;
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Expediente.Core.ExpedienteHelper eh = new Expediente.Core.ExpedienteHelper();
            eh.Command = cmd;

            fila = dgvExpedientes.CurrentCell.RowIndex;
            Expediente.Core.Expediente exp = new Expediente.Core.Expediente();
            exp.idtrabajador = int.Parse(dgvExpedientes.Rows[fila].Cells[0].Value.ToString());

            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar el expediente?. \r\n \r\n CUIDADO. Esta acción eliminará permanentemente el expediente.", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    cnx.Open();
                    eh.eliminarExpediente(exp);
                    cnx.Close();
                    cnx.Dispose();
                    ListaEmpleados();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }
            }
        }

        private void dgvExpedientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = 0;
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Expediente.Core.ExpedienteHelper eh = new Expediente.Core.ExpedienteHelper();
            eh.Command = cmd;

            fila = dgvExpedientes.CurrentCell.RowIndex;
            Expediente.Core.Expediente exp = new Expediente.Core.Expediente();
            exp.idtrabajador = int.Parse(dgvExpedientes.Rows[fila].Cells[0].Value.ToString());

            try
            {
                cnx.Open();
                int existe = (int)eh.existeExpediente(exp);
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

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            txtBuscar.Font = new Font("Arial", 9);
            txtBuscar.ForeColor = System.Drawing.Color.Black;
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar empleado...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtBuscar.Text) || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    var em = from emp in lstEmpleados join ex in lstExpediente on emp.idtrabajador equals ex.idtrabajador
                             select new
                             {
                                 IdTrabajador = emp.idtrabajador,
                                 NoEmpleado = emp.noempleado,
                                 Nombre = emp.nombrecompleto,
                                 Estatus = ((int)ex.estatus) == 0 ? "SIN EXPEDIENTE" :
                                    ((int)ex.estatus > 0 && (int)ex.estatus < 14) ? "EXPEDIENTE SIN COMPLETAR" : "COMPLETO"
                             };
                    dgvExpedientes.DataSource = em.ToList();
                }
                else
                {
                    var busqueda = from b in lstEmpleados
                                   join ex in lstExpediente on b.idtrabajador equals ex.idtrabajador
                                   where b.nombrecompleto.Contains(txtBuscar.Text.ToUpper()) || b.noempleado.Contains(txtBuscar.Text)
                                   select new
                                   {
                                       IdTrabajador = b.idtrabajador,
                                       NoEmpleado = b.noempleado,
                                       Nombre = b.nombrecompleto,
                                       Estatus = ((int)ex.estatus) == 0 ? "SIN EXPEDIENTE" :
                                        ((int)ex.estatus > 0 && (int)ex.estatus < 14) ? "EXPEDIENTE SIN COMPLETAR" : "COMPLETO"
                                   };
                    dgvExpedientes.DataSource = busqueda.ToList();
                }
                dgvExpedientes.Columns["IdTrabajador"].Visible = false;
            }
        }

    }
}
