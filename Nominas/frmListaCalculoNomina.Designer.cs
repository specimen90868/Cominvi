namespace Nominas
{
    partial class frmListaCalculoNomina
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaCalculoNomina));
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolEmpleados = new System.Windows.Forms.ToolStripLabel();
            this.toolBusqueda = new System.Windows.Forms.ToolStrip();
            this.toolCargar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lblBuscar = new System.Windows.Forms.ToolStripLabel();
            this.txtBuscar = new System.Windows.Forms.ToolStripTextBox();
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noempleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paterno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.materno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sueldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.despensa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asistencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.puntualidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.horasextras = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inicioperiodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finperiodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolPrenomina = new System.Windows.Forms.ToolStripButton();
            this.workNomina = new System.ComponentModel.BackgroundWorker();
            this.toolTitulo.SuspendLayout();
            this.toolBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTitulo
            // 
            this.toolTitulo.BackColor = System.Drawing.Color.DarkGray;
            this.toolTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolEmpleados});
            this.toolTitulo.Location = new System.Drawing.Point(0, 0);
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(1303, 27);
            this.toolTitulo.TabIndex = 9;
            this.toolTitulo.Text = "ToolStrip1";
            // 
            // toolEmpleados
            // 
            this.toolEmpleados.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolEmpleados.Name = "toolEmpleados";
            this.toolEmpleados.Size = new System.Drawing.Size(82, 24);
            this.toolEmpleados.Text = "Nómina";
            // 
            // toolBusqueda
            // 
            this.toolBusqueda.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolCargar,
            this.toolPrenomina,
            this.toolStripSeparator3,
            this.lblBuscar,
            this.txtBuscar});
            this.toolBusqueda.Location = new System.Drawing.Point(0, 27);
            this.toolBusqueda.Name = "toolBusqueda";
            this.toolBusqueda.Size = new System.Drawing.Size(1303, 25);
            this.toolBusqueda.TabIndex = 10;
            this.toolBusqueda.Text = "ToolStrip1";
            // 
            // toolCargar
            // 
            this.toolCargar.Image = ((System.Drawing.Image)(resources.GetObject("toolCargar.Image")));
            this.toolCargar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCargar.Name = "toolCargar";
            this.toolCargar.Size = new System.Drawing.Size(62, 22);
            this.toolCargar.Text = "Cargar";
            this.toolCargar.Click += new System.EventHandler(this.toolCargar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // 
            // dgvEmpleados
            // 
            this.dgvEmpleados.AllowUserToAddRows = false;
            this.dgvEmpleados.AllowUserToDeleteRows = false;
            this.dgvEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmpleados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.noempleado,
            this.nombre,
            this.paterno,
            this.materno,
            this.sueldo,
            this.despensa,
            this.asistencia,
            this.puntualidad,
            this.horasextras,
            this.inicioperiodo,
            this.finperiodo});
            this.dgvEmpleados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmpleados.Location = new System.Drawing.Point(0, 52);
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.Size = new System.Drawing.Size(1303, 573);
            this.dgvEmpleados.TabIndex = 11;
            // 
            // check
            // 
            this.check.HeaderText = "Check";
            this.check.Name = "check";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "No. Empleado";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Nombre";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Paterno";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Materno";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Sueldo";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Despensa";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "P. Asist.";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "P. Puntualidad";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "H. Extras Dobles";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Inicio";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Fin";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // noempleado
            // 
            this.noempleado.HeaderText = "No. Empleado";
            this.noempleado.Name = "noempleado";
            this.noempleado.ReadOnly = true;
            this.noempleado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.noempleado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // nombre
            // 
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // paterno
            // 
            this.paterno.HeaderText = "Paterno";
            this.paterno.Name = "paterno";
            // 
            // materno
            // 
            this.materno.HeaderText = "Materno";
            this.materno.Name = "materno";
            // 
            // sueldo
            // 
            this.sueldo.HeaderText = "Sueldo";
            this.sueldo.Name = "sueldo";
            // 
            // despensa
            // 
            this.despensa.HeaderText = "Despensa";
            this.despensa.Name = "despensa";
            // 
            // asistencia
            // 
            this.asistencia.HeaderText = "P. Asist.";
            this.asistencia.Name = "asistencia";
            // 
            // puntualidad
            // 
            this.puntualidad.HeaderText = "P. Puntualidad";
            this.puntualidad.Name = "puntualidad";
            // 
            // horasextras
            // 
            this.horasextras.HeaderText = "H. Extras Dobles";
            this.horasextras.Name = "horasextras";
            // 
            // inicioperiodo
            // 
            this.inicioperiodo.HeaderText = "Inicio";
            this.inicioperiodo.Name = "inicioperiodo";
            this.inicioperiodo.ReadOnly = true;
            // 
            // finperiodo
            // 
            this.finperiodo.HeaderText = "Fin";
            this.finperiodo.Name = "finperiodo";
            this.finperiodo.ReadOnly = true;
            // 
            // toolPrenomina
            // 
            this.toolPrenomina.Image = ((System.Drawing.Image)(resources.GetObject("toolPrenomina.Image")));
            this.toolPrenomina.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPrenomina.Name = "toolPrenomina";
            this.toolPrenomina.Size = new System.Drawing.Size(85, 22);
            this.toolPrenomina.Text = "Prenomina";
            this.toolPrenomina.Click += new System.EventHandler(this.toolPrenomina_Click);
            // 
            // workNomina
            // 
            this.workNomina.WorkerReportsProgress = true;
            // 
            // frmListaCalculoNomina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 625);
            this.Controls.Add(this.dgvEmpleados);
            this.Controls.Add(this.toolBusqueda);
            this.Controls.Add(this.toolTitulo);
            this.Name = "frmListaCalculoNomina";
            this.Text = "Nomina";
            this.toolTitulo.ResumeLayout(false);
            this.toolTitulo.PerformLayout();
            this.toolBusqueda.ResumeLayout(false);
            this.toolBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolTitulo;
        internal System.Windows.Forms.ToolStripLabel toolEmpleados;
        internal System.Windows.Forms.ToolStrip toolBusqueda;
        private System.Windows.Forms.ToolStripButton toolCargar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.ToolStripLabel lblBuscar;
        internal System.Windows.Forms.ToolStripTextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvEmpleados;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn noempleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn paterno;
        private System.Windows.Forms.DataGridViewTextBoxColumn materno;
        private System.Windows.Forms.DataGridViewTextBoxColumn sueldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn despensa;
        private System.Windows.Forms.DataGridViewTextBoxColumn asistencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn puntualidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn horasextras;
        private System.Windows.Forms.DataGridViewTextBoxColumn inicioperiodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn finperiodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.ToolStripButton toolPrenomina;
        private System.ComponentModel.BackgroundWorker workNomina;
    }
}