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
            this.label6 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpInicioPeriodo = new System.Windows.Forms.DateTimePicker();
            this.dtpFinPeriodo = new System.Windows.Forms.DateTimePicker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolPorcentaje = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolEtapa = new System.Windows.Forms.ToolStripStatusLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbNetoCero = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbOrden = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lstvDepartamentos = new System.Windows.Forms.ListView();
            this.lstvEmpleados = new System.Windows.Forms.ListView();
            this.btnObtener = new System.Windows.Forms.Button();
            this.chkTodosDeptos = new System.Windows.Forms.CheckBox();
            this.chkTodosEmpleados = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 69);
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
            "Gravados y Exentos"});
            this.cmbTipoReporte.Location = new System.Drawing.Point(11, 86);
            this.cmbTipoReporte.Name = "cmbTipoReporte";
            this.cmbTipoReporte.Size = new System.Drawing.Size(138, 21);
            this.cmbTipoReporte.TabIndex = 1;
            this.cmbTipoReporte.SelectedIndexChanged += new System.EventHandler(this.cmbTipoReporte_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 279);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Empleados:";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(351, 474);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 12;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(351, 503);
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
            this.label7.Location = new System.Drawing.Point(10, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "De:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Hasta:";
            // 
            // dtpInicioPeriodo
            // 
            this.dtpInicioPeriodo.Location = new System.Drawing.Point(115, 12);
            this.dtpInicioPeriodo.Name = "dtpInicioPeriodo";
            this.dtpInicioPeriodo.Size = new System.Drawing.Size(200, 20);
            this.dtpInicioPeriodo.TabIndex = 16;
            this.dtpInicioPeriodo.ValueChanged += new System.EventHandler(this.dtpInicioPeriodo_ValueChanged);
            // 
            // dtpFinPeriodo
            // 
            this.dtpFinPeriodo.Location = new System.Drawing.Point(115, 38);
            this.dtpFinPeriodo.Name = "dtpFinPeriodo";
            this.dtpFinPeriodo.Size = new System.Drawing.Size(200, 20);
            this.dtpFinPeriodo.TabIndex = 17;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolEstado,
            this.toolPorcentaje,
            this.toolEtapa});
            this.statusStrip1.Location = new System.Drawing.Point(0, 545);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(438, 22);
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
            this.label10.Location = new System.Drawing.Point(8, 436);
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
            this.cmbNetoCero.Location = new System.Drawing.Point(11, 452);
            this.cmbNetoCero.Name = "cmbNetoCero";
            this.cmbNetoCero.Size = new System.Drawing.Size(138, 21);
            this.cmbNetoCero.TabIndex = 22;
            this.cmbNetoCero.SelectedIndexChanged += new System.EventHandler(this.cmbNetoCero_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 489);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Ordenar por:";
            // 
            // cmbOrden
            // 
            this.cmbOrden.FormattingEnabled = true;
            this.cmbOrden.Location = new System.Drawing.Point(11, 505);
            this.cmbOrden.Name = "cmbOrden";
            this.cmbOrden.Size = new System.Drawing.Size(138, 21);
            this.cmbOrden.TabIndex = 24;
            this.cmbOrden.SelectedIndexChanged += new System.EventHandler(this.cmbOrden_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Departamentos:";
            // 
            // lstvDepartamentos
            // 
            this.lstvDepartamentos.Location = new System.Drawing.Point(11, 137);
            this.lstvDepartamentos.Name = "lstvDepartamentos";
            this.lstvDepartamentos.Size = new System.Drawing.Size(415, 130);
            this.lstvDepartamentos.TabIndex = 25;
            this.lstvDepartamentos.UseCompatibleStateImageBehavior = false;
            this.lstvDepartamentos.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstvDepartamentos_ItemChecked);
            // 
            // lstvEmpleados
            // 
            this.lstvEmpleados.Location = new System.Drawing.Point(11, 295);
            this.lstvEmpleados.Name = "lstvEmpleados";
            this.lstvEmpleados.Size = new System.Drawing.Size(415, 130);
            this.lstvEmpleados.TabIndex = 26;
            this.lstvEmpleados.UseCompatibleStateImageBehavior = false;
            // 
            // btnObtener
            // 
            this.btnObtener.Location = new System.Drawing.Point(321, 12);
            this.btnObtener.Name = "btnObtener";
            this.btnObtener.Size = new System.Drawing.Size(105, 46);
            this.btnObtener.TabIndex = 27;
            this.btnObtener.Text = "Mostrar";
            this.btnObtener.UseVisualStyleBackColor = true;
            this.btnObtener.Click += new System.EventHandler(this.btnObtener_Click);
            // 
            // chkTodosDeptos
            // 
            this.chkTodosDeptos.AutoSize = true;
            this.chkTodosDeptos.Location = new System.Drawing.Point(315, 114);
            this.chkTodosDeptos.Name = "chkTodosDeptos";
            this.chkTodosDeptos.Size = new System.Drawing.Size(111, 17);
            this.chkTodosDeptos.TabIndex = 28;
            this.chkTodosDeptos.Text = "Seleccionar todos";
            this.chkTodosDeptos.UseVisualStyleBackColor = true;
            this.chkTodosDeptos.CheckedChanged += new System.EventHandler(this.chkTodosDeptos_CheckedChanged);
            // 
            // chkTodosEmpleados
            // 
            this.chkTodosEmpleados.AutoSize = true;
            this.chkTodosEmpleados.Location = new System.Drawing.Point(315, 273);
            this.chkTodosEmpleados.Name = "chkTodosEmpleados";
            this.chkTodosEmpleados.Size = new System.Drawing.Size(111, 17);
            this.chkTodosEmpleados.TabIndex = 29;
            this.chkTodosEmpleados.Text = "Seleccionar todos";
            this.chkTodosEmpleados.UseVisualStyleBackColor = true;
            this.chkTodosEmpleados.CheckedChanged += new System.EventHandler(this.chkTodosEmpleados_CheckedChanged);
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 567);
            this.Controls.Add(this.chkTodosEmpleados);
            this.Controls.Add(this.chkTodosDeptos);
            this.Controls.Add(this.btnObtener);
            this.Controls.Add(this.lstvEmpleados);
            this.Controls.Add(this.lstvDepartamentos);
            this.Controls.Add(this.cmbOrden);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbNetoCero);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dtpFinPeriodo);
            this.Controls.Add(this.dtpInicioPeriodo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbTipoReporte);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReportes";
            this.Text = "Reportes de nómina";
            this.Load += new System.EventHandler(this.frmReportes_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTipoReporte;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpInicioPeriodo;
        private System.Windows.Forms.DateTimePicker dtpFinPeriodo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolEstado;
        private System.Windows.Forms.ToolStripStatusLabel toolPorcentaje;
        private System.Windows.Forms.ToolStripStatusLabel toolEtapa;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbNetoCero;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbOrden;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lstvDepartamentos;
        private System.Windows.Forms.ListView lstvEmpleados;
        private System.Windows.Forms.Button btnObtener;
        private System.Windows.Forms.CheckBox chkTodosDeptos;
        private System.Windows.Forms.CheckBox chkTodosEmpleados;
    }
}