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
    public partial class frmVacaciones : Form
    {
        public frmVacaciones()
        {
            InitializeComponent();
        }

        #region DELEGADOS
        public delegate void delOnVacacion(string noempleado, string nombre, string paterno, string materno, bool prima, bool pago, int diaspagopv, 
            bool vacacion, int diaspago, DateTime fechainicio, DateTime fechafin);
        public event delOnVacacion OnVacacion;
        #endregion

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Empleados.Core.EmpleadosHelper eh;
        Periodos.Core.PeriodosHelper ph;
        int periodo, idperiodo, idempleado = 0;
        List<Empleados.Core.Empleados> lstEmpleado;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoNomina;
        #endregion

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            if (idempleado != 0)
                if (OnVacacion != null)
                    OnVacacion(
                        lstEmpleado[0].noempleado, 
                        lstEmpleado[0].nombres, 
                        lstEmpleado[0].paterno, 
                        lstEmpleado[0].materno, 
                        chkPrimaVacacional.Checked,
                        chkPagoTotal.Checked,
                        int.Parse(txtDiasPagoPV.Text),
                        chkVacaciones.Checked, 
                        int.Parse(txtDiasPago.Text),
                        dtpInicio.Value,
                        dtpFin.Value);
        }

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar b = new frmBuscar();
            b.OnBuscar += b_OnBuscar;
            b._catalogo = GLOBALES.EMPLEADOS;
            b._tipoNomina = _tipoNomina;
            b.MdiParent = this.MdiParent;
            b.Show();
        }

        void b_OnBuscar(int id, string nombre)
        {
            idempleado = id;
            lblEmpleado.Text = nombre;

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            eh = new Empleados.Core.EmpleadosHelper();
            ph = new Periodos.Core.PeriodosHelper();
            eh.Command = cmd;
            ph.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = idempleado;

            Periodos.Core.Periodos per = new Periodos.Core.Periodos();
            per.idempresa = GLOBALES.IDEMPRESA;

            lstEmpleado = new List<Empleados.Core.Empleados>();
            List<Periodos.Core.Periodos> lstPeriodos = new List<Periodos.Core.Periodos>();

            try
            {
                cnx.Open();
                lstEmpleado = eh.obtenerEmpleado(empleado);
                lstPeriodos = ph.obtenerPeriodos(per);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            var datos = from e in lstEmpleado
                        join p in lstPeriodos on e.idperiodo equals p.idperiodo
                        select new
                        {
                            p.dias,
                            e.idperiodo
                        };
            foreach (var d in datos)
            {
                periodo = d.dias;
                idperiodo = d.idperiodo;
            }

            if (periodo == 7)
            {
                DateTime dt = DateTime.Now;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                dtpInicio.Value = dt;
                dtpFin.Value = dt.AddDays(6);
            }
            else
            {
                if (DateTime.Now.Day <= 15)
                {
                    dtpInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dtpFin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
                }
                else
                {
                    dtpInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 16);
                    dtpFin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                }

            }
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmVacaciones_Load(object sender, EventArgs e)
        {
            dtpFin.Enabled = false;
            txtDiasPago.Text = "0";
            txtDiasPagoPV.Text = "0";
        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            if (periodo == 7)
            {
                DateTime dt = dtpInicio.Value;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                dtpInicio.Value = dt;
                dtpFin.Value = dt.AddDays(6);
            }
            else
            {
                if (DateTime.Now.Day <= 15)
                {
                    dtpInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dtpFin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
                }
                else
                {
                    dtpInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 16);
                    dtpFin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                }
            }
        }

        private void chkVacaciones_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkVacaciones.Checked)
                txtDiasPago.Text = "0";
        }

        private void chkPrimaVacacional_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrimaVacacional.Checked)
            {
                chkPagoTotal.Checked = false;
                chkPagoTotal.Enabled = true;
                txtDiasPagoPV.Enabled = true;
            }
            else
            {
                chkPagoTotal.Checked = false;
                txtDiasPagoPV.Text = "0";
                chkPagoTotal.Enabled = false;
                txtDiasPagoPV.Enabled = false;
            }
        }

        private void chkPagoTotal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPagoTotal.Checked)
            {
                txtDiasPagoPV.Text = "0";
                txtDiasPagoPV.Enabled = false;
            }
            else
            {
                txtDiasPagoPV.Text = "0";
                txtDiasPagoPV.Enabled = true;
            }
        }
    }
}
