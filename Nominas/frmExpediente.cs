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
    public partial class frmExpediente : Form
    {
        public frmExpediente()
        {
            InitializeComponent();
        }

        #region VARIABLES PUBLICAS
        public int _idEmpleado;
        public string _nombreEmpleado;
        public int _tipoOperacion;
        #endregion

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Expediente.Core.ExpedienteHelper eh;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoExpediente(int edicion);
        public event delOnNuevoExpediente OnNuevoExpediente;
        #endregion

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            //SE VALIDA SI TODOS LOS CAMPOS HAN SIDO LLENADOS.
            string control = GLOBALES.VALIDAR(this, typeof(TextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            if (_idEmpleado == 0)
            {
                MessageBox.Show("No se puede guardar no ha seleccionado al Empleado", "Error");
                return;
            }

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            eh = new Expediente.Core.ExpedienteHelper();
            eh.Command = cmd;

            Expediente.Core.Expediente ex = new Expediente.Core.Expediente();
            ex.idempresa = GLOBALES.IDEMPRESA;
            ex.contrato = chkContrato.Checked;
            ex.solicitud = chkSolicitud.Checked;
            ex.altaimss = chkImss.Checked;
            ex.credencial = chkCredencial.Checked;
            ex.actanacimiento = chkActa.Checked;
            ex.ife = chkIFE.Checked;
            ex.curp = chkCurp.Checked;
            ex.cdomicilio = chkCDomicilio.Checked;
            ex.nss = chkNss.Checked;
            ex.rfc = chkRfc.Checked;
            ex.infonavit = chkInfonavit.Checked;
            ex.afore = chkAfore.Checked;
            ex.fotografias = chkFotografias.Checked;
            ex.autorizacion = chkAutorizacion.Checked;
            ex.estatus = calcularEstatus();
            ex.observacion = txtObservaciones.Text;

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        ex.idtrabajador = _idEmpleado;
                        eh.insertaExpediente(ex);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar los datos. \r\n \r\n Error: " + error.Message);
                        this.Dispose();
                    }
                    break;
                case 2:
                    try
                    {
                        ex.idtrabajador = _idEmpleado;
                        cnx.Open();
                        eh.actualizaExpediente(ex);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar los datos. \r\n \r\n Error: " + error.Message);
                        this.Dispose();
                    }
                    break;
            }

            if (OnNuevoExpediente != null)
                OnNuevoExpediente(_tipoOperacion);
            this.Dispose();
        }

        private void frmExpediente_Load(object sender, EventArgs e)
        {
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                eh = new Expediente.Core.ExpedienteHelper();
                eh.Command = cmd;

                List<Expediente.Core.Expediente> lstExpediente;

                Expediente.Core.Expediente ex = new Expediente.Core.Expediente();
                ex.idtrabajador = _idEmpleado;

                try
                {
                    cnx.Open();
                    lstExpediente = eh.obtenerExpediente(ex);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstExpediente.Count; i++)
                    {
                        chkContrato.Checked = lstExpediente[i].contrato;
                        chkSolicitud.Checked = lstExpediente[i].solicitud;
                        chkImss.Checked = lstExpediente[i].altaimss;
                        chkCredencial.Checked = lstExpediente[i].credencial;
                        chkActa.Checked = lstExpediente[i].actanacimiento;
                        chkIFE.Checked = lstExpediente[i].ife;
                        chkCurp.Checked = lstExpediente[i].curp;
                        chkCDomicilio.Checked = lstExpediente[i].cdomicilio;
                        chkNss.Checked = lstExpediente[i].nss;
                        chkRfc.Checked = lstExpediente[i].rfc;
                        chkInfonavit.Checked = lstExpediente[i].infonavit;
                        chkAfore.Checked = lstExpediente[i].afore;
                        chkFotografias.Checked = lstExpediente[i].fotografias;
                        chkAutorizacion.Checked = lstExpediente[i].autorizacion;
                        txtObservaciones.Text = lstExpediente[i].observacion;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolVentana.Text = "Consulta del Expediente";
                    GLOBALES.INHABILITAR(this, typeof(CheckBox));
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                    toolGuardar.Enabled = false;
                    toolBuscar.Enabled = false;
                }
                else
                {
                    toolVentana.Text = "Edición del Expediente";
                    lblEmpleado.Text = _nombreEmpleado;
                    toolBuscar.Enabled = false;
                }
                
            }
        }

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar b = new frmBuscar();
            b._catalogo = GLOBALES.EMPLEADOS;
            b.OnBuscar += b_OnBuscar;
            b._busqueda = GLOBALES.FORMULARIOS;
            b.Show();
        }

        void b_OnBuscar(int id, string nombre)
        {
            _idEmpleado = id;
            lblEmpleado.Text = nombre;
        }

        private int calcularEstatus()
        {
            int total = 0;
            int contrato = (chkContrato.Checked) ? 1 : 0;
            int solicitud = (chkSolicitud.Checked) ? 1 : 0;
            int imss = (chkImss.Checked) ? 1 : 0;
            int credencial = (chkCredencial.Checked) ? 1 : 0;
            int acta = (chkActa.Checked) ? 1 : 0;
            int ife = (chkIFE.Checked) ? 1 : 0;
            int curp = (chkCurp.Checked) ? 1 : 0;
            int cdomicilio = (chkCDomicilio.Checked) ? 1 : 0;
            int nss = (chkNss.Checked) ? 1 : 0;
            int rfc = (chkRfc.Checked) ? 1 : 0;
            int infonavit = (chkInfonavit.Checked) ? 1 : 0;
            int afore = (chkAfore.Checked) ? 1 : 0;
            int fotografias = (chkFotografias.Checked) ? 1 : 0;
            int autorizacion = (chkAutorizacion.Checked) ? 1 : 0;
            return total = contrato+solicitud+imss+credencial+acta+ife+curp+cdomicilio+nss+rfc+infonavit+afore+fotografias+autorizacion;
        }
    }
}
