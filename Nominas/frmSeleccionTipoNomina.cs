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
    public partial class frmSeleccionTipoNomina : Form
    {
        public frmSeleccionTipoNomina()
        {
            InitializeComponent();
        }

        #region VARIABLES PUBLICAS
        public string _ventana;
        #endregion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            switch (_ventana)
            {
                case "HistorialFaltas": 
                    frmListaFaltas lf = new frmListaFaltas();
                    lf.MdiParent = this.MdiParent;

                    if (rbtnNormal.Checked)
                        lf._tipoNomina = GLOBALES.NORMAL;

                    if (rbtnEspecial.Checked)
                        lf._tipoNomina = GLOBALES.ESPECIAL;

                    lf.Show();
                    this.Dispose();
                    break;
            }
        }
    }
}
