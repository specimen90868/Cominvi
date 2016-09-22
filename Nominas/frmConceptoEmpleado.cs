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
    public partial class frmConceptoEmpleado : Form
    {
        public frmConceptoEmpleado()
        {
            InitializeComponent();
        }

        #region VARIABLES PUBLICAS
        public int _idEmpleado;
        public string _nombreEmpleado;
        #endregion

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Conceptos.Core.ConceptosHelper ch;
        List<Conceptos.Core.Conceptos> lstConcepto;
        List<Conceptos.Core.ConceptoTrabajador> lstCT;
        #endregion

        private void ListaConceptosEmpleado()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Conceptos.Core.ConceptoTrabajador ct = new Conceptos.Core.ConceptoTrabajador();
            ct.idempleado = _idEmpleado;

            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            concepto.idempresa = GLOBALES.IDEMPRESA;

            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            try
            {
                int periodo = 0;
                cnx.Open();
                periodo = int.Parse(eh.obtenerDiasPeriodo(_idEmpleado).ToString());
                lstConcepto = ch.obtenerConceptos(concepto, periodo);
                lstCT = ch.obtenerConceptosTrabajador(ct);
                cnx.Close();
                cnx.Dispose();

                var conceptotrab = from ctrab in lstCT join con in lstConcepto on ctrab.idconcepto equals con.id
                         select new
                         {
                             Id = ctrab.id,
                             Concepto = con.concepto,
                             TipoConcepto = (con.tipoconcepto == "P") ? "PERCEPCION" : "DEDUCCION"
                         };
                dgvConceptosEmpleado.DataSource = conceptotrab.ToList();

                for (int i = 0; i < dgvConceptosEmpleado.Columns.Count; i++)
                {
                    dgvConceptosEmpleado.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }

            dgvConceptosEmpleado.Columns["Id"].Visible = false;

            int contenido = dgvConceptosEmpleado.Rows.Count;
            if (contenido == 0)
                toolBaja.Enabled = false;
            else
                toolBaja.Enabled = true;
        }

        private void frmConceptoEmpleado_Load(object sender, EventArgs e)
        {
            dgvConceptosEmpleado.RowHeadersVisible = false;
            toolTitulo.Text += " - " + _nombreEmpleado;
            ListaConceptosEmpleado();
            CargaPerfil();
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Concepto - Empleado");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Crear":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Baja": toolBaja.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                }
            }
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            frmBuscaConcepto bc = new frmBuscaConcepto();
            bc.OnConcepto += bc_OnConcepto;
            bc._idEmpleado = _idEmpleado;
            bc._nombreEmpleado = _nombreEmpleado;
            bc.ShowDialog();
        }

        void bc_OnConcepto(int id)
        {
            _idEmpleado = id;
            ListaConceptosEmpleado();
        }

        private void toolBaja_Click(object sender, EventArgs e)
        {
            int fila = dgvConceptosEmpleado.CurrentCell.RowIndex;
            int id = int.Parse(dgvConceptosEmpleado.Rows[fila].Cells[0].Value.ToString());

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Conceptos.Core.ConceptoTrabajador ct = new Conceptos.Core.ConceptoTrabajador();
            ct.id = id;

            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar el concepto?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    cnx.Open();
                    ch.eliminaConceptoTrabajador(ct);
                    cnx.Close();
                    cnx.Dispose();
                    ListaConceptosEmpleado();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
            }
        }
    }
}
