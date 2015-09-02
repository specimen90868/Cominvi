﻿namespace Nominas
{
    partial class frmEmpleados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmpleados));
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolTitulo = new System.Windows.Forms.ToolStripLabel();
            this.toolEmpleado = new System.Windows.Forms.ToolStrip();
            this.toolHistorial = new System.Windows.Forms.ToolStripButton();
            this.toolGuardarCerrar = new System.Windows.Forms.ToolStripButton();
            this.toolGuardarNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.lblSalario = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.txtNSS = new System.Windows.Forms.MaskedTextBox();
            this.txtCURP = new System.Windows.Forms.TextBox();
            this.Label27 = new System.Windows.Forms.Label();
            this.Label26 = new System.Windows.Forms.Label();
            this.cmbTipoSalario = new System.Windows.Forms.ComboBox();
            this.lblTipoSalario = new System.Windows.Forms.Label();
            this.txtSueldo = new System.Windows.Forms.TextBox();
            this.lblSueldo = new System.Windows.Forms.Label();
            this.txtSD = new System.Windows.Forms.TextBox();
            this.lblSD = new System.Windows.Forms.Label();
            this.txtSDI = new System.Windows.Forms.TextBox();
            this.lblSDI = new System.Windows.Forms.Label();
            this.dtpFechaIngreso = new System.Windows.Forms.DateTimePicker();
            this.Label11 = new System.Windows.Forms.Label();
            this.cmbPeriodo = new System.Windows.Forms.ComboBox();
            this.lblPeriodo = new System.Windows.Forms.Label();
            this.txtApMaterno = new System.Windows.Forms.TextBox();
            this.txtApPaterno = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRFC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.txtEdad = new System.Windows.Forms.TextBox();
            this.txtDigito = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNoEmpleado = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbDepartamento = new System.Windows.Forms.ComboBox();
            this.cmbPuesto = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpFechaAntiguedad = new System.Windows.Forms.DateTimePicker();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.txtAntiguedadMod = new System.Windows.Forms.TextBox();
            this.rbtnHombre = new System.Windows.Forms.RadioButton();
            this.rbtnMujer = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.btnObtenerCurp = new System.Windows.Forms.Button();
            this.btnVer = new System.Windows.Forms.Button();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtAntiguedad = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbZona = new System.Windows.Forms.ComboBox();
            this.toolAcciones.SuspendLayout();
            this.toolEmpleado.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolAcciones
            // 
            this.toolAcciones.BackColor = System.Drawing.Color.DarkGray;
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTitulo});
            this.toolAcciones.Location = new System.Drawing.Point(0, 0);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(696, 27);
            this.toolAcciones.TabIndex = 1;
            this.toolAcciones.Text = "toolAcciones";
            // 
            // toolTitulo
            // 
            this.toolTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(170, 24);
            this.toolTitulo.Text = "Nuevo empleado";
            // 
            // toolEmpleado
            // 
            this.toolEmpleado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolHistorial,
            this.toolGuardarCerrar,
            this.toolGuardarNuevo,
            this.toolCerrar});
            this.toolEmpleado.Location = new System.Drawing.Point(0, 27);
            this.toolEmpleado.Name = "toolEmpleado";
            this.toolEmpleado.Size = new System.Drawing.Size(696, 25);
            this.toolEmpleado.TabIndex = 2;
            this.toolEmpleado.Text = "toolEmpresa";
            // 
            // toolHistorial
            // 
            this.toolHistorial.Image = ((System.Drawing.Image)(resources.GetObject("toolHistorial.Image")));
            this.toolHistorial.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolHistorial.Name = "toolHistorial";
            this.toolHistorial.Size = new System.Drawing.Size(71, 22);
            this.toolHistorial.Text = "Historial";
            this.toolHistorial.Click += new System.EventHandler(this.toolHistorial_Click);
            // 
            // toolGuardarCerrar
            // 
            this.toolGuardarCerrar.Image = ((System.Drawing.Image)(resources.GetObject("toolGuardarCerrar.Image")));
            this.toolGuardarCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGuardarCerrar.Name = "toolGuardarCerrar";
            this.toolGuardarCerrar.Size = new System.Drawing.Size(113, 22);
            this.toolGuardarCerrar.Text = "Guardar y Cerrar";
            this.toolGuardarCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolGuardarCerrar.Click += new System.EventHandler(this.toolGuardarCerrar_Click);
            // 
            // toolGuardarNuevo
            // 
            this.toolGuardarNuevo.Image = ((System.Drawing.Image)(resources.GetObject("toolGuardarNuevo.Image")));
            this.toolGuardarNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGuardarNuevo.Name = "toolGuardarNuevo";
            this.toolGuardarNuevo.Size = new System.Drawing.Size(116, 22);
            this.toolGuardarNuevo.Text = "Guardar y Nuevo";
            this.toolGuardarNuevo.Click += new System.EventHandler(this.toolGuardarNuevo_Click);
            // 
            // toolCerrar
            // 
            this.toolCerrar.Image = ((System.Drawing.Image)(resources.GetObject("toolCerrar.Image")));
            this.toolCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCerrar.Name = "toolCerrar";
            this.toolCerrar.Size = new System.Drawing.Size(59, 22);
            this.toolCerrar.Text = "Cerrar";
            this.toolCerrar.Click += new System.EventHandler(this.toolCerrar_Click);
            // 
            // lblSalario
            // 
            this.lblSalario.AutoSize = true;
            this.lblSalario.BackColor = System.Drawing.Color.White;
            this.lblSalario.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblSalario.Location = new System.Drawing.Point(13, 362);
            this.lblSalario.Name = "lblSalario";
            this.lblSalario.Size = new System.Drawing.Size(61, 18);
            this.lblSalario.TabIndex = 209;
            this.lblSalario.Text = "Salario";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.Color.White;
            this.Label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.Label16.Location = new System.Drawing.Point(14, 73);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(139, 18);
            this.Label16.TabIndex = 208;
            this.Label16.Text = "Datos principales";
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(285, 419);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(54, 20);
            this.btnCalcular.TabIndex = 23;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // txtNSS
            // 
            this.txtNSS.Location = new System.Drawing.Point(415, 165);
            this.txtNSS.Mask = "9999999999";
            this.txtNSS.Name = "txtNSS";
            this.txtNSS.Size = new System.Drawing.Size(151, 20);
            this.txtNSS.TabIndex = 14;
            // 
            // txtCURP
            // 
            this.txtCURP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCURP.Location = new System.Drawing.Point(415, 139);
            this.txtCURP.Name = "txtCURP";
            this.txtCURP.Size = new System.Drawing.Size(151, 20);
            this.txtCURP.TabIndex = 12;
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.Location = new System.Drawing.Point(357, 142);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(52, 13);
            this.Label27.TabIndex = 195;
            this.Label27.Text = "C.U.R.P.:";
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.Location = new System.Drawing.Point(368, 168);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(41, 13);
            this.Label26.TabIndex = 196;
            this.Label26.Text = "N.S.S.:";
            // 
            // cmbTipoSalario
            // 
            this.cmbTipoSalario.Enabled = false;
            this.cmbTipoSalario.FormattingEnabled = true;
            this.cmbTipoSalario.Location = new System.Drawing.Point(126, 392);
            this.cmbTipoSalario.Name = "cmbTipoSalario";
            this.cmbTipoSalario.Size = new System.Drawing.Size(152, 21);
            this.cmbTipoSalario.TabIndex = 21;
            // 
            // lblTipoSalario
            // 
            this.lblTipoSalario.AutoSize = true;
            this.lblTipoSalario.Location = new System.Drawing.Point(42, 395);
            this.lblTipoSalario.Name = "lblTipoSalario";
            this.lblTipoSalario.Size = new System.Drawing.Size(79, 13);
            this.lblTipoSalario.TabIndex = 193;
            this.lblTipoSalario.Text = "Tipo de salario:";
            // 
            // txtSueldo
            // 
            this.txtSueldo.Enabled = false;
            this.txtSueldo.Location = new System.Drawing.Point(127, 419);
            this.txtSueldo.Name = "txtSueldo";
            this.txtSueldo.Size = new System.Drawing.Size(152, 20);
            this.txtSueldo.TabIndex = 22;
            this.txtSueldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSueldo
            // 
            this.lblSueldo.AutoSize = true;
            this.lblSueldo.Location = new System.Drawing.Point(23, 423);
            this.lblSueldo.Name = "lblSueldo";
            this.lblSueldo.Size = new System.Drawing.Size(98, 13);
            this.lblSueldo.TabIndex = 192;
            this.lblSueldo.Text = "Sueldo del periodo:";
            // 
            // txtSD
            // 
            this.txtSD.Location = new System.Drawing.Point(126, 445);
            this.txtSD.Name = "txtSD";
            this.txtSD.ReadOnly = true;
            this.txtSD.Size = new System.Drawing.Size(152, 20);
            this.txtSD.TabIndex = 24;
            this.txtSD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSD
            // 
            this.lblSD.AutoSize = true;
            this.lblSD.Location = new System.Drawing.Point(51, 448);
            this.lblSD.Name = "lblSD";
            this.lblSD.Size = new System.Drawing.Size(70, 13);
            this.lblSD.TabIndex = 190;
            this.lblSD.Text = "Salario diario:";
            // 
            // txtSDI
            // 
            this.txtSDI.Location = new System.Drawing.Point(126, 471);
            this.txtSDI.Name = "txtSDI";
            this.txtSDI.ReadOnly = true;
            this.txtSDI.Size = new System.Drawing.Size(152, 20);
            this.txtSDI.TabIndex = 25;
            this.txtSDI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSDI
            // 
            this.lblSDI.AutoSize = true;
            this.lblSDI.Location = new System.Drawing.Point(4, 474);
            this.lblSDI.Name = "lblSDI";
            this.lblSDI.Size = new System.Drawing.Size(117, 13);
            this.lblSDI.TabIndex = 187;
            this.lblSDI.Text = "Salario diario integrado:";
            // 
            // dtpFechaIngreso
            // 
            this.dtpFechaIngreso.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIngreso.Location = new System.Drawing.Point(127, 217);
            this.dtpFechaIngreso.Name = "dtpFechaIngreso";
            this.dtpFechaIngreso.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpFechaIngreso.Size = new System.Drawing.Size(152, 20);
            this.dtpFechaIngreso.TabIndex = 5;
            this.dtpFechaIngreso.Leave += new System.EventHandler(this.dtpFechaIngreso_Leave);
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(29, 220);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(92, 13);
            this.Label11.TabIndex = 184;
            this.Label11.Text = "Fecha de ingreso:";
            // 
            // cmbPeriodo
            // 
            this.cmbPeriodo.FormattingEnabled = true;
            this.cmbPeriodo.Location = new System.Drawing.Point(415, 273);
            this.cmbPeriodo.Name = "cmbPeriodo";
            this.cmbPeriodo.Size = new System.Drawing.Size(151, 21);
            this.cmbPeriodo.TabIndex = 18;
            this.cmbPeriodo.SelectedIndexChanged += new System.EventHandler(this.cmbPeriodo_SelectedIndexChanged);
            // 
            // lblPeriodo
            // 
            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.Location = new System.Drawing.Point(363, 276);
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.Size = new System.Drawing.Size(46, 13);
            this.lblPeriodo.TabIndex = 183;
            this.lblPeriodo.Text = "Periodo:";
            // 
            // txtApMaterno
            // 
            this.txtApMaterno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtApMaterno.Location = new System.Drawing.Point(127, 165);
            this.txtApMaterno.Name = "txtApMaterno";
            this.txtApMaterno.Size = new System.Drawing.Size(152, 20);
            this.txtApMaterno.TabIndex = 3;
            // 
            // txtApPaterno
            // 
            this.txtApPaterno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtApPaterno.Location = new System.Drawing.Point(127, 139);
            this.txtApPaterno.Name = "txtApPaterno";
            this.txtApPaterno.Size = new System.Drawing.Size(152, 20);
            this.txtApPaterno.TabIndex = 2;
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Location = new System.Drawing.Point(127, 113);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(152, 20);
            this.txtNombre.TabIndex = 1;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(32, 168);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(89, 13);
            this.Label3.TabIndex = 179;
            this.Label3.Text = "Apellido Materno:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(34, 142);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(87, 13);
            this.Label2.TabIndex = 177;
            this.Label2.Text = "Apellido Paterno:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(63, 116);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(58, 13);
            this.Label7.TabIndex = 175;
            this.Label7.Text = "Nombre(s):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(369, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 210;
            this.label1.Text = "R.F.C.:";
            // 
            // txtRFC
            // 
            this.txtRFC.Location = new System.Drawing.Point(415, 113);
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.Size = new System.Drawing.Size(151, 20);
            this.txtRFC.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(320, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 213;
            this.label4.Text = "Digito verificador:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 272);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 214;
            this.label5.Text = "Fecha de nacimiento:";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(127, 269);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(152, 20);
            this.dtpFechaNacimiento.TabIndex = 7;
            this.dtpFechaNacimiento.Leave += new System.EventHandler(this.dtpFechaNacimiento_Leave);
            // 
            // txtEdad
            // 
            this.txtEdad.Enabled = false;
            this.txtEdad.Location = new System.Drawing.Point(284, 269);
            this.txtEdad.Name = "txtEdad";
            this.txtEdad.Size = new System.Drawing.Size(30, 20);
            this.txtEdad.TabIndex = 19;
            // 
            // txtDigito
            // 
            this.txtDigito.Location = new System.Drawing.Point(415, 191);
            this.txtDigito.Mask = "9";
            this.txtDigito.Name = "txtDigito";
            this.txtDigito.Size = new System.Drawing.Size(23, 20);
            this.txtDigito.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 216;
            this.label6.Text = "No. de Empleado:";
            // 
            // txtNoEmpleado
            // 
            this.txtNoEmpleado.Location = new System.Drawing.Point(127, 191);
            this.txtNoEmpleado.Name = "txtNoEmpleado";
            this.txtNoEmpleado.Size = new System.Drawing.Size(152, 20);
            this.txtNoEmpleado.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(332, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 218;
            this.label8.Text = "Departamento:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(366, 249);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 219;
            this.label9.Text = "Puesto:";
            // 
            // cmbDepartamento
            // 
            this.cmbDepartamento.FormattingEnabled = true;
            this.cmbDepartamento.Location = new System.Drawing.Point(415, 219);
            this.cmbDepartamento.Name = "cmbDepartamento";
            this.cmbDepartamento.Size = new System.Drawing.Size(151, 21);
            this.cmbDepartamento.TabIndex = 16;
            // 
            // cmbPuesto
            // 
            this.cmbPuesto.FormattingEnabled = true;
            this.cmbPuesto.Location = new System.Drawing.Point(415, 246);
            this.cmbPuesto.Name = "cmbPuesto";
            this.cmbPuesto.Size = new System.Drawing.Size(151, 21);
            this.cmbPuesto.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 246);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 13);
            this.label10.TabIndex = 222;
            this.label10.Text = "Fecha de antiguedad:";
            // 
            // dtpFechaAntiguedad
            // 
            this.dtpFechaAntiguedad.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaAntiguedad.Location = new System.Drawing.Point(127, 243);
            this.dtpFechaAntiguedad.Name = "dtpFechaAntiguedad";
            this.dtpFechaAntiguedad.Size = new System.Drawing.Size(151, 20);
            this.dtpFechaAntiguedad.TabIndex = 6;
            this.dtpFechaAntiguedad.ValueChanged += new System.EventHandler(this.dtpFechaAntiguedad_ValueChanged);
            this.dtpFechaAntiguedad.Leave += new System.EventHandler(this.dtpFechaAntiguedad_Leave);
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(78, 298);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(43, 13);
            this.lblEstado.TabIndex = 224;
            this.lblEstado.Text = "Estado:";
            // 
            // cmbEstado
            // 
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.ItemHeight = 13;
            this.cmbEstado.Location = new System.Drawing.Point(127, 295);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(151, 21);
            this.cmbEstado.TabIndex = 225;
            // 
            // txtAntiguedadMod
            // 
            this.txtAntiguedadMod.Enabled = false;
            this.txtAntiguedadMod.Location = new System.Drawing.Point(284, 243);
            this.txtAntiguedadMod.Name = "txtAntiguedadMod";
            this.txtAntiguedadMod.Size = new System.Drawing.Size(30, 20);
            this.txtAntiguedadMod.TabIndex = 226;
            // 
            // rbtnHombre
            // 
            this.rbtnHombre.AutoSize = true;
            this.rbtnHombre.Location = new System.Drawing.Point(128, 322);
            this.rbtnHombre.Name = "rbtnHombre";
            this.rbtnHombre.Size = new System.Drawing.Size(62, 17);
            this.rbtnHombre.TabIndex = 9;
            this.rbtnHombre.TabStop = true;
            this.rbtnHombre.Text = "Hombre";
            this.rbtnHombre.UseVisualStyleBackColor = true;
            // 
            // rbtnMujer
            // 
            this.rbtnMujer.AutoSize = true;
            this.rbtnMujer.Location = new System.Drawing.Point(206, 322);
            this.rbtnMujer.Name = "rbtnMujer";
            this.rbtnMujer.Size = new System.Drawing.Size(51, 17);
            this.rbtnMujer.TabIndex = 10;
            this.rbtnMujer.TabStop = true;
            this.rbtnMujer.Text = "Mujer";
            this.rbtnMujer.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(86, 324);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 230;
            this.label12.Text = "Sexo:";
            // 
            // btnObtenerCurp
            // 
            this.btnObtenerCurp.Location = new System.Drawing.Point(572, 139);
            this.btnObtenerCurp.Name = "btnObtenerCurp";
            this.btnObtenerCurp.Size = new System.Drawing.Size(63, 23);
            this.btnObtenerCurp.TabIndex = 13;
            this.btnObtenerCurp.Text = "Obtener";
            this.btnObtenerCurp.UseVisualStyleBackColor = true;
            this.btnObtenerCurp.Click += new System.EventHandler(this.btnObtenerCurp_Click);
            // 
            // btnVer
            // 
            this.btnVer.Location = new System.Drawing.Point(415, 339);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(67, 23);
            this.btnVer.TabIndex = 19;
            this.btnVer.Text = "Ver";
            this.btnVer.UseVisualStyleBackColor = true;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Location = new System.Drawing.Point(499, 339);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(67, 23);
            this.btnAsignar.TabIndex = 20;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(378, 342);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 13);
            this.label13.TabIndex = 234;
            this.label13.Text = "Foto:";
            // 
            // txtAntiguedad
            // 
            this.txtAntiguedad.Enabled = false;
            this.txtAntiguedad.Location = new System.Drawing.Point(284, 218);
            this.txtAntiguedad.Name = "txtAntiguedad";
            this.txtAntiguedad.Size = new System.Drawing.Size(30, 20);
            this.txtAntiguedad.TabIndex = 235;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(374, 303);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 13);
            this.label14.TabIndex = 236;
            this.label14.Text = "Zona:";
            // 
            // cmbZona
            // 
            this.cmbZona.FormattingEnabled = true;
            this.cmbZona.Location = new System.Drawing.Point(415, 300);
            this.cmbZona.Name = "cmbZona";
            this.cmbZona.Size = new System.Drawing.Size(151, 21);
            this.cmbZona.TabIndex = 237;
            // 
            // frmEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 609);
            this.Controls.Add(this.cmbZona);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtAntiguedad);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnAsignar);
            this.Controls.Add(this.btnVer);
            this.Controls.Add(this.btnObtenerCurp);
            this.Controls.Add(this.rbtnMujer);
            this.Controls.Add(this.rbtnHombre);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtAntiguedadMod);
            this.Controls.Add(this.cmbEstado);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.dtpFechaAntiguedad);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmbPuesto);
            this.Controls.Add(this.cmbDepartamento);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtNoEmpleado);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDigito);
            this.Controls.Add(this.txtEdad);
            this.Controls.Add(this.dtpFechaNacimiento);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRFC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSalario);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.txtNSS);
            this.Controls.Add(this.txtCURP);
            this.Controls.Add(this.Label27);
            this.Controls.Add(this.Label26);
            this.Controls.Add(this.cmbTipoSalario);
            this.Controls.Add(this.lblTipoSalario);
            this.Controls.Add(this.txtSueldo);
            this.Controls.Add(this.lblSueldo);
            this.Controls.Add(this.txtSD);
            this.Controls.Add(this.lblSD);
            this.Controls.Add(this.txtSDI);
            this.Controls.Add(this.lblSDI);
            this.Controls.Add(this.dtpFechaIngreso);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.cmbPeriodo);
            this.Controls.Add(this.lblPeriodo);
            this.Controls.Add(this.txtApMaterno);
            this.Controls.Add(this.txtApPaterno);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.toolEmpleado);
            this.Controls.Add(this.toolAcciones);
            this.Name = "frmEmpleados";
            this.Text = "Empleados";
            this.Load += new System.EventHandler(this.frmEmpleados_Load);
            this.toolAcciones.ResumeLayout(false);
            this.toolAcciones.PerformLayout();
            this.toolEmpleado.ResumeLayout(false);
            this.toolEmpleado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolAcciones;
        internal System.Windows.Forms.ToolStripLabel toolTitulo;
        internal System.Windows.Forms.ToolStrip toolEmpleado;
        internal System.Windows.Forms.ToolStripButton toolGuardarCerrar;
        internal System.Windows.Forms.ToolStripButton toolGuardarNuevo;
        private System.Windows.Forms.ToolStripButton toolCerrar;
        internal System.Windows.Forms.Label lblSalario;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.Button btnCalcular;
        internal System.Windows.Forms.MaskedTextBox txtNSS;
        internal System.Windows.Forms.TextBox txtCURP;
        internal System.Windows.Forms.Label Label27;
        internal System.Windows.Forms.Label Label26;
        internal System.Windows.Forms.ComboBox cmbTipoSalario;
        internal System.Windows.Forms.Label lblTipoSalario;
        internal System.Windows.Forms.TextBox txtSueldo;
        internal System.Windows.Forms.Label lblSueldo;
        internal System.Windows.Forms.TextBox txtSD;
        internal System.Windows.Forms.Label lblSD;
        internal System.Windows.Forms.TextBox txtSDI;
        internal System.Windows.Forms.Label lblSDI;
        internal System.Windows.Forms.DateTimePicker dtpFechaIngreso;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.ComboBox cmbPeriodo;
        internal System.Windows.Forms.Label lblPeriodo;
        internal System.Windows.Forms.TextBox txtApMaterno;
        internal System.Windows.Forms.TextBox txtApPaterno;
        internal System.Windows.Forms.TextBox txtNombre;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRFC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.TextBox txtEdad;
        private System.Windows.Forms.MaskedTextBox txtDigito;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNoEmpleado;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbDepartamento;
        private System.Windows.Forms.ComboBox cmbPuesto;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpFechaAntiguedad;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.TextBox txtAntiguedadMod;
        private System.Windows.Forms.RadioButton rbtnHombre;
        private System.Windows.Forms.RadioButton rbtnMujer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnObtenerCurp;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtAntiguedad;
        private System.Windows.Forms.ToolStripButton toolHistorial;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbZona;
    }
}