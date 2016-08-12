using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using Ionic.Zip;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing.Imaging;

namespace Nominas
{
    public partial class frmEnvioRecibos : Form
    {
        public frmEnvioRecibos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        private SqlConnection cnx;
        private SqlCommand cmd;
        private string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        string idDepartamentos = "";
        string ruta = "";
        string correoEnvio, passwordEnvio, servidorEnvio;
        int puertoEnvio, tipoNomina;
        bool usaSSL;
        string fecha = "";
        string fechafin = "";
        List<Empleados.Core.Empleados> lstEmp;
        #endregion

        private void frmEnvioRecibos_Load(object sender, EventArgs e)
        {
            cmbTipoNomina.SelectedIndex = 0;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
            dh.Command = cmd;
            Configuracion.Core.ConfiguracionHelper confh = new Configuracion.Core.ConfiguracionHelper();
            confh.Command = cmd;

            try
            {
                cnx.Open();
                correoEnvio = confh.obtenerValorConfiguracion("CorreoEnvio").ToString();
                passwordEnvio = confh.obtenerValorConfiguracion("PasswordEnvio").ToString();
                puertoEnvio = int.Parse(confh.obtenerValorConfiguracion("PuertoEnvio").ToString());
                ruta = confh.obtenerValorConfiguracion("RutaRecibos").ToString();
                servidorEnvio = confh.obtenerValorConfiguracion("ServidorEnvio").ToString();
                if(int.Parse(confh.obtenerValorConfiguracion("UsaSSL").ToString()) == 1)
                    usaSSL = true;
                else
                    usaSSL = false;
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener los periodos de la empresa.", "Error");
                cnx.Dispose();
                return;
            }

            lstvPeriodos.View = View.Details;
            lstvPeriodos.CheckBoxes = false;
            lstvPeriodos.GridLines = false;
            lstvPeriodos.Columns.Add("Inicio", 100, HorizontalAlignment.Left);
            lstvPeriodos.Columns.Add("Fin", 100, HorizontalAlignment.Left);

            lstvDepartamentos.View = View.Details;
            lstvDepartamentos.CheckBoxes = true;
            lstvDepartamentos.GridLines = false;
            lstvDepartamentos.Columns.Add("Id", 60, HorizontalAlignment.Left);
            lstvDepartamentos.Columns.Add("Departamento", 150, HorizontalAlignment.Left);

            this.Visor.RefreshReport();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            List<CalculoNomina.Core.tmpPagoNomina> lstPeriodos = new List<CalculoNomina.Core.tmpPagoNomina>();
            lstvPeriodos.Items.Clear();

            switch (cmbTipoNomina.Text)
            {
                case "Normal":
                    try
                    {
                        cnx.Open();
                        lstPeriodos = nh.obtenerPeriodosNomina(GLOBALES.IDEMPRESA, GLOBALES.NORMAL);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Al obtener los periodos de la empresa.", "Error");
                        cnx.Dispose();
                        return;
                    }
                    break;

                case "Extraordinaria normal":
                    try
                    {
                        cnx.Open();
                        lstPeriodos = nh.obtenerPeriodosNomina(GLOBALES.IDEMPRESA, GLOBALES.EXTRAORDINARIO_NORMAL);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Al obtener los periodos de la empresa.", "Error");
                        cnx.Dispose();
                        return;
                    }
                    break;
            }

            for (int i = 0; i < lstPeriodos.Count; i++)
            {
                ListViewItem Lista;
                Lista = lstvPeriodos.Items.Add(lstPeriodos[i].fechainicio.ToShortDateString());
                Lista.SubItems.Add(lstPeriodos[i].fechafin.ToShortDateString());
            }
        }

        private void lstvDepartamentos_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            idDepartamentos = "";
            for (int i = 0; i < lstvDepartamentos.Items.Count; i++)
            {
                if (lstvDepartamentos.Items[i].Checked)
                {
                    idDepartamentos += lstvDepartamentos.Items[i].Text + ",";
                }
            }

