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
    public partial class frmCambioPeriodo : Form
    {
        public frmCambioPeriodo()
        {
            InitializeComponent();
        }

        #region DELEGADOS
        public delegate void delOnNuevoPeriodo(DateTime inicio, DateTime fin);
        public event delOnNuevoPeriodo OnNuevoPeriodo;
        #endregion

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        DateTime periodoInicio, periodoFin;
        #endregion

        #region VARIABLES PUBLICAS
        public int _periodo;
        public int _tipoNomina;
        #endregion

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (OnNuevoPeriodo != null)
                OnNuevoPeriodo(periodoInicio, periodoFin);
            this.Dispose();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (OnNuevoPeriodo != null)
                OnNuevoPeriodo(new DateTime(1900, 1, 1), new DateTime(1900, 1, 1));
            this.Dispose();
        }

        private void frmCambioPeriodo_Load(object sender, EventArgs e)
        {
            btnAceptar.Enabled = false;
        }

        private void dtpSeleccionaFecha_ValueChanged(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            int existe = 0;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            if (_tipoNomina != GLOBALES.EXTRAORDINARIO_NORMAL)
            {
                if (_periodo == 7)
                {
                    DateTime dt = dtpSeleccionaFecha.Value.Date;
                    while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);

                    dtpPeriodoInicio.Value = dt;
                    dtpPeriodoFin.Value = dt.AddDays(6);

                    periodoInicio = dtpPeriodoInicio.Value.Date;
                    periodoFin = dtpPeriodoFin.Value.Date;
                }
                else
                {
                    if (dtpSeleccionaFecha.Value.Day <= 15)
                    {
                        dtpPeriodoInicio.Value = new DateTime(dtpSeleccionaFecha.Value.Year, dtpSeleccionaFecha.Value.Month, 1);
                        dtpPeriodoFin.Value = new DateTime(dtpSeleccionaFecha.Value.Year, dtpSeleccionaFecha.Value.Month, 15);
                    }
                    else
                    {
                        dtpPeriodoInicio.Value = new DateTime(dtpSeleccionaFecha.Value.Year, dtpSeleccionaFecha.Value.Month, 16);
                        dtpPeriodoFin.Value = new DateTime(dtpSeleccionaFecha.Value.Year, dtpSeleccionaFecha.Value.Month, DateTime.DaysInMonth(dtpSeleccionaFecha.Value.Year, dtpSeleccionaFecha.Value.Month));
                    }
                    periodoInicio = dtpPeriodoInicio.Value.Date;
                    periodoFin = dtpPeriodoFin.Value.Date;
                }


                try
                {
                    cnx.Open();
                    existe = (int)nh.existeNomina(GLOBALES.IDEMPRESA, periodoInicio.Date, periodoFin.Date, _periodo);
                    cnx.Close();
                    if (existe != 0)
                    {
                        MessageBox.Show("NOMINA CALCULADA \r\n \r\n " +
                                            "El periodo seleccionado se encuenta: \r\n " +
                                            "Calculado y Autorizado.", "Información");
                        btnAceptar.Enabled = false;
                    }
                    else
                    {
                        btnAceptar.Enabled = true;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }
            }
            else
            {
                periodoInicio = dtpSeleccionaFecha.Value.Date;
                periodoFin = dtpSeleccionaFecha.Value.Date;
                btnAceptar.Enabled = true;
            }
        }
    }
}
