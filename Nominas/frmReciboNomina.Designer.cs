namespace Nominas
{
    partial class frmReciboNomina
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReciboNomina));
            this.dgvPercepciones = new System.Windows.Forms.DataGridView();
            this.dgvDeducciones = new System.Windows.Forms.DataGridView();
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolEmpleados = new System.Windows.Forms.ToolStripLabel();
            this.toolBusqueda = new System.Windows.Forms.ToolStrip();
            this.toolBuscar = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNoEmpleado = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPeriodoNomina = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblSumaPercepciones = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblSumaDeducciones = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblNeto = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPercepciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeducciones)).BeginInit();
            this.toolTitulo.SuspendLayout();
            this.toolBusqueda.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPercepciones
            // 
            this.dgvPercepciones.BackgroundColor = System.Drawing.Color.White;
            this.dgvPercepciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPercepciones.Location = new System.Drawing.Point(0, 181);
            this.dgvPercepciones.Name = "dgvPercepciones";
            this.dgvPercepciones.Size = new System.Drawing.Size(283, 218);
            this.dgvPercepciones.TabIndex = 0;
            // 
            // dgvDeducciones
            // 
            this.dgvDeducciones.BackgroundColor = System.Drawing.Color.White;
            this.dgvDeducciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeducciones.Location = new System.Drawing.Point(284, 181);
            this.dgvDeducciones.Name = "dgvDeducciones";
            this.dgvDeducciones.Size = new System.Drawing.Size(283, 218);
            this.dgvDeducciones.TabIndex = 1;
            // 
            // toolTitulo
            // 
            this.toolTitulo.BackColor = System.Drawing.Color.DarkGray;
            this.toolTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolEmpleados});
            this.toolTitulo.Location = new System.Drawing.Point(0, 0);
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(567, 27);
            this.toolTitulo.TabIndex = 10;
            this.toolTitulo.Text = "ToolStrip1";
            // 
            // toolEmpleados
            // 
            this.toolEmpleados.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolEmpleados.Name = "toolEmpleados";
            this.toolEmpleados.Size = new System.Drawing.Size(181, 24);
            this.toolEmpleados.Text = "Recibo de nómina";
            // 
            // toolBusqueda
            // 
            this.toolBusqueda.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBuscar});
            this.toolBusqueda.Location = new System.Drawing.Point(0, 27);
            this.toolBusqueda.Name = "toolBusqueda";
            this.toolBusqueda.Size = new System.Drawing.Size(567, 25);
            this.toolBusqueda.TabIndex = 11;
            this.toolBusqueda.Text = "ToolStrip1";
            // 
            // toolBuscar
            // 
            this.toolBuscar.Image = ((System.Drawing.Image)(resources.GetObject("toolBuscar.Image")));
            this.toolBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBuscar.Name = "toolBuscar";
            this.toolBuscar.Size = new System.Drawing.Size(62, 22);
            this.toolBuscar.Text = "Buscar";
            this.toolBuscar.Click += new System.EventHandler(this.toolBuscar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Empleado:";
            // 
            // lblNoEmpleado
            // 
            this.lblNoEmpleado.AutoSize = true;
            this.lblNoEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoEmpleado.Location = new System.Drawing.Point(109, 94);
            this.lblNoEmpleado.Name = "lblNoEmpleado";
            this.lblNoEmpleado.Size = new System.Drawing.Size(132, 17);
            this.lblNoEmpleado.TabIndex = 13;
            this.lblNoEmpleado.Text = "No. de Empleado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Nombre:";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(109, 122);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(166, 17);
            this.lblNombre.TabIndex = 15;
            this.lblNombre.Text = "Nombre del empleado";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(19, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 17);
            this.label9.TabIndex = 20;
            this.label9.Text = "Periodo:";
            // 
            // lblPeriodoNomina
            // 
            this.lblPeriodoNomina.AutoSize = true;
            this.lblPeriodoNomina.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeriodoNomina.Location = new System.Drawing.Point(94, 68);
            this.lblPeriodoNomina.Name = "lblPeriodoNomina";
            this.lblPeriodoNomina.Size = new System.Drawing.Size(162, 17);
            this.lblPeriodoNomina.TabIndex = 21;
            this.lblPeriodoNomina.Text = "Periodo de la nómina";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(94, 161);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 17);
            this.label11.TabIndex = 22;
            this.label11.Text = "Percepciones";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(385, 161);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 17);
            this.label12.TabIndex = 23;
            this.label12.Text = "Deducciones";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(-3, 403);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(173, 17);
            this.label13.TabIndex = 24;
            this.label13.Text = "Suma de percepciones";
            // 
            // lblSumaPercepciones
            // 
            this.lblSumaPercepciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSumaPercepciones.Location = new System.Drawing.Point(183, 403);
            this.lblSumaPercepciones.Name = "lblSumaPercepciones";
            this.lblSumaPercepciones.Size = new System.Drawing.Size(100, 23);
            this.lblSumaPercepciones.TabIndex = 25;
            this.lblSumaPercepciones.Text = "Total";
            this.lblSumaPercepciones.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(281, 403);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(167, 17);
            this.label15.TabIndex = 26;
            this.label15.Text = "Suma de deducciones";
            // 
            // lblSumaDeducciones
            // 
            this.lblSumaDeducciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSumaDeducciones.Location = new System.Drawing.Point(467, 403);
            this.lblSumaDeducciones.Name = "lblSumaDeducciones";
            this.lblSumaDeducciones.Size = new System.Drawing.Size(100, 23);
            this.lblSumaDeducciones.TabIndex = 27;
            this.lblSumaDeducciones.Text = "Total";
            this.lblSumaDeducciones.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(281, 426);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(108, 17);
            this.label17.TabIndex = 28;
            this.label17.Text = "Neto a pagar:";
            // 
            // lblNeto
            // 
            this.lblNeto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNeto.ForeColor = System.Drawing.Color.Blue;
            this.lblNeto.Location = new System.Drawing.Point(467, 426);
            this.lblNeto.Name = "lblNeto";
            this.lblNeto.Size = new System.Drawing.Size(100, 23);
            this.lblNeto.TabIndex = 29;
            this.lblNeto.Text = "Total";
            this.lblNeto.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmReciboNomina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 452);
            this.Controls.Add(this.lblNeto);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lblSumaDeducciones);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblSumaPercepciones);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblPeriodoNomina);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblNoEmpleado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolBusqueda);
            this.Controls.Add(this.toolTitulo);
            this.Controls.Add(this.dgvDeducciones);
            this.Controls.Add(this.dgvPercepciones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReciboNomina";
            this.Text = "Recibo de nomina";
            this.Load += new System.EventHandler(this.frmReciboNomina_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPercepciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeducciones)).EndInit();
            this.toolTitulo.ResumeLayout(false);
            this.toolTitulo.PerformLayout();
            this.toolBusqueda.ResumeLayout(false);
            this.toolBusqueda.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPercepciones;
        private System.Windows.Forms.DataGridView dgvDeducciones;
        internal System.Windows.Forms.ToolStrip toolTitulo;
        internal System.Windows.Forms.ToolStripLabel toolEmpleados;
        internal System.Windows.Forms.ToolStrip toolBusqueda;
        private System.Windows.Forms.ToolStripButton toolBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNoEmpleado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPeriodoNomina;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblSumaPercepciones;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblSumaDeducciones;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblNeto;
    }
}