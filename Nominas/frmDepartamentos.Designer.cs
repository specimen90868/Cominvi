namespace Nominas
{
    partial class frmDepartamentos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDepartamentos));
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolTitulo = new System.Windows.Forms.ToolStripLabel();
            this.toolDepartamento = new System.Windows.Forms.ToolStrip();
            this.toolGuardarCerrar = new System.Windows.Forms.ToolStripButton();
            this.toolGuardarNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblDeptoPuesto = new System.Windows.Forms.Label();
            this.toolAcciones.SuspendLayout();
            this.toolDepartamento.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolAcciones
            // 
            this.toolAcciones.BackColor = System.Drawing.Color.DarkGray;
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTitulo});
            this.toolAcciones.Location = new System.Drawing.Point(0, 0);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(576, 27);
            this.toolAcciones.TabIndex = 2;
            this.toolAcciones.Text = "toolAcciones";
            // 
            // toolTitulo
            // 
            this.toolTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(205, 24);
            this.toolTitulo.Text = "Nuevo departamento";
            // 
            // toolDepartamento
            // 
            this.toolDepartamento.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGuardarCerrar,
            this.toolGuardarNuevo,
            this.toolCerrar});
            this.toolDepartamento.Location = new System.Drawing.Point(0, 27);
            this.toolDepartamento.Name = "toolDepartamento";
            this.toolDepartamento.Size = new System.Drawing.Size(576, 25);
            this.toolDepartamento.TabIndex = 3;
            this.toolDepartamento.Text = "toolEmpresa";
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
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.Label1.Location = new System.Drawing.Point(24, 76);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(176, 18);
            this.Label1.TabIndex = 97;
            this.Label1.Text = "Nombre departamento";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(104, 117);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(289, 20);
            this.txtDescripcion.TabIndex = 96;
            // 
            // lblDeptoPuesto
            // 
            this.lblDeptoPuesto.AutoSize = true;
            this.lblDeptoPuesto.Location = new System.Drawing.Point(24, 120);
            this.lblDeptoPuesto.Name = "lblDeptoPuesto";
            this.lblDeptoPuesto.Size = new System.Drawing.Size(77, 13);
            this.lblDeptoPuesto.TabIndex = 95;
            this.lblDeptoPuesto.Text = "Departamento:";
            // 
            // frmDepartamentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 488);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.lblDeptoPuesto);
            this.Controls.Add(this.toolDepartamento);
            this.Controls.Add(this.toolAcciones);
            this.Name = "frmDepartamentos";
            this.Text = "Departamentos";
            this.Load += new System.EventHandler(this.frmDepartamentos_Load);
            this.toolAcciones.ResumeLayout(false);
            this.toolAcciones.PerformLayout();
            this.toolDepartamento.ResumeLayout(false);
            this.toolDepartamento.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolAcciones;
        internal System.Windows.Forms.ToolStripLabel toolTitulo;
        internal System.Windows.Forms.ToolStrip toolDepartamento;
        internal System.Windows.Forms.ToolStripButton toolGuardarCerrar;
        internal System.Windows.Forms.ToolStripButton toolGuardarNuevo;
        private System.Windows.Forms.ToolStripButton toolCerrar;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtDescripcion;
        internal System.Windows.Forms.Label lblDeptoPuesto;
    }
}