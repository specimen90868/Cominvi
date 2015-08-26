﻿namespace Nominas
{
    partial class frmListaBajasSua
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaBajasSua));
            this.toolVentana = new System.Windows.Forms.ToolStrip();
            this.toolNombreVentana = new System.Windows.Forms.ToolStripLabel();
            this.toolBusqueda = new System.Windows.Forms.ToolStrip();
            this.toolFiltrar = new System.Windows.Forms.ToolStripButton();
            this.toolExportar = new System.Windows.Forms.ToolStripButton();
            this.dgvBajasSua = new System.Windows.Forms.DataGridView();
            this.workBajas = new System.ComponentModel.BackgroundWorker();
            this.toolVentana.SuspendLayout();
            this.toolBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBajasSua)).BeginInit();
            this.SuspendLayout();
            // 
            // toolVentana
            // 
            this.toolVentana.BackColor = System.Drawing.Color.DarkGray;
            this.toolVentana.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNombreVentana});
            this.toolVentana.Location = new System.Drawing.Point(0, 0);
            this.toolVentana.Name = "toolVentana";
            this.toolVentana.Size = new System.Drawing.Size(680, 27);
            this.toolVentana.TabIndex = 7;
            this.toolVentana.Text = "ToolStrip1";
            // 
            // toolNombreVentana
            // 
            this.toolNombreVentana.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolNombreVentana.Name = "toolNombreVentana";
            this.toolNombreVentana.Size = new System.Drawing.Size(99, 24);
            this.toolNombreVentana.Text = "Bajas sua";
            // 
            // toolBusqueda
            // 
            this.toolBusqueda.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolFiltrar,
            this.toolExportar});
            this.toolBusqueda.Location = new System.Drawing.Point(0, 27);
            this.toolBusqueda.Name = "toolBusqueda";
            this.toolBusqueda.Size = new System.Drawing.Size(680, 25);
            this.toolBusqueda.TabIndex = 8;
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
            // dgvBajasSua
            // 
            this.dgvBajasSua.AllowUserToAddRows = false;
            this.dgvBajasSua.AllowUserToDeleteRows = false;
            this.dgvBajasSua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBajasSua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBajasSua.Location = new System.Drawing.Point(0, 52);
            this.dgvBajasSua.Name = "dgvBajasSua";
            this.dgvBajasSua.ReadOnly = true;
            this.dgvBajasSua.Size = new System.Drawing.Size(680, 524);
            this.dgvBajasSua.TabIndex = 9;
            // 
            // workBajas
            // 
            this.workBajas.WorkerReportsProgress = true;
            this.workBajas.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workBajas_DoWork);
            this.workBajas.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workBajas_RunWorkerCompleted);
            // 
            // frmListaBajasSua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 576);
            this.Controls.Add(this.dgvBajasSua);
            this.Controls.Add(this.toolBusqueda);
            this.Controls.Add(this.toolVentana);
            this.Name = "frmListaBajasSua";
            this.Text = "Bajas Sua";
            this.Load += new System.EventHandler(this.frmListaBajasSua_Load);
            this.toolVentana.ResumeLayout(false);
            this.toolVentana.PerformLayout();
            this.toolBusqueda.ResumeLayout(false);
            this.toolBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBajasSua)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolVentana;
        internal System.Windows.Forms.ToolStripLabel toolNombreVentana;
        internal System.Windows.Forms.ToolStrip toolBusqueda;
        private System.Windows.Forms.ToolStripButton toolFiltrar;
        private System.Windows.Forms.ToolStripButton toolExportar;
        private System.Windows.Forms.DataGridView dgvBajasSua;
        private System.ComponentModel.BackgroundWorker workBajas;
    }
}