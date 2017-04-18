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
    public partial class frmListaImss : Form
    {
        public frmListaImss()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Imss.Core.Imss> lstImss;
        Imss.Core.ImssHelper ih;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        #endregion

        private void ListaImss()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ih = new Imss.Core.ImssHelper();
            ih.Command = cmd;

            try
            {
                cnx.Open();
                lstImss = ih.ObtenerImss();
                cnx.Close();
                cnx.Dispose();

                var imss = from i in lstImss
                          select new
                          {
                              Id = i.id,
                              Prestacion = i.prestacion,
                              Porcentaje = i.porcentaje,
                              SeCalcula = i.calculo
                          };

                dgvImss.DataSource = imss.ToList();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.Alignment = DataGridViewContentAlignment.MiddleRight;
            estilo.Format = "n6";         
            dgvImss.Columns[2].DefaultCellStyle = estilo;

            dgvImss.Columns["Id"].Visible = false;

            for (int i = 0; i < dgvImss.Columns.Count; i++)
            {
                dgvImss.AutoResizeColumn(i);
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("IMSS");

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

        private void frmListaImss_Load(object sender, EventArgs e)
        {
            dgvImss.RowHeadersVisible = false;
            ListaImss();
            CargaPerfil();
        }

        private void Seleccion(int edicion)
        {
            frmImss i = new frmImss();
            i.OnNuevoImss += i_OnNuevoImss;
            i.MdiParent = this.MdiParent;
            int fila = 0;
            if (!edicion.Equals(GLOBALES.NUEVO))
            {
                fila = dgvImss.CurrentCell.RowIndex;
                i._idImss = int.Parse(dgvImss.Rows[fila].Cells[0].Value.ToString());
            }
            i._tipoOperacion = edicion;
            i.Show();
        }

        void i_OnNuevoImss(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaImss();
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
            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar la prestación?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                int fila = dgvImss.CurrentCell.RowIndex;
                int id = int.Parse(dgvImss.Rows[fila].Cells[0].Value.ToString());
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                ih = new Imss.Core.ImssHelper();
                ih.Command = cmd;
                Imss.Core.Imss imss = new Imss.Core.Imss();
                imss.id = id;

                try
                {
                    cnx.Open();
                    ih.eliminarImss(imss);
                    cnx.Close();
                    cnx.Dispose();
                    ListaImss();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
            }
        }
    }
}
