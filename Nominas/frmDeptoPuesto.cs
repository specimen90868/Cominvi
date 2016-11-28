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
    public partial class frmDeptoPuesto : Form
    {
        public frmDeptoPuesto()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        SqlConnection cnx;
        SqlCommand cmd;
        DateTime inicioPeriodo, finPeriodo;
        DateTime periodoInicioCalculo, periodoFinCalculo;
        int idperiodo, iddepto, idpuesto;
        decimal sdi;
        List<CalculoNomina.Core.tmpPagoNomina> lstUltimaNomina;
        #endregion

        #region VARIABLES PUBLICAS
        public int _deptopuesto;
        public int _idempleado;
        #endregion

        private void frmDeptoPuesto_Load(object sender, EventArgs e)
        {
            cmbDeptoPuesto.DataSource = null;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;
            Empleados.Core.Empleados emp = new Empleados.Core.Empleados();
            emp.idtrabajador = _idempleado;
            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();
            try
            {
                cnx.Open();
                lstEmpleado = eh.obtenerEmpleado(emp);
                cnx.Close();
            }
            catch (Exception)
            {
                
                throw;
            }

            lblEmpleado.Text = lstEmpleado[0].nombrecompleto;
            mtxtNoEmpleado.Text = lstEmpleado[0].noempleado;
            idperiodo = lstEmpleado[0].idperiodo;
            sdi = lstEmpleado[0].sdi;
            iddepto = lstEmpleado[0].iddepartamento;
            idpuesto = lstEmpleado[0].idpuesto;

            if (_deptopuesto == 0)
            {
                Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
                dh.Command = cmd;

                Departamento.Core.Depto d = new Departamento.Core.Depto();
                d.idempresa = GLOBALES.IDEMPRESA;

                List<Departamento.Core.Depto> lstDeptos = new List<Departamento.Core.Depto>();

                try
                {
                    cnx.Open();
                    lstDeptos = dh.obtenerDepartamentos(d);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception)
                {
                    
                    throw;
                }

                cmbDeptoPuesto.DataSource = lstDeptos.ToList();
                cmbDeptoPuesto.DisplayMember = "descripcion";
                cmbDeptoPuesto.ValueMember = "id";
            }
            else {
                Puestos.Core.PuestosHelper ph = new Puestos.Core.PuestosHelper();
                ph.Command = cmd;

                Puestos.Core.Puestos p = new Puestos.Core.Puestos();
                p.idempresa = GLOBALES.IDEMPRESA;

                List<Puestos.Core.Puestos> lstPuestos = new List<Puestos.Core.Puestos>();

                try
                {
                    cnx.Open();
                    lstPuestos = ph.obtenerPuestos(p);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception)
                {
                    
                    throw;
                }
                cmbDeptoPuesto.DataSource = lstPuestos.ToList();
                cmbDeptoPuesto.DisplayMember = "nombre";
                cmbDeptoPuesto.ValueMember = "idpuesto";
            }
        }

        private void toolCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmDeptoPuesto_FormClosed(object sender, FormClosedEventArgs e)
        {
            cmbDeptoPuesto.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            obtenerPeriodoCalculo();

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Aplicaciones.Core.AplicacionesHelper ah = new Aplicaciones.Core.AplicacionesHelper();
            ah.Command = cmd;

            Aplicaciones.Core.Aplicaciones a = new Aplicaciones.Core.Aplicaciones();

            a.idtrabajador = _idempleado;
            a.idempresa = GLOBALES.IDEMPRESA;
            a.iddeptopuesto = int.Parse(cmbDeptoPuesto.SelectedValue.ToString());
            a.fecha = dtpFechaAplicacion.Value.Date;
            a.registro = DateTime.Now;
            a.idusuario = GLOBALES.IDUSUARIO;
            a.periodoinicio = inicioPeriodo;
            a.periodofin = finPeriodo;

            Historial.Core.HistorialHelper hh = new Historial.Core.HistorialHelper();
            hh.Command = cmd;

            Historial.Core.Historial historial = new Historial.Core.Historial();

            historial = new Historial.Core.Historial();
            historial.idtrabajador = _idempleado;
            historial.idempresa = GLOBALES.IDEMPRESA;
            historial.valor = sdi;
            historial.fecha_sistema = DateTime.Now;
            historial.motivobaja = 0;
            historial.fecha_imss = dtpFechaAplicacion.Value.Date;

            if (_deptopuesto == 0)
            {
                a.deptopuesto = "D";
                historial.tipomovimiento = GLOBALES.mCAMBIODEPARTAMENTO;
                historial.iddepartamento = int.Parse(cmbDeptoPuesto.SelectedValue.ToString());
                historial.idpuesto = idpuesto;
            }
            else
            {
                a.deptopuesto = "P";
                historial.tipomovimiento = GLOBALES.mCAMBIOPUESTO;
                historial.idpuesto = int.Parse(cmbDeptoPuesto.SelectedValue.ToString());
                historial.iddepartamento = iddepto;
            }

            try
            {
                cnx.Open();
                hh.insertarHistorial(historial);
                cnx.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al insertar el historico.", "Error");
                cnx.Dispose();
            }

            if (inicioPeriodo.Date == periodoInicioCalculo.Date && finPeriodo.Date == periodoFinCalculo.Date)
            {
                Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
                eh.Command = cmd;
                try
                {
                    cnx.Open();
                    if (_deptopuesto == 0)
                        eh.actualizaDeptoPuesto(int.Parse(cmbDeptoPuesto.SelectedValue.ToString()), _idempleado, "D");
                    else
                        eh.actualizaDeptoPuesto(int.Parse(cmbDeptoPuesto.SelectedValue.ToString()), _idempleado, "P");
                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al actualizar el depto/puesto.", "Error");
                    cnx.Dispose();
                }
            }
            else
            {
                try
                {
                    cnx.Open();
                    ah.insertaAplicacion(a);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al ingresar la aplicacion del depto/puesto.", "Error");
                    cnx.Dispose();
                }
            }

           
            this.Dispose();
        }

        private void dtpFechaAplicacion_ValueChanged(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            lstUltimaNomina = new List<CalculoNomina.Core.tmpPagoNomina>();

            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
            periodo.idperiodo = idperiodo;

            int dias = 0;
            try
            {
                cnx.Open();
                dias = int.Parse(ph.DiasDePago(periodo).ToString());
                lstUltimaNomina = nh.obtenerUltimaNominaTrabajador(GLOBALES.IDEMPRESA, _idempleado, dias);
                cnx.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener la ultima nómina del trabajador.", "Error");
                cnx.Dispose();
            }

            if (lstUltimaNomina.Count != 0)
                if (dtpFechaAplicacion.Value.Date <= lstUltimaNomina[0].fechafin)
                {
                    MessageBox.Show("La fecha de aplicación seleccionada no es valida.\r\n\r\n" +
                                    "Se empalma con la ultima nomina del trabajador, por favor verifique.", "Información");
                    dtpFechaAplicacion.Value = DateTime.Now;
                }


            if (dias == 7)
            {
                DateTime dt = dtpFechaAplicacion.Value;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                inicioPeriodo = dt;
                finPeriodo = dt.AddDays(6);
            }
            else
            {
                if (dtpFechaAplicacion.Value.Day <= 15)
                {
                    inicioPeriodo = new DateTime(dtpFechaAplicacion.Value.Year, dtpFechaAplicacion.Value.Month, 1);
                    finPeriodo = new DateTime(dtpFechaAplicacion.Value.Year, dtpFechaAplicacion.Value.Month, 15);
                }
                else
                {
                    inicioPeriodo = new DateTime(dtpFechaAplicacion.Value.Year, dtpFechaAplicacion.Value.Month, 16);
                    finPeriodo = new DateTime(dtpFechaAplicacion.Value.Year, dtpFechaAplicacion.Value.Month, 
                        DateTime.DaysInMonth(dtpFechaAplicacion.Value.Year, dtpFechaAplicacion.Value.Month));
                }
            }
        }

        private void obtenerPeriodoCalculo()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            List<CalculoNomina.Core.tmpPagoNomina> lstUltimaNomina = new List<CalculoNomina.Core.tmpPagoNomina>();

            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos p = new Periodos.Core.Periodos();
            p.idperiodo = idperiodo;

            List<Periodos.Core.Periodos> lstPeriodo = new List<Periodos.Core.Periodos>();

            try
            {
                cnx.Open();
                lstPeriodo = ph.obtenerPeriodo(p);
                cnx.Close();
            }
            catch (Exception)
            {
                
                throw;
            }
            int periodo = lstPeriodo[0].dias;
                
            try
            {
                cnx.Open();
                lstUltimaNomina = nh.obtenerUltimaNominaTrabajador(GLOBALES.IDEMPRESA, _idempleado, periodo);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            if (lstUltimaNomina.Count != 0)
            {
                if (periodo == 7)
                {
                    periodoInicioCalculo = lstUltimaNomina[0].fechafin.AddDays(1);
                    periodoFinCalculo = lstUltimaNomina[0].fechafin.AddDays(7);
                }
                else
                {
                    periodoInicioCalculo = lstUltimaNomina[0].fechafin.AddDays(1);

                    if (periodoInicioCalculo.Day <= 15)
                        periodoFinCalculo = lstUltimaNomina[0].fechafin.AddDays(15);
                    else
                        periodoFinCalculo = new DateTime(periodoInicioCalculo.Year, periodoInicioCalculo.Month,
                            DateTime.DaysInMonth(periodoInicioCalculo.Year, periodoInicioCalculo.Month));

                }
            }
        }
    }
}
