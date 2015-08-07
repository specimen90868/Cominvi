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
    public partial class frmDiasAusentismo : Form
    {
        public frmDiasAusentismo()
        {
            InitializeComponent();
        }

        #region DELEGADOS
        public delegate void delOnDiasAusentismo(int dias);
        public event delOnDiasAusentismo OnDiasAusentismo;
        #endregion

        private void frmDiasAusentismo_Load(object sender, EventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (OnDiasAusentismo != null)
                OnDiasAusentismo(0);
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (OnDiasAusentismo != null)
                OnDiasAusentismo(int.Parse(txtDias.Text));
            this.Dispose();
        }
    }
}
