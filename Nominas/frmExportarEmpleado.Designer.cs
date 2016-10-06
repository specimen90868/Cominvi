namespace Nominas
{
    partial class frmExportarEmpleado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportarEmpleado));
            this.toolEmpleado = new System.Windows.Forms.ToolStrip();
            this.toolExportar = new System.Windows.Forms.ToolStripButton();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolTitulo = new System.Windows.Forms.ToolStripLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolPorcentaje = new System.Windows.Forms.ToolStripStatusLabel();
            this.workerExportar = new System.ComponentModel.BackgroundWorker();
            this.lstvOrdenCampos = new System.Windows.Forms.ListView();
            this.lstvCampos = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnArriba = new System.Windows.Forms.Button();
            this.btnAbajo = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbPeriodo = new System.Windows.Forms.ComboBox();
            this.cmbTipoNomina = new System.Windows.Forms.ComboBox();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.toolEmpleado.SuspendLayout();
            this.toolAcciones.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolEmpleado
            // 
            this.toolEmpleado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolExportar});
            this.toolEmpleado.Location = new System.Drawing.Point(0, 27);
            this.toolEmpleado.Name = "toolEmpleado";
            this.toolEmpleado.Size = new System.Drawing.Size(384, 25);
            this.toolEmpleado.TabIndex = 211;
            this.toolEmpleado.Text = "toolEmpresa";
            // 
            // toolExportar
            // 
            this.toolExportar.Image = ((System.Drawing.Image)(resources.GetObject("toolExportar.Image")));
            this.toolExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExportar.Name = "toolExportar";
            this.toolExportar.Size = new System.Drawing.Size(70, 22);
            this.toolExportar.Text = "Exportar";
            this.toolExportar.Click += new System.EventHandler(this.toolExportar_Click);
            // 
            // toolAcciones
            // 
            this.toolAcciones.BackColor = System.Drawing.Color.DarkGray;
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTitulo});
            this.toolAcciones.Location = new System.Drawing.Point(0, 0);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(384, 27);
            this.toolAcciones.TabIndex = 210;
            this.toolAcciones.Text = "toolAcciones";
            // 
            // toolTitulo
            // 
            this.toolTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(192, 24);
            this.toolTitulo.Text = "Datos para exportar";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolEstado,
            this.toolPorcentaje});
            this.statusStrip1.Location = new System.Drawing.Point(0, 511);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(384, 22);
            this.statusStrip1.TabIndex = 247;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolEstado
            // 
            this.toolEstado.Name = "toolEstado";
            this.toolEstado.Size = new System.Drawing.Size(76, 17);
            this.toolEstado.Text = "Exportando...";
            // 
            // toolPorcentaje
            // 
            this.toolPorcentaje.Name = "toolPorcentaje";
            this.toolPorcentaje.Size = new System.Drawing.Size(23, 17);
            this.toolPorcentaje.Text = "0%";
            // 
            // workerExportar
            // 
            this.workerExportar.WorkerReportsProgress = true;
            this.workerExportar.WorkerSupportsCancellation = true;
            this.workerExportar.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerExportar_DoWork);
            this.workerExportar.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workerExportar_ProgressChanged);
            this.workerExportar.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerExportar_RunWorkerCompleted);
            // 
            // lstvOrdenCampos
            // 
            this.lstvOrdenCampos.AllowDrop = true;
            this.lstvOrdenCampos.Location = new System.Drawing.Point(181, 166);
            this.lstvOrdenCampos.Name = "lstvOrdenCampos";
            this.lstvOrdenCampos.Size = new System.Drawing.Size(161, 338);
            this.lstvOrdenCampos.TabIndex = 249;
            this.lstvOrdenCampos.UseCompatibleStateImageBehavior = false;
            this.lstvOrdenCampos.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lstvOrdenCampos_ItemDrag);
            this.lstvOrdenCampos.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstvOrdenCampos_DragDrop);
            this.lstvOrdenCampos.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstvOrdenCampos_DragEnter);
            // 
            // lstvCampos
            // 
            this.lstvCampos.AllowDrop = true;
            this.lstvCampos.Location = new System.Drawing.Point(12, 166);
            this.lstvCampos.MultiSelect = false;
            this.lstvCampos.Name = "lstvCampos";
            this.lstvCampos.Size = new System.Drawing.Size(162, 338);
            this.lstvCampos.TabIndex = 250;
            this.lstvCampos.UseCompatibleStateImageBehavior = false;
            this.lstvCampos.View = System.Windows.Forms.View.Details;
            this.lstvCampos.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lstvCampos_ItemDrag);
            this.lstvCampos.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstvCampos_DragDrop);
            this.lstvCampos.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstvCampos_DragEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 251;
            this.label1.Text = "Campos:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 252;
            this.label2.Text = "Campos a exportar:";
            // 
            // btnArriba
            // 
            this.btnArriba.Image = ((System.Drawing.Image)(resources.GetObject("btnArriba.Image")));
            this.btnArriba.Location = new System.Drawing.Point(344, 166);
            this.btnArriba.Name = "btnArriba";
            this.btnArriba.Size = new System.Drawing.Size(31, 29);
            this.btnArriba.TabIndex = 253;
            this.btnArriba.UseVisualStyleBackColor = true;
            this.btnArriba.Click += new System.EventHandler(this.btnArriba_Click);
            // 
            // btnAbajo
            // 
            this.btnAbajo.Image = ((System.Drawing.Image)(resources.GetObject("btnAbajo.Image")));
            this.btnAbajo.Location = new System.Drawing.Point(344, 201);
            this.btnAbajo.Name = "btnAbajo";
            this.btnAbajo.Size = new System.Drawing.Size(31, 29);
            this.btnAbajo.TabIndex = 254;
            this.btnAbajo.UseVisualStyleBackColor = true;
            this.btnAbajo.Click += new System.EventHandler(this.btnAbajo_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 255;
            this.label3.Text = "Periodo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 256;
            this.label4.Text = "Tipo de nómina:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 257;
            this.label5.Text = "Inicio:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 258;
            this.label6.Text = "Fin:";
            // 
            // cmbPeriodo
            // 
            this.cmbPeriodo.FormattingEnabled = true;
            this.cmbPeriodo.Location = new System.Drawing.Point(99, 53);
            this.cmbPeriodo.Name = "cmbPeriodo";
            this.cmbPeriodo.Size = new System.Drawing.Size(121, 21);
            this.cmbPeriodo.TabIndex = 259;
            // 
            // cmbTipoNomina
            // 
            this.cmbTipoNomina.FormattingEnabled = true;
            this.cmbTipoNomina.Items.AddRange(new object[] {
            "NORMAL",
            "EXTRAORDINARIO"});
            this.cmbTipoNomina.Location = new System.Drawing.Point(99, 75);
            this.cmbTipoNomina.Name = "cmbTipoNomina";
            this.cmbTipoNomina.Size = new System.Drawing.Size(121, 21);
            this.cmbTipoNomina.TabIndex = 260;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Location = new System.Drawing.Point(99, 97);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaInicio.TabIndex = 261;
            this.dtpFechaInicio.ValueChanged += new System.EventHandler(this.dtpFechaInicio_ValueChanged);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Enabled = false;
            this.dtpFechaFin.Location = new System.Drawing.Point(99, 119);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaFin.TabIndex = 262;
            // 
            // frmExportarEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 533);
            this.Controls.Add(this.dtpFechaFin);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.cmbTipoNomina);
            this.Controls.Add(this.cmbPeriodo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAbajo);
            this.Controls.Add(this.btnArriba);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstvCampos);
            this.Controls.Add(this.lstvOrdenCampos);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolEmpleado);
            this.Controls.Add(this.toolAcciones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExportarEmpleado";
            this.Text = "Datos para exportar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmExportarEmpleado_FormClosing);
            this.Load += new System.EventHandler(this.frmExportarEmpleado_Load);
            this.toolEmpleado.ResumeLayout(false);
            this.toolEmpleado.PerformLayout();
            this.toolAcciones.ResumeLayout(false);
            this.toolAcciones.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolEmpleado;
        private System.Windows.Forms.ToolStripButton toolExportar;
        internal System.Windows.Forms.ToolStrip toolAcciones;
        internal System.Windows.Forms.ToolStripLabel toolTitulo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolEstado;
        private System.Windows.Forms.ToolStripStatusLabel toolPorcentaje;
        private System.ComponentModel.BackgroundWorker workerExportar;
        private System.Windows.Forms.ListView lstvOrdenCampos;
        private System.Windows.Forms.ListView lstvCampos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnArriba;
        private System.Windows.Forms.Button btnAbajo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbPeriodo;
        private System.Windows.Forms.ComboBox cmbTipoNomina;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
    }
}