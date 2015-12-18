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
    public partial class frmMovimientos : Form
    {
        public frmMovimientos()
        {
            InitializeComponent();
        }

        #region DELEGADOS
        public delegate void delOnMovimiento(int idEmpleado, string concepto, double cantidad, DateTime inicio, DateTime fin);
        public event delOnMovimiento OnMovimiento;

        public delegate void delOnMovimientoNuevo();
        public event delOnMovimientoNuevo OnMovimientoNuevo;
        #endregion

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Empleados.Core.EmpleadosHelper eh;
        Conceptos.Core.ConceptosHelper ch;
        Periodos.Core.PeriodosHelper ph;
        Movimientos.Core.MovimientosHelper mh;
        int periodo, idperiodo, idempleado = 0;
        List<Empleados.Core.Empleados> lstEmpleado;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoNomina;
        public string _ventana;
        #endregion

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
                DateTime dt = dtpFechaInicio.Value.Date;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                dtpFechaInicio.Value = dt;
                dtpFechaFin.Value = dt.AddDays(6);
            }
            else
            {
                if (dtpFechaInicio.Value.Day <= 15)
                {
                    dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
                    dtpFechaFin.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 15);
                }
                else
                {
                    dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 16);
                    dtpFechaFin.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, DateTime.DaysInMonth(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month));
                }

            }
        }

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            if (_ventana == "Carga")
            {
                if (idempleado != 0)
                {
                    if (OnMovimiento != null)
                        OnMovimiento(idempleado, cmbConcepto.Text, double.Parse(txtCantidad.Text), dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date);
                }
            }
            else
            {
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                mh = new Movimientos.Core.MovimientosHelper();
                mh.Command = cmd;

                Movimientos.Core.Movimientos mov = new Movimientos.Core.Movimientos();
                mov.idtrabajador = idempleado;
                mov.idempresa = GLOBALES.IDEMPRESA;
                mov.idconcepto = int.Parse(cmbConcepto.SelectedValue.ToString());
                mov.cantidad = double.Parse(txtCantidad.Text.Trim());
                mov.fechainicio = dtpFechaInicio.Value.Date;
                mov.fechafin = dtpFechaFin.Value.Date;

                try
                {
                    cnx.Open();
                    mh.insertaMovimiento(mov);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Ingreso de movimiento. \r\n \r\n" + error.Message, "Error");
                    cnx.Dispose();
                }

                if (OnMovimientoNuevo != null)
                    OnMovimientoNuevo();
                
                this.Dispose();
            }
        }

        private void frmMovimientos_Load(object sender, EventArgs e)
        {
            rbtnDeducciones.Checked = true;
            cargaCombo();
        }

        private void cargaCombo()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            concepto.idempresa = GLOBALES.IDEMPRESA;

            if (rbtnDeducciones.Checked)
                concepto.tipoconcepto = "D";
            if (rbtnPercepcion.Checked)
                concepto.tipoconcepto = "P";

            List<Conceptos.Core.Conceptos> lstConceptos = new List<Conceptos.Core.Conceptos>();

            try
            {
                cnx.Open();
                lstConceptos = ch.obtenerConceptosDeducciones(concepto);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            cmbConcepto.DataSource = lstConceptos;
            cmbConcepto.DisplayMember = "concepto";
            cmbConcepto.ValueMember = "id";
        }

        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            if (periodo == 7)
            {
                DateTime dt = dtpFechaInicio.Value.Date;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                dtpFechaInicio.Value = dt;
                dtpFechaFin.Value = dt.AddDays(6);
            }
            else
            {
                if (dtpFechaInicio.Value.Day <= 15)
                {
                    dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
                    dtpFechaFin.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 15);
                }
                else
                {
                    dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 16);
                    dtpFechaFin.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, DateTime.DaysInMonth(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month));
                }
            }
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void rbtnDeducciones_CheckedChanged(object sender, EventArgs e)
        {
            cargaCombo();
        }

        private void rbtnPercepcion_CheckedChanged(object sender, EventArgs e)
        {
            cargaCombo();
        }
    }
}
