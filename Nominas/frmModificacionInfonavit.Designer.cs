namespace Nominas
{
    partial class frmModificacionInfonavit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModificacionInfonavit));
            this.rbtnTipoDescuento = new System.Windows.Forms.RadioButton();
            this.rbtnValorDescuento = new System.Windows.Forms.RadioButton();
            this.rbtnCredito = new System.Windows.Forms.RadioButton();
            this.Label5 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.grpTipoDescuento = new System.Windows.Forms.GroupBox();
            this.rbtnFijoPesos = new System.Windows.Forms.RadioButton();
            this.rbtnVSM = new System.Windows.Forms.RadioButton();
            this.rbtnPorcentaje = new System.Windows.Forms.RadioButton();
            this.grpValorDescuento = new System.Windows.Forms.GroupBox();
            this.txtValorDescuento = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.grpCredito = new System.Windows.Forms.GroupBox();
            this.txtCredito = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolVentana = new System.Windows.Forms.ToolStripLabel();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.grpTipoDescuento.SuspendLayout();
            this.grpValorDescuento.SuspendLayout();
            this.grpCredito.SuspendLayout();
            this.toolTitulo.SuspendLayout();
            this.toolAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtnTipoDescuento
            // 
            this.rbtnTipoDescuento.AutoSize = true;
            this.rbtnTipoDescuento.Location = new System.Drawing.Point(21, 350);
            this.rbtnTipoDescuento.Name = "rbtnTipoDescuento";
            this.rbtnTipoDescuento.Size = new System.Drawing.Size(114, 17);
            this.rbtnTipoDescuento.TabIndex = 30;
            this.rbtnTipoDescuento.TabStop = true;
            this.rbtnTipoDescuento.Text = "Tipo de descuento";
            this.rbtnTipoDescuento.UseVisualStyleBackColor = true;
            this.rbtnTipoDescuento.CheckedChanged += new System.EventHandler(this.rbtnTipoDescuento_CheckedChanged);
            // 
            // rbtnValorDescuento
            // 
            this.rbtnValorDescuento.AutoSize = true;
            this.rbtnValorDescuento.Location = new System.Drawing.Point(21, 255);
            this.rbtnValorDescuento.Name = "rbtnValorDescuento";
            this.rbtnValorDescuento.Size = new System.Drawing.Size(117, 17);
            this.rbtnValorDescuento.TabIndex = 29;
            this.rbtnValorDescuento.TabStop = true;
            this.rbtnValorDescuento.Text = "Valor de descuento";
            this.rbtnValorDescuento.UseVisualStyleBackColor = true;
            this.rbtnValorDescuento.CheckedChanged += new System.EventHandler(this.rbtnValorDescuento_CheckedChanged);
            // 
            // rbtnCredito
            // 
            this.rbtnCredito.AutoSize = true;
            this.rbtnCredito.Location = new System.Drawing.Point(21, 159);
            this.rbtnCredito.Name = "rbtnCredito";
            this.rbtnCredito.Size = new System.Drawing.Size(92, 17);
            this.rbtnCredito.TabIndex = 28;
            this.rbtnCredito.TabStop = true;
            this.rbtnCredito.Text = "No. de crédito";
            this.rbtnCredito.UseVisualStyleBackColor = true;
            this.rbtnCredito.CheckedChanged += new System.EventHandler(this.rbtnCredito_CheckedChanged);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(168, 161);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(40, 13);
            this.Label5.TabIndex = 27;
            this.Label5.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(214, 159);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(82, 20);
            this.dtpFecha.TabIndex = 26;
            // 
            // grpTipoDescuento
            // 
            this.grpTipoDescuento.Controls.Add(this.rbtnFijoPesos);
            this.grpTipoDescuento.Controls.Add(this.rbtnVSM);
            this.grpTipoDescuento.Controls.Add(this.rbtnPorcentaje);
            this.grpTipoDescuento.Location = new System.Drawing.Point(21, 373);
            this.grpTipoDescuento.Name = "grpTipoDescuento";
            this.grpTipoDescuento.Size = new System.Drawing.Size(275, 53);
            this.grpTipoDescuento.TabIndex = 25;
            this.grpTipoDescuento.TabStop = false;
            this.grpTipoDescuento.Text = "Tipo de descuento";
            // 
            // rbtnFijoPesos
            // 
            this.rbtnFijoPesos.AutoSize = true;
            this.rbtnFijoPesos.Location = new System.Drawing.Point(150, 20);
            this.rbtnFijoPesos.Name = "rbtnFijoPesos";
            this.rbtnFijoPesos.Size = new System.Drawing.Size(75, 17);
            this.rbtnFijoPesos.TabIndex = 6;
            this.rbtnFijoPesos.TabStop = true;
            this.rbtnFijoPesos.Text = "Fijo/Pesos";
            this.rbtnFijoPesos.UseVisualStyleBackColor = true;
            this.rbtnFijoPesos.CheckedChanged += new System.EventHandler(this.rbtnFijoPesos_CheckedChanged);
            // 
            // rbtnVSM
            // 
            this.rbtnVSM.AutoSize = true;
            this.rbtnVSM.Location = new System.Drawing.Point(96, 20);
            this.rbtnVSM.Name = "rbtnVSM";
            this.rbtnVSM.Size = new System.Drawing.Size(48, 17);
            this.rbtnVSM.TabIndex = 5;
            this.rbtnVSM.TabStop = true;
            this.rbtnVSM.Text = "VSM";
            this.rbtnVSM.UseVisualStyleBackColor = true;
            this.rbtnVSM.CheckedChanged += new System.EventHandler(this.rbtnVSM_CheckedChanged);
            // 
            // rbtnPorcentaje
            // 
            this.rbtnPorcentaje.AutoSize = true;
            this.rbtnPorcentaje.Location = new System.Drawing.Point(14, 20);
            this.rbtnPorcentaje.Name = "rbtnPorcentaje";
            this.rbtnPorcentaje.Size = new System.Drawing.Size(76, 17);
            this.rbtnPorcentaje.TabIndex = 4;
            this.rbtnPorcentaje.TabStop = true;
            this.rbtnPorcentaje.Text = "Porcentaje";
            this.rbtnPorcentaje.UseVisualStyleBackColor = true;
            this.rbtnPorcentaje.CheckedChanged += new System.EventHandler(this.rbtnPorcentaje_CheckedChanged);
            // 
            // grpValorDescuento
            // 
            this.grpValorDescuento.Controls.Add(this.txtValorDescuento);
            this.grpValorDescuento.Controls.Add(this.Label4);
            this.grpValorDescuento.Location = new System.Drawing.Point(21, 278);
            this.grpValorDescuento.Name = "grpValorDescuento";
            this.grpValorDescuento.Size = new System.Drawing.Size(275, 66);
            this.grpValorDescuento.TabIndex = 24;
            this.grpValorDescuento.TabStop = false;
            this.grpValorDescuento.Text = "Valor de descuento";
            // 
            // txtValorDescuento
            // 
            this.txtValorDescuento.Location = new System.Drawing.Point(114, 29);
            this.txtValorDescuento.Name = "txtValorDescuento";
            this.txtValorDescuento.Size = new System.Drawing.Size(68, 20);
            this.txtValorDescuento.TabIndex = 1;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(11, 32);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(102, 13);
            this.Label4.TabIndex = 0;
            this.Label4.Text = "Valor de descuento:";
            // 
            // grpCredito
            // 
            this.grpCredito.Controls.Add(this.txtCredito);
            this.grpCredito.Controls.Add(this.Label3);
            this.grpCredito.Location = new System.Drawing.Point(21, 183);
            this.grpCredito.Name = "grpCredito";
            this.grpCredito.Size = new System.Drawing.Size(275, 66);
            this.grpCredito.TabIndex = 23;
            this.grpCredito.TabStop = false;
            this.grpCredito.Text = "No. de crédito";
            // 
            // txtCredito
            // 
            this.txtCredito.Location = new System.Drawing.Point(114, 28);
            this.txtCredito.Name = "txtCredito";
            this.txtCredito.Size = new System.Drawing.Size(144, 20);
            this.txtCredito.TabIndex = 1;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(11, 31);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(97, 13);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Número de crédito:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(18, 128);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(108, 13);
            this.Label1.TabIndex = 20;
            this.Label1.Text = "Tipo de modificación:";
            // 
            // toolTitulo
            // 
            this.toolTitulo.BackColor = System.Drawing.Color.DarkGray;
            this.toolTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolVentana});
            this.toolTitulo.Location = new System.Drawing.Point(0, 0);
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(494, 27);
            this.toolTitulo.TabIndex = 31;
            this.toolTitulo.Text = "toolAcciones";
            // 
            // toolVentana
            // 
            this.toolVentana.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolVentana.Name = "toolVentana";
            this.toolVentana.Size = new System.Drawing.Size(281, 24);
            this.toolVentana.Text = "Modificación crédito infonavit";
            // 
            // toolAcciones
            // 
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGuardar,
            this.toolCerrar});
            this.toolAcciones.Location = new System.Drawing.Point(0, 27);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(494, 25);
            this.toolAcciones.TabIndex = 32;
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
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.Location = new System.Drawing.Point(117, 75);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(183, 20);
            this.lblEmpleado.TabIndex = 253;
            this.lblEmpleado.Text = "Nombre del empleado";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(17, 75);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(94, 20);
            this.label23.TabIndex = 252;
            this.label23.Text = "Empleado:";
            // 
            // frmModificacionInfonavit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 641);
            this.Controls.Add(this.lblEmpleado);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.toolAcciones);
            this.Controls.Add(this.toolTitulo);
            this.Controls.Add(this.rbtnTipoDescuento);
            this.Controls.Add(this.rbtnValorDescuento);
            this.Controls.Add(this.rbtnCredito);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.grpTipoDescuento);
            this.Controls.Add(this.grpValorDescuento);
            this.Controls.Add(this.grpCredito);
            this.Controls.Add(this.Label1);
            this.Name = "frmModificacionInfonavit";
            this.Text = "Modificacion Infonavit";
            this.Load += new System.EventHandler(this.frmModificacionInfonavit_Load);
            this.grpTipoDescuento.ResumeLayout(false);
            this.grpTipoDescuento.PerformLayout();
            this.grpValorDescuento.ResumeLayout(false);
            this.grpValorDescuento.PerformLayout();
            this.grpCredito.ResumeLayout(false);
            this.grpCredito.PerformLayout();
            this.toolTitulo.ResumeLayout(false);
            this.toolTitulo.PerformLayout();
            this.toolAcciones.ResumeLayout(false);
            this.toolAcciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.RadioButton rbtnTipoDescuento;
        internal System.Windows.Forms.RadioButton rbtnValorDescuento;
        internal System.Windows.Forms.RadioButton rbtnCredito;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.DateTimePicker dtpFecha;
        internal System.Windows.Forms.GroupBox grpTipoDescuento;
        internal System.Windows.Forms.RadioButton rbtnFijoPesos;
        internal System.Windows.Forms.RadioButton rbtnVSM;
        internal System.Windows.Forms.RadioButton rbtnPorcentaje;
        internal System.Windows.Forms.GroupBox grpValorDescuento;
        internal System.Windows.Forms.TextBox txtValorDescuento;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.GroupBox grpCredito;
        internal System.Windows.Forms.TextBox txtCredito;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ToolStrip toolTitulo;
        internal System.Windows.Forms.ToolStripLabel toolVentana;
        internal System.Windows.Forms.ToolStrip toolAcciones;
        internal System.Windows.Forms.ToolStripButton toolGuardar;
        private System.Windows.Forms.ToolStripButton toolCerrar;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.Label label23;
    }
}