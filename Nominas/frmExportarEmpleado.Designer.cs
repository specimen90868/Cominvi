namespace Nominas
{
    partial class frmExportarEmpleado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportarEmpleado));
            this.toolEmpleado = new System.Windows.Forms.ToolStrip();
            this.toolExportar = new System.Windows.Forms.ToolStripButton();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolTitulo = new System.Windows.Forms.ToolStripLabel();
            this.chkNombre = new System.Windows.Forms.CheckBox();
            this.chkPaterno = new System.Windows.Forms.CheckBox();
            this.chkMaterno = new System.Windows.Forms.CheckBox();
            this.chkNombreCompleto = new System.Windows.Forms.CheckBox();
            this.chkDepartamento = new System.Windows.Forms.CheckBox();
            this.chkPuesto = new System.Windows.Forms.CheckBox();
            this.chkFechaIngreso = new System.Windows.Forms.CheckBox();
            this.chkFechaAntiguedad = new System.Windows.Forms.CheckBox();
            this.chkFechaNacimiento = new System.Windows.Forms.CheckBox();
            this.chkEdad = new System.Windows.Forms.CheckBox();
            this.chkRfc = new System.Windows.Forms.CheckBox();
            this.chkCurp = new System.Windows.Forms.CheckBox();
            this.chkNss = new System.Windows.Forms.CheckBox();
            this.chkSdi = new System.Windows.Forms.CheckBox();
            this.chkSd = new System.Windows.Forms.CheckBox();
            this.chkSueldo = new System.Windows.Forms.CheckBox();
            this.chkEstatus = new System.Windows.Forms.CheckBox();
            this.chkCuenta = new System.Windows.Forms.CheckBox();
            this.chkClabe = new System.Windows.Forms.CheckBox();
            this.chkIdBancario = new System.Windows.Forms.CheckBox();
            this.chkCalle = new System.Windows.Forms.CheckBox();
            this.chkExterior = new System.Windows.Forms.CheckBox();
            this.chkInterior = new System.Windows.Forms.CheckBox();
            this.chkColonia = new System.Windows.Forms.CheckBox();
            this.chkCp = new System.Windows.Forms.CheckBox();
            this.chkCiudad = new System.Windows.Forms.CheckBox();
            this.chkEstado = new System.Windows.Forms.CheckBox();
            this.chkPais = new System.Windows.Forms.CheckBox();
            this.chkEstadoCivil = new System.Windows.Forms.CheckBox();
            this.chkSexo = new System.Windows.Forms.CheckBox();
            this.chkEscolaridad = new System.Windows.Forms.CheckBox();
            this.chkNacionalidad = new System.Windows.Forms.CheckBox();
            this.chkCreditoInfonavit = new System.Windows.Forms.CheckBox();
            this.chkDescuentoInfonavit = new System.Windows.Forms.CheckBox();
            this.chkValorDescuento = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolPorcentaje = new System.Windows.Forms.ToolStripStatusLabel();
            this.workerExportar = new System.ComponentModel.BackgroundWorker();
            this.toolEmpleado.SuspendLayout();
            this.toolAcciones.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolEmpleado
            // 
            this.toolEmpleado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolExportar});
            this.toolEmpleado.Location = new System.Drawing.Point(0, 27);
            this.toolEmpleado.Name = "toolEmpleado";
            this.toolEmpleado.Size = new System.Drawing.Size(425, 25);
            this.toolEmpleado.TabIndex = 211;
            this.toolEmpleado.Text = "toolEmpresa";
            // 
            // toolExportar
            // 
            this.toolExportar.Image = ((System.Drawing.Image)(resources.GetObject("toolExportar.Image")));
            this.toolExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExportar.Name = "toolExportar";
            this.toolExportar.Size = new System.Drawing.Size(70, 22);
            this.toolExportar.Text = "Exportar";
            this.toolExportar.Click += new System.EventHandler(this.toolExportar_Click);
            // 
            // toolAcciones
            // 
            this.toolAcciones.BackColor = System.Drawing.Color.DarkGray;
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTitulo});
            this.toolAcciones.Location = new System.Drawing.Point(0, 0);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(425, 27);
            this.toolAcciones.TabIndex = 210;
            this.toolAcciones.Text = "toolAcciones";
            // 
            // toolTitulo
            // 
            this.toolTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(192, 24);
            this.toolTitulo.Text = "Datos para exportar";
            // 
            // chkNombre
            // 
            this.chkNombre.AutoSize = true;
            this.chkNombre.Location = new System.Drawing.Point(30, 70);
            this.chkNombre.Name = "chkNombre";
            this.chkNombre.Size = new System.Drawing.Size(74, 17);
            this.chkNombre.TabIndex = 212;
            this.chkNombre.Text = "Nombre(s)";
            this.chkNombre.UseVisualStyleBackColor = true;
            // 
            // chkPaterno
            // 
            this.chkPaterno.AutoSize = true;
            this.chkPaterno.Location = new System.Drawing.Point(30, 94);
            this.chkPaterno.Name = "chkPaterno";
            this.chkPaterno.Size = new System.Drawing.Size(82, 17);
            this.chkPaterno.TabIndex = 213;
            this.chkPaterno.Text = "Ap. Paterno";
            this.chkPaterno.UseVisualStyleBackColor = true;
            // 
            // chkMaterno
            // 
            this.chkMaterno.AutoSize = true;
            this.chkMaterno.Location = new System.Drawing.Point(30, 118);
            this.chkMaterno.Name = "chkMaterno";
            this.chkMaterno.Size = new System.Drawing.Size(84, 17);
            this.chkMaterno.TabIndex = 214;
            this.chkMaterno.Text = "Ap. Materno";
            this.chkMaterno.UseVisualStyleBackColor = true;
            // 
            // chkNombreCompleto
            // 
            this.chkNombreCompleto.AutoSize = true;
            this.chkNombreCompleto.Location = new System.Drawing.Point(30, 142);
            this.chkNombreCompleto.Name = "chkNombreCompleto";
            this.chkNombreCompleto.Size = new System.Drawing.Size(110, 17);
            this.chkNombreCompleto.TabIndex = 215;
            this.chkNombreCompleto.Text = "Nombre Completo";
            this.chkNombreCompleto.UseVisualStyleBackColor = true;
            // 
            // chkDepartamento
            // 
            this.chkDepartamento.AutoSize = true;
            this.chkDepartamento.Location = new System.Drawing.Point(30, 166);
            this.chkDepartamento.Name = "chkDepartamento";
            this.chkDepartamento.Size = new System.Drawing.Size(93, 17);
            this.chkDepartamento.TabIndex = 216;
            this.chkDepartamento.Text = "Departamento";
            this.chkDepartamento.UseVisualStyleBackColor = true;
            // 
            // chkPuesto
            // 
            this.chkPuesto.AutoSize = true;
            this.chkPuesto.Location = new System.Drawing.Point(30, 190);
            this.chkPuesto.Name = "chkPuesto";
            this.chkPuesto.Size = new System.Drawing.Size(59, 17);
            this.chkPuesto.TabIndex = 217;
            this.chkPuesto.Text = "Puesto";
            this.chkPuesto.UseVisualStyleBackColor = true;
            // 
            // chkFechaIngreso
            // 
            this.chkFechaIngreso.AutoSize = true;
            this.chkFechaIngreso.Location = new System.Drawing.Point(30, 213);
            this.chkFechaIngreso.Name = "chkFechaIngreso";
            this.chkFechaIngreso.Size = new System.Drawing.Size(108, 17);
            this.chkFechaIngreso.TabIndex = 218;
            this.chkFechaIngreso.Text = "Fecha de ingreso";
            this.chkFechaIngreso.UseVisualStyleBackColor = true;
            // 
            // chkFechaAntiguedad
            // 
            this.chkFechaAntiguedad.AutoSize = true;
            this.chkFechaAntiguedad.Location = new System.Drawing.Point(30, 237);
            this.chkFechaAntiguedad.Name = "chkFechaAntiguedad";
            this.chkFechaAntiguedad.Size = new System.Drawing.Size(127, 17);
            this.chkFechaAntiguedad.TabIndex = 219;
            this.chkFechaAntiguedad.Text = "Fecha de antigüedad";
            this.chkFechaAntiguedad.UseVisualStyleBackColor = true;
            // 
            // chkFechaNacimiento
            // 
            this.chkFechaNacimiento.AutoSize = true;
            this.chkFechaNacimiento.Location = new System.Drawing.Point(30, 261);
            this.chkFechaNacimiento.Name = "chkFechaNacimiento";
            this.chkFechaNacimiento.Size = new System.Drawing.Size(125, 17);
            this.chkFechaNacimiento.TabIndex = 220;
            this.chkFechaNacimiento.Text = "Fecha de nacimiento";
            this.chkFechaNacimiento.UseVisualStyleBackColor = true;
            // 
            // chkEdad
            // 
            this.chkEdad.AutoSize = true;
            this.chkEdad.Location = new System.Drawing.Point(30, 285);
            this.chkEdad.Name = "chkEdad";
            this.chkEdad.Size = new System.Drawing.Size(51, 17);
            this.chkEdad.TabIndex = 221;
            this.chkEdad.Text = "Edad";
            this.chkEdad.UseVisualStyleBackColor = true;
            // 
            // chkRfc
            // 
            this.chkRfc.AutoSize = true;
            this.chkRfc.Location = new System.Drawing.Point(30, 309);
            this.chkRfc.Name = "chkRfc";
            this.chkRfc.Size = new System.Drawing.Size(56, 17);
            this.chkRfc.TabIndex = 222;
            this.chkRfc.Text = "R.F.C.";
            this.chkRfc.UseVisualStyleBackColor = true;
            // 
            // chkCurp
            // 
            this.chkCurp.AutoSize = true;
            this.chkCurp.Location = new System.Drawing.Point(30, 333);
            this.chkCurp.Name = "chkCurp";
            this.chkCurp.Size = new System.Drawing.Size(56, 17);
            this.chkCurp.TabIndex = 223;
            this.chkCurp.Text = "CURP";
            this.chkCurp.UseVisualStyleBackColor = true;
            // 
            // chkNss
            // 
            this.chkNss.AutoSize = true;
            this.chkNss.Location = new System.Drawing.Point(30, 357);
            this.chkNss.Name = "chkNss";
            this.chkNss.Size = new System.Drawing.Size(57, 17);
            this.chkNss.TabIndex = 224;
            this.chkNss.Text = "N.S.S.";
            this.chkNss.UseVisualStyleBackColor = true;
            // 
            // chkSdi
            // 
            this.chkSdi.AutoSize = true;
            this.chkSdi.Location = new System.Drawing.Point(30, 381);
            this.chkSdi.Name = "chkSdi";
            this.chkSdi.Size = new System.Drawing.Size(53, 17);
            this.chkSdi.TabIndex = 225;
            this.chkSdi.Text = "S.D.I.";
            this.chkSdi.UseVisualStyleBackColor = true;
            // 
            // chkSd
            // 
            this.chkSd.AutoSize = true;
            this.chkSd.Location = new System.Drawing.Point(171, 70);
            this.chkSd.Name = "chkSd";
            this.chkSd.Size = new System.Drawing.Size(47, 17);
            this.chkSd.TabIndex = 226;
            this.chkSd.Text = "S.D.";
            this.chkSd.UseVisualStyleBackColor = true;
            // 
            // chkSueldo
            // 
            this.chkSueldo.AutoSize = true;
            this.chkSueldo.Location = new System.Drawing.Point(171, 94);
            this.chkSueldo.Name = "chkSueldo";
            this.chkSueldo.Size = new System.Drawing.Size(59, 17);
            this.chkSueldo.TabIndex = 227;
            this.chkSueldo.Text = "Sueldo";
            this.chkSueldo.UseVisualStyleBackColor = true;
            // 
            // chkEstatus
            // 
            this.chkEstatus.AutoSize = true;
            this.chkEstatus.Location = new System.Drawing.Point(171, 118);
            this.chkEstatus.Name = "chkEstatus";
            this.chkEstatus.Size = new System.Drawing.Size(61, 17);
            this.chkEstatus.TabIndex = 228;
            this.chkEstatus.Text = "Estatus";
            this.chkEstatus.UseVisualStyleBackColor = true;
            // 
            // chkCuenta
            // 
            this.chkCuenta.AutoSize = true;
            this.chkCuenta.Location = new System.Drawing.Point(171, 142);
            this.chkCuenta.Name = "chkCuenta";
            this.chkCuenta.Size = new System.Drawing.Size(60, 17);
            this.chkCuenta.TabIndex = 229;
            this.chkCuenta.Text = "Cuenta";
            this.chkCuenta.UseVisualStyleBackColor = true;
            // 
            // chkClabe
            // 
            this.chkClabe.AutoSize = true;
            this.chkClabe.Location = new System.Drawing.Point(171, 166);
            this.chkClabe.Name = "chkClabe";
            this.chkClabe.Size = new System.Drawing.Size(53, 17);
            this.chkClabe.TabIndex = 230;
            this.chkClabe.Text = "Clabe";
            this.chkClabe.UseVisualStyleBackColor = true;
            // 
            // chkIdBancario
            // 
            this.chkIdBancario.AutoSize = true;
            this.chkIdBancario.Location = new System.Drawing.Point(171, 190);
            this.chkIdBancario.Name = "chkIdBancario";
            this.chkIdBancario.Size = new System.Drawing.Size(82, 17);
            this.chkIdBancario.TabIndex = 231;
            this.chkIdBancario.Text = "ID Bancario";
            this.chkIdBancario.UseVisualStyleBackColor = true;
            // 
            // chkCalle
            // 
            this.chkCalle.AutoSize = true;
            this.chkCalle.Location = new System.Drawing.Point(171, 213);
            this.chkCalle.Name = "chkCalle";
            this.chkCalle.Size = new System.Drawing.Size(49, 17);
            this.chkCalle.TabIndex = 232;
            this.chkCalle.Text = "Calle";
            this.chkCalle.UseVisualStyleBackColor = true;
            // 
            // chkExterior
            // 
            this.chkExterior.AutoSize = true;
            this.chkExterior.Location = new System.Drawing.Point(171, 237);
            this.chkExterior.Name = "chkExterior";
            this.chkExterior.Size = new System.Drawing.Size(61, 17);
            this.chkExterior.TabIndex = 233;
            this.chkExterior.Text = "Exterior";
            this.chkExterior.UseVisualStyleBackColor = true;
            // 
            // chkInterior
            // 
            this.chkInterior.AutoSize = true;
            this.chkInterior.Location = new System.Drawing.Point(171, 261);
            this.chkInterior.Name = "chkInterior";
            this.chkInterior.Size = new System.Drawing.Size(58, 17);
            this.chkInterior.TabIndex = 234;
            this.chkInterior.Text = "Interior";
            this.chkInterior.UseVisualStyleBackColor = true;
            // 
            // chkColonia
            // 
            this.chkColonia.AutoSize = true;
            this.chkColonia.Location = new System.Drawing.Point(171, 285);
            this.chkColonia.Name = "chkColonia";
            this.chkColonia.Size = new System.Drawing.Size(61, 17);
            this.chkColonia.TabIndex = 235;
            this.chkColonia.Text = "Colonia";
            this.chkColonia.UseVisualStyleBackColor = true;
            // 
            // chkCp
            // 
            this.chkCp.AutoSize = true;
            this.chkCp.Location = new System.Drawing.Point(171, 309);
            this.chkCp.Name = "chkCp";
            this.chkCp.Size = new System.Drawing.Size(46, 17);
            this.chkCp.TabIndex = 236;
            this.chkCp.Text = "C.P.";
            this.chkCp.UseVisualStyleBackColor = true;
            // 
            // chkCiudad
            // 
            this.chkCiudad.AutoSize = true;
            this.chkCiudad.Location = new System.Drawing.Point(171, 333);
            this.chkCiudad.Name = "chkCiudad";
            this.chkCiudad.Size = new System.Drawing.Size(59, 17);
            this.chkCiudad.TabIndex = 237;
            this.chkCiudad.Text = "Ciudad";
            this.chkCiudad.UseVisualStyleBackColor = true;
            // 
            // chkEstado
            // 
            this.chkEstado.AutoSize = true;
            this.chkEstado.Location = new System.Drawing.Point(171, 357);
            this.chkEstado.Name = "chkEstado";
            this.chkEstado.Size = new System.Drawing.Size(59, 17);
            this.chkEstado.TabIndex = 238;
            this.chkEstado.Text = "Estado";
            this.chkEstado.UseVisualStyleBackColor = true;
            // 
            // chkPais
            // 
            this.chkPais.AutoSize = true;
            this.chkPais.Location = new System.Drawing.Point(171, 381);
            this.chkPais.Name = "chkPais";
            this.chkPais.Size = new System.Drawing.Size(46, 17);
            this.chkPais.TabIndex = 239;
            this.chkPais.Text = "Pais";
            this.chkPais.UseVisualStyleBackColor = true;
            // 
            // chkEstadoCivil
            // 
            this.chkEstadoCivil.AutoSize = true;
            this.chkEstadoCivil.Location = new System.Drawing.Point(279, 70);
            this.chkEstadoCivil.Name = "chkEstadoCivil";
            this.chkEstadoCivil.Size = new System.Drawing.Size(80, 17);
            this.chkEstadoCivil.TabIndex = 240;
            this.chkEstadoCivil.Text = "Estado civil";
            this.chkEstadoCivil.UseVisualStyleBackColor = true;
            // 
            // chkSexo
            // 
            this.chkSexo.AutoSize = true;
            this.chkSexo.Location = new System.Drawing.Point(279, 94);
            this.chkSexo.Name = "chkSexo";
            this.chkSexo.Size = new System.Drawing.Size(50, 17);
            this.chkSexo.TabIndex = 241;
            this.chkSexo.Text = "Sexo";
            this.chkSexo.UseVisualStyleBackColor = true;
            // 
            // chkEscolaridad
            // 
            this.chkEscolaridad.AutoSize = true;
            this.chkEscolaridad.Location = new System.Drawing.Point(279, 118);
            this.chkEscolaridad.Name = "chkEscolaridad";
            this.chkEscolaridad.Size = new System.Drawing.Size(81, 17);
            this.chkEscolaridad.TabIndex = 242;
            this.chkEscolaridad.Text = "Escolaridad";
            this.chkEscolaridad.UseVisualStyleBackColor = true;
            // 
            // chkNacionalidad
            // 
            this.chkNacionalidad.AutoSize = true;
            this.chkNacionalidad.Location = new System.Drawing.Point(279, 142);
            this.chkNacionalidad.Name = "chkNacionalidad";
            this.chkNacionalidad.Size = new System.Drawing.Size(88, 17);
            this.chkNacionalidad.TabIndex = 243;
            this.chkNacionalidad.Text = "Nacionalidad";
            this.chkNacionalidad.UseVisualStyleBackColor = true;
            // 
            // chkCreditoInfonavit
            // 
            this.chkCreditoInfonavit.AutoSize = true;
            this.chkCreditoInfonavit.Location = new System.Drawing.Point(279, 166);
            this.chkCreditoInfonavit.Name = "chkCreditoInfonavit";
            this.chkCreditoInfonavit.Size = new System.Drawing.Size(103, 17);
            this.chkCreditoInfonavit.TabIndex = 244;
            this.chkCreditoInfonavit.Text = "Credito Infonavit";
            this.chkCreditoInfonavit.UseVisualStyleBackColor = true;
            // 
            // chkDescuentoInfonavit
            // 
            this.chkDescuentoInfonavit.AutoSize = true;
            this.chkDescuentoInfonavit.Location = new System.Drawing.Point(279, 190);
            this.chkDescuentoInfonavit.Name = "chkDescuentoInfonavit";
            this.chkDescuentoInfonavit.Size = new System.Drawing.Size(122, 17);
            this.chkDescuentoInfonavit.TabIndex = 245;
            this.chkDescuentoInfonavit.Text = "Descuento Infonavit";
            this.chkDescuentoInfonavit.UseVisualStyleBackColor = true;
            // 
            // chkValorDescuento
            // 
            this.chkValorDescuento.AutoSize = true;
            this.chkValorDescuento.Location = new System.Drawing.Point(279, 213);
            this.chkValorDescuento.Name = "chkValorDescuento";
            this.chkValorDescuento.Size = new System.Drawing.Size(120, 17);
            this.chkValorDescuento.TabIndex = 246;
            this.chkValorDescuento.Text = "Valor del descuento";
            this.chkValorDescuento.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolEstado,
            this.toolPorcentaje});
            this.statusStrip1.Location = new System.Drawing.Point(0, 409);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(425, 22);
            this.statusStrip1.TabIndex = 247;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolEstado
            // 
            this.toolEstado.Name = "toolEstado";
            this.toolEstado.Size = new System.Drawing.Size(76, 17);
            this.toolEstado.Text = "Exportando...";
            // 
            // toolPorcentaje
            // 
            this.toolPorcentaje.Name = "toolPorcentaje";
            this.toolPorcentaje.Size = new System.Drawing.Size(23, 17);
            this.toolPorcentaje.Text = "0%";
            // 
            // workerExportar
            // 
            this.workerExportar.WorkerReportsProgress = true;
            this.workerExportar.WorkerSupportsCancellation = true;
            this.workerExportar.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerExportar_DoWork);
            this.workerExportar.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.workerExportar_ProgressChanged);
            this.workerExportar.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerExportar_RunWorkerCompleted);
            // 
            // frmExportarEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 431);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkValorDescuento);
            this.Controls.Add(this.chkDescuentoInfonavit);
            this.Controls.Add(this.chkCreditoInfonavit);
            this.Controls.Add(this.chkNacionalidad);
            this.Controls.Add(this.chkEscolaridad);
            this.Controls.Add(this.chkSexo);
            this.Controls.Add(this.chkEstadoCivil);
            this.Controls.Add(this.chkPais);
            this.Controls.Add(this.chkEstado);
            this.Controls.Add(this.chkCiudad);
            this.Controls.Add(this.chkCp);
            this.Controls.Add(this.chkColonia);
            this.Controls.Add(this.chkInterior);
            this.Controls.Add(this.chkExterior);
            this.Controls.Add(this.chkCalle);
            this.Controls.Add(this.chkIdBancario);
            this.Controls.Add(this.chkClabe);
            this.Controls.Add(this.chkCuenta);
            this.Controls.Add(this.chkEstatus);
            this.Controls.Add(this.chkSueldo);
            this.Controls.Add(this.chkSd);
            this.Controls.Add(this.chkSdi);
            this.Controls.Add(this.chkNss);
            this.Controls.Add(this.chkCurp);
            this.Controls.Add(this.chkRfc);
            this.Controls.Add(this.chkEdad);
            this.Controls.Add(this.chkFechaNacimiento);
            this.Controls.Add(this.chkFechaAntiguedad);
            this.Controls.Add(this.chkFechaIngreso);
            this.Controls.Add(this.chkPuesto);
            this.Controls.Add(this.chkDepartamento);
            this.Controls.Add(this.chkNombreCompleto);
            this.Controls.Add(this.chkMaterno);
            this.Controls.Add(this.chkPaterno);
            this.Controls.Add(this.chkNombre);
            this.Controls.Add(this.toolEmpleado);
            this.Controls.Add(this.toolAcciones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(441, 470);
            this.MinimizeBox = false;
            this.Name = "frmExportarEmpleado";
            this.Text = "Datos para exportar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmExportarEmpleado_FormClosing);
            this.Load += new System.EventHandler(this.frmExportarEmpleado_Load);
            this.toolEmpleado.ResumeLayout(false);
            this.toolEmpleado.PerformLayout();
            this.toolAcciones.ResumeLayout(false);
            this.toolAcciones.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolEmpleado;
        private System.Windows.Forms.ToolStripButton toolExportar;
        internal System.Windows.Forms.ToolStrip toolAcciones;
        internal System.Windows.Forms.ToolStripLabel toolTitulo;
        private System.Windows.Forms.CheckBox chkNombre;
        private System.Windows.Forms.CheckBox chkPaterno;
        private System.Windows.Forms.CheckBox chkMaterno;
        private System.Windows.Forms.CheckBox chkNombreCompleto;
        private System.Windows.Forms.CheckBox chkDepartamento;
        private System.Windows.Forms.CheckBox chkPuesto;
        private System.Windows.Forms.CheckBox chkFechaIngreso;
        private System.Windows.Forms.CheckBox chkFechaAntiguedad;
        private System.Windows.Forms.CheckBox chkFechaNacimiento;
        private System.Windows.Forms.CheckBox chkEdad;
        private System.Windows.Forms.CheckBox chkRfc;
        private System.Windows.Forms.CheckBox chkCurp;
        private System.Windows.Forms.CheckBox chkNss;
        private System.Windows.Forms.CheckBox chkSdi;
        private System.Windows.Forms.CheckBox chkSd;
        private System.Windows.Forms.CheckBox chkSueldo;
        private System.Windows.Forms.CheckBox chkEstatus;
        private System.Windows.Forms.CheckBox chkCuenta;
        private System.Windows.Forms.CheckBox chkClabe;
        private System.Windows.Forms.CheckBox chkIdBancario;
        private System.Windows.Forms.CheckBox chkCalle;
        private System.Windows.Forms.CheckBox chkExterior;
        private System.Windows.Forms.CheckBox chkInterior;
        private System.Windows.Forms.CheckBox chkColonia;
        private System.Windows.Forms.CheckBox chkCp;
        private System.Windows.Forms.CheckBox chkCiudad;
        private System.Windows.Forms.CheckBox chkEstado;
        private System.Windows.Forms.CheckBox chkPais;
        private System.Windows.Forms.CheckBox chkEstadoCivil;
        private System.Windows.Forms.CheckBox chkSexo;
        private System.Windows.Forms.CheckBox chkEscolaridad;
        private System.Windows.Forms.CheckBox chkNacionalidad;
        private System.Windows.Forms.CheckBox chkCreditoInfonavit;
        private System.Windows.Forms.CheckBox chkDescuentoInfonavit;
        private System.Windows.Forms.CheckBox chkValorDescuento;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolEstado;
        private System.Windows.Forms.ToolStripStatusLabel toolPorcentaje;
        private System.ComponentModel.BackgroundWorker workerExportar;
    }
}