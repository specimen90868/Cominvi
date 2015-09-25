﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

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
        SqlConnection cnx;
        SqlCommand cmd;
        #endregion

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            toolEstatusPerfil.Text = "";
            MenuInicial(0);
        }

        private void workPerfil_DoWork(object sender, DoWorkEventArgs e)
        {
            cnx = new SqlConnection();
            cmd = new SqlCommand();

            cnx.ConnectionString = cdn;
            cmd.Connection = cnx;

            Autorizaciones.Core.AutorizacionHelper ah = new Autorizaciones.Core.AutorizacionHelper();
            ah.Command = cmd;

            cnx.Open();

            List<Autorizaciones.Core.Autorizaciones> lstAuth = ah.getAutorizacion(GLOBALES.IDUSUARIO);
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
                    case "Expedientes":
                        mnuExpedientes.Enabled = Convert.ToBoolean(lstMenu[i].ver);
                        break;
                    case "Empresas":
                        mnuEmpresa.Enabled = Convert.ToBoolean(lstMenu[i].ver);
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

        private void mnuPerfiles_Click(object sender, EventArgs e)
        {
            frmListaPerfiles lp = new frmListaPerfiles();
            lp.MdiParent = this;
            lp.Show();
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

        private void mnuFactores_Click_1(object sender, EventArgs e)
        {
            frmListaFactores lf = new frmListaFactores();
            lf.MdiParent = this;
            lf.Show();
        }

        private void mnuSalarioMinimo_Click_1(object sender, EventArgs e)
        {
            frmListaSalario ls = new frmListaSalario();
            ls.MdiParent = this;
            ls.Show();
        }

        private void mnuEmpleadoNomina_Click(object sender, EventArgs e)
        {
            frmListaEmpleados le = new frmListaEmpleados();
            le._empleadoAltaBaja = GLOBALES.ACTIVO;
            le.MdiParent = this;
            le.Show();
        }

        private void mnuComplementos_Click(object sender, EventArgs e)
        {
            frmListaComplementos lc = new frmListaComplementos();
            lc.MdiParent = this;
            lc.Show();
        }

        private void mnuEmpresa_Click(object sender, EventArgs e)
        {
            frmListaEmpresas le = new frmListaEmpresas();
            le.MdiParent = this;
            le.WindowState = FormWindowState.Maximized;
            le.Show();
        }

        private void mnuUsuarios_Click(object sender, EventArgs e)
        {
            frmListaUsuarios lu = new frmListaUsuarios();
            lu.MdiParent = this;
            lu.WindowState = FormWindowState.Maximized;
            lu.Show();
        }

        private void mnuCambiarContrasenia_Click(object sender, EventArgs e)
        {

        }

        private void mnuPreferencias_Click(object sender, EventArgs e)
        {

        }

        private void mnuEmpleadosBaja_Click(object sender, EventArgs e)
        {
            frmListaEmpleados le = new frmListaEmpleados();
            le._empleadoAltaBaja = GLOBALES.INACTIVO;
            le.MdiParent = this;
            le.Show();
        }

        private void toolProcesoSalarial_Click(object sender, EventArgs e)
        {
            frmListaProcesoSalarial lps = new frmListaProcesoSalarial();
            lps.MdiParent = this;
            lps.Show();
        }

        private void mnuExpedientes_Click(object sender, EventArgs e)
        {
            frmListaExpedientes le = new frmListaExpedientes();
            le.MdiParent = this;
            le.Show();
        }

        private void mnuInfonavit_Click(object sender, EventArgs e)
        {
            frmListaInfonavit li = new frmListaInfonavit();
            li.MdiParent = this;
            li.Show();
        }

        private void toolAltas_Click(object sender, EventArgs e)
        {
            frmListaAltasSua las = new frmListaAltasSua();
            las.MdiParent = this;
            las.Show();
        }

        private void toolBajas_Click(object sender, EventArgs e)
        {
            frmListaBajasSua lbs = new frmListaBajasSua();
            lbs.MdiParent = this;
            lbs.Show();
        }

        private void toolAusentismos_Click(object sender, EventArgs e)
        {
            frmListaAusentimosSua las = new frmListaAusentimosSua();
            las.MdiParent = this;
            las.Show();
        }

        private void toolModificaciones_Click(object sender, EventArgs e)
        {
            frmListaModificacionesSua lms = new frmListaModificacionesSua();
            lms.MdiParent = this;
            lms.Show();
        }

        private void toolReingresos_Click(object sender, EventArgs e)
        {
            frmListaReingresosSua lrs = new frmListaReingresosSua();
            lrs.MdiParent = this;
            lrs.Show();
        }

        private void toolInfonavit_Click(object sender, EventArgs e)
        {
            frmListaInfonavitSua lis = new frmListaInfonavitSua();
            lis.MdiParent = this;
            lis.Show();
        }

        private void toolAltasIdse_Click(object sender, EventArgs e)
        {
            frmListaOperacionesIdse loi = new frmListaOperacionesIdse();
            loi.MdiParent = this;
            loi._tipoOperacion = 0;
            loi.Show();
        }

        private void toolModificacionIdse_Click(object sender, EventArgs e)
        {
            frmListaOperacionesIdse loi = new frmListaOperacionesIdse();
            loi.MdiParent = this;
            loi._tipoOperacion = 1;
            loi.Show();
        }

        private void toolBaja_Click(object sender, EventArgs e)
        {
            frmListaOperacionesIdse loi = new frmListaOperacionesIdse();
            loi.MdiParent = this;
            loi._tipoOperacion = 2;
            loi.Show();
        }

        private void toolIsr_Click(object sender, EventArgs e)
        {
            frmListaIsr li = new frmListaIsr();
            li.MdiParent = this;
            li.Show();
        }

        private void toolSubsidio_Click(object sender, EventArgs e)
        {
            frmListaSubsidio ls = new frmListaSubsidio();
            ls.MdiParent = this;
            ls.Show();
        }

        private void toolConceptos_Click(object sender, EventArgs e)
        {
            frmListaConceptos lc = new frmListaConceptos();
            lc.MdiParent = this;
            lc.Show();
        }

        private void toolConceptoEmpleado_Click(object sender, EventArgs e)
        {
            frmListaConceptoEmpleado lce = new frmListaConceptoEmpleado();
            lce.MdiParent = this;
            lce.Show();
        }

        private void toolImss_Click(object sender, EventArgs e)
        {
            frmListaImss li = new frmListaImss();
            li.MdiParent = this;
            li.Show();
        }

        private void toolHistorialFaltas_Click(object sender, EventArgs e)
        {
            frmListaFaltas lf = new frmListaFaltas();
            lf.MdiParent = this;
            lf.Show();
        }

        private void toolCargaFaltas_Click(object sender, EventArgs e)
        {
            frmListaCargaFaltas lcf = new frmListaCargaFaltas();
            lcf.MdiParent = this;
            lcf.Show();
        }

        private void toolHistorialIncapacidad_Click(object sender, EventArgs e)
        {
            frmListaIncapacidad li = new frmListaIncapacidad();
            li.MdiParent = this;
            li.Show();
        }

        private void toolCargaIncapacidad_Click(object sender, EventArgs e)
        {
            frmListaCargaIncapacidades lci = new frmListaCargaIncapacidades();
            lci.MdiParent = this;
            lci.Show();
        }

        private void toolVacaciones_Click(object sender, EventArgs e)
        {
            frmListaCargaVacaciones lcv = new frmListaCargaVacaciones();
            lcv.MdiParent = this;
            lcv.Show();
        }

        private void cálculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListaCalculoNomina lcn = new frmListaCalculoNomina();
            lcn.MdiParent = this;
            lcn.Show();
        }
      
    }
}

