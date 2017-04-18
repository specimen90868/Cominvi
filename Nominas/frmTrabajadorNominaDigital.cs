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
    public partial class frmTrabajadorNominaDigital : Form
    {
        public frmTrabajadorNominaDigital()
        {
            InitializeComponent();
        }

        #region VARIABLES PUBLICAS
        public int _idEmpleado;
        #endregion

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        #endregion

        private void frmTrabajadorNominaDigital_Load(object sender, EventArgs e)
        {
            CargaCombox();

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idEmpleado;

            Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
            dh.Command = cmd;

            Departamento.Core.Depto depto = new Departamento.Core.Depto();
            depto.idempresa = GLOBALES.IDEMPRESA;

            satTrabajadorNomina.Core.satTrabajadorNominaHelper tnh = new satTrabajadorNomina.Core.satTrabajadorNominaHelper();
            tnh.Command = cmd;

            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();
            List<Departamento.Core.Depto> lstDeptos = new List<Departamento.Core.Depto>();
            List<satTrabajadorNomina.Core.satTrabajadorNomina> lstNominaDigital = new List<satTrabajadorNomina.Core.satTrabajadorNomina>();
            
            try
            {
                cnx.Open();
                lstEmpleado = eh.obtenerEmpleado(empleado);
                lstDeptos = dh.obtenerDepartamentos(depto);
                lstNominaDigital = tnh.obtenerTrabajadorNomina(_idEmpleado);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener los datos del trabajador", "Error");
                cnx.Dispose();
            }

            var dato = from emp in lstEmpleado join d in lstDeptos on emp.iddepartamento equals d.id select new { 
                emp.noempleado, emp.nombrecompleto, d.descripcion
            };

            foreach (var d in dato)
            {
                mtxtNoEmpleado.Text = d.noempleado;
                lblEmpleado.Text = d.nombrecompleto;
                txtDepartamento.Text = d.descripcion;
            }

            cmbEstado.SelectedValue = lstNominaDigital[0].idestado;
            cmbMetodoPago.SelectedValue = lstNominaDigital[0].idmetodopago;
            cmbRiesgoPuesto.SelectedValue = lstNominaDigital[0].idriesgopuesto;
            cmbTipoContrato.SelectedValue = lstNominaDigital[0].idtipocontrato;
            cmbTipoRegimen.SelectedValue = lstNominaDigital[0].idtiporegimen;
        }

        private void CargaCombox()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            SatCatalogos.Core.satCatalogosHelper ch = new SatCatalogos.Core.satCatalogosHelper();
            ch.Command = cmd;

            List<SatCatalogos.Core.satTipoRegimen> lstTipoRegimen = new List<SatCatalogos.Core.satTipoRegimen>();
            List<SatCatalogos.Core.satMetodoPago> lstMetodoPago = new List<SatCatalogos.Core.satMetodoPago>();
            List<SatCatalogos.Core.satTipoContrato> lstTipoContrato = new List<SatCatalogos.Core.satTipoContrato>();
            List<SatCatalogos.Core.satEstados> lstEstado = new List<SatCatalogos.Core.satEstados>();
            List<SatCatalogos.Core.satRiesgoPuesto> lstRiesgoPuesto = new List<SatCatalogos.Core.satRiesgoPuesto>();

            try
            {
                cnx.Open();
                lstTipoRegimen = ch.obtenerTipoRegimen();
                lstMetodoPago = ch.obtenerMetodoPago();
                lstTipoContrato = ch.obtenerTipoContrato();
                lstEstado = ch.obtenerEstados();
                lstRiesgoPuesto = ch.obtenerRiesgoPuesto();
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener los catalogos. Se cerrará la ventana.", "Error");
                cnx.Dispose();
                this.Dispose();
            }

            cmbTipoRegimen.DataSource = lstTipoRegimen.ToList();
            cmbTipoRegimen.DisplayMember = "descripcion";
            cmbTipoRegimen.ValueMember = "idregimen";

            cmbMetodoPago.DataSource = lstMetodoPago.ToList();
            cmbMetodoPago.DisplayMember = "descripcion";
            cmbMetodoPago.ValueMember = "idmetodopago";

            cmbTipoContrato.DataSource = lstTipoContrato.ToList();
            cmbTipoContrato.DisplayMember = "descripcion";
            cmbTipoContrato.ValueMember = "id";

            cmbEstado.DataSource = lstEstado.ToList();
            cmbEstado.DisplayMember = "estado";
            cmbEstado.ValueMember = "idestado";

            cmbRiesgoPuesto.DataSource = lstRiesgoPuesto.ToList();
            cmbRiesgoPuesto.DisplayMember = "descripcion";
            cmbRiesgoPuesto.ValueMember = "id";
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            satTrabajadorNomina.Core.satTrabajadorNominaHelper stnh = new satTrabajadorNomina.Core.satTrabajadorNominaHelper();
            stnh.Command = cmd;

            satTrabajadorNomina.Core.satTrabajadorNomina nomina = new satTrabajadorNomina.Core.satTrabajadorNomina();
            nomina.idtrabajador = _idEmpleado;
            nomina.idempresa = GLOBALES.IDEMPRESA;
            nomina.idriesgopuesto = int.Parse(cmbRiesgoPuesto.SelectedValue.ToString());
            nomina.idtipocontrato = int.Parse(cmbTipoContrato.SelectedValue.ToString());
            nomina.idestado = int.Parse(cmbEstado.SelectedValue.ToString());
            nomina.idtiporegimen = int.Parse(cmbTipoRegimen.SelectedValue.ToString());
            nomina.idmetodopago = int.Parse(cmbMetodoPago.SelectedValue.ToString());

            try
            {
                cnx.Open();
                stnh.actualizaTrabajadorNomina(nomina);
                cnx.Close();
                cnx.Dispose();
                this.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al actualizar la nomina digital.","Error");
                cnx.Dispose();
            }
        }
    }
}
