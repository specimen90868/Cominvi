namespace Nominas
{
    partial class frmModificaSalarioImss
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
            this.lblSalario = new System.Windows.Forms.Label();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.txtSueldo = new System.Windows.Forms.TextBox();
            this.lblSueldo = new System.Windows.Forms.Label();
            this.txtSD = new System.Windows.Forms.TextBox();
            this.lblSD = new System.Windows.Forms.Label();
            this.txtSDI = new System.Windows.Forms.TextBox();
            this.lblSDI = new System.Windows.Forms.Label();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSalario
            // 
            this.lblSalario.AutoSize = true;
            this.lblSalario.BackColor = System.Drawing.Color.White;
            this.lblSalario.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblSalario.Location = new System.Drawing.Point(12, 47);
            this.lblSalario.Name = "lblSalario";
            this.lblSalario.Size = new System.Drawing.Size(61, 18);
            this.lblSalario.TabIndex = 218;
            this.lblSalario.Text = "Salario";
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(304, 77);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(63, 20);
            this.btnCalcular.TabIndex = 212;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // txtSueldo
            // 
            this.txtSueldo.Location = new System.Drawing.Point(146, 77);
            this.txtSueldo.Name = "txtSueldo";
            this.txtSueldo.Size = new System.Drawing.Size(152, 20);
            this.txtSueldo.TabIndex = 211;
            this.txtSueldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSueldo
            // 
            this.lblSueldo.AutoSize = true;
            this.lblSueldo.Location = new System.Drawing.Point(9, 81);
            this.lblSueldo.Name = "lblSueldo";
            this.lblSueldo.Size = new System.Drawing.Size(98, 13);
            this.lblSueldo.TabIndex = 217;
            this.lblSueldo.Text = "Sueldo del periodo:";
            // 
            // txtSD
            // 
            this.txtSD.Location = new System.Drawing.Point(146, 103);
            this.txtSD.Name = "txtSD";
            this.txtSD.ReadOnly = true;
            this.txtSD.Size = new System.Drawing.Size(152, 20);
            this.txtSD.TabIndex = 213;
            this.txtSD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSD
            // 
            this.lblSD.AutoSize = true;
            this.lblSD.Location = new System.Drawing.Point(9, 106);
            this.lblSD.Name = "lblSD";
            this.lblSD.Size = new System.Drawing.Size(70, 13);
            this.lblSD.TabIndex = 216;
            this.lblSD.Text = "Salario diario:";
            // 
            // txtSDI
            // 
            this.txtSDI.Location = new System.Drawing.Point(146, 129);
            this.txtSDI.Name = "txtSDI";
            this.txtSDI.ReadOnly = true;
            this.txtSDI.Size = new System.Drawing.Size(152, 20);
            this.txtSDI.TabIndex = 214;
            this.txtSDI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSDI
            // 
            this.lblSDI.AutoSize = true;
            this.lblSDI.Location = new System.Drawing.Point(9, 132);
            this.lblSDI.Name = "lblSDI";
            this.lblSDI.Size = new System.Drawing.Size(117, 13);
            this.lblSDI.TabIndex = 215;
            this.lblSDI.Text = "Salario diario integrado:";
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.Location = new System.Drawing.Point(108, 9);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(183, 20);
            this.lblEmpleado.TabIndex = 220;
            this.lblEmpleado.Text = "Nombre del empleado";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 219;
            this.label1.Text = "Empleado:";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(277, 168);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 221;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(358, 168);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 222;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmModificaSalarioImss
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 202);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.lblEmpleado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSalario);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.txtSueldo);
            this.Controls.Add(this.lblSueldo);
            this.Controls.Add(this.txtSD);
            this.Controls.Add(this.lblSD);
            this.Controls.Add(this.txtSDI);
            this.Controls.Add(this.lblSDI);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModificaSalarioImss";
            this.Text = "Modifica de salario (IMSS)";
            this.Load += new System.EventHandler(this.frmModificaSalarioImss_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblSalario;
        internal System.Windows.Forms.Button btnCalcular;
        internal System.Windows.Forms.TextBox txtSueldo;
        internal System.Windows.Forms.Label lblSueldo;
        internal System.Windows.Forms.TextBox txtSD;
        internal System.Windows.Forms.Label lblSD;
        internal System.Windows.Forms.TextBox txtSDI;
        internal System.Windows.Forms.Label lblSDI;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
    }
}