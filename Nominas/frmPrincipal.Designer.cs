namespace Nominas
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.mnuPrincipal = new System.Windows.Forms.MenuStrip();
            this.mnuArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbrirEmpresa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCerrarEmpresa = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuIniciarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRecursosHumanos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEmpleados = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEmpleadoNomina = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuComplementos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuModificarCliente = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuModificarSueldo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEmpleadoSindicato = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExpedientes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBajas = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuInfonavit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIncapacidades = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMovimientos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAlta = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuModificacion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaja = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSeguroSocial = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContratos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNominas = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCatalogos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDepartamentos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPuestos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPeriodos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFactores = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSalarioMinimo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfiguracion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEmpresa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEmpresas = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPerfiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPlazas = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCambiarContrasenia = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPreferencias = new System.Windows.Forms.ToolStripMenuItem();
            this.workPerfil = new System.ComponentModel.BackgroundWorker();
            this.stsPrincipal = new System.Windows.Forms.StatusStrip();
            this.toolEstatusPerfil = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuPrincipal.SuspendLayout();
            this.stsPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuPrincipal
            // 
            this.mnuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuArchivo,
            this.mnuRecursosHumanos,
            this.mnuSeguroSocial,
            this.mnuContratos,
            this.mnuNominas,
            this.mnuCatalogos,
            this.mnuConfiguracion});
            this.mnuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.mnuPrincipal.Name = "mnuPrincipal";
            this.mnuPrincipal.Size = new System.Drawing.Size(795, 24);
            this.mnuPrincipal.TabIndex = 1;
            this.mnuPrincipal.Text = "menuStrip1";
            // 
            // mnuArchivo
            // 
            this.mnuArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbrirEmpresa,
            this.mnuCerrarEmpresa,
            this.toolStripSeparator1,
            this.mnuIniciarSesion,
            this.mnuCerrarSesion,
            this.toolStripSeparator4,
            this.mnuSalir});
            this.mnuArchivo.Name = "mnuArchivo";
            this.mnuArchivo.Size = new System.Drawing.Size(60, 20);
            this.mnuArchivo.Text = "Archivo";
            // 
            // mnuAbrirEmpresa
            // 
            this.mnuAbrirEmpresa.Name = "mnuAbrirEmpresa";
            this.mnuAbrirEmpresa.Size = new System.Drawing.Size(154, 22);
            this.mnuAbrirEmpresa.Text = "Abrir Empresa";
            this.mnuAbrirEmpresa.Click += new System.EventHandler(this.mnuAbrirEmpresa_Click);
            // 
            // mnuCerrarEmpresa
            // 
            this.mnuCerrarEmpresa.Name = "mnuCerrarEmpresa";
            this.mnuCerrarEmpresa.Size = new System.Drawing.Size(154, 22);
            this.mnuCerrarEmpresa.Text = "Cerrar Empresa";
            this.mnuCerrarEmpresa.Click += new System.EventHandler(this.mnuCerrarEmpresa_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(151, 6);
            // 
            // mnuIniciarSesion
            // 
            this.mnuIniciarSesion.Name = "mnuIniciarSesion";
            this.mnuIniciarSesion.Size = new System.Drawing.Size(154, 22);
            this.mnuIniciarSesion.Text = "Iniciar sesión";
            this.mnuIniciarSesion.Click += new System.EventHandler(this.mniIniciarSesion_Click);
            // 
            // mnuCerrarSesion
            // 
            this.mnuCerrarSesion.Name = "mnuCerrarSesion";
            this.mnuCerrarSesion.Size = new System.Drawing.Size(154, 22);
            this.mnuCerrarSesion.Text = "Cerrar sesión";
            this.mnuCerrarSesion.Click += new System.EventHandler(this.mnuCerrarSesion_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(151, 6);
            // 
            // mnuSalir
            // 
            this.mnuSalir.Name = "mnuSalir";
            this.mnuSalir.Size = new System.Drawing.Size(154, 22);
            this.mnuSalir.Text = "Salir";
            this.mnuSalir.Click += new System.EventHandler(this.mnuSalir_Click);
            // 
            // mnuRecursosHumanos
            // 
            this.mnuRecursosHumanos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEmpleados,
            this.mnuExpedientes,
            this.mnuBajas,
            this.toolStripSeparator3,
            this.mnuInfonavit,
            this.mnuIncapacidades,
            this.toolStripSeparator5,
            this.mnuMovimientos});
            this.mnuRecursosHumanos.Name = "mnuRecursosHumanos";
            this.mnuRecursosHumanos.Size = new System.Drawing.Size(121, 20);
            this.mnuRecursosHumanos.Text = "Recursos Humanos";
            // 
            // mnuEmpleados
            // 
            this.mnuEmpleados.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEmpleadoNomina,
            this.mnuComplementos,
            this.mnuModificarCliente,
            this.mnuModificarSueldo,
            this.toolStripSeparator6,
            this.mnuEmpleadoSindicato});
            this.mnuEmpleados.Name = "mnuEmpleados";
            this.mnuEmpleados.Size = new System.Drawing.Size(152, 22);
            this.mnuEmpleados.Text = "Empleados";
            // 
            // mnuEmpleadoNomina
            // 
            this.mnuEmpleadoNomina.Name = "mnuEmpleadoNomina";
            this.mnuEmpleadoNomina.Size = new System.Drawing.Size(218, 22);
            this.mnuEmpleadoNomina.Text = "Empleados de nómina";
            this.mnuEmpleadoNomina.Click += new System.EventHandler(this.mnuEmpleadoNomina_Click);
            // 
            // mnuComplementos
            // 
            this.mnuComplementos.Name = "mnuComplementos";
            this.mnuComplementos.Size = new System.Drawing.Size(218, 22);
            this.mnuComplementos.Text = "Complementos";
            this.mnuComplementos.Click += new System.EventHandler(this.mnuComplementos_Click);
            // 
            // mnuModificarCliente
            // 
            this.mnuModificarCliente.Name = "mnuModificarCliente";
            this.mnuModificarCliente.Size = new System.Drawing.Size(218, 22);
            this.mnuModificarCliente.Text = "Modificar Cliente y Periodo";
            this.mnuModificarCliente.Click += new System.EventHandler(this.mnuModificarCliente_Click);
            // 
            // mnuModificarSueldo
            // 
            this.mnuModificarSueldo.Name = "mnuModificarSueldo";
            this.mnuModificarSueldo.Size = new System.Drawing.Size(218, 22);
            this.mnuModificarSueldo.Text = "Modificar Sueldo";
            this.mnuModificarSueldo.Click += new System.EventHandler(this.mnuModificarSueldo_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(215, 6);
            // 
            // mnuEmpleadoSindicato
            // 
            this.mnuEmpleadoSindicato.Name = "mnuEmpleadoSindicato";
            this.mnuEmpleadoSindicato.Size = new System.Drawing.Size(218, 22);
            this.mnuEmpleadoSindicato.Text = "Empleados de sindicato";
            // 
            // mnuExpedientes
            // 
            this.mnuExpedientes.Name = "mnuExpedientes";
            this.mnuExpedientes.Size = new System.Drawing.Size(152, 22);
            this.mnuExpedientes.Text = "Expedientes";
            // 
            // mnuBajas
            // 
            this.mnuBajas.Name = "mnuBajas";
            this.mnuBajas.Size = new System.Drawing.Size(152, 22);
            this.mnuBajas.Text = "Bajas";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuInfonavit
            // 
            this.mnuInfonavit.Name = "mnuInfonavit";
            this.mnuInfonavit.Size = new System.Drawing.Size(152, 22);
            this.mnuInfonavit.Text = "Infonavit";
            // 
            // mnuIncapacidades
            // 
            this.mnuIncapacidades.Name = "mnuIncapacidades";
            this.mnuIncapacidades.Size = new System.Drawing.Size(152, 22);
            this.mnuIncapacidades.Text = "Incapacidades";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuMovimientos
            // 
            this.mnuMovimientos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAlta,
            this.mnuModificacion,
            this.mnuBaja});
            this.mnuMovimientos.Name = "mnuMovimientos";
            this.mnuMovimientos.Size = new System.Drawing.Size(152, 22);
            this.mnuMovimientos.Text = "Movimientos";
            // 
            // mnuAlta
            // 
            this.mnuAlta.Name = "mnuAlta";
            this.mnuAlta.Size = new System.Drawing.Size(197, 22);
            this.mnuAlta.Text = "Alta";
            // 
            // mnuModificacion
            // 
            this.mnuModificacion.Name = "mnuModificacion";
            this.mnuModificacion.Size = new System.Drawing.Size(197, 22);
            this.mnuModificacion.Text = "Modificación de salario";
            // 
            // mnuBaja
            // 
            this.mnuBaja.Name = "mnuBaja";
            this.mnuBaja.Size = new System.Drawing.Size(197, 22);
            this.mnuBaja.Text = "Baja";
            // 
            // mnuSeguroSocial
            // 
            this.mnuSeguroSocial.Name = "mnuSeguroSocial";
            this.mnuSeguroSocial.Size = new System.Drawing.Size(90, 20);
            this.mnuSeguroSocial.Text = "Seguro Social";
            // 
            // mnuContratos
            // 
            this.mnuContratos.Name = "mnuContratos";
            this.mnuContratos.Size = new System.Drawing.Size(71, 20);
            this.mnuContratos.Text = "Contratos";
            // 
            // mnuNominas
            // 
            this.mnuNominas.Name = "mnuNominas";
            this.mnuNominas.Size = new System.Drawing.Size(67, 20);
            this.mnuNominas.Text = "Nominas";
            // 
            // mnuCatalogos
            // 
            this.mnuCatalogos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClientes,
            this.mnuDepartamentos,
            this.mnuPuestos,
            this.mnuPeriodos,
            this.mnuFactores,
            this.mnuSalarioMinimo});
            this.mnuCatalogos.Name = "mnuCatalogos";
            this.mnuCatalogos.Size = new System.Drawing.Size(72, 20);
            this.mnuCatalogos.Text = "Catálogos";
            // 
            // mnuClientes
            // 
            this.mnuClientes.Name = "mnuClientes";
            this.mnuClientes.Size = new System.Drawing.Size(155, 22);
            this.mnuClientes.Text = "Clientes";
            this.mnuClientes.Click += new System.EventHandler(this.mnuClientes_Click);
            // 
            // mnuDepartamentos
            // 
            this.mnuDepartamentos.Name = "mnuDepartamentos";
            this.mnuDepartamentos.Size = new System.Drawing.Size(155, 22);
            this.mnuDepartamentos.Text = "Departamentos";
            this.mnuDepartamentos.Click += new System.EventHandler(this.mnuDepartamentos_Click);
            // 
            // mnuPuestos
            // 
            this.mnuPuestos.Name = "mnuPuestos";
            this.mnuPuestos.Size = new System.Drawing.Size(155, 22);
            this.mnuPuestos.Text = "Puestos";
            this.mnuPuestos.Click += new System.EventHandler(this.mnuPuestos_Click);
            // 
            // mnuPeriodos
            // 
            this.mnuPeriodos.Name = "mnuPeriodos";
            this.mnuPeriodos.Size = new System.Drawing.Size(155, 22);
            this.mnuPeriodos.Text = "Periodos";
            this.mnuPeriodos.Click += new System.EventHandler(this.mnuPeriodos_Click);
            // 
            // mnuFactores
            // 
            this.mnuFactores.Name = "mnuFactores";
            this.mnuFactores.Size = new System.Drawing.Size(155, 22);
            this.mnuFactores.Text = "Factores";
            this.mnuFactores.Click += new System.EventHandler(this.mnuFactores_Click);
            // 
            // mnuSalarioMinimo
            // 
            this.mnuSalarioMinimo.Name = "mnuSalarioMinimo";
            this.mnuSalarioMinimo.Size = new System.Drawing.Size(155, 22);
            this.mnuSalarioMinimo.Text = "Salario minimo";
            this.mnuSalarioMinimo.Click += new System.EventHandler(this.mnuSalarioMinimo_Click);
            // 
            // mnuConfiguracion
            // 
            this.mnuConfiguracion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEmpresa,
            this.mnuUsuarios,
            this.mnuPerfiles,
            this.mnuPlazas,
            this.mnuCambiarContrasenia,
            this.toolStripSeparator2,
            this.mnuPreferencias});
            this.mnuConfiguracion.Name = "mnuConfiguracion";
            this.mnuConfiguracion.Size = new System.Drawing.Size(95, 20);
            this.mnuConfiguracion.Text = "Configuración";
            // 
            // mnuEmpresa
            // 
            this.mnuEmpresa.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEmpresas,
            this.mnuLogo});
            this.mnuEmpresa.Name = "mnuEmpresa";
            this.mnuEmpresa.Size = new System.Drawing.Size(180, 22);
            this.mnuEmpresa.Text = "Empresa";
            // 
            // mnuEmpresas
            // 
            this.mnuEmpresas.Name = "mnuEmpresas";
            this.mnuEmpresas.Size = new System.Drawing.Size(124, 22);
            this.mnuEmpresas.Text = "Empresas";
            this.mnuEmpresas.Click += new System.EventHandler(this.mnuEmpresas_Click_1);
            // 
            // mnuLogo
            // 
            this.mnuLogo.Name = "mnuLogo";
            this.mnuLogo.Size = new System.Drawing.Size(124, 22);
            this.mnuLogo.Text = "Logo";
            this.mnuLogo.Click += new System.EventHandler(this.mnuLogo_Click);
            // 
            // mnuUsuarios
            // 
            this.mnuUsuarios.Name = "mnuUsuarios";
            this.mnuUsuarios.Size = new System.Drawing.Size(180, 22);
            this.mnuUsuarios.Text = "Usuarios";
            // 
            // mnuPerfiles
            // 
            this.mnuPerfiles.Name = "mnuPerfiles";
            this.mnuPerfiles.Size = new System.Drawing.Size(180, 22);
            this.mnuPerfiles.Text = "Perfiles";
            this.mnuPerfiles.Click += new System.EventHandler(this.mnuPerfiles_Click);
            // 
            // mnuPlazas
            // 
            this.mnuPlazas.Name = "mnuPlazas";
            this.mnuPlazas.Size = new System.Drawing.Size(180, 22);
            this.mnuPlazas.Text = "Plazas";
            this.mnuPlazas.Click += new System.EventHandler(this.mnuPlazas_Click);
            // 
            // mnuCambiarContrasenia
            // 
            this.mnuCambiarContrasenia.Name = "mnuCambiarContrasenia";
            this.mnuCambiarContrasenia.Size = new System.Drawing.Size(180, 22);
            this.mnuCambiarContrasenia.Text = "Cambiar contraseña";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuPreferencias
            // 
            this.mnuPreferencias.Name = "mnuPreferencias";
            this.mnuPreferencias.Size = new System.Drawing.Size(180, 22);
            this.mnuPreferencias.Text = "Preferencias";
            // 
            // workPerfil
            // 
            this.workPerfil.WorkerReportsProgress = true;
            this.workPerfil.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workPerfil_DoWork);
            this.workPerfil.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workPerfil_ProgressChanged);
            this.workPerfil.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workPerfil_RunWorkerCompleted);
            // 
            // stsPrincipal
            // 
            this.stsPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolEstatusPerfil});
            this.stsPrincipal.Location = new System.Drawing.Point(0, 645);
            this.stsPrincipal.Name = "stsPrincipal";
            this.stsPrincipal.Size = new System.Drawing.Size(795, 22);
            this.stsPrincipal.TabIndex = 3;
            this.stsPrincipal.Text = "statusStrip1";
            // 
            // toolEstatusPerfil
            // 
            this.toolEstatusPerfil.Name = "toolEstatusPerfil";
            this.toolEstatusPerfil.Size = new System.Drawing.Size(98, 17);
            this.toolEstatusPerfil.Text = "Cargando perfil...";
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 667);
            this.Controls.Add(this.stsPrincipal);
            this.Controls.Add(this.mnuPrincipal);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuPrincipal;
            this.Name = "frmPrincipal";
            this.Text = "Sistema de Nomina";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPrincipal_FormClosed);
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.mnuPrincipal.ResumeLayout(false);
            this.mnuPrincipal.PerformLayout();
            this.stsPrincipal.ResumeLayout(false);
            this.stsPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem mnuArchivo;
        private System.Windows.Forms.ToolStripMenuItem mnuAbrirEmpresa;
        private System.Windows.Forms.ToolStripMenuItem mnuCerrarEmpresa;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuSalir;
        private System.Windows.Forms.ToolStripMenuItem mnuConfiguracion;
        private System.Windows.Forms.ToolStripMenuItem mnuUsuarios;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuPreferencias;
        private System.Windows.Forms.ToolStripMenuItem mnuCatalogos;
        private System.Windows.Forms.ToolStripMenuItem mnuClientes;
        private System.Windows.Forms.ToolStripMenuItem mnuDepartamentos;
        private System.Windows.Forms.ToolStripMenuItem mnuPuestos;
        private System.Windows.Forms.ToolStripMenuItem mnuPeriodos;
        private System.Windows.Forms.ToolStripMenuItem mnuFactores;
        private System.Windows.Forms.ToolStripMenuItem mnuSalarioMinimo;
        private System.Windows.Forms.ToolStripMenuItem mnuRecursosHumanos;
        private System.Windows.Forms.ToolStripMenuItem mnuEmpleados;
        private System.Windows.Forms.ToolStripMenuItem mnuExpedientes;
        private System.Windows.Forms.ToolStripMenuItem mnuBajas;
        private System.Windows.Forms.ToolStripMenuItem mnuIniciarSesion;
        private System.Windows.Forms.ToolStripMenuItem mnuCerrarSesion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuCambiarContrasenia;
        private System.Windows.Forms.ToolStripMenuItem mnuPerfiles;
        private System.ComponentModel.BackgroundWorker workPerfil;
        private System.Windows.Forms.StatusStrip stsPrincipal;
        private System.Windows.Forms.ToolStripMenuItem mnuEmpresa;
        private System.Windows.Forms.ToolStripStatusLabel toolEstatusPerfil;
        private System.Windows.Forms.ToolStripMenuItem mnuPlazas;
        private System.Windows.Forms.ToolStripMenuItem mnuSeguroSocial;
        private System.Windows.Forms.ToolStripMenuItem mnuContratos;
        private System.Windows.Forms.ToolStripMenuItem mnuNominas;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuIncapacidades;
        private System.Windows.Forms.ToolStripMenuItem mnuInfonavit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mnuMovimientos;
        private System.Windows.Forms.ToolStripMenuItem mnuAlta;
        private System.Windows.Forms.ToolStripMenuItem mnuBaja;
        private System.Windows.Forms.ToolStripMenuItem mnuModificacion;
        private System.Windows.Forms.ToolStripMenuItem mnuEmpleadoNomina;
        private System.Windows.Forms.ToolStripMenuItem mnuModificarCliente;
        private System.Windows.Forms.ToolStripMenuItem mnuModificarSueldo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mnuEmpleadoSindicato;
        private System.Windows.Forms.ToolStripMenuItem mnuComplementos;
        private System.Windows.Forms.ToolStripMenuItem mnuEmpresas;
        private System.Windows.Forms.ToolStripMenuItem mnuLogo;
    }
}

