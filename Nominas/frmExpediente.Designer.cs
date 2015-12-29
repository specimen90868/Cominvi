namespace Nominas
{
    partial class frmExpediente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExpediente));
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolVentana = new System.Windows.Forms.ToolStripLabel();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.chkAutorizacion = new System.Windows.Forms.CheckBox();
            this.chkFotografias = new System.Windows.Forms.CheckBox();
            this.chkAfore = new System.Windows.Forms.CheckBox();
            this.chkInfonavit = new System.Windows.Forms.CheckBox();
            this.chkRfc = new System.Windows.Forms.CheckBox();
            this.chkNss = new System.Windows.Forms.CheckBox();
            this.chkCDomicilio = new System.Windows.Forms.CheckBox();
            this.chkCurp = new System.Windows.Forms.CheckBox();
            this.chkIFE = new System.Windows.Forms.CheckBox();
            this.chkActa = new System.Windows.Forms.CheckBox();
            this.chkCredencial = new System.Windows.Forms.CheckBox();
            this.chkImss = new System.Windows.Forms.CheckBox();
            this.chkSolicitud = new System.Windows.Forms.CheckBox();
            this.chkContrato = new System.Windows.Forms.CheckBox();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
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
            this.toolTitulo.Size = new System.Drawing.Size(469, 27);
            this.toolTitulo.TabIndex = 3;
            this.toolTitulo.Text = "toolAcciones";
            // 
            // toolVentana
            // 
            this.toolVentana.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolVentana.Name = "toolVentana";
            this.toolVentana.Size = new System.Drawing.Size(182, 24);
            this.toolVentana.Text = "Nuevo expediente";
            // 
            // toolAcciones
            // 
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGuardar,
            this.toolBuscar,
            this.toolCerrar});
            this.toolAcciones.Location = new System.Drawing.Point(0, 27);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(469, 25);
            this.toolAcciones.TabIndex = 4;
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
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.Location = new System.Drawing.Point(119, 81);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(183, 20);
            this.lblEmpleado.TabIndex = 253;
            this.lblEmpleado.Text = "Nombre del empleado";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(19, 81);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(94, 20);
            this.label23.TabIndex = 252;
            this.label23.Text = "Empleado:";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.Color.White;
            this.Label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.Label16.Location = new System.Drawing.Point(20, 132);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(195, 18);
            this.Label16.TabIndex = 254;
            this.Label16.Text = "Documentación obtenida";
            // 
            // chkAutorizacion
            // 
            this.chkAutorizacion.AutoSize = true;
            this.chkAutorizacion.Location = new System.Drawing.Point(189, 410);
            this.chkAutorizacion.Name = "chkAutorizacion";
            this.chkAutorizacion.Size = new System.Drawing.Size(143, 17);
            this.chkAutorizacion.TabIndex = 268;
            this.chkAutorizacion.Text = "Autorización permanente";
            this.chkAutorizacion.UseVisualStyleBackColor = true;
            // 
            // chkFotografias
            // 
            this.chkFotografias.AutoSize = true;
            this.chkFotografias.Location = new System.Drawing.Point(189, 371);
            this.chkFotografias.Name = "chkFotografias";
            this.chkFotografias.Size = new System.Drawing.Size(78, 17);
            this.chkFotografias.TabIndex = 267;
            this.chkFotografias.Text = "Fotografias";
            this.chkFotografias.UseVisualStyleBackColor = true;
            // 
            // chkAfore
            // 
            this.chkAfore.AutoSize = true;
            this.chkAfore.Location = new System.Drawing.Point(189, 332);
            this.chkAfore.Name = "chkAfore";
            this.chkAfore.Size = new System.Drawing.Size(51, 17);
            this.chkAfore.TabIndex = 266;
            this.chkAfore.Text = "Afore";
            this.chkAfore.UseVisualStyleBackColor = true;
            // 
            // chkInfonavit
            // 
            this.chkInfonavit.AutoSize = true;
            this.chkInfonavit.Location = new System.Drawing.Point(189, 293);
            this.chkInfonavit.Name = "chkInfonavit";
            this.chkInfonavit.Size = new System.Drawing.Size(67, 17);
            this.chkInfonavit.TabIndex = 265;
            this.chkInfonavit.Text = "Infonavit";
            this.chkInfonavit.UseVisualStyleBackColor = true;
            // 
            // chkRfc
            // 
            this.chkRfc.AutoSize = true;
            this.chkRfc.Location = new System.Drawing.Point(189, 254);
            this.chkRfc.Name = "chkRfc";
            this.chkRfc.Size = new System.Drawing.Size(47, 17);
            this.chkRfc.TabIndex = 264;
            this.chkRfc.Text = "RFC";
            this.chkRfc.UseVisualStyleBackColor = true;
            // 
            // chkNss
            // 
            this.chkNss.AutoSize = true;
            this.chkNss.Location = new System.Drawing.Point(189, 215);
            this.chkNss.Name = "chkNss";
            this.chkNss.Size = new System.Drawing.Size(57, 17);
            this.chkNss.TabIndex = 263;
            this.chkNss.Text = "N.S.S.";
            this.chkNss.UseVisualStyleBackColor = true;
            // 
            // chkCDomicilio
            // 
            this.chkCDomicilio.AutoSize = true;
            this.chkCDomicilio.Location = new System.Drawing.Point(189, 176);
            this.chkCDomicilio.Name = "chkCDomicilio";
            this.chkCDomicilio.Size = new System.Drawing.Size(147, 17);
            this.chkCDomicilio.TabIndex = 262;
            this.chkCDomicilio.Text = "Comprobante de domicilio";
            this.chkCDomicilio.UseVisualStyleBackColor = true;
            // 
            // chkCurp
            // 
            this.chkCurp.AutoSize = true;
            this.chkCurp.Location = new System.Drawing.Point(23, 410);
            this.chkCurp.Name = "chkCurp";
            this.chkCurp.Size = new System.Drawing.Size(56, 17);
            this.chkCurp.TabIndex = 261;
            this.chkCurp.Text = "CURP";
            this.chkCurp.UseVisualStyleBackColor = true;
            // 
            // chkIFE
            // 
            this.chkIFE.AutoSize = true;
            this.chkIFE.Location = new System.Drawing.Point(23, 371);
            this.chkIFE.Name = "chkIFE";
            this.chkIFE.Size = new System.Drawing.Size(126, 17);
            this.chkIFE.TabIndex = 260;
            this.chkIFE.Text = "Credencial de elector";
            this.chkIFE.UseVisualStyleBackColor = true;
            // 
            // chkActa
            // 
            this.chkActa.AutoSize = true;
            this.chkActa.Location = new System.Drawing.Point(23, 332);
            this.chkActa.Name = "chkActa";
            this.chkActa.Size = new System.Drawing.Size(117, 17);
            this.chkActa.TabIndex = 259;
            this.chkActa.Text = "Acta de nacimiento";
            this.chkActa.UseVisualStyleBackColor = true;
            // 
            // chkCredencial
            // 
            this.chkCredencial.AutoSize = true;
            this.chkCredencial.Location = new System.Drawing.Point(23, 293);
            this.chkCredencial.Name = "chkCredencial";
            this.chkCredencial.Size = new System.Drawing.Size(111, 17);
            this.chkCredencial.TabIndex = 258;
            this.chkCredencial.Text = "Credencial interna";
            this.chkCredencial.UseVisualStyleBackColor = true;
            // 
            // chkImss
            // 
            this.chkImss.AutoSize = true;
            this.chkImss.Location = new System.Drawing.Point(23, 254);
            this.chkImss.Name = "chkImss";
            this.chkImss.Size = new System.Drawing.Size(90, 17);
            this.chkImss.TabIndex = 257;
            this.chkImss.Text = "Alta del IMSS";
            this.chkImss.UseVisualStyleBackColor = true;
            // 
            // chkSolicitud
            // 
            this.chkSolicitud.AutoSize = true;
            this.chkSolicitud.Location = new System.Drawing.Point(23, 215);
            this.chkSolicitud.Name = "chkSolicitud";
            this.chkSolicitud.Size = new System.Drawing.Size(66, 17);
            this.chkSolicitud.TabIndex = 256;
            this.chkSolicitud.Text = "Solicitud";
            this.chkSolicitud.UseVisualStyleBackColor = true;
            // 
            // chkContrato
            // 
            this.chkContrato.AutoSize = true;
            this.chkContrato.Location = new System.Drawing.Point(23, 176);
            this.chkContrato.Name = "chkContrato";
            this.chkContrato.Size = new System.Drawing.Size(66, 17);
            this.chkContrato.TabIndex = 255;
            this.chkContrato.Text = "Contrato";
            this.chkContrato.UseVisualStyleBackColor = true;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(23, 470);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(313, 139);
            this.txtObservaciones.TabIndex = 270;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(20, 445);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(78, 13);
            this.Label3.TabIndex = 269;
            this.Label3.Text = "Observaciones";
            // 
            // frmExpediente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 686);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.chkAutorizacion);
            this.Controls.Add(this.chkFotografias);
            this.Controls.Add(this.chkAfore);
            this.Controls.Add(this.chkInfonavit);
            this.Controls.Add(this.chkRfc);
            this.Controls.Add(this.chkNss);
            this.Controls.Add(this.chkCDomicilio);
            this.Controls.Add(this.chkCurp);
            this.Controls.Add(this.chkIFE);
            this.Controls.Add(this.chkActa);
            this.Controls.Add(this.chkCredencial);
            this.Controls.Add(this.chkImss);
            this.Controls.Add(this.chkSolicitud);
            this.Controls.Add(this.chkContrato);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.lblEmpleado);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.toolAcciones);
            this.Controls.Add(this.toolTitulo);
            this.Name = "frmExpediente";
            this.Text = "Expediente";
            this.Load += new System.EventHandler(this.frmExpediente_Load);
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
        private System.Windows.Forms.ToolStripButton toolCerrar;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.Label label23;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.CheckBox chkAutorizacion;
        internal System.Windows.Forms.CheckBox chkFotografias;
        internal System.Windows.Forms.CheckBox chkAfore;
        internal System.Windows.Forms.CheckBox chkInfonavit;
        internal System.Windows.Forms.CheckBox chkRfc;
        internal System.Windows.Forms.CheckBox chkNss;
        internal System.Windows.Forms.CheckBox chkCDomicilio;
        internal System.Windows.Forms.CheckBox chkCurp;
        internal System.Windows.Forms.CheckBox chkIFE;
        internal System.Windows.Forms.CheckBox chkActa;
        internal System.Windows.Forms.CheckBox chkCredencial;
        internal System.Windows.Forms.CheckBox chkImss;
        internal System.Windows.Forms.CheckBox chkSolicitud;
        internal System.Windows.Forms.CheckBox chkContrato;
        internal System.Windows.Forms.TextBox txtObservaciones;
        internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.ToolStripButton toolBuscar;
    }
}