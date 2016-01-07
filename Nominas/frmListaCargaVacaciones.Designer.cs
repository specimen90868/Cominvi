namespace Nominas
{
    partial class frmListaCargaVacaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaCargaVacaciones));
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolEmpleados = new System.Windows.Forms.ToolStripLabel();
            this.toolBusqueda = new System.Windows.Forms.ToolStrip();
            this.toolNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolCargar = new System.Windows.Forms.ToolStripButton();
            this.toolLimpiar = new System.Windows.Forms.ToolStripButton();
            this.toolAplicar = new System.Windows.Forms.ToolStripButton();
            this.dgvCargaVacaciones = new System.Windows.Forms.DataGridView();
            this.workVacaciones = new System.ComponentModel.BackgroundWorker();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noempleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paterno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.materno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.concepto = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.diaspago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaaplicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTitulo.SuspendLayout();
            this.toolBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCargaVacaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTitulo
            // 
            this.toolTitulo.BackColor = System.Drawing.Color.DarkGray;
            this.toolTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolEmpleados});
            this.toolTitulo.Location = new System.Drawing.Point(0, 0);
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(951, 27);
            this.toolTitulo.TabIndex = 9;
            this.toolTitulo.Text = "ToolStrip1";
            // 
            // toolEmpleados
            // 
            this.toolEmpleados.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolEmpleados.Name = "toolEmpleados";
            this.toolEmpleados.Size = new System.Drawing.Size(206, 24);
            this.toolEmpleados.Text = "Carga de vacaciones";
            // 
            // toolBusqueda
            // 
            this.toolBusqueda.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNuevo,
            this.toolCargar,
            this.toolLimpiar,
            this.toolAplicar});
            this.toolBusqueda.Location = new System.Drawing.Point(0, 27);
            this.toolBusqueda.Name = "toolBusqueda";
            this.toolBusqueda.Size = new System.Drawing.Size(951, 25);
            this.toolBusqueda.TabIndex = 10;
            this.toolBusqueda.Text = "ToolStrip1";
            // 
            // toolNuevo
            // 
            this.toolNuevo.Image = ((System.Drawing.Image)(resources.GetObject("toolNuevo.Image")));
            this.toolNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNuevo.Name = "toolNuevo";
            this.toolNuevo.Size = new System.Drawing.Size(62, 22);
            this.toolNuevo.Text = "Nuevo";
            this.toolNuevo.Visible = false;
            this.toolNuevo.Click += new System.EventHandler(this.toolNuevo_Click);
            // 
            // toolCargar
            // 
            this.toolCargar.Image = ((System.Drawing.Image)(resources.GetObject("toolCargar.Image")));
            this.toolCargar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCargar.Name = "toolCargar";
            this.toolCargar.Size = new System.Drawing.Size(62, 22);
            this.toolCargar.Text = "Cargar";
            this.toolCargar.Click += new System.EventHandler(this.toolCargar_Click);
            // 
            // toolLimpiar
            // 
            this.toolLimpiar.Image = ((System.Drawing.Image)(resources.GetObject("toolLimpiar.Image")));
            this.toolLimpiar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolLimpiar.Name = "toolLimpiar";
            this.toolLimpiar.Size = new System.Drawing.Size(67, 22);
            this.toolLimpiar.Text = "Limpiar";
            this.toolLimpiar.Click += new System.EventHandler(this.toolLimpiar_Click);
            // 
            // toolAplicar
            // 
            this.toolAplicar.Image = ((System.Drawing.Image)(resources.GetObject("toolAplicar.Image")));
            this.toolAplicar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAplicar.Name = "toolAplicar";
            this.toolAplicar.Size = new System.Drawing.Size(64, 22);
            this.toolAplicar.Text = "Aplicar";
            this.toolAplicar.Click += new System.EventHandler(this.toolAplicar_Click);
            // 
            // dgvCargaVacaciones
            // 
            this.dgvCargaVacaciones.AllowUserToAddRows = false;
            this.dgvCargaVacaciones.AllowUserToDeleteRows = false;
            this.dgvCargaVacaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCargaVacaciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.noempleado,
            this.nombre,
            this.paterno,
            this.materno,
            this.concepto,
            this.diaspago,
            this.inicio,
            this.fin,
            this.fechaaplicacion});
            this.dgvCargaVacaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCargaVacaciones.Location = new System.Drawing.Point(0, 52);
            this.dgvCargaVacaciones.Name = "dgvCargaVacaciones";
            this.dgvCargaVacaciones.Size = new System.Drawing.Size(951, 356);
            this.dgvCargaVacaciones.TabIndex = 11;
            // 
            // workVacaciones
            // 
            this.workVacaciones.WorkerReportsProgress = true;
            this.workVacaciones.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workVacaciones_DoWork);
            this.workVacaciones.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workVacaciones_RunWorkerCompleted);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Id Trabajador";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "No. Empleado";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Nombre";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Ap. Paterno";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Ap. Materno";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Dias a pagar";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Fecha inicio";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Fecha fin";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // noempleado
            // 
            this.noempleado.HeaderText = "No. Empleado";
            this.noempleado.Name = "noempleado";
            this.noempleado.ReadOnly = true;
            // 
            // nombre
            // 
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // paterno
            // 
            this.paterno.HeaderText = "Ap. Paterno";
            this.paterno.Name = "paterno";
            this.paterno.ReadOnly = true;
            // 
            // materno
            // 
            this.materno.HeaderText = "Ap. Materno";
            this.materno.Name = "materno";
            this.materno.ReadOnly = true;
            // 
            // concepto
            // 
            this.concepto.HeaderText = "Concepto";
            this.concepto.Items.AddRange(new object[] {
            "Prima Vacacional",
            "Vacaciones"});
            this.concepto.Name = "concepto";
            // 
            // diaspago
            // 
            this.diaspago.HeaderText = "Dias";
            this.diaspago.Name = "diaspago";
            this.diaspago.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.diaspago.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // inicio
            // 
            this.inicio.HeaderText = "Fecha inicio";
            this.inicio.Name = "inicio";
            this.inicio.ReadOnly = true;
            // 
            // fin
            // 
            this.fin.HeaderText = "Fecha fin";
            this.fin.Name = "fin";
            this.fin.ReadOnly = true;
            // 
            // fechaaplicacion
            // 
            this.fechaaplicacion.HeaderText = "Fecha de Inicio";
            this.fechaaplicacion.Name = "fechaaplicacion";
            // 
            // frmListaCargaVacaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 408);
            this.Controls.Add(this.dgvCargaVacaciones);
            this.Controls.Add(this.toolBusqueda);
            this.Controls.Add(this.toolTitulo);
            this.Name = "frmListaCargaVacaciones";
            this.Text = "Carga de vacaciones";
            this.Load += new System.EventHandler(this.frmListaCargaVacaciones_Load);
            this.toolTitulo.ResumeLayout(false);
            this.toolTitulo.PerformLayout();
            this.toolBusqueda.ResumeLayout(false);
            this.toolBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCargaVacaciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolTitulo;
        internal System.Windows.Forms.ToolStripLabel toolEmpleados;
        internal System.Windows.Forms.ToolStrip toolBusqueda;
        private System.Windows.Forms.ToolStripButton toolNuevo;
        private System.Windows.Forms.ToolStripButton toolCargar;
        private System.Windows.Forms.ToolStripButton toolLimpiar;
        private System.Windows.Forms.ToolStripButton toolAplicar;
        private System.Windows.Forms.DataGridView dgvCargaVacaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.ComponentModel.BackgroundWorker workVacaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn noempleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn paterno;
        private System.Windows.Forms.DataGridViewTextBoxColumn materno;
        private System.Windows.Forms.DataGridViewComboBoxColumn concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn diaspago;
        private System.Windows.Forms.DataGridViewTextBoxColumn inicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn fin;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaaplicacion;
    }
}