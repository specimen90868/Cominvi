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
    public partial class frmListaConceptos : Form
    {
        public frmListaConceptos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Conceptos.Core.Conceptos> lstConceptos;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Conceptos.Core.ConceptosHelper ch;
        #endregion

        private void ListaConceptos()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            concepto.idempresa = GLOBALES.IDEMPRESA;

            try
            {
                cnx.Open();
                lstConceptos = ch.obtenerConceptos(concepto, 0);
                cnx.Close();
                cnx.Dispose();

                var con = from c in lstConceptos
                             select new
                             {
                                 Id = c.id,
                                 Concepto = c.concepto,
                                 Tipo = (c.tipoconcepto == "P") ? "PERCEPCION" : "DEDUCCION",
                                 NoConcepto = c.noconcepto
                             };
                dgvConceptos.DataSource = con.ToList();

                for (int i = 0; i < dgvConceptos.Columns.Count; i++)
                {
                    dgvConceptos.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }

            dgvConceptos.Columns["Id"].Visible = false;
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Conceptos");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Crear":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Consultar": toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Editar": toolEditar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Eliminar": toolBaja.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                }
            }
        }

        private void Seleccion(int edicion)
        {
            frmConceptos c = new frmConceptos();
            c.OnNuevoConcepto += c_OnNuevoConcepto;
            c.MdiParent = this.MdiParent;
            int fila = 0;
            if (!edicion.Equals(GLOBALES.NUEVO))
            {
                fila = dgvConceptos.CurrentCell.RowIndex;
                c._idConcepto = int.Parse(dgvConceptos.Rows[fila].Cells[0].Value.ToString());
            }
            c._tipoOperacion = edicion;
            c.Show();
        }

        void c_OnNuevoConcepto(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaConceptos();
        }

        private void frmListaConceptos_Load(object sender, EventArgs e)
        {
            dgvConceptos.RowHeadersVisible = false;
            ListaConceptos();
            CargaPerfil();
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.NUEVO);
        }

        private void toolConsultar_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.CONSULTAR);
        }

        private void toolEditar_Click(object sender, EventArgs e)
        {
            Seleccion(GLOBALES.MODIFICAR);
        }

        private void toolBaja_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar el concepto?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
                int fila = dgvConceptos.CurrentCell.RowIndex;
                int id = int.Parse(dgvConceptos.Rows[fila].Cells[0].Value.ToString());
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                ch = new Conceptos.Core.ConceptosHelper();
                ch.Command = cmd;
                Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                concepto.id = id;

                try
                {
                    cnx.Open();
                    ch.eliminarConcepto(concepto);
                    cnx.Close();
                    cnx.Dispose();
                    ListaConceptos();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
            }
        }
    }
}
