using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nominas
{
    public partial class frmComplementos : Form
    {
        public frmComplementos()
        {
            InitializeComponent();
        }

        #region VARIABLES PUBLICAS
        public int _idEmpleado;
        public string _nombreEmpleado;
        public int _tipoOperacion;
        #endregion

        #region VARIABLES GLOBALES
        MySqlConnection cnx;
        MySqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Complementos.Core.ComplementoHelper ch;
        Direccion.Core.DireccionesHelper dh;
        int idDireccion;
        int idComplemento;
        #endregion

        private void CargaComboBox()
        {
            cnx = new MySqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new MySqlCommand();
            cmd.Connection = cnx;

            Catalogos.Core.CatalogosHelper cath = new Catalogos.Core.CatalogosHelper();
            Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
            Puestos.Core.PuestosHelper ph = new Puestos.Core.PuestosHelper();
            cath.Command = cmd;
            dh.Command = cmd;
            ph.Command = cmd;

            Catalogos.Core.Catalogo contrato = new Catalogos.Core.Catalogo();
            contrato.grupodescripcion = "CONTRATO";
            Catalogos.Core.Catalogo jornada = new Catalogos.Core.Catalogo();
            jornada.grupodescripcion = "JORNADA";
            Catalogos.Core.Catalogo estadocivil = new Catalogos.Core.Catalogo();
            estadocivil.grupodescripcion = "ESTADO CIVIL";
            Catalogos.Core.Catalogo sexo = new Catalogos.Core.Catalogo();
            sexo.grupodescripcion = "SEXO";
            Catalogos.Core.Catalogo escolaridad = new Catalogos.Core.Catalogo();
            escolaridad.grupodescripcion = "ESCOLARIDAD";

            List<Catalogos.Core.Catalogo> lstContrato = new List<Catalogos.Core.Catalogo>();
            List<Catalogos.Core.Catalogo> lstJornada = new List<Catalogos.Core.Catalogo>();
            List<Catalogos.Core.Catalogo> lstEstadoCivil = new List<Catalogos.Core.Catalogo>();
            List<Catalogos.Core.Catalogo> lstSexo = new List<Catalogos.Core.Catalogo>();
            List<Catalogos.Core.Catalogo> lstEscolaridad = new List<Catalogos.Core.Catalogo>();

            List<Departamento.Core.Depto> lstDeptos = new List<Departamento.Core.Depto>();
            List<Puestos.Core.Puestos> lstPuestos = new List<Puestos.Core.Puestos>();

            try
            {
                cnx.Open();
                lstContrato = cath.obtenerGrupo(contrato);
                lstJornada = cath.obtenerGrupo(jornada);
                lstEstadoCivil = cath.obtenerGrupo(estadocivil);
                lstSexo = cath.obtenerGrupo(sexo);
                lstEscolaridad = cath.obtenerGrupo(escolaridad);
                lstDeptos = dh.obtenerDepartamentos();
                lstPuestos = ph.obtenerPuestos();
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                this.Dispose();
            }
            cmbContrato.DataSource = lstContrato.ToList();
            cmbContrato.DisplayMember = "descripcion";
            cmbContrato.ValueMember = "valor";

            cmbJornada.DataSource = lstJornada.ToList();
            cmbJornada.DisplayMember = "descripcion";
            cmbJornada.ValueMember = "valor";

            cmbEstadoCivil.DataSource = lstEstadoCivil.ToList();
            cmbEstadoCivil.DisplayMember = "descripcion";
            cmbEstadoCivil.ValueMember = "valor";

            cmbSexo.DataSource = lstSexo.ToList();
            cmbSexo.DisplayMember = "descripcion";
            cmbSexo.ValueMember = "valor";

            cmbEscolaridad.DataSource = lstEscolaridad.ToList();
            cmbEscolaridad.DisplayMember = "descripcion";
            cmbEscolaridad.ValueMember = "valor";

            cmbDepartamento.DataSource = lstDeptos.ToList();
            cmbDepartamento.DisplayMember = "descripcion";
            cmbDepartamento.ValueMember = "id";

            cmbPuesto.DataSource = lstPuestos.ToList();
            cmbPuesto.DisplayMember = "descripcion";
            cmbPuesto.ValueMember = "id";
        }

        private void frmComplementos_Load(object sender, EventArgs e)
        {
            CargaComboBox();
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new MySqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new MySqlCommand();
                cmd.Connection = cnx;
                ch = new Complementos.Core.ComplementoHelper();
                dh = new Direccion.Core.DireccionesHelper();
                ch.Command = cmd;
                dh.Command = cmd;

                List<Complementos.Core.Complemento> lstComplemento;
                List<Direccion.Core.Direcciones> lstDireccion;

                Complementos.Core.Complemento c = new Complementos.Core.Complemento();
                c.idtrabajador = _idEmpleado;

                Direccion.Core.Direcciones d = new Direccion.Core.Direcciones();
                d.idpersona = _idEmpleado;
                d.tipopersona = 2;

                try
                {
                    cnx.Open();
                    lstComplemento = ch.obtenerComplemento(c);
                    lstDireccion = dh.obtenerDireccion(d);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstComplemento.Count; i++)
                    {
                        idComplemento = lstComplemento[i].id;
                        cmbContrato.SelectedValue = lstComplemento[i].contrato;
                        cmbJornada.SelectedValue = lstComplemento[i].jornada;
                        cmbDepartamento.SelectedValue = lstComplemento[i].iddepartamento;
                        cmbPuesto.SelectedValue = lstComplemento[i].idpuesto;
                        cmbEstadoCivil.SelectedValue = lstComplemento[i].estadocivil;
                        cmbSexo.SelectedValue = lstComplemento[i].sexo;
                        cmbEscolaridad.SelectedValue = lstComplemento[i].escolaridad;
                        txtHorario.Text = lstComplemento[i].horario;
                        txtNoControl.Text = lstComplemento[i].nocontrol;
                        txtClinica.Text = lstComplemento[i].clinica;
                        txtNacionalidad.Text = lstComplemento[i].nacionalidad;
                        txtFunciones.Text = lstComplemento[i].funciones;
                    }

                    for (int j = 0; j < lstDireccion.Count; j++)
                    {
                        idDireccion = lstDireccion[j].iddireccion;
                        txtCalle.Text = lstDireccion[j].calle;
                        txtExterior.Text = lstDireccion[j].exterior;
                        txtInterior.Text = lstDireccion[j].interior;
                        txtColonia.Text = lstDireccion[j].colonia;
                        txtCP.Text = lstDireccion[j].cp;
                        txtMunicipio.Text = lstDireccion[j].ciudad;
                        txtEstado.Text = lstDireccion[j].estado;
                        txtPais.Text = lstDireccion[j].pais;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta del Complemento";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                    GLOBALES.INHABILITAR(this, typeof(MaskedTextBox));
                    GLOBALES.INHABILITAR(this, typeof(ComboBox));
                }
                else
                    toolTitulo.Text = "Edición del Complemento";
            }
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
            ch = new Complementos.Core.ComplementoHelper();
            dh = new Direccion.Core.DireccionesHelper();
            ch.Command = cmd;
            dh.Command = cmd;

            Direccion.Core.Direcciones d = new Direccion.Core.Direcciones();
            d.idpersona = _idEmpleado;
            d.calle = txtCalle.Text;
            d.exterior = txtExterior.Text;
            d.interior = txtInterior.Text;
            d.cp = txtCP.Text;
            d.colonia = txtColonia.Text;
            d.ciudad = txtMunicipio.Text;
            d.estado = txtEstado.Text;
            d.pais = txtPais.Text;
            d.tipodireccion = 2;
            d.tipopersona = 2;

            Complementos.Core.Complemento c = new Complementos.Core.Complemento();
            c.idtrabajador = _idEmpleado;
            c.contrato = cmbContrato.SelectedValue.ToString();
            c.jornada = cmbJornada.SelectedValue.ToString();
            c.iddepartamento = int.Parse(cmbDepartamento.SelectedValue.ToString());
            c.idpuesto = int.Parse(cmbPuesto.SelectedValue.ToString());
            c.estadocivil = cmbEstadoCivil.SelectedValue.ToString();
            c.sexo = cmbSexo.SelectedValue.ToString();
            c.escolaridad = cmbEscolaridad.SelectedValue.ToString();
            c.horario = txtHorario.Text;
            c.nocontrol = txtNoControl.Text;
            c.clinica = txtClinica.Text;
            c.nacionalidad = txtNacionalidad.Text;
            c.funciones = txtFunciones.Text;
            
            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        dh.insertaDireccion(d);
                        ch.insertaComplemento(c);
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
                        d.iddireccion = idDireccion;
                        c.id = idComplemento;

                        cnx.Open();
                        ch.actualizaComplemento(c);
                        dh.actualizaDireccion(d);
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
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
