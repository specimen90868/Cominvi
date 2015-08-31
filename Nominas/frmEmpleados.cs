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
        Historial.Core.HistorialHelper hh;
        string sexo;
        string estado;
        Bitmap bmp;
        bool ImagenAsignada = false;
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

            List<Catalogos.Core.Catalogo> lstTipoSalario = new List<Catalogos.Core.Catalogo>();
            List<Departamento.Core.Depto> lstDepto = new List<Departamento.Core.Depto>();
            List<Puestos.Core.Puestos> lstPuesto = new List<Puestos.Core.Puestos>();
            List<Estados.Core.Estados> lstEstados = new List<Estados.Core.Estados>();
            List<Periodos.Core.Periodos> lstPeriodos = new List<Periodos.Core.Periodos>();

            try
            {
                cnx.Open();
                lstTipoSalario = cath.obtenerGrupo(ts);
                lstDepto = dh.obtenerDepartamentos(depto);
                lstPuesto = ph.obtenerPuestos(puesto);
                lstEstados = edoh.obtenerEstados();
                lstPeriodos = pdh.obtenerPeriodos(periodo);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message,"Error");
                this.Dispose();
            }

            cmbTipoSalario.DataSource = lstTipoSalario.ToList();
            cmbTipoSalario.DisplayMember = "descripcion";
            cmbTipoSalario.ValueMember = "id";

            cmbDepartamento.DataSource = lstDepto.ToList();
            cmbDepartamento.DisplayMember = "descripcion";
            cmbDepartamento.ValueMember = "id";

            cmbPuesto.DataSource = lstPuesto.ToList();
            cmbPuesto.DisplayMember = "descripcion";
            cmbPuesto.ValueMember = "id";

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
            /// _tipoOperacion CONSULTA = 1, EDICION = 2
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                eh = new Empleados.Core.EmpleadosHelper();
                eh.Command = cmd;

                List<Empleados.Core.Empleados> lstEmpleado;

                Empleados.Core.Empleados em = new Empleados.Core.Empleados();
                em.idtrabajador = _idempleado;

                try
                {
                    cnx.Open();
                    lstEmpleado = eh.obtenerEmpleado(em);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstEmpleado.Count; i++)
                    {
                        txtNombre.Text = lstEmpleado[i].nombres;
                        txtApPaterno.Text = lstEmpleado[i].paterno;
                        txtApMaterno.Text = lstEmpleado[i].materno;
                        txtNoEmpleado.Text = lstEmpleado[i].noempleado;
                        dtpFechaIngreso.Value = DateTime.Parse(lstEmpleado[i].fechaingreso.ToString());
                        dtpFechaAntiguedad.Value = DateTime.Parse(lstEmpleado[i].fechaantiguedad.ToString());
                        dtpFechaNacimiento.Value = DateTime.Parse(lstEmpleado[i].fechanacimiento.ToString());
                        txtAntiguedad.Text = lstEmpleado[i].antiguedad.ToString();
                        txtEdad.Text = lstEmpleado[i].edad.ToString();
                        txtAntiguedadMod.Text = lstEmpleado[i].antiguedadmod.ToString();
                        txtRFC.Text = lstEmpleado[i].rfc;
                        txtCURP.Text = lstEmpleado[i].curp;
                        txtNSS.Text = lstEmpleado[i].nss;
                        txtDigito.Text = lstEmpleado[i].digitoverificador.ToString();

                        cmbDepartamento.SelectedValue = int.Parse(lstEmpleado[i].iddepartamento.ToString());
                        cmbPuesto.SelectedValue = int.Parse(lstEmpleado[i].idpuesto.ToString());
                        cmbPeriodo.SelectedValue = int.Parse(lstEmpleado[i].idperiodo.ToString());
                        cmbTipoSalario.SelectedValue = int.Parse(lstEmpleado[i].tiposalario.ToString());

                        txtSueldo.Text = lstEmpleado[i].sueldo.ToString("F6");
                        txtSD.Text = lstEmpleado[i].sd.ToString("F6");
                        txtSDI.Text = lstEmpleado[i].sdi.ToString("F6");
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
                    toolGuardarCerrar.Enabled = false;
                    toolGuardarNuevo.Enabled = false;
                }
                else
                    toolTitulo.Text = "Edición Empleado";
            }
            else
                toolHistorial.Enabled = false;
        }

        private void dtpFechaAntiguedad_Leave(object sender, EventArgs e)
        {
            txtAntiguedadMod.Text = ObtieneEdad(dtpFechaAntiguedad.Value).ToString();
        }

        private void dtpFechaNacimiento_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApPaterno.Text) || string.IsNullOrEmpty(txtApMaterno.Text))
                return;

            Empleados.Core.RFC rfc = new Empleados.Core.RFC();
            try
            {
                string registro = rfc.RFC13Pocisiones(txtApPaterno.Text, txtApMaterno.Text, txtNombre.Text, dtpFechaNacimiento.Value.ToString("yy/MM/dd"));
                txtRFC.Text = registro.Substring(0, 4) + registro.Substring(5, 9);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message);
            }

            txtEdad.Text = ObtieneEdad(dtpFechaNacimiento.Value).ToString();
        }

        private void cmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTipoSalario.Enabled = true;
            txtSueldo.Enabled = true;
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (txtSueldo.Text.Length != 0)
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

                    txtSD.Text = (double.Parse(txtSueldo.Text) / DiasDePago).ToString("F6");
                    txtSDI.Text = (double.Parse(txtSD.Text) * FactorDePago).ToString("F6");
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

            int idtrabajador;

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados em = new Empleados.Core.Empleados();
            em.nombres = txtNombre.Text;
            em.paterno = txtApPaterno.Text;
            em.materno = txtApMaterno.Text;
            em.noempleado = txtNoEmpleado.Text;
            em.nombrecompleto = txtApPaterno.Text + (string.IsNullOrEmpty(txtApMaterno.Text) ? "" : " " + txtApMaterno.Text) + " " + txtNombre.Text;
            em.fechaingreso = dtpFechaIngreso.Value;
            em.antiguedad = int.Parse(txtAntiguedad.Text);
            em.fechaantiguedad = dtpFechaAntiguedad.Value;
            em.fechanacimiento = dtpFechaNacimiento.Value;
            em.antiguedadmod = int.Parse(txtAntiguedadMod.Text);
            em.edad = int.Parse(txtEdad.Text);
            em.idempresa = GLOBALES.IDEMPRESA;
            em.rfc = txtRFC.Text;
            em.curp = txtCURP.Text;
            em.nss = txtNSS.Text;
            em.digitoverificador = int.Parse(txtDigito.Text);

            em.iddepartamento = int.Parse(cmbDepartamento.SelectedValue.ToString());
            em.idpuesto = int.Parse(cmbPuesto.SelectedValue.ToString());
            em.idperiodo = int.Parse(cmbPeriodo.SelectedValue.ToString());
            em.tiposalario = int.Parse(cmbTipoSalario.SelectedValue.ToString());

            em.sdi = double.Parse(txtSDI.Text);
            em.sd = double.Parse(txtSD.Text);
            em.sueldo = double.Parse(txtSueldo.Text);

            hh = new Historial.Core.HistorialHelper();
            hh.Command = cmd;
            Historial.Core.Historial h = new Historial.Core.Historial();
            h.tipomovimiento = GLOBALES.mALTA;
            h.valor = double.Parse(txtSDI.Text);
            h.fecha_imss = dtpFechaIngreso.Value;
            h.fecha_sistema = DateTime.Now;
            h.motivobaja = 0;

            ih = new Imagen.Core.ImagenesHelper();
            ih.Command = cmd;

            Imagen.Core.Imagenes img = null;

            Altas.Core.AltasHelper ah = new Altas.Core.AltasHelper();
            ah.Command = cmd;
            Altas.Core.Altas a = new Altas.Core.Altas();
            a.nss = txtNSS.Text + txtDigito.Text;
            a.rfc = txtRFC.Text;
            a.curp = txtCURP.Text;
            a.paterno = txtApPaterno.Text;
            a.materno = txtApMaterno.Text;
            a.nombre = txtNombre.Text;
            a.fechaingreso = dtpFechaIngreso.Value;
            a.sdi = double.Parse(txtSDI.Text);
            a.fechanacimiento = dtpFechaNacimiento.Value;
            a.estado = cmbEstado.Text;
            a.noestado = int.Parse(cmbEstado.SelectedValue.ToString());
            a.sexo = ObtieneSexo();

            Empresas.Core.EmpresasHelper empresash = new Empresas.Core.EmpresasHelper();
            empresash.Command = cmd;
            Empresas.Core.Empresas empresa = new Empresas.Core.Empresas();
            empresa.idempresa = GLOBALES.IDEMPRESA;

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
                        em.estatus = GLOBALES.ACTIVO;
                        em.idusuario = GLOBALES.IDUSUARIO;

                        cnx.Open();
                        eh.insertaEmpleado(em);
                        idtrabajador = (int)eh.obtenerIdTrabajador(em);
                        h.idtrabajador = idtrabajador;
                        h.idempresa = GLOBALES.IDEMPRESA;
                        hh.insertarHistorial(h);

                        a.idtrabajador = idtrabajador;
                        a.idempresa = GLOBALES.IDEMPRESA;
                        a.contrato = 4;
                        a.jornada = 12;
                        a.registropatronal = empresash.obtenerRegistroPatronal(empresa).ToString();
                        ah.insertaAlta(a);

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
            dtpFechaAntiguedad.Value = dtpFechaIngreso.Value;
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
            switch(cmbEstado.Text)
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
            estado = ObtieneEstado();
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApPaterno.Text) || string.IsNullOrEmpty(txtApMaterno.Text) || ObtieneSexo().Equals("X"))
                return;
            Empleados.Core.CURP obtenerCurp = new Empleados.Core.CURP();
            try
            {
                txtCURP.Text = obtenerCurp.CURPCompleta(txtApPaterno.Text, txtApMaterno.Text, txtNombre.Text, dtpFechaNacimiento.Value.ToString("yy/MM/dd"), sexo, estado);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
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

        private void dtpFechaAntiguedad_ValueChanged(object sender, EventArgs e)
        {
            txtAntiguedadMod.Text = ObtieneEdad(dtpFechaAntiguedad.Value).ToString();
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
    }
}
