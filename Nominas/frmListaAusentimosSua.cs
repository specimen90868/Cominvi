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
        List<Faltas.Core.Faltas> lstAusentismo;
        List<Empleados.Core.Empleados> lstEmpleados;
        Faltas.Core.FaltasHelper fh;
        Empleados.Core.EmpleadosHelper eh;
        Empresas.Core.EmpresasHelper empresah;
        string registroPatronal = "";
        FolderBrowserDialog ubicacion;
        StreamWriter sw;
        #endregion

        private void ListaEmpleados()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            fh = new Faltas.Core.FaltasHelper();
            eh = new Empleados.Core.EmpleadosHelper();
            empresah = new Empresas.Core.EmpresasHelper();
            fh.Command = cmd;
            eh.Command = cmd;
            empresah.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;

            Empresas.Core.Empresas empresa = new Empresas.Core.Empresas();
            empresa.idempresa = GLOBALES.IDEMPRESA;

            try
            {
                cnx.Open();
                lstAusentismo = fh.obtenerFaltas(GLOBALES.IDEMPRESA);
                lstEmpleados = eh.obtenerEmpleadosAusentismo(empleado);
                registroPatronal = empresah.obtenerRegistroPatronal(empresa).ToString();
                cnx.Close();
                cnx.Dispose();

                var au = from a in lstAusentismo
                          join t in lstEmpleados on a.idtrabajador equals t.idtrabajador
                          select new
                          {
                              NoEmpleado = t.noempleado,
                              Nss = t.nss + t.digitoverificador.ToString(),
                              Nombre = t.nombrecompleto,
                              Fecha = a.fecha,
                              Dias = a.faltas,
                              Sbc = t.sdi
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
                              NoEmpleado = t.noempleado,
                              Nss = t.nss + t.digitoverificador.ToString(),
                              Nombre = t.nombrecompleto,
                              Fecha = a.fecha,
                              Dias = a.faltas,
                              Sbc = t.sdi
                          };
                dgvAusentismoSua.DataSource = au.ToList();
            }
            else
            {
                var au = from a in lstAusentismo
                          join t in lstEmpleados on a.idtrabajador equals t.idtrabajador
                          where (a.fecha >= new DateTime(desde.Year, desde.Month, desde.Day) && a.fecha <= new DateTime(hasta.Year, hasta.Month, hasta.Day))
                          select new
                          {
                              NoEmpleado = t.noempleado,
                              Nss = t.nss + t.digitoverificador.ToString(),
                              Nombre = t.nombrecompleto,
                              Fecha = a.fecha,
                              Dias = a.faltas,
                              Sbc = t.sdi
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
            int sbc = 0;
            string _sbc = "";
            try
            {
                using (sw = new StreamWriter(ubicacion.SelectedPath + @"\Ausentismo_Sua.txt"))
                {
                    for (int i = 0; i < dgvAusentismoSua.Rows.Count; i++)
                    {
                        _sbc = dgvAusentismoSua.Rows[i].Cells["Sbc"].Value.ToString().Replace(".", "");
                        sbc = int.Parse(_sbc);
                        linea1 = "";                        
                        linea1 += registroPatronal;
                        linea1 += dgvAusentismoSua.Rows[i].Cells["Nss"].Value.ToString();
                        linea1 += "11";
                        linea1 += DateTime.Parse(dgvAusentismoSua.Rows[i].Cells["Fecha"].Value.ToString()).ToString("ddMMyyyy");
                        linea1 += (" ").ToString().PadLeft(8);
                        linea1 += int.Parse(dgvAusentismoSua.Rows[i].Cells["Dias"].Value.ToString()).ToString("D2");
                        linea1 += sbc.ToString("D7");
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
