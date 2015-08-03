namespace Nominas
{
    partial class frmClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientes));
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolTitulo = new System.Windows.Forms.ToolStripLabel();
            this.toolCliente = new System.Windows.Forms.ToolStrip();
            this.toolGuardarCerrar = new System.Windows.Forms.ToolStripButton();
            this.toolGuardarNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtRfc = new System.Windows.Forms.TextBox();
            this.txtMunicipio = new System.Windows.Forms.TextBox();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtCP = new System.Windows.Forms.MaskedTextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtColonia = new System.Windows.Forms.TextBox();
            this.txtInterior = new System.Windows.Forms.TextBox();
            this.txtExterior = new System.Windows.Forms.TextBox();
            this.txtCalle = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPais = new System.Windows.Forms.TextBox();
            this.toolAcciones.SuspendLayout();
            this.toolCliente.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolAcciones
            // 
            this.toolAcciones.BackColor = System.Drawing.Color.DarkGray;
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTitulo});
            this.toolAcciones.Location = new System.Drawing.Point(0, 0);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(670, 27);
            this.toolAcciones.TabIndex = 1;
            this.toolAcciones.Text = "toolAcciones";
            // 
            // toolTitulo
            // 
            this.toolTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(139, 24);
            this.toolTitulo.Text = "Nuevo cliente";
            // 
            // toolCliente
            // 
            this.toolCliente.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGuardarCerrar,
            this.toolGuardarNuevo,
            this.toolCerrar});
            this.toolCliente.Location = new System.Drawing.Point(0, 27);
            this.toolCliente.Name = "toolCliente";
            this.toolCliente.Size = new System.Drawing.Size(670, 25);
            this.toolCliente.TabIndex = 2;
            this.toolCliente.Text = "toolEmpresa";
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
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.Label9.Location = new System.Drawing.Point(23, 403);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(72, 18);
            this.Label9.TabIndex = 126;
            this.Label9.Text = "Registro";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.Label7.Location = new System.Drawing.Point(23, 177);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(80, 18);
            this.Label7.TabIndex = 125;
            this.Label7.Text = "Dirección";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.Label6.Location = new System.Drawing.Point(23, 77);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(122, 18);
            this.Label6.TabIndex = 124;
            this.Label6.Text = "Nombre cliente";
            // 
            // txtRfc
            // 
            this.txtRfc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRfc.Location = new System.Drawing.Point(145, 441);
            this.txtRfc.Name = "txtRfc";
            this.txtRfc.Size = new System.Drawing.Size(117, 20);
            this.txtRfc.TabIndex = 10;
            this.txtRfc.Leave += new System.EventHandler(this.txtRfc_Leave);
            // 
            // txtMunicipio
            // 
            this.txtMunicipio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMunicipio.Location = new System.Drawing.Point(145, 312);
            this.txtMunicipio.Name = "txtMunicipio";
            this.txtMunicipio.Size = new System.Drawing.Size(117, 20);
            this.txtMunicipio.TabIndex = 6;
            // 
            // txtEstado
            // 
            this.txtEstado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEstado.Location = new System.Drawing.Point(145, 338);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Size = new System.Drawing.Size(118, 20);
            this.txtEstado.TabIndex = 7;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(23, 315);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(119, 13);
            this.Label11.TabIndex = 119;
            this.Label11.Text = "Municipio o delegación:";
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(96, 341);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(43, 13);
            this.Label13.TabIndex = 120;
            this.Label13.Text = "Estado:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(307, 315);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(30, 13);
            this.Label10.TabIndex = 118;
            this.Label10.Text = "C.P.:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(49, 126);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(93, 13);
            this.Label1.TabIndex = 110;
            this.Label1.Text = "Nombre completo:";
            // 
            // txtCP
            // 
            this.txtCP.Location = new System.Drawing.Point(340, 312);
            this.txtCP.Mask = "00000";
            this.txtCP.Name = "txtCP";
            this.txtCP.Size = new System.Drawing.Size(49, 20);
            this.txtCP.TabIndex = 9;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(102, 444);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(40, 13);
            this.Label8.TabIndex = 108;
            this.Label8.Text = "R.F.C.:";
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Location = new System.Drawing.Point(145, 125);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(426, 20);
            this.txtNombre.TabIndex = 1;
            // 
            // txtColonia
            // 
            this.txtColonia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtColonia.Location = new System.Drawing.Point(145, 286);
            this.txtColonia.Name = "txtColonia";
            this.txtColonia.Size = new System.Drawing.Size(426, 20);
            this.txtColonia.TabIndex = 5;
            // 
            // txtInterior
            // 
            this.txtInterior.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtInterior.Location = new System.Drawing.Point(145, 261);
            this.txtInterior.Name = "txtInterior";
            this.txtInterior.Size = new System.Drawing.Size(67, 20);
            this.txtInterior.TabIndex = 4;
            // 
            // txtExterior
            // 
            this.txtExterior.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtExterior.Location = new System.Drawing.Point(145, 236);
            this.txtExterior.Name = "txtExterior";
            this.txtExterior.Size = new System.Drawing.Size(67, 20);
            this.txtExterior.TabIndex = 3;
            // 
            // txtCalle
            // 
            this.txtCalle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCalle.Location = new System.Drawing.Point(145, 211);
            this.txtCalle.Name = "txtCalle";
            this.txtCalle.Size = new System.Drawing.Size(426, 20);
            this.txtCalle.TabIndex = 2;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(109, 214);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(33, 13);
            this.Label2.TabIndex = 106;
            this.Label2.Text = "Calle:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(80, 264);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(62, 13);
            this.Label3.TabIndex = 109;
            this.Label3.Text = "No. Interior:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(96, 289);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(45, 13);
            this.Label5.TabIndex = 112;
            this.Label5.Text = "Colonia:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(77, 239);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(65, 13);
            this.Label4.TabIndex = 111;
            this.Label4.Text = "No. Exterior:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(109, 367);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 13);
            this.label12.TabIndex = 127;
            this.label12.Text = "Pais:";
            // 
            // txtPais
            // 
            this.txtPais.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPais.Location = new System.Drawing.Point(145, 364);
            this.txtPais.Name = "txtPais";
            this.txtPais.Size = new System.Drawing.Size(117, 20);
            this.txtPais.TabIndex = 8;
            // 
            // frmClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 527);
            this.Controls.Add(this.txtPais);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.txtRfc);
            this.Controls.Add(this.txtMunicipio);
            this.Controls.Add(this.txtEstado);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtCP);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtColonia);
            this.Controls.Add(this.txtInterior);
            this.Controls.Add(this.txtExterior);
            this.Controls.Add(this.txtCalle);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.toolCliente);
            this.Controls.Add(this.toolAcciones);
            this.Name = "frmClientes";
            this.Text = "Clientes";
            this.Load += new System.EventHandler(this.frmClientes_Load);
            this.toolAcciones.ResumeLayout(false);
            this.toolAcciones.PerformLayout();
            this.toolCliente.ResumeLayout(false);
            this.toolCliente.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolAcciones;
        internal System.Windows.Forms.ToolStripLabel toolTitulo;
        internal System.Windows.Forms.ToolStrip toolCliente;
        internal System.Windows.Forms.ToolStripButton toolGuardarCerrar;
        internal System.Windows.Forms.ToolStripButton toolGuardarNuevo;
        private System.Windows.Forms.ToolStripButton toolCerrar;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox txtRfc;
        internal System.Windows.Forms.TextBox txtMunicipio;
        internal System.Windows.Forms.TextBox txtEstado;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.MaskedTextBox txtCP;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.TextBox txtNombre;
        internal System.Windows.Forms.TextBox txtColonia;
        internal System.Windows.Forms.TextBox txtInterior;
        internal System.Windows.Forms.TextBox txtExterior;
        internal System.Windows.Forms.TextBox txtCalle;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPais;
    }
}