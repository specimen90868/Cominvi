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
    public partial class frmListaVacaciones : Form
    {
        public frmListaVacaciones()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        List<Empleados.Core.Empleados> lstEmpleados;
        List<Vacaciones.Core.Vacaciones> lstVacaciones;
        Empleados.Core.EmpleadosHelper eh;
        Vacaciones.Core.VacacionesHelper vh;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoNomina;
        #endregion

        private void ListaVacaciones()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            eh = new Empleados.Core.EmpleadosHelper();
            vh = new Vacaciones.Core.VacacionesHelper();
            eh.Command = cmd;
            vh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;

            Vacaciones.Core.Vacaciones vacacion = new Vacaciones.Core.Vacaciones();
            vacacion.idempresa = GLOBALES.IDEMPRESA;

            if (_tipoNomina == GLOBALES.NORMAL)
                empleado.estatus = GLOBALES.ACTIVO;
            if (_tipoNomina == GLOBALES.ESPECIAL)
                empleado.estatus = GLOBALES.INACTIVO;

            try
            {
                cnx.Open();
                lstEmpleados = eh.obtenerEmpleados(empleado);
                lstVacaciones = vh.obtenerVacaciones(vacacion);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            var datos = from e in lstEmpleados
                        join v in lstVacaciones on e.idtrabajador equals v.idtrabajador
                        orderby e.nombrecompleto ascending
                        select new
                        {
                            Id = v.id,
                            IdTrabajador = e.idtrabajador,
                            NoEmpleado = e.noempleado,
                            Nombre = e.nombrecompleto,
                            DiasPagados = v.diasapagar,
                            DiasPendientes = v.diaspendientes,
                            Prima = (v.pvpagada == true ? "Pagada" : "N/A"),
                            Vacaciones = (v.pagada == true ? "Pagada" : "N/A"),
                            Total = v.total,
                            Fecha = v.fechapago
                        };
            dgvVacaciones.DataSource = datos.ToList();
            dgvVacaciones.Columns["Id"].Visible = false;
            dgvVacaciones.Columns["IdTrabajador"].Visible = false;

            for (int i = 0; i < dgvVacaciones.Columns.Count; i++)
            {
                dgvVacaciones.AutoResizeColumn(i);
            }

        }

        private void frmListaVacaciones_Load(object sender, EventArgs e)
        {
            ListaVacaciones();
            CargaPerfil();
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            frmVacaciones v = new frmVacaciones();
            v.OnVacacionNueva += v_OnVacacionNueva;
            v._tipoNomina = _tipoNomina;
            v._ventana = "Vacacion";
            v.Show();
        }

        void v_OnVacacionNueva()
        {
            ListaVacaciones();
        }

        private void toolEliminar_Click(object sender, EventArgs e)
        {
            int fila = 0;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            vh = new Vacaciones.Core.VacacionesHelper();
            vh.Command = cmd;

            fila = dgvVacaciones.CurrentCell.RowIndex;
            Vacaciones.Core.Vacaciones vacacion = new Vacaciones.Core.Vacaciones();
            vacacion.id = int.Parse(dgvVacaciones.Rows[fila].Cells[0].Value.ToString());

            try
            {
                cnx.Open();
                vh.eliminaVacacion(vacacion);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
            ListaVacaciones();
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Historial Vacaciones");

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
    }
}
