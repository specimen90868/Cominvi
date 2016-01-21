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
    public partial class frmReingresoEmpleado : Form
    {
        public frmReingresoEmpleado()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        string sexo;
        #endregion

        #region DELEGADOS
        public delegate void delOnReingreso(int edicion);
        public event delOnReingreso OnReingreso;
        #endregion

        #region VARIABLES PUBLICAS
        public int _idempleado;
        public string _nombreEmpleado;
        #endregion

        private void frmReingresoEmpleado_Load(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Empresas.Core.EmpresasHelper eh = new Empresas.Core.EmpresasHelper();
            Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
            Puestos.Core.PuestosHelper ph = new Puestos.Core.PuestosHelper();
            Periodos.Core.PeriodosHelper periodoh = new Periodos.Core.PeriodosHelper();
            Factores.Core.FactoresHelper fh = new Factores.Core.FactoresHelper();
            Estados.Core.EstadosHelper edoh = new Estados.Core.EstadosHelper();

            eh.Command = cmd;
            dh.Command = cmd;
            ph.Command = cmd;
            periodoh.Command = cmd;
            fh.Command = cmd;
            edoh.Command = cmd;

            Empresas.Core.Empresas empresa = new Empresas.Core.Empresas();
            Departamento.Core.Depto depto = new Departamento.Core.Depto();
            Puestos.Core.Puestos puesto = new Puestos.Core.Puestos();
            Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
            Factores.Core.Factores factor = new Factores.Core.Factores();
            
            depto.idempresa = GLOBALES.IDEMPRESA;
            puesto.idempresa = GLOBALES.IDEMPRESA;
            periodo.idempresa = GLOBALES.IDEMPRESA;

            List<Empresas.Core.Empresas> lstEmpresa = new List<Empresas.Core.Empresas>();
            List<Departamento.Core.Depto> lstDepto = new List<Departamento.Core.Depto>();
            List<Puestos.Core.Puestos> lstPuesto = new List<Puestos.Core.Puestos>();
            List<Periodos.Core.Periodos> lstPeriodo = new List<Periodos.Core.Periodos>();
            List<Estados.Core.Estados> lstEstados = new List<Estados.Core.Estados>();

            try {
                cnx.Open();
                lstEmpresa = eh.obtenerEmpresas();
                lstDepto = dh.obtenerDepartamentos(depto);
                lstPuesto = ph.obtenerPuestos(puesto);
                lstPeriodo = periodoh.obtenerPeriodos(periodo);
                lstEstados = edoh.obtenerEstados();
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error) 
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            cmbRegistroPatronal.DataSource = lstEmpresa.ToList();
            cmbRegistroPatronal.DisplayMember = "nombre";
            cmbRegistroPatronal.ValueMember = "idempresa";

            cmbDepartamento.DataSource = lstDepto.ToList();
            cmbDepartamento.DisplayMember = "descripcion";
            cmbDepartamento.ValueMember = "id";

            cmbPuesto.DataSource = lstPuesto.ToList();
            cmbPuesto.DisplayMember = "nombre";
            cmbPuesto.ValueMember = "id";

            cmbPeriodo.DataSource = lstPeriodo.ToList();
            cmbPeriodo.DisplayMember = "pago";
            cmbPeriodo.ValueMember = "idperiodo";

            cmbEstados.DataSource = lstEstados.ToList();
            cmbEstados.DisplayMember = "nombre";
            cmbEstados.ValueMember = "idestado";

            lblNombreEmpleado.Text = _nombreEmpleado;
            rbtnHombre.Checked = true;
        }

        private int ObtieneEdad(DateTime fecha)
        {
            DateTime fechaNacimiento = fecha;
            int edad = (DateTime.Now.Subtract(fechaNacimiento).Days / 365);
            return edad;
        }

        private void dtpFechaReingreso_Leave(object sender, EventArgs e)
        {
            txtAntiguedad.Text = ObtieneEdad(dtpFechaReingreso.Value).ToString();
        }

        private void dtpFechaAntiguedad_Leave(object sender, EventArgs e)
        {
            txtAntiguedadMod.Text = ObtieneEdad(dtpFechaAntiguedad.Value).ToString();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (txtAntiguedadMod.Text.Length == 0)
                return;

            if (txtSDI.Text.Length != 0)
            {
                int DiasDePago = 0;
                double FactorDePago = 0;
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                Factores.Core.FactoresHelper fh = new Factores.Core.FactoresHelper();
                Factores.Core.Factores f = new Factores.Core.Factores();

                ph.Command = cmd;
                fh.Command = cmd;

                p.idperiodo = int.Parse(cmbPeriodo.SelectedValue.ToString());
                f.anio = int.Parse(txtAntiguedadMod.Text);

                try
                {
                    cnx.Open();
                    DiasDePago = (int)ph.DiasDePago(p);
                    FactorDePago = double.Parse(fh.FactorDePago(f).ToString());
                    cnx.Close();
                    cnx.Dispose();

                    //txtSalarioDiario.Text = (double.Parse(txtSueldo.Text) / DiasDePago).ToString("F6");
                    //txtSDI.Text = (double.Parse(txtSalarioDiario.Text) * FactorDePago).ToString("F6");
                    txtSalarioDiario.Text = (double.Parse(txtSDI.Text) / FactorDePago).ToString("F6");
                    txtSueldo.Text = (double.Parse(txtSalarioDiario.Text) * DiasDePago).ToString("F6");

                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    this.Dispose();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string rp;
            //SE VALIDA SI TODOS LOS CAMPOS HAN SIDO LLENADOS.
            string control = GLOBALES.VALIDAR(this, typeof(TextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            control = GLOBALES.VALIDAR(this, typeof(MaskedTextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Empleados.Core.EmpleadosHelper empleadoh = new Empleados.Core.EmpleadosHelper();
            Historial.Core.HistorialHelper hh = new Historial.Core.HistorialHelper();
            Reingreso.Core.ReingresoHelper rh = new Reingreso.Core.ReingresoHelper();
            Altas.Core.AltasHelper ah = new Altas.Core.AltasHelper();
            Empresas.Core.EmpresasHelper eh = new Empresas.Core.EmpresasHelper();
            Direccion.Core.DireccionesHelper dh = new Direccion.Core.DireccionesHelper();
            Complementos.Core.ComplementoHelper ch = new Complementos.Core.ComplementoHelper();

            empleadoh.Command = cmd;
            hh.Command = cmd;
            rh.Command = cmd;
            ah.Command = cmd;
            eh.Command = cmd;
            dh.Command = cmd;
            ch.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            Historial.Core.Historial historia = new Historial.Core.Historial();
            Reingreso.Core.Reingresos reingreso = new Reingreso.Core.Reingresos();
            Altas.Core.Altas alta = new Altas.Core.Altas();
            Empresas.Core.Empresas empresa = new Empresas.Core.Empresas();

            empleado.idtrabajador = _idempleado;
            empleado.idempresa = int.Parse(cmbRegistroPatronal.SelectedValue.ToString());
            empleado.fechaingreso = dtpFechaReingreso.Value;
            empleado.fechaantiguedad = dtpFechaAntiguedad.Value;
            empleado.antiguedad = int.Parse(txtAntiguedad.Text);
            empleado.antiguedadmod = int.Parse(txtAntiguedadMod.Text);
            empleado.iddepartamento = int.Parse(cmbDepartamento.SelectedValue.ToString());
            empleado.idpuesto = int.Parse(cmbPuesto.SelectedValue.ToString());
            empleado.idperiodo = int.Parse(cmbPeriodo.SelectedValue.ToString());
            empleado.sueldo = double.Parse(txtSueldo.Text);
            empleado.sd = double.Parse(txtSalarioDiario.Text);
            empleado.sdi = double.Parse(txtSDI.Text);
            empleado.idusuario = GLOBALES.IDUSUARIO;
            empleado.estatus = GLOBALES.ACTIVO;
            empleado.cuenta = mtxtCuentaBancaria.Text;
            empleado.clabe = mtxtCuentaClabe.Text;
            empleado.idbancario = mtxtIdBancario.Text;
            empleado.metodopago = cmbMetodoPago.Text;
            

            historia.idtrabajador = _idempleado;
            historia.idempresa = int.Parse(cmbRegistroPatronal.SelectedValue.ToString());
            historia.valor = double.Parse(txtSDI.Text);
            historia.fecha_imss = dtpFechaReingreso.Value;
            historia.fecha_sistema = DateTime.Now;
            historia.motivobaja = 0;

            empresa.idempresa = int.Parse(cmbRegistroPatronal.SelectedValue.ToString());

            reingreso.idtrabajador = _idempleado;
            reingreso.idempresa = int.Parse(cmbRegistroPatronal.SelectedValue.ToString());
            reingreso.fechaingreso = dtpFechaReingreso.Value;
            reingreso.sdi = double.Parse(txtSDI.Text);

            Periodos.Core.PeriodosHelper pdh = new Periodos.Core.PeriodosHelper();
            pdh.Command = cmd;

            Periodos.Core.Periodos p = new Periodos.Core.Periodos();
            p.idperiodo = int.Parse(cmbPeriodo.SelectedValue.ToString());
            int diasPago = 0;
            try { cnx.Open(); diasPago = (int)pdh.DiasDePago(p); cnx.Close(); }
            catch { MessageBox.Show("Error: Al obtener los dias de pago.", "Error"); }

            DateTime dt = dtpFechaReingreso.Value.Date;
            DateTime periodoInicio, periodoFin;
            int diasProporcionales = 0;
            if (diasPago == 7)
            {
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                periodoInicio = dt;
                periodoFin = dt.AddDays(6);
                diasProporcionales = (int)(periodoFin.Date - dtpFechaReingreso.Value.Date).TotalDays + 1;
            }
            else
            {
                if (dt.Day <= 15)
                {
                    periodoInicio = new DateTime(dt.Year, dt.Month, 1);
                    periodoFin = new DateTime(dt.Year, dt.Month, 15);
                    diasProporcionales = (int)(periodoFin.Date - dtpFechaReingreso.Value.Date).TotalDays + 1;
                }
                else
                {
                    int diasMes = DateTime.DaysInMonth(dt.Year, dt.Month);
                    int diasNoLaborados = 0;
                    periodoInicio = new DateTime(dt.Year, dt.Month, 16);
                    periodoFin = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
                    diasNoLaborados = (int)(dtpFechaReingreso.Value.Date - periodoInicio).TotalDays;
                    diasProporcionales = 15 - diasNoLaborados;
                }
            }

            alta.idtrabajador = _idempleado;
            alta.idempresa = int.Parse(cmbRegistroPatronal.SelectedValue.ToString());
            alta.contrato = 4;
            alta.jornada = 12;
            alta.fechaingreso = dtpFechaReingreso.Value;
            alta.diasproporcionales = diasProporcionales;
            alta.sdi = double.Parse(txtSDI.Text);
            alta.cp = "00000";
            alta.clinica = "000";
            alta.estado = cmbEstados.Text;
            alta.noestado = int.Parse(cmbEstados.SelectedValue.ToString());
            alta.sexo = ObtieneSexo();
            alta.periodoInicio = periodoInicio;
            alta.periodoFin = periodoFin;
            
            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();

            try {
                cnx.Open();
                empleadoh.reingreso(empleado);

                rp = (string)eh.obtenerRegistroPatronal(empresa);
                reingreso.registropatronal = rp;
                alta.registropatronal = rp;

                lstEmpleado = empleadoh.obtenerEmpleado(empleado);

                for (int i = 0; i < lstEmpleado.Count; i++)
                {
                    reingreso.nss = lstEmpleado[i].nss + lstEmpleado[i].digitoverificador;
                    alta.nss = lstEmpleado[i].nss + lstEmpleado[i].digitoverificador;
                    alta.rfc = lstEmpleado[i].rfc;
                    alta.curp = lstEmpleado[i].curp;
                    alta.paterno = lstEmpleado[i].paterno;
                    alta.materno = lstEmpleado[i].materno;
                    alta.nombre = lstEmpleado[i].nombres;
                    alta.fechanacimiento = lstEmpleado[i].fechanacimiento;
                }

                int idEmpresaEmpleado = (int)empleadoh.obtenerIdEmpresa(empleado);
                if (idEmpresaEmpleado == int.Parse(cmbRegistroPatronal.SelectedValue.ToString()))
                {
                    historia.tipomovimiento = GLOBALES.mREINGRESO;
                    reingreso.diasproporcionales = diasProporcionales;
                    reingreso.periodoinicio = periodoInicio;
                    reingreso.periodofin = periodoFin;
                    rh.insertaReingreso(reingreso);
                }
                else
                {
                    historia.tipomovimiento = GLOBALES.mALTA;
                    ah.insertaAlta(alta);
                }

                hh.insertarHistorial(historia);

                cnx.Close();
                cnx.Dispose();

                MessageBox.Show("Empleado reingresado con éxito.", "Información");

                if (OnReingreso != null)
                    OnReingreso(GLOBALES.NUEVO);
            }
            catch (Exception error) {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
            this.Dispose();
        }

        private string ObtieneSexo()
        {
            if (rbtnHombre.Checked)
            {
                sexo = "H";
            }
            else if (rbtnMujer.Checked)
            {
                sexo = "M";
            }
            return sexo.ToString();
        }
    }
}
