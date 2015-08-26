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
    public partial class frmFiltro : Form
    {
        public frmFiltro()
        {
            InitializeComponent();
        }

        #region DELEGADOS
        public delegate void delOnFecha(DateTime desde, DateTime hasta);
        public event delOnFecha OnFecha;
        #endregion 

        private void toolAceptar_Click(object sender, EventArgs e)
        {
            DateTime desde = dtpDesde.Value;
            DateTime hasta = dtpHasta.Value;
            if (OnFecha != null)
            {
                OnFecha(desde, hasta);
            }
        }

        private void toolCancelar_Click(object sender, EventArgs e)
        {
            if (OnFecha != null)
            {
                OnFecha(DateTime.Now, DateTime.Now);
            }
            this.Dispose();
        }
    }
}
