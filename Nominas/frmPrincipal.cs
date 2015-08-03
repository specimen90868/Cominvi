using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Nominas
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        MySqlConnection cnx;
        MySqlCommand cmd;
        #endregion

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            toolEstatusPerfil.Text = "";
            MenuInicial(0);
        }

        private void workPerfil_DoWork(object sender, DoWorkEventArgs e)
        {
            cnx = new MySqlConnection();
            cmd = new MySqlCommand();

            cnx.ConnectionString = cdn;
            cmd.Connection = cnx;

            Autorizaciones.Core.AutorizacionHelper ah = new Autorizaciones.Core.AutorizacionHelper();
            ah.Command = cmd;

            cnx.Open();

            List<Autorizaciones.Core.Autorizaciones> lstAuth = ah.getAutorizacion(GLOBALES.IDUSUARIO.ToString());
            List<Autorizaciones.Core.Menus> lstMenu = ah.getMenus(GLOBALES.IDPERFIL.ToString());

            cnx.Close();
            cnx.Dispose();

            for (int i = 0; i < lstAuth.Count; i++)
            {
                switch (lstAuth[i].modulo.ToString())
                {
                    case "Recursos Humanos":
                        mnuRecursosHumanos.Enabled = Convert.ToBoolean(lstAuth[i].acceso);
                        break;
                    case "Seguro Social":
                        mnuSeguroSocial.Enabled = Convert.ToBoolean(lstAuth[i].acceso);
                        break;
                    case "Contratos":
                        mnuContratos.Enabled = Convert.ToBoolean(lstAuth[i].acceso);
                        break;
                    case "Nominas":
                        mnuNominas.Enabled = Convert.ToBoolean(lstAuth[i].acceso);
                        break;
                    case "Catálogos":
                        mnuCatalogos.Enabled = Convert.ToBoolean(lstAuth[i].acceso);
                        break;
                    case "Configuración":
                        mnuConfiguracion.Enabled = Convert.ToBoolean(lstAuth[i].acceso);
                        break;
                }
            }

            for (int i = 0; i < lstMenu.Count; i++)
            {
                switch (lstMenu[i].nombre.ToString())
                {
                    case "Empleados":
                        mnuEmpleados.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Fotografías":
                        mnuFotografias.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Expedientes":
                        mnuExpedientes.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Bajas":
                        mnuBajas.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Empresas":
                        mnuEmpresa.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Clientes":
                        mnuClientes.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Departamentos":
                        mnuDepartamentos.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Puestos":
                        mnuPuestos.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Periodos":
                        mnuPeriodos.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Factores":
                        mnuFactores.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Salario minimo":
                        mnuSalarioMinimo.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Plazas":
                        mnuPlazas.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Usuarios":
                        mnuUsuarios.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Perfiles":
                        mnuPerfiles.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Preferencias":
                        mnuPreferencias.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                }
            }

            workPerfil.ReportProgress(100);
        }

        private void workPerfil_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //cargaPerfil.Value = e.ProgressPercentage;
            toolEstatusPerfil.Text = "Cargando perfil...";
        }

        private void mnuAbrirEmpresa_Click(object sender, EventArgs e)
        {
            if (GLOBALES.IDEMPRESA != 0)
            {
                foreach (Form frm in this.MdiChildren)
                {
                    frm.Dispose();
                }
            }

            frmSeleccionarEmpresa frmEmpresa = new frmSeleccionarEmpresa();
            frmEmpresa.OnAbrirEmpresa += frmEmpresa_OnAbrirEmpresa;
            frmEmpresa.MdiParent = this;
            frmEmpresa.Show();
        }

        void frmEmpresa_OnAbrirEmpresa()
        {
            this.Text = "Sistema de Nomina - [" + GLOBALES.NOMBREEMPRESA + "]";
            workPerfil.RunWorkerAsync();
            MenuPerfil();
        }

        private void MenuInicial(int sesion)
        {
            mnuRecursosHumanos.Visible = false;
            mnuSeguroSocial.Visible = false;
            mnuContratos.Visible = false;
            mnuNominas.Visible = false;
            mnuCatalogos.Visible = false;
            mnuConfiguracion.Visible = false;
            if (sesion == 0)
            {
                /// MENUS DE SESION
                mnuAbrirEmpresa.Enabled = true;
                mnuCerrarEmpresa.Enabled = true;
                mnuCerrarSesion.Enabled = true;
                mnuIniciarSesion.Enabled = false;
            }
            else
            {
                mnuAbrirEmpresa.Enabled = false;
                mnuCerrarEmpresa.Enabled = false;
                mnuCerrarSesion.Enabled = false;
                mnuIniciarSesion.Enabled = true;
            }
        }

        private void MenuPerfil()
        {
            mnuRecursosHumanos.Visible = true;
            mnuSeguroSocial.Visible = true;
            mnuContratos.Visible = true;
            mnuNominas.Visible = true;
            mnuCatalogos.Visible = true;
            mnuConfiguracion.Visible = true;
        }

        private void mniIniciarSesion_Click(object sender, EventArgs e)
        {
            frmLogIn login = new frmLogIn();
            login.ShowDialog();
            MenuInicial(0);
        }

        private void mnuCerrarSesion_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Dispose();
            }

            GLOBALES.IDUSUARIO = 0;
            GLOBALES.IDPERFIL = 0;
            GLOBALES.IDEMPRESA = 0;
            GLOBALES.NOMBREEMPRESA = null;
            this.Text = "Sistema de Nomina";
            mnuRecursosHumanos.Visible = false;
            mnuSeguroSocial.Visible = false;
            mnuContratos.Visible = false;
            mnuNominas.Visible = false;
            mnuCatalogos.Visible = false;
            mnuConfiguracion.Visible = false;
            MenuInicial(1);
        }

        private void mnuCerrarEmpresa_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Dispose();
            }
            GLOBALES.NOMBREEMPRESA = null;
            GLOBALES.IDEMPRESA = 0;
            this.Text = "Sistema de Nomina";
            MenuInicial(0);
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void workPerfil_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolEstatusPerfil.Text = "Perfil cargado.";
        }

        private void mnuPlazas_Click(object sender, EventArgs e)
        {
            frmListaPlazas lp = new frmListaPlazas();
            lp.MdiParent = this;
            lp.Show();
        }

        private void mnuPerfiles_Click(object sender, EventArgs e)
        {
            frmListaPerfiles lp = new frmListaPerfiles();
            lp.MdiParent = this;
            lp.Show();
        }

        private void mnuClientes_Click(object sender, EventArgs e)
        {
            frmListaClientes c = new frmListaClientes();
            c.MdiParent = this;
            c.Show();
        }

        private void mnuDepartamentos_Click(object sender, EventArgs e)
        {
            frmListaDepartamentos ld = new frmListaDepartamentos();
            ld.MdiParent = this;
            ld.Show();
        }

        private void mnuPuestos_Click(object sender, EventArgs e)
        {
            frmListaPuestos lp = new frmListaPuestos();
            lp.MdiParent = this;
            lp.Show();
        }

        private void mnuPeriodos_Click(object sender, EventArgs e)
        {
            frmListaPeriodos lp = new frmListaPeriodos();
            lp.MdiParent = this;
            lp.Show();
        }

        private void mnuFactores_Click(object sender, EventArgs e)
        {
            frmListaFactores lf = new frmListaFactores();
            lf.MdiParent = this;
            lf.Show();
        }

        private void mnuSalarioMinimo_Click(object sender, EventArgs e)
        {
            frmListaSalario ls = new frmListaSalario();
            ls.MdiParent = this;
            ls.Show();
        }

        private void mnuEmpleadoNomina_Click(object sender, EventArgs e)
        {
            frmListaEmpleados le = new frmListaEmpleados();
            le.MdiParent = this;
            le.Show();
        }

        private void mnuModificarCliente_Click(object sender, EventArgs e)
        {
            frmModificaClienteEmpleado mce = new frmModificaClienteEmpleado();
            mce.MdiParent = this;
            mce.Show();
        }

        private void mnuModificarSueldo_Click(object sender, EventArgs e)
        {
            frmModificaSueldoEmpleado mse = new frmModificaSueldoEmpleado();
            mse.MdiParent = this;
            mse.Show();
        }

        private void mnuComplementos_Click(object sender, EventArgs e)
        {
            frmListaComplementos lc = new frmListaComplementos();
            lc.MdiParent = this;
            lc.Show();
        }

        private void mnuEmpresas_Click_1(object sender, EventArgs e)
        {
            frmListaEmpresas le = new frmListaEmpresas();
            le.MdiParent = this;
            le.WindowState = FormWindowState.Maximized;
            le.Show();
        }

        private void mnuLogo_Click(object sender, EventArgs e)
        {
            frmListaImagenes li = new frmListaImagenes();
            li.MdiParent = this;
            li._EmpresaEmpleado = GLOBALES.EMPRESAS;
            li.Show();
        }
    }
}

