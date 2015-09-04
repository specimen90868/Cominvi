namespace Nominas
{
    partial class frmIncapacidad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIncapacidad));
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolVentana = new System.Windows.Forms.ToolStripLabel();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFinPeriodo = new System.Windows.Forms.DateTimePicker();
            this.dtpInicioPeriodo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDiasIncapacidad = new System.Windows.Forms.TextBox();
            this.chkConsecutiva = new System.Windows.Forms.CheckBox();
            this.toolTitulo.SuspendLayout();
            this.toolAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTitulo
            // 
            this.toolTitulo.BackColor = System.Drawing.Color.DarkGray;
            this.toolTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolVentana});
            this.toolTitulo.Location = new System.Drawing.Point(0, 0);
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(512, 27);
            this.toolTitulo.TabIndex = 6;
            this.toolTitulo.Text = "toolAcciones";
            // 
            // toolVentana
            // 
            this.toolVentana.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolVentana.Name = "toolVentana";
            this.toolVentana.Size = new System.Drawing.Size(189, 24);
            this.toolVentana.Text = "Nueva incapacidad";
            // 
            // toolAcciones
            // 
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGuardar,
            this.toolBuscar,
            this.toolCerrar});
            this.toolAcciones.Location = new System.Drawing.Point(0, 27);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(512, 25);
            this.toolAcciones.TabIndex = 7;
            this.toolAcciones.Text = "toolEmpresa";
            // 
            // toolGuardar
            // 
            this.toolGuardar.Image = ((System.Drawing.Image)(resources.GetObject("toolGuardar.Image")));
            this.toolGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGuardar.Name = "toolGuardar";
            this.toolGuardar.Size = new System.Drawing.Size(69, 22);
            this.toolGuardar.Text = "Guardar";
            this.toolGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolGuardar.Click += new System.EventHandler(this.toolGuardar_Click);
            // 
            // toolBuscar
            // 
            this.toolBuscar.Image = ((System.Drawing.Image)(resources.GetObject("toolBuscar.Image")));
            this.toolBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBuscar.Name = "toolBuscar";
            this.toolBuscar.Size = new System.Drawing.Size(62, 22);
            this.toolBuscar.Text = "Buscar";
            this.toolBuscar.Click += new System.EventHandler(this.toolBuscar_Click);
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
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.Location = new System.Drawing.Point(112, 75);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(183, 20);
            this.lblEmpleado.TabIndex = 257;
            this.lblEmpleado.Text = "Nombre del empleado";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(12, 75);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(94, 20);
            this.label23.TabIndex = 256;
            this.label23.Text = "Empleado:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(129, 120);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaInicio.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 259;
            this.label1.Text = "Fecha de inicio:";
            // 
            // dtpFinPeriodo
            // 
            this.dtpFinPeriodo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFinPeriodo.Location = new System.Drawing.Point(129, 198);
            this.dtpFinPeriodo.Name = "dtpFinPeriodo";
            this.dtpFinPeriodo.Size = new System.Drawing.Size(100, 20);
            this.dtpFinPeriodo.TabIndex = 4;
            // 
            // dtpInicioPeriodo
            // 
            this.dtpInicioPeriodo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInicioPeriodo.Location = new System.Drawing.Point(129, 172);
            this.dtpInicioPeriodo.Name = "dtpInicioPeriodo";
            this.dtpInicioPeriodo.Size = new System.Drawing.Size(100, 20);
            this.dtpInicioPeriodo.TabIndex = 3;
            this.dtpInicioPeriodo.ValueChanged += new System.EventHandler(this.dtpInicio_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 260;
            this.label3.Text = "Periodo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 263;
            this.label2.Text = "Dias de incapacidad:";
            // 
            // txtDiasIncapacidad
            // 
            this.txtDiasIncapacidad.Location = new System.Drawing.Point(129, 146);
            this.txtDiasIncapacidad.Name = "txtDiasIncapacidad";
            this.txtDiasIncapacidad.Size = new System.Drawing.Size(100, 20);
            this.txtDiasIncapacidad.TabIndex = 2;
            this.txtDiasIncapacidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkConsecutiva
            // 
            this.chkConsecutiva.AutoSize = true;
            this.chkConsecutiva.Location = new System.Drawing.Point(235, 122);
            this.chkConsecutiva.Name = "chkConsecutiva";
            this.chkConsecutiva.Size = new System.Drawing.Size(85, 17);
            this.chkConsecutiva.TabIndex = 6;
            this.chkConsecutiva.Text = "Consecutiva";
            this.chkConsecutiva.UseVisualStyleBackColor = true;
            // 
            // frmIncapacidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 461);
            this.Controls.Add(this.chkConsecutiva);
            this.Controls.Add(this.txtDiasIncapacidad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFinPeriodo);
            this.Controls.Add(this.dtpInicioPeriodo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.lblEmpleado);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.toolAcciones);
            this.Controls.Add(this.toolTitulo);
            this.Name = "frmIncapacidad";
            this.Text = "Incapacidad";
            this.Load += new System.EventHandler(this.frmIncapacidad_Load);
            this.toolTitulo.ResumeLayout(false);
            this.toolTitulo.PerformLayout();
            this.toolAcciones.ResumeLayout(false);
            this.toolAcciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolTitulo;
        internal System.Windows.Forms.ToolStripLabel toolVentana;
        internal System.Windows.Forms.ToolStrip toolAcciones;
        internal System.Windows.Forms.ToolStripButton toolGuardar;
        private System.Windows.Forms.ToolStripButton toolBuscar;
        private System.Windows.Forms.ToolStripButton toolCerrar;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFinPeriodo;
        private System.Windows.Forms.DateTimePicker dtpInicioPeriodo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDiasIncapacidad;
        private System.Windows.Forms.CheckBox chkConsecutiva;
    }
}