namespace Nominas
{
    partial class frmEditorFormulas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditorFormulas));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("(");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode(")");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("+");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("-");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("*");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("/");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Operadores", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            this.toolTitulo = new System.Windows.Forms.ToolStrip();
            this.toolVentana = new System.Windows.Forms.ToolStripLabel();
            this.toolAcciones = new System.Windows.Forms.ToolStrip();
            this.toolAsignar = new System.Windows.Forms.ToolStripButton();
            this.toolCerrar = new System.Windows.Forms.ToolStripButton();
            this.treeFormulas = new System.Windows.Forms.TreeView();
            this.txtFormula = new System.Windows.Forms.TextBox();
            this.treeOperadores = new System.Windows.Forms.TreeView();
            this.ContenedorTreeView = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTitulo.SuspendLayout();
            this.toolAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContenedorTreeView)).BeginInit();
            this.ContenedorTreeView.Panel1.SuspendLayout();
            this.ContenedorTreeView.Panel2.SuspendLayout();
            this.ContenedorTreeView.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTitulo
            // 
            this.toolTitulo.BackColor = System.Drawing.Color.DarkGray;
            this.toolTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolVentana});
            this.toolTitulo.Location = new System.Drawing.Point(0, 0);
            this.toolTitulo.Name = "toolTitulo";
            this.toolTitulo.Size = new System.Drawing.Size(482, 27);
            this.toolTitulo.TabIndex = 5;
            this.toolTitulo.Text = "toolAcciones";
            // 
            // toolVentana
            // 
            this.toolVentana.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolVentana.Name = "toolVentana";
            this.toolVentana.Size = new System.Drawing.Size(180, 24);
            this.toolVentana.Text = "Editor de fórmulas";
            // 
            // toolAcciones
            // 
            this.toolAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAsignar,
            this.toolCerrar});
            this.toolAcciones.Location = new System.Drawing.Point(0, 27);
            this.toolAcciones.Name = "toolAcciones";
            this.toolAcciones.Size = new System.Drawing.Size(482, 25);
            this.toolAcciones.TabIndex = 6;
            this.toolAcciones.Text = "toolEmpresa";
            // 
            // toolAsignar
            // 
            this.toolAsignar.Image = ((System.Drawing.Image)(resources.GetObject("toolAsignar.Image")));
            this.toolAsignar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAsignar.Name = "toolAsignar";
            this.toolAsignar.Size = new System.Drawing.Size(67, 22);
            this.toolAsignar.Text = "Asignar";
            this.toolAsignar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolAsignar.Click += new System.EventHandler(this.toolAsignar_Click);
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
            // treeFormulas
            // 
            this.treeFormulas.AllowDrop = true;
            this.treeFormulas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeFormulas.Location = new System.Drawing.Point(0, 0);
            this.treeFormulas.Name = "treeFormulas";
            this.treeFormulas.Size = new System.Drawing.Size(218, 229);
            this.treeFormulas.TabIndex = 7;
            this.treeFormulas.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeFormulas_ItemDrag);
            // 
            // txtFormula
            // 
            this.txtFormula.AllowDrop = true;
            this.txtFormula.Location = new System.Drawing.Point(65, 287);
            this.txtFormula.Multiline = true;
            this.txtFormula.Name = "txtFormula";
            this.txtFormula.Size = new System.Drawing.Size(408, 43);
            this.txtFormula.TabIndex = 8;
            this.txtFormula.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtFormula_DragDrop);
            this.txtFormula.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtFormula_DragEnter);
            // 
            // treeOperadores
            // 
            this.treeOperadores.AllowDrop = true;
            this.treeOperadores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeOperadores.Location = new System.Drawing.Point(0, 0);
            this.treeOperadores.Name = "treeOperadores";
            treeNode1.Name = "(";
            treeNode1.Text = "(";
            treeNode2.Name = ")";
            treeNode2.Text = ")";
            treeNode3.Name = "+";
            treeNode3.Text = "+";
            treeNode4.Name = "-";
            treeNode4.Text = "-";
            treeNode5.Name = "*";
            treeNode5.Text = "*";
            treeNode6.Name = "/";
            treeNode6.Text = "/";
            treeNode7.Name = "Operadores";
            treeNode7.Text = "Operadores";
            this.treeOperadores.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.treeOperadores.Size = new System.Drawing.Size(260, 229);
            this.treeOperadores.TabIndex = 9;
            this.treeOperadores.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeOperadores_ItemDrag);
            // 
            // ContenedorTreeView
            // 
            this.ContenedorTreeView.Dock = System.Windows.Forms.DockStyle.Top;
            this.ContenedorTreeView.Location = new System.Drawing.Point(0, 52);
            this.ContenedorTreeView.Name = "ContenedorTreeView";
            // 
            // ContenedorTreeView.Panel1
            // 
            this.ContenedorTreeView.Panel1.Controls.Add(this.treeFormulas);
            // 
            // ContenedorTreeView.Panel2
            // 
            this.ContenedorTreeView.Panel2.Controls.Add(this.treeOperadores);
            this.ContenedorTreeView.Size = new System.Drawing.Size(482, 229);
            this.ContenedorTreeView.SplitterDistance = 218;
            this.ContenedorTreeView.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Fórmula:";
            // 
            // frmEditorFormulas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 353);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ContenedorTreeView);
            this.Controls.Add(this.txtFormula);
            this.Controls.Add(this.toolAcciones);
            this.Controls.Add(this.toolTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditorFormulas";
            this.Text = "Formulas";
            this.Load += new System.EventHandler(this.frmEditorFormulas_Load);
            this.toolTitulo.ResumeLayout(false);
            this.toolTitulo.PerformLayout();
            this.toolAcciones.ResumeLayout(false);
            this.toolAcciones.PerformLayout();
            this.ContenedorTreeView.Panel1.ResumeLayout(false);
            this.ContenedorTreeView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ContenedorTreeView)).EndInit();
            this.ContenedorTreeView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolTitulo;
        internal System.Windows.Forms.ToolStripLabel toolVentana;
        internal System.Windows.Forms.ToolStrip toolAcciones;
        internal System.Windows.Forms.ToolStripButton toolAsignar;
        private System.Windows.Forms.ToolStripButton toolCerrar;
        private System.Windows.Forms.TreeView treeFormulas;
        private System.Windows.Forms.TextBox txtFormula;
        private System.Windows.Forms.TreeView treeOperadores;
        private System.Windows.Forms.SplitContainer ContenedorTreeView;
        private System.Windows.Forms.Label label1;
    }
}