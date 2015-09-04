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
    public partial class frmListaIncapacidad : Form
    {
        public frmListaIncapacidad()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        List<Empleados.Core.Empleados> lstEmpleados;
        List<Incapacidad.Core.Incapacidades> lstIncapacidades;
        Empleados.Core.EmpleadosHelper eh;
        Incapacidad.Core.IncapacidadHelper ih;
        #endregion

        private void ListaIncapacidad()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            eh = new Empleados.Core.EmpleadosHelper();
            ih = new Incapacidad.Core.IncapacidadHelper();
            eh.Command = cmd;
            ih.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.estatus = GLOBALES.ACTIVO;

            Incapacidad.Core.Incapacidades incapacidad = new Incapacidad.Core.Incapacidades();
            incapacidad.idempresa = GLOBALES.IDEMPRESA;

            try
            {
                cnx.Open();
                lstEmpleados = eh.obtenerEmpleados(empleado);
                lstIncapacidades = ih.obtenerIncapacidades(incapacidad);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            var datos = from e in lstEmpleados
                        join i in lstIncapacidades on e.idtrabajador equals i.idtrabajador
                        orderby e.nombrecompleto ascending
                        select new
                        {
                            Id = i.id,
                            IdTrabajador = e.idtrabajador,
                            NoEmpleado = e.noempleado,
                            Nombre = e.nombrecompleto,
                            DiasIncapacidad = i.diasincapacidad,
                            DiasTomados = i.diastomados,
                            DiasRestantes = i.diasrestantes,
                            FechaInicio = i.fechainicio,
                            FechaFin = i.fechafin
                        };
            dgvIncapacidad.DataSource = datos.ToList();
            dgvIncapacidad.Columns["Id"].Visible = false;
            dgvIncapacidad.Columns["IdTrabajador"].Visible = false;

            for (int i = 0; i < dgvIncapacidad.Columns.Count; i++)
            {
                dgvIncapacidad.AutoResizeColumn(i);
            }

        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Incapacidades");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].nombre.ToString())
                {
                    case "Incapacidades":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].crear);
                        break;
                }
            }
        }

        private void Seleccion(int edicion)
        {
            int fila = 0;
            frmIncapacidad i = new frmIncapacidad();
            i.MdiParent = this.MdiParent;
            i.OnIncapacidad += i_OnIncapacidad;

            if (edicion != GLOBALES.NUEVO)
            {
                fila = dgvIncapacidad.CurrentCell.RowIndex;
                i._idIncapacidad = int.Parse(dgvIncapacidad.Rows[fila].Cells[0].Value.ToString());
                i._idEmpleado = int.Parse(dgvIncapacidad.Rows[fila].Cells[1].Value.ToString());
                i._nombreEmpleado = dgvIncapacidad.Rows[fila].Cells[3].Value.ToString();
            }

            i._tipoOperacion = edicion;
            i._tipoForma = 0; //NUEVA FALTA. SE AGREGA DIRECTAMENTE A LA BASE.
            i.Show();
        }

        void i_OnIncapacidad(string noempleado, string nombre, string paterno, string materno, int dias, DateTime fechainicio, DateTime inicio, DateTime fin)
        {
            ListaIncapacidad();
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
                    var datos = from emp in lstEmpleados
                                join i in lstIncapacidades on emp.idtrabajador equals i.idtrabajador
                                orderby emp.nombrecompleto ascending
                                select new
                                {
                                    Id = i.id,
                                    IdTrabajador = emp.idtrabajador,
                                    NoEmpleado = emp.noempleado,
                                    Nombre = emp.nombrecompleto,
                                    DiasIncapacidad = i.diasincapacidad,
                                    DiasTomados = i.diastomados,
                                    DiasRestantes = i.diasrestantes,
                                    FechaInicio = i.fechainicio,
                                    FechaFin = i.fechafin
                                };
                    dgvIncapacidad.DataSource = datos.ToList();
                }
                else
                {
                    var busqueda = from be in lstEmpleados
                                   join bi in lstIncapacidades on be.idtrabajador equals bi.idtrabajador
                                   where be.nombrecompleto.Contains(txtBuscar.Text)
                                   orderby be.nombrecompleto ascending
                                   select new
                                   {
                                       Id = bi.id,
                                       IdTrabajador = be.idtrabajador,
                                       NoEmpleado = be.noempleado,
                                       Nombre = be.nombrecompleto,
                                       DiasIncapacidad = bi.diasincapacidad,
                                       DiasTomados = bi.diastomados,
                                       DiasRestantes = bi.diasrestantes,
                                       FechaInicio = bi.fechainicio,
                                       FechaFin = bi.fechafin
                                   };
                    dgvIncapacidad.DataSource = busqueda.ToList();
                }
                dgvIncapacidad.Columns["Id"].Visible = false;
                dgvIncapacidad.Columns["IdTrabajador"].Visible = false;
                for (int i = 0; i < dgvIncapacidad.Columns.Count; i++)
                {
                    dgvIncapacidad.AutoResizeColumn(i);
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar empleado...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void frmListaIncapacidad_Load(object sender, EventArgs e)
        {
            dgvIncapacidad.RowHeadersVisible = false;
            ListaIncapacidad();
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.NUEVO);
        }
    }
}
