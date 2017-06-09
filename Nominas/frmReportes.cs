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
        CalculoNomina.Core.NominaHelper nh;
        int noReporte;
        string netocero, orden, idDepartamentos, idEmpleados;
        List<Empleados.Core.Empleados> lstEmp;
        #endregion

        #region DELEGADOS
        public delegate void delOnReporte(string netocero, string orden, int noreporte, int empleadoinicial, int empleadofinal);
        public event delOnReporte OnReporte;
        #endregion

        #region VARIABLES PUBLICA
        public DateTime _inicio;
        public DateTime _fin;
        public int _noReporte;
        public int _tipoNomina;
        public int _periodo;
        #endregion

        private void frmReportes_Load(object sender, EventArgs e)
        {
            dtpFinPeriodo.Enabled = false;

            cmbTipoReporte.SelectedIndex = 0;
            cmbNetoCero.SelectedIndex = 0;
            cmbOrden.SelectedIndex = 3;

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

            //if (_ReportePreNomina)
            //{
            //    dtpInicioPeriodo.Value = _inicio;
            //    dtpFinPeriodo.Value = _fin;
            //    dtpInicioPeriodo.Enabled = false;
            //    dtpFinPeriodo.Enabled = false;
            //    cmbTipoReporte.Enabled = false;

            //    if (_noReporte == 2)
            //    {
            //        cmbOrden.Items.Clear();
            //        cmbOrden.Items.Add("Departamento");
            //        cmbOrden.SelectedIndex = 0;
            //    }
            //    else
            //    {
            //        cmbOrden.Items.Clear();
            //        cmbOrden.Items.Add("No. de Empleado");
            //        cmbOrden.Items.Add("Departamento");
            //        cmbOrden.Items.Add("No. de Empleado, Departamento");
            //        cmbOrden.Items.Add("Departamento, No. de Empleado");
            //        cmbOrden.SelectedIndex = 0;
            //    }

            //    if (_noReporte == 9 || _noReporte == 1)
            //    {
            //        lstvEmpleados.Enabled = true;
            //    }
            //}
        } 

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            frmVisorReportes vr = new frmVisorReportes();
            vr._tipoNomina = _tipoNomina;
            vr._inicioPeriodo = dtpInicioPeriodo.Value.Date;
            vr._finPeriodo = dtpFinPeriodo.Value.Date;
            vr._periodo = _periodo;
            vr._netoCero = netocero;
            vr._orden = orden;
            vr._noReporte = noReporte;
            vr.WindowState = FormWindowState.Maximized;

            switch (noReporte)
            {
                case 3: //REPORTE TOTAL GENERAL
                    vr.Show();
                    break;

                case 4: //REPORTE POR DEPARTAMENTOS
                    if (chkTodosDeptos.Checked)
                    {
                        vr._departamentos = "";
                        vr._todos = true;
                    }
                    else
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
                            idDepartamentos = idDepartamentos.Substring(0, idDepartamentos.Length - 1);
                            vr._departamentos = idDepartamentos;
                            vr._todos = false;
                        }
                        else
                        {
                            MessageBox.Show("INFORMACÓN:\r\n\r\n" +
                                            "Por favor elija al menos un departamento.",
                                            "Información",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                            return;
                        }
                    }
                    vr.Show();
                    break;

                case 5: //REPORTE DE NOMINA POR EMPLEADO
                    if (chkTodosDeptos.Checked)
                    {
                        vr._empleados = "";
                        vr._todos = true;
                    }
                    else
                    {
                        idEmpleados = "";
                        for (int i = 0; i < lstvEmpleados.Items.Count; i++)
                        {
                            if (lstvEmpleados.Items[i].Checked)
                                idEmpleados += lstvEmpleados.Items[i].Text + ",";
                        }
                        if (idEmpleados != "")
                        {
                            idEmpleados = idEmpleados.Substring(0, idEmpleados.Length - 1);
                            vr._empleados = idEmpleados;
                            vr._todos = false;
                        }
                        else
                        {
                            MessageBox.Show("INFORMACÓN:\r\n\r\n" +
                                            "Por favor elija al menos un trabajador.",
                                            "Información",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                            return;
                        }
                    }
                    vr.Show();
                    break;

                case 6:
                    excelTabular();
                    break;

                case 7: //REPORTE RECIBOS DE NOMINA
                    if (chkTodosDeptos.Checked)
                    {
                        vr._empleados = "";
                        vr._todos = true;
                    }
                    else
                    {
                        idEmpleados = "";
                        for (int i = 0; i < lstvEmpleados.Items.Count; i++)
                        {
                            if (lstvEmpleados.Items[i].Checked)
                                idEmpleados += lstvEmpleados.Items[i].Text + ",";
                        }
                        if (idEmpleados != "")
                        {
                            idEmpleados = idEmpleados.Substring(0, idEmpleados.Length - 1);
                            vr._empleados = idEmpleados;
                            vr._todos = false;
                        }
                        else
                        {
                            MessageBox.Show("INFORMACÓN:\r\n\r\n" +
                                            "Por favor elija al menos un trabajador.",
                                            "Información",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                            return;
                        }
                    }
                    vr.Show();
                    break;

                case 9:
                    excelGravadosExentos();
                    break;
            }
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
                    lstvDepartamentos.Enabled = true;
                    lstvEmpleados.Enabled = true;
                    cmbOrden.Enabled = true;
                    cmbNetoCero.Enabled = true;
                    chkTodosDeptos.Enabled = true;
                    chkTodosEmpleados.Enabled = true;
                    noReporte = 5;
                    cmbOrden.Items.Clear();
                    cmbOrden.Items.Add("No. de Empleado");
                    cmbOrden.Items.Add("Departamento");
                    cmbOrden.Items.Add("No. de Empleado, Departamento");
                    cmbOrden.Items.Add("Departamento, No. de Empleado");
                    cmbOrden.SelectedIndex = 3;
                    break;
                case "Departamentos":
                    lstvDepartamentos.Enabled = true;
                    lstvEmpleados.Enabled = false;
                    cmbOrden.Enabled = true;
                    cmbNetoCero.Enabled = true;
                    chkTodosDeptos.Enabled = true;
                    chkTodosEmpleados.Enabled = false;
                    noReporte = 4;
                    cmbOrden.Items.Clear();
                    cmbOrden.Items.Add("Departamento");
                    cmbOrden.SelectedIndex = 0;
                    break;
                case "Total General":
                    lstvDepartamentos.Enabled = false;
                    lstvEmpleados.Enabled = false;
                    cmbOrden.Enabled = false;
                    cmbNetoCero.Enabled = false;
                    chkTodosDeptos.Enabled = false;
                    chkTodosEmpleados.Enabled = false;
                    cmbOrden.Items.Clear();
                    cmbOrden.Items.Add("No. de Empleado");
                    cmbOrden.Items.Add("Departamento");
                    cmbOrden.Items.Add("No. de Empleado, Departamento");
                    cmbOrden.Items.Add("Departamento, No. de Empleado");
                    noReporte = 3;
                    break;
                case "Tabular":
                    lstvDepartamentos.Enabled = true;
                    lstvEmpleados.Enabled = true;
                    cmbOrden.Enabled = true;
                    cmbNetoCero.Enabled = true;
                    chkTodosDeptos.Enabled = true;
                    chkTodosEmpleados.Enabled = true;
                    noReporte = 6;
                    cmbOrden.Items.Clear();
                    cmbOrden.Items.Add("No. de Empleado");
                    cmbOrden.SelectedIndex = 0;
                    break;
                case "Recibos de Nomina":
                    lstvDepartamentos.Enabled = true;
                    lstvEmpleados.Enabled = true;
                    cmbOrden.Enabled = true;
                    cmbNetoCero.Enabled = true;
                    chkTodosDeptos.Enabled = true;
                    chkTodosEmpleados.Enabled = true;
                    noReporte = 7;
                    cmbOrden.Items.Clear();
                    cmbOrden.Items.Add("No. de Empleado");
                    cmbOrden.Items.Add("Departamento");
                    cmbOrden.Items.Add("No. de Empleado, Departamento");
                    cmbOrden.Items.Add("Departamento, No. de Empleado");
                    cmbOrden.SelectedIndex = 0;
                    break;

                case "Gravados y Exentos":
                    lstvDepartamentos.Enabled = false;
                    lstvEmpleados.Enabled = false;
                    cmbOrden.Enabled = false;
                    cmbNetoCero.Enabled = false;
                    chkTodosDeptos.Enabled = false;
                    chkTodosEmpleados.Enabled = false;
                    noReporte = 9;
                    break;
            }
        }

        private void dtpInicioPeriodo_ValueChanged(object sender, EventArgs e)
        {
            if (_tipoNomina == GLOBALES.NORMAL)
            {
                if (_periodo == 7)
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
            else
            {
                dtpFinPeriodo.Value = dtpInicioPeriodo.Value;
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
            DataTable dtDatos = new DataTable();
            if (chkTodosDeptos.Checked)
                idEmpleados = "";
            else {
                idEmpleados = "";
                for (int i = 0; i < lstvEmpleados.Items.Count; i++)
                {
                    if (lstvEmpleados.Items[i].Checked)
                        idEmpleados += lstvEmpleados.Items[i].Text + ",";
                }
                if (idEmpleados != "")
                {
                    idEmpleados = idEmpleados.Substring(0, idEmpleados.Length - 1);
                }
            }

            try
            {
                cnx.Open();
                lstEmpresa = eh.obtenerEmpresa(GLOBALES.IDEMPRESA);
                dtDatos = nh.obtenerNominaTabular(pn, idEmpleados, _tipoNomina, netocero, orden, chkTodosDeptos.Checked, _periodo);
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

            //COLOCACION DE COLUMNAS
            int iCol = 1;
            int columnasTotales = 0;
            int colTotalPercepciones = 0, colTotalDeducciones = 0, colTotal = 0, colSubsidio = 0;
            for (int i = 0; i < dtDatos.Columns.Count; i++)
            {
                excel.Cells[5, iCol] = dtDatos.Columns[i].ColumnName;
                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[5, iCol];
                rng.Interior.ColorIndex = 36;
                rng.Font.Bold = true;
                switch (dtDatos.Columns[i].ColumnName)
                {
                    case "TOTAL PERCEPCIONES":
                        colTotalPercepciones = iCol;
                        rng.Font.ColorIndex = 32;
                        break;
                    case "TOTAL DEDUCCIONES":
                        colTotalDeducciones = iCol;
                        rng.Font.ColorIndex = 32;
                        break;
                    case "TOTAL":
                        colTotal = iCol;
                        rng.Font.ColorIndex = 32;
                        break;
                    default:
                        if (dtDatos.Columns[i].ColumnName == "SUBSIDIO AL EMPLEO")
                            colSubsidio = iCol;
                        rng.Font.ColorIndex = 1;
                        break;
                }
                iCol++;
            }

            columnasTotales = iCol;

            //COLOCACION DE LOS DATOS
            int contadorDt = dtDatos.Rows.Count;
            int contador = 0;
            int progreso = 0;
            int iFil = 6;
            iCol = 1;
            for (int i = 0; i < dtDatos.Rows.Count; i++)
            {
                progreso = (contador * 100) / contadorDt;
                toolPorcentaje.Text = progreso.ToString() + "%";
                toolEtapa.Text = "Excel: Colocando datos.";
                contador++;

                if (i != dtDatos.Rows.Count - 1)
                {
                    for (int j = 0; j < dtDatos.Columns.Count; j++)
                    {
                        excel.Cells[iFil, iCol] = dtDatos.Rows[i][j];
                        iCol++;
                    }
                    iFil++;
                }
                else
                {
                    for (int j = 0; j < dtDatos.Columns.Count; j++)
                    {
                        excel.Cells[iFil, iCol] = dtDatos.Rows[i][j];
                        iCol++;
                    }
                }
                iCol = 1;
            }
            iFil++;

            //FORMULA DE PERCEPCIONES
            contadorDt = iFil;
            contador = 0;
            progreso = 0;
            string columna1, columna2, columna3;
            for (int i = 6; i < iFil; i++)
            {
                columna1 = "C";
                columna2 = GLOBALES.COLUMNAS_EXCEL(colTotalPercepciones - 1);
                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, colTotalPercepciones];
                rng.Formula = string.Format("=SUM({0}:{1})", columna1 + i.ToString(), columna2 + i.ToString());

                progreso = (contador * 100) / contadorDt;
                toolPorcentaje.Text = progreso.ToString() + "%";
                toolEtapa.Text = "Excel: Formula Percepciones.";
                contador++;
            }

            //FORMULA DE DEDUCCIONES
            columna1 = GLOBALES.COLUMNAS_EXCEL(colTotalPercepciones + 1);
            columna3 = GLOBALES.COLUMNAS_EXCEL(colSubsidio);
            contador = 0;
            progreso = 0;
            for (int i = 6; i < iFil; i++)
            {
                progreso = (contador * 100) / contadorDt;
                toolPorcentaje.Text = progreso.ToString() + "%";
                toolEtapa.Text = "Excel: Formula Deducciones.";
                contador++;

                columna2 = GLOBALES.COLUMNAS_EXCEL(colTotalDeducciones - 1);
                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, colTotalDeducciones];
                if (colSubsidio != 0)
                    rng.Formula = string.Format("=SUM({0}:{1}) - {2}", columna1 + i.ToString(), columna2 + i.ToString(), columna3 + i.ToString());
                else
                    rng.Formula = string.Format("=SUM({0}:{1})", columna1 + i.ToString(), columna2 + i.ToString());
            }

            //FORMULA TOTAL NETO
            columna1 = GLOBALES.COLUMNAS_EXCEL(colTotalPercepciones);
            columna2 = GLOBALES.COLUMNAS_EXCEL(colTotalDeducciones);
            contador = 0;
            progreso = 0;
            for (int i = 6; i < iFil; i++)
            {
                progreso = (contador * 100) / contadorDt;
                toolPorcentaje.Text = progreso.ToString() + "%";
                toolEtapa.Text = "Excel: Formula Totales.";
                contador++;

                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, colTotal];
                if (colSubsidio != 0)
                    rng.Formula = string.Format("={0}+{1}-{2}", columna1 + i.ToString(), columna3 + i.ToString(), columna2 + i.ToString());
                else
                    rng.Formula = string.Format("={0}-{1}", columna1 + i.ToString(), columna2 + i.ToString());
            }

            //FORMULAS TOTALES POR COLUMNA
            for (int i = 3; i < columnasTotales; i++)
            {
                columna1 = GLOBALES.COLUMNAS_EXCEL(i);
                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil + 2, i];
                rng.Font.Bold = true;
                rng.NumberFormat = "#,##0.00";
                rng.Formula = string.Format("=SUM({0}:{1})", columna1 + (6).ToString(), columna1 + iFil.ToString());
            }

            excel.Range["C6", GLOBALES.COLUMNAS_EXCEL(colTotal) + iFil.ToString()].NumberFormat = "#,##0.00";
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
                dt = nh.obtenerGravadosExentos(pn, _periodo);
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

        private void btnObtener_Click(object sender, EventArgs e)
        {
            lstvDepartamentos.Items.Clear();
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
            dh.Command = cmd;

            List<Departamento.Core.Depto> lstDeptos = new List<Departamento.Core.Depto>();

            try
            {
                cnx.Open();
                lstDeptos = dh.obtenerDepartamentos(GLOBALES.IDEMPRESA, dtpInicioPeriodo.Value.Date, _tipoNomina, false);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener los departamentos de la empresa.", "Error");
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

        private void lstvDepartamentos_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (chkTodosEmpleados.Enabled)
            {
                if (e.Item.Checked)
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
                            lstEmp = eh.obtenerEmpleadoPorDepto(GLOBALES.IDEMPRESA, idDepartamentos, dtpInicioPeriodo.Value.Date, _tipoNomina, false);
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
                }
                else
                {
                    lstvEmpleados.Items.Clear();
                }
            }
        }

        private void chkTodosDeptos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodosDeptos.Checked)
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

        private void chkTodosEmpleados_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodosEmpleados.Checked)
            {
                for (int i = 0; i < lstvEmpleados.Items.Count; i++)
                    lstvEmpleados.Items[i].Checked = true;
            }
            else
            {
                for (int i = 0; i < lstvEmpleados.Items.Count; i++)
                    lstvEmpleados.Items[i].Checked = false;
            }
        }

    }
}
