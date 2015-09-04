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
    public partial class frmConceptos : Form
    {
        public frmConceptos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Conceptos.Core.ConceptosHelper ch;
        string TipoConcepto;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _idConcepto;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoConcepto(int edicion);
        public event delOnNuevoConcepto OnNuevoConcepto;
        #endregion

        private void guardar(int tipoGuardar)
        {
            //SE VALIDA SI TODOS LOS TEXTBOX HAN SIDO LLENADOS.
            string control = GLOBALES.VALIDAR(this, typeof(TextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            concepto.idempresa = GLOBALES.IDEMPRESA;
            concepto.concepto = txtConcepto.Text;
            concepto.tipoconcepto = TipoConcepto;
            concepto.formula = txtFormula.Text;

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        ch.insertaConcepto(concepto);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar el factor. \r\n \r\n Error: " + error.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        concepto.id = _idConcepto;
                        cnx.Open();
                        ch.actualizaConcepto(concepto);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar el factor. \r\n \r\n Error: " + error.Message);
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    GLOBALES.LIMPIAR(this, typeof(TextBox));
                    break;
                case 1:
                    if (OnNuevoConcepto != null)
                        OnNuevoConcepto(_tipoOperacion);
                    this.Dispose();
                    break;
            }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.Text == "PERCEPCION")
                TipoConcepto = "P";
            else
                TipoConcepto = "D";
        }

        private void toolGuardarCerrar_Click(object sender, EventArgs e)
        {
            guardar(1);
        }

        private void toolGuardarNuevo_Click(object sender, EventArgs e)
        {
            guardar(0);
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmConceptos_Load(object sender, EventArgs e)
        {
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                ch = new Conceptos.Core.ConceptosHelper();
                ch.Command = cmd;

                Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                concepto.id = _idConcepto;

                List<Conceptos.Core.Conceptos> lstConcepto;

                try
                {
                    cnx.Open();
                    lstConcepto = ch.obtenerConcepto(concepto);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstConcepto.Count; i++)
                    {
                        txtConcepto.Text = lstConcepto[i].concepto.ToString();
                        cmbTipo.SelectedIndex = (lstConcepto[i].tipoconcepto == "P") ? 0 : 1;
                        txtFormula.Text = lstConcepto[i].formula;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta concepto";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                    GLOBALES.INHABILITAR(this, typeof(ComboBox));
                }
                else
                    toolTitulo.Text = "Edición concepto";
            }
        }

        private void btnEditor_Click(object sender, EventArgs e)
        {
            frmEditorFormulas ef = new frmEditorFormulas();
            ef.OnFormula += ef_OnFormula;
            ef.ShowDialog();
        }

        void ef_OnFormula(string formula)
        {
            txtFormula.Text = formula;
        }
    }
}
