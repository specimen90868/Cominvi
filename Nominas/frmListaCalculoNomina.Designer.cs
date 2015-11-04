namespace Nominas
{
    partial class frmListaCalculoNomina
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaCalculoNomina));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolGuardar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolAutorizar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolCaratula = new System.Windows.Forms.ToolStripMenuItem();
            this.empleadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBusqueda = new System.Windows.Forms.ToolStrip();
            this.toolFiltro = new System.Windows.Forms.ToolStripSplitButton();
            this.toolTodos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolDepartamento = new System.Windows.Forms.ToolStripMenuItem();
            this.toolPuesto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolNoEmpleado = new System.Windows.Forms.ToolStripMenuItem();
            this.toolPrenomina = new System.Windows.Forms.ToolStripButton();
            this.toolCalcular = new System.Windows.Forms.ToolStripButton();
            this.toolCargar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolDescalcular = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.lblBuscar = new System.Windows.Forms.ToolStripLabel();
            this.txtBuscar = new System.Windows.Forms.ToolStripTextBox();
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            this.seleccion = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idtrabajador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iddepartamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idpuesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noempleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paterno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.materno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sueldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.despensa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asistencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.puntualidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.horas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpPeriodoInicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpPeriodoFin = new System.Windows.Forms.DateTimePicker();
            this.workerCalculo = new System.ComponentModel.BackgroundWorker();
            this.BarraEstado = new System.Windows.Forms.StatusStrip();
            this.toolEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolPorcentaje = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolEtapa = new System.Windows.Forms.ToolStripStatusLabel();
            this.PanelBarra = new System.Windows.Forms.Panel();
            this.PanelGrid = new System.Windows.Forms.Panel();
            this.workDescalculo = new System.ComponentModel.BackgroundWorker();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workHoras = new System.ComponentModel.BackgroundWorker();
            this.toolReporteDepto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTitulo.SuspendLayout();
            this.toolBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.BarraEstado.SuspendLayout();
            this.PanelBarra.SuspendLayout();
            this.PanelGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTitulo
            // 
            this.toolTitulo.BackColor = System.Drawing.Color.DarkGray;
            this.toolTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1});
            this.toolTitulo.Location = new System.Drawing.Point(0, 0);
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(1303, 32);
            this.toolTitulo.TabIndex = 9;
            this.toolTitulo.Text = "ToolStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAbrir,
            this.toolGuardar,
            this.toolStripSeparator5,
            this.toolAutorizar,
            this.toolStripSeparator2,
            this.toolReportes});
            this.toolStripSplitButton1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(99, 29);
            this.toolStripSplitButton1.Text = "Nómina";
            // 
            // toolAbrir
            // 
            this.toolAbrir.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolAbrir.Name = "toolAbrir";
            this.toolAbrir.Size = new System.Drawing.Size(152, 22);
            this.toolAbrir.Text = "Abrir";
            this.toolAbrir.Click += new System.EventHandler(this.toolAbrir_Click);
            // 
            // toolGuardar
            // 
            this.toolGuardar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolGuardar.Name = "toolGuardar";
            this.toolGuardar.Size = new System.Drawing.Size(152, 22);
            this.toolGuardar.Text = "Guardar";
            this.toolGuardar.Click += new System.EventHandler(this.toolGuardar_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // toolAutorizar
            // 
            this.toolAutorizar.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.toolAutorizar.Name = "toolAutorizar";
            this.toolAutorizar.Size = new System.Drawing.Size(152, 22);
            this.toolAutorizar.Text = "Autorizar";
            this.toolAutorizar.Click += new System.EventHandler(this.toolAutorizar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // toolReportes
            // 
            this.toolReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolCaratula,
            this.empleadosToolStripMenuItem,
            this.toolReporteDepto});
            this.toolReportes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolReportes.Name = "toolReportes";
            this.toolReportes.Size = new System.Drawing.Size(152, 22);
            this.toolReportes.Text = "Reportes";
            // 
            // toolCaratula
            // 
            this.toolCaratula.Name = "toolCaratula";
            this.toolCaratula.Size = new System.Drawing.Size(155, 22);
            this.toolCaratula.Text = "Caratula";
            this.toolCaratula.Click += new System.EventHandler(this.toolCaratula_Click);
            // 
            // empleadosToolStripMenuItem
            // 
            this.empleadosToolStripMenuItem.Name = "empleadosToolStripMenuItem";
            this.empleadosToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.empleadosToolStripMenuItem.Text = "Empleados";
            this.empleadosToolStripMenuItem.Click += new System.EventHandler(this.empleadosToolStripMenuItem_Click);
            // 
            // toolBusqueda
            // 
            this.toolBusqueda.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolFiltro,
            this.toolPrenomina,
            this.toolCalcular,
            this.toolCargar,
            this.toolStripSeparator3,
            this.toolDescalcular,
            this.toolStripSeparator1,
            this.toolCerrar,
            this.toolStripSeparator4,
            this.lblBuscar,
            this.txtBuscar});
            this.toolBusqueda.Location = new System.Drawing.Point(0, 32);
            this.toolBusqueda.Name = "toolBusqueda";
            this.toolBusqueda.Size = new System.Drawing.Size(1303, 25);
            this.toolBusqueda.TabIndex = 10;
            this.toolBusqueda.Text = "ToolStrip1";
            // 
            // toolFiltro
            // 
            this.toolFiltro.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTodos,
            this.toolDepartamento,
            this.toolPuesto,
            this.toolNoEmpleado});
            this.toolFiltro.Image = ((System.Drawing.Image)(resources.GetObject("toolFiltro.Image")));
            this.toolFiltro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFiltro.Name = "toolFiltro";
            this.toolFiltro.Size = new System.Drawing.Size(66, 22);
            this.toolFiltro.Text = "Filtro";
            // 
            // toolTodos
            // 
            this.toolTodos.Name = "toolTodos";
            this.toolTodos.Size = new System.Drawing.Size(150, 22);
            this.toolTodos.Text = "Todos";
            this.toolTodos.Click += new System.EventHandler(this.toolTodos_Click);
            // 
            // toolDepartamento
            // 
            this.toolDepartamento.Name = "toolDepartamento";
            this.toolDepartamento.Size = new System.Drawing.Size(150, 22);
            this.toolDepartamento.Text = "Departamento";
            this.toolDepartamento.Click += new System.EventHandler(this.toolDepartamento_Click);
            // 
            // toolPuesto
            // 
            this.toolPuesto.Name = "toolPuesto";
            this.toolPuesto.Size = new System.Drawing.Size(150, 22);
            this.toolPuesto.Text = "Puesto";
            this.toolPuesto.Click += new System.EventHandler(this.toolPuesto_Click);
            // 
            // toolNoEmpleado
            // 
            this.toolNoEmpleado.Name = "toolNoEmpleado";
            this.toolNoEmpleado.Size = new System.Drawing.Size(150, 22);
            this.toolNoEmpleado.Text = "No. Empleado";
            this.toolNoEmpleado.Click += new System.EventHandler(this.toolNoEmpleado_Click);
            // 
            // toolPrenomina
            // 
            this.toolPrenomina.Image = ((System.Drawing.Image)(resources.GetObject("toolPrenomina.Image")));
            this.toolPrenomina.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPrenomina.Name = "toolPrenomina";
            this.toolPrenomina.Size = new System.Drawing.Size(85, 22);
            this.toolPrenomina.Text = "Prenomina";
            this.toolPrenomina.Click += new System.EventHandler(this.toolPrenomina_Click);
            // 
            // toolCalcular
            // 
            this.toolCalcular.Image = ((System.Drawing.Image)(resources.GetObject("toolCalcular.Image")));
            this.toolCalcular.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCalcular.Name = "toolCalcular";
            this.toolCalcular.Size = new System.Drawing.Size(70, 22);
            this.toolCalcular.Text = "Calcular";
            this.toolCalcular.Click += new System.EventHandler(this.toolCalcular_Click);
            // 
            // toolCargar
            // 
            this.toolCargar.Image = ((System.Drawing.Image)(resources.GetObject("toolCargar.Image")));
            this.toolCargar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCargar.Name = "toolCargar";
            this.toolCargar.Size = new System.Drawing.Size(96, 22);
            this.toolCargar.Text = "Cargar Horas";
            this.toolCargar.Click += new System.EventHandler(this.toolCargar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolDescalcular
            // 
            this.toolDescalcular.Image = ((System.Drawing.Image)(resources.GetObject("toolDescalcular.Image")));
            this.toolDescalcular.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDescalcular.Name = "toolDescalcular";
            this.toolDescalcular.Size = new System.Drawing.Size(87, 22);
            this.toolDescalcular.Text = "Descalcular";
            this.toolDescalcular.Click += new System.EventHandler(this.toolDescalcular_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolCerrar
            // 
            this.toolCerrar.Image = ((System.Drawing.Image)(resources.GetObject("toolCerrar.Image")));
            this.toolCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCerrar.Name = "toolCerrar";
            this.toolCerrar.Size = new System.Drawing.Size(59, 22);
            this.toolCerrar.Text = "Cerrar";
            this.toolCerrar.Click += new System.EventHandler(this.toolCerrar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // lblBuscar
            // 
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(45, 22);
            this.lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.txtBuscar.ForeColor = System.Drawing.Color.Gray;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(300, 25);
            this.txtBuscar.Text = "Buscar no. empleado...";
            this.txtBuscar.Leave += new System.EventHandler(this.txtBuscar_Leave);
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            this.txtBuscar.Click += new System.EventHandler(this.txtBuscar_Click);
            // 
            // dgvEmpleados
            // 
            this.dgvEmpleados.AllowUserToAddRows = false;
            this.dgvEmpleados.AllowUserToDeleteRows = false;
            this.dgvEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmpleados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.seleccion,
            this.idtrabajador,
            this.iddepartamento,
            this.idpuesto,
            this.noempleado,
            this.nombres,
            this.paterno,
            this.materno,
            this.sueldo,
            this.despensa,
            this.asistencia,
            this.puntualidad,
            this.horas});
            this.dgvEmpleados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmpleados.Location = new System.Drawing.Point(0, 0);
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.Size = new System.Drawing.Size(1303, 546);
            this.dgvEmpleados.TabIndex = 11;
            this.dgvEmpleados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmpleados_CellContentClick);
            this.dgvEmpleados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmpleados_CellDoubleClick);
            // 
            // seleccion
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.NullValue = false;
            this.seleccion.DefaultCellStyle = dataGridViewCellStyle1;
            this.seleccion.HeaderText = "Seleccion";
            this.seleccion.Name = "seleccion";
            // 
            // idtrabajador
            // 
            this.idtrabajador.HeaderText = "Id";
            this.idtrabajador.Name = "idtrabajador";
            this.idtrabajador.Visible = false;
            // 
            // iddepartamento
            // 
            this.iddepartamento.HeaderText = "iddepartamento";
            this.iddepartamento.Name = "iddepartamento";
            this.iddepartamento.Visible = false;
            // 
            // idpuesto
            // 
            this.idpuesto.HeaderText = "idpuesto";
            this.idpuesto.Name = "idpuesto";
            this.idpuesto.Visible = false;
            // 
            // noempleado
            // 
            this.noempleado.HeaderText = "No. Empleado";
            this.noempleado.Name = "noempleado";
            this.noempleado.ReadOnly = true;
            this.noempleado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.noempleado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // nombres
            // 
            this.nombres.HeaderText = "Nombre";
            this.nombres.Name = "nombres";
            this.nombres.ReadOnly = true;
            // 
            // paterno
            // 
            this.paterno.HeaderText = "Paterno";
            this.paterno.Name = "paterno";
            // 
            // materno
            // 
            this.materno.HeaderText = "Materno";
            this.materno.Name = "materno";
            // 
            // sueldo
            // 
            dataGridViewCellStyle2.Format = "C6";
            dataGridViewCellStyle2.NullValue = null;
            this.sueldo.DefaultCellStyle = dataGridViewCellStyle2;
            this.sueldo.HeaderText = "Sueldo";
            this.sueldo.Name = "sueldo";
            this.sueldo.ReadOnly = true;
            // 
            // despensa
            // 
            dataGridViewCellStyle3.Format = "C6";
            this.despensa.DefaultCellStyle = dataGridViewCellStyle3;
            this.despensa.HeaderText = "Despensa";
            this.despensa.Name = "despensa";
            this.despensa.ReadOnly = true;
            // 
            // asistencia
            // 
            dataGridViewCellStyle4.Format = "C6";
            this.asistencia.DefaultCellStyle = dataGridViewCellStyle4;
            this.asistencia.HeaderText = "Asistencia";
            this.asistencia.Name = "asistencia";
            this.asistencia.ReadOnly = true;
            // 
            // puntualidad
            // 
            dataGridViewCellStyle5.Format = "C6";
            this.puntualidad.DefaultCellStyle = dataGridViewCellStyle5;
            this.puntualidad.HeaderText = "Puntualidad";
            this.puntualidad.Name = "puntualidad";
            this.puntualidad.ReadOnly = true;
            // 
            // horas
            // 
            dataGridViewCellStyle6.Format = "C6";
            this.horas.DefaultCellStyle = dataGridViewCellStyle6;
            this.horas.HeaderText = "H. Extras Dobles";
            this.horas.Name = "horas";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkGray;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(119, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Periodo:";
            // 
            // dtpPeriodoInicio
            // 
            this.dtpPeriodoInicio.Location = new System.Drawing.Point(171, 9);
            this.dtpPeriodoInicio.Name = "dtpPeriodoInicio";
            this.dtpPeriodoInicio.Size = new System.Drawing.Size(200, 20);
            this.dtpPeriodoInicio.TabIndex = 13;
            this.dtpPeriodoInicio.ValueChanged += new System.EventHandler(this.dtpPeriodoInicio_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DarkGray;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(377, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "al";
            // 
            // dtpPeriodoFin
            // 
            this.dtpPeriodoFin.Enabled = false;
            this.dtpPeriodoFin.Location = new System.Drawing.Point(398, 9);
            this.dtpPeriodoFin.Name = "dtpPeriodoFin";
            this.dtpPeriodoFin.Size = new System.Drawing.Size(200, 20);
            this.dtpPeriodoFin.TabIndex = 15;
            // 
            // workerCalculo
            // 
            this.workerCalculo.WorkerReportsProgress = true;
            this.workerCalculo.WorkerSupportsCancellation = true;
            this.workerCalculo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerCalculo_DoWork);
            this.workerCalculo.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workerCalculo_ProgressChanged);
            this.workerCalculo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerCalculo_RunWorkerCompleted);
            // 
            // BarraEstado
            // 
            this.BarraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolEstado,
            this.toolPorcentaje,
            this.toolEtapa});
            this.BarraEstado.Location = new System.Drawing.Point(0, 0);
            this.BarraEstado.Name = "BarraEstado";
            this.BarraEstado.Size = new System.Drawing.Size(1303, 22);
            this.BarraEstado.TabIndex = 16;
            this.BarraEstado.Text = "statusStrip1";
            // 
            // toolEstado
            // 
            this.toolEstado.Name = "toolEstado";
            this.toolEstado.Size = new System.Drawing.Size(111, 17);
            this.toolEstado.Text = "Procesando:.............";
            // 
            // toolPorcentaje
            // 
            this.toolPorcentaje.Name = "toolPorcentaje";
            this.toolPorcentaje.Size = new System.Drawing.Size(23, 17);
            this.toolPorcentaje.Text = "0%";
            // 
            // toolEtapa
            // 
            this.toolEtapa.Name = "toolEtapa";
            this.toolEtapa.Size = new System.Drawing.Size(36, 17);
            this.toolEtapa.Text = "Etapa";
            // 
            // PanelBarra
            // 
            this.PanelBarra.Controls.Add(this.BarraEstado);
            this.PanelBarra.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelBarra.Location = new System.Drawing.Point(0, 603);
            this.PanelBarra.Name = "PanelBarra";
            this.PanelBarra.Size = new System.Drawing.Size(1303, 22);
            this.PanelBarra.TabIndex = 17;
            // 
            // PanelGrid
            // 
            this.PanelGrid.Controls.Add(this.dgvEmpleados);
            this.PanelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelGrid.Location = new System.Drawing.Point(0, 57);
            this.PanelGrid.Name = "PanelGrid";
            this.PanelGrid.Size = new System.Drawing.Size(1303, 546);
            this.PanelGrid.TabIndex = 18;
            // 
            // workDescalculo
            // 
            this.workDescalculo.WorkerReportsProgress = true;
            this.workDescalculo.WorkerSupportsCancellation = true;
            this.workDescalculo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workDescalculo_DoWork);
            this.workDescalculo.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workDescalculo_ProgressChanged);
            this.workDescalculo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workDescalculo_RunWorkerCompleted);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "No. Empleado";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Nombre";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Paterno";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Materno";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Sueldo";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Despensa";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "P. Asist.";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle7.Format = "C6";
            dataGridViewCellStyle7.NullValue = null;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn8.HeaderText = "P. Puntualidad";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewCellStyle8.Format = "C6";
            this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn9.HeaderText = "H. Extras Dobles";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewCellStyle9.Format = "C6";
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn10.HeaderText = "Inicio";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewCellStyle10.Format = "C6";
            this.dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn11.HeaderText = "Fin";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            dataGridViewCellStyle11.Format = "C6";
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn12.HeaderText = "H. Extras Dobles";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // workHoras
            // 
            this.workHoras.WorkerReportsProgress = true;
            this.workHoras.WorkerSupportsCancellation = true;
            this.workHoras.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workHoras_DoWork);
            this.workHoras.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workHoras_ProgressChanged);
            this.workHoras.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workHoras_RunWorkerCompleted);
            // 
            // toolReporteDepto
            // 
            this.toolReporteDepto.Name = "toolReporteDepto";
            this.toolReporteDepto.Size = new System.Drawing.Size(155, 22);
            this.toolReporteDepto.Text = "Departamentos";
            this.toolReporteDepto.Click += new System.EventHandler(this.toolReporteDepto_Click);
            // 
            // frmListaCalculoNomina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 625);
            this.Controls.Add(this.PanelGrid);
            this.Controls.Add(this.PanelBarra);
            this.Controls.Add(this.dtpPeriodoFin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpPeriodoInicio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolBusqueda);
            this.Controls.Add(this.toolTitulo);
            this.Name = "frmListaCalculoNomina";
            this.Text = "Nomina";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmListaCalculoNomina_FormClosing);
            this.Load += new System.EventHandler(this.frmListaCalculoNomina_Load);
            this.toolTitulo.ResumeLayout(false);
            this.toolTitulo.PerformLayout();
            this.toolBusqueda.ResumeLayout(false);
            this.toolBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.BarraEstado.ResumeLayout(false);
            this.BarraEstado.PerformLayout();
            this.PanelBarra.ResumeLayout(false);
            this.PanelBarra.PerformLayout();
            this.PanelGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolTitulo;
        internal System.Windows.Forms.ToolStrip toolBusqueda;
        private System.Windows.Forms.ToolStripButton toolCargar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.ToolStripLabel lblBuscar;
        internal System.Windows.Forms.ToolStripTextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvEmpleados;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.ToolStripButton toolPrenomina;
        private System.Windows.Forms.ToolStripButton toolCalcular;
        private System.Windows.Forms.ToolStripSplitButton toolFiltro;
        private System.Windows.Forms.ToolStripMenuItem toolDepartamento;
        private System.Windows.Forms.ToolStripMenuItem toolPuesto;
        private System.Windows.Forms.ToolStripMenuItem toolTodos;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpPeriodoInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpPeriodoFin;
        private System.Windows.Forms.ToolStripButton toolDescalcular;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.ComponentModel.BackgroundWorker workerCalculo;
        private System.Windows.Forms.StatusStrip BarraEstado;
        private System.Windows.Forms.Panel PanelBarra;
        private System.Windows.Forms.Panel PanelGrid;
        private System.ComponentModel.BackgroundWorker workDescalculo;
        private System.Windows.Forms.ToolStripStatusLabel toolEstado;
        private System.Windows.Forms.ToolStripStatusLabel toolPorcentaje;
        private System.Windows.Forms.ToolStripStatusLabel toolEtapa;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem toolAbrir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolGuardar;
        private System.Windows.Forms.ToolStripButton toolCerrar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolAutorizar;
        private System.Windows.Forms.ToolStripMenuItem toolNoEmpleado;
        private System.Windows.Forms.DataGridViewCheckBoxColumn seleccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn idtrabajador;
        private System.Windows.Forms.DataGridViewTextBoxColumn iddepartamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn idpuesto;
        private System.Windows.Forms.DataGridViewTextBoxColumn noempleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombres;
        private System.Windows.Forms.DataGridViewTextBoxColumn paterno;
        private System.Windows.Forms.DataGridViewTextBoxColumn materno;
        private System.Windows.Forms.DataGridViewTextBoxColumn sueldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn despensa;
        private System.Windows.Forms.DataGridViewTextBoxColumn asistencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn puntualidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn horas;
        private System.ComponentModel.BackgroundWorker workHoras;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolReportes;
        private System.Windows.Forms.ToolStripMenuItem toolCaratula;
        private System.Windows.Forms.ToolStripMenuItem empleadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolReporteDepto;
    }
}