namespace Nominas
{
    partial class frmComplementos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComplementos));
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolVentana = new System.Windows.Forms.ToolStripLabel();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.Label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPais = new System.Windows.Forms.TextBox();
            this.txtMunicipio = new System.Windows.Forms.TextBox();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtCP = new System.Windows.Forms.MaskedTextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtColonia = new System.Windows.Forms.TextBox();
            this.txtInterior = new System.Windows.Forms.TextBox();
            this.txtCalle = new System.Windows.Forms.TextBox();
            this.txtExterior = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbContrato = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbJornada = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbEstadoCivil = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbSexo = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbEscolaridad = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtClinica = new System.Windows.Forms.TextBox();
            this.txtNacionalidad = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.mTelEmpresa = new System.Windows.Forms.MaskedTextBox();
            this.mTelPersonal = new System.Windows.Forms.MaskedTextBox();
            this.txtExtension = new System.Windows.Forms.TextBox();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.mCelEmpresa = new System.Windows.Forms.MaskedTextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.mTelEmpresa2 = new System.Windows.Forms.MaskedTextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.mTelEmpresa3 = new System.Windows.Forms.MaskedTextBox();
            this.toolTitulo.SuspendLayout();
            this.toolAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTitulo
            // 
            this.toolTitulo.BackColor = System.Drawing.Color.DarkGray;
            this.toolTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolVentana});
            this.toolTitulo.Location = new System.Drawing.Point(0, 0);
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(602, 27);
            this.toolTitulo.TabIndex = 2;
            this.toolTitulo.Text = "toolAcciones";
            // 
            // toolVentana
            // 
            this.toolVentana.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolVentana.Name = "toolVentana";
            this.toolVentana.Size = new System.Drawing.Size(204, 24);
            this.toolVentana.Text = "Nuevo complemento";
            // 
            // toolAcciones
            // 
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGuardar,
            this.toolCerrar});
            this.toolAcciones.Location = new System.Drawing.Point(0, 27);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(602, 25);
            this.toolAcciones.TabIndex = 3;
            this.toolAcciones.Text = "toolEmpresa";
            // 
            // toolGuardar
            // 
            this.toolGuardar.Image = ((System.Drawing.Image)(resources.GetObject("toolGuardar.Image")));
            this.toolGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGuardar.Name = "toolGuardar";
            this.toolGuardar.Size = new System.Drawing.Size(69, 22);
            this.toolGuardar.Text = "Guardar";
            this.toolGuardar.Click += new System.EventHandler(this.toolGuardar_Click);
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
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.SystemColors.Control;
            this.Label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.Label16.Location = new System.Drawing.Point(12, 119);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(80, 18);
            this.Label16.TabIndex = 209;
            this.Label16.Text = "Dirección";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(100, 309);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 13);
            this.label13.TabIndex = 225;
            this.label13.Text = "Pais:";
            // 
            // txtPais
            // 
            this.txtPais.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPais.Location = new System.Drawing.Point(137, 306);
            this.txtPais.Name = "txtPais";
            this.txtPais.Size = new System.Drawing.Size(130, 20);
            this.txtPais.TabIndex = 7;
            // 
            // txtMunicipio
            // 
            this.txtMunicipio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMunicipio.Location = new System.Drawing.Point(137, 254);
            this.txtMunicipio.Name = "txtMunicipio";
            this.txtMunicipio.Size = new System.Drawing.Size(130, 20);
            this.txtMunicipio.TabIndex = 5;
            // 
            // txtEstado
            // 
            this.txtEstado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEstado.Location = new System.Drawing.Point(137, 280);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Size = new System.Drawing.Size(130, 20);
            this.txtEstado.TabIndex = 6;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(302, 257);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(30, 13);
            this.Label10.TabIndex = 224;
            this.Label10.Text = "C.P.:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(97, 156);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(33, 13);
            this.Label2.TabIndex = 218;
            this.Label2.Text = "Calle:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(65, 181);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(65, 13);
            this.Label4.TabIndex = 220;
            this.Label4.Text = "No. Exterior:";
            // 
            // txtCP
            // 
            this.txtCP.Location = new System.Drawing.Point(335, 254);
            this.txtCP.Mask = "00000";
            this.txtCP.Name = "txtCP";
            this.txtCP.Size = new System.Drawing.Size(40, 20);
            this.txtCP.TabIndex = 8;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(68, 206);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(62, 13);
            this.Label3.TabIndex = 219;
            this.Label3.Text = "No. Interior:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(85, 231);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(45, 13);
            this.Label5.TabIndex = 221;
            this.Label5.Text = "Colonia:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(11, 257);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(119, 13);
            this.Label6.TabIndex = 222;
            this.Label6.Text = "Municipio o delegación:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(87, 283);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(43, 13);
            this.Label7.TabIndex = 223;
            this.Label7.Text = "Estado:";
            // 
            // txtColonia
            // 
            this.txtColonia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtColonia.Location = new System.Drawing.Point(137, 228);
            this.txtColonia.Name = "txtColonia";
            this.txtColonia.Size = new System.Drawing.Size(426, 20);
            this.txtColonia.TabIndex = 4;
            // 
            // txtInterior
            // 
            this.txtInterior.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtInterior.Location = new System.Drawing.Point(137, 203);
            this.txtInterior.Name = "txtInterior";
            this.txtInterior.Size = new System.Drawing.Size(67, 20);
            this.txtInterior.TabIndex = 3;
            // 
            // txtCalle
            // 
            this.txtCalle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCalle.Location = new System.Drawing.Point(137, 153);
            this.txtCalle.Name = "txtCalle";
            this.txtCalle.Size = new System.Drawing.Size(426, 20);
            this.txtCalle.TabIndex = 1;
            // 
            // txtExterior
            // 
            this.txtExterior.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtExterior.Location = new System.Drawing.Point(137, 178);
            this.txtExterior.Name = "txtExterior";
            this.txtExterior.Size = new System.Drawing.Size(67, 20);
            this.txtExterior.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 18);
            this.label1.TabIndex = 226;
            this.label1.Text = "Complementos";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(80, 399);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 227;
            this.label8.Text = "Contrato:";
            // 
            // cmbContrato
            // 
            this.cmbContrato.FormattingEnabled = true;
            this.cmbContrato.Location = new System.Drawing.Point(137, 396);
            this.cmbContrato.Name = "cmbContrato";
            this.cmbContrato.Size = new System.Drawing.Size(130, 21);
            this.cmbContrato.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(82, 426);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 229;
            this.label9.Text = "Jornada:";
            // 
            // cmbJornada
            // 
            this.cmbJornada.FormattingEnabled = true;
            this.cmbJornada.Location = new System.Drawing.Point(137, 423);
            this.cmbJornada.Name = "cmbJornada";
            this.cmbJornada.Size = new System.Drawing.Size(130, 21);
            this.cmbJornada.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(66, 453);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 13);
            this.label14.TabIndex = 235;
            this.label14.Text = "Estado civil:";
            // 
            // cmbEstadoCivil
            // 
            this.cmbEstadoCivil.FormattingEnabled = true;
            this.cmbEstadoCivil.Location = new System.Drawing.Point(137, 450);
            this.cmbEstadoCivil.Name = "cmbEstadoCivil";
            this.cmbEstadoCivil.Size = new System.Drawing.Size(130, 21);
            this.cmbEstadoCivil.TabIndex = 11;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(96, 480);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 13);
            this.label15.TabIndex = 237;
            this.label15.Text = "Sexo:";
            // 
            // cmbSexo
            // 
            this.cmbSexo.FormattingEnabled = true;
            this.cmbSexo.Location = new System.Drawing.Point(137, 477);
            this.cmbSexo.Name = "cmbSexo";
            this.cmbSexo.Size = new System.Drawing.Size(130, 21);
            this.cmbSexo.TabIndex = 12;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(66, 507);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 13);
            this.label17.TabIndex = 239;
            this.label17.Text = "Escolaridad:";
            // 
            // cmbEscolaridad
            // 
            this.cmbEscolaridad.FormattingEnabled = true;
            this.cmbEscolaridad.Location = new System.Drawing.Point(137, 504);
            this.cmbEscolaridad.Name = "cmbEscolaridad";
            this.cmbEscolaridad.Size = new System.Drawing.Size(130, 21);
            this.cmbEscolaridad.TabIndex = 13;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(311, 399);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 13);
            this.label20.TabIndex = 245;
            this.label20.Text = "Clinica:";
            // 
            // txtClinica
            // 
            this.txtClinica.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClinica.Location = new System.Drawing.Point(358, 397);
            this.txtClinica.Name = "txtClinica";
            this.txtClinica.Size = new System.Drawing.Size(42, 20);
            this.txtClinica.TabIndex = 14;
            // 
            // txtNacionalidad
            // 
            this.txtNacionalidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNacionalidad.Location = new System.Drawing.Point(137, 531);
            this.txtNacionalidad.Name = "txtNacionalidad";
            this.txtNacionalidad.Size = new System.Drawing.Size(130, 20);
            this.txtNacionalidad.TabIndex = 15;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(58, 534);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(72, 13);
            this.label21.TabIndex = 248;
            this.label21.Text = "Nacionalidad:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(49, 586);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(81, 13);
            this.label22.TabIndex = 249;
            this.label22.Text = "Observaciones:";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservaciones.Location = new System.Drawing.Point(143, 583);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(445, 134);
            this.txtObservaciones.TabIndex = 16;
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.Location = new System.Drawing.Point(112, 78);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(183, 20);
            this.lblEmpleado.TabIndex = 251;
            this.lblEmpleado.Text = "Nombre del empleado";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(12, 78);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(94, 20);
            this.label23.TabIndex = 250;
            this.label23.Text = "Empleado:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(271, 425);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 252;
            this.label11.Text = "Tel. Empresa 1:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(280, 529);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 253;
            this.label12.Text = "Tel. Personal:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(311, 557);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 13);
            this.label18.TabIndex = 254;
            this.label18.Text = "Correo:";
            // 
            // mTelEmpresa
            // 
            this.mTelEmpresa.Location = new System.Drawing.Point(358, 424);
            this.mTelEmpresa.Mask = "(999)-999-9999";
            this.mTelEmpresa.Name = "mTelEmpresa";
            this.mTelEmpresa.Size = new System.Drawing.Size(81, 20);
            this.mTelEmpresa.TabIndex = 255;
            // 
            // mTelPersonal
            // 
            this.mTelPersonal.Location = new System.Drawing.Point(358, 531);
            this.mTelPersonal.Mask = "(999)-999-9999";
            this.mTelPersonal.Name = "mTelPersonal";
            this.mTelPersonal.Size = new System.Drawing.Size(81, 20);
            this.mTelPersonal.TabIndex = 258;
            // 
            // txtExtension
            // 
            this.txtExtension.Location = new System.Drawing.Point(445, 424);
            this.txtExtension.Name = "txtExtension";
            this.txtExtension.Size = new System.Drawing.Size(43, 20);
            this.txtExtension.TabIndex = 260;
            this.txtExtension.Text = "00000";
            // 
            // txtCorreo
            // 
            this.txtCorreo.Location = new System.Drawing.Point(358, 557);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(230, 20);
            this.txtCorreo.TabIndex = 261;
            this.txtCorreo.Text = "user@dominio.com";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(280, 503);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(72, 13);
            this.label19.TabIndex = 262;
            this.label19.Text = "Cel. Empresa:";
            // 
            // mCelEmpresa
            // 
            this.mCelEmpresa.Location = new System.Drawing.Point(358, 504);
            this.mCelEmpresa.Mask = "(999)-999-9999";
            this.mCelEmpresa.Name = "mCelEmpresa";
            this.mCelEmpresa.Size = new System.Drawing.Size(81, 20);
            this.mCelEmpresa.TabIndex = 263;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(271, 451);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(81, 13);
            this.label24.TabIndex = 264;
            this.label24.Text = "Tel. Empresa 2:";
            // 
            // mTelEmpresa2
            // 
            this.mTelEmpresa2.Location = new System.Drawing.Point(358, 451);
            this.mTelEmpresa2.Mask = "(999)-000-0000";
            this.mTelEmpresa2.Name = "mTelEmpresa2";
            this.mTelEmpresa2.Size = new System.Drawing.Size(81, 20);
            this.mTelEmpresa2.TabIndex = 265;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(271, 477);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(81, 13);
            this.label25.TabIndex = 266;
            this.label25.Text = "Tel. Empresa 3:";
            // 
            // mTelEmpresa3
            // 
            this.mTelEmpresa3.Location = new System.Drawing.Point(358, 478);
            this.mTelEmpresa3.Mask = "(000)-000-0000";
            this.mTelEmpresa3.Name = "mTelEmpresa3";
            this.mTelEmpresa3.Size = new System.Drawing.Size(81, 20);
            this.mTelEmpresa3.TabIndex = 267;
            // 
            // frmComplementos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 766);
            this.Controls.Add(this.mTelEmpresa3);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.mTelEmpresa2);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.mCelEmpresa);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.txtExtension);
            this.Controls.Add(this.mTelPersonal);
            this.Controls.Add(this.mTelEmpresa);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblEmpleado);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtNacionalidad);
            this.Controls.Add(this.txtClinica);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cmbEscolaridad);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cmbSexo);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cmbEstadoCivil);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbJornada);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbContrato);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtPais);
            this.Controls.Add(this.txtMunicipio);
            this.Controls.Add(this.txtEstado);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.txtCP);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.txtColonia);
            this.Controls.Add(this.txtInterior);
            this.Controls.Add(this.txtCalle);
            this.Controls.Add(this.txtExterior);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.toolAcciones);
            this.Controls.Add(this.toolTitulo);
            this.Name = "frmComplementos";
            this.Text = "Complementos";
            this.Load += new System.EventHandler(this.frmComplementos_Load);
            this.toolTitulo.ResumeLayout(false);
            this.toolTitulo.PerformLayout();
            this.toolAcciones.ResumeLayout(false);
            this.toolAcciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolTitulo;
        internal System.Windows.Forms.ToolStripLabel toolVentana;
        internal System.Windows.Forms.ToolStrip toolAcciones;
        internal System.Windows.Forms.ToolStripButton toolGuardar;
        private System.Windows.Forms.ToolStripButton toolCerrar;
        internal System.Windows.Forms.Label Label16;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPais;
        internal System.Windows.Forms.TextBox txtMunicipio;
        internal System.Windows.Forms.TextBox txtEstado;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.MaskedTextBox txtCP;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox txtColonia;
        internal System.Windows.Forms.TextBox txtInterior;
        internal System.Windows.Forms.TextBox txtCalle;
        internal System.Windows.Forms.TextBox txtExterior;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbContrato;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbJornada;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbEstadoCivil;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbSexo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbEscolaridad;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtClinica;
        private System.Windows.Forms.TextBox txtNacionalidad;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.MaskedTextBox mTelEmpresa;
        private System.Windows.Forms.MaskedTextBox mTelPersonal;
        private System.Windows.Forms.TextBox txtExtension;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.MaskedTextBox mCelEmpresa;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.MaskedTextBox mTelEmpresa2;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.MaskedTextBox mTelEmpresa3;
    }
}