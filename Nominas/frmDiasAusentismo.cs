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

        public delegate void delOnCantidad(decimal cantidad);
        public event delOnCantidad OnCantidad;

        public delegate void delOnDespensa(decimal cantidad);
        public event delOnDespensa OnDespensa;

        public delegate void delOnSubsidio(decimal cantidad);
        public event delOnSubsidio OnSubsidio;

        public delegate void delOnIsr(decimal cantidad);
        public event delOnIsr OnIsr;

        public delegate void delOnInfonavit(decimal cantidad);
        public event delOnInfonavit OnInfonavit;

        #endregion

        private void frmDiasAusentismo_Load(object sender, EventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (OnDiasAusentismo != null)
                OnDiasAusentismo(0);
            if (OnCantidad != null)
                OnCantidad(0);
            if (OnDespensa != null)
                OnDespensa(0);
            if (OnSubsidio != null)
                OnSubsidio(0);
            if (OnIsr != null)
                OnIsr(0);
            if (OnInfonavit != null)
                OnInfonavit(0);
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (OnDiasAusentismo != null)
                OnDiasAusentismo(int.Parse(txtDias.Text));
            if (OnCantidad != null)
                OnCantidad(decimal.Parse(txtDias.Text));
            if (OnDespensa != null)
                OnDespensa(decimal.Parse(txtDias.Text));
            if (OnSubsidio != null)
                OnSubsidio(decimal.Parse(txtDias.Text));
            if (OnIsr != null)
                OnIsr(decimal.Parse(txtDias.Text));
            if (OnInfonavit != null)
                OnInfonavit(decimal.Parse(txtDias.Text));
            this.Dispose();
        }
    }
}
