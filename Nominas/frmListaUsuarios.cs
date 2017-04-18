using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Nominas
{
    public partial class frmListaUsuarios : Form
    {
        public frmListaUsuarios()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Usuarios.Core.Usuarios> lstUsuarios;
        #endregion

        private void frmListaUsuarios_Load(object sender, EventArgs e)
        {
            dgvUsuarios.RowHeadersVisible = false;
            CargaPerfil();
            ListaUsuarios();
        }

        private void ListaUsuarios()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Usuarios.Core.UsuariosHelper uh = new Usuarios.Core.UsuariosHelper();
            uh.Command = cmd;

            try
            {
                cnx.Open();
                lstUsuarios = uh.obtenerUsuarios();
                cnx.Close();
                cnx.Dispose();

                var usr = from u in lstUsuarios
                         select new
                         {
                             Id = u.idusuario,
                             Nombre = u.nombre,
                             FechaRegistro = u.fecharegistro,
                         };
                dgvUsuarios.DataSource = usr.ToList();

                for (int i = 0; i < dgvUsuarios.Columns.Count; i++)
                {
                    dgvUsuarios.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Usuarios");

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
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            int fila = dgvUsuarios.CurrentCell.RowIndex;
            int idusuario = int.Parse(dgvUsuarios.Rows[fila].Cells[0].Value.ToString());

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Usuarios.Core.UsuariosHelper uh = new Usuarios.Core.UsuariosHelper();
            uh.Command = cmd;

            Usuarios.Core.Usuarios usuario = new Usuarios.Core.Usuarios();
            usuario.idusuario = idusuario;

            try
            {
                DialogResult respuesta = MessageBox.Show("¿Quiere eliminar el usuario?", "Confirmación", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    cnx.Open();
                    uh.bajaUsuario(usuario);
                    cnx.Close();
                    cnx.Dispose();
                    ListaUsuarios();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
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
                    var usr = from u in lstUsuarios
                                   select new
                                   {
                                       Id = u.idusuario,
                                       Nombre = u.nombre,
                                       FechaRegistro = u.fecharegistro
                                   };
                    dgvUsuarios.DataSource = usr.ToList();
                }
                else
                {
                    var busqueda = from b in lstUsuarios
                                   where b.nombre.Contains(txtBuscar.Text.ToUpper())
                                   select new
                                   {
                                       Id = b.idusuario,
                                       Nombre = b.nombre,
                                       FechaRegistro = b.fecharegistro
                                   };
                    dgvUsuarios.DataSource = busqueda.ToList();
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar usuario...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void Seleccion(int edicion)
        {
            int fila = 0;
            frmUsuarios u = new frmUsuarios();
            u.MdiParent = this.MdiParent;
            u.StartPosition = FormStartPosition.CenterParent;
            u.OnNuevoUsuario += u_OnNuevoUsuario;
            if (!edicion.Equals(GLOBALES.NUEVO))
            {
                fila = dgvUsuarios.CurrentCell.RowIndex;
                u._idusuario = int.Parse(dgvUsuarios.Rows[fila].Cells[0].Value.ToString());
            }
            u._tipoOperacion = edicion;
            u.Show();
        }

        void u_OnNuevoUsuario(int edicion)
        {
            if (edicion == GLOBALES.NUEVO || edicion == GLOBALES.MODIFICAR)
                ListaUsuarios();
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Seleccion(GLOBALES.CONSULTAR);
        }
    }
}
