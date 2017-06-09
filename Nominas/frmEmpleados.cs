using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Nominas
{
    public partial class frmEmpleados : Form
    {
        public frmEmpleados()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Empleados.Core.EmpleadosHelper eh;
        Departamento.Core.DeptoHelper dh;
        Puestos.Core.PuestosHelper ph;
        Estados.Core.EstadosHelper edoh;
        Imagen.Core.ImagenesHelper ih;
        Catalogos.Core.CatalogosHelper cath;
        Periodos.Core.PeriodosHelper pdh;
        //Historial.Core.HistorialHelper hh;
        Salario.Core.SalariosHelper sh;
        //Aplicaciones.Core.AplicacionesHelper aplih;
        string sexo;
        string estado;
        Bitmap bmp;
        bool ImagenAsignada = false;
        //historicoDepto = false, historicoPuesto = false;
        //DateTime inicioPeriodo, finPeriodo;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoEmpleado(int edicion);
        public event delOnNuevoEmpleado OnNuevoEmpleado;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _idempleado;
        #endregion

        private void CargaComboBox()
        {
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            cath = new Catalogos.Core.CatalogosHelper();
            cath.Command = cmd;
            Catalogos.Core.Catalogo ts = new Catalogos.Core.Catalogo();
            ts.grupodescripcion = "SALARIO";

            Catalogos.Core.Catalogo mp = new Catalogos.Core.Catalogo();
            mp.grupodescripcion = "METODO DE PAGO";

            dh = new Departamento.Core.DeptoHelper();
            dh.Command = cmd;
            Departamento.Core.Depto depto = new Departamento.Core.Depto();
            depto.idempresa = GLOBALES.IDEMPRESA;

            ph = new Puestos.Core.PuestosHelper();
            ph.Command = cmd;
            Puestos.Core.Puestos puesto = new Puestos.Core.Puestos();
            puesto.idempresa = GLOBALES.IDEMPRESA;

            edoh = new Estados.Core.EstadosHelper();
            edoh.Command = cmd;

            pdh = new Periodos.Core.PeriodosHelper();
            pdh.Command = cmd;
            Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
            periodo.idempresa = GLOBALES.IDEMPRESA;

            sh = new Salario.Core.SalariosHelper();
            sh.Command = cmd;

            List<Catalogos.Core.Catalogo> lstTipoSalario = new List<Catalogos.Core.Catalogo>();
            List<Departamento.Core.Depto> lstDepto = new List<Departamento.Core.Depto>();
            List<Puestos.Core.Puestos> lstPuesto = new List<Puestos.Core.Puestos>();
            List<Estados.Core.Estados> lstEstados = new List<Estados.Core.Estados>();
            List<Periodos.Core.Periodos> lstPeriodos = new List<Periodos.Core.Periodos>();
            List<Catalogos.Core.Catalogo> lstMetodoPago = new List<Catalogos.Core.Catalogo>();

            try
            {
                cnx.Open();
                lstTipoSalario = cath.obtenerGrupo(ts);
                lstDepto = dh.obtenerDepartamentos(depto);
                lstPuesto = ph.obtenerPuestos(puesto);
                lstEstados = edoh.obtenerEstados();
                lstPeriodos = pdh.obtenerPeriodos(periodo);
                lstMetodoPago = cath.obtenerGrupo(mp);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                this.Dispose();
            }

            cmbTipoSalario.DataSource = lstTipoSalario.ToList();
            cmbTipoSalario.DisplayMember = "descripcion";
            cmbTipoSalario.ValueMember = "id";

            cmbDepartamento.DataSource = lstDepto.ToList();
            cmbDepartamento.DisplayMember = "descripcion";
            cmbDepartamento.ValueMember = "id";

            cmbPuesto.DataSource = lstPuesto.ToList();
            cmbPuesto.DisplayMember = "nombre";
            cmbPuesto.ValueMember = "idpuesto";

            cmbEstado.DataSource = lstEstados.ToList();
            cmbEstado.DisplayMember = "nombre";
            cmbEstado.ValueMember = "idestado";

            cmbPeriodo.DataSource = lstPeriodos.ToList();
            cmbPeriodo.DisplayMember = "pago";
            cmbPeriodo.ValueMember = "idperiodo";

        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            CargaComboBox();

            if (GLOBALES.OBRACIVIL)
                chkObraCivil.Visible = true;
            else
                chkObraCivil.Visible = false;

            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                object fechaBaja;
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                eh = new Empleados.Core.EmpleadosHelper();
                eh.Command = cmd;

                Bajas.Core.BajasHelper bh = new Bajas.Core.BajasHelper();
                bh.Command = cmd;

                Bajas.Core.Bajas b = new Bajas.Core.Bajas();
                b.idempresa = GLOBALES.IDEMPRESA;
                b.idtrabajador = _idempleado;

                List<Empleados.Core.Empleados> lstEmpleado;

                Empleados.Core.Empleados em = new Empleados.Core.Empleados();
                em.idtrabajador = _idempleado;

                try
                {
                    cnx.Open();
                    lstEmpleado = eh.obtenerEmpleado(em);
                    fechaBaja = bh.obtenerFechaBaja(b);
                    cnx.Close();
                    cnx.Dispose();

                    if (fechaBaja != null)
                    {
                        dtpFechaBaja.Value = DateTime.Parse(fechaBaja.ToString());
                    }
                    else
                        dtpFechaBaja.Value = new DateTime(1900, 1, 1);

                    for (int i = 0; i < lstEmpleado.Count; i++)
                    {
                        txtNombre.Text = lstEmpleado[i].nombres;
                        txtApPaterno.Text = lstEmpleado[i].paterno;
                        txtApMaterno.Text = lstEmpleado[i].materno;
                        mtxtNoEmpleado.Text = lstEmpleado[i].noempleado;
                        dtpFechaIngreso.Value = DateTime.Parse(lstEmpleado[i].fechaingreso.ToString());
                        
                        dtpFechaNacimiento.Value = DateTime.Parse(lstEmpleado[i].fechanacimiento.ToString());
                        txtAntiguedad.Text = lstEmpleado[i].antiguedad.ToString();
                        txtEdad.Text = lstEmpleado[i].edad.ToString();
                        
                        txtRFC.Text = lstEmpleado[i].rfc;
                        txtCURP.Text = lstEmpleado[i].curp;
                        txtNSS.Text = lstEmpleado[i].nss + lstEmpleado[i].digitoverificador.ToString();

                        cmbDepartamento.SelectedValue = int.Parse(lstEmpleado[i].iddepartamento.ToString());
                        cmbPuesto.SelectedValue = int.Parse(lstEmpleado[i].idpuesto.ToString());
                        cmbPeriodo.SelectedValue = int.Parse(lstEmpleado[i].idperiodo.ToString());
                        
                        cmbTipoSalario.SelectedValue = int.Parse(lstEmpleado[i].tiposalario.ToString());

                        txtSueldo.Text = lstEmpleado[i].sueldo.ToString("F6");
                        txtSD.Text = lstEmpleado[i].sd.ToString("F6");
                        txtSDI.Text = lstEmpleado[i].sdi.ToString("F6");

                        mtxtCuentaBancaria.Text = lstEmpleado[i].cuenta.ToString();
                        mtxtCuentaClabe.Text = lstEmpleado[i].clabe.ToString();
                        mtxtIdBancario.Text = lstEmpleado[i].idbancario.ToString();

                        if (lstEmpleado[i].obracivil)
                            chkObraCivil.Checked = true;
                        else
                            chkObraCivil.Checked = false;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta Empleado";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                    GLOBALES.INHABILITAR(this, typeof(MaskedTextBox));
                    GLOBALES.INHABILITAR(this, typeof(Button));
                    GLOBALES.INHABILITAR(this, typeof(DateTimePicker));
                    GLOBALES.INHABILITAR(this, typeof(ComboBox));
                    GLOBALES.INHABILITAR(this, typeof(RadioButton));
                    GLOBALES.INHABILITAR(this, typeof(CheckBox));
                    toolGuardarCerrar.Enabled = false;
                    toolGuardarNuevo.Enabled = false;
                }
                else
                {
                    toolTitulo.Text = "Edición Empleado";
                    cmbPeriodo.Enabled = false;
                }
            }
            else
                toolHistorial.Enabled = false;
        }

        private async void dtpFechaNacimiento_Leave(object sender, EventArgs e)
        {
            MessageBox.Show("Al dar Aceptar se calculará el RFC, por favor esperar a que se muestre.\r\n\r\n" +
                            "El calculo del RFC es informativo. La RENAPO es la entidad oficial para otorgar el RFC.","Calculo del RFC");
            txtRFC.Text = await obtieneRFC();
            txtEdad.Text = ObtieneEdad(dtpFechaNacimiento.Value).ToString();
            //Empleados.Core.RFC rfc = new Empleados.Core.RFC();
            //string _rfc = rfc.ObtieneRFC(txtApPaterno.Text, txtApMaterno.Text, txtNombre.Text);
            //string _homo = rfc.ClaveHomonimia(txtApPaterno.Text, txtApMaterno.Text, txtNombre.Text);
            //string _fecha = dtpFechaNacimiento.Value.ToString("yyMMdd");
            //string _dv = rfc.DigitoVerificador(_rfc + _fecha + _homo);
            //_rfc = _rfc + _fecha + _homo + _dv;
            //txtRFC.Text = _rfc;
        }

        private async Task<string> obtieneRFC()
        {
            string seleniumDriver = ConfigurationManager.AppSettings["DriverSelenium"];
            string valorRFC = "";
            IWebDriver driver;
            var chromeDriverService = ChromeDriverService.CreateDefaultService(seleniumDriver);
            chromeDriverService.HideCommandPromptWindow = true;
            var options = new ChromeOptions();
            options.AddArgument("--window-position=-32000,-32000");
            driver = new ChromeDriver(chromeDriverService, options);
            try
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
                driver.Navigate().GoToUrl("http://www.mi-rfc.com.mx/consulta-rfc-homoclave");
                IWebElement nombre = driver.FindElement(By.Id("nameInput"));
                IWebElement paterno = driver.FindElement(By.Id("fatherSurnameInput"));
                IWebElement materno = driver.FindElement(By.Id("motherSurnameInput"));
                IWebElement dia = driver.FindElement(By.Name("birth_day"));
                IWebElement mes = driver.FindElement(By.Name("birth_month"));
                IWebElement anio = driver.FindElement(By.Name("birth_year"));
                IWebElement rfc = driver.FindElement(By.XPath(".//div[@class='controls no-bottom']/a"));

                nombre.SendKeys(txtNombre.Text);
                paterno.SendKeys(txtApPaterno.Text);
                materno.SendKeys(txtApMaterno.Text);
                dia.SendKeys(dtpFechaNacimiento.Value.Day.ToString());
                mes.SendKeys(Mes(dtpFechaNacimiento.Value.Month));
                anio.SendKeys(dtpFechaNacimiento.Value.Year.ToString());

                rfc.Click();

                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));

                IWebElement clave = driver.FindElement(By.XPath(".//div[@class='text']/span"));
                valorRFC = clave.Text;
                driver.Close();
                driver.Dispose();
                return valorRFC;
            }
            catch (Selenium.SeleniumException error)
            {
                driver.Close();
                driver.Dispose();
                MessageBox.Show("Se encontró un error al obtener el RFC. \r\n\r\n Error: \r\n\r\n " + error.Message);
                return "";
            }

        }

        private string Mes(int valor)
        {
            string mes = "";
            switch (valor) {

                case 1: mes = "Enero"; break;
                case 2: mes = "Febrero"; break;
                case 3: mes = "Marzo"; break;
                case 4: mes = "Abril"; break;
                case 5: mes = "Mayo"; break;
                case 6: mes = "Junio"; break;
                case 7: mes = "Julio"; break;
                case 8: mes = "Agosto"; break;
                case 9: mes = "Septiembre"; break;
                case 10: mes = "Octubre"; break;
                case 11: mes = "Noviembre"; break;
                case 12: mes = "Diciembre"; break; 
            }
            return mes;
        }

        private void cmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTipoSalario.Enabled = true;
            txtSueldo.Enabled = true;
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (txtAntiguedad.Text.Length == 0)
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
                f.anio = int.Parse(txtAntiguedad.Text);

                try
                {
                    cnx.Open();
                    DiasDePago = (int)ph.DiasDePago(p);
                    FactorDePago = double.Parse(fh.FactorDePago(f).ToString());
                    cnx.Close();
                    cnx.Dispose();

                    //txtSD.Text = (double.Parse(txtSueldo.Text) / DiasDePago).ToString("F6");
                    //txtSDI.Text = (double.Parse(txtSD.Text) * FactorDePago).ToString("F6");

                    txtSD.Text = (double.Parse(txtSDI.Text) / FactorDePago).ToString("F6");
                    txtSueldo.Text = (double.Parse(txtSD.Text) * DiasDePago).ToString("F6");
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    this.Dispose();
                }
            }
        }

        private void toolGuardarCerrar_Click(object sender, EventArgs e)
        {
            guardar(1);
        }

        private void toolGuardarNuevo_Click(object sender, EventArgs e)
        {
            guardar(0);
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void guardar(int tipoGuardar)
        {
            int existe = 0;
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

            if (txtNSS.Text.Length != 11)
            {
                MessageBox.Show("El campo NSS es mayor o meno a 11 dígitos.", "Error");
                return;
            }

            int idtrabajador;

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            Empleados.Core.Empleados existeEmpleado = new Empleados.Core.Empleados();
            existeEmpleado.nss = txtNSS.Text.Trim().Substring(0, 10);
            existeEmpleado.digitoverificador = int.Parse(txtNSS.Text.Trim().Substring(10, 1));
            existeEmpleado.idempresa = GLOBALES.IDEMPRESA;

            int existeNss;
            try
            {
                cnx.Open();
                existeNss = (int)eh.existeEmpleado(existeEmpleado);
                cnx.Close();
            }
            catch
            {
                MessageBox.Show("Error al validar existencia de empleado.", "Error");
                return;
            }

            Empleados.Core.Empleados em = new Empleados.Core.Empleados();
            em.nombres = txtNombre.Text;
            em.paterno = txtApPaterno.Text;
            em.materno = txtApMaterno.Text;
            em.noempleado = mtxtNoEmpleado.Text;
            em.nombrecompleto = txtApPaterno.Text + (string.IsNullOrEmpty(txtApMaterno.Text) ? "" : " " + txtApMaterno.Text) + " " + txtNombre.Text;
            em.fechaingreso = dtpFechaIngreso.Value;
            em.antiguedad = int.Parse(txtAntiguedad.Text);
            em.fechaantiguedad = dtpFechaIngreso.Value;
            em.fechanacimiento = dtpFechaNacimiento.Value;
            em.antiguedadmod = 0;
            em.edad = int.Parse(txtEdad.Text);
            em.idempresa = GLOBALES.IDEMPRESA;
            em.rfc = txtRFC.Text;
            em.curp = txtCURP.Text;
            em.nss = txtNSS.Text.Trim().Substring(0, 10);
            em.digitoverificador = int.Parse(txtNSS.Text.Trim().Substring(10, 1));

            em.idperiodo = int.Parse(cmbPeriodo.SelectedValue.ToString());
            em.idsalario = 0;
            em.iddepartamento = int.Parse(cmbDepartamento.SelectedValue.ToString());
            em.idpuesto = int.Parse(cmbPuesto.SelectedValue.ToString());
            em.tiposalario = int.Parse(cmbTipoSalario.SelectedValue.ToString());
            em.tiporegimen = 0;

            em.sdi = decimal.Parse(txtSDI.Text);
            em.sd = decimal.Parse(txtSD.Text);
            em.sueldo = decimal.Parse(txtSueldo.Text);

            em.cuenta = mtxtCuentaBancaria.Text;
            em.clabe = mtxtCuentaClabe.Text;
            em.idbancario = mtxtIdBancario.Text;
            em.metodopago = "NA";
            //em.metodopago = int.Parse(cmbMetodoPago.SelectedValue.ToString());

            if (chkObraCivil.Checked)
                em.obracivil = true;
            else
                em.obracivil = false;

            //hh = new Historial.Core.HistorialHelper();
            //hh.Command = cmd;
            //Historial.Core.Historial h = new Historial.Core.Historial();
            //h.idempresa = GLOBALES.IDEMPRESA;
            //h.tipomovimiento = GLOBALES.mALTA;
            //h.valor = decimal.Parse(txtSDI.Text);
            //h.fecha_imss = dtpFechaIngreso.Value;
            //h.fecha_sistema = DateTime.Now;
            //h.motivobaja = 0;
            //h.iddepartamento = int.Parse(cmbDepartamento.SelectedValue.ToString());
            //h.idpuesto = int.Parse(cmbPuesto.SelectedValue.ToString());

            ih = new Imagen.Core.ImagenesHelper();
            ih.Command = cmd;

            Imagen.Core.Imagenes img = null;

            //Empresas.Core.EmpresasHelper empresash = new Empresas.Core.EmpresasHelper();
            //empresash.Command = cmd;
            //Empresas.Core.Empresas empresa = new Empresas.Core.Empresas();
            //empresa.idempresa = GLOBALES.IDEMPRESA;

            try
            {
                if (ImagenAsignada == true)
                {
                    img = new Imagen.Core.Imagenes();
                    img.imagen = GLOBALES.IMAGEN_BYTES(bmp);
                    img.tipopersona = GLOBALES.pEMPLEADO;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Error");
            }

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        //Empleados.Core.EmpleadosEstatus ee = new Empleados.Core.EmpleadosEstatus();
                        //ee.estatus = GLOBALES.ACTIVO;
                        //ee.idempresa = GLOBALES.IDEMPRESA;

                        em.estatus = GLOBALES.ACTIVO;
                        em.idusuario = GLOBALES.IDUSUARIO;
                        em.iddepartamento = int.Parse(cmbDepartamento.SelectedValue.ToString());
                        em.idpuesto = int.Parse(cmbPuesto.SelectedValue.ToString());

                        if (existeNss != 0)
                        {
                            MessageBox.Show("El empleado que desea ingresar ya existe actualmente. \r\n \r\n Es necesario realizar un reingreso.", "Error");
                            return;
                        }

                        cnx.Open();
                        eh.insertaEmpleado(em);
                        idtrabajador = (int)eh.obtenerIdTrabajador(em);

                        //h.idtrabajador = idtrabajador;
                        //hh.insertarHistorial(h);

                        //ee.idtrabajador = idtrabajador;
                        //eh.insertaEmpleadoEstatus(ee);

                        //a.idtrabajador = idtrabajador;
                        //a.registropatronal = empresash.obtenerRegistroPatronal(empresa).ToString();
                        //ah.insertaAlta(a);

                        if (ImagenAsignada == true)
                        {
                            img.idpersona = idtrabajador;
                            ih.insertaImagen(img);
                        }

                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar el empleado. \r\n \r\n Error: " + error.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        em.idtrabajador = _idempleado;

                        pdh = new Periodos.Core.PeriodosHelper();
                        pdh.Command = cmd;

                        Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                        p.idperiodo = int.Parse(cmbPeriodo.SelectedValue.ToString());
                        int diasPago = 0;
                        try { cnx.Open(); diasPago = (int)pdh.DiasDePago(p); cnx.Close(); }
                        catch { MessageBox.Show("Error: Al obtener los dias de pago.", "Error"); }

                        DateTime dt = dtpFechaIngreso.Value.Date;
                        DateTime periodoInicio, periodoFin;
                        int diasProporcionales = 0;
                        if (diasPago == 7)
                        {
                            while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                            periodoInicio = dt;
                            periodoFin = dt.AddDays(6);
                            diasProporcionales = (int)(periodoFin.Date - dtpFechaIngreso.Value.Date).TotalDays + 1;
                        }
                        else
                        {
                            if (dt.Day <= 15)
                            {
                                periodoInicio = new DateTime(dt.Year, dt.Month, 1);
                                periodoFin = new DateTime(dt.Year, dt.Month, 15);
                                diasProporcionales = (int)(periodoFin.Date - dtpFechaIngreso.Value.Date).TotalDays + 1;
                            }
                            else
                            {
                                int diasMes = DateTime.DaysInMonth(dt.Year, dt.Month);
                                int diasNoLaborados = 0;
                                periodoInicio = new DateTime(dt.Year, dt.Month, 16);
                                periodoFin = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
                                diasNoLaborados = (int)(dtpFechaIngreso.Value.Date - periodoInicio).TotalDays;

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

                        Altas.Core.AltasHelper ah = new Altas.Core.AltasHelper();
                        ah.Command = cmd;
                        Altas.Core.Altas a = new Altas.Core.Altas();
                        a.idempresa = GLOBALES.IDEMPRESA;
                        a.contrato = 4;
                        a.jornada = 12;
                        a.nss = txtNSS.Text;
                        a.rfc = txtRFC.Text;
                        a.curp = txtCURP.Text;
                        a.paterno = txtApPaterno.Text;
                        a.materno = txtApMaterno.Text;
                        a.nombre = txtNombre.Text;
                        a.fechaingreso = dtpFechaIngreso.Value;
                        a.diasproporcionales = diasProporcionales;
                        a.sdi = decimal.Parse(txtSDI.Text);
                        a.fechanacimiento = dtpFechaNacimiento.Value;
                        a.estado = cmbEstado.Text;
                        a.noestado = int.Parse(cmbEstado.SelectedValue.ToString());
                        a.sexo = ObtieneSexo();
                        a.periodoInicio = periodoInicio;
                        a.periodoFin = periodoFin;

                        cnx.Open();
                        eh.actualizaEmpleado(em);

                        a.idtrabajador = _idempleado;
                        ah.actualizaAlta(a);

                        if (ImagenAsignada == true)
                        {
                            img.idpersona = _idempleado;
                            img.tipopersona = GLOBALES.pEMPLEADO;
                            existe = (int)ih.ExisteImagen(img);
                            if (existe != 0)
                                ih.actualizaImagen(img);
                            else
                                ih.insertaImagen(img);
                        }
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar el empleado. \r\n \r\n Error: " + error.Message);
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    GLOBALES.LIMPIAR(this, typeof(TextBox));
                    GLOBALES.LIMPIAR(this, typeof(MaskedTextBox));
                    GLOBALES.REFRESCAR(this, typeof(ComboBox));
                    break;
                case 1:
                    if (OnNuevoEmpleado != null)
                        OnNuevoEmpleado(_tipoOperacion);
                    this.Dispose();
                    break;
            }
        }

        private void dtpFechaIngreso_Leave(object sender, EventArgs e)
        {
            txtAntiguedad.Text = ObtieneEdad(dtpFechaIngreso.Value).ToString();
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
            else
            {
                sexo = "X";
            }

            return sexo.ToString();
        }

        private string ObtieneEstado()
        {
            switch (cmbEstado.Text)
            {
                case "AGUASCALIENTES": estado = "AS";
                    break;
                case "BAJA CALIFORNIA": estado = "BC";
                    break;
                case "BAJA CALIFORNIA SUR": estado = "BS";
                    break;
                case "CAMPECHE": estado = "CC";
                    break;
                case "CHIAPAS": estado = "CS";
                    break;
                case "CHIHUAHUA": estado = "CH";
                    break;
                case "COAHUILA": estado = "CL";
                    break;
                case "COLIMA": estado = "CM";
                    break;
                case "DISTRITO FEDERAL": estado = "DF";
                    break;
                case "DURANGO": estado = "DG";
                    break;
                case "GUANAJUATO": estado = "GT";
                    break;
                case "GUERRERO": estado = "GR";
                    break;
                case "HIDALGO": estado = "HG";
                    break;
                case "JALISCO": estado = "JC";
                    break;
                case "MEXICO": estado = "MC";
                    break;
                case "MICHOACAN": estado = "MN";
                    break;
                case "MORELOS": estado = "MS";
                    break;
                case "NAYARIT": estado = "NT";
                    break;
                case "NUEVO LEON": estado = "NL";
                    break;
                case "OAXACA": estado = "OC";
                    break;
                case "PUEBLA": estado = "PL";
                    break;
                case "QUERETARO": estado = "QT";
                    break;
                case "QUINTANA ROO": estado = "QR";
                    break;
                case "SAN LUIS POTOSI": estado = "SP";
                    break;
                case "SINALOA": estado = "SL";
                    break;
                case "SONORA": estado = "SR";
                    break;
                case "TABASCO": estado = "TC";
                    break;
                case "TAMAULIPAS": estado = "TS";
                    break;
                case "TLAXCALA": estado = "TL";
                    break;
                case "VERACRUZ": estado = "VZ";
                    break;
                case "YUCATAN": estado = "YN";
                    break;
                case "ZACATECAS": estado = "ZS";
                    break;
            }
            return estado;
        }

        private int ObtieneEdad(DateTime fecha)
        {
            DateTime fechaNacimiento = fecha;
            int edad = (DateTime.Now.Subtract(fechaNacimiento).Days / 365);
            return edad;
        }

        private void btnObtenerCurp_Click(object sender, EventArgs e)
        {
            Empleados.Core.CURP curp = new Empleados.Core.CURP();
            string rfc = curp.ObtieneRFC_CURP(txtApPaterno.Text, txtApMaterno.Text, txtNombre.Text);
            string fecha = dtpFechaNacimiento.Value.Date.ToString("yyMMdd");
            string _sexo = ObtieneSexo();
            string estado = curp.TablaEstados(cmbEstado.Text);
            string consonantes = curp.ConsonantesRFC(txtApPaterno.Text, txtApMaterno.Text, txtNombre.Text);
            string dv = curp.DigitoVerificador(rfc + fecha + _sexo + estado + consonantes);
            txtCURP.Text = rfc + fecha + _sexo + estado + consonantes + "0" + dv;
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleccionar imagen";
            ofd.Filter = "Archivo de Imagen (*.jpg, *.png, *.bmp)|*.jpg; *.png; *.bmp";
            ofd.RestoreDirectory = false;

            if (DialogResult.OK == ofd.ShowDialog())
            {
                bmp = new Bitmap(ofd.FileName);
                ImagenAsignada = true;
            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            frmImagen i = new frmImagen();
            i._idpersona = _idempleado;
            i._tipopersona = GLOBALES.pEMPLEADO;
            i.Show();
        }

        private void toolHistorial_Click(object sender, EventArgs e)
        {
            frmListaHistorial lh = new frmListaHistorial();
            lh._idempleado = _idempleado;
            lh.Show();
        }

        private void cmbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCURP_Leave(object sender, EventArgs e)
        {
            if (txtCURP.Text.Length == 0 || txtCURP.Text.Length < 18 || txtCURP.Text.Length >= 19)
            {
                MessageBox.Show("Verifique el CURP, ya que es menor a 18 digitos o los excede.", "Error");
                return;
            }


            int numero17 = 0;
            string posicion17 = txtCURP.Text.Substring(16, 1);
            string anio = txtCURP.Text.Substring(4, 2);
            string mes = txtCURP.Text.Substring(6, 2);
            string dia = txtCURP.Text.Substring(8, 2);
            string estado = txtCURP.Text.Substring(11, 2);
            string sexo = txtCURP.Text.Substring(10, 1);
            DateTime fechaNacimiento;
            try
            {
                numero17 = int.Parse(posicion17);
                fechaNacimiento = new DateTime(int.Parse("19" + anio), int.Parse(mes), int.Parse(dia));
                dtpFechaNacimiento.Value = fechaNacimiento;
            }
            catch
            {
                fechaNacimiento = new DateTime(int.Parse("20" + anio), int.Parse(mes), int.Parse(dia));
                dtpFechaNacimiento.Value = fechaNacimiento;
            }

            switch (estado)
            {
                case "AS": estado = "AGUASCALIENTES"; break;
                case "BC": estado = "BAJA CALIFORNIA"; break;
                case "BS": estado = "BAJA CALIFORNIA SUR"; break;
                case "CC": estado = "CAMPECHE"; break;
                case "CL": estado = "COAHUILA"; break;
                case "CM": estado = "COLIMA"; break;
                case "CS": estado = "CHIAPAS"; break;
                case "CH": estado = "CHIHUAHUA"; break;
                case "DF": estado = "DISTRITO FEDERAL"; break;
                case "DG": estado = "DURANGO"; break;
                case "GT": estado = "GUANAJUATO"; break;
                case "GR": estado = "GUERRERO"; break;
                case "HG": estado = "HIDALGO"; break;
                case "JC": estado = "JALISCO"; break;
                case "MC": estado = "MEXICO"; break;
                case "MN": estado = "MICHOACAN"; break;
                case "MS": estado = "MORELOS"; break;
                case "NT": estado = "NAYARIT"; break;
                case "NL": estado = "NUEVO LEON"; break;
                case "OC": estado = "OAXACA"; break;
                case "PL": estado = "PUEBLA"; break;
                case "QT": estado = "QUERETARO"; break;
                case "QR": estado = "QUINTANA ROO"; break;
                case "SP": estado = "SAN LUIS POTOSI"; break;
                case "SL": estado = "SINALOA"; break;
                case "SR": estado = "SONORA"; break;
                case "TC": estado = "TABASCO"; break;
                case "TS": estado = "TAMAULIPAS"; break;
                case "TL": estado = "TLAXCALA"; break;
                case "VZ": estado = "VERACRUZ"; break;
                case "YN": estado = "YUCATAN"; break;
                case "ZS": estado = "ZACATECAS"; break;
                default: MessageBox.Show("Verifique el CURP, es incorrecta.", "Error");
                    return;
            }
            cmbEstado.SelectedIndex = cmbEstado.FindString(estado);

            if (sexo == "H")
                rbtnHombre.Checked = true;
            else
                rbtnMujer.Checked = true;

            dtpFechaNacimiento_Leave(sender, e);
        }

        private void mtxtNoEmpleado_Leave(object sender, EventArgs e)
        {
            int existeNoEmpleado = 0;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            try
            {
                cnx.Open();
                existeNoEmpleado = (int)eh.existeEmpleado(GLOBALES.IDEMPRESA, mtxtNoEmpleado.Text);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Al obtener la existencia del No. de Empleado." + error.Message, "Error");
                cnx.Dispose();
                return;
            }

            if (existeNoEmpleado != 0)
            {
                MessageBox.Show("El No. de Empleado ya existe. Verifique.");
            }
        }
    }
}

