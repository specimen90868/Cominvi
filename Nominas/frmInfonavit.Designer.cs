namespace Nominas
{
    partial class frmInfonavit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInfonavit));
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolVentana = new System.Windows.Forms.ToolStripLabel();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.Label16 = new System.Windows.Forms.Label();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.rbtnPesos = new System.Windows.Forms.RadioButton();
            this.rbtnVsmdf = new System.Windows.Forms.RadioButton();
            this.Label52 = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.rbtnPorcentaje = new System.Windows.Forms.RadioButton();
            this.txtNumeroCredito = new System.Windows.Forms.TextBox();
            this.Label50 = new System.Windows.Forms.Label();
            this.Label51 = new System.Windows.Forms.Label();
            this.chkActivo = new System.Windows.Forms.CheckBox();
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
            this.toolTitulo.Size = new System.Drawing.Size(500, 27);
            this.toolTitulo.TabIndex = 4;
            this.toolTitulo.Text = "toolAcciones";
            // 
            // toolVentana
            // 
            this.toolVentana.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolVentana.Name = "toolVentana";
            this.toolVentana.Size = new System.Drawing.Size(224, 24);
            this.toolVentana.Text = "Nuevo crédito infonavit";
            // 
            // toolAcciones
            // 
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGuardar,
            this.toolBuscar,
            this.toolCerrar});
            this.toolAcciones.Location = new System.Drawing.Point(0, 27);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(500, 25);
            this.toolAcciones.TabIndex = 5;
            this.toolAcciones.Text = "toolEmpresa";
            // 
            // toolGuardar
            // 
            this.toolGuardar.Image = ((System.Drawing.Image)(resources.GetObject("toolGuardar.Image")));
            this.toolGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGuardar.Name = "toolGuardar";
            this.toolGuardar.Size = new System.Drawing.Size(69, 22);
            this.toolGuardar.Text = "Guardar";
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
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.Color.White;
            this.Label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.Label16.Location = new System.Drawing.Point(13, 126);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(137, 18);
            this.Label16.TabIndex = 257;
            this.Label16.Text = "Datos del crédito";
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.Location = new System.Drawing.Point(112, 75);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(183, 20);
            this.lblEmpleado.TabIndex = 256;
            this.lblEmpleado.Text = "Nombre del empleado";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(12, 75);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(94, 20);
            this.label23.TabIndex = 255;
            this.label23.Text = "Empleado:";
            // 
            // rbtnPesos
            // 
            this.rbtnPesos.AutoSize = true;
            this.rbtnPesos.Location = new System.Drawing.Point(144, 306);
            this.rbtnPesos.Name = "rbtnPesos";
            this.rbtnPesos.Size = new System.Drawing.Size(54, 17);
            this.rbtnPesos.TabIndex = 265;
            this.rbtnPesos.TabStop = true;
            this.rbtnPesos.Text = "Pesos";
            this.rbtnPesos.UseVisualStyleBackColor = true;
            this.rbtnPesos.CheckedChanged += new System.EventHandler(this.rbtnPesos_CheckedChanged);
            // 
            // rbtnVsmdf
            // 
            this.rbtnVsmdf.AutoSize = true;
            this.rbtnVsmdf.Location = new System.Drawing.Point(144, 272);
            this.rbtnVsmdf.Name = "rbtnVsmdf";
            this.rbtnVsmdf.Size = new System.Drawing.Size(62, 17);
            this.rbtnVsmdf.TabIndex = 260;
            this.rbtnVsmdf.TabStop = true;
            this.rbtnVsmdf.Text = "VSMDF";
            this.rbtnVsmdf.UseVisualStyleBackColor = true;
            this.rbtnVsmdf.CheckedChanged += new System.EventHandler(this.rbtnVsmdf_CheckedChanged);
            // 
            // Label52
            // 
            this.Label52.AutoSize = true;
            this.Label52.Location = new System.Drawing.Point(25, 167);
            this.Label52.Name = "Label52";
            this.Label52.Size = new System.Drawing.Size(97, 13);
            this.Label52.TabIndex = 258;
            this.Label52.Text = "Número de crédito:";
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(144, 200);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(76, 20);
            this.txtValor.TabIndex = 264;
            // 
            // rbtnPorcentaje
            // 
            this.rbtnPorcentaje.AutoSize = true;
            this.rbtnPorcentaje.Location = new System.Drawing.Point(144, 238);
            this.rbtnPorcentaje.Name = "rbtnPorcentaje";
            this.rbtnPorcentaje.Size = new System.Drawing.Size(76, 17);
            this.rbtnPorcentaje.TabIndex = 259;
            this.rbtnPorcentaje.TabStop = true;
            this.rbtnPorcentaje.Text = "Porcentaje";
            this.rbtnPorcentaje.UseVisualStyleBackColor = true;
            this.rbtnPorcentaje.CheckedChanged += new System.EventHandler(this.rbtnPorcentaje_CheckedChanged);
            // 
            // txtNumeroCredito
            // 
            this.txtNumeroCredito.Location = new System.Drawing.Point(144, 164);
            this.txtNumeroCredito.Name = "txtNumeroCredito";
            this.txtNumeroCredito.Size = new System.Drawing.Size(76, 20);
            this.txtNumeroCredito.TabIndex = 261;
            // 
            // Label50
            // 
            this.Label50.AutoSize = true;
            this.Label50.Location = new System.Drawing.Point(25, 203);
            this.Label50.Name = "Label50";
            this.Label50.Size = new System.Drawing.Size(104, 13);
            this.Label50.TabIndex = 263;
            this.Label50.Text = "Valor del descuento:";
            // 
            // Label51
            // 
            this.Label51.AutoSize = true;
            this.Label51.Location = new System.Drawing.Point(25, 237);
            this.Label51.Name = "Label51";
            this.Label51.Size = new System.Drawing.Size(80, 13);
            this.Label51.TabIndex = 262;
            this.Label51.Text = "Descuento por:";
            // 
            // chkActivo
            // 
            this.chkActivo.AutoSize = true;
            this.chkActivo.Location = new System.Drawing.Point(156, 129);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(56, 17);
            this.chkActivo.TabIndex = 266;
            this.chkActivo.Text = "Activo";
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // frmInfonavit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 630);
            this.Controls.Add(this.chkActivo);
            this.Controls.Add(this.rbtnPesos);
            this.Controls.Add(this.rbtnVsmdf);
            this.Controls.Add(this.Label52);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.rbtnPorcentaje);
            this.Controls.Add(this.txtNumeroCredito);
            this.Controls.Add(this.Label50);
            this.Controls.Add(this.Label51);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.lblEmpleado);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.toolAcciones);
            this.Controls.Add(this.toolTitulo);
            this.Name = "frmInfonavit";
            this.Text = "Infonavit";
            this.Load += new System.EventHandler(this.frmInfonavit_Load);
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
        internal System.Windows.Forms.Label Label16;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.Label label23;
        internal System.Windows.Forms.RadioButton rbtnPesos;
        internal System.Windows.Forms.RadioButton rbtnVsmdf;
        internal System.Windows.Forms.Label Label52;
        internal System.Windows.Forms.TextBox txtValor;
        internal System.Windows.Forms.RadioButton rbtnPorcentaje;
        internal System.Windows.Forms.TextBox txtNumeroCredito;
        internal System.Windows.Forms.Label Label50;
        internal System.Windows.Forms.Label Label51;
        private System.Windows.Forms.CheckBox chkActivo;
    }
}