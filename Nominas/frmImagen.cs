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
    public partial class frmImagen : Form
    {
        public frmImagen()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Imagen.Core.ImagenesHelper ih;
        #endregion

        #region VARIABLES PUBLICAS
        public int _idpersona;
        public int _tipopersona;
        #endregion

        private void frmImagen_Load(object sender, EventArgs e)
        {
            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            byte[] imagen = null;

            ih = new Imagen.Core.ImagenesHelper();
            ih.Command = cmd;

            Imagen.Core.Imagenes img = new Imagen.Core.Imagenes();
            img.idpersona = _idpersona;
            img.tipopersona = _tipopersona;

            List<Imagen.Core.Imagenes> lstImagen = new List<Imagen.Core.Imagenes>();

            try
            {
                cnx.Open();
                lstImagen = ih.obtenerImagen(img);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener la imagen.", "Error");
                throw;
            }
            

            if (lstImagen.Count != 0)
            {
                for (int i = 0; i < lstImagen.Count; i++)
                {
                    imagen = lstImagen[i].imagen;
                }
                pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                pbImagen.Image = GLOBALES.BYTES_IMAGEN(imagen);
            }
        }
    }
}
