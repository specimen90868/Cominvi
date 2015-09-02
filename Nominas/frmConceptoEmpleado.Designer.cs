namespace Nominas
{
    partial class frmConceptoEmpleado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConceptoEmpleado));
            this.toolVentanaTitulo = new System.Windows.Forms.ToolStrip();
            this.toolTitulo = new System.Windows.Forms.ToolStripLabel();
            this.toolBusqueda = new System.Windows.Forms.ToolStrip();
            this.toolNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolBaja = new System.Windows.Forms.ToolStripButton();
            this.dgvConceptosEmpleado = new System.Windows.Forms.DataGridView();
            this.toolVentanaTitulo.SuspendLayout();
            this.toolBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConceptosEmpleado)).BeginInit();
            this.SuspendLayout();
            // 
            // toolVentanaTitulo
            // 
            this.toolVentanaTitulo.BackColor = System.Drawing.Color.DarkGray;
            this.toolVentanaTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTitulo});
            this.toolVentanaTitulo.Location = new System.Drawing.Point(0, 0);
            this.toolVentanaTitulo.Name = "toolVentanaTitulo";
            this.toolVentanaTitulo.Size = new System.Drawing.Size(577, 27);
            this.toolVentanaTitulo.TabIndex = 7;
            this.toolVentanaTitulo.Text = "ToolStrip1";
            // 
            // toolTitulo
            // 
            this.toolTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(110, 24);
            this.toolTitulo.Text = "Conceptos";
            // 
            // toolBusqueda
            // 
            this.toolBusqueda.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNuevo,
            this.toolBaja});
            this.toolBusqueda.Location = new System.Drawing.Point(0, 27);
            this.toolBusqueda.Name = "toolBusqueda";
            this.toolBusqueda.Size = new System.Drawing.Size(577, 25);
            this.toolBusqueda.TabIndex = 8;
            this.toolBusqueda.Text = "ToolStrip1";
            // 
            // toolNuevo
            // 
            this.toolNuevo.Image = ((System.Drawing.Image)(resources.GetObject("toolNuevo.Image")));
            this.toolNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNuevo.Name = "toolNuevo";
            this.toolNuevo.Size = new System.Drawing.Size(62, 22);
            this.toolNuevo.Text = "Nuevo";
            this.toolNuevo.Click += new System.EventHandler(this.toolNuevo_Click);
            // 
            // toolBaja
            // 
            this.toolBaja.Image = ((System.Drawing.Image)(resources.GetObject("toolBaja.Image")));
            this.toolBaja.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBaja.Name = "toolBaja";
            this.toolBaja.Size = new System.Drawing.Size(49, 22);
            this.toolBaja.Text = "Baja";
            this.toolBaja.Click += new System.EventHandler(this.toolBaja_Click);
            // 
            // dgvConceptosEmpleado
            // 
            this.dgvConceptosEmpleado.AllowUserToAddRows = false;
            this.dgvConceptosEmpleado.AllowUserToDeleteRows = false;
            this.dgvConceptosEmpleado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConceptosEmpleado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConceptosEmpleado.Location = new System.Drawing.Point(0, 52);
            this.dgvConceptosEmpleado.Name = "dgvConceptosEmpleado";
            this.dgvConceptosEmpleado.ReadOnly = true;
            this.dgvConceptosEmpleado.Size = new System.Drawing.Size(577, 541);
            this.dgvConceptosEmpleado.TabIndex = 9;
            // 
            // frmConceptoEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 593);
            this.Controls.Add(this.dgvConceptosEmpleado);
            this.Controls.Add(this.toolBusqueda);
            this.Controls.Add(this.toolVentanaTitulo);
            this.Name = "frmConceptoEmpleado";
            this.Text = "Conceptos del empleado";
            this.Load += new System.EventHandler(this.frmConceptoEmpleado_Load);
            this.toolVentanaTitulo.ResumeLayout(false);
            this.toolVentanaTitulo.PerformLayout();
            this.toolBusqueda.ResumeLayout(false);
            this.toolBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConceptosEmpleado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolVentanaTitulo;
        internal System.Windows.Forms.ToolStripLabel toolTitulo;
        internal System.Windows.Forms.ToolStrip toolBusqueda;
        private System.Windows.Forms.ToolStripButton toolNuevo;
        private System.Windows.Forms.ToolStripButton toolBaja;
        private System.Windows.Forms.DataGridView dgvConceptosEmpleado;
    }
}