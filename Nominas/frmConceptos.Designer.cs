namespace Nominas
{
    partial class frmConceptos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConceptos));
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolVentana = new System.Windows.Forms.ToolStripLabel();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolGuardarCerrar = new System.Windows.Forms.ToolStripButton();
            this.toolGuardarNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.Label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtConcepto = new System.Windows.Forms.TextBox();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFormula = new System.Windows.Forms.TextBox();
            this.btnEditor = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtExento = new System.Windows.Forms.TextBox();
            this.btnEditor2 = new System.Windows.Forms.Button();
            this.chkVisible = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGrupoSat = new System.Windows.Forms.TextBox();
            this.btnGrupoSat = new System.Windows.Forms.Button();
            this.chkGrava = new System.Windows.Forms.CheckBox();
            this.chkExenta = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNoConcepto = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbPeriodo = new System.Windows.Forms.ComboBox();
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
            this.toolTitulo.Size = new System.Drawing.Size(678, 27);
            this.toolTitulo.TabIndex = 5;
            this.toolTitulo.Text = "toolAcciones";
            // 
            // toolVentana
            // 
            this.toolVentana.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolVentana.Name = "toolVentana";
            this.toolVentana.Size = new System.Drawing.Size(164, 24);
            this.toolVentana.Text = "Nuevo concepto";
            // 
            // toolAcciones
            // 
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGuardarCerrar,
            this.toolGuardarNuevo,
            this.toolCerrar});
            this.toolAcciones.Location = new System.Drawing.Point(0, 27);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(678, 25);
            this.toolAcciones.TabIndex = 6;
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
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(12, 69);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(155, 18);
            this.Label2.TabIndex = 107;
            this.Label2.Text = "Datos del concepto";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 108;
            this.label1.Text = "Nombre:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 109;
            this.label3.Text = "Tipo:";
            // 
            // txtConcepto
            // 
            this.txtConcepto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConcepto.Location = new System.Drawing.Point(102, 105);
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.Size = new System.Drawing.Size(148, 20);
            this.txtConcepto.TabIndex = 1;
            // 
            // cmbTipo
            // 
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "PERCEPCION",
            "DEDUCCION"});
            this.cmbTipo.Location = new System.Drawing.Point(102, 131);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(148, 21);
            this.cmbTipo.TabIndex = 2;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 112;
            this.label4.Text = "Fórmula:";
            // 
            // txtFormula
            // 
            this.txtFormula.Location = new System.Drawing.Point(102, 184);
            this.txtFormula.Name = "txtFormula";
            this.txtFormula.Size = new System.Drawing.Size(280, 20);
            this.txtFormula.TabIndex = 4;
            // 
            // btnEditor
            // 
            this.btnEditor.Location = new System.Drawing.Point(388, 182);
            this.btnEditor.Name = "btnEditor";
            this.btnEditor.Size = new System.Drawing.Size(43, 22);
            this.btnEditor.TabIndex = 5;
            this.btnEditor.Text = "Editor";
            this.btnEditor.UseVisualStyleBackColor = true;
            this.btnEditor.Click += new System.EventHandler(this.btnEditor_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 115;
            this.label5.Text = "Exento:";
            // 
            // txtExento
            // 
            this.txtExento.Location = new System.Drawing.Point(102, 210);
            this.txtExento.Name = "txtExento";
            this.txtExento.Size = new System.Drawing.Size(280, 20);
            this.txtExento.TabIndex = 6;
            // 
            // btnEditor2
            // 
            this.btnEditor2.Location = new System.Drawing.Point(388, 209);
            this.btnEditor2.Name = "btnEditor2";
            this.btnEditor2.Size = new System.Drawing.Size(43, 21);
            this.btnEditor2.TabIndex = 7;
            this.btnEditor2.Text = "Editor";
            this.btnEditor2.UseVisualStyleBackColor = true;
            this.btnEditor2.Click += new System.EventHandler(this.btnEditor2_Click);
            // 
            // chkVisible
            // 
            this.chkVisible.AutoSize = true;
            this.chkVisible.Location = new System.Drawing.Point(262, 108);
            this.chkVisible.Name = "chkVisible";
            this.chkVisible.Size = new System.Drawing.Size(56, 17);
            this.chkVisible.TabIndex = 12;
            this.chkVisible.Text = "Visible";
            this.chkVisible.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 240);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 119;
            this.label6.Text = "Clave Sat:";
            // 
            // txtGrupoSat
            // 
            this.txtGrupoSat.Location = new System.Drawing.Point(102, 236);
            this.txtGrupoSat.Name = "txtGrupoSat";
            this.txtGrupoSat.Size = new System.Drawing.Size(148, 20);
            this.txtGrupoSat.TabIndex = 8;
            this.txtGrupoSat.Text = "000";
            this.txtGrupoSat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnGrupoSat
            // 
            this.btnGrupoSat.Image = ((System.Drawing.Image)(resources.GetObject("btnGrupoSat.Image")));
            this.btnGrupoSat.Location = new System.Drawing.Point(256, 235);
            this.btnGrupoSat.Name = "btnGrupoSat";
            this.btnGrupoSat.Size = new System.Drawing.Size(43, 23);
            this.btnGrupoSat.TabIndex = 9;
            this.btnGrupoSat.UseVisualStyleBackColor = true;
            this.btnGrupoSat.Click += new System.EventHandler(this.btnGrupoSat_Click);
            // 
            // chkGrava
            // 
            this.chkGrava.AutoSize = true;
            this.chkGrava.Location = new System.Drawing.Point(102, 275);
            this.chkGrava.Name = "chkGrava";
            this.chkGrava.Size = new System.Drawing.Size(76, 17);
            this.chkGrava.TabIndex = 10;
            this.chkGrava.Text = "Grava ISR";
            this.chkGrava.UseVisualStyleBackColor = true;
            // 
            // chkExenta
            // 
            this.chkExenta.AutoSize = true;
            this.chkExenta.Location = new System.Drawing.Point(184, 275);
            this.chkExenta.Name = "chkExenta";
            this.chkExenta.Size = new System.Drawing.Size(80, 17);
            this.chkExenta.TabIndex = 11;
            this.chkExenta.Text = "Exenta ISR";
            this.chkExenta.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 124;
            this.label7.Text = "No. Concepto:";
            // 
            // txtNoConcepto
            // 
            this.txtNoConcepto.Location = new System.Drawing.Point(102, 158);
            this.txtNoConcepto.Name = "txtNoConcepto";
            this.txtNoConcepto.Size = new System.Drawing.Size(148, 20);
            this.txtNoConcepto.TabIndex = 3;
            this.txtNoConcepto.Text = "0";
            this.txtNoConcepto.Leave += new System.EventHandler(this.txtNoConcepto_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 305);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 125;
            this.label8.Text = "Periodo:";
            // 
            // cmbPeriodo
            // 
            this.cmbPeriodo.FormattingEnabled = true;
            this.cmbPeriodo.Location = new System.Drawing.Point(102, 302);
            this.cmbPeriodo.Name = "cmbPeriodo";
            this.cmbPeriodo.Size = new System.Drawing.Size(148, 21);
            this.cmbPeriodo.TabIndex = 126;
            // 
            // frmConceptos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 583);
            this.Controls.Add(this.cmbPeriodo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtNoConcepto);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkExenta);
            this.Controls.Add(this.chkGrava);
            this.Controls.Add(this.btnGrupoSat);
            this.Controls.Add(this.txtGrupoSat);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkVisible);
            this.Controls.Add(this.btnEditor2);
            this.Controls.Add(this.txtExento);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnEditor);
            this.Controls.Add(this.txtFormula);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.txtConcepto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.toolAcciones);
            this.Controls.Add(this.toolTitulo);
            this.Name = "frmConceptos";
            this.Text = "Conceptos";
            this.Load += new System.EventHandler(this.frmConceptos_Load);
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
        internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtConcepto;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFormula;
        private System.Windows.Forms.Button btnEditor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtExento;
        private System.Windows.Forms.Button btnEditor2;
        private System.Windows.Forms.CheckBox chkVisible;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtGrupoSat;
        private System.Windows.Forms.Button btnGrupoSat;
        private System.Windows.Forms.CheckBox chkGrava;
        private System.Windows.Forms.CheckBox chkExenta;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNoConcepto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbPeriodo;
    }
}