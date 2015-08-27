namespace Nominas
{
    partial class frmListaReingresosSua
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaReingresosSua));
            this.toolVentana = new System.Windows.Forms.ToolStrip();
            this.toolNombreVentana = new System.Windows.Forms.ToolStripLabel();
            this.toolBusqueda = new System.Windows.Forms.ToolStrip();
            this.toolFiltrar = new System.Windows.Forms.ToolStripButton();
            this.toolExportar = new System.Windows.Forms.ToolStripButton();
            this.dgvReingresoSua = new System.Windows.Forms.DataGridView();
            this.workReingreso = new System.ComponentModel.BackgroundWorker();
            this.toolVentana.SuspendLayout();
            this.toolBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReingresoSua)).BeginInit();
            this.SuspendLayout();
            // 
            // toolVentana
            // 
            this.toolVentana.BackColor = System.Drawing.Color.DarkGray;
            this.toolVentana.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNombreVentana});
            this.toolVentana.Location = new System.Drawing.Point(0, 0);
            this.toolVentana.Name = "toolVentana";
            this.toolVentana.Size = new System.Drawing.Size(724, 27);
            this.toolVentana.TabIndex = 10;
            this.toolVentana.Text = "ToolStrip1";
            // 
            // toolNombreVentana
            // 
            this.toolNombreVentana.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolNombreVentana.Name = "toolNombreVentana";
            this.toolNombreVentana.Size = new System.Drawing.Size(155, 24);
            this.toolNombreVentana.Text = "Reingresos sua";
            // 
            // toolBusqueda
            // 
            this.toolBusqueda.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolFiltrar,
            this.toolExportar});
            this.toolBusqueda.Location = new System.Drawing.Point(0, 27);
            this.toolBusqueda.Name = "toolBusqueda";
            this.toolBusqueda.Size = new System.Drawing.Size(724, 25);
            this.toolBusqueda.TabIndex = 11;
            this.toolBusqueda.Text = "ToolStrip1";
            // 
            // toolFiltrar
            // 
            this.toolFiltrar.Image = ((System.Drawing.Image)(resources.GetObject("toolFiltrar.Image")));
            this.toolFiltrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFiltrar.Name = "toolFiltrar";
            this.toolFiltrar.Size = new System.Drawing.Size(57, 22);
            this.toolFiltrar.Text = "Filtrar";
            this.toolFiltrar.Click += new System.EventHandler(this.toolFiltrar_Click);
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
            // dgvReingresoSua
            // 
            this.dgvReingresoSua.AllowUserToAddRows = false;
            this.dgvReingresoSua.AllowUserToDeleteRows = false;
            this.dgvReingresoSua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReingresoSua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReingresoSua.Location = new System.Drawing.Point(0, 52);
            this.dgvReingresoSua.Name = "dgvReingresoSua";
            this.dgvReingresoSua.ReadOnly = true;
            this.dgvReingresoSua.Size = new System.Drawing.Size(724, 620);
            this.dgvReingresoSua.TabIndex = 12;
            // 
            // workReingreso
            // 
            this.workReingreso.WorkerReportsProgress = true;
            this.workReingreso.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workReingreso_DoWork);
            this.workReingreso.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workReingreso_RunWorkerCompleted);
            // 
            // frmListaReingresosSua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 672);
            this.Controls.Add(this.dgvReingresoSua);
            this.Controls.Add(this.toolBusqueda);
            this.Controls.Add(this.toolVentana);
            this.Name = "frmListaReingresosSua";
            this.Text = "Reingresos sua";
            this.Load += new System.EventHandler(this.frmListaReingresosSua_Load);
            this.toolVentana.ResumeLayout(false);
            this.toolVentana.PerformLayout();
            this.toolBusqueda.ResumeLayout(false);
            this.toolBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReingresoSua)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolVentana;
        internal System.Windows.Forms.ToolStripLabel toolNombreVentana;
        internal System.Windows.Forms.ToolStrip toolBusqueda;
        private System.Windows.Forms.ToolStripButton toolFiltrar;
        private System.Windows.Forms.ToolStripButton toolExportar;
        private System.Windows.Forms.DataGridView dgvReingresoSua;
        private System.ComponentModel.BackgroundWorker workReingreso;
    }
}