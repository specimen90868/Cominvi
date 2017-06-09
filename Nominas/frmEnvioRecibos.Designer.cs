namespace Nominas
{
    partial class frmEnvioRecibos
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
            this.lstvDepartamentos = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstvPeriodos = new System.Windows.Forms.ListView();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCorreoElectronico = new System.Windows.Forms.TextBox();
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            this.Visor = new Microsoft.Reporting.WinForms.ReportViewer();
            this.BarraEstado = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEtapa = new System.Windows.Forms.ToolStripStatusLabel();
            this.workerEnvio = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.BarraEstado.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstvDepartamentos
            // 
            this.lstvDepartamentos.Location = new System.Drawing.Point(247, 51);
            this.lstvDepartamentos.Name = "lstvDepartamentos";
            this.lstvDepartamentos.Size = new System.Drawing.Size(231, 162);
            this.lstvDepartamentos.TabIndex = 8;
            this.lstvDepartamentos.UseCompatibleStateImageBehavior = false;
            this.lstvDepartamentos.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstvDepartamentos_ItemChecked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Departamentos:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Periodos:";
            // 
            // lstvPeriodos
            // 
            this.lstvPeriodos.CheckBoxes = true;
            this.lstvPeriodos.Location = new System.Drawing.Point(10, 51);
            this.lstvPeriodos.MultiSelect = false;
            this.lstvPeriodos.Name = "lstvPeriodos";
            this.lstvPeriodos.Size = new System.Drawing.Size(231, 162);
            this.lstvPeriodos.TabIndex = 5;
            this.lstvPeriodos.UseCompatibleStateImageBehavior = false;
            this.lstvPeriodos.SelectedIndexChanged += new System.EventHandler(this.lstvPeriodos_SelectedIndexChanged);
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(322, 458);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(75, 23);
            this.btnEnviar.TabIndex = 9;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(403, 458);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Correo:";
            // 
            // txtCorreoElectronico
            // 
            this.txtCorreoElectronico.Location = new System.Drawing.Point(54, 6);
            this.txtCorreoElectronico.Name = "txtCorreoElectronico";
            this.txtCorreoElectronico.Size = new System.Drawing.Size(187, 20);
            this.txtCorreoElectronico.TabIndex = 12;
            // 
            // dgvEmpleados
            // 
            this.dgvEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmpleados.ColumnHeadersVisible = false;
            this.dgvEmpleados.Location = new System.Drawing.Point(10, 232);
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.RowHeadersVisible = false;
            this.dgvEmpleados.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvEmpleados.Size = new System.Drawing.Size(466, 220);
            this.dgvEmpleados.TabIndex = 18;
            // 
            // Visor
            // 
            this.Visor.Location = new System.Drawing.Point(164, 439);
            this.Visor.Name = "Visor";
            this.Visor.Size = new System.Drawing.Size(133, 42);
            this.Visor.TabIndex = 19;
            this.Visor.Visible = false;
            // 
            // BarraEstado
            // 
            this.BarraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblEtapa});
            this.BarraEstado.Location = new System.Drawing.Point(0, 485);
            this.BarraEstado.Name = "BarraEstado";
            this.BarraEstado.Size = new System.Drawing.Size(486, 22);
            this.BarraEstado.TabIndex = 20;
            this.BarraEstado.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(125, 17);
            this.toolStripStatusLabel1.Text = "Proceso de enviado:....";
            // 
            // lblEtapa
            // 
            this.lblEtapa.Name = "lblEtapa";
            this.lblEtapa.Size = new System.Drawing.Size(36, 17);
            this.lblEtapa.Text = "Etapa";
            // 
            // workerEnvio
            // 
            this.workerEnvio.WorkerReportsProgress = true;
            this.workerEnvio.WorkerSupportsCancellation = true;
            this.workerEnvio.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerEnvio_DoWork);
            this.workerEnvio.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workerEnvio_ProgressChanged);
            this.workerEnvio.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerEnvio_RunWorkerCompleted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Empleados:";
            // 
            // chkTodos
            // 
            this.chkTodos.AutoSize = true;
            this.chkTodos.Location = new System.Drawing.Point(332, 28);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(111, 17);
            this.chkTodos.TabIndex = 24;
            this.chkTodos.Text = "Seleccionar todos";
            this.chkTodos.UseVisualStyleBackColor = true;
            this.chkTodos.CheckedChanged += new System.EventHandler(this.chkTodos_CheckedChanged);
            // 
            // frmEnvioRecibos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 507);
            this.Controls.Add(this.chkTodos);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BarraEstado);
            this.Controls.Add(this.dgvEmpleados);
            this.Controls.Add(this.txtCorreoElectronico);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.lstvDepartamentos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstvPeriodos);
            this.Controls.Add(this.Visor);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEnvioRecibos";
            this.Text = "Envio de recibos";
            this.Load += new System.EventHandler(this.frmEnvioRecibos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.BarraEstado.ResumeLayout(false);
            this.BarraEstado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstvDepartamentos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lstvPeriodos;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCorreoElectronico;
        private System.Windows.Forms.DataGridView dgvEmpleados;
        private Microsoft.Reporting.WinForms.ReportViewer Visor;
        private System.Windows.Forms.StatusStrip BarraEstado;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblEtapa;
        private System.ComponentModel.BackgroundWorker workerEnvio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkTodos;
    }
}