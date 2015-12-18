using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nominas
{
    public partial class frmListaModificacionesSua : Form
    {
        public frmListaModificacionesSua()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Modificaciones.Core.Modificaciones> lstMod;
        List<Empleados.Core.Empleados> lstEmpleados;
        Modificaciones.Core.ModificacionesHelper mh;
        Empleados.Core.EmpleadosHelper eh;
        FolderBrowserDialog ubicacion;
        StreamWriter sw;
        #endregion

        private void ListaEmpleados()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            mh = new Modificaciones.Core.ModificacionesHelper();
            eh = new Empleados.Core.EmpleadosHelper();
            mh.Command = cmd;
            eh.Command = cmd;

            Modificaciones.Core.Modificaciones mod = new Modificaciones.Core.Modificaciones();
            mod.idempresa = GLOBALES.IDEMPRESA;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.estatus = GLOBALES.ACTIVO;

            try
            {
                cnx.Open();
                lstMod = mh.obtieneModificaciones(mod);
                lstEmpleados = eh.obtenerEmpleados(empleado);
                cnx.Close();
                cnx.Dispose();

                var modif = from m in lstMod
                         join t in lstEmpleados on m.idtrabajador equals t.idtrabajador
                         select new
                         {
                             RegistroPatronal = m.registropatronal,
                             Nss = m.nss,
                             Nombre = t.nombrecompleto,
                             Modificacion = m.fecha,
                             Integrado = m.sdi
                         };
                dgvModSua.DataSource = modif.ToList();

                for (int i = 0; i < dgvModSua.Columns.Count; i++)
                {
                    dgvModSua.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void frmListaModificacionesSua_Load(object sender, EventArgs e)
        {
            dgvModSua.RowHeadersVisible = false;
            ListaEmpleados();
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Modificaciones");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Filtrar":
                        toolFiltrar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Exportar": toolExportar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                }
            }
        }

        private void toolFiltrar_Click(object sender, EventArgs e)
        {
            frmFiltro f = new frmFiltro();
            f.OnFecha += f_OnFecha;
            f.MdiParent = this.MdiParent;
            f.Show();
        }

        void f_OnFecha(DateTime desde, DateTime hasta)
        {
            if (desde.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") && hasta.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
            {
                var modif = from m in lstMod
                         join t in lstEmpleados on m.idtrabajador equals t.idtrabajador
                         select new
                         {
                             RegistroPatronal = m.registropatronal,
                             Nss = m.nss,
                             Nombre = t.nombrecompleto,
                             Modificacion = m.fecha,
                             Integrado = m.sdi
                         };
                dgvModSua.DataSource = modif.ToList();
            }
            else
            {
                var modif = from m in lstMod
                         join t in lstEmpleados on m.idtrabajador equals t.idtrabajador
                         where (m.fecha >= new DateTime(desde.Year, desde.Month, desde.Day) && m.fecha <= new DateTime(hasta.Year, hasta.Month, hasta.Day))
                         select new
                         {
                             RegistroPatronal = m.registropatronal,
                             Nss = m.nss,
                             Nombre = t.nombrecompleto,
                             Modificacion = m.fecha,
                             Integrado = m.sdi
                         };
                dgvModSua.DataSource = modif.ToList();
            }

            for (int i = 0; i < dgvModSua.Columns.Count; i++)
            {
                dgvModSua.AutoResizeColumn(i);
            }
        }

        private void toolExportar_Click(object sender, EventArgs e)
        {
            ubicacion = new FolderBrowserDialog();
            ubicacion.Description = "Seleccion la carpeta";
            ubicacion.RootFolder = Environment.SpecialFolder.Desktop;
            ubicacion.ShowNewFolderButton = true;
            if (DialogResult.OK == ubicacion.ShowDialog())
            {
                workMod.RunWorkerAsync();
            }
        }

        private void workMod_DoWork(object sender, DoWorkEventArgs e)
        {
            string linea1 = "";

            try
            {
                using (sw = new StreamWriter(ubicacion.SelectedPath + @"\Mod_Sua.txt"))
                {
                    for (int i = 0; i < dgvModSua.Rows.Count; i++)
                    {
                        linea1 = "";
                        DateTime modificacion = DateTime.Parse(dgvModSua.Rows[i].Cells["Modificacion"].Value.ToString());
                        double integrado = double.Parse(dgvModSua.Rows[i].Cells["Integrado"].Value.ToString());
                        decimal dIntegrado = Decimal.Round((decimal)integrado, 2);
                        int iIntegrado = (int)(dIntegrado * 100);
                        linea1 += dgvModSua.Rows[i].Cells["RegistroPatronal"].Value.ToString();
                        linea1 += dgvModSua.Rows[i].Cells["Nss"].Value.ToString();
                        linea1 += "07";
                        linea1 += modificacion.ToString("ddMMyyyy");
                        linea1 += (" ").ToString().PadLeft(8);
                        linea1 += "00";
                        linea1 += iIntegrado.ToString("D7");
                        sw.WriteLine(linea1);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message,"Error");
            }

            workMod.ReportProgress(100);
        }

        private void workMod_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Archivo generado con exito", "Confirmación");
        }
    }
}
