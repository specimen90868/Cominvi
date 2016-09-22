namespace Nominas
{
    partial class frmReportes
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTipoReporte = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDeptoInicial = new System.Windows.Forms.ComboBox();
            this.cmbDeptoFinal = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEmpleadoInicial = new System.Windows.Forms.ComboBox();
            this.cmbEmpleadoFinal = new System.Windows.Forms.ComboBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpInicioPeriodo = new System.Windows.Forms.DateTimePicker();
            this.dtpFinPeriodo = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbPeriodo = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolPorcentaje = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolEtapa = new System.Windows.Forms.ToolStripStatusLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbNetoCero = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbOrden = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTipoNomina = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reporte:";
            // 
            // cmbTipoReporte
            // 
            this.cmbTipoReporte.FormattingEnabled = true;
            this.cmbTipoReporte.Items.AddRange(new object[] {
            "Empleados",
            "Departamentos",
            "Total General",
            "Tabular",
            "Recibos de Nomina",
            "Recibos Timbrados",
            "Gravados y Exentos"});
            this.cmbTipoReporte.Location = new System.Drawing.Point(16, 144);
            this.cmbTipoReporte.Name = "cmbTipoReporte";
            this.cmbTipoReporte.Size = new System.Drawing.Size(138, 21);
            this.cmbTipoReporte.TabIndex = 1;
            this.cmbTipoReporte.SelectedIndexChanged += new System.EventHandler(this.cmbTipoReporte_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Departamento incial:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(179, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Departamento final:";
            // 
            // cmbDeptoInicial
            // 
            this.cmbDeptoInicial.FormattingEnabled = true;
            this.cmbDeptoInicial.Location = new System.Drawing.Point(16, 196);
            this.cmbDeptoInicial.Name = "cmbDeptoInicial";
            this.cmbDeptoInicial.Size = new System.Drawing.Size(138, 21);
            this.cmbDeptoInicial.TabIndex = 6;
            // 
            // cmbDeptoFinal
            // 
            this.cmbDeptoFinal.FormattingEnabled = true;
            this.cmbDeptoFinal.Location = new System.Drawing.Point(182, 196);
            this.cmbDeptoFinal.Name = "cmbDeptoFinal";
            this.cmbDeptoFinal.Size = new System.Drawing.Size(138, 21);
            this.cmbDeptoFinal.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Empleado inicial:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(179, 237);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Empleado final:";
            // 
            // cmbEmpleadoInicial
            // 
            this.cmbEmpleadoInicial.FormattingEnabled = true;
            this.cmbEmpleadoInicial.Location = new System.Drawing.Point(16, 253);
            this.cmbEmpleadoInicial.Name = "cmbEmpleadoInicial";
            this.cmbEmpleadoInicial.Size = new System.Drawing.Size(138, 21);
            this.cmbEmpleadoInicial.TabIndex = 10;
            // 
            // cmbEmpleadoFinal
            // 
            this.cmbEmpleadoFinal.FormattingEnabled = true;
            this.cmbEmpleadoFinal.Location = new System.Drawing.Point(182, 253);
            this.cmbEmpleadoFinal.Name = "cmbEmpleadoFinal";
            this.cmbEmpleadoFinal.Size = new System.Drawing.Size(138, 21);
            this.cmbEmpleadoFinal.TabIndex = 11;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(166, 358);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 12;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(245, 358);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "De:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Hasta:";
            // 
            // dtpInicioPeriodo
            // 
            this.dtpInicioPeriodo.Location = new System.Drawing.Point(120, 70);
            this.dtpInicioPeriodo.Name = "dtpInicioPeriodo";
            this.dtpInicioPeriodo.Size = new System.Drawing.Size(200, 20);
            this.dtpInicioPeriodo.TabIndex = 16;
            this.dtpInicioPeriodo.ValueChanged += new System.EventHandler(this.dtpInicioPeriodo_ValueChanged);
            // 
            // dtpFinPeriodo
            // 
            this.dtpFinPeriodo.Location = new System.Drawing.Point(120, 96);
            this.dtpFinPeriodo.Name = "dtpFinPeriodo";
            this.dtpFinPeriodo.Size = new System.Drawing.Size(200, 20);
            this.dtpFinPeriodo.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Periodo:";
            // 
            // cmbPeriodo
            // 
            this.cmbPeriodo.FormattingEnabled = true;
            this.cmbPeriodo.Location = new System.Drawing.Point(120, 15);
            this.cmbPeriodo.Name = "cmbPeriodo";
            this.cmbPeriodo.Size = new System.Drawing.Size(200, 21);
            this.cmbPeriodo.TabIndex = 19;
            this.cmbPeriodo.SelectedIndexChanged += new System.EventHandler(this.cmbPeriodo_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolEstado,
            this.toolPorcentaje,
            this.toolEtapa});
            this.statusStrip1.Location = new System.Drawing.Point(0, 392);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(337, 22);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolEstado
            // 
            this.toolEstado.Name = "toolEstado";
            this.toolEstado.Size = new System.Drawing.Size(108, 17);
            this.toolEstado.Text = "Procesando:............";
            // 
            // toolPorcentaje
            // 
            this.toolPorcentaje.Name = "toolPorcentaje";
            this.toolPorcentaje.Size = new System.Drawing.Size(23, 17);
            this.toolPorcentaje.Text = "0%";
            // 
            // toolEtapa
            // 
            this.toolEtapa.Name = "toolEtapa";
            this.toolEtapa.Size = new System.Drawing.Size(36, 17);
            this.toolEtapa.Text = "Etapa";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 292);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Incluye neto cero:";
            // 
            // cmbNetoCero
            // 
            this.cmbNetoCero.FormattingEnabled = true;
            this.cmbNetoCero.Items.AddRange(new object[] {
            "Si",
            "No"});
            this.cmbNetoCero.Location = new System.Drawing.Point(16, 308);
            this.cmbNetoCero.Name = "cmbNetoCero";
            this.cmbNetoCero.Size = new System.Drawing.Size(138, 21);
            this.cmbNetoCero.TabIndex = 22;
            this.cmbNetoCero.SelectedIndexChanged += new System.EventHandler(this.cmbNetoCero_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(179, 292);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Ordenar por:";
            // 
            // cmbOrden
            // 
            this.cmbOrden.FormattingEnabled = true;
            this.cmbOrden.Location = new System.Drawing.Point(182, 308);
            this.cmbOrden.Name = "cmbOrden";
            this.cmbOrden.Size = new System.Drawing.Size(138, 21);
            this.cmbOrden.TabIndex = 24;
            this.cmbOrden.SelectedIndexChanged += new System.EventHandler(this.cmbOrden_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Tipo nomina:";
            // 
            // cmbTipoNomina
            // 
            this.cmbTipoNomina.FormattingEnabled = true;
            this.cmbTipoNomina.Items.AddRange(new object[] {
            "Normal",
            "Extraordinaria normal"});
            this.cmbTipoNomina.Location = new System.Drawing.Point(120, 42);
            this.cmbTipoNomina.Name = "cmbTipoNomina";
            this.cmbTipoNomina.Size = new System.Drawing.Size(200, 21);
            this.cmbTipoNomina.TabIndex = 26;
            this.cmbTipoNomina.SelectedIndexChanged += new System.EventHandler(this.cmbTipoNomina_SelectedIndexChanged);
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 414);
            this.Controls.Add(this.cmbTipoNomina);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbOrden);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbNetoCero);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmbPeriodo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtpFinPeriodo);
            this.Controls.Add(this.dtpInicioPeriodo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.cmbEmpleadoFinal);
            this.Controls.Add(this.cmbEmpleadoInicial);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbDeptoFinal);
            this.Controls.Add(this.cmbDeptoInicial);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbTipoReporte);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReportes";
            this.Text = "Reportes de nómina";
            this.Load += new System.EventHandler(this.frmReportes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReportes_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTipoReporte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDeptoInicial;
        private System.Windows.Forms.ComboBox cmbDeptoFinal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbEmpleadoInicial;
        private System.Windows.Forms.ComboBox cmbEmpleadoFinal;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpInicioPeriodo;
        private System.Windows.Forms.DateTimePicker dtpFinPeriodo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbPeriodo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolEstado;
        private System.Windows.Forms.ToolStripStatusLabel toolPorcentaje;
        private System.Windows.Forms.ToolStripStatusLabel toolEtapa;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbNetoCero;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbOrden;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTipoNomina;
    }
}