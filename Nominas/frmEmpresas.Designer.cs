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
            this.txtRepresentante = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.toolEmpresa = new System.Windows.Forms.ToolStrip();
            this.toolGuardarCerrar = new System.Windows.Forms.ToolStripButton();
            this.toolGuardarNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolTitulo = new System.Windows.Forms.ToolStripLabel();
            this.txtRfc = new System.Windows.Forms.TextBox();
            this.txtRegistroPatronal = new System.Windows.Forms.MaskedTextBox();
            this.txtDigitoVerificador = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
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
            this.label22 = new System.Windows.Forms.Label();
            this.txtCertificado = new System.Windows.Forms.TextBox();
            this.txtLlave = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtRegimen = new System.Windows.Forms.TextBox();
            this.btnExaminarCertificado = new System.Windows.Forms.Button();
            this.btnExaminarLlave = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.txtNoCertificado = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.dtpVigencia = new System.Windows.Forms.DateTimePicker();
            this.Panel2.SuspendLayout();
            this.toolEmpresa.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.toolAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRepresentante
            // 
            this.txtRepresentante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRepresentante.Location = new System.Drawing.Point(135, 144);
            this.txtRepresentante.Name = "txtRepresentante";
            this.txtRepresentante.Size = new System.Drawing.Size(425, 20);
            this.txtRepresentante.TabIndex = 2;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(52, 147);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(80, 13);
            this.Label16.TabIndex = 146;
            this.Label16.Text = "Representante:";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(11, 421);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(81, 18);
            this.Label15.TabIndex = 145;
            this.Label15.Text = "Registros";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(14, 72);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(138, 18);
            this.Label12.TabIndex = 143;
            this.Label12.Text = "Nombre empresa";
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.toolEmpresa);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel2.Location = new System.Drawing.Point(0, 28);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(632, 28);
            this.Panel2.TabIndex = 142;
            // 
            // toolEmpresa
            // 
            this.toolEmpresa.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGuardarCerrar,
            this.toolGuardarNuevo,
            this.toolCerrar});
            this.toolEmpresa.Location = new System.Drawing.Point(0, 0);
            this.toolEmpresa.Name = "toolEmpresa";
            this.toolEmpresa.Size = new System.Drawing.Size(632, 25);
            this.toolEmpresa.TabIndex = 0;
            this.toolEmpresa.Text = "toolEmpresa";
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
            // Panel1
            // 
            this.Panel1.Controls.Add(this.toolAcciones);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(632, 28);
            this.Panel1.TabIndex = 141;
            // 
            // toolAcciones
            // 
            this.toolAcciones.BackColor = System.Drawing.Color.DarkGray;
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTitulo});
            this.toolAcciones.Location = new System.Drawing.Point(0, 0);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(632, 27);
            this.toolAcciones.TabIndex = 0;
            this.toolAcciones.Text = "toolAcciones";
            // 
            // toolTitulo
            // 
            this.toolTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(157, 24);
            this.toolTitulo.Text = "Nueva empresa";
            // 
            // txtRfc
            // 
            this.txtRfc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRfc.Location = new System.Drawing.Point(135, 454);
            this.txtRfc.Name = "txtRfc";
            this.txtRfc.Size = new System.Drawing.Size(97, 20);
            this.txtRfc.TabIndex = 11;
            this.txtRfc.Leave += new System.EventHandler(this.txtRfc_Leave);
            // 
            // txtRegistroPatronal
            // 
            this.txtRegistroPatronal.Location = new System.Drawing.Point(136, 480);
            this.txtRegistroPatronal.Mask = "AAAAAAAAAA";
            this.txtRegistroPatronal.Name = "txtRegistroPatronal";
            this.txtRegistroPatronal.Size = new System.Drawing.Size(96, 20);
            this.txtRegistroPatronal.TabIndex = 12;
            // 
            // txtDigitoVerificador
            // 
            this.txtDigitoVerificador.Location = new System.Drawing.Point(374, 454);
            this.txtDigitoVerificador.Name = "txtDigitoVerificador";
            this.txtDigitoVerificador.Size = new System.Drawing.Size(21, 20);
            this.txtDigitoVerificador.TabIndex = 13;
            this.txtDigitoVerificador.Text = "0";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(38, 483);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(91, 13);
            this.Label9.TabIndex = 120;
            this.Label9.Text = "Registro Patronal:";
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Location = new System.Drawing.Point(134, 118);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(426, 20);
            this.txtNombre.TabIndex = 1;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(39, 121);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(93, 13);
            this.Label1.TabIndex = 119;
            this.Label1.Text = "Nombre completo:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(89, 457);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(40, 13);
            this.Label8.TabIndex = 115;
            this.Label8.Text = "R.F.C.:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(281, 457);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(90, 13);
            this.Label11.TabIndex = 124;
            this.Label11.Text = "Digito Verificador:";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(14, 204);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(80, 18);
            this.Label14.TabIndex = 161;
            this.Label14.Text = "Dirección";
            // 
            // txtMunicipio
            // 
            this.txtMunicipio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMunicipio.Location = new System.Drawing.Point(136, 331);
            this.txtMunicipio.Name = "txtMunicipio";
            this.txtMunicipio.Size = new System.Drawing.Size(130, 20);
            this.txtMunicipio.TabIndex = 7;
            // 
            // txtEstado
            // 
            this.txtEstado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEstado.Location = new System.Drawing.Point(136, 357);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Size = new System.Drawing.Size(130, 20);
            this.txtEstado.TabIndex = 8;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(301, 334);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(30, 13);
            this.Label10.TabIndex = 158;
            this.Label10.Text = "C.P.:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(96, 233);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(33, 13);
            this.Label2.TabIndex = 147;
            this.Label2.Text = "Calle:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(64, 258);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(65, 13);
            this.Label4.TabIndex = 149;
            this.Label4.Text = "No. Exterior:";
            // 
            // txtCP
            // 
            this.txtCP.Location = new System.Drawing.Point(334, 331);
            this.txtCP.Mask = "00000";
            this.txtCP.Name = "txtCP";
            this.txtCP.Size = new System.Drawing.Size(49, 20);
            this.txtCP.TabIndex = 10;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(67, 283);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(62, 13);
            this.Label3.TabIndex = 148;
            this.Label3.Text = "No. Interior:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(84, 308);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(45, 13);
            this.Label5.TabIndex = 150;
            this.Label5.Text = "Colonia:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(10, 334);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(119, 13);
            this.Label6.TabIndex = 151;
            this.Label6.Text = "Municipio o delegación:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(86, 360);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(43, 13);
            this.Label7.TabIndex = 152;
            this.Label7.Text = "Estado:";
            // 
            // txtColonia
            // 
            this.txtColonia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtColonia.Location = new System.Drawing.Point(136, 305);
            this.txtColonia.Name = "txtColonia";
            this.txtColonia.Size = new System.Drawing.Size(426, 20);
            this.txtColonia.TabIndex = 6;
            // 
            // txtInterior
            // 
            this.txtInterior.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtInterior.Location = new System.Drawing.Point(136, 280);
            this.txtInterior.Name = "txtInterior";
            this.txtInterior.Size = new System.Drawing.Size(67, 20);
            this.txtInterior.TabIndex = 5;
            // 
            // txtCalle
            // 
            this.txtCalle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCalle.Location = new System.Drawing.Point(136, 230);
            this.txtCalle.Name = "txtCalle";
            this.txtCalle.Size = new System.Drawing.Size(426, 20);
            this.txtCalle.TabIndex = 3;
            // 
            // txtExterior
            // 
            this.txtExterior.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtExterior.Location = new System.Drawing.Point(136, 255);
            this.txtExterior.Name = "txtExterior";
            this.txtExterior.Size = new System.Drawing.Size(67, 20);
            this.txtExterior.TabIndex = 4;
            // 
            // txtPais
            // 
            this.txtPais.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPais.Location = new System.Drawing.Point(136, 383);
            this.txtPais.Name = "txtPais";
            this.txtPais.Size = new System.Drawing.Size(130, 20);
            this.txtPais.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(99, 386);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 13);
            this.label13.TabIndex = 163;
            this.label13.Text = "Pais:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(95, 509);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(34, 13);
            this.label17.TabIndex = 164;
            this.label17.Text = "Logo:";
            // 
            // btnVer
            // 
            this.btnVer.Image = ((System.Drawing.Image)(resources.GetObject("btnVer.Image")));
            this.btnVer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVer.Location = new System.Drawing.Point(136, 504);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(67, 35);
            this.btnVer.TabIndex = 14;
            this.btnVer.Text = "Ver";
            this.btnVer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVer.UseVisualStyleBackColor = true;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Image = ((System.Drawing.Image)(resources.GetObject("btnAsignar.Image")));
            this.btnAsignar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAsignar.Location = new System.Drawing.Point(209, 504);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(67, 35);
            this.btnAsignar.TabIndex = 15;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(11, 560);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 18);
            this.label18.TabIndex = 169;
            this.label18.Text = "Timbrado";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(69, 601);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(60, 13);
            this.label19.TabIndex = 170;
            this.label19.Text = "Certificado:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(354, 601);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(36, 13);
            this.label20.TabIndex = 171;
            this.label20.Text = "Llave:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(334, 696);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(56, 13);
            this.label21.TabIndex = 172;
            this.label21.Text = "Password:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(77, 173);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(52, 13);
            this.label22.TabIndex = 173;
            this.label22.Text = "Regimen:";
            // 
            // txtCertificado
            // 
            this.txtCertificado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCertificado.Location = new System.Drawing.Point(135, 598);
            this.txtCertificado.Multiline = true;
            this.txtCertificado.Name = "txtCertificado";
            this.txtCertificado.Size = new System.Drawing.Size(213, 85);
            this.txtCertificado.TabIndex = 16;
            // 
            // txtLlave
            // 
            this.txtLlave.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLlave.Location = new System.Drawing.Point(396, 598);
            this.txtLlave.Multiline = true;
            this.txtLlave.Name = "txtLlave";
            this.txtLlave.Size = new System.Drawing.Size(214, 85);
            this.txtLlave.TabIndex = 17;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(396, 693);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(169, 20);
            this.txtPassword.TabIndex = 19;
            // 
            // txtRegimen
            // 
            this.txtRegimen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRegimen.Location = new System.Drawing.Point(135, 170);
            this.txtRegimen.Name = "txtRegimen";
            this.txtRegimen.Size = new System.Drawing.Size(425, 20);
            this.txtRegimen.TabIndex = 177;
            // 
            // btnExaminarCertificado
            // 
            this.btnExaminarCertificado.Location = new System.Drawing.Point(267, 569);
            this.btnExaminarCertificado.Name = "btnExaminarCertificado";
            this.btnExaminarCertificado.Size = new System.Drawing.Size(24, 23);
            this.btnExaminarCertificado.TabIndex = 178;
            this.btnExaminarCertificado.Text = "...";
            this.btnExaminarCertificado.UseVisualStyleBackColor = true;
            this.btnExaminarCertificado.Visible = false;
            this.btnExaminarCertificado.Click += new System.EventHandler(this.btnExaminarCertificado_Click);
            // 
            // btnExaminarLlave
            // 
            this.btnExaminarLlave.Location = new System.Drawing.Point(297, 569);
            this.btnExaminarLlave.Name = "btnExaminarLlave";
            this.btnExaminarLlave.Size = new System.Drawing.Size(24, 23);
            this.btnExaminarLlave.TabIndex = 179;
            this.btnExaminarLlave.Text = "...";
            this.btnExaminarLlave.UseVisualStyleBackColor = true;
            this.btnExaminarLlave.Visible = false;
            this.btnExaminarLlave.Click += new System.EventHandler(this.btnExaminarLlave_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(52, 692);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(80, 13);
            this.label23.TabIndex = 180;
            this.label23.Text = "No. Certificado:";
            // 
            // txtNoCertificado
            // 
            this.txtNoCertificado.Location = new System.Drawing.Point(136, 689);
            this.txtNoCertificado.Name = "txtNoCertificado";
            this.txtNoCertificado.Size = new System.Drawing.Size(175, 20);
            this.txtNoCertificado.TabIndex = 18;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(77, 718);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(51, 13);
            this.label24.TabIndex = 182;
            this.label24.Text = "Vigencia:";
            // 
            // dtpVigencia
            // 
            this.dtpVigencia.Location = new System.Drawing.Point(135, 718);
            this.dtpVigencia.Name = "dtpVigencia";
            this.dtpVigencia.Size = new System.Drawing.Size(213, 20);
            this.dtpVigencia.TabIndex = 20;
            // 
            // frmEmpresas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 758);
            this.Controls.Add(this.dtpVigencia);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txtNoCertificado);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.btnExaminarLlave);
            this.Controls.Add(this.btnExaminarCertificado);
            this.Controls.Add(this.txtRegimen);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLlave);
            this.Controls.Add(this.txtCertificado);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btnAsignar);
            this.Controls.Add(this.btnVer);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtPais);
            this.Controls.Add(this.Label14);
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
            this.Controls.Add(this.txtRepresentante);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.txtRfc);
            this.Controls.Add(this.txtRegistroPatronal);
            this.Controls.Add(this.txtDigitoVerificador);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label8);
            this.Name = "frmEmpresas";
            this.Text = "Empresa";
            this.Load += new System.EventHandler(this.frmEmpresas_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.toolEmpresa.ResumeLayout(false);
            this.toolEmpresa.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.toolAcciones.ResumeLayout(false);
            this.toolAcciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtRepresentante;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.ToolStrip toolEmpresa;
        internal System.Windows.Forms.ToolStripButton toolGuardarCerrar;
        internal System.Windows.Forms.ToolStripButton toolGuardarNuevo;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.ToolStrip toolAcciones;
        internal System.Windows.Forms.ToolStripLabel toolTitulo;
        internal System.Windows.Forms.TextBox txtRfc;
        internal System.Windows.Forms.MaskedTextBox txtRegistroPatronal;
        internal System.Windows.Forms.TextBox txtDigitoVerificador;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.TextBox txtNombre;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label11;
        private System.Windows.Forms.ToolStripButton toolCerrar;
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
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtCertificado;
        private System.Windows.Forms.TextBox txtLlave;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtRegimen;
        private System.Windows.Forms.Button btnExaminarCertificado;
        private System.Windows.Forms.Button btnExaminarLlave;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtNoCertificado;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.DateTimePicker dtpVigencia;
    }
}