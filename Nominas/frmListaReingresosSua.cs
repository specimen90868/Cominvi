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
    public partial class frmListaReingresosSua : Form
    {
        public frmListaReingresosSua()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Reingreso.Core.Reingresos> lstReingreso;
        List<Empleados.Core.Empleados> lstEmpleados;
        Reingreso.Core.ReingresoHelper rh;
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
            rh = new Reingreso.Core.ReingresoHelper();
            eh = new Empleados.Core.EmpleadosHelper();
            rh.Command = cmd;
            eh.Command = cmd;

            Reingreso.Core.Reingresos reingreso = new Reingreso.Core.Reingresos();
            reingreso.idempresa = GLOBALES.IDEMPRESA;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.estatus = GLOBALES.ACTIVO;

            try
            {
                cnx.Open();
                lstReingreso = rh.obtenerReingresos(reingreso);
                lstEmpleados = eh.obtenerEmpleados(empleado);
                cnx.Close();
                cnx.Dispose();

                var rein = from r in lstReingreso
                            join t in lstEmpleados on r.idtrabajador equals t.idtrabajador
                            select new
                            {
                                RegistroPatronal = r.registropatronal,
                                Nss = r.nss,
                                Nombre = t.nombrecompleto,
                                Reingreso = r.fechaingreso,
                                Integrado = r.sdi
                            };
                dgvReingresoSua.DataSource = rein.ToList();

                for (int i = 0; i < dgvReingresoSua.Columns.Count; i++)
                {
                    dgvReingresoSua.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void frmListaReingresosSua_Load(object sender, EventArgs e)
        {
            dgvReingresoSua.RowHeadersVisible = false;
            ListaEmpleados();
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Reingresos");

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
                var rein = from r in lstReingreso
                            join t in lstEmpleados on r.idtrabajador equals t.idtrabajador
                            select new
                            {
                                RegistroPatronal = r.registropatronal,
                                Nss = r.nss,
                                Nombre = t.nombrecompleto,
                                Reingreso = r.fechaingreso,
                                Integrado = r.sdi
                            };
                dgvReingresoSua.DataSource = rein.ToList();
            }
            else
            {
                var rein = from r in lstReingreso
                            join t in lstEmpleados on r.idtrabajador equals t.idtrabajador
                           where (r.fechaingreso >= new DateTime(desde.Year, desde.Month, desde.Day) && r.fechaingreso <= new DateTime(hasta.Year, hasta.Month, hasta.Day))
                            select new
                            {
                                RegistroPatronal = r.registropatronal,
                                Nss = r.nss,
                                Nombre = t.nombrecompleto,
                                Reingreso = r.fechaingreso,
                                Integrado = r.sdi
                            };
                dgvReingresoSua.DataSource = rein.ToList();
            }

            for (int i = 0; i < dgvReingresoSua.Columns.Count; i++)
            {
                dgvReingresoSua.AutoResizeColumn(i);
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
                workReingreso.RunWorkerAsync();
            }
        }

        private void workReingreso_DoWork(object sender, DoWorkEventArgs e)
        {
            string linea1 = "";

            try
            {
                using (sw = new StreamWriter(ubicacion.SelectedPath + @"\Reing_Sua.txt"))
                {
                    for (int i = 0; i < dgvReingresoSua.Rows.Count; i++)
                    {
                        linea1 = "";
                        DateTime reingreso = DateTime.Parse(dgvReingresoSua.Rows[i].Cells["Reingreso"].Value.ToString());
                        double integrado = double.Parse(dgvReingresoSua.Rows[i].Cells["Integrado"].Value.ToString());
                        decimal dIntegrado = Decimal.Round((decimal)integrado, 2);
                        int iIntegrado = (int)(dIntegrado * 100);
                        linea1 += dgvReingresoSua.Rows[i].Cells["RegistroPatronal"].Value.ToString();
                        linea1 += dgvReingresoSua.Rows[i].Cells["Nss"].Value.ToString();
                        linea1 += "08";
                        linea1 += reingreso.ToString("ddMMyyyy");
                        linea1 += (" ").ToString().PadLeft(8);
                        linea1 += "00";
                        linea1 += iIntegrado.ToString("D7");
                        sw.WriteLine(linea1);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            workReingreso.ReportProgress(100);
        }

        private void workReingreso_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Archivo generado con exito", "Confirmación");
        }
    }
}
