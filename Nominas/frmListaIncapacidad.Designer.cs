namespace Nominas
{
    partial class frmListaIncapacidad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaIncapacidad));
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolEmpleados = new System.Windows.Forms.ToolStripLabel();
            this.toolBusqueda = new System.Windows.Forms.ToolStrip();
            this.toolNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblBuscar = new System.Windows.Forms.ToolStripLabel();
            this.txtBuscar = new System.Windows.Forms.ToolStripTextBox();
            this.dgvIncapacidad = new System.Windows.Forms.DataGridView();
            this.toolTitulo.SuspendLayout();
            this.toolBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncapacidad)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTitulo
            // 
            this.toolTitulo.BackColor = System.Drawing.Color.DarkGray;
            this.toolTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolEmpleados});
            this.toolTitulo.Location = new System.Drawing.Point(0, 0);
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(710, 27);
            this.toolTitulo.TabIndex = 8;
            this.toolTitulo.Text = "ToolStrip1";
            // 
            // toolEmpleados
            // 
            this.toolEmpleados.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolEmpleados.Name = "toolEmpleados";
            this.toolEmpleados.Size = new System.Drawing.Size(380, 24);
            this.toolEmpleados.Text = "Empleados - Historial de incapacidades";
            // 
            // toolBusqueda
            // 
            this.toolBusqueda.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNuevo,
            this.toolEliminar,
            this.toolStripSeparator1,
            this.lblBuscar,
            this.txtBuscar});
            this.toolBusqueda.Location = new System.Drawing.Point(0, 27);
            this.toolBusqueda.Name = "toolBusqueda";
            this.toolBusqueda.Size = new System.Drawing.Size(710, 25);
            this.toolBusqueda.TabIndex = 9;
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
            // toolEliminar
            // 
            this.toolEliminar.Image = ((System.Drawing.Image)(resources.GetObject("toolEliminar.Image")));
            this.toolEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolEliminar.Name = "toolEliminar";
            this.toolEliminar.Size = new System.Drawing.Size(70, 22);
            this.toolEliminar.Text = "Eliminar";
            this.toolEliminar.Click += new System.EventHandler(this.toolEliminar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lblBuscar
            // 
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(45, 22);
            this.lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.txtBuscar.ForeColor = System.Drawing.Color.Gray;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(300, 25);
            this.txtBuscar.Text = "Buscar empleado...";
            this.txtBuscar.Leave += new System.EventHandler(this.txtBuscar_Leave);
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            this.txtBuscar.Click += new System.EventHandler(this.txtBuscar_Click);
            // 
            // dgvIncapacidad
            // 
            this.dgvIncapacidad.AllowUserToAddRows = false;
            this.dgvIncapacidad.AllowUserToDeleteRows = false;
            this.dgvIncapacidad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIncapacidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIncapacidad.Location = new System.Drawing.Point(0, 52);
            this.dgvIncapacidad.Name = "dgvIncapacidad";
            this.dgvIncapacidad.ReadOnly = true;
            this.dgvIncapacidad.Size = new System.Drawing.Size(710, 642);
            this.dgvIncapacidad.TabIndex = 10;
            // 
            // frmListaIncapacidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 694);
            this.Controls.Add(this.dgvIncapacidad);
            this.Controls.Add(this.toolBusqueda);
            this.Controls.Add(this.toolTitulo);
            this.Name = "frmListaIncapacidad";
            this.Text = "Lista de incapacidades";
            this.Load += new System.EventHandler(this.frmListaIncapacidad_Load);
            this.toolTitulo.ResumeLayout(false);
            this.toolTitulo.PerformLayout();
            this.toolBusqueda.ResumeLayout(false);
            this.toolBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncapacidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolTitulo;
        internal System.Windows.Forms.ToolStripLabel toolEmpleados;
        internal System.Windows.Forms.ToolStrip toolBusqueda;
        private System.Windows.Forms.ToolStripButton toolNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripLabel lblBuscar;
        internal System.Windows.Forms.ToolStripTextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvIncapacidad;
        private System.Windows.Forms.ToolStripButton toolEliminar;
    }
}