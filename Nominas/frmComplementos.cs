using System.Data.SqlClient;
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
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Complementos.Core.ComplementoHelper ch;
        Direccion.Core.DireccionesHelper dh;
        Altas.Core.AltasHelper ah;
        int idDireccion;
        int idComplemento;
        #endregion

        private void CargaComboBox()
        {
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Catalogos.Core.CatalogosHelper cath = new Catalogos.Core.CatalogosHelper();
            cath.Command = cmd;

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

            try
            {
                cnx.Open();
                lstContrato = cath.obtenerGrupo(contrato);
                lstJornada = cath.obtenerGrupo(jornada);
                lstEstadoCivil = cath.obtenerGrupo(estadocivil);
                lstSexo = cath.obtenerGrupo(sexo);
                lstEscolaridad = cath.obtenerGrupo(escolaridad);
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
            cmbContrato.ValueMember = "id";

            cmbJornada.DataSource = lstJornada.ToList();
            cmbJornada.DisplayMember = "descripcion";
            cmbJornada.ValueMember = "id";

            cmbEstadoCivil.DataSource = lstEstadoCivil.ToList();
            cmbEstadoCivil.DisplayMember = "descripcion";
            cmbEstadoCivil.ValueMember = "id";

            cmbSexo.DataSource = lstSexo.ToList();
            cmbSexo.DisplayMember = "descripcion";
            cmbSexo.ValueMember = "id";

            cmbEscolaridad.DataSource = lstEscolaridad.ToList();
            cmbEscolaridad.DisplayMember = "descripcion";
            cmbEscolaridad.ValueMember = "id";
        }

        private void frmComplementos_Load(object sender, EventArgs e)
        {
            CargaComboBox();
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
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
                d.tipopersona = GLOBALES.pEMPLEADO;

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
                        cmbEstadoCivil.SelectedValue = lstComplemento[i].estadocivil;
                        cmbSexo.SelectedValue = lstComplemento[i].sexo;
                        cmbEscolaridad.SelectedValue = lstComplemento[i].escolaridad;
                        txtClinica.Text = lstComplemento[i].clinica;
                        txtNacionalidad.Text = lstComplemento[i].nacionalidad;
                        mTelEmpresa.Text = lstComplemento[i].tempresa1;
                        mTelEmpresa2.Text = lstComplemento[i].tempresa2;
                        mTelEmpresa3.Text = lstComplemento[i].tempresa3;
                        txtExtension.Text = lstComplemento[i].extension;
                        mCelEmpresa.Text = lstComplemento[i].cempresa;
                        mTelPersonal.Text = lstComplemento[i].cpersonal;
                        txtCorreo.Text = lstComplemento[i].email;
                        txtObservaciones.Text = lstComplemento[i].observaciones;
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
                    toolGuardar.Enabled = false;
                }
                else
                    toolTitulo.Text = "Edición del Complemento";
            }
            else
            {
                lblEmpleado.Text = _nombreEmpleado;
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

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            ch = new Complementos.Core.ComplementoHelper();
            dh = new Direccion.Core.DireccionesHelper();
            ah = new Altas.Core.AltasHelper();
            ch.Command = cmd;
            dh.Command = cmd;
            ah.Command = cmd;

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
            d.tipodireccion = GLOBALES.dPERSONAL;
            d.tipopersona = GLOBALES.pEMPLEADO;

            Complementos.Core.Complemento c = new Complementos.Core.Complemento();
            c.idtrabajador = _idEmpleado;
            c.contrato = int.Parse(cmbContrato.SelectedValue.ToString());
            c.jornada = int.Parse(cmbJornada.SelectedValue.ToString());
            c.estadocivil = int.Parse(cmbEstadoCivil.SelectedValue.ToString());
            c.sexo = int.Parse(cmbSexo.SelectedValue.ToString());
            c.escolaridad = int.Parse(cmbEscolaridad.SelectedValue.ToString());
            c.clinica = txtClinica.Text;
            c.nacionalidad = txtNacionalidad.Text;
            c.tempresa1 = mTelEmpresa.Text;
            c.tempresa2 = mTelEmpresa2.Text;
            c.tempresa3 = mTelEmpresa3.Text;
            c.extension = txtExtension.Text;
            c.cempresa = mCelEmpresa.Text;
            c.cpersonal = mTelPersonal.Text;
            c.email = txtCorreo.Text;
            c.observaciones = txtObservaciones.Text;

            Altas.Core.Altas a = new Altas.Core.Altas();
            a.idtrabajador = _idEmpleado;
            a.jornada = int.Parse(cmbJornada.SelectedValue.ToString());
            a.contrato = int.Parse(cmbContrato.SelectedValue.ToString());
            a.cp = txtCP.Text;
            a.clinica = txtClinica.Text;
            
            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        dh.insertaDireccion(d);
                        ch.insertaComplemento(c);
                        ah.actualizaAltaComplemento(a);
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
                        ah.actualizaAltaComplemento(a);
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

            this.Dispose();
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
