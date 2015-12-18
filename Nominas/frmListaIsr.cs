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
    public partial class frmListaIsr : Form
    {
        public frmListaIsr()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<TablaIsr.Core.TablaIsr> lstIsr;
        TablaIsr.Core.IsrHelper ih;
        #endregion

        private void frmListaIsr_Load(object sender, EventArgs e)
        {
            dgvIsr.RowHeadersVisible = false;
            ListaIsr();
            CargaPerfil();
        }

        private void ListaIsr()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ih = new TablaIsr.Core.IsrHelper();
            ih.Command = cmd;

            try {
                cnx.Open();
                lstIsr = ih.obtenerTablaIsr();
                cnx.Close();
                cnx.Dispose();

                var isr = from i in lstIsr
                          select new 
                          {
                              Id = i.id,
                              Inferior = i.inferior,
                              Cuota = i.cuota,
                              Porcentaje = i.porcentaje,
                              Periodo = (i.periodo == 7) ? "SEMANAL" : "QUINCENAL",
                              Anio = i.anio
                          };

                dgvIsr.DataSource = isr.ToList();
            }
            catch (Exception error) {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.Alignment = DataGridViewContentAlignment.MiddleRight;
            estilo.Format = "n6";
            dgvIsr.Columns[1].DefaultCellStyle = estilo;
            dgvIsr.Columns[2].DefaultCellStyle = estilo;
            dgvIsr.Columns[3].DefaultCellStyle = estilo;

            dgvIsr.Columns["Id"].Visible = false;

            for (int i = 0; i < dgvIsr.Columns.Count; i++)
            {
                dgvIsr.AutoResizeColumn(i);
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("ISR");

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
            frmIsr i = new frmIsr();
            i.OnNuevoIsr += i_OnNuevoIsr;
            i.MdiParent = this.MdiParent;
            int fila = 0;
            if (!edicion.Equals(GLOBALES.NUEVO))
            {
                fila = dgvIsr.CurrentCell.RowIndex;
                i._idIsr = int.Parse(dgvIsr.Rows[fila].Cells[0].Value.ToString());
            }
            i._tipoOperacion = edicion;
            i.Show();
        }

        void i_OnNuevoIsr(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaIsr();
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
            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar el registro ISR?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
                int fila = dgvIsr.CurrentCell.RowIndex;
                int id = int.Parse(dgvIsr.Rows[fila].Cells[0].Value.ToString());
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                ih = new TablaIsr.Core.IsrHelper();
                ih.Command = cmd;
                TablaIsr.Core.TablaIsr isr = new TablaIsr.Core.TablaIsr();
                isr.id = id;

                try
                {
                    cnx.Open();
                    ih.eliminaIsr(isr);
                    cnx.Close();
                    cnx.Dispose();
                    ListaIsr();
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
            var busqueda = from b in lstIsr
                           where b.periodo.ToString().Contains(periodo)
                           select new
                           {
                               Id = b.id,
                               Inferior = b.inferior,
                               Cuota = b.cuota,
                               Porcentaje = b.porcentaje,
                               Periodo = (b.periodo == 7) ? "SEMANAL" : "QUINCENAL",
                               Anio = b.anio
                           };
            dgvIsr.DataSource = busqueda.ToList();
            dgvIsr.Columns["Id"].Visible = false;
            for (int i = 0; i < dgvIsr.Columns.Count; i++)
            {
                dgvIsr.AutoResizeColumn(i);
            }
        }
    }
}
