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
        #endregion

        #region DELEGADOS
        public delegate void delOnReporte(string netocero, string orden, int noreporte);
        public event delOnReporte OnReporte;
        #endregion

        #region VARIABLES PUBLICA
        public DateTime _inicio;
        public DateTime _fin;
        public bool _ReportePreNomina;
        public int _noReporte;
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

            cmbPeriodo.DataSource = lstPeriodos;
            cmbPeriodo.DisplayMember = "pago";
            cmbPeriodo.ValueMember = "idperiodo";

            if (_ReportePreNomina)
            {
                dtpInicioPeriodo.Value = _inicio;
                dtpFinPeriodo.Value = _fin;
                cmbPeriodo.Enabled = false;
                dtpInicioPeriodo.Enabled = false;
                dtpFinPeriodo.Enabled = false;
                cmbTipoReporte.Enabled = false;
                cmbEmpleados.Enabled = false;
                cmbDeptoInicial.Enabled = false;
                cmbDeptoFinal.Enabled = false;
                cmbEmpleadoInicial.Enabled = false;
                cmbEmpleadoFinal.Enabled = false;
            }
        } 

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!_ReportePreNomina)
            {
                if (noReporte != 6)
                {
                    frmVisorReportes vr = new frmVisorReportes();
                    vr._inicioPeriodo = dtpInicioPeriodo.Value;
                    vr._finPeriodo = dtpFinPeriodo.Value;
                    vr._tipoNomina = (cmbEmpleados.Text == "Alta" ? 0 : 1);
                    vr._noReporte = noReporte;
                    vr._deptoInicio = int.Parse(cmbDeptoInicial.SelectedValue.ToString());
                    vr._deptoFin = int.Parse(cmbDeptoFinal.SelectedValue.ToString());
                    vr._empleadoInicio = int.Parse(cmbEmpleadoInicial.SelectedValue.ToString());
                    vr._empleadoFin = int.Parse(cmbEmpleadoFinal.SelectedValue.ToString());
                    vr._netoCero = netocero;
                    vr._orden = orden;
                    vr.Show();
                }
                else
                {
                    excelTabular();
                }
            }
            else
                if (OnReporte != null)
                    OnReporte(netocero, orden, _noReporte);
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
                    cmbEmpleados.Enabled = true;
                    cmbDeptoInicial.Enabled = true;
                    cmbDeptoFinal.Enabled = true;
                    cmbEmpleadoInicial.Enabled = true;
                    cmbEmpleadoFinal.Enabled = true;
                    cmbOrden.Enabled = true;
                    cmbNetoCero.Enabled = true;
                    noReporte = 5;
                    break;
                case "Departamentos":
                    cmbEmpleados.Enabled = true;
                    cmbDeptoInicial.Enabled = true;
                    cmbDeptoFinal.Enabled = true;
                    cmbEmpleadoInicial.Enabled = false;
                    cmbEmpleadoFinal.Enabled = false;
                    cmbOrden.Enabled = true;
                    cmbNetoCero.Enabled = false;
                    noReporte = 4;
                    break;
                case "Total General":
                    cmbEmpleados.Enabled = true;
                    cmbDeptoInicial.Enabled = false;
                    cmbDeptoFinal.Enabled = false;
                    cmbEmpleadoInicial.Enabled = false;
                    cmbEmpleadoFinal.Enabled = false;
                    cmbOrden.Enabled = false;
                    cmbNetoCero.Enabled = false;
                    noReporte = 3;
                    break;
                case "Tabular":
                    cmbEmpleados.Enabled = true;
                    cmbDeptoInicial.Enabled = true;
                    cmbDeptoFinal.Enabled = true;
                    cmbEmpleadoInicial.Enabled = true;
                    cmbEmpleadoFinal.Enabled = true;
                    cmbOrden.Enabled = true;
                    cmbNetoCero.Enabled = true;
                    noReporte = 6;
                    break;
            }
        }

        private void dtpInicioPeriodo_ValueChanged(object sender, EventArgs e)
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

            DataTable dt = new DataTable();
            try
            {
                cnx.Open();
                dt = nh.obtenerNominaTabular(pn,
                    int.Parse(cmbDeptoInicial.SelectedValue.ToString()),
                    int.Parse(cmbDeptoFinal.SelectedValue.ToString()),
                    int.Parse(cmbEmpleadoInicial.SelectedValue.ToString()),
                    int.Parse(cmbEmpleadoFinal.SelectedValue.ToString()),
                    (cmbEmpleados.Text == "Alta" ? 0 : 1),
                    netocero, orden);
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
            excel.Cells[1, 6] = "Periodo";
            excel.Cells[2, 1] = "RFC:";
            excel.Cells[3, 1] = "REG. PAT:";

            excel.Cells[2, 2] = dt.Rows[0][1];
            excel.Cells[3, 2] = dt.Rows[0][2];

            excel.Cells[2, 6] = dt.Rows[0][3];
            excel.Cells[2, 7] = dt.Rows[0][4];

            //SE COLOCAN LOS TITULOS DE LAS COLUMNAS
            int iCol = 1;
            for (int i = 6; i < dt.Columns.Count; i++)
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
            Microsoft.Office.Interop.Excel.Range rng;
            decimal totalPercepciones = 0, totalDeducciones = 0;
            decimal totalSueldo = 0, totalHoras = 0, totalAsistencia = 0, totalPuntualidad = 0, totalDespensa = 0,
               totalPrimaVacacional = 0, totalVacaciones = 0, totalAguinaldo = 0, totalIsr = 0, totalInfonavitPorcentaje = 0, totalInfonavitVSM = 0, totalInfonavitFijo = 0,
               totalResponsabilidades = 0, totalPrestamoEmpresa = 0, totalIsrRetenido = 0, totalPension = 0, totalDescTrab = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                progreso = (contador * 100) / contadorDt;
                toolPorcentaje.Text = progreso.ToString() + "%";
                toolEtapa.Text = "Reporte a Excel";
                contador++;
                if (i != dt.Rows.Count - 1)
                {
                    totalSueldo += decimal.Parse(dt.Rows[i][8].ToString());
                    totalHoras += decimal.Parse(dt.Rows[i][9].ToString());
                    totalAsistencia += decimal.Parse(dt.Rows[i][10].ToString());
                    totalPuntualidad += decimal.Parse(dt.Rows[i][11].ToString());
                    totalDespensa += decimal.Parse(dt.Rows[i][12].ToString());
                    totalPrimaVacacional += decimal.Parse(dt.Rows[i][13].ToString());
                    totalVacaciones += decimal.Parse(dt.Rows[i][14].ToString());
                    totalAguinaldo += decimal.Parse(dt.Rows[i][15].ToString());
                    totalPercepciones += decimal.Parse(dt.Rows[i][16].ToString());
                    totalIsr += decimal.Parse(dt.Rows[i][17].ToString());
                    totalInfonavitPorcentaje += decimal.Parse(dt.Rows[i][18].ToString());
                    totalInfonavitVSM += decimal.Parse(dt.Rows[i][19].ToString());
                    totalInfonavitFijo += decimal.Parse(dt.Rows[i][20].ToString());
                    totalResponsabilidades += decimal.Parse(dt.Rows[i][21].ToString());
                    totalPrestamoEmpresa += decimal.Parse(dt.Rows[i][22].ToString());
                    totalIsrRetenido += decimal.Parse(dt.Rows[i][23].ToString());
                    totalPension += decimal.Parse(dt.Rows[i][24].ToString());
                    totalDescTrab += decimal.Parse(dt.Rows[i][25].ToString());
                    totalDeducciones += decimal.Parse(dt.Rows[i][26].ToString());

                    for (int j = 6; j < dt.Columns.Count; j++)
                    {
                        excel.Cells[iFil, iCol] = dt.Rows[i][j];
                        iCol++;
                    }
                    iFil++;
                }
                else
                {
                    totalSueldo += decimal.Parse(dt.Rows[i][8].ToString());
                    totalHoras += decimal.Parse(dt.Rows[i][9].ToString());
                    totalAsistencia += decimal.Parse(dt.Rows[i][10].ToString());
                    totalPuntualidad += decimal.Parse(dt.Rows[i][11].ToString());
                    totalDespensa += decimal.Parse(dt.Rows[i][12].ToString());
                    totalPrimaVacacional += decimal.Parse(dt.Rows[i][13].ToString());
                    totalVacaciones += decimal.Parse(dt.Rows[i][14].ToString());
                    totalAguinaldo += decimal.Parse(dt.Rows[i][15].ToString());
                    totalPercepciones += decimal.Parse(dt.Rows[i][16].ToString());
                    totalIsr += decimal.Parse(dt.Rows[i][17].ToString());
                    totalInfonavitPorcentaje += decimal.Parse(dt.Rows[i][18].ToString());
                    totalInfonavitVSM += decimal.Parse(dt.Rows[i][19].ToString());
                    totalInfonavitFijo += decimal.Parse(dt.Rows[i][20].ToString());
                    totalResponsabilidades += decimal.Parse(dt.Rows[i][21].ToString());
                    totalPrestamoEmpresa += decimal.Parse(dt.Rows[i][22].ToString());
                    totalIsrRetenido += decimal.Parse(dt.Rows[i][23].ToString());
                    totalPension += decimal.Parse(dt.Rows[i][24].ToString());
                    totalDescTrab += decimal.Parse(dt.Rows[i][25].ToString());
                    totalDeducciones += decimal.Parse(dt.Rows[i][26].ToString());

                    for (int j = 6; j < dt.Columns.Count; j++)
                    {
                        excel.Cells[iFil, iCol] = dt.Rows[i][j];
                        iCol++;
                    }
                }
                
                iCol = 1;

            }
            iFil++;
            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 3];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 3] = totalSueldo.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 4];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 4] = totalHoras.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 5];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 5] = totalAsistencia.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 6];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 6] = totalPuntualidad.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 7];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 7] = totalDespensa.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 8];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 8] = totalPrimaVacacional.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 9];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 9] = totalVacaciones.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 10];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 10] = totalAguinaldo.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 11];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 11] = totalPercepciones.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 12];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 12] = totalIsr.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 13];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 13] = totalInfonavitPorcentaje.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 14];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 14] = totalInfonavitVSM.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 15];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 15] = totalInfonavitFijo.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 16];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 16] = totalResponsabilidades.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 17];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 17] = totalPrestamoEmpresa.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 18];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 18] = totalIsrRetenido.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 19];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 19] = totalPension.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 20];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 20] = totalDescTrab.ToString();

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 21];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            excel.Cells[iFil, 21] = totalDeducciones.ToString();

            excel.Range["A1", "G3"].Font.Bold = true;
            excel.Range["A5", "U5"].Font.Bold = true;
            excel.Range["A5", "U5"].Interior.ColorIndex = 36;
            excel.Range["A5", "J5"].Font.ColorIndex = 1;
            excel.Range["L5", "T5"].Font.ColorIndex = 1;
            excel.Range["K5"].Font.ColorIndex = 32;
            excel.Range["U5"].Font.ColorIndex = 32;
            excel.Range["B6", "U2000"].NumberFormat = "#,##0.00";


            workSheet.SaveAs("Reporte_Tabular.xlsx");
            excel.Visible = true;

            toolPorcentaje.Text = "100%";
            toolEtapa.Text = "Reporte a Excel";
        }

        private void cmbEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;
            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.estatus = (cmbEmpleados.Text == "Alta" ? GLOBALES.ACTIVO : GLOBALES.INACTIVO);

            List<Empleados.Core.Empleados> lstEmpleadoDe = new List<Empleados.Core.Empleados>();
            List<Empleados.Core.Empleados> lstEmpleadoHasta = new List<Empleados.Core.Empleados>();

            try
            {
                cnx.Open();
                
                lstEmpleadoDe = eh.obtenerEmpleados(empleado);
                lstEmpleadoHasta = eh.obtenerEmpleados(empleado);
                
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
    }
}
