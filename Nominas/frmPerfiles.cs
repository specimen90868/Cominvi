using MySql.Data.MySqlClient;
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

namespace Nominas
{
    public partial class frmPerfiles : Form
    {
        public frmPerfiles()
        {
            InitializeComponent();
        }

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        public int _idperfil;
        #endregion

        #region VARIABLES GLOBALES
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        MySqlConnection cnx;
        MySqlCommand cmd;
        Perfil.Core.PerfilesHelper ph;
        Autorizaciones.Core.AutorizacionHelper auth;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoPerfil(int edicion);
        public event delOnNuevoPerfil OnNuevoPerfil;
        #endregion

        private void toolGuardarCerrar_Click(object sender, EventArgs e)
        {
            Guardar(1);
        }

        private void toolGuardarNuevo_Click(object sender, EventArgs e)
        {
            Guardar(0);
        }

        private void Guardar(int tipoGuardar)
        {//SE VALIDA SI TODOS LOS TEXTBOX HAN SIDO LLENADOS.
            //string control = GLOBALES.VALIDAR(this, typeof(TextBox));
            //if (!control.Equals(""))
            //{
            //    MessageBox.Show("Falta el campo: " + control, "Información");
            //    return;
            //}

            int idperfil;

            cnx = new MySqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new MySqlCommand();
            cmd.Connection = cnx;

            ph = new Perfil.Core.PerfilesHelper();
            ph.Command = cmd;

            ///ASIGNACION DEL NOMBRE DEL PERFIL A LA CLASE PERFILES
            Perfil.Core.Perfiles p = new Perfil.Core.Perfiles();
            p.nombre = txtNombre.Text;

            ///ASIGNACION DE LOS CHECKBOXES A LA CLASE AUTORIZACION
            List<Autorizaciones.Core.Autorizacion> lstAutorizacion = new List<Autorizaciones.Core.Autorizacion>();
            
            var controls = this.Controls.Cast<Control>();
            foreach (Control c in controls.Where(c => c.GetType() == typeof(CheckBox)))
            {
                Autorizaciones.Core.Autorizacion a = new Autorizaciones.Core.Autorizacion();
                switch ((c as CheckBox).Text)
                {
                    case "Recursos Humanos":
                        a.idacceso = 1;
                        a.acceso = int.Parse(chkRecursosHumanos.Checked.ToString());
                        break;
                    case "Seguro Social":
                        a.idacceso = 2;
                        a.acceso = int.Parse(chkSeguroSocial.Checked.ToString());
                        break;
                    case "Contratos":
                        a.idacceso = 3;
                        a.acceso = int.Parse(chkContratos.Checked.ToString());
                        break;
                    case "Nominas":
                        a.idacceso = 4;
                        a.acceso = int.Parse(chkNominas.Checked.ToString());
                        break;
                    case "Catálogos":
                        a.idacceso = 5;
                        a.acceso = int.Parse(chkCatalogos.Checked.ToString());
                        break;
                    case "Configuración":
                        a.idacceso = 6;
                        a.acceso = int.Parse(chkConfiguracion.Checked.ToString());
                        break;
                }
                lstAutorizacion.Add(a);
            }

            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        ph.insertaPerfil(p);
                        idperfil = (int)ph.obtenerIdPerfil(p);
                        auth.insertaAutorizacion(idperfil, lstAutorizacion);
                        cnx.Close();
                        cnx.Dispose();

                        if (OnNuevoPerfil != null)
                            OnNuevoPerfil(0);
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    }
                    break;
                case 2:
                    try
                    {
                        cnx.Open();
                        p.idperfil = _idperfil;
                        ph.actualizaPerfil(p);
                        cnx.Close();
                        cnx.Dispose();

                        if (OnNuevoPerfil != null)
                            OnNuevoPerfil(2);
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    }
                    break;
            }

            switch (tipoGuardar)
            {
                case 0:
                    limpiar(this, typeof(TextBox));
                    break;
                case 1:
                    this.Dispose();
                    break;
            }
        }

        #region FUNCION LIMPIAR TEXTBOX E INHABILITAR CONTROLES
        private void limpiar(Control control, Type tipo)
        {
            var controls = control.Controls.Cast<Control>();
            foreach (Control c in controls.Where(c => c.GetType() == tipo))
            {
                (c as TextBox).Clear();
            }
        }

        private void inhabilitar(Control control, Type tipo)
        {
            var controls = control.Controls.Cast<Control>();
            foreach (Control c in controls.Where(c => c.GetType() == tipo))
            {
                (c as TextBox).Enabled = false;
            }
            toolGuardarCerrar.Enabled = false;
            toolGuardarNuevo.Enabled = false;
        }
        #endregion

        private void frmPerfiles_Load(object sender, EventArgs e)
        {
            /// _tipoOperacion CONSULTA = 1, EDICION = 2
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new MySqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new MySqlCommand();
                cmd.Connection = cnx;
                ph = new Perfil.Core.PerfilesHelper();
                ph.Command = cmd;

                Perfil.Core.Perfiles p = new Perfil.Core.Perfiles();
                p.idperfil = _idperfil;
                List<Perfil.Core.Perfiles> lstPerfil;

                Autorizaciones.Core.Autorizacion auth = new Autorizaciones.Core.Autorizacion();
                auth.idperfil = _idperfil;
                List<Autorizaciones.Core.Autorizacion> lstAutorizacion;

                try
                {
                    cnx.Open();
                    lstPerfil = ph.obtenerPerfile(p);
                    cnx.Close();
                    cnx.Dispose();

                    for (int i = 0; i < lstPerfil.Count; i++)
                    {
                        txtNombre.Text = lstPerfil[i].nombre;
                    }

                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolTitulo.Text = "Consulta Perfil";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                }
                else
                    toolTitulo.Text = "Edición Perfil";
            }
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
