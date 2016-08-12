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
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Usuarios.Core.UsuariosHelper uh;
        Perfil.Core.PerfilesHelper ph;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoUsuario(int edicion);
        public event delOnNuevoUsuario OnNuevoUsuario;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _idusuario;
        #endregion

        private void toolGuardarCerrar_Click(object sender, EventArgs e)
        {
            guardar(1);
        }

        private void toolGuardarNuevo_Click(object sender, EventArgs e)
        {
            guardar(0);
        }

        private void guardar(int tipoGuardar)
        {
            //SE VALIDA SI TODOS LOS TEXTBOX HAN SIDO LLENADOS.
            string control = GLOBALES.VALIDAR(this, typeof(TextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            uh = new Usuarios.Core.UsuariosHelper();
            uh.Command = cmd;

            Usuarios.Core.Usuarios u = new Usuarios.Core.Usuarios();
            u.nombre = txtNombre.Text;
            u.usuario = txtUsuario.Text;
            u.activo = true;
            u.idperfil = int.Parse(cmbPerfil.SelectedValue.ToString());

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        u.password = txtPassword.Text;
                        u.fecharegistro = DateTime.Now;
                        uh.insertaUsuario(u);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar el usuario. \r\n \r\n Error: " + error.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        u.idusuario = _idusuario;
                        cnx.Open();
                        uh.modificaUsuario(u);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar el usuario. \r\n \r\n Error: " + error.Message);
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    GLOBALES.LIMPIAR(this, typeof(TextBox));
                    //limpiar(this, typeof(TextBox));
                    break;
                case 1:
                    if (OnNuevoUsuario != null)
                        OnNuevoUsuario(_tipoOperacion);
                    this.Dispose();
                    break;
            }
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            CargaCombos();
            /// _tipoOperacion CONSULTA = 1, EDICION = 2
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                uh = new Usuarios.Core.UsuariosHelper();
                uh.Command = cmd;

                DataTable dtUsuario = new DataTable();

                lblPassword.Visible = false;
                lblPassword2.Visible = false;
                txtPassword.Visible = false;
                txtPassword2.Visible = false;

                txtPassword.Text = "1";
                txtPassword2.Text = "1";

                try
                {
                    cnx.Open();
                    dtUsuario = uh.obtenerUsuario(_idusuario);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < dtUsuario.Rows.Count; i++)
                    {
                        txtNombre.Text = dtUsuario.Rows[i]["nombre"].ToString();
                        txtUsuario.Text = dtUsuario.Rows[i]["usuario"].ToString();
                        cmbPerfil.SelectedValue = int.Parse(dtUsuario.Rows[i]["idperfil"].ToString());
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolNombreVentana.Text = "Consulta Usuario";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                }
                else
                    toolNombreVentana.Text = "Edición Usuario";
            }
        }

        private void CargaCombos()
        {
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ph = new Perfil.Core.PerfilesHelper();
            ph.Command = cmd;

            List<Perfil.Core.Perfiles> lstPerfiles = new List<Perfil.Core.Perfiles>();

            try
            {
                cnx.Open();
                lstPerfiles = ph.obtenerPerfiles();
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Al obtener lista de perfiles.\r\n \r\n" + error.Message, "Error");
            }

            cmbPerfil.DataSource = lstPerfiles.ToList();
            cmbPerfil.DisplayMember = "nombre";
            cmbPerfil.ValueMember = "idperfil";
        }
    }
}
