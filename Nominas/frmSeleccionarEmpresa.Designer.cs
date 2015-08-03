namespace Nominas
{
    partial class frmSeleccionarEmpresa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSeleccionarEmpresa));
            this.toolEmpresa = new System.Windows.Forms.ToolStrip();
            this.toolAbrir = new System.Windows.Forms.ToolStripButton();
            this.toolNuevo = new System.Windows.Forms.ToolStripButton();
            this.dgvEmpresas = new System.Windows.Forms.DataGridView();
            this.toolEmpresa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpresas)).BeginInit();
            this.SuspendLayout();
            // 
            // toolEmpresa
            // 
            this.toolEmpresa.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAbrir,
            this.toolNuevo});
            this.toolEmpresa.Location = new System.Drawing.Point(0, 0);
            this.toolEmpresa.Name = "toolEmpresa";
            this.toolEmpresa.Size = new System.Drawing.Size(488, 25);
            this.toolEmpresa.TabIndex = 0;
            this.toolEmpresa.Text = "toolStrip1";
            // 
            // toolAbrir
            // 
            this.toolAbrir.Image = ((System.Drawing.Image)(resources.GetObject("toolAbrir.Image")));
            this.toolAbrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAbrir.Name = "toolAbrir";
            this.toolAbrir.Size = new System.Drawing.Size(53, 22);
            this.toolAbrir.Text = "Abrir";
            this.toolAbrir.Click += new System.EventHandler(this.toolAbrir_Click);
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
            // dgvEmpresas
            // 
            this.dgvEmpresas.AllowUserToAddRows = false;
            this.dgvEmpresas.AllowUserToDeleteRows = false;
            this.dgvEmpresas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmpresas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmpresas.Location = new System.Drawing.Point(0, 25);
            this.dgvEmpresas.Name = "dgvEmpresas";
            this.dgvEmpresas.ReadOnly = true;
            this.dgvEmpresas.Size = new System.Drawing.Size(488, 299);
            this.dgvEmpresas.TabIndex = 1;
            this.dgvEmpresas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmpresas_CellDoubleClick);
            // 
            // frmSeleccionarEmpresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 324);
            this.Controls.Add(this.dgvEmpresas);
            this.Controls.Add(this.toolEmpresa);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSeleccionarEmpresa";
            this.Text = "Seleccionar empresa";
            this.Load += new System.EventHandler(this.frmSeleccionarEmpresa_Load);
            this.toolEmpresa.ResumeLayout(false);
            this.toolEmpresa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpresas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolEmpresa;
        private System.Windows.Forms.DataGridView dgvEmpresas;
        private System.Windows.Forms.ToolStripButton toolAbrir;
        private System.Windows.Forms.ToolStripButton toolNuevo;
    }
}