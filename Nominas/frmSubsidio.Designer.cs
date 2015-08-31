namespace Nominas
{
    partial class frmSubsidio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSubsidio));
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolVentana = new System.Windows.Forms.ToolStripLabel();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolGuardarCerrar = new System.Windows.Forms.ToolStripButton();
            this.toolGuardarNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.txtAnio = new System.Windows.Forms.TextBox();
            this.txtDias = new System.Windows.Forms.TextBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
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
            this.toolTitulo.Size = new System.Drawing.Size(474, 27);
            this.toolTitulo.TabIndex = 6;
            this.toolTitulo.Text = "toolAcciones";
            // 
            // toolVentana
            // 
            this.toolVentana.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolVentana.Name = "toolVentana";
            this.toolVentana.Size = new System.Drawing.Size(155, 24);
            this.toolVentana.Text = "Nuevo subsidio";
            // 
            // toolAcciones
            // 
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGuardarCerrar,
            this.toolGuardarNuevo,
            this.toolCerrar});
            this.toolAcciones.Location = new System.Drawing.Point(0, 27);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(474, 25);
            this.toolAcciones.TabIndex = 7;
            this.toolAcciones.Text = "toolEmpresa";
            // 
            // toolGuardarCerrar
            // 
            this.toolGuardarCerrar.Image = ((System.Drawing.Image)(resources.GetObject("toolGuardarCerrar.Image")));
            this.toolGuardarCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGuardarCerrar.Name = "toolGuardarCerrar";
            this.toolGuardarCerrar.Size = new System.Drawing.Size(113, 22);
            this.toolGuardarCerrar.Text = "Guardar y Cerrar";
            this.toolGuardarCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolGuardarCerrar.Click += new System.EventHandler(this.toolGuardarCerrar_Click);
            // 
            // toolGuardarNuevo
            // 
            this.toolGuardarNuevo.Image = ((System.Drawing.Image)(resources.GetObject("toolGuardarNuevo.Image")));
            this.toolGuardarNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGuardarNuevo.Name = "toolGuardarNuevo";
            this.toolGuardarNuevo.Size = new System.Drawing.Size(116, 22);
            this.toolGuardarNuevo.Text = "Guardar y Nuevo";
            this.toolGuardarNuevo.Click += new System.EventHandler(this.toolGuardarNuevo_Click);
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
            // txtAnio
            // 
            this.txtAnio.Location = new System.Drawing.Point(99, 201);
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.Size = new System.Drawing.Size(100, 20);
            this.txtAnio.TabIndex = 130;
            // 
            // txtDias
            // 
            this.txtDias.Location = new System.Drawing.Point(99, 170);
            this.txtDias.Name = "txtDias";
            this.txtDias.Size = new System.Drawing.Size(100, 20);
            this.txtDias.TabIndex = 129;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(99, 144);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(100, 20);
            this.txtCantidad.TabIndex = 127;
            // 
            // txtDesde
            // 
            this.txtDesde.Location = new System.Drawing.Point(99, 113);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.Size = new System.Drawing.Size(100, 20);
            this.txtDesde.TabIndex = 126;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 125;
            this.label6.Text = "Año:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 124;
            this.label5.Text = "Periodo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 122;
            this.label3.Text = "Cantidad:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 121;
            this.label1.Text = "Desde:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(12, 70);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(160, 18);
            this.Label2.TabIndex = 120;
            this.Label2.Text = "Valores del subsidio";
            // 
            // frmSubsidio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 688);
            this.Controls.Add(this.txtAnio);
            this.Controls.Add(this.txtDias);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.txtDesde);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.toolAcciones);
            this.Controls.Add(this.toolTitulo);
            this.Name = "frmSubsidio";
            this.Text = "Subsidio";
            this.Load += new System.EventHandler(this.frmSubsidio_Load);
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
        internal System.Windows.Forms.ToolStripButton toolGuardarCerrar;
        internal System.Windows.Forms.ToolStripButton toolGuardarNuevo;
        private System.Windows.Forms.ToolStripButton toolCerrar;
        private System.Windows.Forms.TextBox txtAnio;
        private System.Windows.Forms.TextBox txtDias;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label Label2;
    }
}