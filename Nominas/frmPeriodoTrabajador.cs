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
    public partial class frmPeriodoTrabajador : Form
    {
        public frmPeriodoTrabajador()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Empleados.Core.EmpleadosHelper eh;
        #endregion

        #region VARIABLES PUBLICAS
        public int _idEmpleado;
        public string _nombreEmpleado;
        #endregion

        private void frmPeriodo_Load(object sender, EventArgs e)
        {
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
           
            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
            dh.Command = cmd;

            List<Departamento.Core.Depto> lstDepartamento = new List<Departamento.Core.Depto>();
            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();
            List<Periodos.Core.Periodos> lstPeriodos = new List<Periodos.Core.Periodos>();

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idEmpleado;

            Departamento.Core.Depto dpto = new Departamento.Core.Depto();
            dpto.idempresa = GLOBALES.IDEMPRESA;

            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
            periodo.idempresa = GLOBALES.IDEMPRESA;

            try
            {
                cnx.Open();
                lstPeriodos = ph.obtenerPeriodos(periodo);
                lstEmpleado = eh.obtenerEmpleado(empleado);
                lstDepartamento = dh.obtenerDepartamentos(dpto);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al obtener los dias del periodo. \r\n \r\n La ventana se cerrará. \r\n \r\n" + error.Message, "Error");
                cnx.Dispose();
                this.Dispose();
            }

            cmbPeriodo.DataSource = lstPeriodos.ToList();
            cmbPeriodo.DisplayMember = "pago";
            cmbPeriodo.ValueMember = "idperiodo";

            var dato = from emp in lstEmpleado
                       join d in lstDepartamento on emp.iddepartamento equals d.id
                       select new
                       {
                           emp.noempleado,
                           emp.nombrecompleto,
                           d.descripcion,
                           emp.idperiodo
                       };
            foreach (var inf in dato)
            {
                mtxtNoEmpleado.Text = inf.noempleado;
                txtNombre.Text = inf.nombrecompleto;
                txtDepartamento.Text = inf.descripcion;
                cmbPeriodo.SelectedValue = inf.idperiodo;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            try
            {
                cnx.Open();
                ch.actualizaConceptoTrabajador(_idEmpleado, GLOBALES.IDEMPRESA, int.Parse(cmbPeriodo.SelectedValue.ToString()));
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al actualizar los conceptos.", "Error");
                cnx.Dispose();
            }

        }
    }
}
