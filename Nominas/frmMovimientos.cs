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
        public delegate void delOnMovimientoNuevo();
        public event delOnMovimientoNuevo OnMovimientoNuevo;
        #endregion

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Conceptos.Core.ConceptosHelper ch;
        Movimientos.Core.MovimientosHelper mh;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoNomina;
        public int _idEmpleado = 0;
        public string _nombreEmpleado = "";
        public int _periodo = 0;
        #endregion

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            mh = new Movimientos.Core.MovimientosHelper();
            mh.Command = cmd;

            Movimientos.Core.Movimientos mov = new Movimientos.Core.Movimientos();
            mov.idtrabajador = _idEmpleado;
            mov.idempresa = GLOBALES.IDEMPRESA;
            mov.idconcepto = int.Parse(cmbConcepto.SelectedValue.ToString());
            mov.cantidad = decimal.Parse(txtCantidad.Text.Trim());
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

        private void frmMovimientos_Load(object sender, EventArgs e)
        {
            rbtnDeducciones.Checked = true;
            cargaCombo();
            if (_idEmpleado != 0)
                lblEmpleado.Text = _nombreEmpleado;
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
                lstConceptos = ch.obtenerConceptosDeducciones(concepto, _periodo);
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
            if (_periodo == 7)
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
