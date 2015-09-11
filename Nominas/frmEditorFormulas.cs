using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nominas
{
    public partial class frmEditorFormulas : Form
    {
        public frmEditorFormulas()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        List<Formulas.Core.Formulas> lstFormulas;
        Formulas.Core.FormulasHelper fh;
        #endregion

        #region DELEGADOS
        public delegate void delOnFormula(string formula, int tipo);
        public event delOnFormula OnFormula;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipo;
        #endregion

        private void frmEditorFormulas_Load(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            fh = new Formulas.Core.FormulasHelper();
            fh.Command = cmd;

            try 
            {
                cnx.Open();
                lstFormulas = fh.obtenerFormulas();
                cnx.Close(); 
                cnx.Dispose();
            }
            catch (Exception error) 
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            Base();

            treeFormulas.ExpandAll();
            treeOperadores.ExpandAll();
        }

        private void Base()
        {
            treeFormulas.Nodes.Clear();
            treeFormulas.BeginUpdate();

            for (int i = 0; i < lstFormulas.Count; i++)
            {
                if (lstFormulas[i].padre == 0)
                {
                    treeFormulas.Nodes.Add(lstFormulas[i].nombre, lstFormulas[i].nombre);
                    treeFormulas.Nodes[treeFormulas.Nodes.Count - 1].Tag = lstFormulas[i];
                }
            }

            for (int i = 0; i < treeFormulas.Nodes.Count; i++)
                Hijos(treeFormulas.Nodes[i], lstFormulas);

            treeFormulas.EndUpdate();
            treeFormulas.Refresh();

        }

        public void Hijos(TreeNode parentNode, List<Formulas.Core.Formulas> lista)
        {
            Formulas.Core.Formulas parentRed = (Formulas.Core.Formulas)parentNode.Tag;

            for (int i = parentRed.padre + 1; i < lista.Count; i++)
            {
                if (lista[i].padre == (parentRed.padre + 1))
                {
                    parentNode.Nodes.Add(lista[i].nombre, lista[i].nombre);
                    parentNode.Nodes[parentNode.Nodes.Count - 1].Tag = lista[i];
                    Hijos(parentNode.Nodes[parentNode.Nodes.Count - 1], lista);
                }
                if (lista[i].padre <= parentRed.padre) break;
            }
        }

        private void treeFormulas_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        private void txtFormula_DragEnter(object sender, DragEventArgs e)
        {
            TreeNode tn;
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                tn = (TreeNode)(e.Data.GetData(typeof(TreeNode)));
                TreeNode tPn = tn.Parent;
                if (tPn.Text == "Variables")
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else if (tPn.Text == "Operadores")
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }

            }
            else
                e.Effect = DragDropEffects.None;
        }
 
        private void txtFormula_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode tn;
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                tn = (TreeNode)(e.Data.GetData(typeof(TreeNode)));
                switch (tn.Text)
                {
                    case "(":
                        txtFormula.Text += tn.Text;
                        break;
                    case ")":
                        txtFormula.Text += tn.Text;
                        break;
                    case "+":
                        txtFormula.Text += tn.Text;
                        break;
                    case "-":
                        txtFormula.Text += tn.Text;
                        break;
                    case "*":
                        txtFormula.Text += tn.Text;
                        break;
                    case "/": 
                        txtFormula.Text += tn.Text;
                        break;
                    default: 
                        txtFormula.Text += "[" + tn.Text + "]";
                        break;
                }
            } 
        }

        private void treeOperadores_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolAsignar_Click(object sender, EventArgs e)
        {
            if (OnFormula != null)
                OnFormula(txtFormula.Text, _tipo);
        }
    }
}
