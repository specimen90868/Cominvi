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
    public partial class frmListaAusentimosSua : Form
    {
        public frmListaAusentimosSua()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Ausentismo.Core.Ausentismo> lstAusentismo;
        List<Empleados.Core.Empleados> lstEmpleados;
        Ausentismo.Core.AusentismoHelper ah;
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
            ah = new Ausentismo.Core.AusentismoHelper();
            eh = new Empleados.Core.EmpleadosHelper();
            ah.Command = cmd;
            eh.Command = cmd;

            Ausentismo.Core.Ausentismo ausentismo = new Ausentismo.Core.Ausentismo();
            ausentismo.idempresa = GLOBALES.IDEMPRESA;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.estatus = GLOBALES.INACTIVO;

            try
            {
                cnx.Open();
                lstAusentismo = ah.obtenerAusentimos(ausentismo);
                lstEmpleados = eh.obtenerEmpleadosBaja(empleado);
                cnx.Close();
                cnx.Dispose();

                var au = from a in lstAusentismo
                          join t in lstEmpleados on a.idtrabajador equals t.idtrabajador
                          select new
                          {
                              RegistroPatronal = a.registropatronal,
                              Nss = a.nss,
                              Nombre = t.nombrecompleto,
                              Baja = a.fecha_imss,
                              Dias = a.dias
                          };
                dgvAusentismoSua.DataSource = au.ToList();

                for (int i = 0; i < dgvAusentismoSua.Columns.Count; i++)
                {
                    dgvAusentismoSua.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void frmListaAusentimosSua_Load(object sender, EventArgs e)
        {
            dgvAusentismoSua.RowHeadersVisible = false;
            ListaEmpleados();
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Ausentismos");

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
                var au = from a in lstAusentismo
                          join t in lstEmpleados on a.idtrabajador equals t.idtrabajador
                          select new
                          {
                              RegistroPatronal = a.registropatronal,
                              Nss = a.nss,
                              Nombre = t.nombrecompleto,
                              Baja = a.fecha_imss,
                              Dias = a.dias
                          };
                dgvAusentismoSua.DataSource = au.ToList();
            }
            else
            {
                var au = from a in lstAusentismo
                          join t in lstEmpleados on a.idtrabajador equals t.idtrabajador
                          where (a.fecha_imss >= new DateTime(desde.Year, desde.Month, desde.Day) && a.fecha_imss <= new DateTime(hasta.Year, hasta.Month, hasta.Day))
                          select new
                          {
                              RegistroPatronal = a.registropatronal,
                              Nss = a.nss,
                              Nombre = t.nombrecompleto,
                              Baja = a.fecha_imss,
                              Dias = a.dias
                          };
                dgvAusentismoSua.DataSource = au.ToList();
            }

            for (int i = 0; i < dgvAusentismoSua.Columns.Count; i++)
            {
                dgvAusentismoSua.AutoResizeColumn(i);
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
                workAusentismo.RunWorkerAsync();
            }
        }

        private void workAusentismo_DoWork(object sender, DoWorkEventArgs e)
        {
            string linea1 = "";

            try
            {
                using (sw = new StreamWriter(ubicacion.SelectedPath + @"\Ausentismo_Sua.txt"))
                {
                    for (int i = 0; i < dgvAusentismoSua.Rows.Count; i++)
                    {
                        linea1 = "";
                        DateTime baja = DateTime.Parse(dgvAusentismoSua.Rows[i].Cells["Baja"].Value.ToString());
                        int dias = int.Parse(dgvAusentismoSua.Rows[i].Cells["Dias"].Value.ToString());

                        linea1 += dgvAusentismoSua.Rows[i].Cells["RegistroPatronal"].Value.ToString();
                        linea1 += dgvAusentismoSua.Rows[i].Cells["Nss"].Value.ToString();
                        linea1 += "01";
                        linea1 += baja.ToString("ddMMyyyy");
                        linea1 += (" ").ToString().PadLeft(8);
                        linea1 += dias.ToString("D2");
                        linea1 += "000000000";
                        sw.WriteLine(linea1);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            workAusentismo.ReportProgress(100);
        }

        private void workAusentismo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Archivo generado con exito", "Confirmación");
        }

    }
}