            if (idDepartamentos != "")
            {
                for (int i = 0; i < lstvPeriodos.SelectedItems.Count; i++)
                    fecha = lstvPeriodos.SelectedItems[i].Text;

                if (fecha != "")
                {
                    dgvEmpleados.DataSource = null;
                    dgvEmpleados.Columns.Clear();
                    cnx = new SqlConnection(cdn);
                    cmd = new SqlCommand();
                    cmd.Connection = cnx;

                    Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
                    eh.Command = cmd;

                    lstEmp = new List<Empleados.Core.Empleados>();
                    idDepartamentos = idDepartamentos.Substring(0, idDepartamentos.Length - 1);

                    try
                    {
                        cnx.Open();
                        lstEmp = eh.obtenerEmpleadoPorDepto(GLOBALES.IDEMPRESA, idDepartamentos, DateTime.Parse(fecha).Date);
                        cnx.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Error: Al obtener el listado de los empleados.", "Error");
                        cnx.Dispose();
                        return;
                    }

                    var empleados = from emp in lstEmp select new { emp.idtrabajador, emp.noempleado, emp.nombrecompleto };
                    dgvEmpleados.DataSource = empleados.ToList();
                    dgvEmpleados.Columns.Add("generado", "Generado");

                    dgvEmpleados.Columns["idtrabajador"].Visible = false;
                    dgvEmpleados.Columns["noempleado"].Width = 60;
                    dgvEmpleados.Columns["nombrecompleto"].Width = 230;
                    dgvEmpleados.Columns["generado"].Width = 150;
                }
            }
            else
                dgvEmpleados.DataSource = null;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (txtCorreoElectronico.Text.Length == 0)
            {
                MessageBox.Show("No es posible enviarlos, falta ingresar el correo.", "Información");
                return;
            }
            fecha = "";
            fechafin = "";
            for (int i = 0; i < lstvPeriodos.SelectedItems.Count; i++)
            {
                fecha = lstvPeriodos.SelectedItems[i].Text;
                fechafin = lstvPeriodos.SelectedItems[i].SubItems[1].Text;
            }
                

            if (cmbTipoNomina.Text == "Normal")
                tipoNomina = GLOBALES.NORMAL;
            else
                tipoNomina = GLOBALES.EXTRAORDINARIO_NORMAL;

            workerEnvio.RunWorkerAsync();
        }

        private void workerEnvio_DoWork(object sender, DoWorkEventArgs e)
        {
            int existeNullCodeQR = 0;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            workerEnvio.ReportProgress(0, "Generando códigos QR.");

            try
            {
                cnx.Open();
                existeNullCodeQR = nh.existeNullQR(GLOBALES.IDEMPRESA, DateTime.Parse(fecha).Date, DateTime.Parse(fechafin).Date);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Al obtener existencia de nulos Code QR." + error.Message, "Error");
                cnx.Dispose();
                return;
            }

            if (existeNullCodeQR != 0)
            {
                List<CalculoNomina.Core.CodigoBidimensional> lstQR = new List<CalculoNomina.Core.CodigoBidimensional>();
                try
                {
                    cnx.Open();
                    lstQR = nh.obtenerListaQr(GLOBALES.IDEMPRESA, DateTime.Parse(fecha).Date, DateTime.Parse(fechafin).Date);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Al obtener el listado de los XML." + error.Message, "Error");
                    cnx.Dispose();
                    return;
                }

                string codigoQR = "";
                string[] valores = null;
                string numero = "";
                string vEntero = "";
                string vDecimal = "";

                for (int i = 0; i < lstQR.Count; i++)
                {
                    numero = lstQR[i].tt.ToString();
                    valores = numero.Split('.');
                    vEntero = valores[0];
                    vDecimal = valores[1];
                    codigoQR = string.Format("?re={0}&rr={1}&tt={2}.{3}&id={4}", lstQR[i].re, lstQR[i].rr,
                        vEntero.PadLeft(10, '0'), vDecimal.PadRight(6, '0'), lstQR[i].uuid);
                    var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
                    var qrCode = qrEncoder.Encode(codigoQR);
                    var renderer = new GraphicsRenderer(new FixedModuleSize(2, QuietZoneModules.Two), Brushes.Black, Brushes.White);

                    using (var stream = new FileStream(lstQR[i].uuid + ".png", FileMode.Create))
                        renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);

                    Bitmap bmp = new Bitmap(lstQR[i].uuid + ".png");
                    Byte[] qr = GLOBALES.IMAGEN_BYTES(bmp);
                    bmp.Dispose();
                    File.Delete(lstQR[i].uuid + ".png");
                    try
                    {
                        cnx.Open();
                        nh.actualizaXml(GLOBALES.IDEMPRESA, DateTime.Parse(fecha).Date, DateTime.Parse(fechafin).Date, lstQR[i].idtrabajador, qr);
                        cnx.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Al actualizar el código QR.", "Error");
                        cnx.Dispose();
                        return;
                    }
                }
            }
            
