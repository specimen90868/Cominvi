namespace Nominas
{
    partial class frmReingresoEmpleado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReingresoEmpleado));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbRegistroPatronal = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpFechaReingreso = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaAntiguedad = new System.Windows.Forms.DateTimePicker();
            this.cmbDepartamento = new System.Windows.Forms.ComboBox();
            this.cmbPuesto = new System.Windows.Forms.ComboBox();
            this.txtSueldo = new System.Windows.Forms.TextBox();
            this.txtSalarioDiario = new System.Windows.Forms.TextBox();
            this.txtSDI = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.txtAntiguedad = new System.Windows.Forms.TextBox();
            this.txtAntiguedadMod = new System.Windows.Forms.TextBox();
            this.lblNombreEmpleado = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbPeriodo = new System.Windows.Forms.ComboBox();
            this.cmbEstados = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.rbtnMujer = new System.Windows.Forms.RadioButton();
            this.rbtnHombre = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.mtxtIdBancario = new System.Windows.Forms.MaskedTextBox();
            this.mtxtCuentaClabe = new System.Windows.Forms.MaskedTextBox();
            this.mtxtCuentaBancaria = new System.Windows.Forms.MaskedTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbMetodoPago = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registro patronal:";
            // 
            // cmbRegistroPatronal
            // 
            this.cmbRegistroPatronal.FormattingEnabled = true;
            this.cmbRegistroPatronal.Location = new System.Drawing.Point(127, 41);
            this.cmbRegistroPatronal.Name = "cmbRegistroPatronal";
            this.cmbRegistroPatronal.Size = new System.Drawing.Size(327, 21);
            this.cmbRegistroPatronal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fecha reingreso:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Fecha de antiguedad:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Departamento:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(78, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Puesto:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Sueldo del periodo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(51, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Salario diario:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 256);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Salario diario integrado:";
            // 
            // dtpFechaReingreso
            // 
            this.dtpFechaReingreso.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaReingreso.Location = new System.Drawing.Point(127, 68);
            this.dtpFechaReingreso.Name = "dtpFechaReingreso";
            this.dtpFechaReingreso.Size = new System.Drawing.Size(128, 20);
            this.dtpFechaReingreso.TabIndex = 9;
            this.dtpFechaReingreso.Leave += new System.EventHandler(this.dtpFechaReingreso_Leave);
            // 
            // dtpFechaAntiguedad
            // 
            this.dtpFechaAntiguedad.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaAntiguedad.Location = new System.Drawing.Point(127, 94);
            this.dtpFechaAntiguedad.Name = "dtpFechaAntiguedad";
            this.dtpFechaAntiguedad.Size = new System.Drawing.Size(128, 20);
            this.dtpFechaAntiguedad.TabIndex = 10;
            this.dtpFechaAntiguedad.Leave += new System.EventHandler(this.dtpFechaAntiguedad_Leave);
            // 
            // cmbDepartamento
            // 
            this.cmbDepartamento.FormattingEnabled = true;
            this.cmbDepartamento.Location = new System.Drawing.Point(127, 120);
            this.cmbDepartamento.Name = "cmbDepartamento";
            this.cmbDepartamento.Size = new System.Drawing.Size(128, 21);
            this.cmbDepartamento.TabIndex = 11;
            // 
            // cmbPuesto
            // 
            this.cmbPuesto.FormattingEnabled = true;
            this.cmbPuesto.Location = new System.Drawing.Point(127, 147);
            this.cmbPuesto.Name = "cmbPuesto";
            this.cmbPuesto.Size = new System.Drawing.Size(128, 21);
            this.cmbPuesto.TabIndex = 12;
            // 
            // txtSueldo
            // 
            this.txtSueldo.Enabled = false;
            this.txtSueldo.Location = new System.Drawing.Point(127, 201);
            this.txtSueldo.Name = "txtSueldo";
            this.txtSueldo.Size = new System.Drawing.Size(128, 20);
            this.txtSueldo.TabIndex = 13;
            // 
            // txtSalarioDiario
            // 
            this.txtSalarioDiario.Enabled = false;
            this.txtSalarioDiario.Location = new System.Drawing.Point(127, 227);
            this.txtSalarioDiario.Name = "txtSalarioDiario";
            this.txtSalarioDiario.Size = new System.Drawing.Size(128, 20);
            this.txtSalarioDiario.TabIndex = 14;
            // 
            // txtSDI
            // 
            this.txtSDI.Location = new System.Drawing.Point(127, 253);
            this.txtSDI.Name = "txtSDI";
            this.txtSDI.Size = new System.Drawing.Size(128, 20);
            this.txtSDI.TabIndex = 15;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(298, 416);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 28);
            this.btnAceptar.TabIndex = 16;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(379, 416);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 28);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(261, 253);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(62, 23);
            this.btnCalcular.TabIndex = 18;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // txtAntiguedad
            // 
            this.txtAntiguedad.Enabled = false;
            this.txtAntiguedad.Location = new System.Drawing.Point(261, 68);
            this.txtAntiguedad.Name = "txtAntiguedad";
            this.txtAntiguedad.Size = new System.Drawing.Size(27, 20);
            this.txtAntiguedad.TabIndex = 19;
            // 
            // txtAntiguedadMod
            // 
            this.txtAntiguedadMod.Enabled = false;
            this.txtAntiguedadMod.Location = new System.Drawing.Point(261, 94);
            this.txtAntiguedadMod.Name = "txtAntiguedadMod";
            this.txtAntiguedadMod.Size = new System.Drawing.Size(27, 20);
            this.txtAntiguedadMod.TabIndex = 20;
            // 
            // lblNombreEmpleado
            // 
            this.lblNombreEmpleado.AutoSize = true;
            this.lblNombreEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreEmpleado.Location = new System.Drawing.Point(12, 9);
            this.lblNombreEmpleado.Name = "lblNombreEmpleado";
            this.lblNombreEmpleado.Size = new System.Drawing.Size(191, 20);
            this.lblNombreEmpleado.TabIndex = 21;
            this.lblNombreEmpleado.Text = "Nombre del empleado";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(75, 177);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Periodo:";
            // 
            // cmbPeriodo
            // 
            this.cmbPeriodo.FormattingEnabled = true;
            this.cmbPeriodo.Location = new System.Drawing.Point(127, 174);
            this.cmbPeriodo.Name = "cmbPeriodo";
            this.cmbPeriodo.Size = new System.Drawing.Size(128, 21);
            this.cmbPeriodo.TabIndex = 23;
            // 
            // cmbEstados
            // 
            this.cmbEstados.FormattingEnabled = true;
            this.cmbEstados.Location = new System.Drawing.Point(127, 279);
            this.cmbEstados.Name = "cmbEstados";
            this.cmbEstados.Size = new System.Drawing.Size(128, 21);
            this.cmbEstados.TabIndex = 24;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(78, 282);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Estado:";
            // 
            // rbtnMujer
            // 
            this.rbtnMujer.AutoSize = true;
            this.rbtnMujer.Location = new System.Drawing.Point(205, 311);
            this.rbtnMujer.Name = "rbtnMujer";
            this.rbtnMujer.Size = new System.Drawing.Size(51, 17);
            this.rbtnMujer.TabIndex = 232;
            this.rbtnMujer.TabStop = true;
            this.rbtnMujer.Text = "Mujer";
            this.rbtnMujer.UseVisualStyleBackColor = true;
            // 
            // rbtnHombre
            // 
            this.rbtnHombre.AutoSize = true;
            this.rbtnHombre.Location = new System.Drawing.Point(127, 311);
            this.rbtnHombre.Name = "rbtnHombre";
            this.rbtnHombre.Size = new System.Drawing.Size(62, 17);
            this.rbtnHombre.TabIndex = 231;
            this.rbtnHombre.TabStop = true;
            this.rbtnHombre.Text = "Hombre";
            this.rbtnHombre.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(85, 313);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 233;
            this.label12.Text = "Sexo:";
            // 
            // mtxtIdBancario
            // 
            this.mtxtIdBancario.Location = new System.Drawing.Point(127, 388);
            this.mtxtIdBancario.Mask = "9999";
            this.mtxtIdBancario.Name = "mtxtIdBancario";
            this.mtxtIdBancario.Size = new System.Drawing.Size(129, 20);
            this.mtxtIdBancario.TabIndex = 250;
            // 
            // mtxtCuentaClabe
            // 
            this.mtxtCuentaClabe.Location = new System.Drawing.Point(127, 362);
            this.mtxtCuentaClabe.Mask = "999 999 99999999999 9";
            this.mtxtCuentaClabe.Name = "mtxtCuentaClabe";
            this.mtxtCuentaClabe.Size = new System.Drawing.Size(129, 20);
            this.mtxtCuentaClabe.TabIndex = 249;
            // 
            // mtxtCuentaBancaria
            // 
            this.mtxtCuentaBancaria.Location = new System.Drawing.Point(127, 336);
            this.mtxtCuentaBancaria.Mask = "9999999999";
            this.mtxtCuentaBancaria.Name = "mtxtCuentaBancaria";
            this.mtxtCuentaBancaria.Size = new System.Drawing.Size(128, 20);
            this.mtxtCuentaBancaria.TabIndex = 248;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(56, 391);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 13);
            this.label19.TabIndex = 247;
            this.label19.Text = "ID bancario:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(48, 365);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(73, 13);
            this.label18.TabIndex = 246;
            this.label18.Text = "Cuenta clabe:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(33, 339);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(88, 13);
            this.label17.TabIndex = 245;
            this.label17.Text = "Cuenta bancaria:";
            // 
            // cmbMetodoPago
            // 
            this.cmbMetodoPago.FormattingEnabled = true;
            this.cmbMetodoPago.Items.AddRange(new object[] {
            "Efectivo",
            "Cheque",
            "Transferencia"});
            this.cmbMetodoPago.Location = new System.Drawing.Point(126, 414);
            this.cmbMetodoPago.Name = "cmbMetodoPago";
            this.cmbMetodoPago.Size = new System.Drawing.Size(129, 21);
            this.cmbMetodoPago.TabIndex = 252;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(33, 417);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(88, 13);
            this.label20.TabIndex = 251;
            this.label20.Text = "Metodo de pago:";
            // 
            // frmReingresoEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 453);
            this.Controls.Add(this.cmbMetodoPago);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.mtxtIdBancario);
            this.Controls.Add(this.mtxtCuentaClabe);
            this.Controls.Add(this.mtxtCuentaBancaria);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.rbtnMujer);
            this.Controls.Add(this.rbtnHombre);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmbEstados);
            this.Controls.Add(this.cmbPeriodo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblNombreEmpleado);
            this.Controls.Add(this.txtAntiguedadMod);
            this.Controls.Add(this.txtAntiguedad);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtSDI);
            this.Controls.Add(this.txtSalarioDiario);
            this.Controls.Add(this.txtSueldo);
            this.Controls.Add(this.cmbPuesto);
            this.Controls.Add(this.cmbDepartamento);
            this.Controls.Add(this.dtpFechaAntiguedad);
            this.Controls.Add(this.dtpFechaReingreso);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbRegistroPatronal);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReingresoEmpleado";
            this.Text = "Reingreso del empleado";
            this.Load += new System.EventHandler(this.frmReingresoEmpleado_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbRegistroPatronal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpFechaReingreso;
        private System.Windows.Forms.DateTimePicker dtpFechaAntiguedad;
        private System.Windows.Forms.ComboBox cmbDepartamento;
        private System.Windows.Forms.ComboBox cmbPuesto;
        private System.Windows.Forms.TextBox txtSueldo;
        private System.Windows.Forms.TextBox txtSalarioDiario;
        private System.Windows.Forms.TextBox txtSDI;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.TextBox txtAntiguedad;
        private System.Windows.Forms.TextBox txtAntiguedadMod;
        private System.Windows.Forms.Label lblNombreEmpleado;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbPeriodo;
        private System.Windows.Forms.ComboBox cmbEstados;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rbtnMujer;
        private System.Windows.Forms.RadioButton rbtnHombre;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.MaskedTextBox mtxtIdBancario;
        private System.Windows.Forms.MaskedTextBox mtxtCuentaClabe;
        private System.Windows.Forms.MaskedTextBox mtxtCuentaBancaria;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbMetodoPago;
        private System.Windows.Forms.Label label20;
    }
}