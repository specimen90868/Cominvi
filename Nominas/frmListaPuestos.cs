using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Nominas
{
    public partial class frmListaPuestos : Form
    {
        public frmListaPuestos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        MySqlConnection cnx;
        MySqlCommand cmd;
        List<Puestos.Core.Puestos> lstPuestos;
        #endregion

        private void ListaPuestos()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new MySqlConnection(cdn);
            cmd = new MySqlCommand();
            cmd.Connection = cnx;
            Puestos.Core.PuestosHelper ph = new Puestos.Core.PuestosHelper();
            ph.Command = cmd;

            try
            {
                cnx.Open();
                lstPuestos = ph.obtenerPuestos();
                cnx.Close();
                cnx.Dispose();

                var puesto = from p in lstPuestos
                          select new
                          {
                              Id = p.id,
                              Descripcion = p.descripcion
                          };
                dgvPuestos.DataSource = puesto.ToList();

                for (int i = 0; i < dgvPuestos.Columns.Count; i++)
                {
                    dgvPuestos.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Puestos");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].nombre.ToString())
                {
                    case "Puestos":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].crear);
                        toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].consulta);
                        toolEditar.Enabled = Convert.ToBoolean(lstEdiciones[i].modificar);
                        toolBaja.Enabled = Convert.ToBoolean(lstEdiciones[i].baja);
                        break;
                }
            }
        }

        private void frmListaPuestos_Load(object sender, EventArgs e)
        {
            dgvPuestos.RowHeadersVisible = false;
            CargaPerfil();
            ListaPuestos();
        }

        private void Seleccion(int edicion)
        {
            frmPuestos p = new frmPuestos();
            p.MdiParent = this.MdiParent;
            p.OnNuevoPuesto += p_OnNuevoPuesto;
            int fila = 0;
            if (!edicion.Equals(GLOBALES.NUEVO))
            {
                fila = dgvPuestos.CurrentCell.RowIndex;
                p._idPuesto = int.Parse(dgvPuestos.Rows[fila].Cells[0].Value.ToString());
            }
            p._tipoOperacion = edicion;
            p.Show();
        }

        void p_OnNuevoPuesto(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaPuestos();
        }

        private void dgvPuestos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Seleccion(GLOBALES.CONSULTAR);
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
            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar el puesto?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
                int fila = dgvPuestos.CurrentCell.RowIndex;
                int id = int.Parse(dgvPuestos.Rows[fila].Cells[0].Value.ToString());
                cnx = new MySqlConnection(cdn);
                cmd = new MySqlCommand();
                cmd.Connection = cnx;
                Puestos.Core.PuestosHelper ph = new Puestos.Core.PuestosHelper();
                ph.Command = cmd;
                Puestos.Core.Puestos puesto = new Puestos.Core.Puestos();
                puesto.id = id;
                puesto.estatus = 0;
                try
                {
                    cnx.Open();
                    ph.bajaPuesto(puesto);
                    cnx.Close();
                    cnx.Dispose();
                    ListaPuestos();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
            }
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            txtBuscar.Font = new Font("Arial", 9);
            txtBuscar.ForeColor = System.Drawing.Color.Black;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtBuscar.Text) || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    var puesto = from p in lstPuestos
                                select new
                                {
                                    Id = p.id,
                                    Nombre = p.descripcion
                                };
                    dgvPuestos.DataSource = puesto.ToList();
                }
                else
                {
                    var busqueda = from b in lstPuestos
                                   where b.descripcion.Contains(txtBuscar.Text.ToUpper())
                                   select new
                                   {
                                       Id = b.id,
                                       Nombre = b.descripcion
                                   };
                    dgvPuestos.DataSource = busqueda.ToList();
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar puesto...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }
    }
}