            int existeRecibo = 0;
            if (fecha != "")
            {
                for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
                {
                    
                    try
                    {
                        cnx.Open();
                        existeRecibo = (int)nh.existeXMLTrabajador(GLOBALES.IDEMPRESA, int.Parse(dgvEmpleados.Rows[i].Cells["idtrabajador"].Value.ToString()),
                            DateTime.Parse(fecha).Date);
                        cnx.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Al obtener existencia del XML", "Error");
                        cnx.Dispose();
                        return;
                    }

                    if (existeRecibo != 0)
                    {
                        dsReportes.NominaRecibosDataTable dtImpresionNomina = new dsReportes.NominaRecibosDataTable();
                        SqlDataAdapter daImpresionNomina = new SqlDataAdapter();
                        cmd.CommandText = "exec stp_rptNominaImpresionTrabajador @idempresa, @fechainicio, @tiponomina, @idtrabajador";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("fechainicio", DateTime.Parse(fecha).Date);
                        cmd.Parameters.AddWithValue("tiponomina", tipoNomina);
                        cmd.Parameters.AddWithValue("idtrabajador", int.Parse(dgvEmpleados.Rows[i].Cells["idtrabajador"].Value.ToString()));
                        cmd.CommandTimeout = 300;
                        daImpresionNomina.SelectCommand = cmd;
                        daImpresionNomina.Fill(dtImpresionNomina);

                        ReportDataSource rd = new ReportDataSource();
                        rd.Value = dtImpresionNomina;
                        rd.Name = "dsNominaRecibo";

                        Visor.LocalReport.DataSources.Clear();
                        Visor.LocalReport.DataSources.Add(rd);

                        Visor.LocalReport.ReportEmbeddedResource = "rptNominaRecibos.rdlc";
                        Visor.LocalReport.ReportPath = @"rptNominaRecibos.rdlc";

                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string extension;

                        byte[] bytes = Visor.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                        if (!Directory.Exists(ruta + DateTime.Parse(fecha).ToString("yyyyMMdd")))
                            Directory.CreateDirectory(ruta + DateTime.Parse(fecha).ToString("yyyyMMdd"));

                        using (FileStream fs = new FileStream(string.Format(@"{0}\{1}.{2}",
                            ruta + DateTime.Parse(fecha).ToString("yyyyMMdd"),
                            dgvEmpleados.Rows[i].Cells["nombrecompleto"].Value.ToString() + "_" + DateTime.Parse(fecha).ToString("yyyyMMdd"),
                            "pdf"), FileMode.Create))
                        {
                            fs.Write(bytes, 0, bytes.Length);
                            fs.Flush();
                            fs.Close();
                            fs.Dispose();
                        }

                        List<CalculoNomina.Core.XmlCabecera> lstXml = new List<CalculoNomina.Core.XmlCabecera>();
                        try
                        {
                            cnx.Open();
                            lstXml = nh.obtenerXmlTrabajador(GLOBALES.IDEMPRESA, int.Parse(dgvEmpleados.Rows[i].Cells["idtrabajador"].Value.ToString()),
                                DateTime.Parse(fecha).Date);
                            cnx.Close();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Error: Al obtener el XML del Trabajador", "Error");
                            cnx.Dispose();
                            return;
                        }

                        using (StreamWriter sw = new StreamWriter(ruta + DateTime.Parse(fecha).ToString("yyyyMMdd") + "\\" + dgvEmpleados.Rows[i].Cells["nombrecompleto"].Value.ToString() + "_" + DateTime.Parse(fecha).ToString("yyyyMMdd") + ".xml"))
                        {
                            sw.WriteLine(lstXml[0].xml);
                        }
                        workerEnvio.ReportProgress(i, "Recibo generado.");
                    }
                    else
                    {
                        workerEnvio.ReportProgress(i, "Recibo no existe.");
                    }
                }

                try
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        if (File.Exists(ruta + "RecibosNomina_" + DateTime.Parse(fecha).ToString("yyyyMMdd") + ".zip"))
                            File.Delete(ruta + "RecibosNomina_" + DateTime.Parse(fecha).ToString("yyyyMMdd") + ".zip");
                        zip.AddDirectory(ruta + DateTime.Parse(fecha).ToString("yyyyMMdd") + "\\");
                        zip.Save(ruta + "RecibosNomina_" + DateTime.Parse(fecha).ToString("yyyyMMdd") + ".zip");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al crear el archivo comprimido.", "Error");
                }

