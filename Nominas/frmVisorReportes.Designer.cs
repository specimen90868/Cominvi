namespace Nominas
{
    partial class frmVisorReportes
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
            this.rpvVisor = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpvVisor
            // 
            this.rpvVisor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvVisor.Location = new System.Drawing.Point(0, 0);
            this.rpvVisor.Name = "rpvVisor";
            this.rpvVisor.Size = new System.Drawing.Size(492, 481);
            this.rpvVisor.TabIndex = 0;
            // 
            // frmVisorReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 481);
            this.Controls.Add(this.rpvVisor);
            this.Name = "frmVisorReportes";
            this.Text = "Visor de reportes";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmVisorReportes_FormClosed);
            this.Load += new System.EventHandler(this.frmVisorReportes_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpvVisor;
    }
}