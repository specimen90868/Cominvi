using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Nominas
{
    public partial class frmEmpresas : Form
    {
        public frmEmpresas()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Empresas.Core.EmpresasHelper eh;
        Direccion.Core.DireccionesHelper dh;
        Imagen.Core.ImagenesHelper ih;
        Bitmap bmp;
        bool ImagenAsignada = false;
        #endregion
        
        #region DELEGADOS
        public delegate void delOnNuevaEmpresa(int edicion);
        public event delOnNuevaEmpresa OnNuevaEmpresa;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _idempresa;
        public int _iddireccion;
        #endregion

        private void toolGuardarCerrar_Click(object sender, EventArgs e)
        {
            guardar(1);
        }

        private void toolGuardarNuevo_Click(object sender, EventArgs e)
        {
            guardar(0);   
        }

        private void guardar(int tipoGuardar)
        {
            int existe = 0;
            //SE VALIDA SI TODOS LOS TEXTBOX HAN SIDO LLENADOS.
            string control = GLOBALES.VALIDAR(this,typeof(TextBox));
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

            int idempresa;

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            eh = new Empresas.Core.EmpresasHelper();
            eh.Command = cmd;

            Empresas.Core.Empresas em = new Empresas.Core.Empresas();
            em.nombre = txtNombre.Text;
            em.rfc = txtRfc.Text;
            em.registro = txtRegistroPatronal.Text;
            em.digitoverificador = int.Parse(txtDigitoVerificador.Text);
            em.representante = txtRepresentante.Text;
            em.estatus = 1;
            em.regimen = txtRegimen.Text;
            em.certificado = txtCertificado.Text;
            em.llave = txtLlave.Text;
            em.password = txtPassword.Text;
            em.nocertificado = txtNoCertificado.Text;
            em.vigenciacertificado = dtpVigencia.Value.Date;

            dh = new Direccion.Core.DireccionesHelper();
            dh.Command = cmd;

            Direccion.Core.Direcciones d = new Direccion.Core.Direcciones();
            d.calle = txtCalle.Text;
            d.exterior = txtExterior.Text;
            d.interior = txtInterior.Text;
            d.colonia = txtColonia.Text;
            d.ciudad = txtMunicipio.Text;
            d.estado = txtEstado.Text;
            d.pais = txtPais.Text;
            d.cp = txtCP.Text;
            d.tipodireccion = GLOBALES.dFISCAL;
            d.tipopersona = GLOBALES.pEMPRESA;

            ih = new Imagen.Core.ImagenesHelper();
            ih.Command = cmd;

            Imagen.Core.Imagenes img = null;

            try
            {
                if (ImagenAsignada == true)
                {
                    img = new Imagen.Core.Imagenes();
                    img.imagen = GLOBALES.IMAGEN_BYTES(bmp);
                    img.tipopersona = GLOBALES.pEMPRESA;
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
                        cnx.Open();
                        eh.insertaEmpresa(em);
                        idempresa = (int)eh.obtenerIdEmpresa(em);
                        d.idpersona = idempresa;
                        dh.insertaDireccion(d);
                        if (ImagenAsignada == true)
                        {
                            img.idpersona = idempresa;
                            ih.insertaImagen(img);
                        }
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar la empresa. \r\n \r\n Error: " + error.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        em.idempresa = _idempresa;
                        d.iddireccion = _iddireccion;
                        d.idpersona = _idempresa;

                        cnx.Open();
                        eh.actualizaEmpresa(em);
                        dh.actualizaDireccion(d);
                        if (ImagenAsignada == true)
                        {
                            img.idpersona = _idempresa;
                            img.tipopersona = GLOBALES.pEMPRESA;
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
                        MessageBox.Show("Error al actualizar la empresa. \r\n \r\n Error: " + error.Message);
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    GLOBALES.LIMPIAR(this,typeof(TextBox));
                    GLOBALES.LIMPIAR(this, typeof(MaskedTextBox));
                    //limpiar(this, typeof(TextBox));
                    break;
                case 1:
                    if (OnNuevaEmpresa != null)
                        OnNuevaEmpresa(_tipoOperacion);
                    this.Dispose();
                    break;
            }
        }

        private void txtRfc_Leave(object sender, EventArgs e)
        {
            string lsPatron = @"^[A-ZÑ&]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9][A-Z,0-9][0-9A]$";
            Regex loRE = new Regex(lsPatron);
            if (!loRE.IsMatch(txtRfc.Text))
                MessageBox.Show("El RFC no es valido. Verifique","Error");
        }

        private void frmEmpresas_Load(object sender, EventArgs e)
        {
            /// _tipoOperacion CONSULTA = 1, EDICION = 2
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                eh = new Empresas.Core.EmpresasHelper();
                eh.Command = cmd;

                dh = new Direccion.Core.DireccionesHelper();
                dh.Command = cmd;

                Direccion.Core.Direcciones d = new Direccion.Core.Direcciones();
                d.idpersona = _idempresa;
                d.tipopersona = GLOBALES.pEMPRESA; ///TIPO PERSONA 0 - Empresas
                List<Empresas.Core.Empresas> lstEmpresa;
                List<Direccion.Core.Direcciones> lstDireccion;

                try
                {
                    cnx.Open();
                    lstEmpresa = eh.obtenerEmpresa(_idempresa);
                    lstDireccion = dh.obtenerDireccion(d);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstEmpresa.Count; i++)
                    {
                        txtNombre.Text = lstEmpresa[i].nombre;
                        txtRepresentante.Text = lstEmpresa[i].representante;
                        txtRfc.Text = lstEmpresa[i].rfc;
                        txtRegistroPatronal.Text = lstEmpresa[i].registro;
                        txtDigitoVerificador.Text = lstEmpresa[i].digitoverificador.ToString();
                        txtRegimen.Text = lstEmpresa[i].regimen.ToString();
                        txtCertificado.Text = lstEmpresa[i].certificado;
                        txtLlave.Text = lstEmpresa[i].llave;
                        txtPassword.Text = lstEmpresa[i].password;
                        txtNoCertificado.Text = lstEmpresa[i].nocertificado;
                        dtpVigencia.Value = lstEmpresa[i].vigenciacertificado;
                    }

                    for (int i = 0; i < lstDireccion.Count; i++)
                    {
                        _iddireccion = lstDireccion[i].iddireccion;
                        txtCalle.Text = lstDireccion[i].calle;
                        txtExterior.Text = lstDireccion[i].exterior;
                        txtInterior.Text = lstDireccion[i].interior;
                        txtColonia.Text = lstDireccion[i].colonia;
                        txtCP.Text = lstDireccion[i].cp;
                        txtMunicipio.Text = lstDireccion[i].ciudad;
                        txtEstado.Text = lstDireccion[i].estado;
                        txtPais.Text = lstDireccion[i].pais;
                    }

                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
                
                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta Empresa";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                    GLOBALES.INHABILITAR(this, typeof(MaskedTextBox));
                    GLOBALES.INHABILITAR(this, typeof(CheckBox));
                    btnAsignar.Enabled = false;
                }
                else
                    toolTitulo.Text = "Edición Empresa";
            }
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
            i._idpersona = _idempresa;
            i._tipopersona = GLOBALES.pEMPRESA;
            i.Show();
        }

        private void btnExaminarCertificado_Click(object sender, EventArgs e)
        {
            OpenFileDialog pfd = new OpenFileDialog();
            pfd.Title = "Seleccionar Certificado Digital";
            pfd.InitialDirectory = @"C:\";
            pfd.Filter = "Certificado Digital|*.cer";
            pfd.RestoreDirectory = true;
            if (DialogResult.OK == pfd.ShowDialog())
            {
                txtCertificado.Text = pfd.FileName;
            }
        }

        private void btnExaminarLlave_Click(object sender, EventArgs e)
        {
            OpenFileDialog pfd = new OpenFileDialog();
            pfd.Title = "Seleccionar Llave Digital";
            pfd.InitialDirectory = @"C:\";
            pfd.Filter = "Llave Digital|*.key";
            pfd.RestoreDirectory = true;
            if (DialogResult.OK == pfd.ShowDialog())
            {
                txtLlave.Text = pfd.FileName;
            }
        }
    }
}
