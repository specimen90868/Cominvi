using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

using System.IO;

namespace Nominas
{
    public static class GLOBALES
    {
        #region CONSTANTES GLOBALES
        public static int NUEVO = 0;
        public static int CONSULTAR = 1;
        public static int MODIFICAR = 2;
        public static int EMPRESAS = 100;
        public static int EMPLEADOS = 101;
        public static int CLIENTES = 102;
        #endregion

        #region VARIABLES TIPO PERSONA
        public static int pEMPRESA = 0;
        public static int pCLIENTE = 1;
        public static int pEMPLEADO = 2;
        #endregion

        #region VARIABLES TIPO DIRECCION
        public static int dFISCAL = 0;
        public static int dSUCURSAL = 1;
        public static int dPERSONAL = 2;
        #endregion

        public static int IDUSUARIO { get; set; }
        public static int IDPLAZA { get; set; }
        public static int IDPERFIL { get; set; }
        public static int IDEMPRESA { get; set; }
        public static string NOMBREEMPRESA { get; set; }
        public static int SESION { get; set; }

        public static string VALIDAR(Control control, Type tipo)
        {
            string nombre = "";
            var controls = control.Controls.Cast<Control>();
            
            foreach (Control c in controls.Where(c => c.GetType() == tipo))
            {
                if (string.IsNullOrEmpty(c.Text))
                {
                    nombre = c.Name.Substring(3);
                    break;
                }
            }
            
            return nombre;
        }

        public static void LIMPIAR(Control control, Type tipo)
        {
            var controls = control.Controls.Cast<Control>();

            foreach (Control c in controls.Where(c => c.GetType() == tipo))
            {
                c.Text = "";
            }
            
        }

        public static void INHABILITAR(Control control, Type tipo)
        {
            var controls = control.Controls.Cast<Control>();

            foreach (Control c in controls.Where(c => c.GetType() == tipo))
            {
                c.Enabled = false;
            }

        }

        public static void REFRESCAR(Control control, Type tipo)
        {
            var controls = control.Controls.Cast<Control>();

            foreach (Control c in controls.Where(c => c.GetType() == tipo))
            {
                c.Refresh();
            }

        }

        public static List<Autorizaciones.Core.Ediciones> PERFILEDICIONES(string menu)
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            MySqlConnection cnx = new MySqlConnection(cdn);
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            Autorizaciones.Core.AutorizacionHelper ah = new Autorizaciones.Core.AutorizacionHelper();
            ah.Command = cmd;
            List<Autorizaciones.Core.Ediciones> lstEdiciones = null;
            try 
            {
                cnx.Open();
                lstEdiciones = ah.getEdiciones(IDPERFIL.ToString(), menu);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message,"Error");
            }
            return lstEdiciones;
        }

        public static Byte[] IMAGEN_BYTES(Image imagen)
        {
            MemoryStream ms = new MemoryStream();
            imagen.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }

        public static Image BYTES_IMAGEN(Byte[] Arreglo)
        {
            MemoryStream ms = new MemoryStream(Arreglo);
            Image img = Image.FromStream(ms);
            return img;
        }
    }   
}