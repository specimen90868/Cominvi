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
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Departamento.Core.DeptoHelper dh;
        Empleados.Core.EmpleadosHelper eh;
        Periodos.Core.PeriodosHelper ph;
        CalculoNomina.Core.NominaHelper nh;
        int noReporte;
        string netocero, orden;
        int tipoNomina, periodo;
        Boolean FLAG_COMBOBOX = false;
        #endregion

        #region DELEGADOS
        public delegate void delOnReporte(string netocero, string orden, int noreporte, int empleadoinicial, int empleadofinal);
        public event delOnReporte OnReporte;
        #endregion

        #region VARIABLES PUBLICA
        public DateTime _inicio;
        public DateTime _fin;
        public bool _ReportePreNomina;
        public int _noReporte;
        public int _tipoNomina;
        #endregion

        private void frmReportes_Load(object sender, EventArgs e)
        {
            dtpFinPeriodo.Enabled = false;

            cmbDeptoInicial.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbDeptoInicial.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbDeptoFinal.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbDeptoFinal.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbEmpleadoInicial.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbEmpleadoInicial.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbEmpleadoFinal.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbEmpleadoFinal.AutoCompleteSource = AutoCompleteSource.ListItems;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            dh = new Departamento.Core.DeptoHelper();
            dh.Command = cmd;

            Departamento.Core.Depto depto = new Departamento.Core.Depto();
            depto.idempresa = GLOBALES.IDEMPRESA;
            depto.estatus = GLOBALES.ACTIVO;

            ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
            periodo.idempresa = GLOBALES.IDEMPRESA;

            List<Departamento.Core.Depto> lstDeptosDe = new List<Departamento.Core.Depto>();
            List<Departamento.Core.Depto> lstDeptosHasta = new List<Departamento.Core.Depto>();
            List<Periodos.Core.Periodos> lstPeriodos = new List<Periodos.Core.Periodos>();
            
            try
            {
                cnx.Open();
                lstDeptosDe = dh.obtenerDepartamentos(depto);
                lstDeptosHasta = dh.obtenerDepartamentos(depto);
                lstPeriodos = ph.obtenerPeriodos(periodo);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error) { MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error"); }

            cmbDeptoInicial.DataSource = lstDeptosDe;
            cmbDeptoInicial.DisplayMember = "descripcion";
            cmbDeptoInicial.ValueMember = "id";

            cmbDeptoFinal.DataSource = lstDeptosHasta;
            cmbDeptoFinal.DisplayMember = "descripcion";
            cmbDeptoFinal.ValueMember = "id";

            cmbPeriodo.DataSource = lstPeriodos.ToList();
            cmbPeriodo.DisplayMember = "pago";
            cmbPeriodo.ValueMember = "idperiodo";

            cmbTipoReporte.SelectedIndex = 0;
            cmbTipoNomina.SelectedIndex = 0;
            cmbNetoCero.SelectedIndex = 0;
            cmbOrden.SelectedIndex = 3;

            if (_ReportePreNomina)
            {
                dtpInicioPeriodo.Value = _inicio;
                dtpFinPeriodo.Value = _fin;
                cmbPeriodo.Enabled = true;
                dtpInicioPeriodo.Enabled = false;
                dtpFinPeriodo.Enabled = false;
                cmbTipoReporte.Enabled = false;
                cmbDeptoInicial.Enabled = false;
                cmbDeptoFinal.Enabled = false;
                cmbEmpleadoInicial.Enabled = false;
                cmbEmpleadoFinal.Enabled = false;

                if (_noReporte == 2)
                {
                    cmbOrden.Items.Clear();
                    cmbOrden.Items.Add("Departamento");
                    cmbOrden.SelectedIndex = 0;
                }
                else
                {
                    cmbOrden.Items.Clear();
                    cmbOrden.Items.Add("No. de Empleado");
                    cmbOrden.Items.Add("Departamento");
                    cmbOrden.Items.Add("No. de Empleado, Departamento");
                    cmbOrden.Items.Add("Departamento, No. de Empleado");
                    cmbOrden.SelectedIndex = 0;
                }

                if (_noReporte == 9 || _noReporte == 1)
                {
                    cmbEmpleadoInicial.Enabled = true;
                    cmbEmpleadoFinal.Enabled = true;
                }
            }
            FLAG_COMBOBOX = true;
        } 

        private void btnAceptar_Click(object sender, EventArgs e)

        {
            if (!_ReportePreNomina)
            {
                if (noReporte != 6 && noReporte != 9)
                {
                    if (noReporte == 8)
                    {
                        int existeNullCodeQR = 0;
                        cnx = new SqlConnection(cdn);
                        cmd = new SqlCommand();
                        cmd.Connection = cnx;
                        nh = new CalculoNomina.Core.NominaHelper();
                        nh.Command = cmd;

                        #region EXISTENCIA DE NULOS EN TABLA xmlCabecera
                        try
                        {
                            cnx.Open();
                            existeNullCodeQR = nh.existeNullQR(GLOBALES.IDEMPRESA, dtpInicioPeriodo.Value.Date, dtpFinPeriodo.Value.Date);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error: Al obtener existencia de nulos Code QR." + error.Message, "Error");
                            cnx.Dispose();
                            return;
                        }
                        #endregion

                        #region GENERACION DE CODIGO QR SI EXISTEN NULOS
                        if (existeNullCodeQR != 0)
                        {
                            List<CalculoNomina.Core.CodigoBidimensional> lstXml = new List<CalculoNomina.Core.CodigoBidimensional>();
                            try
                            {
                                cnx.Open();
                                lstXml = nh.obtenerListaQr(GLOBALES.IDEMPRESA, dtpInicioPeriodo.Value.Date, dtpFinPeriodo.Value.Date, periodo);
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
                                    nh.actualizaXml(GLOBALES.IDEMPRESA, dtpInicioPeriodo.Value.Date, dtpFinPeriodo.Value.Date, lstXml[i].idtrabajador, qr);
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
                        #endregion
                    }

                    frmVisorReportes vr = new frmVisorReportes();
                    vr._inicioPeriodo = dtpInicioPeriodo.Value;
                    vr._finPeriodo = dtpFinPeriodo.Value;
                    if (_ReportePreNomina)
                        vr._tipoNomina = _tipoNomina;
                    else
                        vr._tipoNomina = tipoNomina;
                    vr._noReporte = noReporte;
                    vr._deptoInicio = int.Parse(cmbDeptoInicial.SelectedValue.ToString());
                    vr._deptoFin = int.Parse(cmbDeptoFinal.SelectedValue.ToString());
                    vr._empleadoInicio = int.Parse(cmbEmpleadoInicial.SelectedValue.ToString());
                    vr._empleadoFin = int.Parse(cmbEmpleadoFinal.SelectedValue.ToString());
                    vr._netoCero = netocero;
                    vr._orden = orden;
                    vr._periodo = periodo;
                    vr.Show();
                }
                else
                {
                    if (noReporte == 6)
                        excelTabular();
                    if (noReporte == 9)
                        excelGravadosExentos();
                }
            }
            else
            {
                int empleadoInicial = 0, empleadoFinal = 0;
                if (_noReporte == 9 || _noReporte == 1)
                {
                    empleadoInicial = int.Parse(cmbEmpleadoInicial.SelectedValue.ToString());
                    empleadoFinal = int.Parse(cmbEmpleadoFinal.SelectedValue.ToString());
                }
                if (OnReporte != null)
                    OnReporte(netocero, orden, _noReporte, empleadoInicial, empleadoFinal);
            }
            this.Dispose();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbTipoReporte.Text)
            {
                case "Empleados":
                    cmbDeptoInicial.Enabled = true;
                    cmbDeptoFinal.Enabled = true;
                    cmbEmpleadoInicial.Enabled = true;
                    cmbEmpleadoFinal.Enabled = true;
                    cmbOrden.Enabled = true;
                    cmbNetoCero.Enabled = true;
                    noReporte = 5;
                    cmbOrden.Items.Clear();
                    cmbOrden.Items.Add("No. de Empleado");
                    cmbOrden.Items.Add("Departamento");
                    cmbOrden.Items.Add("No. de Empleado, Departamento");
                    cmbOrden.Items.Add("Departamento, No. de Empleado");
                    cmbOrden.SelectedIndex = 3;
                    break;
                case "Departamentos":
                    
                    cmbDeptoInicial.Enabled = true;
                    cmbDeptoFinal.Enabled = true;
                    cmbEmpleadoInicial.Enabled = false;
                    cmbEmpleadoFinal.Enabled = false;
                    cmbOrden.Enabled = true;
                    cmbNetoCero.Enabled = true;
                    noReporte = 4;
                    cmbOrden.Items.Clear();
                    cmbOrden.Items.Add("Departamento");
                    cmbOrden.SelectedIndex = 0;
                    break;
                case "Total General":
                    
                    cmbDeptoInicial.Enabled = false;
                    cmbDeptoFinal.Enabled = false;
                    cmbEmpleadoInicial.Enabled = false;
                    cmbEmpleadoFinal.Enabled = false;
                    cmbOrden.Enabled = false;
                    cmbNetoCero.Enabled = false;
                    cmbOrden.Items.Clear();
                    cmbOrden.Items.Add("No. de Empleado");
                    cmbOrden.Items.Add("Departamento");
                    cmbOrden.Items.Add("No. de Empleado, Departamento");
                    cmbOrden.Items.Add("Departamento, No. de Empleado");
                    noReporte = 3;
                    break;
                case "Tabular":
                    
                    cmbDeptoInicial.Enabled = true;
                    cmbDeptoFinal.Enabled = true;
                    cmbEmpleadoInicial.Enabled = true;
                    cmbEmpleadoFinal.Enabled = true;
                    cmbOrden.Enabled = true;
                    cmbNetoCero.Enabled = true;
                    noReporte = 6;
                    cmbOrden.Items.Clear();
                    cmbOrden.Items.Add("No. de Empleado");
                    cmbOrden.Items.Add("Departamento");
                    cmbOrden.Items.Add("No. de Empleado, Departamento");
                    cmbOrden.Items.Add("Departamento, No. de Empleado");
                    break;
                case "Recibos de Nomina":
                   
                    cmbDeptoInicial.Enabled = true;
                    cmbDeptoFinal.Enabled = true;
                    cmbEmpleadoInicial.Enabled = true;
                    cmbEmpleadoFinal.Enabled = true;
                    cmbOrden.Enabled = true;
                    cmbNetoCero.Enabled = true;
                    noReporte = 7;
                    cmbOrden.Items.Clear();
                    cmbOrden.Items.Add("No. de Empleado");
                    cmbOrden.Items.Add("Departamento");
                    cmbOrden.Items.Add("No. de Empleado, Departamento");
                    cmbOrden.Items.Add("Departamento, No. de Empleado");
                    cmbOrden.SelectedIndex = 0;
                    break;

                case "Recibos Timbrados": 
                    
                    cmbDeptoInicial.Enabled = true;
                    cmbDeptoFinal.Enabled = true;
                    cmbEmpleadoInicial.Enabled = true;
                    cmbEmpleadoFinal.Enabled = true;
                    cmbOrden.Enabled = true;
                    cmbNetoCero.Enabled = true;
                    noReporte = 8;
                    cmbOrden.Items.Clear();
                    cmbOrden.Items.Add("No. de Empleado");
                    cmbOrden.Items.Add("Departamento");
                    cmbOrden.Items.Add("No. de Empleado, Departamento");
                    cmbOrden.Items.Add("Departamento, No. de Empleado");
                    cmbOrden.SelectedIndex = 0;
                    break;
                case "Gravados y Exentos":
                   
                    cmbDeptoInicial.Enabled = false;
                    cmbDeptoFinal.Enabled = false;
                    cmbEmpleadoInicial.Enabled = false;
                    cmbEmpleadoFinal.Enabled = false;
                    cmbOrden.Enabled = false;
                    cmbNetoCero.Enabled = false;
                    noReporte = 9;
                    break;
            }
        }

        private void dtpInicioPeriodo_ValueChanged(object sender, EventArgs e)
        {
            if (tipoNomina == GLOBALES.NORMAL)
            {
                if (cmbPeriodo.Text == "SEMANAL")
                {
                    DateTime dt = dtpInicioPeriodo.Value;
                    while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                    dtpInicioPeriodo.Value = dt;
                    dtpFinPeriodo.Value = dt.AddDays(6);
                }
                else
                {
                    if (dtpInicioPeriodo.Value.Day <= 15)
                    {
                        dtpInicioPeriodo.Value = new DateTime(dtpInicioPeriodo.Value.Year, dtpInicioPeriodo.Value.Month, 1);
                        dtpFinPeriodo.Value = new DateTime(dtpInicioPeriodo.Value.Year, dtpInicioPeriodo.Value.Month, 15);
                    }
                    else
                    {
                        dtpInicioPeriodo.Value = new DateTime(dtpInicioPeriodo.Value.Year, dtpInicioPeriodo.Value.Month, 16);
                        dtpFinPeriodo.Value = new DateTime(dtpInicioPeriodo.Value.Year, dtpInicioPeriodo.Value.Month, DateTime.DaysInMonth(dtpInicioPeriodo.Value.Year, dtpInicioPeriodo.Value.Month));
                    }
                }
            }            
        }

        private void excelTabular()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = dtpInicioPeriodo.Value;
            pn.fechafin = dtpFinPeriodo.Value;

            Empresas.Core.EmpresasHelper eh = new Empresas.Core.EmpresasHelper();
            eh.Command = cmd;

            List<Empresas.Core.Empresas> lstEmpresa = new List<Empresas.Core.Empresas>();
            DataTable dtConceptosP = new DataTable();
            DataTable dtConceptosD = new DataTable();
            DataTable dtTrabajadores = new DataTable();
            DataTable dtDatos = new DataTable();

            try
            {
                cnx.Open();
                dtConceptosP = nh.conceptosNominaTabular(pn, int.Parse(cmbDeptoInicial.SelectedValue.ToString()),
                    int.Parse(cmbDeptoFinal.SelectedValue.ToString()),
                    int.Parse(cmbEmpleadoInicial.SelectedValue.ToString()),
                    int.Parse(cmbEmpleadoFinal.SelectedValue.ToString()),
                    tipoNomina, periodo, "P");

                dtConceptosD = nh.conceptosNominaTabular(pn, int.Parse(cmbDeptoInicial.SelectedValue.ToString()),
                    int.Parse(cmbDeptoFinal.SelectedValue.ToString()),
                    int.Parse(cmbEmpleadoInicial.SelectedValue.ToString()),
                    int.Parse(cmbEmpleadoFinal.SelectedValue.ToString()),
                    tipoNomina, periodo, "D");

                lstEmpresa = eh.obtenerEmpresa(GLOBALES.IDEMPRESA);

                dtTrabajadores = nh.trabajadoresNominaTabular(pn, int.Parse(cmbDeptoInicial.SelectedValue.ToString()),
                    int.Parse(cmbDeptoFinal.SelectedValue.ToString()),
                    int.Parse(cmbEmpleadoInicial.SelectedValue.ToString()),
                    int.Parse(cmbEmpleadoFinal.SelectedValue.ToString()),
                    tipoNomina, netocero, orden, periodo);

                dtDatos = nh.obtenerNominaTabular(pn,
                    int.Parse(cmbDeptoInicial.SelectedValue.ToString()),
                    int.Parse(cmbDeptoFinal.SelectedValue.ToString()),
                    int.Parse(cmbEmpleadoInicial.SelectedValue.ToString()),
                    int.Parse(cmbEmpleadoFinal.SelectedValue.ToString()),
                    tipoNomina,
                    netocero, orden, periodo);

                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            if (dtDatos.Rows.Count == 0)
            {
                MessageBox.Show("No es posible generar el reporte. \r\n \r\n Verifique los parametros del reporte.", "Error");
                return;
            }

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Workbooks.Add();

            Microsoft.Office.Interop.Excel._Worksheet workSheet = excel.ActiveSheet;
            Microsoft.Office.Interop.Excel.Range rng;

            excel.Cells[1, 1] = lstEmpresa[0].nombre;
            excel.Cells[1, 6] = "Periodo";
            excel.Cells[2, 1] = "RFC:";
            excel.Cells[3, 1] = "REG. PAT:";

            excel.Cells[2, 2] = lstEmpresa[0].rfc;
            excel.Cells[3, 2] = lstEmpresa[0].registro + lstEmpresa[0].digitoverificador.ToString();

            excel.Cells[2, 6] = dtpInicioPeriodo.Value.ToShortDateString();
            excel.Cells[2, 7] = dtpFinPeriodo.Value.ToShortDateString();

            //COLUMNAS DE NO DE EMPLEADO Y NOMBRE
            excel.Cells[5, 1] = "NO. EMPLEADO";
            excel.Cells[5, 2] = "NOMBRE";
            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[5, 1];
            rng.Interior.ColorIndex = 36;
            rng.Font.ColorIndex = 1;
            rng.Font.Bold = true;

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[5, 2];
            rng.Interior.ColorIndex = 36;
            rng.Font.ColorIndex = 1;
            rng.Font.Bold = true;


            //COLUMNAS DE PERCEPCIONES
            int iCol = 3;
            int colTotalPercepciones = 0;
            for (int i = 0; i < dtConceptosP.Rows.Count; i++)
            {
                excel.Cells[5, iCol] = dtConceptosP.Rows[i]["concepto"].ToString();
                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[5, iCol];
                rng.Interior.ColorIndex = 36;
                rng.Font.ColorIndex = 1;
                rng.Font.Bold = true;
                iCol++;
            }

            //COLUMNA DE TOTAL DE PERCEPCIONES
            colTotalPercepciones = iCol;
            excel.Cells[5, colTotalPercepciones] = "TOTAL PERCEPCIONES";
            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[5, colTotalPercepciones];
            rng.Interior.ColorIndex = 36;
            rng.Font.ColorIndex = 32;
            rng.Font.Bold = true;

            //COLUMNAS DE DEDUCCIONES
            iCol++;
            int colTotalDeducciones = iCol;
            for (int i = 0; i < dtConceptosD.Rows.Count; i++)
            {
                excel.Cells[5, iCol] = dtConceptosD.Rows[i]["concepto"].ToString();
                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[5, iCol];
                rng.Interior.ColorIndex = 36;
                rng.Font.ColorIndex = 1;
                rng.Font.Bold = true;
                iCol++;
                colTotalDeducciones++;
            }

            //COLUMNA DE TOTAL DE DEDUCCIONES
            iCol++;
            int colTotalNeto = iCol;
            excel.Cells[5, colTotalDeducciones] = "TOTAL DEDUCCIONES";
            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[5, colTotalDeducciones];
            rng.Interior.ColorIndex = 36;
            rng.Font.ColorIndex = 32;
            rng.Font.Bold = true;

            //COLUMNA TOTAL NETO
            excel.Cells[5, colTotalNeto] = "TOTAL NETO";
            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[5, colTotalNeto];
            rng.Interior.ColorIndex = 36;
            rng.Font.ColorIndex = 32;
            rng.Font.Bold = true;

            //SE COLOCAN LOS DATOs DEL TRABAJADOR
            int contadorDt = dtTrabajadores.Rows.Count;
            int contador = 0;
            int progreso = 0;
            int iFil = 6;
            for (int i = 0; i < dtTrabajadores.Rows.Count; i++)
            {
                progreso = (contador * 100) / contadorDt;
                toolPorcentaje.Text = progreso.ToString() + "%";
                toolEtapa.Text = "Reporte a Excel: Colocando Trabajadores";
                contador++;

                excel.Cells[iFil, 1] = dtTrabajadores.Rows[i]["NO. EMPLEADO"].ToString();
                excel.Cells[iFil, 2] = dtTrabajadores.Rows[i]["NOMBRE"].ToString();
                iFil++;
            }

            //FORMULA DE PERCEPCIONES
            string columna1, columna2, columna3;
            for (int i = 6; i < iFil; i++)
            {
                columna1 = "C";
                columna2 = GLOBALES.COLUMNAS_EXCEL(colTotalPercepciones - 1);
                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, colTotalPercepciones];
                rng.NumberFormat = "#,##0.00";
                rng.Formula = string.Format("=SUM({0}:{1})", columna1 + i.ToString(), columna2 + i.ToString());
                rng.Calculate();
            }

            //FORMULA DE DEDUCCIONES
            var colSubsidio = "";
            int colSub = 3;
            for (int i = 3; i < iCol; i++)
            {
                colSubsidio = (excel.Cells[5, i] as Microsoft.Office.Interop.Excel.Range).Value;
                if (colSubsidio == "SUBSIDIO AL EMPLEO")
                {
                    colSub = i;
                    break;
                }
            }
            columna1 = GLOBALES.COLUMNAS_EXCEL(colTotalPercepciones + 1);
            columna3 = GLOBALES.COLUMNAS_EXCEL(colSub);
            for (int i = 6; i < iFil; i++)
            {
                columna2 = GLOBALES.COLUMNAS_EXCEL(colTotalDeducciones - 1);
                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, colTotalDeducciones];
                rng.NumberFormat = "#,##0.00";
                rng.Formula = string.Format("=SUM({0}:{1}) - {2}", columna1 + i.ToString(), columna2 + i.ToString(), columna3 + i.ToString());
                rng.Calculate();
            }

            //FORMULA TOTAL NETO
            columna1 = GLOBALES.COLUMNAS_EXCEL(colTotalPercepciones);
            columna2 = GLOBALES.COLUMNAS_EXCEL(colTotalDeducciones);
            for (int i = 6; i < iFil; i++)
            {
                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, colTotalNeto];
                rng.NumberFormat = "#,##0.00";
                rng.Formula = string.Format("={0}+{1}-{2}", columna1 + i.ToString(), columna3 + i.ToString(), columna2 + i.ToString());
                rng.Calculate();
            }

            //SE COLOCAN LOS DATOS DE LAS CANTIDADES
            contadorDt = dtDatos.Rows.Count;
            contador = 0;
            progreso = 0;
            iFil = 6;
            double valorCelda = 0;
            int noEmpleadoCelda = 0;
            var conceptoColumna = "";
            int noEmpleadoDt = 0;
            for (int i = 0; i < dtDatos.Rows.Count; i++)
            {
                progreso = (contador * 100) / contadorDt;
                toolPorcentaje.Text = progreso.ToString() + "%";
                toolEtapa.Text = "Reporte a Excel: Colocando Datos";
                contador++;

                valorCelda = (excel.Cells[iFil, 1] as Microsoft.Office.Interop.Excel.Range).Value;
                noEmpleadoDt = int.Parse(dtDatos.Rows[i]["NO. EMPLEADO"].ToString());
                noEmpleadoCelda = Convert.ToInt32(valorCelda);
                if (noEmpleadoCelda == noEmpleadoDt)
                {
                    for (int j = 3; j < iCol; j++)
                    {
                        conceptoColumna = (excel.Cells[5, j] as Microsoft.Office.Interop.Excel.Range).Value;
                        if (conceptoColumna == dtDatos.Rows[i]["CONCEPTO"].ToString())
                        {
                            excel.Cells[iFil, j] = dtDatos.Rows[i]["CANTIDAD"].ToString();
                            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, j];
                            rng.NumberFormat = "#,##0.00";
                        }
                    }
                }
                else
                {
                    iFil++;
                    i--;
                    contador--;
                }
            }

            for (int i = 6; i < iFil; i++)
            {
                for (int j = 3; j < iCol; j++)
                {
                    if ((excel.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        excel.Cells[i, j] = "0";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, j];
                        rng.NumberFormat = "#,##0.00";
                    }
                }
            }

            for (int i = 3; i < iCol + 1; i++)
            {
                columna1 = GLOBALES.COLUMNAS_EXCEL(i);
                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil + 2, i];
                rng.Font.Bold = true;
                rng.NumberFormat = "#,##0.00";
                rng.Formula = string.Format("=SUM({0}:{1})", columna1 + (6).ToString(), columna1 + iFil.ToString());
            }

            excel.Range["A1", "G3"].Font.Bold = true;
            excel.Range["B:AZ"].EntireColumn.AutoFit();
            excel.Range["A6"].Select();
            excel.ActiveWindow.FreezePanes = true;

            #region REPORTE EXCEL
            //excel.Cells[1, 1] = dt.Rows[0][0];
            //excel.Cells[1, 6] = "Periodo";
            //excel.Cells[2, 1] = "RFC:";
            //excel.Cells[3, 1] = "REG. PAT:";

            //excel.Cells[2, 2] = dt.Rows[0][1];
            //excel.Cells[3, 2] = dt.Rows[0][2];

            //excel.Cells[2, 6] = dt.Rows[0][3];
            //excel.Cells[2, 7] = dt.Rows[0][4];

            ////SE COLOCAN LOS TITULOS DE LAS COLUMNAS
            //int iCol = 1;
            //for (int i = 6; i < dt.Columns.Count; i++)
            //{
            //    excel.Cells[5, iCol] = dt.Columns[i].ColumnName;
            //    iCol++;
            //}
            ////SE COLOCAN LOS DATOS
            //int contadorDt = dt.Rows.Count;
            //int contador = 0;
            //int progreso = 0;
            //iCol = 1;
            //int iFil = 6;
            //Microsoft.Office.Interop.Excel.Range rng;

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    progreso = (contador * 100) / contadorDt;
            //    toolPorcentaje.Text = progreso.ToString() + "%";
            //    toolEtapa.Text = "Reporte a Excel";
            //    contador++;
            //    if (i != dt.Rows.Count - 1)
            //    {
            //        for (int j = 6; j < dt.Columns.Count; j++)
            //        {
            //            excel.Cells[iFil, iCol] = dt.Rows[i][j];
            //            iCol++;
            //        }
            //        iFil++;
            //    }
            //    else
            //    {
            //        for (int j = 6; j < dt.Columns.Count; j++)
            //        {
            //            excel.Cells[iFil, iCol] = dt.Rows[i][j];
            //            iCol++;
            //        }
            //    }
            //    iCol = 1;
            //}
            //iFil++;

            //for (int i = 6; i < iFil; i++)
            //{
            //    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, 2];
            //    rng.Columns.AutoFit();

            //    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, 15];
            //    rng.NumberFormat = "#,##0.00";
            //    rng.Formula = string.Format("=C{0}+D{0}+E{0}+F{0}+G{0}+H{0}+I{0}+J{0}+K{0}+L{0}+M{0}+N{0}", i);

            //    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, 25];
            //    rng.NumberFormat = "#,##0.00";
            //    rng.Formula = string.Format("=P{0}+Q{0}+R{0}+S{0}+T{0}+W{0}+X{0}", i);

            //    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, 26];
            //    rng.NumberFormat = "#,##0.00";
            //    rng.Formula = string.Format("=O{0}+V{0}-Y{0}", i);
            //}

            //int suma = iFil - 1;
            //iFil++;

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 3];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(C6:C{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 4];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(D6:D{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 5];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(E6:E{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 6];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(F6:F{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 7];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(G6:G{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 8];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(H6:H{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 9];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(I6:I{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 10];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(J6:J{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 11];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(K6:K{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 12];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(L6:L{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 13];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(M6:M{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 14];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(N6:N{0})", suma.ToString());

            ////TOTAL PERCEPCIONES
            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 15];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(O6:O{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 16];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(P6:P{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 17];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(Q6:Q{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 18];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(R6:R{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 19];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(S6:S{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 20];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(T6:T{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 21];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(U6:U{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 22];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(V6:V{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 23];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(W6:W{0})", suma.ToString());

            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 24];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(X6:X{0})", suma.ToString());

            ////TOTAL DEDUCCIONES
            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 25];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(Y6:Y{0})", suma.ToString());

            ////TOTAL NETO
            //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 26];
            //rng.NumberFormat = "#,##0.00";
            //rng.Font.Bold = true;
            //rng.Formula = string.Format("=SUM(Z6:Z{0})", suma.ToString());

            //excel.Range["A1", "G3"].Font.Bold = true;
            //excel.Range["A5", "Z5"].Font.Bold = true;
            //excel.Range["B:Z"].EntireColumn.AutoFit();
            //excel.Range["A6"].Select();
            //excel.ActiveWindow.FreezePanes = true;
            //excel.Range["A5", "Z5"].Interior.ColorIndex = 36;
            //excel.Range["A5", "N5"].Font.ColorIndex = 1;
            //excel.Range["P5", "X5"].Font.ColorIndex = 1;
            //excel.Range["O5"].Font.ColorIndex = 32;
            //excel.Range["Y5"].Font.ColorIndex = 32;
            //excel.Range["Z5"].Font.ColorIndex = 32;
            //excel.Range["B6", "Z" + iFil.ToString()].NumberFormat = "#,##0.00";
            #endregion
            
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Guardar como";
            sfd.Filter = "Archivo de excel (*.xlsx)|*.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                workSheet.SaveAs(sfd.FileName);
                excel.Visible = true;
            }

            toolPorcentaje.Text = "100%";
            toolEtapa.Text = "Reporte a Excel";
        }

        private void excelGravadosExentos()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = dtpInicioPeriodo.Value;
            pn.fechafin = dtpFinPeriodo.Value;

            DataTable dt = new DataTable();
            try
            {
                cnx.Open();
                dt = nh.obtenerGravadosExentos(pn, periodo);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No es posible generar el reporte. \r\n \r\n Verifique los parametros del reporte.", "Error");
                return;
            }

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Workbooks.Add();

            Microsoft.Office.Interop.Excel._Worksheet workSheet = excel.ActiveSheet;

            excel.Cells[1, 1] = dt.Rows[0][0];
            excel.Cells[2, 1] = "RFC:";
            excel.Cells[3, 1] = "REG. PAT:";

            excel.Cells[2, 2] = dt.Rows[0][1];
            excel.Cells[3, 2] = dt.Rows[0][2];

            //SE COLOCAN LOS TITULOS DE LAS COLUMNAS
            int iCol = 1;
            for (int i = 3; i < dt.Columns.Count; i++)
            {
                excel.Cells[5, iCol] = dt.Columns[i].ColumnName;
                iCol++;
            }
            //SE COLOCAN LOS DATOS
            int contadorDt = dt.Rows.Count;
            int contador = 0;
            int progreso = 0;
            iCol = 1;
            int iFil = 6;
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                progreso = (contador * 100) / contadorDt;
                toolPorcentaje.Text = progreso.ToString() + "%";
                toolEtapa.Text = "Reporte a Excel";
                contador++;
                if (i != dt.Rows.Count - 1)
                {
                    for (int j = 3; j < dt.Columns.Count; j++)
                    {
                        excel.Cells[iFil, iCol] = dt.Rows[i][j];
                        iCol++;
                    }
                    iFil++;
                }
                else
                {
                    for (int j = 3; j < dt.Columns.Count; j++)
                    {
                        excel.Cells[iFil, iCol] = dt.Rows[i][j];
                        iCol++;
                    }
                }

                iCol = 1;

            }
            iFil++;

            excel.Range["A1", "B3"].Font.Bold = true;
            excel.Range["B:J"].EntireColumn.AutoFit();
            excel.Range["A6"].Select();
            excel.ActiveWindow.FreezePanes = true;
            excel.Range["A5", "J5"].Font.Bold = true;
            excel.Range["A5", "J5"].Interior.ColorIndex = 36;
            excel.Range["C6", "G" + iFil.ToString()].NumberFormat = "#,##0.00";

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Guardar como";
            sfd.Filter = "Archivo de excel (*.xlsx)|*.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                workSheet.SaveAs(sfd.FileName);
                excel.Visible = true;
            }

            toolPorcentaje.Text = "100%";
            toolEtapa.Text = "Reporte a Excel";
        }

        private void cmbNetoCero_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbNetoCero.Text)
            {
                case "Si": netocero = " "; break;
                case "No": netocero = " and pn.cantidad <> 0 "; break;
            }
        }

        private void cmbOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cmbOrden.Text)
            {
                case "No. de Empleado": orden = " t.noempleado "; break;
                case "Departamento": orden = " d.descripcion "; break;
                case "No. de Empleado, Departamento": orden = " t.noempleado, d.descripcion "; break;
                case "Departamento, No. de Empleado": orden = " d.descripcion, t.noempleado "; break;
            }
        }

        private void cmbTipoNomina_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbTipoNomina.Text)
            {
                case "Normal":
                    tipoNomina = GLOBALES.NORMAL;
                    dtpFinPeriodo.Enabled = false;
                    break;
                case "Extraordinaria normal":
                    tipoNomina = GLOBALES.EXTRAORDINARIO_NORMAL;
                    dtpFinPeriodo.Enabled = true;
                    break;
            }
        }

        private void frmReportes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                eh = new Empleados.Core.EmpleadosHelper();
                eh.Command = cmd;

                Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
                empleado.idempresa = GLOBALES.IDEMPRESA;

                List<Empleados.Core.Empleados> lstEmpleadoDe = new List<Empleados.Core.Empleados>();
                List<Empleados.Core.Empleados> lstEmpleadoHasta = new List<Empleados.Core.Empleados>();

                try
                {
                    cnx.Open();
                    lstEmpleadoDe = eh.obtenerEmpleadosBaja(empleado);
                    lstEmpleadoHasta = eh.obtenerEmpleadosBaja(empleado);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception error) { MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error"); }

                cmbEmpleadoInicial.DataSource = lstEmpleadoDe;
                cmbEmpleadoInicial.DisplayMember = "noempleado";
                cmbEmpleadoInicial.ValueMember = "idtrabajador";

                cmbEmpleadoFinal.DataSource = lstEmpleadoHasta;
                cmbEmpleadoFinal.DisplayMember = "noempleado";
                cmbEmpleadoFinal.ValueMember = "idtrabajador";
            }
        }

        private void cmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;

            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos p = new Periodos.Core.Periodos();

            if (!FLAG_COMBOBOX)
            {
                Periodos.Core.Periodos a = (Periodos.Core.Periodos)cmbPeriodo.SelectedValue;
                p.idperiodo = a.idperiodo;
            }
            else
                p.idperiodo = int.Parse(cmbPeriodo.SelectedValue.ToString());
            
            List<Empleados.Core.Empleados> lstEmpleadoDe = new List<Empleados.Core.Empleados>();
            List<Empleados.Core.Empleados> lstEmpleadoHasta = new List<Empleados.Core.Empleados>();

            try
            {
                cnx.Open();
                periodo = int.Parse(ph.DiasDePago(p).ToString());
                lstEmpleadoDe = eh.obtenerEmpleadosBaja(empleado, periodo);
                lstEmpleadoHasta = eh.obtenerEmpleadosBaja(empleado, periodo);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error) { MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error"); }

            cmbEmpleadoInicial.DataSource = lstEmpleadoDe;
            cmbEmpleadoInicial.DisplayMember = "noempleado";
            cmbEmpleadoInicial.ValueMember = "idtrabajador";

            cmbEmpleadoFinal.DataSource = lstEmpleadoHasta;
            cmbEmpleadoFinal.DisplayMember = "noempleado";
            cmbEmpleadoFinal.ValueMember = "idtrabajador";
        }
    }
}
