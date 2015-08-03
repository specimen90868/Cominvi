using MySql.Data.MySqlClient;
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
        MySqlConnection cnx;
        MySqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Empleados.Core.EmpleadosHelper eh;
        Imagen.Core.ImagenesHelper ih;
        //byte[] fotografia;
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
            cnx = new MySqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new MySqlCommand();
            cmd.Connection = cnx;

            Clientes.Core.ClientesHelper ch = new Clientes.Core.ClientesHelper();
            Catalogos.Core.CatalogosHelper cath = new Catalogos.Core.CatalogosHelper();
            ch.Command = cmd;
            cath.Command = cmd;

            Clientes.Core.Clientes cli = new Clientes.Core.Clientes();
            cli.plaza = GLOBALES.IDPLAZA;
            Catalogos.Core.Catalogo lf = new Catalogos.Core.Catalogo();
            lf.grupodescripcion = "LOCALFORANEO";
            Catalogos.Core.Catalogo sua = new Catalogos.Core.Catalogo();
            sua.grupodescripcion = "SUA";
            Catalogos.Core.Catalogo ts = new Catalogos.Core.Catalogo();
            ts.grupodescripcion = "SALARIO";

            List<Clientes.Core.Clientes> lstClientes = new List<Clientes.Core.Clientes>();
            List<Catalogos.Core.Catalogo> lstLocalForaneo = new List<Catalogos.Core.Catalogo>();
            List<Catalogos.Core.Catalogo> lstSua = new List<Catalogos.Core.Catalogo>();
            List<Catalogos.Core.Catalogo> lstTipoSalario = new List<Catalogos.Core.Catalogo>();

            try
            {
                cnx.Open();
                lstClientes = ch.obtenerClientes(cli);
                lstLocalForaneo = cath.obtenerGrupo(lf);
                lstSua = cath.obtenerGrupo(sua);
                lstTipoSalario = cath.obtenerGrupo(ts);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message,"Error");
                this.Dispose();
            }
            cmbCliente.DataSource = lstClientes.ToList();
            cmbCliente.DisplayMember = "nombre";
            cmbCliente.ValueMember = "idcliente";

            cmbLocalForaneo.DataSource = lstLocalForaneo.ToList();
            cmbLocalForaneo.DisplayMember = "descripcion";
            cmbLocalForaneo.ValueMember = "valor";

            cmbSua.DataSource = lstSua.ToList();
            cmbSua.DisplayMember = "descripcion";
            cmbSua.ValueMember = "valor";

            cmbTipoSalario.DataSource = lstTipoSalario.ToList();
            cmbTipoSalario.DisplayMember = "descripcion";
            cmbTipoSalario.ValueMember = "valor";
        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            CargaComboBox();
            /// _tipoOperacion CONSULTA = 1, EDICION = 2
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new MySqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new MySqlCommand();
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
                        dtpFechaNacimiento.Value = DateTime.Parse(lstEmpleado[i].fechanacimiento.ToString());
                        txtEdad.Text = lstEmpleado[i].edad.ToString();
                        dtpFechaIngreso.Value = lstEmpleado[i].fechaingreso;
                        cmbLocalForaneo.SelectedValue = lstEmpleado[i].localforaneo;
                        cmbSua.SelectedValue = lstEmpleado[i].sua;
                        txtRFC.Text = lstEmpleado[i].rfc;
                        txtCURP.Text = lstEmpleado[i].curp;
                        txtNSS.Text = lstEmpleado[i].nss;
                        txtDigito.Text = lstEmpleado[i].digitoverificador.ToString();

                        lblCliente.Visible = false;
                        lblPeriodo.Visible = false;
                        cmbCliente.Visible = false;
                        cmbPeriodo.Visible = false;

                        lblSalario.Visible = false;
                        lblTipoSalario.Visible = false;
                        lblSueldo.Visible = false;
                        lblSD.Visible = false;
                        lblSDI.Visible = false;
                        cmbTipoSalario.Visible = false;
                        txtSueldo.Visible = false;
                        txtSD.Visible = false;
                        txtSDI.Visible = false;
                        btnCalcular.Visible = false;
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
                }
                else
                    toolTitulo.Text = "Edición Empleado";
            }
        }

        private void cmbCliente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cnx = new MySqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new MySqlCommand();
            cmd.Connection = cnx;

            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos p = new Periodos.Core.Periodos();
            p.idcliente = int.Parse(cmbCliente.SelectedValue.ToString());

            List<Periodos.Core.Periodos> lstPeriodo = new List<Periodos.Core.Periodos>();

            try
            {
                cnx.Open();
                lstPeriodo = ph.obtenerPeriodoCliente(p);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                this.Dispose();
            }

            cmbPeriodo.DataSource = lstPeriodo.ToList();
            cmbPeriodo.DisplayMember = "pago";
            cmbPeriodo.ValueMember = "idperiodo";
        }

        private void dtpFechaNacimiento_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApPaterno.Text) || string.IsNullOrEmpty(txtApMaterno.Text))
                return;

            RFC.ObtieneRFC rfc = new RFC.ObtieneRFC();
            string registro = rfc.RFC13Pocisiones(txtApPaterno.Text, txtApMaterno.Text, txtNombre.Text, dtpFechaNacimiento.Value.ToString("yy/MM/dd"));
            txtRFC.Text = registro.Substring(0, 4) + registro.Substring(5, 9);

            DateTime fechaActual = DateTime.Now;
            DateTime fechaNacimiento = dtpFechaNacimiento.Value;
            double anios = (fechaActual - fechaNacimiento).TotalDays / 365;
            int aniosTruncado = (int)Math.Truncate(anios);
            if (fechaActual.Month < fechaNacimiento.Month)
                aniosTruncado -= 1;
            else if (fechaActual.Day < fechaNacimiento.Day)
                    aniosTruncado -= 1;
                    
            txtEdad.Text = aniosTruncado.ToString();
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
                cnx = new MySqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new MySqlCommand();
                cmd.Connection = cnx;

                Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
                Periodos.Core.Periodos p = new Periodos.Core.Periodos();
                Factores.Core.FactoresHelper fh = new Factores.Core.FactoresHelper();
                Factores.Core.Factores f = new Factores.Core.Factores();
                
                ph.Command = cmd;
                fh.Command = cmd;

                p.idperiodo = int.Parse(cmbPeriodo.SelectedValue.ToString());
                f.anio = 0;

                try
                {
                    cnx.Open();
                    DiasDePago = (int)ph.DiasDePago(p);
                    FactorDePago = double.Parse(fh.FactorDePago(f).ToString());
                    cnx.Close();
                    cnx.Dispose();

                    txtSD.Text = (double.Parse(txtSueldo.Text) / DiasDePago).ToString("F4");
                    txtSDI.Text = (double.Parse(txtSD.Text) * FactorDePago).ToString("F4");
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

            cnx = new MySqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new MySqlCommand();
            cmd.Connection = cnx;
            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados em = new Empleados.Core.Empleados();
            em.nombres = txtNombre.Text;
            em.paterno = txtApMaterno.Text;
            em.materno = txtApMaterno.Text;
            em.nombrecompleto = txtApPaterno.Text + (string.IsNullOrEmpty(txtApMaterno.Text) ? "" : " " + txtApMaterno.Text) + " " + txtNombre.Text;
            em.fechanacimiento = dtpFechaNacimiento.Value;
            em.edad = int.Parse(txtEdad.Text);
            em.idempresa = GLOBALES.IDEMPRESA;
            em.fechaingreso = dtpFechaIngreso.Value;
            em.localforaneo = cmbLocalForaneo.SelectedValue.ToString();
            em.sua = cmbSua.SelectedValue.ToString();
            em.rfc = txtRFC.Text;
            em.curp = txtCURP.Text;
            em.nss = txtNSS.Text;
            em.digitoverificador = int.Parse(txtDigito.Text);

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        em.antiguedad = 0;
                        em.idcliente = int.Parse(cmbCliente.SelectedValue.ToString());
                        em.idperiodo = int.Parse(cmbPeriodo.SelectedValue.ToString());
                        em.tiposalario = cmbTipoSalario.SelectedValue.ToString();
                        em.sdi = decimal.Parse(txtSDI.Text);
                        em.sd = decimal.Parse(txtSD.Text);
                        em.sueldo = decimal.Parse(txtSueldo.Text);
                        em.idinfonavit = 0;
                        em.estatus = 0;
                        em.idse = 0;
                        em.modsalario = 0;
                        em.idplaza = GLOBALES.IDPLAZA;

                        cnx.Open();
                        eh.insertaEmpleado(em);
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
                        em.idempresa = _idempleado;
                        cnx.Open();
                        eh.actualizaEmpleado(em);
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

        private byte[] ImagenByte(Image imagen)
        {
            MemoryStream ms = new MemoryStream();
            imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        private Image ByteImage(byte[] byteArray)
        {
            MemoryStream ms = new MemoryStream(byteArray);
            Image img = Image.FromStream(ms);
            return img;
        }

        //private void pbFoto_DoubleClick(object sender, EventArgs e)
        //{
        //    OpenFileDialog abrir = new OpenFileDialog();
        //    abrir.Title = "Seleccionar fotografia";
        //    abrir.Filter = "Archivo de imagen (*.jpg)|*.jpg";

        //    if (abrir.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        try
        //        {
        //            Bitmap img = new Bitmap(abrir.FileName);
        //            pbFoto.Image = img;
        //            fotografia = ImagenByte(pbFoto.Image);
        //        }
        //        catch (Exception error)
        //        {
        //            MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
        //        }
        //    }
        //}
    }
}
