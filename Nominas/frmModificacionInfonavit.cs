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
    public partial class frmModificacionInfonavit : Form
    {
        public frmModificacionInfonavit()
        {
            InitializeComponent();
        }

        #region VARIABLES PUBLICAS
        public int _idEmpleado;
        public string _nombreEmpleado;
        public string _activo;
        #endregion

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Infonavit.Core.InfonavitHelper ih;
        Empleados.Core.EmpleadosHelper eh;
        Empresas.Core.EmpresasHelper ph;
        int movimiento, descuento;
        #endregion

        #region DELEGADOS
        public delegate void delOnInfonavit();
        public event delOnInfonavit OnInfonavit;
        #endregion

        private void frmModificacionInfonavit_Load(object sender, EventArgs e)
        {
            
            lblEmpleado.Text = _nombreEmpleado;
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            ih = new Infonavit.Core.InfonavitHelper();
            ih.Command = cmd;
            Infonavit.Core.Infonavit infonavit = new Infonavit.Core.Infonavit();
            infonavit.idtrabajador = _idEmpleado;

            List<Infonavit.Core.Infonavit> lstInfonavit = new List<Infonavit.Core.Infonavit>();

            try {
                cnx.Open();

                lstInfonavit = ih.obtenerInfonavit(infonavit);

                cnx.Close();
                cnx.Dispose();

                for (int i = 0; i < lstInfonavit.Count; i++)
                {
                    txtCredito.Text = lstInfonavit[i].credito;
                    txtValorDescuento.Text = lstInfonavit[i].valordescuento.ToString();
                    descuento = lstInfonavit[i].descuento;
                }

                switch (descuento)
                {
                    case 1:
                        rbtnPorcentaje.Checked = true;
                        break;
                    case 2:
                        rbtnFijoPesos.Checked = true;
                        break;
                    case 3:
                        rbtnVSM.Checked = true;
                        break;
                }
            }
            catch (Exception error) {
                MessageBox.Show("Error: \r\n \r\n" + error.Message,"Error");
            }
        }

        private void rbtnCredito_CheckedChanged(object sender, EventArgs e)
        {
            movimiento = GLOBALES.mCREDITO;
            grpCredito.Enabled = true;
            grpTipoDescuento.Enabled = false;
        }

        private void rbtnTipoDescuento_CheckedChanged(object sender, EventArgs e)
        {
            movimiento = GLOBALES.mTIPODESCUENTO;
            grpCredito.Enabled = false;
            grpTipoDescuento.Enabled = true;
        }

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            ih = new Infonavit.Core.InfonavitHelper();
            eh = new Empleados.Core.EmpleadosHelper();
            ph = new Empresas.Core.EmpresasHelper();
            ih.Command = cmd;
            eh.Command = cmd;
            ph.Command = cmd;

            Infonavit.Core.Infonavit infonavit = new Infonavit.Core.Infonavit();
            infonavit.idtrabajador = _idEmpleado;
            infonavit.credito = txtCredito.Text;
            infonavit.descuento = descuento;
            infonavit.valordescuento = double.Parse(txtValorDescuento.Text);

            Infonavit.Core.suaInfonavit sua = new Infonavit.Core.suaInfonavit();
            sua.idtrabajador = _idEmpleado;
            sua.idempresa = GLOBALES.IDEMPRESA;
            sua.credito = txtCredito.Text;
            sua.modificacion = movimiento;
            sua.fecha = dtpFecha.Value;
            sua.descuento = descuento;
            sua.valor = double.Parse(txtValorDescuento.Text);

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idEmpleado;

            Empresas.Core.Empresas empresa = new Empresas.Core.Empresas();
            empresa.idempresa = GLOBALES.IDEMPRESA;

            try {
                cnx.Open();

                ih.actualizaInfonavit(infonavit);

                sua.nss = (string)eh.obtenerNss(empleado);
                sua.registropatronal = (string)ph.obtenerRegistroPatronal(empresa);
                ih.insertarInfonavitSua(sua);

                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error) {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            if (OnInfonavit != null)
                OnInfonavit();
            this.Dispose();
        }

        private void rbtnPorcentaje_CheckedChanged(object sender, EventArgs e)
        {
            descuento = GLOBALES.dPORCENTAJE;
        }

        private void rbtnVSM_CheckedChanged(object sender, EventArgs e)
        {
            descuento = GLOBALES.dVSMDF;
        }

        private void rbtnFijoPesos_CheckedChanged(object sender, EventArgs e)
        {
            descuento = GLOBALES.dPESOS;
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
