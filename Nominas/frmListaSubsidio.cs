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
    public partial class frmListaSubsidio : Form
    {
        public frmListaSubsidio()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<TablaSubsidio.Core.TablaSubsidio> lstSubsidio;
        TablaSubsidio.Core.SubsidioHelper sh;
        #endregion

        private void ListaSubsidio()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            sh = new TablaSubsidio.Core.SubsidioHelper();
            sh.Command = cmd;

            try
            {
                cnx.Open();
                lstSubsidio = sh.obtenerTablaSubsidio();
                cnx.Close();
                cnx.Dispose();

                var sub = from s in lstSubsidio
                          select new
                          {
                              Id = s.id,
                              Desde = s.desde,
                              Cantidad = s.cantidad,
                              Periodo = (s.periodo == 7) ? "SEMANAL" : "QUINCENAL",
                              Anio = s.anio
                          };

                dgvSubsidio.DataSource = sub.ToList();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.Alignment = DataGridViewContentAlignment.MiddleRight;
            estilo.Format = "n6";
            dgvSubsidio.Columns[1].DefaultCellStyle = estilo;
            dgvSubsidio.Columns[2].DefaultCellStyle = estilo;

            dgvSubsidio.Columns["Id"].Visible = false;

            for (int i = 0; i < dgvSubsidio.Columns.Count; i++)
            {
                dgvSubsidio.AutoResizeColumn(i);
            }
        }

        private void frmListaSubsidio_Load(object sender, EventArgs e)
        {
            dgvSubsidio.RowHeadersVisible = false;
            ListaSubsidio();
            CargaPerfil();
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Subsidio");

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
            frmSubsidio s = new frmSubsidio();
            s.OnNuevoSubsidio += s_OnNuevoSubsidio;
            s.MdiParent = this.MdiParent;
            int fila = 0;
            if (!edicion.Equals(GLOBALES.NUEVO))
            {
                fila = dgvSubsidio.CurrentCell.RowIndex;
                s._idSubsidio = int.Parse(dgvSubsidio.Rows[fila].Cells[0].Value.ToString());
            }
            s._tipoOperacion = edicion;
            s.Show();
        }

        void s_OnNuevoSubsidio(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaSubsidio();
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
            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar el subsidio?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
                int fila = dgvSubsidio.CurrentCell.RowIndex;
                int id = int.Parse(dgvSubsidio.Rows[fila].Cells[0].Value.ToString());
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                sh = new TablaSubsidio.Core.SubsidioHelper();
                sh.Command = cmd;
                TablaSubsidio.Core.TablaSubsidio subsidio = new TablaSubsidio.Core.TablaSubsidio();
                subsidio.id = id;

                try
                {
                    cnx.Open();
                    sh.eliminaSubsidio(subsidio);
                    cnx.Close();
                    cnx.Dispose();
                    ListaSubsidio();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
            }
        }

        private void cmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbPeriodo.Text)
            {
                case "SEMANAL":
                    filtrado("7");
                    break;
                case "QUINCENAL":
                    filtrado("15");
                    break;
            }
        }

        private void filtrado(string periodo)
        {
            var busqueda = from b in lstSubsidio
                           where b.periodo.ToString().Contains(periodo)
                           select new
                           {
                               Id = b.id,
                               Desde = b.desde,
                               Cantidad = b.cantidad,
                               Periodo = (b.periodo == 7) ? "SEMANAL" : "QUINCENAL",
                               Anio = b.anio
                           };
            dgvSubsidio.DataSource = busqueda.ToList();
            dgvSubsidio.Columns["Id"].Visible = false;
            for (int i = 0; i < dgvSubsidio.Columns.Count; i++)
            {
                dgvSubsidio.AutoResizeColumn(i);
            }
        }
    }
}
