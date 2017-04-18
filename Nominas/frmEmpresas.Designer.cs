namespace Nominas
{
    partial class frmEmpresas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmpresas));
            this.toolEmpresa = new System.Windows.Forms.ToolStrip();
            this.toolGuardarCerrar = new System.Windows.Forms.ToolStripButton();
            this.toolGuardarNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolTitulo = new System.Windows.Forms.ToolStripLabel();
            this.txtRfc = new System.Windows.Forms.TextBox();
            this.txtRegistroPatronal = new System.Windows.Forms.MaskedTextBox();
            this.txtDigitoVerificador = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
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
            this.txtPais = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnVer = new System.Windows.Forms.Button();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtCertificado = new System.Windows.Forms.TextBox();
            this.txtLlave = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtNoCertificado = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.dtpVigencia = new System.Windows.Forms.DateTimePicker();
            this.cmbPago = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtDias = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.tabEmpresas = new System.Windows.Forms.TabControl();
            this.tabEmpresa = new System.Windows.Forms.TabPage();
            this.cmbRegimenFiscal = new System.Windows.Forms.ComboBox();
            this.chkObraCivil = new System.Windows.Forms.CheckBox();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtRepresentante = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.tabDomicilio = new System.Windows.Forms.TabPage();
            this.tabPeriodo = new System.Windows.Forms.TabPage();
            this.tabTimbrado = new System.Windows.Forms.TabPage();
            this.toolEmpresa.SuspendLayout();
            this.toolAcciones.SuspendLayout();
            this.tabEmpresas.SuspendLayout();
            this.tabEmpresa.SuspendLayout();
            this.tabDomicilio.SuspendLayout();
            this.tabPeriodo.SuspendLayout();
            this.tabTimbrado.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolEmpresa
            // 
            this.toolEmpresa.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGuardarCerrar,
            this.toolGuardarNuevo,
            this.toolCerrar});
            this.toolEmpresa.Location = new System.Drawing.Point(0, 27);
            this.toolEmpresa.Name = "toolEmpresa";
            this.toolEmpresa.Size = new System.Drawing.Size(624, 25);
            this.toolEmpresa.TabIndex = 0;
            this.toolEmpresa.Text = "toolEmpresa";
            // 
            // toolGuardarCerrar
            // 
            this.toolGuardarCerrar.Image = ((System.Drawing.Image)(resources.GetObject("toolGuardarCerrar.Image")));
            this.toolGuardarCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGuardarCerrar.Name = "toolGuardarCerrar";
            this.toolGuardarCerrar.Size = new System.Drawing.Size(111, 22);
            this.toolGuardarCerrar.Text = "Guardar y cerrar";
            this.toolGuardarCerrar.Click += new System.EventHandler(this.toolGuardarCerrar_Click);
            // 
            // toolGuardarNuevo
            // 
            this.toolGuardarNuevo.Image = ((System.Drawing.Image)(resources.GetObject("toolGuardarNuevo.Image")));
            this.toolGuardarNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGuardarNuevo.Name = "toolGuardarNuevo";
            this.toolGuardarNuevo.Size = new System.Drawing.Size(114, 22);
            this.toolGuardarNuevo.Text = "Guardar y nuevo";
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
            // toolAcciones
            // 
            this.toolAcciones.BackColor = System.Drawing.Color.DarkGray;
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTitulo});
            this.toolAcciones.Location = new System.Drawing.Point(0, 0);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(624, 27);
            this.toolAcciones.TabIndex = 0;
            this.toolAcciones.Text = "toolAcciones";
            // 
            // toolTitulo
            // 
            this.toolTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(159, 24);
            this.toolTitulo.Text = "Nueva Empresa";
            // 
            // txtRfc
            // 
            this.txtRfc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRfc.Location = new System.Drawing.Point(113, 214);
            this.txtRfc.Name = "txtRfc";
            this.txtRfc.Size = new System.Drawing.Size(97, 20);
            this.txtRfc.TabIndex = 12;
            this.txtRfc.Leave += new System.EventHandler(this.txtRfc_Leave);
            // 
            // txtRegistroPatronal
            // 
            this.txtRegistroPatronal.Location = new System.Drawing.Point(113, 240);
            this.txtRegistroPatronal.Mask = "AAAAAAAAAA";
            this.txtRegistroPatronal.Name = "txtRegistroPatronal";
            this.txtRegistroPatronal.Size = new System.Drawing.Size(96, 20);
            this.txtRegistroPatronal.TabIndex = 13;
            // 
            // txtDigitoVerificador
            // 
            this.txtDigitoVerificador.Location = new System.Drawing.Point(349, 240);
            this.txtDigitoVerificador.Name = "txtDigitoVerificador";
            this.txtDigitoVerificador.Size = new System.Drawing.Size(21, 20);
            this.txtDigitoVerificador.TabIndex = 14;
            this.txtDigitoVerificador.Text = "0";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(12, 243);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(91, 13);
            this.Label9.TabIndex = 120;
            this.Label9.Text = "Registro Patronal:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(63, 217);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(40, 13);
            this.Label8.TabIndex = 115;
            this.Label8.Text = "R.F.C.:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(253, 243);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(90, 13);
            this.Label11.TabIndex = 124;
            this.Label11.Text = "Digito Verificador:";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(6, 15);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(80, 18);
            this.Label14.TabIndex = 161;
            this.Label14.Text = "Dirección";
            // 
            // txtMunicipio
            // 
            this.txtMunicipio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMunicipio.Location = new System.Drawing.Point(130, 150);
            this.txtMunicipio.Name = "txtMunicipio";
            this.txtMunicipio.Size = new System.Drawing.Size(130, 20);
            this.txtMunicipio.TabIndex = 8;
            // 
            // txtEstado
            // 
            this.txtEstado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEstado.Location = new System.Drawing.Point(130, 176);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Size = new System.Drawing.Size(130, 20);
            this.txtEstado.TabIndex = 9;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(295, 153);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(30, 13);
            this.Label10.TabIndex = 158;
            this.Label10.Text = "C.P.:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(90, 52);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(33, 13);
            this.Label2.TabIndex = 147;
            this.Label2.Text = "Calle:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(58, 77);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(65, 13);
            this.Label4.TabIndex = 149;
            this.Label4.Text = "No. Exterior:";
            // 
            // txtCP
            // 
            this.txtCP.Location = new System.Drawing.Point(328, 150);
            this.txtCP.Mask = "00000";
            this.txtCP.Name = "txtCP";
            this.txtCP.Size = new System.Drawing.Size(49, 20);
            this.txtCP.TabIndex = 11;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(61, 102);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(62, 13);
            this.Label3.TabIndex = 148;
            this.Label3.Text = "No. Interior:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(78, 127);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(45, 13);
            this.Label5.TabIndex = 150;
            this.Label5.Text = "Colonia:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(4, 153);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(119, 13);
            this.Label6.TabIndex = 151;
            this.Label6.Text = "Municipio o delegación:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(80, 179);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(43, 13);
            this.Label7.TabIndex = 152;
            this.Label7.Text = "Estado:";
            // 
            // txtColonia
            // 
            this.txtColonia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtColonia.Location = new System.Drawing.Point(130, 124);
            this.txtColonia.Name = "txtColonia";
            this.txtColonia.Size = new System.Drawing.Size(426, 20);
            this.txtColonia.TabIndex = 7;
            // 
            // txtInterior
            // 
            this.txtInterior.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtInterior.Location = new System.Drawing.Point(130, 99);
            this.txtInterior.Name = "txtInterior";
            this.txtInterior.Size = new System.Drawing.Size(67, 20);
            this.txtInterior.TabIndex = 6;
            // 
            // txtCalle
            // 
            this.txtCalle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCalle.Location = new System.Drawing.Point(130, 49);
            this.txtCalle.Name = "txtCalle";
            this.txtCalle.Size = new System.Drawing.Size(426, 20);
            this.txtCalle.TabIndex = 4;
            // 
            // txtExterior
            // 
            this.txtExterior.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtExterior.Location = new System.Drawing.Point(130, 74);
            this.txtExterior.Name = "txtExterior";
            this.txtExterior.Size = new System.Drawing.Size(67, 20);
            this.txtExterior.TabIndex = 5;
            // 
            // txtPais
            // 
            this.txtPais.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPais.Location = new System.Drawing.Point(130, 202);
            this.txtPais.Name = "txtPais";
            this.txtPais.Size = new System.Drawing.Size(130, 20);
            this.txtPais.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(93, 205);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 13);
            this.label13.TabIndex = 163;
            this.label13.Text = "Pais:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(69, 269);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(34, 13);
            this.label17.TabIndex = 164;
            this.label17.Text = "Logo:";
            // 
            // btnVer
            // 
            this.btnVer.Image = ((System.Drawing.Image)(resources.GetObject("btnVer.Image")));
            this.btnVer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVer.Location = new System.Drawing.Point(113, 264);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(67, 35);
            this.btnVer.TabIndex = 15;
            this.btnVer.Text = "Ver";
            this.btnVer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVer.UseVisualStyleBackColor = true;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Image = ((System.Drawing.Image)(resources.GetObject("btnAsignar.Image")));
            this.btnAsignar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAsignar.Location = new System.Drawing.Point(186, 264);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(67, 35);
            this.btnAsignar.TabIndex = 16;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(6, 12);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 18);
            this.label18.TabIndex = 169;
            this.label18.Text = "Timbrado";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(35, 42);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(60, 13);
            this.label19.TabIndex = 170;
            this.label19.Text = "Certificado:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(57, 133);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(36, 13);
            this.label20.TabIndex = 171;
            this.label20.Text = "Llave:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(39, 250);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(56, 13);
            this.label21.TabIndex = 172;
            this.label21.Text = "Password:";
            // 
            // txtCertificado
            // 
            this.txtCertificado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCertificado.Location = new System.Drawing.Point(101, 39);
            this.txtCertificado.Multiline = true;
            this.txtCertificado.Name = "txtCertificado";
            this.txtCertificado.Size = new System.Drawing.Size(380, 85);
            this.txtCertificado.TabIndex = 17;
            // 
            // txtLlave
            // 
            this.txtLlave.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLlave.Location = new System.Drawing.Point(101, 130);
            this.txtLlave.Multiline = true;
            this.txtLlave.Name = "txtLlave";
            this.txtLlave.Size = new System.Drawing.Size(380, 85);
            this.txtLlave.TabIndex = 18;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(101, 247);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(380, 20);
            this.txtPassword.TabIndex = 20;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(16, 224);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(80, 13);
            this.label23.TabIndex = 180;
            this.label23.Text = "No. Certificado:";
            // 
            // txtNoCertificado
            // 
            this.txtNoCertificado.Location = new System.Drawing.Point(102, 221);
            this.txtNoCertificado.Name = "txtNoCertificado";
            this.txtNoCertificado.Size = new System.Drawing.Size(379, 20);
            this.txtNoCertificado.TabIndex = 19;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(44, 279);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(51, 13);
            this.label24.TabIndex = 182;
            this.label24.Text = "Vigencia:";
            // 
            // dtpVigencia
            // 
            this.dtpVigencia.Location = new System.Drawing.Point(101, 275);
            this.dtpVigencia.Name = "dtpVigencia";
            this.dtpVigencia.Size = new System.Drawing.Size(213, 20);
            this.dtpVigencia.TabIndex = 21;
            // 
            // cmbPago
            // 
            this.cmbPago.FormattingEnabled = true;
            this.cmbPago.Items.AddRange(new object[] {
            "SEMANAL",
            "QUINCENAL"});
            this.cmbPago.Location = new System.Drawing.Point(97, 49);
            this.cmbPago.Name = "cmbPago";
            this.cmbPago.Size = new System.Drawing.Size(100, 21);
            this.cmbPago.TabIndex = 183;
            this.cmbPago.SelectedIndexChanged += new System.EventHandler(this.cmbPago_SelectedIndexChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.label25.Location = new System.Drawing.Point(6, 16);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(132, 18);
            this.label25.TabIndex = 187;
            this.label25.Text = "Periodo de pago";
            // 
            // txtDias
            // 
            this.txtDias.Enabled = false;
            this.txtDias.Location = new System.Drawing.Point(97, 76);
            this.txtDias.Name = "txtDias";
            this.txtDias.Size = new System.Drawing.Size(41, 20);
            this.txtDias.TabIndex = 184;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(5, 79);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(86, 13);
            this.label26.TabIndex = 186;
            this.label26.Text = "Dias del periodo:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(56, 53);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(35, 13);
            this.label27.TabIndex = 185;
            this.label27.Text = "Pago:";
            // 
            // tabEmpresas
            // 
            this.tabEmpresas.Controls.Add(this.tabEmpresa);
            this.tabEmpresas.Controls.Add(this.tabDomicilio);
            this.tabEmpresas.Controls.Add(this.tabPeriodo);
            this.tabEmpresas.Controls.Add(this.tabTimbrado);
            this.tabEmpresas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabEmpresas.Location = new System.Drawing.Point(0, 52);
            this.tabEmpresas.Name = "tabEmpresas";
            this.tabEmpresas.SelectedIndex = 0;
            this.tabEmpresas.Size = new System.Drawing.Size(624, 379);
            this.tabEmpresas.TabIndex = 188;
            // 
            // tabEmpresa
            // 
            this.tabEmpresa.Controls.Add(this.cmbRegimenFiscal);
            this.tabEmpresa.Controls.Add(this.chkObraCivil);
            this.tabEmpresa.Controls.Add(this.txtObservacion);
            this.tabEmpresa.Controls.Add(this.label28);
            this.tabEmpresa.Controls.Add(this.Label15);
            this.tabEmpresa.Controls.Add(this.Label12);
            this.tabEmpresa.Controls.Add(this.label22);
            this.tabEmpresa.Controls.Add(this.txtRepresentante);
            this.tabEmpresa.Controls.Add(this.Label16);
            this.tabEmpresa.Controls.Add(this.txtNombre);
            this.tabEmpresa.Controls.Add(this.Label1);
            this.tabEmpresa.Controls.Add(this.btnAsignar);
            this.tabEmpresa.Controls.Add(this.Label8);
            this.tabEmpresa.Controls.Add(this.Label9);
            this.tabEmpresa.Controls.Add(this.Label11);
            this.tabEmpresa.Controls.Add(this.txtDigitoVerificador);
            this.tabEmpresa.Controls.Add(this.txtRegistroPatronal);
            this.tabEmpresa.Controls.Add(this.txtRfc);
            this.tabEmpresa.Controls.Add(this.label17);
            this.tabEmpresa.Controls.Add(this.btnVer);
            this.tabEmpresa.Location = new System.Drawing.Point(4, 22);
            this.tabEmpresa.Name = "tabEmpresa";
            this.tabEmpresa.Padding = new System.Windows.Forms.Padding(3);
            this.tabEmpresa.Size = new System.Drawing.Size(616, 353);
            this.tabEmpresa.TabIndex = 0;
            this.tabEmpresa.Text = "Empresa";
            this.tabEmpresa.UseVisualStyleBackColor = true;
            // 
            // cmbRegimenFiscal
            // 
            this.cmbRegimenFiscal.FormattingEnabled = true;
            this.cmbRegimenFiscal.Location = new System.Drawing.Point(113, 108);
            this.cmbRegimenFiscal.Name = "cmbRegimenFiscal";
            this.cmbRegimenFiscal.Size = new System.Drawing.Size(425, 21);
            this.cmbRegimenFiscal.TabIndex = 193;
            // 
            // chkObraCivil
            // 
            this.chkObraCivil.AutoSize = true;
            this.chkObraCivil.Location = new System.Drawing.Point(113, 160);
            this.chkObraCivil.Name = "chkObraCivil";
            this.chkObraCivil.Size = new System.Drawing.Size(71, 17);
            this.chkObraCivil.TabIndex = 192;
            this.chkObraCivil.Text = "Obra Civil";
            this.chkObraCivil.UseVisualStyleBackColor = true;
            // 
            // txtObservacion
            // 
            this.txtObservacion.Location = new System.Drawing.Point(113, 134);
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(425, 20);
            this.txtObservacion.TabIndex = 191;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(37, 137);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(70, 13);
            this.label28.TabIndex = 190;
            this.label28.Text = "Observación:";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(3, 173);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(81, 18);
            this.Label15.TabIndex = 189;
            this.Label15.Text = "Registros";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(6, 20);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(138, 18);
            this.Label12.TabIndex = 180;
            this.Label12.Text = "Nombre empresa";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(55, 111);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(52, 13);
            this.label22.TabIndex = 179;
            this.label22.Text = "Regimen:";
            // 
            // txtRepresentante
            // 
            this.txtRepresentante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRepresentante.Location = new System.Drawing.Point(113, 82);
            this.txtRepresentante.Name = "txtRepresentante";
            this.txtRepresentante.Size = new System.Drawing.Size(425, 20);
            this.txtRepresentante.TabIndex = 175;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(30, 85);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(80, 13);
            this.Label16.TabIndex = 178;
            this.Label16.Text = "Representante:";
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Location = new System.Drawing.Point(113, 56);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(426, 20);
            this.txtNombre.TabIndex = 174;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(17, 59);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(93, 13);
            this.Label1.TabIndex = 177;
            this.Label1.Text = "Nombre completo:";
            // 
            // tabDomicilio
            // 
            this.tabDomicilio.Controls.Add(this.Label14);
            this.tabDomicilio.Controls.Add(this.txtExterior);
            this.tabDomicilio.Controls.Add(this.txtCalle);
            this.tabDomicilio.Controls.Add(this.txtInterior);
            this.tabDomicilio.Controls.Add(this.txtColonia);
            this.tabDomicilio.Controls.Add(this.Label7);
            this.tabDomicilio.Controls.Add(this.Label6);
            this.tabDomicilio.Controls.Add(this.Label5);
            this.tabDomicilio.Controls.Add(this.Label3);
            this.tabDomicilio.Controls.Add(this.txtCP);
            this.tabDomicilio.Controls.Add(this.Label4);
            this.tabDomicilio.Controls.Add(this.Label2);
            this.tabDomicilio.Controls.Add(this.Label10);
            this.tabDomicilio.Controls.Add(this.txtEstado);
            this.tabDomicilio.Controls.Add(this.txtMunicipio);
            this.tabDomicilio.Controls.Add(this.txtPais);
            this.tabDomicilio.Controls.Add(this.label13);
            this.tabDomicilio.Location = new System.Drawing.Point(4, 22);
            this.tabDomicilio.Name = "tabDomicilio";
            this.tabDomicilio.Padding = new System.Windows.Forms.Padding(3);
            this.tabDomicilio.Size = new System.Drawing.Size(616, 353);
            this.tabDomicilio.TabIndex = 1;
            this.tabDomicilio.Text = "Domicilio";
            this.tabDomicilio.UseVisualStyleBackColor = true;
            // 
            // tabPeriodo
            // 
            this.tabPeriodo.Controls.Add(this.cmbPago);
            this.tabPeriodo.Controls.Add(this.label27);
            this.tabPeriodo.Controls.Add(this.label25);
            this.tabPeriodo.Controls.Add(this.label26);
            this.tabPeriodo.Controls.Add(this.txtDias);
            this.tabPeriodo.Location = new System.Drawing.Point(4, 22);
            this.tabPeriodo.Name = "tabPeriodo";
            this.tabPeriodo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPeriodo.Size = new System.Drawing.Size(616, 353);
            this.tabPeriodo.TabIndex = 2;
            this.tabPeriodo.Text = "Periodo";
            this.tabPeriodo.UseVisualStyleBackColor = true;
            // 
            // tabTimbrado
            // 
            this.tabTimbrado.Controls.Add(this.label18);
            this.tabTimbrado.Controls.Add(this.label19);
            this.tabTimbrado.Controls.Add(this.label20);
            this.tabTimbrado.Controls.Add(this.dtpVigencia);
            this.tabTimbrado.Controls.Add(this.label21);
            this.tabTimbrado.Controls.Add(this.label24);
            this.tabTimbrado.Controls.Add(this.txtCertificado);
            this.tabTimbrado.Controls.Add(this.txtNoCertificado);
            this.tabTimbrado.Controls.Add(this.txtLlave);
            this.tabTimbrado.Controls.Add(this.label23);
            this.tabTimbrado.Controls.Add(this.txtPassword);
            this.tabTimbrado.Location = new System.Drawing.Point(4, 22);
            this.tabTimbrado.Name = "tabTimbrado";
            this.tabTimbrado.Padding = new System.Windows.Forms.Padding(3);
            this.tabTimbrado.Size = new System.Drawing.Size(616, 353);
            this.tabTimbrado.TabIndex = 3;
            this.tabTimbrado.Text = "Timbrado";
            this.tabTimbrado.UseVisualStyleBackColor = true;
            // 
            // frmEmpresas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 431);
            this.Controls.Add(this.tabEmpresas);
            this.Controls.Add(this.toolEmpresa);
            this.Controls.Add(this.toolAcciones);
            this.Name = "frmEmpresas";
            this.Text = "Empresa";
            this.Load += new System.EventHandler(this.frmEmpresas_Load);
            this.toolEmpresa.ResumeLayout(false);
            this.toolEmpresa.PerformLayout();
            this.toolAcciones.ResumeLayout(false);
            this.toolAcciones.PerformLayout();
            this.tabEmpresas.ResumeLayout(false);
            this.tabEmpresa.ResumeLayout(false);
            this.tabEmpresa.PerformLayout();
            this.tabDomicilio.ResumeLayout(false);
            this.tabDomicilio.PerformLayout();
            this.tabPeriodo.ResumeLayout(false);
            this.tabPeriodo.PerformLayout();
            this.tabTimbrado.ResumeLayout(false);
            this.tabTimbrado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolEmpresa;
        internal System.Windows.Forms.ToolStrip toolAcciones;
        internal System.Windows.Forms.TextBox txtRfc;
        internal System.Windows.Forms.MaskedTextBox txtRegistroPatronal;
        internal System.Windows.Forms.TextBox txtDigitoVerificador;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label14;
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
        private System.Windows.Forms.TextBox txtPais;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Button btnAsignar;
        internal System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtCertificado;
        private System.Windows.Forms.TextBox txtLlave;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtNoCertificado;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.DateTimePicker dtpVigencia;
        private System.Windows.Forms.ToolStripLabel toolTitulo;
        private System.Windows.Forms.ToolStripButton toolGuardarCerrar;
        private System.Windows.Forms.ToolStripButton toolGuardarNuevo;
        private System.Windows.Forms.ToolStripButton toolCerrar;
        private System.Windows.Forms.ComboBox cmbPago;
        internal System.Windows.Forms.Label label25;
        internal System.Windows.Forms.TextBox txtDias;
        internal System.Windows.Forms.Label label26;
        internal System.Windows.Forms.Label label27;
        private System.Windows.Forms.TabControl tabEmpresas;
        private System.Windows.Forms.TabPage tabEmpresa;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.Label Label12;
        private System.Windows.Forms.Label label22;
        internal System.Windows.Forms.TextBox txtRepresentante;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.TextBox txtNombre;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TabPage tabDomicilio;
        private System.Windows.Forms.TabPage tabPeriodo;
        private System.Windows.Forms.TabPage tabTimbrado;
        private System.Windows.Forms.TextBox txtObservacion;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.CheckBox chkObraCivil;
        private System.Windows.Forms.ComboBox cmbRegimenFiscal;
    }
}