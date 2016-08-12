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
        List<Empleados.Core.Empleados> lstEmpleado;
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

            Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
            Puestos.Core.PuestosHelper ph = new Puestos.Core.PuestosHelper();
            Periodos.Core.PeriodosHelper periodoh = new Periodos.Core.PeriodosHelper();
            Factores.Core.FactoresHelper fh = new Factores.Core.FactoresHelper();
            Empleados.Core.EmpleadosHelper emph = new Empleados.Core.EmpleadosHelper();

            emph.Command = cmd;
            dh.Command = cmd;
            ph.Command = cmd;
            periodoh.Command = cmd;
            fh.Command = cmd;

            Departamento.Core.Depto depto = new Departamento.Core.Depto();
            Puestos.Core.Puestos puesto = new Puestos.Core.Puestos();
            Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
            Factores.Core.Factores factor = new Factores.Core.Factores();
            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            
            empleado.idtrabajador = _idempleado;
            depto.idempresa = GLOBALES.IDEMPRESA;
            puesto.idempresa = GLOBALES.IDEMPRESA;
            periodo.idempresa = GLOBALES.IDEMPRESA;

            List<Departamento.Core.Depto> lstDepto = new List<Departamento.Core.Depto>();
            List<Puestos.Core.Puestos> lstPuesto = new List<Puestos.Core.Puestos>();
            List<Periodos.Core.Periodos> lstPeriodo = new List<Periodos.Core.Periodos>();
            lstEmpleado = new List<Empleados.Core.Empleados>();

            try {
                cnx.Open();
                lstDepto = dh.obtenerDepartamentos(depto);
                lstPuesto = ph.obtenerPuestos(puesto);
                lstPeriodo = periodoh.obtenerPeriodos(periodo);
                lstEmpleado = emph.obtenerEmpleado(empleado);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error) 
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            cmbDepartamento.DataSource = lstDepto.ToList();
            cmbDepartamento.DisplayMember = "descripcion";
            cmbDepartamento.ValueMember = "id";

            cmbPuesto.DataSource = lstPuesto.ToList();
            cmbPuesto.DisplayMember = "nombre";
            cmbPuesto.ValueMember = "idpuesto";

            cmbPeriodo.DataSource = lstPeriodo.ToList();
            cmbPeriodo.DisplayMember = "pago";
            cmbPeriodo.ValueMember = "idperiodo";

            txtNombreCompleto.Text = _nombreEmpleado;
            mtxtNoEmpleado.Text = lstEmpleado[0].noempleado;
            cmbMetodoPago.SelectedIndex = 2;
            
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
            Empresas.Core.EmpresasHelper eh = new Empresas.Core.EmpresasHelper();
            Infonavit.Core.InfonavitHelper ih = new Infonavit.Core.InfonavitHelper();
            
            empleadoh.Command = cmd;
            hh.Command = cmd;
            rh.Command = cmd;
            eh.Command = cmd;
            ih.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            Empleados.Core.EmpleadosEstatus ee = new Empleados.Core.EmpleadosEstatus();
            Historial.Core.Historial historia = new Historial.Core.Historial();
            Reingreso.Core.Reingresos reingreso = new Reingreso.Core.Reingresos();
            Empresas.Core.Empresas empresa = new Empresas.Core.Empresas();

            empleado.idtrabajador = _idempleado;
            empleado.idempresa = lstEmpleado[0].idempresa;
            empleado.fechaingreso = dtpFechaReingreso.Value;
            empleado.fechaantiguedad = dtpFechaAntiguedad.Value;
            empleado.antiguedad = int.Parse(txtAntiguedad.Text);
            empleado.antiguedadmod = int.Parse(txtAntiguedadMod.Text);
            empleado.iddepartamento = int.Parse(cmbDepartamento.SelectedValue.ToString());
            empleado.idpuesto = int.Parse(cmbPuesto.SelectedValue.ToString());
            empleado.idperiodo = int.Parse(cmbPeriodo.SelectedValue.ToString());
            empleado.sueldo = decimal.Parse(txtSueldo.Text);
            empleado.sd = decimal.Parse(txtSalarioDiario.Text);
            empleado.sdi = decimal.Parse(txtSDI.Text);
            empleado.idusuario = GLOBALES.IDUSUARIO;
            empleado.estatus = GLOBALES.ACTIVO;
            empleado.cuenta = mtxtCuentaBancaria.Text;
            empleado.clabe = mtxtCuentaClabe.Text;
            empleado.idbancario = mtxtIdBancario.Text;
            empleado.metodopago = cmbMetodoPago.Text;

            if (chkObraCivil.Checked)
                empleado.obracivil = true;
            else
                empleado.obracivil = false;

            ee.idtrabajador = _idempleado;
            ee.idempresa = GLOBALES.IDEMPRESA;
            ee.estatus = GLOBALES.REINGRESO;            

            historia.idtrabajador = _idempleado;
            historia.idempresa = lstEmpleado[0].idempresa;
            historia.valor = decimal.Parse(txtSDI.Text);
            historia.fecha_imss = dtpFechaReingreso.Value;
            historia.fecha_sistema = DateTime.Now;
            historia.motivobaja = 0;
            historia.tipomovimiento = GLOBALES.mREINGRESO;
            historia.iddepartamento = int.Parse(cmbDepartamento.SelectedValue.ToString());
            historia.idpuesto = int.Parse(cmbPuesto.SelectedValue.ToString());

            empresa.idempresa = lstEmpleado[0].idempresa;

            reingreso.idtrabajador = _idempleado;
            reingreso.idempresa = lstEmpleado[0].idempresa;
            reingreso.fechaingreso = dtpFechaReingreso.Value;
            reingreso.sdi = decimal.Parse(txtSDI.Text);

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
                    switch (diasMes)
                    {
                        case 28:
                            diasProporcionales = 15 - diasNoLaborados;
                            break;
                        case 29:
                            diasProporcionales = 15 - diasNoLaborados;
                            break;
                        case 30:
                            diasProporcionales = (diasMes - 15) - diasNoLaborados;
                            break;
                        case 31:
                            diasProporcionales = (diasMes - 16) - diasNoLaborados;
                            break;
                    }
                }
            }

            try {
                cnx.Open();
                empleadoh.reingreso(empleado);
                empleadoh.bajaEmpleado(ee);

                rp = (string)eh.obtenerRegistroPatronal(empresa);

                reingreso.registropatronal = rp;
                reingreso.nss = lstEmpleado[0].nss + lstEmpleado[0].digitoverificador;
                reingreso.diasproporcionales = diasProporcionales;
                reingreso.periodoinicio = periodoInicio;
                reingreso.periodofin = periodoFin;

                rh.insertaReingreso(reingreso);
                hh.insertarHistorial(historia);

                cnx.Close();
                MessageBox.Show("Empleado reingresado con éxito.", "Información");

                if (OnReingreso != null)
                    OnReingreso(GLOBALES.NUEVO);
            }
            catch (Exception error) {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }

            int existeInfonavit = 0;
            try
            {
                cnx.Open();
                existeInfonavit = (int)ih.existeInfonavit(_idempleado);
                cnx.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener la existencia del Infonavit.\r\n AVISO: INGRESAR O MODIFICAR MANUALMENTE EL CREDITO DE INFONAVIT", "Error");
                cnx.Dispose();
            }

            List<Infonavit.Core.Infonavit> lstInfonavit = new List<Infonavit.Core.Infonavit>();
            if (existeInfonavit != 0)
            {
                try
                {
                    cnx.Open();
                    lstInfonavit = ih.obtenerInfonavitTrabajador(_idempleado);
                    cnx.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al obtener la información de infonavit.\r\n AVISO: INGRESAR O MODIFICAR MANUALMENTE EL CREDITO DE INFONAVIT", "Error");
                    cnx.Dispose();
                }

                try
                {
                    cnx.Open();
                    ih.actualizaEstatusInfonavit(lstInfonavit[0].idinfonavit, _idempleado);
                    cnx.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al obtener al activar el crédito de infonavit.\r\n AVISO: INGRESAR O MODIFICAR MANUALMENTE EL CREDITO DE INFONAVIT", "Error");
                    cnx.Dispose();
                }
            }

            this.Dispose();
        }

    }
}
