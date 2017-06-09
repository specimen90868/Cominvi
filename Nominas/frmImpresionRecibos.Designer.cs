namespace Nominas
{
    partial class frmImpresionRecibos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImpresionRecibos));
            this.lstvPeriodos = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstvDepartamentos = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.lstvEmpleados = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.chkImprimirTodos = new System.Windows.Forms.CheckBox();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.pbxLoad = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // lstvPeriodos
            // 
            this.lstvPeriodos.CheckBoxes = true;
            this.lstvPeriodos.Location = new System.Drawing.Point(15, 25);
            this.lstvPeriodos.MultiSelect = false;
            this.lstvPeriodos.Name = "lstvPeriodos";
            this.lstvPeriodos.Size = new System.Drawing.Size(231, 173);
            this.lstvPeriodos.TabIndex = 0;
            this.lstvPeriodos.UseCompatibleStateImageBehavior = false;
            this.lstvPeriodos.SelectedIndexChanged += new System.EventHandler(this.lstvPeriodos_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Periodos:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Departamentos:";
            // 
            // lstvDepartamentos
            // 
            this.lstvDepartamentos.Location = new System.Drawing.Point(252, 25);
            this.lstvDepartamentos.Name = "lstvDepartamentos";
            this.lstvDepartamentos.Size = new System.Drawing.Size(231, 173);
            this.lstvDepartamentos.TabIndex = 4;
            this.lstvDepartamentos.UseCompatibleStateImageBehavior = false;
            this.lstvDepartamentos.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstvDepartamentos_ItemChecked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Empleados:";
            // 
            // lstvEmpleados
            // 
            this.lstvEmpleados.Location = new System.Drawing.Point(15, 244);
            this.lstvEmpleados.Name = "lstvEmpleados";
            this.lstvEmpleados.Size = new System.Drawing.Size(468, 232);
            this.lstvEmpleados.TabIndex = 6;
            this.lstvEmpleados.UseCompatibleStateImageBehavior = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtBuscar.Location = new System.Drawing.Point(180, 221);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(99, 20);
            this.txtBuscar.TabIndex = 10;
            this.txtBuscar.Text = "No. de empleado";
            this.txtBuscar.Click += new System.EventHandler(this.txtBuscar_Click);
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            this.txtBuscar.Leave += new System.EventHandler(this.txtBuscar_Leave);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(408, 482);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(327, 482);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 12;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // chkImprimirTodos
            // 
            this.chkImprimirTodos.AutoSize = true;
            this.chkImprimirTodos.Location = new System.Drawing.Point(252, 200);
            this.chkImprimirTodos.Name = "chkImprimirTodos";
            this.chkImprimirTodos.Size = new System.Drawing.Size(90, 17);
            this.chkImprimirTodos.TabIndex = 15;
            this.chkImprimirTodos.Text = "Imprimir todos";
            this.chkImprimirTodos.UseVisualStyleBackColor = true;
            this.chkImprimirTodos.CheckedChanged += new System.EventHandler(this.chkImprimirTodos_CheckedChanged);
            // 
            // chkTodos
            // 
            this.chkTodos.AutoSize = true;
            this.chkTodos.Location = new System.Drawing.Point(15, 223);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(111, 17);
            this.chkTodos.TabIndex = 16;
            this.chkTodos.Text = "Seleccionar todos";
            this.chkTodos.UseVisualStyleBackColor = true;
            this.chkTodos.CheckedChanged += new System.EventHandler(this.chkTodos_CheckedChanged);
            // 
            // pbxLoad
            // 
            this.pbxLoad.Image = ((System.Drawing.Image)(resources.GetObject("pbxLoad.Image")));
            this.pbxLoad.Location = new System.Drawing.Point(202, 186);
            this.pbxLoad.Name = "pbxLoad";
            this.pbxLoad.Size = new System.Drawing.Size(93, 74);
            this.pbxLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxLoad.TabIndex = 17;
            this.pbxLoad.TabStop = false;
            this.pbxLoad.WaitOnLoad = true;
            // 
            // frmImpresionRecibos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 518);
            this.Controls.Add(this.pbxLoad);
            this.Controls.Add(this.chkTodos);
            this.Controls.Add(this.chkImprimirTodos);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstvEmpleados);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstvDepartamentos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstvPeriodos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImpresionRecibos";
            this.Text = "Impresión de recibos";
            this.Load += new System.EventHandler(this.frmImpresionRecibos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstvPeriodos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lstvDepartamentos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lstvEmpleados;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.CheckBox chkImprimirTodos;
        private System.Windows.Forms.CheckBox chkTodos;
        private System.Windows.Forms.PictureBox pbxLoad;
    }
}