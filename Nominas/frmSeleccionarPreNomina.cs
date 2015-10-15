using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Nominas
{
    public partial class frmSeleccionarPreNomina : Form
    {
        public frmSeleccionarPreNomina()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        CalculoNomina.Core.NominaHelper nh;
        #endregion

        #region DELEGADOS
        public delegate void delOnPreNomina(DateTime inicio, DateTime fin);
        public event delOnPreNomina OnPreNomina;
        #endregion

        private void cargaPreNomina()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            List<CalculoNomina.Core.tmpPagoNomina> lstPreNominas = new List<CalculoNomina.Core.tmpPagoNomina>();
            try
            {
                cnx.Open();
                lstPreNominas = nh.obtenerFechasPreNomina(pn);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            var fechas = from f in lstPreNominas select new { f.fechainicio, f.fechafin };
            dgvPreNomina.DataSource = fechas.ToList();
        }

        private void frmSeleccionarPreNomina_Load(object sender, EventArgs e)
        {
            dgvPreNomina.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPreNomina.RowHeadersVisible = false;
            cargaPreNomina();
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolAceptar_Click(object sender, EventArgs e)
        {
            if (OnPreNomina != null)
            {
                int fila = dgvPreNomina.CurrentRow.Index;
                OnPreNomina(DateTime.Parse(dgvPreNomina.Rows[fila].Cells[0].Value.ToString()), DateTime.Parse(dgvPreNomina.Rows[fila].Cells[1].Value.ToString()));
                this.Dispose();
            }
        }

        private void toolEliminar_Click(object sender, EventArgs e)
        {
            int fila = dgvPreNomina.CurrentRow.Index;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = DateTime.Parse(dgvPreNomina.Rows[fila].Cells[0].Value.ToString());
            pn.fechafin = DateTime.Parse(dgvPreNomina.Rows[fila].Cells[1].Value.ToString());
            pn.guardada = true;

            try
            {
                cnx.Open();
                nh.eliminaPreNomina(pn);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            cargaPreNomina();
        }
    }
}
