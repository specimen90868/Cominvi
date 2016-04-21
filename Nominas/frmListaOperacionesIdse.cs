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
    public partial class frmListaOperacionesIdse : Form
    {
        public frmListaOperacionesIdse()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Altas.Core.Altas> lstAltas;
        List<Modificaciones.Core.Modificaciones> lstMod;
        List<Bajas.Core.Bajas> lstBaja;
        List<Catalogos.Core.Catalogo> lstCatalogo;
        List<Empleados.Core.Empleados> lstEmpleado;
        Altas.Core.AltasHelper ah;
        Modificaciones.Core.ModificacionesHelper mh;
        Bajas.Core.BajasHelper bh;
        Catalogos.Core.CatalogosHelper ch;
        Empleados.Core.EmpleadosHelper eh;
        FolderBrowserDialog ubicacion;
        StreamWriter sw;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoOperacion;
        #endregion

        private void ListaEmpleados(int operacion)
        {

            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;
            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.estatus = GLOBALES.ACTIVO;

            switch (operacion)
            {
                #region ALTAS
                case 0: //ALTAS
                    ah = new Altas.Core.AltasHelper();
                    ah.Command = cmd;
                    Altas.Core.Altas alta = new Altas.Core.Altas();
                    alta.idempresa = GLOBALES.IDEMPRESA;
                    try
                    {
                        cnx.Open();
                        lstAltas = ah.obtenerAltas(alta);
                        cnx.Close();
                        cnx.Dispose();

                        var alt = from a in lstAltas
                                  select new
                                  {
                                      RegistroPatronal = a.registropatronal,
                                      Nss = a.nss,
                                      Curp = a.curp,
                                      ApPaterno = a.paterno,
                                      ApMaterno = a.materno,
                                      Nombre = a.nombre,
                                      Ingreso = a.fechaingreso,
                                      Integrado = a.sdi
                                  };
                        dgvDatos.DataSource = alt.ToList();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    }
                    break;
                #endregion

                #region MODIFICACIONES
                case 1: //MODIFICACIONES
                    mh = new Modificaciones.Core.ModificacionesHelper();
                    mh.Command = cmd;
                    Modificaciones.Core.Modificaciones modificacion = new Modificaciones.Core.Modificaciones();
                    modificacion.idempresa = GLOBALES.IDEMPRESA;
                    try
                    {
                        cnx.Open();
                        lstMod = mh.obtieneModificaciones(modificacion);
                        lstEmpleado = eh.obtenerEmpleados(empleado);
                        cnx.Close();
                        cnx.Dispose();

                        var mod = from m in lstMod
                                  join e in lstEmpleado on m.idtrabajador equals e.idtrabajador
                                  select new
                                  {
                                      RegistroPatronal = m.registropatronal,
                                      Nss = m.nss,
                                      Paterno = e.paterno,
                                      Materno = e.materno,
                                      Nombre = e.nombres,
                                      Curp = e.curp,
                                      Fecha = m.fecha,
                                      Integrado = m.sdi
                                  };
                        dgvDatos.DataSource = mod.ToList();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    }
                    break;
                #endregion

                #region BAJAS
                case 2://BAJAS
                    bh = new Bajas.Core.BajasHelper();
                    ch = new Catalogos.Core.CatalogosHelper();
                    bh.Command = cmd;
                    ch.Command = cmd;
                    Bajas.Core.Bajas baja = new Bajas.Core.Bajas();
                    Catalogos.Core.Catalogo catalogo = new Catalogos.Core.Catalogo();
                    baja.idempresa = GLOBALES.IDEMPRESA;
                    try
                    {
                        cnx.Open();
                        lstBaja = bh.obtenerBajas(baja);
                        lstEmpleado = eh.obtenerEmpleados(empleado);
                        lstCatalogo = ch.obtenerCatalogos();
                        cnx.Close();
                        cnx.Dispose();

                        var baj = from b in lstBaja
                                  join e in lstEmpleado on b.idtrabajador equals e.idtrabajador
                                  join c in lstCatalogo on b.motivo equals c.id
                                  select new
                                  {
                                      RegistroPatronal = b.registropatronal,
                                      Nss = b.nss,
                                      Paterno = e.paterno,
                                      Materno = e.materno,
                                      Nombre = e.nombres,
                                      Fecha = b.fecha,
                                      Motivo = c.valor
                                  };
                        dgvDatos.DataSource = baj.ToList();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    }
                    break;
                #endregion
            }
            for (int i = 0; i < dgvDatos.Columns.Count; i++)
            {
                dgvDatos.AutoResizeColumn(i);
            }
        }

        private void frmListaOperacionesIdse_Load(object sender, EventArgs e)
        {
            dgvDatos.RowHeadersVisible = false;
            ListaEmpleados(_tipoOperacion);
            toolNombreVentana.Text += " ";
            toolNombreVentana.Text += (_tipoOperacion == 0) ? "Alta" : (_tipoOperacion == 1) ? "Modificación" : "Baja";
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
            switch (_tipoOperacion)
            {
                case 0:
                    #region ALTA
                    if (desde.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") && hasta.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                    {
                        var alt = from a in lstAltas
                                  select new
                                  {
                                      RegistroPatronal = a.registropatronal,
                                      Nss = a.nss,
                                      Curp = a.curp,
                                      ApPaterno = a.paterno,
                                      ApMaterno = a.materno,
                                      Nombre = a.nombre,
                                      Ingreso = a.fechaingreso,
                                      Integrado = a.sdi
                                  };
                        dgvDatos.DataSource = alt.ToList();
                    }
                    else
                    {
                        var alt = from a in lstAltas
                                  where (a.fechaingreso >= new DateTime(desde.Year, desde.Month, desde.Day) && a.fechaingreso <= new DateTime(hasta.Year, hasta.Month, hasta.Day))
                                  select new
                                  {
                                      RegistroPatronal = a.registropatronal,
                                      Nss = a.nss,
                                      Curp = a.curp,
                                      ApPaterno = a.paterno,
                                      ApMaterno = a.materno,
                                      Nombre = a.nombre,
                                      Ingreso = a.fechaingreso,
                                      Integrado = a.sdi
                                  };
                        dgvDatos.DataSource = alt.ToList();
                    }
                    #endregion
                    break;
                case 1:
                    #region MODIFICACIONES
                    if (desde.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") && hasta.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                    {
                        var mod = from m in lstMod
                                  join e in lstEmpleado on m.idtrabajador equals e.idtrabajador
                                  select new
                                  {
                                      RegistroPatronal = m.registropatronal,
                                      Nss = m.nss,
                                      Paterno = e.paterno,
                                      Materno = e.materno,
                                      Nombre = e.nombres,
                                      Curp = e.curp,
                                      Fecha = m.fecha,
                                      Integrado = m.sdi
                                  };
                        dgvDatos.DataSource = mod.ToList();
                    }
                    else
                    {
                        var mod = from m in lstMod
                                  join e in lstEmpleado on m.idtrabajador equals e.idtrabajador
                                  where (m.fecha >= new DateTime(desde.Year, desde.Month, desde.Day) && m.fecha <= new DateTime(hasta.Year, hasta.Month, hasta.Day))
                                  select new
                                  {
                                      RegistroPatronal = m.registropatronal,
                                      Nss = m.nss,
                                      Paterno = e.paterno,
                                      Materno = e.materno,
                                      Nombre = e.nombres,
                                      Curp = e.curp,
                                      Fecha = m.fecha,
                                      Integrado = m.sdi
                                  };
                        dgvDatos.DataSource = mod.ToList();
                    }
                    #endregion
                    break;
                case 2:
                    #region BAJAS
                    if (desde.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") && hasta.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                    {
                        var baj = from b in lstBaja
                                  join e in lstEmpleado on b.idtrabajador equals e.idtrabajador
                                  join c in lstCatalogo on b.motivo equals c.id
                                  select new
                                  {
                                      RegistroPatronal = b.registropatronal,
                                      Nss = b.nss,
                                      Paterno = e.paterno,
                                      Materno = e.materno,
                                      Nombre = e.nombres,
                                      Fecha = b.fecha,
                                      Motivo = c.valor
                                  };
                        dgvDatos.DataSource = baj.ToList();
                    }
                    else
                    {
                        var baj = from b in lstBaja
                                  join e in lstEmpleado on b.idtrabajador equals e.idtrabajador
                                  join c in lstCatalogo on b.motivo equals c.id
                                  where (b.fecha >= new DateTime(desde.Year, desde.Month, desde.Day) && b.fecha <= new DateTime(hasta.Year, hasta.Month, hasta.Day))
                                  select new
                                  {
                                      RegistroPatronal = b.registropatronal,
                                      Nss = b.nss,
                                      Paterno = e.paterno,
                                      Materno = e.materno,
                                      Nombre = e.nombres,
                                      Fecha = b.fecha,
                                      Motivo = c.valor
                                  };
                        dgvDatos.DataSource = baj.ToList();
                    }
                    #endregion
                    break;
            }
        }

        private void workArchivo_DoWork(object sender, DoWorkEventArgs e)
        {
            string linea1 = "";
            int cifraControl = 0;
            switch (_tipoOperacion)
            {
                #region ALTAS
                case 0:
                    try
                    {
                        using (sw = new StreamWriter(ubicacion.SelectedPath + @"\Altas.txt"))
                        {
                            for (int i = 0; i < dgvDatos.Rows.Count; i++)
                            {
                                linea1 = "";
                                DateTime ingreso = DateTime.Parse(dgvDatos.Rows[i].Cells["Ingreso"].Value.ToString());
                                double integrado = double.Parse(dgvDatos.Rows[i].Cells["Integrado"].Value.ToString());
                                decimal dIntegrado = Decimal.Round((decimal)integrado, 2);
                                int iIntegrado = (int)(dIntegrado * 100);
                                linea1 += dgvDatos.Rows[i].Cells["RegistroPatronal"].Value.ToString();
                                linea1 += dgvDatos.Rows[i].Cells["Nss"].Value.ToString();
                                linea1 += dgvDatos.Rows[i].Cells["ApPaterno"].Value.ToString().PadRight(27);
                                linea1 += dgvDatos.Rows[i].Cells["ApMaterno"].Value.ToString().PadRight(27);
                                linea1 += dgvDatos.Rows[i].Cells["Nombre"].Value.ToString().PadRight(27);
                                linea1 += iIntegrado.ToString("D6");
                                linea1 += "000000100";
                                linea1 += ingreso.ToString("ddMMyyyy");
                                linea1 += (" ").ToString().PadLeft(3);
                                linea1 += (" ").ToString().PadLeft(2);
                                linea1 += "08";
                                linea1 += (" ").ToString().PadLeft(5);
                                linea1 += (" ").ToString().PadLeft(10);
                                linea1 += (" ").ToString().PadLeft(1);
                                linea1 += dgvDatos.Rows[i].Cells["Curp"].Value.ToString().PadRight(18);
                                linea1 += "9";
                                cifraControl += 1;
                                sw.WriteLine(linea1);
                            }
                            linea1 = "*************" + (" ").ToString().PadRight(43) + cifraControl.ToString("D6") + (" ").ToString().PadRight(71) +
                                (" ").ToString().PadRight(5) + (" ").ToString().PadRight(29) + "9";
                            sw.WriteLine(linea1);
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    }
                    break;
                #endregion

                #region MODIFICACION
                case 1:
                    try
                    {
                        using (sw = new StreamWriter(ubicacion.SelectedPath + @"\Modificaciones.txt"))
                        {
                            for (int i = 0; i < dgvDatos.Rows.Count; i++)
                            {
                                linea1 = "";
                                DateTime fecha = DateTime.Parse(dgvDatos.Rows[i].Cells["Fecha"].Value.ToString());
                                double integrado = double.Parse(dgvDatos.Rows[i].Cells["Integrado"].Value.ToString());
                                decimal dIntegrado = Decimal.Round((decimal)integrado, 2);
                                int iIntegrado = (int)(dIntegrado * 100);
                                linea1 += dgvDatos.Rows[i].Cells["RegistroPatronal"].Value.ToString();
                                linea1 += dgvDatos.Rows[i].Cells["Nss"].Value.ToString();
                                linea1 += dgvDatos.Rows[i].Cells["Paterno"].Value.ToString().PadRight(27);
                                linea1 += dgvDatos.Rows[i].Cells["Materno"].Value.ToString().PadRight(27);
                                linea1 += dgvDatos.Rows[i].Cells["Nombre"].Value.ToString().PadRight(27);
                                linea1 += iIntegrado.ToString("D6");
                                linea1 += "000000000";
                                linea1 += fecha.ToString("ddMMyyyy");
                                linea1 += (" ").ToString().PadLeft(5);
                                linea1 += "07";
                                linea1 += (" ").ToString().PadLeft(5);
                                linea1 += ("    ").ToString().PadLeft(10);
                                linea1 += (" ").ToString().PadLeft(1);
                                linea1 += dgvDatos.Rows[i].Cells["Curp"].Value.ToString().PadRight(18);
                                linea1 += "9";
                                cifraControl += 1;
                                sw.WriteLine(linea1);
                            }
                            linea1 = "*************" + (" ").ToString().PadRight(43) + cifraControl.ToString("D6") + (" ").ToString().PadRight(71) +
                                (" ").ToString().PadRight(5) + (" ").ToString().PadRight(29) + "9";
                            sw.WriteLine(linea1);
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    }
                    break;
                #endregion

                #region BAJA
                case 2:
                    try
                    {
                        using (sw = new StreamWriter(ubicacion.SelectedPath + @"\Bajas.txt"))
                        {
                            for (int i = 0; i < dgvDatos.Rows.Count; i++)
                            {
                                linea1 = "";
                                DateTime fecha = DateTime.Parse(dgvDatos.Rows[i].Cells["Fecha"].Value.ToString());
                                int motivo = int.Parse(dgvDatos.Rows[i].Cells["Motivo"].Value.ToString());
                                linea1 += dgvDatos.Rows[i].Cells["RegistroPatronal"].Value.ToString();
                                linea1 += dgvDatos.Rows[i].Cells["Nss"].Value.ToString();
                                linea1 += dgvDatos.Rows[i].Cells["Paterno"].Value.ToString().PadRight(27);
                                linea1 += dgvDatos.Rows[i].Cells["Materno"].Value.ToString().PadRight(27);
                                linea1 += dgvDatos.Rows[i].Cells["Nombre"].Value.ToString().PadRight(27);
                                linea1 += (" ").ToString().PadLeft(15);
                                linea1 += fecha.ToString("ddMMyyyy");
                                linea1 += (" ").ToString().PadLeft(5);
                                linea1 += "02";
                                linea1 += (" ").ToString().PadLeft(5);
                                linea1 += (" ").ToString().PadLeft(10);
                                linea1 += motivo.ToString().PadRight(1);
                                linea1 += (" ").ToString().PadLeft(18);
                                linea1 += "9";
                                cifraControl += 1;
                                sw.WriteLine(linea1);
                            }
                            linea1 = "*************" + (" ").ToString().PadRight(43) + cifraControl.ToString("D6") + (" ").ToString().PadRight(71) +
                                (" ").ToString().PadRight(5) + (" ").ToString().PadRight(29) + "9";
                            sw.WriteLine(linea1);
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                    }
                    break;
                #endregion
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
                workArchivo.RunWorkerAsync();
            }
        }

        private void workArchivo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Archivo generado con exito", "Confirmación");
        }
    }
}

