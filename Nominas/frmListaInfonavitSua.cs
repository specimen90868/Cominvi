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
    public partial class frmListaInfonavitSua : Form
    {
        public frmListaInfonavitSua()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Infonavit.Core.suaInfonavit> lstInf;
        List<Empleados.Core.Empleados> lstEmpleados;
        Infonavit.Core.InfonavitHelper ih;
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
            ih = new Infonavit.Core.InfonavitHelper();
            eh = new Empleados.Core.EmpleadosHelper();
            ih.Command = cmd;
            eh.Command = cmd;

            Infonavit.Core.suaInfonavit sua = new Infonavit.Core.suaInfonavit();
            sua.idempresa = GLOBALES.IDEMPRESA;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.estatus = GLOBALES.ACTIVO;

            try
            {
                cnx.Open();
                lstInf = ih.obtenerInfonavit(sua);
                lstEmpleados = eh.obtenerEmpleados(empleado);
                cnx.Close();
                cnx.Dispose();

                var inf = from i in lstInf
                            join t in lstEmpleados on i.idtrabajador equals t.idtrabajador
                            select new
                            {
                                RegistroPatronal = i.registropatronal,
                                Nss = i.nss,
                                Nombre = t.nombrecompleto,
                                Credito = i.credito,
                                Modificacion = i.modificacion,
                                Descuento = i.descuento,
                                Fecha = i.fecha,
                                Valor = i.valor
                            };
                dgvInfSua.DataSource = inf.ToList();

                for (int i = 0; i < dgvInfSua.Columns.Count; i++)
                {
                    dgvInfSua.AutoResizeColumn(i);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void workInf_DoWork(object sender, DoWorkEventArgs e)
        {
            string linea1 = "";

            try
            {
                using (sw = new StreamWriter(ubicacion.SelectedPath + @"\Infonavit_Sua.txt"))
                {
                    for (int i = 0; i < dgvInfSua.Rows.Count; i++)
                    {
                        linea1 = "";
                        DateTime fecha = DateTime.Parse(dgvInfSua.Rows[i].Cells["Fecha"].Value.ToString());
                        double valor = double.Parse(dgvInfSua.Rows[i].Cells["Valor"].Value.ToString());
                        decimal dValor = Decimal.Round((decimal)valor, 2);
                        int iValor = (int)(dValor * 10000);

                        linea1 += dgvInfSua.Rows[i].Cells["RegistroPatronal"].Value.ToString();
                        linea1 += dgvInfSua.Rows[i].Cells["Nss"].Value.ToString();
                        linea1 += dgvInfSua.Rows[i].Cells["Credito"].Value.ToString();
                        linea1 += dgvInfSua.Rows[i].Cells["Modificacion"].Value.ToString();
                        linea1 += fecha.ToString("ddMMyyyy");
                        linea1 += dgvInfSua.Rows[i].Cells["Descuento"].Value.ToString();
                        linea1 += iValor.ToString("D8") + "N";
                        sw.WriteLine(linea1);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }

            workInf.ReportProgress(100);
        }

        private void frmListaInfonavitSua_Load(object sender, EventArgs e)
        {
            dgvInfSua.RowHeadersVisible = false;
            ListaEmpleados();
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Infonavit SUA");

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
                var inf = from i in lstInf
                            join t in lstEmpleados on i.idtrabajador equals t.idtrabajador
                            select new
                            {
                                RegistroPatronal = i.registropatronal,
                                Nss = i.nss,
                                Nombre = t.nombrecompleto,
                                Credito = i.credito,
                                Modificacion = i.modificacion,
                                Descuento = i.descuento,
                                Fecha = i.fecha,
                                Valor = i.valor
                            };
                dgvInfSua.DataSource = inf.ToList();
            }
            else
            {
                var inf = from i in lstInf
                            join t in lstEmpleados on i.idtrabajador equals t.idtrabajador
                            where (i.fecha >= new DateTime(desde.Year, desde.Month, desde.Day) && i.fecha <= new DateTime(hasta.Year, hasta.Month, hasta.Day))
                            select new
                            {
                                RegistroPatronal = i.registropatronal,
                                Nss = i.nss,
                                Nombre = t.nombrecompleto,
                                Credito = i.credito,
                                Modificacion = i.modificacion,
                                Descuento = i.descuento,
                                Fecha = i.fecha,
                                Valor = i.valor
                            };
                dgvInfSua.DataSource = inf.ToList();
            }

            for (int i = 0; i < dgvInfSua.Columns.Count; i++)
            {
                dgvInfSua.AutoResizeColumn(i);
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
                workInf.RunWorkerAsync();
            }
        }

        private void workInf_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Archivo generado con exito", "Confirmación");
        }
    }
}
