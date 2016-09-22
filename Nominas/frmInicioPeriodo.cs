using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nominas
{
    public partial class frmInicioPeriodo : Form
    {
        public frmInicioPeriodo()
        {
            InitializeComponent();
        }

        public int _periodo;

        public delegate void delOnNuevoPeriodo(DateTime inicio, DateTime fin);
        public event delOnNuevoPeriodo OnNuevoPeriodo;

        private void frmInicioPeriodo_Load(object sender, EventArgs e)
        {
            if (_periodo == 7)
            {
                DateTime dt = DateTime.Now.Date;
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (OnNuevoPeriodo != null)
            {
                OnNuevoPeriodo(dtpInicio.Value.Date, dtpFin.Value.Date);
                this.Dispose();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (OnNuevoPeriodo != null)
            {
                OnNuevoPeriodo(new DateTime(1900,1,1), new DateTime(1900,1,1));
                this.Dispose();
            }
        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            if (_periodo == 7)
            {
                DateTime dt = dtpInicio.Value.Date;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                dtpInicio.Value = dt;
                dtpFin.Value = dt.AddDays(6);
            }
            else
            {
                if (dtpInicio.Value.Day <= 15)
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
    }
}
