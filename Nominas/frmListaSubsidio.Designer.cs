namespace Nominas
{
    partial class frmListaSubsidio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaSubsidio));
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.lblTitulo = new System.Windows.Forms.ToolStripLabel();
            this.toolBusqueda = new System.Windows.Forms.ToolStrip();
            this.toolNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolConsultar = new System.Windows.Forms.ToolStripButton();
            this.toolEditar = new System.Windows.Forms.ToolStripButton();
            this.toolBaja = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbPeriodo = new System.Windows.Forms.ToolStripComboBox();
            this.dgvSubsidio = new System.Windows.Forms.DataGridView();
            this.toolTitulo.SuspendLayout();
            this.toolBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubsidio)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTitulo
            // 
            this.toolTitulo.BackColor = System.Drawing.Color.DarkGray;
            this.toolTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTitulo});
            this.toolTitulo.Location = new System.Drawing.Point(0, 0);
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(735, 27);
            this.toolTitulo.TabIndex = 9;
            this.toolTitulo.Text = "ToolStrip1";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(233, 24);
            this.lblTitulo.Text = "Información de subsidio";
            // 
            // toolBusqueda
            // 
            this.toolBusqueda.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNuevo,
            this.toolConsultar,
            this.toolEditar,
            this.toolBaja,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.cmbPeriodo});
            this.toolBusqueda.Location = new System.Drawing.Point(0, 27);
            this.toolBusqueda.Name = "toolBusqueda";
            this.toolBusqueda.Size = new System.Drawing.Size(735, 25);
            this.toolBusqueda.TabIndex = 10;
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
            // toolConsultar
            // 
            this.toolConsultar.Image = ((System.Drawing.Image)(resources.GetObject("toolConsultar.Image")));
            this.toolConsultar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolConsultar.Name = "toolConsultar";
            this.toolConsultar.Size = new System.Drawing.Size(78, 22);
            this.toolConsultar.Text = "Consultar";
            this.toolConsultar.Click += new System.EventHandler(this.toolConsultar_Click);
            // 
            // toolEditar
            // 
            this.toolEditar.Image = ((System.Drawing.Image)(resources.GetObject("toolEditar.Image")));
            this.toolEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolEditar.Name = "toolEditar";
            this.toolEditar.Size = new System.Drawing.Size(57, 22);
            this.toolEditar.Text = "Editar";
            this.toolEditar.Click += new System.EventHandler(this.toolEditar_Click);
            // 
            // toolBaja
            // 
            this.toolBaja.Image = ((System.Drawing.Image)(resources.GetObject("toolBaja.Image")));
            this.toolBaja.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBaja.Name = "toolBaja";
            this.toolBaja.Size = new System.Drawing.Size(70, 22);
            this.toolBaja.Text = "Eliminar";
            this.toolBaja.Click += new System.EventHandler(this.toolBaja_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(51, 22);
            this.toolStripLabel1.Text = "Periodo:";
            // 
            // cmbPeriodo
            // 
            this.cmbPeriodo.Items.AddRange(new object[] {
            "SEMANAL",
            "QUINCENAL"});
            this.cmbPeriodo.Name = "cmbPeriodo";
            this.cmbPeriodo.Size = new System.Drawing.Size(121, 25);
            this.cmbPeriodo.SelectedIndexChanged += new System.EventHandler(this.cmbPeriodo_SelectedIndexChanged);
            // 
            // dgvSubsidio
            // 
            this.dgvSubsidio.AllowUserToAddRows = false;
            this.dgvSubsidio.AllowUserToDeleteRows = false;
            this.dgvSubsidio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubsidio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSubsidio.Location = new System.Drawing.Point(0, 52);
            this.dgvSubsidio.Name = "dgvSubsidio";
            this.dgvSubsidio.ReadOnly = true;
            this.dgvSubsidio.Size = new System.Drawing.Size(735, 488);
            this.dgvSubsidio.TabIndex = 11;
            // 
            // frmListaSubsidio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 540);
            this.Controls.Add(this.dgvSubsidio);
            this.Controls.Add(this.toolBusqueda);
            this.Controls.Add(this.toolTitulo);
            this.Name = "frmListaSubsidio";
            this.Text = "Lista subsidio";
            this.Load += new System.EventHandler(this.frmListaSubsidio_Load);
            this.toolTitulo.ResumeLayout(false);
            this.toolTitulo.PerformLayout();
            this.toolBusqueda.ResumeLayout(false);
            this.toolBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubsidio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolTitulo;
        internal System.Windows.Forms.ToolStripLabel lblTitulo;
        internal System.Windows.Forms.ToolStrip toolBusqueda;
        private System.Windows.Forms.ToolStripButton toolNuevo;
        private System.Windows.Forms.ToolStripButton toolConsultar;
        private System.Windows.Forms.ToolStripButton toolEditar;
        private System.Windows.Forms.ToolStripButton toolBaja;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmbPeriodo;
        private System.Windows.Forms.DataGridView dgvSubsidio;
    }
}