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
    public partial class frmListaBajasSua : Form
    {
        public frmListaBajasSua()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Bajas.Core.Bajas> lstBajas;
        List<Catalogos.Core.Catalogo> lstCatalogos;
        List<Empleados.Core.Empleados> lstEmpleados;
        Bajas.Core.BajasHelper bh;
        Catalogos.Core.CatalogosHelper ch;
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
            bh = new Bajas.Core.BajasHelper();
            ch = new Catalogos.Core.CatalogosHelper();
            eh = new Empleados.Core.EmpleadosHelper();
            bh.Command = cmd;
            ch.Command = cmd;
            eh.Command = cmd;

            Bajas.Core.Bajas baja = new Bajas.Core.Bajas();
            baja.idempresa = GLOBALES.IDEMPRESA;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;

            try
            {
                cnx.Open();
                lstBajas = bh.obtenerBajas(baja);
                lstCatalogos = ch.obtenerCatalogos();
                lstEmpleados = eh.obtenerEmpleadosBaja(empleado);
                cnx.Close();
                cnx.Dispose();

                var baj = from b in lstBajas
                          join c in lstCatalogos on b.motivo equals c.id
                          join t in lstEmpleados on b.idtrabajador equals t.idtrabajador
                          select new
                          {
                              Id = b.idtrabajador,
                              NoEmpleado = t.noempleado,
                              RegistroPatronal = b.registropatronal,
                              Nss = b.nss,
                              Nombre = t.nombrecompleto,
                              Motivo = c.descripcion,
                              MValor = c.valor,
                              Baja = b.fecha
                          };

                dgvBajasSua.DataSource = baj.ToList();

                for (int i = 0; i < dgvBajasSua.Columns.Count; i++)
                {
                    dgvBajasSua.AutoResizeColumn(i);
                }

                dgvBajasSua.Columns["Id"].Visible = false;
                dgvBajasSua.Columns["MValor"].Visible = false;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
        }

        private void frmListaBajasSua_Load(object sender, EventArgs e)
        {
            dgvBajasSua.RowHeadersVisible = false;
            ListaEmpleados();
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Bajas");

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
                var baj = from b in lstBajas
                          join c in lstCatalogos on b.motivo equals c.id
                          join t in lstEmpleados on b.idtrabajador equals t.idtrabajador
                          select new
                          {
                              RegistroPatronal = b.registropatronal,
                              Nss = b.nss,
                              Nombre = t.nombrecompleto,
                              Motivo = c.descripcion,
                              MValor = c.valor,
                              Baja = b.fecha
                          };
                dgvBajasSua.DataSource = baj.ToList();
            }
            else
            {
                var baj = from b in lstBajas
                          join c in lstCatalogos on b.motivo equals c.id
                          join t in lstEmpleados on b.idtrabajador equals t.idtrabajador
                          where (b.fecha >= new DateTime(desde.Year, desde.Month, desde.Day) && b.fecha <= new DateTime(hasta.Year, hasta.Month, hasta.Day))
                          select new
                          {
                              RegistroPatronal = b.registropatronal,
                              Nss = b.nss,
                              Nombre = t.nombrecompleto,
                              Motivo = c.descripcion,
                              MValor = c.valor,
                              Baja = b.fecha
                          };
                dgvBajasSua.DataSource = baj.ToList();
            }

            for (int i = 0; i < dgvBajasSua.Columns.Count; i++)
            {
                dgvBajasSua.AutoResizeColumn(i);
            }
            dgvBajasSua.Columns["MValor"].Visible = false;
        }

        private void toolExportar_Click(object sender, EventArgs e)
        {
            ubicacion = new FolderBrowserDialog();
            ubicacion.Description = "Seleccion la carpeta";
            ubicacion.RootFolder = Environment.SpecialFolder.Desktop;
            ubicacion.ShowNewFolderButton = true;
            if (DialogResult.OK == ubicacion.ShowDialog())
            {
                workBajas.RunWorkerAsync();
            }
        }

        private void workBajas_DoWork(object sender, DoWorkEventArgs e)
        {
            string linea1 = "";

            try
            {
                using (sw = new StreamWriter(ubicacion.SelectedPath + @"\Baja_Sua.txt"))
                {
                    for (int i = 0; i < dgvBajasSua.Rows.Count; i++)
                    {
                        linea1 = "";
                        DateTime baja = DateTime.Parse(dgvBajasSua.Rows[i].Cells["Baja"].Value.ToString());
                        int motivo = int.Parse(dgvBajasSua.Rows[i].Cells["MValor"].Value.ToString());
                        linea1 += dgvBajasSua.Rows[i].Cells["RegistroPatronal"].Value.ToString();
                        linea1 += dgvBajasSua.Rows[i].Cells["Nss"].Value.ToString();
                        linea1 += motivo.ToString("D2");
                        linea1 += baja.ToString("ddMMyyyy");
                        linea1 += (" ").ToString().PadLeft(8);
                        linea1 += "000000000";
                        sw.WriteLine(linea1);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            workBajas.ReportProgress(100);
        }

        private void workBajas_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Archivo generado con exito", "Confirmación");
        }

        private void toolEliminar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Quiere eliminar la baja?. \r\n \r\n CUIDADO. Esta acción eliminará permanentemente el registro.", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                int fila = dgvBajasSua.CurrentCell.RowIndex;
                string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
                eh.Command = cmd;

                Bajas.Core.BajasHelper bh = new Bajas.Core.BajasHelper();
                bh.Command = cmd;

                Historial.Core.HistorialHelper hh = new Historial.Core.HistorialHelper();
                hh.Command = cmd;

                Empleados.Core.EmpleadosEstatus ee = new Empleados.Core.EmpleadosEstatus();
                ee.idtrabajador = int.Parse(dgvBajasSua.Rows[fila].Cells[0].Value.ToString());
                ee.idempresa = GLOBALES.IDEMPRESA;
                ee.estatus = GLOBALES.ACTIVO;

                Bajas.Core.Bajas baja = new Bajas.Core.Bajas();
                baja.idtrabajador = int.Parse(dgvBajasSua.Rows[fila].Cells[0].Value.ToString());
                baja.idempresa = GLOBALES.IDEMPRESA;
                baja.fecha = DateTime.Parse(dgvBajasSua.Rows[fila].Cells[7].Value.ToString()).Date;

                Historial.Core.Historial historial = new Historial.Core.Historial();
                historial.idtrabajador = int.Parse(dgvBajasSua.Rows[fila].Cells[0].Value.ToString());
                historial.idempresa = GLOBALES.IDEMPRESA;
                historial.fecha_imss = DateTime.Parse(dgvBajasSua.Rows[fila].Cells[7].Value.ToString()).Date;

                try
                {
                    cnx.Open();
                    bh.eliminaBaja(baja);
                    eh.bajaEmpleado(ee);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Al eliminar la baja. \r\n" + error.Message, "Error");
                    cnx.Dispose();
                    return;
                }

                try
                {
                    cnx.Open();
                    hh.eliminaHistorial(historial);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Al eliminar el movimiento del historial.\r\n" + error.Message, "Error");
                    cnx.Dispose();
                    return;
                }

                MessageBox.Show("Registro eliminado.", "Confirmación");
                ListaEmpleados();
            }

        }
    }
}
