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
    public partial class frmConceptosEmpresa : Form
    {
        public frmConceptosEmpresa()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        SqlConnection cnx;
        SqlCommand cmd;
        #endregion

        private void frmConceptosEmpresa_Load(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos p = new Periodos.Core.Periodos();
            p.idempresa = GLOBALES.IDEMPRESA;

            List<Periodos.Core.Periodos> lstPeriodos = new List<Periodos.Core.Periodos>();

            try
            {
                cnx.Open();
                lstPeriodos = ph.obtenerPeriodos(p);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al Obtener los periodos de la empresa", "Error");
                cnx.Dispose();
            }

            cmbPeriodo.DataSource = lstPeriodos;
            cmbPeriodo.DisplayMember = "pago";
            cmbPeriodo.ValueMember = "idperiodo";
        }

        private void cmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int periodo = 0;
            if (cmbPeriodo.Text == "SEMANAL")
                periodo = 7;
            else if (cmbPeriodo.Text == "QUINCENAL")
                periodo = 15;
           
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Conceptos.Core.Conceptos c = new Conceptos.Core.Conceptos();
            c.idempresa = GLOBALES.IDEMPRESA;

            Conceptos.Core.ConceptosEmpresa ce = new Conceptos.Core.ConceptosEmpresa();
            ce.idempresa = GLOBALES.IDEMPRESA;
            ce.periodo = periodo;

            List<Conceptos.Core.Conceptos> lstConceptos = new List<Conceptos.Core.Conceptos>();
            List<Conceptos.Core.ConceptosEmpresa> lstConceptosEmpresa = new List<Conceptos.Core.ConceptosEmpresa>();

            try
            {
                cnx.Open();
                lstConceptos = ch.obtenerConceptos(c, periodo);
                lstConceptosEmpresa = ch.obtenerConceptosEmpresa(ce);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al Obtener los conceptos de la empresa.", "Error");
                cnx.Dispose();
            }

            var a = from concepto in lstConceptos
                    join cempresa in lstConceptosEmpresa on concepto.id equals cempresa.idconcepto
                    orderby concepto.noconcepto ascending
                    select new
                    {
                        concepto.id,
                        concepto.noconcepto,
                        concepto.concepto,
                        cempresa.asignacion
                    };

            DataTable dt = new DataTable();
            DataRow dtFila;
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("noconcepto", typeof(Int32));
            dt.Columns.Add("concepto", typeof(String));
            dt.Columns.Add("asignacion", typeof(Boolean));

            foreach (var b in a)
            {
                dtFila = dt.NewRow();
                dtFila["id"] = b.id;
                dtFila["noconcepto"] = b.noconcepto;
                dtFila["concepto"] = b.concepto;
                dtFila["asignacion"] = b.asignacion;
                dt.Rows.Add(dtFila);
            }

            dgvConceptos.Columns["id"].DataPropertyName = "id";
            dgvConceptos.Columns["noconcepto"].DataPropertyName = "noconcepto";
            dgvConceptos.Columns["concepto"].DataPropertyName = "concepto";
            dgvConceptos.Columns["asignacion"].DataPropertyName = "asignacion";

            dgvConceptos.DataSource = dt;
            for (int i = 0; i < dgvConceptos.Columns.Count; i++)
                dgvConceptos.AutoResizeColumn(i);
        }

        private void dgvConceptos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int periodo = 0;
            if (cmbPeriodo.Text == "SEMANAL")
                periodo = 7;
            else if (cmbPeriodo.Text == "QUINCENAL")
                periodo = 15;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Conceptos.Core.ConceptosEmpresa ce = new Conceptos.Core.ConceptosEmpresa();
            ce.idempresa = GLOBALES.IDEMPRESA;
            ce.periodo = periodo;
            ce.idconcepto = int.Parse(dgvConceptos.Rows[e.RowIndex].Cells[0].Value.ToString());
            
            if (e.ColumnIndex == 3) // 0 is the first column, specify the valid index of ur gridview
            {
                bool value = (bool)dgvConceptos.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;
                ce.asignacion = value;
            }

            try
            {
                cnx.Open();
                ch.actualizaAsignacionConcepto(ce);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al actualizar la asignación del concepto", "Error");
                cnx.Dispose();
            }
        }
    }
}
