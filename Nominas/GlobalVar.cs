using System.Data.SqlClient;
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
        #endregion

        #region VARIABLES TIPO PERSONA
        public static int pEMPRESA = 0;
        public static int pEMPLEADO = 1;
        #endregion

        #region VARIABLES TIPO DIRECCION
        public static int dFISCAL = 0;
        public static int dPERSONAL = 1;
        #endregion

        #region TIPOS DE MOVIMIENTO
        public static int mALTA = 1;
        public static int mREINGRESO = 2;
        public static int mMODIFICACIONSALARIO = 3;
        public static int mBAJA = 4;
        public static int mCAMBIOPUESTO = 5;
        public static int mCAMBIODEPARTAMENTO = 6;
        #endregion

        #region TIPOS DE ESTATUS
        public static int ACTIVO = 1;
        public static int INACTIVO = 0;
        #endregion

        #region TIPOS CREDITO INFONAVIT
        public static int dPORCENTAJE = 1;
        public static int dVSMDF = 3;
        public static int dPESOS = 2;

        public static int mCREDITO = 20;
        public static int mVALORDESCUENTO = 19;
        public static int mTIPODESCUENTO = 18;
        #endregion

        #region TIPO NOMINA
        public static int NORMAL = 0;
        public static int ESPECIAL = 1;
        public static int EXTRAORDINARIO_NORMAL = 2;
        public static int EXTRAORDINARIO_ESPECIAL = 3;
        #endregion

        public static int IDUSUARIO { get; set; }
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
            SqlConnection cnx = new SqlConnection(cdn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            Autorizaciones.Core.AutorizacionHelper ah = new Autorizaciones.Core.AutorizacionHelper();
            ah.Command = cmd;
            List<Autorizaciones.Core.Ediciones> lstEdiciones = null;
            try 
            {
                cnx.Open();
                lstEdiciones = ah.getEdiciones(IDPERFIL, menu);
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

        public static List<string> EXTRAEVARIABLES(string formula, string inicio, string fin)
        {
            List<string> coincidencias = new List<string>();
            int indexStart = 0, indexEnd = 0;
            bool exit = false;
            while (!exit)
            {
                indexStart = formula.IndexOf(inicio);
                indexEnd = formula.IndexOf(fin);
                if (indexStart != -1 && indexEnd != -1)
                {
                    coincidencias.Add(formula.Substring(indexStart + inicio.Length,
                        indexEnd - indexStart - inicio.Length));
                    formula = formula.Substring(indexEnd + fin.Length);
                }
                else
                    exit = true;
            }
            return coincidencias;
        }

    }   
}