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
    public partial class frmCambioContrasena : Form
    {
        public frmCambioContrasena()
        {
            InitializeComponent();
        }
        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Usuarios.Core.UsuariosHelper uh;
        #endregion
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals(txtPasswordConfirmacion.Text))
            {
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                uh = new Usuarios.Core.UsuariosHelper();
                uh.Command = cmd;

                Usuarios.Core.Usuarios usr = new Usuarios.Core.Usuarios();
                usr.idusuario = GLOBALES.IDUSUARIO;
                usr.password = txtPassword.Text;

                try
                {
                    cnx.Open();
                    uh.cambioPassword(usr);
                    cnx.Close();
                    cnx.Dispose();

                    MessageBox.Show("Cambio correcto.", "Confirmación");
                    this.Dispose();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Al cambiar password." + error.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Password no coinciden.", "Error");
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
