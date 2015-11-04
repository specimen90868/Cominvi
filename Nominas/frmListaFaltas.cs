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
    public partial class frmListaFaltas : Form
    {
        public frmListaFaltas()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        List<Empleados.Core.Empleados> lstEmpleados;
        List<Faltas.Core.Faltas> lstFaltas;
        Empleados.Core.EmpleadosHelper eh;
        Faltas.Core.FaltasHelper fh;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoNomina;
        #endregion

        private void ListaFaltas()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            eh = new Empleados.Core.EmpleadosHelper();
            fh = new Faltas.Core.FaltasHelper();
            eh.Command = cmd;
            fh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;

            if(_tipoNomina == GLOBALES.NORMAL)
                empleado.estatus = GLOBALES.ACTIVO;
            if(_tipoNomina == GLOBALES.ESPECIAL)
                empleado.estatus = GLOBALES.INACTIVO;

            Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
            falta.idempresa = GLOBALES.IDEMPRESA;

            try {
                cnx.Open();
                lstEmpleados = eh.obtenerEmpleados(empleado);
                lstFaltas = fh.obtenerFaltas(falta);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message,"Error");
            }

            var datos = from e in lstEmpleados
                        join f in lstFaltas on e.idtrabajador equals f.idtrabajador
                        orderby e.nombrecompleto ascending
                        select new
                        {
                            Id = f.id,
                            IdTrabajador = e.idtrabajador,
                            NoEmpleado = e.noempleado,
                            Nombre = e.nombrecompleto,
                            NoFaltas = f.faltas,
                            FechaInicio = f.fechainicio,
                            FechaFin = f.fechafin
                        };
            dgvFaltas.DataSource = datos.ToList();
            dgvFaltas.Columns["Id"].Visible = false;
            dgvFaltas.Columns["IdTrabajador"].Visible = false;

            for (int i = 0; i < dgvFaltas.Columns.Count; i++)
            {
                dgvFaltas.AutoResizeColumn(i);
            }

        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Faltas");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].nombre.ToString())
                {
                    case "Faltas":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].crear);
                        break;
                }
            }
        }

        private void Seleccion(int edicion)
        {
            int fila = 0;
            frmFaltas f = new frmFaltas();
            f.MdiParent = this.MdiParent;
            f.OnFaltas += f_OnFaltas;

            if (edicion != GLOBALES.NUEVO)
            {
                fila = dgvFaltas.CurrentCell.RowIndex;
                f._idFalta = int.Parse(dgvFaltas.Rows[fila].Cells[0].Value.ToString());
                f._idEmpleado = int.Parse(dgvFaltas.Rows[fila].Cells[1].Value.ToString());
                f._nombreEmpleado = dgvFaltas.Rows[fila].Cells[3].Value.ToString();
            }

            f._tipoOperacion = edicion;
            f._tipoForma = 0; //NUEVA FALTA. SE AGREGA DIRECTAMENTE A LA BASE.
            f.Show();
        }

        void f_OnFaltas(string noempleado, string nombre, string paterno, string materno, int faltas, DateTime fechainicio, DateTime fechafin)
        {
            ListaFaltas();
        }

        private void frmListaFaltas_Load(object sender, EventArgs e)
        {
            dgvFaltas.RowHeadersVisible = false;
            ListaFaltas();
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.NUEVO);
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
                                join f in lstFaltas on emp.idtrabajador equals f.idtrabajador
                                orderby emp.nombrecompleto ascending
                                select new
                                {
                                    Id = f.id,
                                    IdTrabajador = emp.idtrabajador,
                                    NoEmpleado = emp.noempleado,
                                    Nombre = emp.nombrecompleto,
                                    NoFaltas = f.faltas,
                                    FechaInicio = f.fechainicio,
                                    FechaFin = f.fechafin
                                };
                    dgvFaltas.DataSource = datos.ToList();
                }
                else
                {
                    var busqueda = from be in lstEmpleados
                                   join bf in lstFaltas on be.idtrabajador equals bf.idtrabajador
                                   where be.nombrecompleto.Contains(txtBuscar.Text)
                                   orderby be.nombrecompleto ascending
                                   select new
                                   {
                                       Id = bf.id,
                                       IdTrabajador = be.idtrabajador,
                                       NoEmpleado = be.noempleado,
                                       Nombre = be.nombrecompleto,
                                       NoFaltas = bf.faltas,
                                       FechaInicio = bf.fechainicio,
                                       FechaFin = bf.fechafin
                                   };
                    dgvFaltas.DataSource = busqueda.ToList();
                }
                dgvFaltas.Columns["Id"].Visible = false;
                dgvFaltas.Columns["IdTrabajador"].Visible = false;
                for (int i = 0; i < dgvFaltas.Columns.Count; i++)
                {
                    dgvFaltas.AutoResizeColumn(i);
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar empleado...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void toolEliminar_Click(object sender, EventArgs e)
        {
            int fila = 0;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            fh = new Faltas.Core.FaltasHelper();
            fh.Command = cmd;

            fila = dgvFaltas.CurrentCell.RowIndex;
            Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
            falta.id = int.Parse(dgvFaltas.Rows[fila].Cells[0].Value.ToString());

            try
            {
                cnx.Open();
                fh.eliminaFalta(falta);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error) 
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
            ListaFaltas();
        }
    }
}
