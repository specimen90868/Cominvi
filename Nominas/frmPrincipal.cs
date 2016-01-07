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
            MenuInicial(0);
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
            MenuPerfil();
            MenuInicial(2);
            Permisos();
            workAntiguedad.RunWorkerAsync();
        }

        private void Permisos()
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
                        mnuRecursosHumanos.Enabled = lstAuth[i].acceso;
                        break;
                    case "Seguro Social":
                        mnuSeguroSocial.Enabled = lstAuth[i].acceso;
                        break;
                    case "Nominas":
                        mnuNominas.Enabled = lstAuth[i].acceso;
                        break;
                    case "Catálogos":
                        mnuCatalogos.Enabled = lstAuth[i].acceso;
                        break;
                    case "Configuración":
                        mnuConfiguracion.Enabled = lstAuth[i].acceso;
                        break;
                }
            }

            for (int i = 0; i < lstMenu.Count; i++)
            {
                switch (lstMenu[i].nombre.ToString())
                {
                    case "Empleados":
                        mnuEmpleados.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Empleados de nómina":
                        mnuEmpleadoNomina.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Complementos":
                        mnuComplementos.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Empleados en Baja":
                        mnuEmpleadosBaja.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Expedientes":
                        mnuExpedientes.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Infonavit":
                        mnuInfonavit.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Incapacidades":
                        toolIncapacidades.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Operaciones SUA":
                        toolSua.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Altas":
                        toolAltas.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Modificaciones":
                        toolModificaciones.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Reingresos":
                        toolReingresos.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Bajas":
                        toolBajas.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Ausentismos":
                        toolAusentismos.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Infonavit SUA":
                        toolInfonavit.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Operaciones Idse":
                        toolIdse.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Altas Idse":
                        toolAltasIdse.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Modificaciones Idse":
                        toolModificacionIdse.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Bajas Idse":
                        toolBajaIdse.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Vacaciones":
                        //toolVacaciones.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Cargar Vacaciones":
                        //toolCargaVacaciones.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Historial de Vacaciones":
                        //toolHistorialVacaciones.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Conceptos - Empleado":
                        //tool.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Programación de concepto":
                        //toolProgramacion.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Movimientos":
                        //toolMovimientos.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Carga movimientos":
                        //toolCargaMovimientos.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Historial de movimientos":
                        //toolHistorialMovimientos.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Calculo de nómina":
                        toolCalculoNomina.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Normal":
                        toolNominaNormal.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Especial":
                        toolNominaEspecial.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Autorizar nómina":
                        toolAutorizarNomina.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Reportes":
                        toolReportes.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Departamentos":
                        mnuDepartamentos.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Puestos":
                        mnuPuestos.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Periodos":
                        mnuPeriodos.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Conceptos":
                        toolConceptos.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Empresas":
                        mnuEmpresa.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Usuarios":
                        mnuUsuarios.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Perfiles":
                        mnuPerfiles.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Cambiar contraseña":
                        mnuCambiarContrasenia.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Preferencias":
                        mnuPreferencias.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Tablas":
                        mnuTablas.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Factores":
                        mnuFactores.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Salario minimo":
                        mnuSalarioMinimo.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "ISR":
                        toolIsr.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "Subsidio":
                        toolSubsidio.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                    case "IMSS":
                        toolImss.Enabled = Convert.ToBoolean(lstMenu[i].accion);
                        break;
                }
            }
        }

        private void MenuInicial(int sesion)
        {
            if (sesion == 0)
            {
                mnuRecursosHumanos.Visible = false;
                mnuSeguroSocial.Visible = false;
                mnuNominas.Visible = false;
                mnuCatalogos.Visible = false;
                mnuConfiguracion.Visible = false;
                /// MENUS DE SESION
                mnuAbrirEmpresa.Enabled = true;
                mnuCerrarEmpresa.Enabled = false;
                mnuCerrarSesion.Enabled = true;
                mnuIniciarSesion.Enabled = false;
            }
            else if (sesion == 2)
            {
                mnuAbrirEmpresa.Enabled = false;
                mnuCerrarEmpresa.Enabled = true;
                mnuCerrarSesion.Enabled = true;
                mnuIniciarSesion.Enabled = false;
            }
            else
            {
                mnuRecursosHumanos.Visible = false;
                mnuSeguroSocial.Visible = false;
                mnuNominas.Visible = false;
                mnuCatalogos.Visible = false;
                mnuConfiguracion.Visible = false;

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
            frmCambioContrasena cp = new frmCambioContrasena();
            cp.MdiParent = this;
            cp.Show();
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

        private void toolImss_Click(object sender, EventArgs e)
        {
            frmListaImss li = new frmListaImss();
            li.MdiParent = this;
            li.Show();
        }

        private void toolNominaNormal_Click(object sender, EventArgs e)
        {
            frmSeleccionPeriodo sp = new frmSeleccionPeriodo();
            sp._TipoNomina = GLOBALES.NORMAL;
            sp.MdiParent = this;
            sp.Show();
        }

        private void toolNominaEspecial_Click(object sender, EventArgs e)
        {
            frmSeleccionPeriodo sp = new frmSeleccionPeriodo();
            sp._TipoNomina = GLOBALES.ESPECIAL;
            sp.MdiParent = this;
            sp.Show();
        }

        private void toolReportes_Click(object sender, EventArgs e)
        {
            frmReportes r = new frmReportes();
            r.MdiParent = this;
            r.Show();
        }

        private void toolIncapacidades_Click(object sender, EventArgs e)
        {
            frmListaIncapacidad i = new frmListaIncapacidad();
            i.MdiParent = this;
            i.Show();
        }

        private void toolAutorizarNomina_Click(object sender, EventArgs e)
        {

        }

        private void toolConceptoEmpleado_Click_1(object sender, EventArgs e)
        {
            frmListaConceptoEmpleado lce = new frmListaConceptoEmpleado();
            lce.MdiParent = this;
            lce.Show();
        }

        private void toolProgramacionConcepto_Click(object sender, EventArgs e)
        {
            frmListaProgramacionConceptos lpc = new frmListaProgramacionConceptos();
            lpc.MdiParent = this;
            lpc.Show();
        }

        private void toolExtraordinario_Click(object sender, EventArgs e)
        {
            frmSeleccionPeriodo sp = new frmSeleccionPeriodo();
            sp._TipoNomina = GLOBALES.EXTRAORDINARIO_NORMAL;
            sp.MdiParent = this;
            sp.Show();
        }

        private void toolExtraordinarioEspecial_Click(object sender, EventArgs e)
        {
            frmSeleccionPeriodo sp = new frmSeleccionPeriodo();
            sp._TipoNomina = GLOBALES.EXTRAORDINARIO_ESPECIAL;
            sp.MdiParent = this;
            sp.Show();
        }

        private void workAntiguedad_DoWork(object sender, DoWorkEventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            List<Empleados.Core.Empleados> lstFechas = new List<Empleados.Core.Empleados>();

            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;

            try
            {
                cnx.Open();
                lstFechas = eh.obtenerFechaAntiguedad(empleado);
                cnx.Close();

                int antiguedad = 0, antiguedadmod = 0;
                DateTime fechaAntiguedad, fechaAntiguedadMod;
                int progreso = 0;
                int total = lstFechas.Count;
                for (int i = 0; i < lstFechas.Count; i++)
                {
                    progreso = (i * 100) / total;
                    workAntiguedad.ReportProgress(progreso);

                    fechaAntiguedad = lstFechas[i].fechaingreso;
                    antiguedad = (DateTime.Now.Subtract(fechaAntiguedad).Days / 365);

                    fechaAntiguedadMod = lstFechas[i].fechaantiguedad;
                    antiguedadmod = (DateTime.Now.Subtract(fechaAntiguedadMod).Days / 365);

                    empleado = new Empleados.Core.Empleados();
                    empleado.antiguedad = antiguedad;
                    empleado.antiguedadmod = antiguedadmod;
                    empleado.idtrabajador = lstFechas[i].idtrabajador;

                    try
                    {
                        cnx.Open();
                        eh.actualizaAntiguedad(empleado);
                        cnx.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Error: Al actualizar la antiguedad del trabajador. ID: " + lstFechas[i].idtrabajador + "\r\n Se detendra la actualización.", "Error");
                        cnx.Dispose();
                        return;
                    }
                }
                workAntiguedad.ReportProgress(100);
                cnx.Dispose();
            }
            catch
            {
                MessageBox.Show("Error: Al obtener las fechas del trabajador. \r\n Incremento de Antiguedad.", "Error");
                cnx.Dispose();
                return;
            }
        }

        private void workAntiguedad_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolPorcentaje.Text = e.ProgressPercentage + "%";
        }

        private void workAntiguedad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolPorcentaje.Text = "Terminado.";
        }
      
    }
}

