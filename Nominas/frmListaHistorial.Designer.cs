namespace Nominas
{
    partial class frmListaHistorial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaHistorial));
            this.toolVentana = new System.Windows.Forms.ToolStrip();
            this.toolNombreVentana = new System.Windows.Forms.ToolStripLabel();
            this.toolBusqueda = new System.Windows.Forms.ToolStrip();
            this.toolExportar = new System.Windows.Forms.ToolStripButton();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.toolVentana.SuspendLayout();
            this.toolBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();
            // 
            // toolVentana
            // 
            this.toolVentana.BackColor = System.Drawing.Color.DarkGray;
            this.toolVentana.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNombreVentana});
            this.toolVentana.Location = new System.Drawing.Point(0, 0);
            this.toolVentana.Name = "toolVentana";
            this.toolVentana.Size = new System.Drawing.Size(795, 27);
            this.toolVentana.TabIndex = 5;
            this.toolVentana.Text = "ToolStrip1";
            // 
            // toolNombreVentana
            // 
            this.toolNombreVentana.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolNombreVentana.Name = "toolNombreVentana";
            this.toolNombreVentana.Size = new System.Drawing.Size(85, 24);
            this.toolNombreVentana.Text = "Historial";
            // 
            // toolBusqueda
            // 
            this.toolBusqueda.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolExportar});
            this.toolBusqueda.Location = new System.Drawing.Point(0, 27);
            this.toolBusqueda.Name = "toolBusqueda";
            this.toolBusqueda.Size = new System.Drawing.Size(795, 25);
            this.toolBusqueda.TabIndex = 6;
            this.toolBusqueda.Text = "ToolStrip1";
            // 
            // toolExportar
            // 
            this.toolExportar.Image = ((System.Drawing.Image)(resources.GetObject("toolExportar.Image")));
            this.toolExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExportar.Name = "toolExportar";
            this.toolExportar.Size = new System.Drawing.Size(70, 22);
            this.toolExportar.Text = "Exportar";
            this.toolExportar.Click += new System.EventHandler(this.toolExportar_Click);
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToDeleteRows = false;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistorial.Location = new System.Drawing.Point(0, 52);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.Size = new System.Drawing.Size(795, 582);
            this.dgvHistorial.TabIndex = 7;
            // 
            // frmListaHistorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 634);
            this.Controls.Add(this.dgvHistorial);
            this.Controls.Add(this.toolBusqueda);
            this.Controls.Add(this.toolVentana);
            this.Name = "frmListaHistorial";
            this.Text = "Historial del trabajador";
            this.Load += new System.EventHandler(this.frmListaHistorial_Load);
            this.toolVentana.ResumeLayout(false);
            this.toolVentana.PerformLayout();
            this.toolBusqueda.ResumeLayout(false);
            this.toolBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolVentana;
        internal System.Windows.Forms.ToolStripLabel toolNombreVentana;
        internal System.Windows.Forms.ToolStrip toolBusqueda;
        private System.Windows.Forms.ToolStripButton toolExportar;
        private System.Windows.Forms.DataGridView dgvHistorial;
    }
}