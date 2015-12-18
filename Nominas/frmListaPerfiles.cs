using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Nominas
{
    public partial class frmListaPerfiles : Form
    {
        public frmListaPerfiles()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        SqlConnection cnx;
        SqlCommand cmd;
        Perfil.Core.PerfilesHelper ph;
        List<Perfil.Core.Perfiles> lstPerfiles;
        #endregion

        private void frmListaPerfiles_Load(object sender, EventArgs e)
        {
            dgvPerfiles.RowHeadersVisible = false;
            CargaPerfil();
            ListaPerfiles();
        }

        private void ListaPerfiles()
        {
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ph = new Perfil.Core.PerfilesHelper();
            ph.Command = cmd;

            try
            {
                cnx.Open();
                lstPerfiles = ph.obtenerPerfiles();
                cnx.Close();
                cnx.Dispose();

                var perfil = from p in lstPerfiles select new { Id = p.idperfil, Nombre = p.nombre };

                dgvPerfiles.DataSource = perfil.ToList();

                for (int i = 0; i < dgvPerfiles.Columns.Count; i++)
                {
                    dgvPerfiles.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message,"Error");
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Perfiles");

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
                    var perfiles = from p in lstPerfiles
                                 select new
                                 {
                                     Id = p.idperfil,
                                     Nombre = p.nombre,
                                 };
                    dgvPerfiles.DataSource = perfiles.ToList();
                }
                else
                {
                    var busqueda = from b in lstPerfiles
                                   where b.nombre.Contains(txtBuscar.Text.ToUpper())
                                   select new
                                   {
                                       Id = b.idperfil,
                                       Nombre = b.nombre,
                                   };
                    dgvPerfiles.DataSource = busqueda.ToList();
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar perfil...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }
        #endregion

        private void dgvPerfiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Seleccion(1);
        }

        private void Seleccion(int edicion)
        {
            int fila = dgvPerfiles.CurrentCell.RowIndex;
            frmPerfiles p = new frmPerfiles();
            p.MdiParent = this.MdiParent;
            p.OnNuevoPerfil += p_OnNuevoPerfil;
            if (!edicion.Equals(GLOBALES.NUEVO))
            {
                p._idperfil = int.Parse(dgvPerfiles.Rows[fila].Cells[0].Value.ToString());
            }
            p._tipoOperacion = edicion;
            p.Show();
        }

        void p_OnNuevoPerfil(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaPerfiles();
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
            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar el perfil?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                int fila = dgvPerfiles.CurrentCell.RowIndex;
                int idperfil = int.Parse(dgvPerfiles.Rows[fila].Cells[0].Value.ToString());
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                ph = new Perfil.Core.PerfilesHelper();
                ph.Command = cmd;
                
                Perfil.Core.Perfiles p = new Perfil.Core.Perfiles();
                p.idperfil = idperfil;

                try
                {
                    cnx.Open();
                    ph.bajaPerfil(p);
                    cnx.Close();
                    cnx.Dispose();
                    ListaPerfiles();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
            }
        }
    }
}
