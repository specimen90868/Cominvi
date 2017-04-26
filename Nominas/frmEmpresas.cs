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
        DateTime inicioPeriodo, finPeriodo;
        bool capturaFecha = false;
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
        public bool _lista;
        #endregion


        private void guardar(int tipoGuardar)
        {
            int existe = 0;
            //SE VALIDA SI TODOS LOS TEXTBOX HAN SIDO LLENADOS.
            string control = GLOBALES.VALIDAR(tabEmpresa, typeof(TextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            control = GLOBALES.VALIDAR(tabEmpresa, typeof(MaskedTextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            control = GLOBALES.VALIDAR(tabDomicilio, typeof(TextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            control = GLOBALES.VALIDAR(tabDomicilio, typeof(MaskedTextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            control = GLOBALES.VALIDAR(tabTimbrado, typeof(TextBox));
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
            em.regimen = "";
            em.certificado = txtCertificado.Text;
            em.llave = txtLlave.Text;
            em.password = txtPassword.Text;
            em.nocertificado = txtNoCertificado.Text;
            em.vigenciacertificado = dtpVigencia.Value.Date;
            em.observacion = txtObservacion.Text;
            em.obracivil = chkObraCivil.Checked;
            em.idregimenfiscal = int.Parse(cmbRegimenFiscal.SelectedValue.ToString());
            em.codigopostal = txtCP.Text;

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

            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
            periodo.dias = int.Parse(txtDias.Text);
            periodo.estatus = GLOBALES.ACTIVO;
            periodo.pago = cmbPago.Text;

            //CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            //nh.Command = cmd;

            //CalculoNomina.Core.PagoNomina pn = new CalculoNomina.Core.PagoNomina();
            //pn.idtrabajador = 0;
            //pn.idconcepto = 0;
            //pn.noconcepto = 0;
            //pn.tipoconcepto = "P";
            //pn.exento = 0;
            //pn.gravado = 0;
            //pn.cantidad = 0;
            //pn.fechainicio = inicioPeriodo;
            //pn.fechafin = finPeriodo;
            //pn.noperiodo = 0;
            //pn.diaslaborados = 0;
            //pn.idusuario = 0;
            //pn.tiponomina = 0;
            //pn.fechapago = finPeriodo;
            //pn.iddepartamento = 0;
            //pn.idpuesto = 0;            

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
                        periodo.idempresa = idempresa;
                        ph.insertaPeriodo(periodo);
                        //pn.idempresa = idempresa;
                        //nh.insertaPrimerPeriodoNomina(pn);
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
            CargaComboBox();
            if (!_lista)
                toolGuardarNuevo.Visible = false;
            /// _tipoOperacion CONSULTA = 1, EDICION = 2
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cmbPago.Visible = false;
                txtDias.Visible = false;

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
                        txtObservacion.Text = lstEmpresa[i].observacion;
                        txtDigitoVerificador.Text = lstEmpresa[i].digitoverificador.ToString();
                        txtCertificado.Text = lstEmpresa[i].certificado;
                        txtLlave.Text = lstEmpresa[i].llave;
                        txtPassword.Text = lstEmpresa[i].password;
                        txtNoCertificado.Text = lstEmpresa[i].nocertificado;
                        dtpVigencia.Value = lstEmpresa[i].vigenciacertificado;
                        chkObraCivil.Checked = lstEmpresa[i].obracivil;
                        cmbRegimenFiscal.SelectedValue = lstEmpresa[i].idregimenfiscal;
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
                    ((Control)this.tabEmpresa).Enabled = false;
                    ((Control)this.tabPeriodo).Enabled = false;
                    ((Control)this.tabDomicilio).Enabled = false;
                    ((Control)this.tabTimbrado).Enabled = false;
                    btnAsignar.Enabled = false;
                    toolGuardarCerrar.Enabled = false;
                    toolGuardarNuevo.Enabled = false;
                    
                }
                else
                    toolTitulo.Text = "Edición Empresa";
            }
            
                
            cmbPago.SelectedIndex = 0;
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

        private void toolGuardarCerrar_Click(object sender, EventArgs e)
        {
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                guardar(1);
            }
            else
            {
                guardar(1);
                //frmInicioPeriodo ip = new frmInicioPeriodo();
                //ip._periodo = int.Parse(txtDias.Text);
                //ip.OnNuevoPeriodo += ip_OnNuevoPeriodo;
                //ip.ShowDialog();
                //if (capturaFecha)
                //    guardar(1);
                //else
                //    return;
            }

        }

        void ip_OnNuevoPeriodo(DateTime inicio, DateTime fin)
        {
            if (inicio.Date != new DateTime(1900, 1, 1).Date)
            {
                if (int.Parse(txtDias.Text) == 7)
                {
                    inicioPeriodo = inicio.AddDays(-7);
                    finPeriodo = inicio.AddDays(-1);
                }
                else
                {
                    if (inicio.Day <= 15)
                    {
                        inicioPeriodo = inicio.AddDays(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - 15);
                        finPeriodo = inicio.AddDays(-1);
                    }
                    else
                    {
                        inicioPeriodo = inicio.AddDays(-15);
                        finPeriodo = inicio.AddDays(-1);
                    }
                }

                capturaFecha = true;
            }
            else
                capturaFecha = false;
        }

        private void toolGuardarNuevo_Click(object sender, EventArgs e)
        {
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                guardar(1);
            }
            else
            {
                guardar(0);
                //frmInicioPeriodo ip = new frmInicioPeriodo();
                //ip._periodo = int.Parse(txtDias.Text);
                //ip.OnNuevoPeriodo += ip_OnNuevoPeriodo;
                //ip.ShowDialog();
                //if (capturaFecha)
                //    guardar(0);
                //else
                //    return;
            }
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmbPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPago.SelectedIndex.Equals(1))
            {
                txtDias.Text = "15";
            }
            else
            {
                txtDias.Text = "7";
            }
        }

        private void CargaComboBox()
        {
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            SatCatalogos.Core.satCatalogosHelper sch = new SatCatalogos.Core.satCatalogosHelper();
            sch.Command = cmd;
            List<SatCatalogos.Core.satRegimenFiscal> lstRegimenFiscal = new List<SatCatalogos.Core.satRegimenFiscal>();

            try
            {
                cnx.Open();
                lstRegimenFiscal = sch.obtenerRegimenes();
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener los datos del regimen fiscal", "Error");
                cnx.Dispose();
            }

            cmbRegimenFiscal.DataSource = lstRegimenFiscal.ToList();
            cmbRegimenFiscal.DisplayMember = "descripcion";
            cmbRegimenFiscal.ValueMember = "id";
        }
       
    }
}