                MailMessage email = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                Attachment comprimido = new Attachment(ruta + "RecibosNomina_" + DateTime.Parse(fecha).ToString("yyyyMMdd") + ".zip");
                email.IsBodyHtml = true;
                email.From = new MailAddress(correoEnvio, "Recibos electrónicos de nómina");
                email.To.Add(txtCorreoElectronico.Text);
                email.Subject = "RecibosNomina_" + DateTime.Parse(fecha).ToString("yyyyMMdd");
                email.Body = "Correo automatico enviado por el sistema de administración de nómina. \r\n \r\n No responder.";
                email.Priority = MailPriority.Normal;
                email.Attachments.Add(comprimido);
                smtp.Host = servidorEnvio;
                smtp.Port = puertoEnvio;
                smtp.EnableSsl = usaSSL;

                smtp.Credentials = new NetworkCredential(correoEnvio, passwordEnvio);
                try
                {
                    workerEnvio.ReportProgress(0, "Enviando recibos de nómina");
                    smtp.Send(email);
                    smtp.Dispose();
                    comprimido.Dispose();
                }
                catch (Exception msg)
                {
                    MessageBox.Show("Error al enviar el correo: " + msg.Message, "Error");
                }
            }
        }

        private void workerEnvio_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
                lblEtapa.Text = e.UserState.ToString();
            else
                lblEtapa.Text = "Generando los recibos para enviar";
            dgvEmpleados.Rows[e.ProgressPercentage].Cells["generado"].Value = e.UserState.ToString();
        }

        private void workerEnvio_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblEtapa.Text = "Terminado.";
            Directory.Delete(ruta + DateTime.Parse(fecha).ToString("yyyyMMdd") + "\\", true);
            MessageBox.Show("Mensaje enviado.", "Confirmación");
            //File.Delete(ruta + "RecibosNomina_" + DateTime.Parse(fecha).ToString("yyyyMMdd") + ".zip");
        }

        private void lstvPeriodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstvPeriodos.SelectedItems.Count; i++)
            {
                fecha = lstvPeriodos.SelectedItems[i].Text;
                fechafin = lstvPeriodos.SelectedItems[i].SubItems[1].Text;
            }

            lstvDepartamentos.Items.Clear();

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
            dh.Command = cmd;

            Departamento.Core.Depto depto = new Departamento.Core.Depto();
            depto.idempresa = GLOBALES.IDEMPRESA;

            List<Departamento.Core.Depto> lstDeptos = new List<Departamento.Core.Depto>();

            try
            {
                cnx.Open();
                lstDeptos = dh.obtenerDepartamentos(GLOBALES.IDEMPRESA, DateTime.Parse(fecha), (cmbTipoNomina.Text == "Normal" ? 0 : 2));
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener los periodos de la empresa.", "Error");
                cnx.Dispose();
                return;
            }

            for (int i = 0; i < lstDeptos.Count; i++)
            {
                ListViewItem Lista;
                Lista = lstvDepartamentos.Items.Add(lstDeptos[i].id.ToString());
                Lista.SubItems.Add(lstDeptos[i].descripcion.ToString());
            }
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodos.Checked)
            {
                for (int i = 0; i < lstvDepartamentos.Items.Count; i++)
                    lstvDepartamentos.Items[i].Checked = true;
            }
            else
            {
                for (int i = 0; i < lstvDepartamentos.Items.Count; i++)
                    lstvDepartamentos.Items[i].Checked = false;
            }
        }
    }
}
