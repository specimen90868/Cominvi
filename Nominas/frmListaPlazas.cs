using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Nominas
{
    public partial class frmListaPlazas : Form
    {
        public frmListaPlazas()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        List<Plaza.Core.Plazas> lstPlazas;
        Plaza.Core.PlazasHelper ph;
        MySqlConnection cnx;
        MySqlCommand cmd;
        #endregion

        private void frmListaPlazas_Load(object sender, EventArgs e)
        {
            dgvPlazas.RowHeadersVisible = false;
            CargaPerfil();
            ListaPlazas();
        }

        private void ListaPlazas()
        {
            cnx = new MySqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new MySqlCommand();
            cmd.Connection = cnx;

            ph = new Plaza.Core.PlazasHelper();
            ph.Command = cmd;

            try
            {
                cnx.Open();
                lstPlazas = ph.obtenerPlazas();
                cnx.Close();
                cnx.Dispose();

                var plazas = from p in lstPlazas select new { Id = p.id, Nombre = p.nombre };
                dgvPlazas.DataSource = plazas.ToList();

                for (int i = 0; i < dgvPlazas.Columns.Count; i++)
                {
                    dgvPlazas.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Plazas");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].nombre.ToString())
                {
                    case "Plazas":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].crear);
                        toolConsultar.Enabled = Convert.ToBoolean(lstEdiciones[i].consulta);
                        toolEditar.Enabled = Convert.ToBoolean(lstEdiciones[i].modificar);
                        toolBaja.Enabled = Convert.ToBoolean(lstEdiciones[i].baja);
                        break;
                }
            }
        }

        #region TEXBOX BUSCAR
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
                    var plazas = from p in lstPlazas
                             select new
                             {
                                 Id = p.id,
                                 Nombre = p.nombre,
                             };
                    dgvPlazas.DataSource = plazas.ToList();
                }
                else
                {
                    var busqueda = from b in lstPlazas
                                   where b.nombre.Contains(txtBuscar.Text.ToUpper())
                                   select new
                                   {
                                       Id = b.id,
                                       Nombre = b.nombre,
                                   };
                    dgvPlazas.DataSource = busqueda.ToList();
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar plaza...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }
        #endregion

        private void dgvPlazas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Seleccion(GLOBALES.CONSULTAR);
        }

        private void Seleccion(int edicion)
        {
            int fila = 0;
            frmPlazas p = new frmPlazas();
            p.MdiParent = this.MdiParent;
            if (!edicion.Equals(GLOBALES.NUEVO))
            { 
                fila = dgvPlazas.CurrentCell.RowIndex;
                p._idplaza = int.Parse(dgvPlazas.Rows[fila].Cells[0].Value.ToString());
            }
            p.OnNuevaPlaza += p_OnNuevaPlaza;
            p._tipoOperacion = edicion;
            p.Show();
        }

        void p_OnNuevaPlaza(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaPlazas();
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
            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar la plaza?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
                int fila = dgvPlazas.CurrentCell.RowIndex;
                int idplaza = int.Parse(dgvPlazas.Rows[fila].Cells[0].Value.ToString());
                cnx = new MySqlConnection(cdn);
                cmd = new MySqlCommand();
                cmd.Connection = cnx;
                ph = new Plaza.Core.PlazasHelper();
                ph.Command = cmd;
                Plaza.Core.Plazas p = new Plaza.Core.Plazas();
                p.id = idplaza;
                p.activo = 0;
                try
                {
                    cnx.Open();
                    ph.bajaPlaza(p);
                    cnx.Close();
                    cnx.Dispose();
                    ListaPlazas();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
            }
        }
    }
}
