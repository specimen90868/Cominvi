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
using System.Management;

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

        public static int EXPORTACATALOGO_GENERAL = 0;
        public static int EXPORTACATALOGO_NOMINA = 1;
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
        public static int REINGRESO = 2;
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
        public static int EXTRAORDINARIO_NORMAL = 2;
        #endregion

        #region BUSQUEDAS
        public static int FORMULARIOS = 0;
        public static int NOMINA = 1;
        #endregion

        public static int REPORTE_TABULAR_PRENOMINA = 0;
        public static int REPORTE_TABULAR_NOMINA = 1;

        public static int IDUSUARIO { get; set; }
        public static int IDPERFIL { get; set; }
        public static int IDEMPRESA { get; set; }
        public static string NOMBREEMPRESA { get; set; }
        public static bool OBRACIVIL { get; set; }
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

        public static Boolean FORMISOPEN(String FormABuscar)
        {
            Boolean lEncontrado = false;

            foreach (Form form in Application.OpenForms)
            {

                if (form.Name == FormABuscar)
                {

                    form.WindowState = FormWindowState.Normal;

                    form.Activate();

                    lEncontrado = true;

                    break;

                }

            }

            return lEncontrado;

        }

        public static string COLUMNAS_EXCEL(int valor)
        {
            string columna = "";
            switch (valor)
            {
                case 1:
                    columna = "A";
                    break;
                case 2:
                    columna = "B";
                    break;
                case 3:
                    columna = "C";
                    break;
                case 4:
                    columna = "D";
                    break;
                case 5:
                    columna = "E";
                    break;
                case 6:
                    columna = "F";
                    break;
                case 7:
                    columna = "G";
                    break;
                case 8:
                    columna = "H";
                    break;
                case 9:
                    columna = "I";
                    break;
                case 10:
                    columna = "J";
                    break;
                case 11:
                    columna = "K";
                    break;
                case 12:
                    columna = "L";
                    break;
                case 13:
                    columna = "M";
                    break;
                case 14:
                    columna = "N";
                    break;
                case 15:
                    columna = "O";
                    break;
                case 16:
                    columna = "P";
                    break;
                case 17:
                    columna = "Q";
                    break;
                case 18:
                    columna = "R";
                    break;
                case 19:
                    columna = "S";
                    break;
                case 20:
                    columna = "T";
                    break;
                case 21:
                    columna = "U";
                    break;
                case 22:
                    columna = "V";
                    break;
                case 23:
                    columna = "W";
                    break;
                case 24:
                    columna = "X";
                    break;
                case 25:
                    columna = "Y";
                    break;
                case 26:
                    columna = "Z";
                    break;
                case 27:
                    columna = "AA";
                    break;
                case 28:
                    columna = "AB";
                    break;
                case 29:
                    columna = "AC";
                    break;
                case 30:
                    columna = "AD";
                    break;
                case 31:
                    columna = "AE";
                    break;
                case 32:
                    columna = "AF";
                    break;
                case 33:
                    columna = "AG";
                    break;
                case 34:
                    columna = "AH";
                    break;
                case 35:
                    columna = "AI";
                    break;
                case 36:
                    columna = "AJ";
                    break;
                case 37:
                    columna = "AK";
                    break;
                case 38:
                    columna = "AL";
                    break;
                case 39:
                    columna = "AM";
                    break;
                case 40:
                    columna = "AN";
                    break;
                case 41:
                    columna = "AO";
                    break;
                case 42:
                    columna = "AP";
                    break;
                case 43:
                    columna = "AQ";
                    break;
                case 44:
                    columna = "AR";
                    break;
                case 45:
                    columna = "AS";
                    break;
                case 46:
                    columna = "AT";
                    break;
                case 47:
                    columna = "AU";
                    break;
                case 48:
                    columna = "AV";
                    break;
                case 49:
                    columna = "AW";
                    break;
                case 50:
                    columna = "AX";
                    break;
                case 51:
                    columna = "AY";
                    break;
                case 52:
                    columna = "AZ";
                    break;
            }
            return columna;
        }

        //public static Boolean IDENTIFICADOR(string wmiClass, string wmiProperty, string serialHD)
        //{
        //    Boolean result = false;
        //    ManagementClass mc = new ManagementClass(wmiClass);
        //    ManagementObjectCollection moc = mc.GetInstances();
        //    foreach (ManagementObject mo in moc)
        //    {
        //        if (mo[wmiProperty].ToString() == serialHD)
        //        {
        //            result = true;
        //            break;
        //        }
        //        else
        //            result = false;
        //    }
        //    return result;
        //}

    }   
}