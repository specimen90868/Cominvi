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
using MySql.Data.MySqlClient;

namespace Nominas
{
    public partial class frmLogIn : Form
    {
        public frmLogIn()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            MySqlConnection cnx = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();

            cnx.ConnectionString = cdn;
            cmd.Connection = cnx;
            Usuarios.Core.UsuariosHelper uh = new Usuarios.Core.UsuariosHelper();

            Usuarios.Core.Usuarios u = new Usuarios.Core.Usuarios();
            u.usuario = txtUsuario.Text;
            u.password = txtPassword.Text;

            try
            {
                uh.Command = cmd;
                cnx.Open();
                
                DataTable usr = new DataTable();
                usr = uh.ValidaUsuario(u);

                cnx.Close();
                cnx.Dispose();

                if (usr != null)
                    if (!usr.Rows.Count.Equals(0))
                    {
                        if (GLOBALES.SESION == 0)
                        {
                            GLOBALES.IDUSUARIO = int.Parse(usr.Rows[0]["idusuario"].ToString());
                            GLOBALES.IDPLAZA = int.Parse(usr.Rows[0]["plaza"].ToString());
                            GLOBALES.IDPERFIL = int.Parse(usr.Rows[0]["idperfil"].ToString());
                            GLOBALES.SESION = 1;
                            frmPrincipal p = new frmPrincipal();
                            this.Hide();
                            p.Show();
                        }
                        else
                        {
                            GLOBALES.IDUSUARIO = int.Parse(usr.Rows[0]["idusuario"].ToString());
                            GLOBALES.IDPLAZA = int.Parse(usr.Rows[0]["plaza"].ToString());
                            GLOBALES.IDPERFIL = int.Parse(usr.Rows[0]["idperfil"].ToString());
                            this.Close();
                        }
                    }
                    else
                        MessageBox.Show("Usuario y/o Contraseña no validos.", "Error");
            }
            catch (Exception error)
            {
                MessageBox.Show("Ocurrió un error al arbir la bace de datos: \r\n" + error.Message,"Error");
            }
        }

        private void frmLogIn_Load(object sender, EventArgs e)
        {
            txtUsuario.Clear();
            txtPassword.Clear();
        }
    }
}
