namespace Nominas
{
    partial class frmPerfiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPerfiles));
            this.toolPerfil = new System.Windows.Forms.ToolStrip();
            this.toolGuardarCerrar = new System.Windows.Forms.ToolStripButton();
            this.toolGuardarNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolTitulo = new System.Windows.Forms.ToolStripLabel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkRecursosHumanos = new System.Windows.Forms.CheckBox();
            this.chkSeguroSocial = new System.Windows.Forms.CheckBox();
            this.chkNominas = new System.Windows.Forms.CheckBox();
            this.chkCatalogos = new System.Windows.Forms.CheckBox();
            this.chkConfiguracion = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbMenus = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbPermiso = new System.Windows.Forms.ComboBox();
            this.chkAccion = new System.Windows.Forms.CheckBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.dgvPermisos = new System.Windows.Forms.DataGridView();
            this.toolPerfil.SuspendLayout();
            this.toolAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermisos)).BeginInit();
            this.SuspendLayout();
            // 
            // toolPerfil
            // 
            this.toolPerfil.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGuardarCerrar,
            this.toolGuardarNuevo,
            this.toolCerrar});
            this.toolPerfil.Location = new System.Drawing.Point(0, 27);
            this.toolPerfil.Name = "toolPerfil";
            this.toolPerfil.Size = new System.Drawing.Size(601, 25);
            this.toolPerfil.TabIndex = 4;
            this.toolPerfil.Text = "toolEmpresa";
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
            // toolAcciones
            // 
            this.toolAcciones.BackColor = System.Drawing.Color.DarkGray;
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTitulo});
            this.toolAcciones.Location = new System.Drawing.Point(0, 0);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(601, 27);
            this.toolAcciones.TabIndex = 3;
            this.toolAcciones.Text = "toolAcciones";
            // 
            // toolTitulo
            // 
            this.toolTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(123, 24);
            this.toolTitulo.Text = "Nuevo perfil";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(21, 73);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(137, 18);
            this.lblTitulo.TabIndex = 101;
            this.lblTitulo.Text = "Nombre del perfil";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 102;
            this.label1.Text = "Perfil:";
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Location = new System.Drawing.Point(60, 111);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(130, 20);
            this.txtNombre.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(21, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 18);
            this.label2.TabIndex = 103;
            this.label2.Text = "Módulos";
            // 
            // chkRecursosHumanos
            // 
            this.chkRecursosHumanos.AutoSize = true;
            this.chkRecursosHumanos.Location = new System.Drawing.Point(24, 188);
            this.chkRecursosHumanos.Name = "chkRecursosHumanos";
            this.chkRecursosHumanos.Size = new System.Drawing.Size(119, 17);
            this.chkRecursosHumanos.TabIndex = 104;
            this.chkRecursosHumanos.Text = "Recursos Humanos";
            this.chkRecursosHumanos.UseVisualStyleBackColor = true;
            // 
            // chkSeguroSocial
            // 
            this.chkSeguroSocial.AutoSize = true;
            this.chkSeguroSocial.Location = new System.Drawing.Point(156, 188);
            this.chkSeguroSocial.Name = "chkSeguroSocial";
            this.chkSeguroSocial.Size = new System.Drawing.Size(92, 17);
            this.chkSeguroSocial.TabIndex = 105;
            this.chkSeguroSocial.Text = "Seguro Social";
            this.chkSeguroSocial.UseVisualStyleBackColor = true;
            // 
            // chkNominas
            // 
            this.chkNominas.AutoSize = true;
            this.chkNominas.Location = new System.Drawing.Point(281, 188);
            this.chkNominas.Name = "chkNominas";
            this.chkNominas.Size = new System.Drawing.Size(67, 17);
            this.chkNominas.TabIndex = 107;
            this.chkNominas.Text = "Nominas";
            this.chkNominas.UseVisualStyleBackColor = true;
            // 
            // chkCatalogos
            // 
            this.chkCatalogos.AutoSize = true;
            this.chkCatalogos.Location = new System.Drawing.Point(372, 188);
            this.chkCatalogos.Name = "chkCatalogos";
            this.chkCatalogos.Size = new System.Drawing.Size(73, 17);
            this.chkCatalogos.TabIndex = 108;
            this.chkCatalogos.Text = "Catálogos";
            this.chkCatalogos.UseVisualStyleBackColor = true;
            // 
            // chkConfiguracion
            // 
            this.chkConfiguracion.AutoSize = true;
            this.chkConfiguracion.Location = new System.Drawing.Point(468, 188);
            this.chkConfiguracion.Name = "chkConfiguracion";
            this.chkConfiguracion.Size = new System.Drawing.Size(91, 17);
            this.chkConfiguracion.TabIndex = 109;
            this.chkConfiguracion.Text = "Configuración";
            this.chkConfiguracion.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(21, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 110;
            this.label3.Text = "Permisos";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 284);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 111;
            this.label4.Text = "Menu:";
            // 
            // cmbMenus
            // 
            this.cmbMenus.FormattingEnabled = true;
            this.cmbMenus.Location = new System.Drawing.Point(89, 281);
            this.cmbMenus.Name = "cmbMenus";
            this.cmbMenus.Size = new System.Drawing.Size(159, 21);
            this.cmbMenus.TabIndex = 112;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 315);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 113;
            this.label5.Text = "Permiso";
            // 
            // cmbPermiso
            // 
            this.cmbPermiso.FormattingEnabled = true;
            this.cmbPermiso.Location = new System.Drawing.Point(89, 312);
            this.cmbPermiso.Name = "cmbPermiso";
            this.cmbPermiso.Size = new System.Drawing.Size(159, 21);
            this.cmbPermiso.TabIndex = 114;
            // 
            // chkAccion
            // 
            this.chkAccion.AutoSize = true;
            this.chkAccion.Location = new System.Drawing.Point(254, 314);
            this.chkAccion.Name = "chkAccion";
            this.chkAccion.Size = new System.Drawing.Size(64, 17);
            this.chkAccion.TabIndex = 115;
            this.chkAccion.Text = "Habilitar";
            this.chkAccion.UseVisualStyleBackColor = true;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(89, 339);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 116;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnQuitar
            // 
            this.btnQuitar.Image = ((System.Drawing.Image)(resources.GetObject("btnQuitar.Image")));
            this.btnQuitar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuitar.Location = new System.Drawing.Point(173, 339);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(75, 23);
            this.btnQuitar.TabIndex = 117;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // dgvPermisos
            // 
            this.dgvPermisos.AllowUserToAddRows = false;
            this.dgvPermisos.AllowUserToDeleteRows = false;
            this.dgvPermisos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPermisos.Location = new System.Drawing.Point(29, 368);
            this.dgvPermisos.Name = "dgvPermisos";
            this.dgvPermisos.ReadOnly = true;
            this.dgvPermisos.Size = new System.Drawing.Size(530, 216);
            this.dgvPermisos.TabIndex = 118;
            // 
            // frmPerfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 613);
            this.Controls.Add(this.dgvPermisos);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.chkAccion);
            this.Controls.Add(this.cmbPermiso);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbMenus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkConfiguracion);
            this.Controls.Add(this.chkCatalogos);
            this.Controls.Add(this.chkNominas);
            this.Controls.Add(this.chkSeguroSocial);
            this.Controls.Add(this.chkRecursosHumanos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.toolPerfil);
            this.Controls.Add(this.toolAcciones);
            this.Name = "frmPerfiles";
            this.Text = "Perfiles";
            this.Load += new System.EventHandler(this.frmPerfiles_Load);
            this.toolPerfil.ResumeLayout(false);
            this.toolPerfil.PerformLayout();
            this.toolAcciones.ResumeLayout(false);
            this.toolAcciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermisos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolPerfil;
        internal System.Windows.Forms.ToolStripButton toolGuardarCerrar;
        internal System.Windows.Forms.ToolStripButton toolGuardarNuevo;
        private System.Windows.Forms.ToolStripButton toolCerrar;
        internal System.Windows.Forms.ToolStrip toolAcciones;
        internal System.Windows.Forms.ToolStripLabel toolTitulo;
        internal System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombre;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkRecursosHumanos;
        private System.Windows.Forms.CheckBox chkSeguroSocial;
        private System.Windows.Forms.CheckBox chkNominas;
        private System.Windows.Forms.CheckBox chkCatalogos;
        private System.Windows.Forms.CheckBox chkConfiguracion;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbMenus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPermiso;
        private System.Windows.Forms.CheckBox chkAccion;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.DataGridView dgvPermisos;
    }
}