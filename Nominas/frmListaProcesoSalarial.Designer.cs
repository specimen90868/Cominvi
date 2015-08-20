namespace Nominas
{
    partial class frmListaProcesoSalarial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaProcesoSalarial));
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolTituloVentana = new System.Windows.Forms.ToolStripLabel();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolAplicar = new System.Windows.Forms.ToolStripButton();
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            this.seleccion = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idtrabajador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noempleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sdivigente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sdinuevo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.antiguedad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.antiguedadmod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaimss = new Nominas.CalendarioColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calendarioColumn1 = new Nominas.CalendarioColumn();
            this.toolTitulo.SuspendLayout();
            this.toolAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTitulo
            // 
            this.toolTitulo.BackColor = System.Drawing.Color.DarkGray;
            this.toolTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTituloVentana});
            this.toolTitulo.Location = new System.Drawing.Point(0, 0);
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(1156, 27);
            this.toolTitulo.TabIndex = 5;
            this.toolTitulo.Text = "ToolStrip1";
            // 
            // toolTituloVentana
            // 
            this.toolTituloVentana.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolTituloVentana.Name = "toolTituloVentana";
            this.toolTituloVentana.Size = new System.Drawing.Size(372, 24);
            this.toolTituloVentana.Text = "Incremento salarial por año de servicio";
            // 
            // toolAcciones
            // 
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAplicar});
            this.toolAcciones.Location = new System.Drawing.Point(0, 27);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(1156, 25);
            this.toolAcciones.TabIndex = 6;
            this.toolAcciones.Text = "ToolStrip1";
            // 
            // toolAplicar
            // 
            this.toolAplicar.Image = ((System.Drawing.Image)(resources.GetObject("toolAplicar.Image")));
            this.toolAplicar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAplicar.Name = "toolAplicar";
            this.toolAplicar.Size = new System.Drawing.Size(64, 22);
            this.toolAplicar.Text = "Aplicar";
            this.toolAplicar.Click += new System.EventHandler(this.toolAplicar_Click);
            // 
            // dgvEmpleados
            // 
            this.dgvEmpleados.AllowUserToAddRows = false;
            this.dgvEmpleados.AllowUserToDeleteRows = false;
            this.dgvEmpleados.AllowUserToResizeRows = false;
            this.dgvEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmpleados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.seleccion,
            this.idtrabajador,
            this.noempleado,
            this.nombre,
            this.sdivigente,
            this.sdinuevo,
            this.antiguedad,
            this.antiguedadmod,
            this.fechaimss});
            this.dgvEmpleados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmpleados.Location = new System.Drawing.Point(0, 52);
            this.dgvEmpleados.MultiSelect = false;
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.Size = new System.Drawing.Size(1156, 640);
            this.dgvEmpleados.TabIndex = 7;
            this.dgvEmpleados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmpleados_CellContentClick);
            // 
            // seleccion
            // 
            this.seleccion.FalseValue = "0";
            this.seleccion.HeaderText = "Selección";
            this.seleccion.Name = "seleccion";
            this.seleccion.TrueValue = "1";
            // 
            // idtrabajador
            // 
            this.idtrabajador.HeaderText = "Id";
            this.idtrabajador.Name = "idtrabajador";
            // 
            // noempleado
            // 
            this.noempleado.HeaderText = "No. Empleado";
            this.noempleado.Name = "noempleado";
            // 
            // nombre
            // 
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            // 
            // sdivigente
            // 
            this.sdivigente.HeaderText = "SDI Vigente";
            this.sdivigente.Name = "sdivigente";
            // 
            // sdinuevo
            // 
            this.sdinuevo.HeaderText = "SDI Nuevo";
            this.sdinuevo.Name = "sdinuevo";
            // 
            // antiguedad
            // 
            this.antiguedad.HeaderText = "Antiguedad";
            this.antiguedad.Name = "antiguedad";
            // 
            // antiguedadmod
            // 
            this.antiguedadmod.HeaderText = "AntiguedadMod";
            this.antiguedadmod.Name = "antiguedadmod";
            // 
            // fechaimss
            // 
            this.fechaimss.HeaderText = "Fecha IMSS";
            this.fechaimss.Name = "fechaimss";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "No. Empleado";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Nombre";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "SDI Vigente";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "SDI Nuevo";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Antiguedad";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "AntiguedadMod";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // calendarioColumn1
            // 
            this.calendarioColumn1.HeaderText = "Fecha IMSS";
            this.calendarioColumn1.Name = "calendarioColumn1";
            // 
            // frmListaProcesoSalarial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 692);
            this.Controls.Add(this.dgvEmpleados);
            this.Controls.Add(this.toolAcciones);
            this.Controls.Add(this.toolTitulo);
            this.Name = "frmListaProcesoSalarial";
            this.Text = "Proceso salarial";
            this.Load += new System.EventHandler(this.frmListaProcesoSalarial_Load);
            this.toolTitulo.ResumeLayout(false);
            this.toolTitulo.PerformLayout();
            this.toolAcciones.ResumeLayout(false);
            this.toolAcciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolTitulo;
        internal System.Windows.Forms.ToolStripLabel toolTituloVentana;
        internal System.Windows.Forms.ToolStrip toolAcciones;
        private System.Windows.Forms.ToolStripButton toolAplicar;
        private System.Windows.Forms.DataGridView dgvEmpleados;
        private System.Windows.Forms.DataGridViewCheckBoxColumn seleccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn idtrabajador;
        private System.Windows.Forms.DataGridViewTextBoxColumn noempleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn sdivigente;
        private System.Windows.Forms.DataGridViewTextBoxColumn sdinuevo;
        private System.Windows.Forms.DataGridViewTextBoxColumn antiguedad;
        private System.Windows.Forms.DataGridViewTextBoxColumn antiguedadmod;
        private CalendarioColumn fechaimss;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private CalendarioColumn calendarioColumn1;
    }
}