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
        List<Incidencias.Core.Incidencias> lstIncidencias;
        Empleados.Core.EmpleadosHelper eh;
        Incidencias.Core.IncidenciasHelper ih;
        #endregion

        private void ListaIncapacidad()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            eh = new Empleados.Core.EmpleadosHelper();
            ih = new Incidencias.Core.IncidenciasHelper();
            eh.Command = cmd;
            ih.Command = cmd;

            Incidencias.Core.Incidencias incidencia = new Incidencias.Core.Incidencias();
            incidencia.idempresa = GLOBALES.IDEMPRESA;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.estatus = GLOBALES.ACTIVO;

            try
            {
                cnx.Open();
                lstEmpleados = eh.obtenerEmpleados(empleado);
                lstIncidencias = ih.obtenerIndicencias(incidencia);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            var datos = from e in lstEmpleados
                        join i in lstIncidencias on e.idtrabajador equals i.idtrabajador
                        orderby e.nombrecompleto ascending 
                        select new
                        {
                            IdTrabajador = e.idtrabajador,
                            NoEmpleado = e.noempleado,
                            Nombre = e.nombrecompleto,
                            Certificado = i.certificado,
                            PeriodoInicio = i.periodoinicio,
                            PeriodoFin = i.periodofin,
                            Dias = i.dias,
                            InicioIncapacidad = i.inicioincapacidad,
                            FinIncapacidad = i.finincapacidad
                        };

            dgvIncapacidad.DataSource = datos.ToList();

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
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Crear":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Eliminar": toolEliminar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
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

        void i_OnIncapacidad()
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
                                join i in lstIncidencias on emp.idtrabajador equals i.idtrabajador
                                orderby emp.nombrecompleto ascending
                                select new
                                {
                                    IdTrabajador = emp.idtrabajador,
                                    NoEmpleado = emp.noempleado,
                                    Nombre = emp.nombrecompleto,
                                    Certificado = i.certificado,
                                    PeriodoInicio = i.periodoinicio,
                                    PeriodoFin = i.periodofin,
                                    Dias = i.dias,
                                    InicioIncapacidad = i.inicioincapacidad,
                                    FinIncapacidad = i.finincapacidad
                                };
                    dgvIncapacidad.DataSource = datos.ToList();
                }
                else
                {
                    var busqueda = from be in lstEmpleados
                                   join bi in lstIncidencias on be.idtrabajador equals bi.idtrabajador
                                   where be.nombrecompleto.Contains(txtBuscar.Text.ToUpper()) || be.noempleado.Contains(txtBuscar.Text)
                                   orderby be.nombrecompleto ascending
                                   select new
                                   {
                                       IdTrabajador = be.idtrabajador,
                                       NoEmpleado = be.noempleado,
                                       Nombre = be.nombrecompleto,
                                       Certificado = bi.certificado,
                                       PeriodoInicio = bi.periodoinicio,
                                       PeriodoFin = bi.periodofin,
                                       Dias = bi.dias,
                                       InicioIncapacidad = bi.inicioincapacidad,
                                       FinIncapacidad = bi.finincapacidad
                                   };
                    dgvIncapacidad.DataSource = busqueda.ToList();
                }
                
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
            CargaPerfil();
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.NUEVO);
        }

        private void toolEliminar_Click(object sender, EventArgs e)
        {
            int fila = 0;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ih = new Incidencias.Core.IncidenciasHelper();
            ih.Command = cmd;

            fila = dgvIncapacidad.CurrentCell.RowIndex;
            Incidencias.Core.Incidencias incidencia = new Incidencias.Core.Incidencias();
            incidencia.idtrabajador = int.Parse(dgvIncapacidad.Rows[fila].Cells[0].Value.ToString());
            incidencia.certificado = dgvIncapacidad.Rows[fila].Cells[3].Value.ToString();

            try
            {
                cnx.Open();
                ih.eliminaIncidencia(incidencia);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            ListaIncapacidad();
        }
    }
}
