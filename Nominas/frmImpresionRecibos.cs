using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nominas
{
    public partial class frmImpresionRecibos : Form
    {
        public frmImpresionRecibos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        private SqlConnection cnx;
        private SqlCommand cmd;
        private string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        string idDepartamentos = "";
        string idEmpleados = "";
        string fecha = "";
        string fechafin = "";
        List<Empleados.Core.Empleados> lstEmp;
        #endregion

        private void frmImpresionRecibos_Load(object sender, EventArgs e)
        {
            cmbTipoNomina.SelectedIndex = 0;

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
                lstDeptos = dh.obtenerDepartamentos(depto);
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

            lstvEmpleados.View = View.Details;
            lstvEmpleados.CheckBoxes = true;
            lstvEmpleados.GridLines = false;
            lstvEmpleados.Columns.Add("Id", 60, HorizontalAlignment.Left);
            lstvEmpleados.Columns.Add("No. Empleado", 70, HorizontalAlignment.Left);
            lstvEmpleados.Columns.Add("Nombre", 250, HorizontalAlignment.Left);

            
            for (int i = 0; i < lstDeptos.Count; i++)
            {
                ListViewItem Lista;
                Lista = lstvDepartamentos.Items.Add(lstDeptos[i].id.ToString());
                Lista.SubItems.Add(lstDeptos[i].descripcion.ToString());
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
                    lstvEmpleados.Items.Clear();
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

                    for (int i = 0; i < lstEmp.Count; i++)
                    {
                        ListViewItem Lista;
                        Lista = lstvEmpleados.Items.Add(lstEmp[i].idtrabajador.ToString());
                        Lista.SubItems.Add(lstEmp[i].noempleado);
                        Lista.SubItems.Add(lstEmp[i].nombrecompleto);
                    }
                }
                else
                    lstvEmpleados.Items.Clear();
            }
        }

        private void btnSeleccionarTodos_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstvEmpleados.Items.Count; i++)
                lstvEmpleados.Items[i].Checked = true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstvEmpleados.Items.Count; i++)
                lstvEmpleados.Items[i].Checked = false;
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            txtBuscar.Font = new Font("Arial", 9);
            txtBuscar.ForeColor = System.Drawing.Color.Black;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                lstvEmpleados.Items.Clear();
                if (string.IsNullOrEmpty(txtBuscar.Text) || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    for (int i = 0; i < lstEmp.Count; i++)
                    {
                        ListViewItem Lista;
                        Lista = lstvEmpleados.Items.Add(lstEmp[i].idtrabajador.ToString());
                        Lista.SubItems.Add(lstEmp[i].noempleado);
                        Lista.SubItems.Add(lstEmp[i].nombrecompleto);
                    }
                }
                else
                {
                    var busqueda = from b in lstEmp
                                   where b.noempleado.Contains(txtBuscar.Text.ToUpper())
                                   select new
                                   {
                                       b.idtrabajador,
                                       b.noempleado,
                                       b.nombrecompleto
                                   };
                    foreach (var emp in busqueda)
                    {
                        ListViewItem Lista;
                        Lista = lstvEmpleados.Items.Add(emp.idtrabajador.ToString());
                        Lista.SubItems.Add(emp.noempleado);
                        Lista.SubItems.Add(emp.nombrecompleto);
                    }
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "No. de empleado";
            txtBuscar.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void btnTodosDeptos_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstvDepartamentos.Items.Count; i++)
                lstvDepartamentos.Items[i].Checked = true;
        }

        private void btnLimpiarDeptos_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstvDepartamentos.Items.Count; i++)
                lstvDepartamentos.Items[i].Checked = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            fecha = "";
            fechafin = "";

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            for (int i = 0; i < lstvPeriodos.SelectedItems.Count; i++)
            {
                fecha = lstvPeriodos.SelectedItems[i].Text;
                fechafin = lstvPeriodos.SelectedItems[i].SubItems[1].Text;
            }
            

            List<CalculoNomina.Core.CodigoBidimensional> lstXml = new List<CalculoNomina.Core.CodigoBidimensional>();
            try
            {
                cnx.Open();
                lstXml = nh.obtenerListaQr(GLOBALES.IDEMPRESA, DateTime.Parse(fecha).Date, DateTime.Parse(fechafin).Date);
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

            for (int i = 0; i < lstXml.Count; i++)
            {
                numero = lstXml[i].tt.ToString();
                valores = numero.Split('.');
                vEntero = valores[0];
                vDecimal = valores[1];
                codigoQR = string.Format("?re={0}&rr={1}&tt={2}.{3}&id={4}", lstXml[i].re, lstXml[i].rr,
                    vEntero.PadLeft(10, '0'), vDecimal.PadRight(6, '0'), lstXml[i].uuid);
                var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
                var qrCode = qrEncoder.Encode(codigoQR);
                var renderer = new GraphicsRenderer(new FixedModuleSize(2, QuietZoneModules.Two), Brushes.Black, Brushes.White);

                using (var stream = new FileStream(lstXml[i].uuid + ".png", FileMode.Create))
                    renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);

                Bitmap bmp = new Bitmap(lstXml[i].uuid + ".png");
                Byte[] qr = GLOBALES.IMAGEN_BYTES(bmp);
                bmp.Dispose();
                File.Delete(lstXml[i].uuid + ".png");
                try
                {
                    cnx.Open();
                    nh.actualizaXml(GLOBALES.IDEMPRESA, DateTime.Parse(fecha).Date, DateTime.Parse(fechafin).Date, lstXml[i].idtrabajador, qr);
                    cnx.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al actualizar el código QR.", "Error");
                    cnx.Dispose();
                    return;
                }
            }

            idEmpleados = "";
            for (int i = 0; i < lstvEmpleados.Items.Count; i++)
            {
                if (lstvEmpleados.Items[i].Checked)
                    idEmpleados += lstvEmpleados.Items[i].Text + ",";
            }

            fecha = "";
            if (idEmpleados != "")
            {
                for (int i = 0; i < lstvPeriodos.SelectedItems.Count; i++)
                    fecha = lstvPeriodos.SelectedItems[i].Text;
                idEmpleados = idEmpleados.Substring(0, idEmpleados.Length - 1);
                frmVisorReportes vr = new frmVisorReportes();
                vr._tipoNomina = (cmbTipoNomina.SelectedIndex == 0 ? GLOBALES.NORMAL : GLOBALES.EXTRAORDINARIO_NORMAL);
                vr._departamentos = idDepartamentos;
                vr._empleados = idEmpleados;
                vr._noReporte = 10;
                vr._inicioPeriodo = DateTime.Parse(fecha).Date;
                vr.Show();
            }
        }

        private void cmbTipoNomina_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}
