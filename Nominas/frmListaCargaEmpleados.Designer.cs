namespace Nominas
{
    partial class frmListaCargaEmpleados
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaCargaEmpleados));
            this.dgvCargaEmpleados = new System.Windows.Forms.DataGridView();
            this.noempleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paterno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.materno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.periodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.departamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.puesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.curp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sdi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolBusqueda = new System.Windows.Forms.ToolStrip();
            this.toolCargar = new System.Windows.Forms.ToolStripButton();
            this.toolLimpiar = new System.Windows.Forms.ToolStripButton();
            this.toolAplicar = new System.Windows.Forms.ToolStripButton();
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolEmpleados = new System.Windows.Forms.ToolStripLabel();
            this.workerImporta = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPorcentaje = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCargaEmpleados)).BeginInit();
            this.toolBusqueda.SuspendLayout();
            this.toolTitulo.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvCargaEmpleados
            // 
            this.dgvCargaEmpleados.AllowUserToAddRows = false;
            this.dgvCargaEmpleados.AllowUserToDeleteRows = false;
            this.dgvCargaEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCargaEmpleados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.noempleado,
            this.paterno,
            this.materno,
            this.nombre,
            this.periodo,
            this.departamento,
            this.puesto,
            this.fechaingreso,
            this.curp,
            this.nss,
            this.dv,
            this.sdi});
            this.dgvCargaEmpleados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCargaEmpleados.Location = new System.Drawing.Point(0, 52);
            this.dgvCargaEmpleados.MultiSelect = false;
            this.dgvCargaEmpleados.Name = "dgvCargaEmpleados";
            this.dgvCargaEmpleados.ReadOnly = true;
            this.dgvCargaEmpleados.Size = new System.Drawing.Size(709, 438);
            this.dgvCargaEmpleados.TabIndex = 13;
            // 
            // noempleado
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.noempleado.DefaultCellStyle = dataGridViewCellStyle1;
            this.noempleado.HeaderText = "No. Empleado";
            this.noempleado.Name = "noempleado";
            this.noempleado.ReadOnly = true;
            // 
            // paterno
            // 
            this.paterno.HeaderText = "Paterno";
            this.paterno.Name = "paterno";
            this.paterno.ReadOnly = true;
            // 
            // materno
            // 
            this.materno.HeaderText = "Materno";
            this.materno.Name = "materno";
            this.materno.ReadOnly = true;
            // 
            // nombre
            // 
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // periodo
            // 
            this.periodo.HeaderText = "Periodo";
            this.periodo.Name = "periodo";
            this.periodo.ReadOnly = true;
            // 
            // departamento
            // 
            this.departamento.HeaderText = "Departamento";
            this.departamento.Name = "departamento";
            this.departamento.ReadOnly = true;
            // 
            // puesto
            // 
            this.puesto.HeaderText = "Puesto";
            this.puesto.Name = "puesto";
            this.puesto.ReadOnly = true;
            // 
            // fechaingreso
            // 
            this.fechaingreso.HeaderText = "Fecha Ingreso";
            this.fechaingreso.Name = "fechaingreso";
            this.fechaingreso.ReadOnly = true;
            // 
            // curp
            // 
            this.curp.HeaderText = "Curp";
            this.curp.Name = "curp";
            this.curp.ReadOnly = true;
            // 
            // nss
            // 
            this.nss.HeaderText = "Nss";
            this.nss.Name = "nss";
            this.nss.ReadOnly = true;
            // 
            // dv
            // 
            this.dv.HeaderText = "Dig. V.";
            this.dv.Name = "dv";
            this.dv.ReadOnly = true;
            // 
            // sdi
            // 
            this.sdi.HeaderText = "Sueldo Integrado";
            this.sdi.Name = "sdi";
            this.sdi.ReadOnly = true;
            // 
            // toolBusqueda
            // 
            this.toolBusqueda.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolCargar,
            this.toolLimpiar,
            this.toolAplicar});
            this.toolBusqueda.Location = new System.Drawing.Point(0, 27);
            this.toolBusqueda.Name = "toolBusqueda";
            this.toolBusqueda.Size = new System.Drawing.Size(709, 25);
            this.toolBusqueda.TabIndex = 12;
            this.toolBusqueda.Text = "ToolStrip1";
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
            // toolTitulo
            // 
            this.toolTitulo.BackColor = System.Drawing.Color.DarkGray;
            this.toolTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolEmpleados});
            this.toolTitulo.Location = new System.Drawing.Point(0, 0);
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(709, 27);
            this.toolTitulo.TabIndex = 11;
            this.toolTitulo.Text = "ToolStrip1";
            // 
            // toolEmpleados
            // 
            this.toolEmpleados.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolEmpleados.Name = "toolEmpleados";
            this.toolEmpleados.Size = new System.Drawing.Size(206, 24);
            this.toolEmpleados.Text = "Carga de Empleados";
            // 
            // workerImporta
            // 
            this.workerImporta.WorkerReportsProgress = true;
            this.workerImporta.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerImporta_DoWork);
            this.workerImporta.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workerImporta_ProgressChanged);
            this.workerImporta.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerImporta_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblPorcentaje});
            this.statusStrip1.Location = new System.Drawing.Point(0, 468);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(709, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(84, 17);
            this.toolStripStatusLabel1.Text = "Importación:...";
            // 
            // lblPorcentaje
            // 
            this.lblPorcentaje.Name = "lblPorcentaje";
            this.lblPorcentaje.Size = new System.Drawing.Size(23, 17);
            this.lblPorcentaje.Text = "0%";
            // 
            // frmListaCargaEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 490);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgvCargaEmpleados);
            this.Controls.Add(this.toolBusqueda);
            this.Controls.Add(this.toolTitulo);
            this.Name = "frmListaCargaEmpleados";
            this.Text = "Carga de empleados";
            this.Load += new System.EventHandler(this.frmListaCargaEmpleados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCargaEmpleados)).EndInit();
            this.toolBusqueda.ResumeLayout(false);
            this.toolBusqueda.PerformLayout();
            this.toolTitulo.ResumeLayout(false);
            this.toolTitulo.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCargaEmpleados;
        internal System.Windows.Forms.ToolStrip toolBusqueda;
        private System.Windows.Forms.ToolStripButton toolCargar;
        private System.Windows.Forms.ToolStripButton toolLimpiar;
        private System.Windows.Forms.ToolStripButton toolAplicar;
        internal System.Windows.Forms.ToolStrip toolTitulo;
        internal System.Windows.Forms.ToolStripLabel toolEmpleados;
        private System.ComponentModel.BackgroundWorker workerImporta;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblPorcentaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn noempleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn paterno;
        private System.Windows.Forms.DataGridViewTextBoxColumn materno;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn periodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn departamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn puesto;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaingreso;
        private System.Windows.Forms.DataGridViewTextBoxColumn curp;
        private System.Windows.Forms.DataGridViewTextBoxColumn nss;
        private System.Windows.Forms.DataGridViewTextBoxColumn dv;
        private System.Windows.Forms.DataGridViewTextBoxColumn sdi;
    }
}