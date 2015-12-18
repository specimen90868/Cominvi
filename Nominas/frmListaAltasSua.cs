using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Nominas
{
    public partial class frmListaAltasSua : Form
    {
        public frmListaAltasSua()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        List<Altas.Core.Altas> lstAltas;
        List<Catalogos.Core.Catalogo> lstCatalogos;
        Altas.Core.AltasHelper ah;
        Catalogos.Core.CatalogosHelper ch;
        FolderBrowserDialog ubicacion;
        StreamWriter sw, sw2;
        #endregion

        #region VARIABLES PUBLICAS

        #endregion

        private void ListaEmpleados()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            ah = new Altas.Core.AltasHelper();
            ch = new Catalogos.Core.CatalogosHelper();
            ah.Command = cmd;
            ch.Command = cmd;

            Altas.Core.Altas alta = new Altas.Core.Altas();
            alta.idempresa = GLOBALES.IDEMPRESA;

            try
            {
                cnx.Open();
                lstAltas = ah.obtenerAltas(alta);
                lstCatalogos = ch.obtenerCatalogos();
                cnx.Close();
                cnx.Dispose();

                var alt = from a in lstAltas join c in lstCatalogos on a.contrato equals c.id
                          join j in lstCatalogos on a.jornada equals j.id
                         select new
                         {
                             RegistroPatronal = a.registropatronal,
                             Nss = a.nss,
                             Rfc = a.rfc,
                             Curp = a.curp,
                             ApPaterno = a.paterno,
                             ApMaterno = a.materno,
                             Nombre = a.nombre,
                             Contrato = c.descripcion,
                             CValor = c.valor,
                             Jornada = j.descripcion,
                             JValor = j.valor,
                             Ingreso = a.fechaingreso,
                             Integrado = a.sdi,
                             CPostal = a.cp,
                             Nacimiento = a.fechanacimiento,
                             Estado = a.estado,
                             NoEstado = a.noestado,
                             Clinica = a.clinica,
                             Sexo = a.sexo
                         };
                dgvAltasSua.DataSource = alt.ToList();

                for (int i = 0; i < dgvAltasSua.Columns.Count; i++)
                {
                    dgvAltasSua.AutoResizeColumn(i);
                }

                dgvAltasSua.Columns["CValor"].Visible = false;
                dgvAltasSua.Columns["JValor"].Visible = false;
                dgvAltasSua.Columns["NoEstado"].Visible = false;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }

            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAltasSua.Columns[9].DefaultCellStyle = estilo;
        }

        private void frmListaAltasSua_Load(object sender, EventArgs e)
        {
            dgvAltasSua.RowHeadersVisible = false;
            ListaEmpleados();
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Altas");

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
                var alt = from a in lstAltas
                          join c in lstCatalogos on a.contrato equals c.id
                          join j in lstCatalogos on a.jornada equals j.id
                          select new
                          {
                              RegistroPatronal = a.registropatronal,
                              Nss = a.nss,
                              Rfc = a.rfc,
                              Curp = a.curp,
                              ApPaterno = a.paterno,
                              ApMaterno = a.materno,
                              Nombre = a.nombre,
                              Contrato = c.descripcion,
                              CValor = c.valor,
                              Jornada = j.descripcion,
                              JValor = j.valor,
                              Ingreso = a.fechaingreso,
                              Integrado = a.sdi,
                              CPostal = a.cp,
                              Nacimiento = a.fechanacimiento,
                              Estado = a.estado,
                              NoEstado = a.noestado,
                              Clinica = a.clinica,
                              Sexo = a.sexo
                          };
                dgvAltasSua.DataSource = alt.ToList();
            }
            else {
                var alt = from a in lstAltas
                          join c in lstCatalogos on a.contrato equals c.id
                          join j in lstCatalogos on a.jornada equals j.id
                          where (a.fechaingreso >= new DateTime(desde.Year, desde.Month, desde.Day) && a.fechaingreso <= new DateTime(hasta.Year, hasta.Month, hasta.Day))
                          select new
                          {
                              RegistroPatronal = a.registropatronal,
                              Nss = a.nss,
                              Rfc = a.rfc,
                              Curp = a.curp,
                              ApPaterno = a.paterno,
                              ApMaterno = a.materno,
                              Nombre = a.nombre,
                              Contrato = c.descripcion,
                              CValor = c.valor,
                              Jornada = j.descripcion,
                              JValor = j.valor,
                              Ingreso = a.fechaingreso,
                              Integrado = a.sdi,
                              CPostal = a.cp,
                              Nacimiento = a.fechanacimiento,
                              Estado = a.estado,
                              NoEstado = a.noestado,
                              Clinica = a.clinica,
                              Sexo = a.sexo
                          };
                dgvAltasSua.DataSource = alt.ToList();
            }

            for (int i = 0; i < dgvAltasSua.Columns.Count; i++)
            {
                dgvAltasSua.AutoResizeColumn(i);
            }

            dgvAltasSua.Columns["CValor"].Visible = false;
            dgvAltasSua.Columns["JValor"].Visible = false;
            dgvAltasSua.Columns["NoEstado"].Visible = false;
        }

        private void toolExportar_Click(object sender, EventArgs e)
        {
            #region VALIDACION DATOS COMPLETOS
            int registros = 0;
            for (int i = 0; i < dgvAltasSua.Rows.Count; i++)
            {
                if (dgvAltasSua.Rows[i].Cells["CPostal"].Value.ToString() == "00000" || dgvAltasSua.Rows[i].Cells["Clinica"].Value.ToString() == "000")
                {
                    registros++;
                }
            }
            if (!registros.Equals(0))
            {
                MessageBox.Show("Existen registros con datos faltantes", "Información");
                return;
            }
            else
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
            #endregion
        }

        private void workArchivo_DoWork(object sender, DoWorkEventArgs e)
        {
            string linea1 = "";
            string linea2 = "";

            try
            {
                using (sw = new StreamWriter(ubicacion.SelectedPath + @"\Alta_Sua.txt"))
                {
                    for (int i = 0; i < dgvAltasSua.Rows.Count; i++)
                    {
                        linea1 = "";
                        DateTime ingreso = DateTime.Parse(dgvAltasSua.Rows[i].Cells["Ingreso"].Value.ToString());
                        double integrado = double.Parse(dgvAltasSua.Rows[i].Cells["Integrado"].Value.ToString());
                        decimal dIntegrado = Decimal.Round((decimal)integrado, 2);
                        int iIntegrado = (int)(dIntegrado * 100);
                        linea1 += dgvAltasSua.Rows[i].Cells["RegistroPatronal"].Value.ToString();
                        linea1 += dgvAltasSua.Rows[i].Cells["Nss"].Value.ToString();
                        linea1 += dgvAltasSua.Rows[i].Cells["Rfc"].Value.ToString().PadLeft(13);
                        linea1 += dgvAltasSua.Rows[i].Cells["Curp"].Value.ToString().PadLeft(18);
                        linea1 += (dgvAltasSua.Rows[i].Cells["ApPaterno"].Value.ToString() + "$" + dgvAltasSua.Rows[i].Cells["ApMaterno"].Value.ToString() + "$" + dgvAltasSua.Rows[i].Cells["Nombre"].Value.ToString()).ToString().PadRight(50);
                        linea1 += dgvAltasSua.Rows[i].Cells["CValor"].Value.ToString();
                        linea1 += dgvAltasSua.Rows[i].Cells["JValor"].Value.ToString();
                        linea1 += ingreso.ToString("ddMMyyyy");
                        linea1 += iIntegrado.ToString("D7");
                        linea1 += (" ").ToString().PadLeft(27);
                        linea1 += ingreso.ToString("ddMMyyyy");
                        linea1 += (" ").ToString().PadLeft(1) + "00000000";
                        sw.WriteLine(linea1);
                    }
                }
            }
            catch (Exception error) {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }

            try {
                using (sw2 = new StreamWriter(ubicacion.SelectedPath + @"\Afil_Sua.txt"))
                {
                    for (int i = 0; i < dgvAltasSua.Rows.Count; i++)
                    {
                        linea2 = "";
                        DateTime nacimiento = DateTime.Parse(dgvAltasSua.Rows[i].Cells["Nacimiento"].Value.ToString());
                        int estado = int.Parse(dgvAltasSua.Rows[i].Cells["NoEstado"].Value.ToString());
                        linea2 = dgvAltasSua.Rows[i].Cells["RegistroPatronal"].Value.ToString() +
                                 dgvAltasSua.Rows[i].Cells["Nss"].Value.ToString() +
                                 dgvAltasSua.Rows[i].Cells["CPostal"].Value.ToString() +
                                 nacimiento.ToString("ddMMyyyy") +
                                 dgvAltasSua.Rows[i].Cells["Estado"].Value.ToString().PadLeft(25) +
                                 estado.ToString("D2") +
                                 dgvAltasSua.Rows[i].Cells["Clinica"].Value.ToString() +
                                 (" ").ToString().PadLeft(12) +
                                 dgvAltasSua.Rows[i].Cells["Sexo"].Value.ToString() + "0 ";
                        sw2.WriteLine(linea2);
                        sw2.Flush();
                    }
                }
            }
            catch (Exception error) {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }
            
            workArchivo.ReportProgress(100);
        }

        private void workArchivo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Archivo generado con exito", "Confirmación");
        }

        private void workArchivo_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
    }
}
