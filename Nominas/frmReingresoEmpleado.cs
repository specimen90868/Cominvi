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
        DateTime periodoInicio, periodoFin;
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
            mtxtCuentaBancaria.Text = lstEmpleado[0].cuenta;
            mtxtCuentaClabe.Text = lstEmpleado[0].clabe;
            mtxtIdBancario.Text = lstEmpleado[0].idbancario;

            if (GLOBALES.OBRACIVIL)
                chkObraCivil.Visible = true;
            else
                chkObraCivil.Visible = false;
           
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

            ObtenerPeriodo(diasPago());

            SqlConnection cnxReingreso = new SqlConnection(cdn);
            SqlCommand cmdReingreso = new SqlCommand();
            cmdReingreso.Connection = cnxReingreso;

            #region VERIFICA ULTIMA NOMINA DEL TRABAJADOR
            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmdReingreso;

            List<CalculoNomina.Core.tmpPagoNomina> lstFechas = new List<CalculoNomina.Core.tmpPagoNomina>();
            bool verificaFechas = false;
             
            try
            {
                cnxReingreso.Open();
                lstFechas = nh.obtenerUltimaNominaTrabajador(GLOBALES.IDEMPRESA, GLOBALES.NORMAL, diasPago(), _idempleado);
                cnxReingreso.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener la fecha de la ultima nómina calculada", "Error");
            }

            if (lstFechas.Count != 0)
            {
                if (dtpFechaReingreso.Value.Date <= lstFechas[0].fechainicio.Date || dtpFechaReingreso.Value.Date <= lstFechas[0].fechafin.Date)
                    verificaFechas = false;
                else
                    verificaFechas = true;
                if (!verificaFechas)
                {
                    MessageBox.Show("La fecha de ingreso es invalida. Fecha menor al ultimo periodo calculado, verifique.", "Error");
                    return;
                }
            }
            #endregion

            #region VALIDACION DE LA FECHA DE REINGRESO
            DateTime fechaIngreso;
            object valor;
            Bajas.Core.BajasHelper bajash = new Bajas.Core.BajasHelper();
            bajash.Command = cmdReingreso;

            Bajas.Core.Bajas baja = new Bajas.Core.Bajas();
            baja.idempresa = GLOBALES.IDEMPRESA;
            baja.idtrabajador = _idempleado;
            try
            {
                cnxReingreso.Open();
                valor = bajash.obtenerFechaBaja(baja);
                cnxReingreso.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Al obtener la fecha de ingreso del trabajador. Vuelva a intentar.\r\n\r\n" + error.Message, "Error");
                return;
            }

            if(valor != null)
            {
                fechaIngreso = DateTime.Parse(valor.ToString());
                if (dtpFechaReingreso.Value.Date < fechaIngreso)
                {
                    MessageBox.Show("La fecha de baja es menor a la fecha de ingreso del trabajador. Verifique.", "Error");
                    return;
                }
            }
            #endregion

            #region DATOS ORIGINALES DEL TRABAJADOR, NSS, REGISTRO PATRONAL
            Empleados.Core.EmpleadosHelper empleadoh = new Empleados.Core.EmpleadosHelper();
            empleadoh.Command = cmdReingreso;
            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idempleado;

            Empresas.Core.EmpresasHelper eh = new Empresas.Core.EmpresasHelper();
            eh.Command = cmdReingreso;
            Empresas.Core.Empresas empresa = new Empresas.Core.Empresas();
            empresa.idempresa = GLOBALES.IDEMPRESA;

            string nss = "";
            string registroParonal = "";
            List<Empleados.Core.Empleados> lstEmpleadoOriginal;
            try
            {
                cnxReingreso.Open();
                lstEmpleadoOriginal = empleadoh.obtenerEmpleado(empleado);
                nss = empleadoh.obtenerNss(empleado).ToString();
                registroParonal = eh.obtenerRegistroPatronal(empresa).ToString();
                cnxReingreso.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Al obtener los datos generales del trabajador.\r\n\r\n" + error.Message, "Error");
                return;
            }
            #endregion

            #region INGRESO DEL REINGRESO TABLA SUAREINGRESOS
            Reingreso.Core.ReingresoHelper rh = new Reingreso.Core.ReingresoHelper();
            rh.Command = cmdReingreso;
            Reingreso.Core.Reingresos reingreso = new Reingreso.Core.Reingresos();
            reingreso.idtrabajador = _idempleado;
            reingreso.idempresa = GLOBALES.IDEMPRESA;
            reingreso.registropatronal = registroParonal;
            reingreso.nss = nss;
            reingreso.fechaingreso = dtpFechaReingreso.Value.Date;
            reingreso.sdi = decimal.Parse(txtSDI.Text);
            reingreso.registro = DateTime.Now;
            reingreso.diasproporcionales = diasProporcionales(diasPago());
            reingreso.periodoinicio = periodoInicio.Date;
            reingreso.periodofin = periodoFin.Date;

            try
            {
                cnxReingreso.Open();
                rh.insertaReingreso(reingreso);
                cnxReingreso.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Al insertar el reingreso del trabajador. \r\n\r\n Esta ventana se cerrará.\r\n\r\n" + error.Message, "Error");
                return;
            }
            #endregion

            #region INGRESO DEL HISTORIAL TABLA MOVIMIENTOTRABAJADOR
            Historial.Core.HistorialHelper hh = new Historial.Core.HistorialHelper();
            hh.Command = cmdReingreso;
            Historial.Core.Historial historia = new Historial.Core.Historial();
            historia.idtrabajador = _idempleado;
            historia.idempresa = GLOBALES.IDEMPRESA;
            historia.valor = decimal.Parse(txtSDI.Text);
            historia.fecha_imss = dtpFechaReingreso.Value;
            historia.fecha_sistema = DateTime.Now;
            historia.motivobaja = 0;
            historia.tipomovimiento = GLOBALES.mREINGRESO;
            historia.iddepartamento = int.Parse(cmbDepartamento.SelectedValue.ToString());
            historia.idpuesto = int.Parse(cmbPuesto.SelectedValue.ToString());

            try
            {
                cnxReingreso.Open();
                hh.insertarHistorial(historia);
                cnxReingreso.Close();
            }
            catch (Exception)
            {
                try
                {
                    cnxReingreso.Open();
                    rh.eliminaReingreso(reingreso);
                    cnxReingreso.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al elmininar el reingreso.\r\n\r\n Esta Ventana se cerrará.", "Error");
                    this.Dispose();
                }
            }
            #endregion

            #region ACTUALIZACION EMPLEADO
            empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idempleado;
            empleado.idempresa = GLOBALES.IDEMPRESA;
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

            try
            {
                cnxReingreso.Open();
                empleadoh.reingreso(empleado);
                cnxReingreso.Close();
            }
            catch (Exception)
            {
                try
                {
                    cnxReingreso.Open();
                    rh.eliminaReingreso(reingreso);
                    hh.eliminaHistorial(_idempleado, GLOBALES.mREINGRESO, historia.fecha_imss);
                    cnxReingreso.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al elmininar el reingreso e historial.\r\n\r\n Esta Ventana se cerrará.", "Error");
                    this.Dispose();
                }
            }
            #endregion

            #region ACTUALIZACION TABLA TRABAJADORES ESTATUS
            Empleados.Core.EmpleadosEstatus ee = new Empleados.Core.EmpleadosEstatus();
            ee.idtrabajador = _idempleado;
            ee.idempresa = GLOBALES.IDEMPRESA;
            ee.estatus = GLOBALES.REINGRESO;

            try
            {
                cnxReingreso.Open();
                empleadoh.bajaEmpleado(ee);
                cnxReingreso.Close();
            }
            catch (Exception)
            {
                empleado = new Empleados.Core.Empleados();
                empleado.idtrabajador = _idempleado;
                empleado.idempresa = GLOBALES.IDEMPRESA;
                empleado.fechaingreso = lstEmpleadoOriginal[0].fechaingreso;
                empleado.fechaantiguedad = lstEmpleadoOriginal[0].fechaantiguedad;
                empleado.antiguedad = lstEmpleadoOriginal[0].antiguedad;
                empleado.antiguedadmod = lstEmpleadoOriginal[0].antiguedadmod;
                empleado.iddepartamento = lstEmpleadoOriginal[0].iddepartamento;
                empleado.idpuesto = lstEmpleadoOriginal[0].idpuesto;
                empleado.idperiodo = lstEmpleadoOriginal[0].idperiodo;
                empleado.sueldo = lstEmpleadoOriginal[0].sueldo;
                empleado.sd = lstEmpleadoOriginal[0].sd;
                empleado.sdi = lstEmpleadoOriginal[0].sdi;
                empleado.idusuario = GLOBALES.IDUSUARIO;
                empleado.estatus = GLOBALES.INACTIVO;
                empleado.cuenta = lstEmpleadoOriginal[0].cuenta;
                empleado.clabe = lstEmpleadoOriginal[0].clabe;
                empleado.idbancario = lstEmpleadoOriginal[0].idbancario;
                empleado.metodopago = lstEmpleadoOriginal[0].metodopago;
                empleado.obracivil = lstEmpleadoOriginal[0].obracivil;
                try
                {
                    cnxReingreso.Open();
                    rh.eliminaReingreso(reingreso);
                    hh.eliminaHistorial(_idempleado, GLOBALES.mREINGRESO, historia.fecha_imss);
                    empleadoh.reingreso(empleado);
                    cnxReingreso.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al eliminar el reingreso, historial y actualización del trabajador.\r\n\r\n Esta Ventana se cerrará.", "Error");
                    this.Dispose();
                }
            }
            #endregion

            #region VERIFICACION DE INFONAVIT
            Infonavit.Core.InfonavitHelper ih = new Infonavit.Core.InfonavitHelper();
            ih.Command = cmdReingreso; 
           
            int existeInfonavit = 0;
            try
            {
                cnxReingreso.Open();
                existeInfonavit = (int)ih.existeInfonavit(_idempleado);
                cnxReingreso.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener la existencia del Infonavit.\r\n AVISO: INGRESAR O MODIFICAR MANUALMENTE EL CREDITO DE INFONAVIT", "Error");
                cnxReingreso.Dispose();
            }

            List<Infonavit.Core.Infonavit> lstInfonavit = new List<Infonavit.Core.Infonavit>();
            if (existeInfonavit != 0)
            {
                try
                {
                    cnxReingreso.Open();
                    lstInfonavit = ih.obtenerInfonavitTrabajador(_idempleado);
                    cnxReingreso.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al obtener la información de infonavit.\r\n AVISO: INGRESAR O MODIFICAR MANUALMENTE EL CREDITO DE INFONAVIT", "Error");
                    cnxReingreso.Dispose();
                }

                try
                {
                    cnxReingreso.Open();
                    ih.actualizaEstatusInfonavit(lstInfonavit[0].idinfonavit, _idempleado);
                    cnxReingreso.Close();
                    MessageBox.Show("Trabajador cuenta con Infonavit. Crédito: " + lstInfonavit[0].credito, "Información");
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al obtener al activar el crédito de infonavit.\r\n AVISO: INGRESAR O MODIFICAR MANUALMENTE EL CREDITO DE INFONAVIT", "Error");
                    cnxReingreso.Dispose();
                }
            }
            #endregion

            if (OnReingreso != null)
                OnReingreso(GLOBALES.NUEVO);  

            this.Dispose();
        }

        private int diasPago() {

            SqlConnection cnxDiasPago = new SqlConnection(cdn);
            SqlCommand cmdDiasPago = new SqlCommand();
            cmdDiasPago.Connection = cnxDiasPago;

            Periodos.Core.PeriodosHelper pdh = new Periodos.Core.PeriodosHelper();
            pdh.Command = cmdDiasPago;

            Periodos.Core.Periodos p = new Periodos.Core.Periodos();
            p.idperiodo = int.Parse(cmbPeriodo.SelectedValue.ToString());
            int diasPago = 0;
            try {
                cnxDiasPago.Open(); 
                diasPago = (int)pdh.DiasDePago(p);
                cnxDiasPago.Close();
                cnxDiasPago.Dispose();
            }
            catch { 
                MessageBox.Show("Error: Al obtener los dias de pago.", "Error"); 
            }
            return diasPago;
        }

        private int diasProporcionales(int dias)
        {
            DateTime dt = dtpFechaReingreso.Value.Date;
            DateTime inicio, fin;
            int diasProporcionales = 0;
            if (dias == 7)
            {
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                inicio = dt;
                fin = dt.AddDays(6);
                diasProporcionales = (int)(fin.Date - dtpFechaReingreso.Value.Date).TotalDays + 1;
            }
            else
            {
                if (dt.Day <= 15)
                {
                    inicio = new DateTime(dt.Year, dt.Month, 1);
                    fin = new DateTime(dt.Year, dt.Month, 15);
                    diasProporcionales = (int)(fin.Date - dtpFechaReingreso.Value.Date).TotalDays + 1;
                }
                else
                {
                    int diasMes = DateTime.DaysInMonth(dt.Year, dt.Month);
                    int diasNoLaborados = 0;
                    inicio = new DateTime(dt.Year, dt.Month, 16);
                    fin = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
                    diasNoLaborados = (int)(dtpFechaReingreso.Value.Date - inicio).TotalDays;
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
            return diasProporcionales;
        }

        private void ObtenerPeriodo(int dias)
        {
            DateTime dt = dtpFechaReingreso.Value.Date;
            if (dias == 7)
            {
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                periodoInicio = dt;
                periodoFin = dt.AddDays(6);
            }
            else
            {
                if (dt.Day <= 15)
                {
                    periodoInicio = new DateTime(dt.Year, dt.Month, 1);
                    periodoFin = new DateTime(dt.Year, dt.Month, 15);
                }
                else
                {
                    periodoInicio = new DateTime(dt.Year, dt.Month, 16);
                    periodoFin = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
                }
            }
        }

        private void txtAntiguedadMod_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
