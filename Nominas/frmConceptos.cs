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
        string concepto;
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
            concepto.noconcepto = int.Parse(txtNoConcepto.Text);
            concepto.formula = txtFormula.Text;
            concepto.formulaexento = txtExento.Text;
            concepto.gravado = chkGrava.Checked;
            concepto.exento = chkExenta.Checked;
            concepto.visible = chkVisible.Checked;
            concepto.gruposat = txtGrupoSat.Text;
            concepto.periodo = int.Parse(cmbPeriodo.SelectedValue.ToString());

            Conceptos.Core.ConceptosEmpresa cempresa = new Conceptos.Core.ConceptosEmpresa();
            cempresa.idempresa = GLOBALES.IDEMPRESA;
            cempresa.noconcepto = int.Parse(txtNoConcepto.Text);
            cempresa.asignacion = false;
            cempresa.periodo = int.Parse(cmbPeriodo.SelectedValue.ToString());

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        ch.insertaConcepto(concepto);
                        cempresa.idconcepto = ch.obtenerConcepto(GLOBALES.IDEMPRESA, concepto.noconcepto, concepto.periodo);
                        ch.insertaConceptoEmpresa(cempresa);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar el concepto. \r\n \r\n Error: " + error.Message);
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
                        MessageBox.Show("Error al actualizar el concepto. \r\n \r\n Error: " + error.Message);
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
            {
                TipoConcepto = "P";
                concepto = "SAT PERCEPCIONES";
            }
            else
            {
                TipoConcepto = "D";
                concepto = "SAT DEDUCCIONES";
            }
                
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
            txtFormula.Text = "0";
            txtExento.Text = "0";

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
            periodo.idempresa = GLOBALES.IDEMPRESA;

            List<Periodos.Core.Periodos> lstPeriodos = new List<Periodos.Core.Periodos>();

            try
            {
                cnx.Open();
                lstPeriodos = ph.obtenerPeriodos(periodo);
                cnx.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener los periodos de la empresa.", "Error");
                cnx.Close();
                return;
            }

            cmbPeriodo.DataSource = lstPeriodos.ToList();
            cmbPeriodo.DisplayMember = "pago";
            cmbPeriodo.ValueMember = "dias";
            
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {

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
                        txtNoConcepto.Text = lstConcepto[i].noconcepto.ToString();
                        cmbTipo.SelectedIndex = (lstConcepto[i].tipoconcepto == "P") ? 0 : 1;
                        txtFormula.Text = lstConcepto[i].formula;
                        txtExento.Text = lstConcepto[i].formulaexento;
                        chkVisible.Checked = lstConcepto[i].visible;
                        txtGrupoSat.Text = lstConcepto[i].gruposat;
                        chkGrava.Checked = lstConcepto[i].gravado;
                        chkExenta.Checked = lstConcepto[i].exento;
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
                    GLOBALES.INHABILITAR(this, typeof(CheckBox));
                }
                else
                    toolTitulo.Text = "Edición concepto";
            }
        }

        private void btnEditor_Click(object sender, EventArgs e)
        {
            frmEditorFormulas ef = new frmEditorFormulas();
            ef.OnFormula += ef_OnFormula;
            ef._tipo = 0;
            ef.ShowDialog();
        }

        void ef_OnFormula(string formula, int tipo)
        {
            if (tipo == 0)
                txtFormula.Text = formula;
            else
                txtExento.Text = formula;
        }

        private void btnEditor2_Click(object sender, EventArgs e)
        {
            frmEditorFormulas ef = new frmEditorFormulas();
            ef.OnFormula += ef_OnFormula;
            ef._tipo = 1;
            ef.ShowDialog();
        }

        private void btnGrupoSat_Click(object sender, EventArgs e)
        {
            if (concepto != null)
            {
                frmGrupoSat gs = new frmGrupoSat();
                gs.OnSeleccion += gs_OnSeleccion;
                gs._percepcionDeduccion = TipoConcepto;
                gs.Show();
            }
            else
                MessageBox.Show("No ha seleccionado el tipo de concepto.", "Información");
        }

        void gs_OnSeleccion(string grupo)
        {
            txtGrupoSat.Text = grupo;
        }

        private void txtNoConcepto_Leave(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Conceptos.Core.Conceptos c = new Conceptos.Core.Conceptos();
            c.idempresa = GLOBALES.IDEMPRESA;

            int existe = 0;
            try
            {
                int.Parse(txtNoConcepto.Text);
                c.noconcepto = int.Parse(txtNoConcepto.Text);
                c.periodo = int.Parse(cmbPeriodo.SelectedValue.ToString());
                cnx.Open();
                existe = (int)ch.existeNoConcepto(c);
                cnx.Close();
                cnx.Dispose();
            }
            catch
            {
                MessageBox.Show("Solo se admiten números.", "Error");
                txtNoConcepto.Text = "0";
                toolGuardarCerrar.Enabled = false;
                toolGuardarNuevo.Enabled = false;
                return;
            }

            if (existe != 0)
            {
                MessageBox.Show("El número de concepto elegido ya se encuentra asignado.", "Error");
                txtNoConcepto.Text = "0";
                toolGuardarCerrar.Enabled = false;
                toolGuardarNuevo.Enabled = false;
                return;
            }
            else
            {
                toolGuardarCerrar.Enabled = true;
                toolGuardarNuevo.Enabled = true;
            }
        }
    }
}
