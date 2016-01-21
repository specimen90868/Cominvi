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

        public delegate void delOnCantidad(double cantidad);
        public event delOnCantidad OnCantidad;

        public delegate void delOnDespensa(double cantidad);
        public event delOnDespensa OnDespensa;

        public delegate void delOnSubsidio(double cantidad);
        public event delOnSubsidio OnSubsidio;

        public delegate void delOnIsr(double cantidad);
        public event delOnIsr OnIsr;
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
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (OnDiasAusentismo != null)
                OnDiasAusentismo(int.Parse(txtDias.Text));
            if (OnCantidad != null)
                OnCantidad(double.Parse(txtDias.Text));
            if (OnDespensa != null)
                OnDespensa(double.Parse(txtDias.Text));
            if (OnSubsidio != null)
                OnSubsidio(double.Parse(txtDias.Text));
            if (OnIsr != null)
                OnIsr(double.Parse(txtDias.Text));
            this.Dispose();
        }
    }
}
