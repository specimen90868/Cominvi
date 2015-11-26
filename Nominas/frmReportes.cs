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

            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;
            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idempresa = GLOBALES.IDEMPRESA;
            empleado.estatus = (cmbEmpleados.Text == "Alta" ? GLOBALES.ACTIVO : GLOBALES.INACTIVO);

            ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
            periodo.idempresa = GLOBALES.IDEMPRESA;
            

            List<Departamento.Core.Depto> lstDeptosDe = new List<Departamento.Core.Depto>();
            List<Departamento.Core.Depto> lstDeptosHasta = new List<Departamento.Core.Depto>();

            List<Empleados.Core.Empleados> lstEmpleadoDe = new List<Empleados.Core.Empleados>();
            List<Empleados.Core.Empleados> lstEmpleadoHasta = new List<Empleados.Core.Empleados>();

            List<Periodos.Core.Periodos> lstPeriodos = new List<Periodos.Core.Periodos>();

            try
            {
                cnx.Open();
                lstDeptosDe = dh.obtenerDepartamentos(depto);
                lstDeptosHasta = dh.obtenerDepartamentos(depto);
                lstEmpleadoDe = eh.obtenerEmpleados(empleado);
                lstEmpleadoHasta = eh.obtenerEmpleados(empleado);
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

            cmbEmpleadoInicial.DataSource = lstEmpleadoDe;
            cmbEmpleadoInicial.DisplayMember = "noempleado";
            cmbEmpleadoInicial.ValueMember = "idtrabajador";

            cmbEmpleadoFinal.DataSource = lstEmpleadoHasta;
            cmbEmpleadoFinal.DisplayMember = "noempleado";
            cmbEmpleadoFinal.ValueMember = "idtrabajador";

            cmbPeriodo.DataSource = lstPeriodos;
            cmbPeriodo.DisplayMember = "pago";
            cmbPeriodo.ValueMember = "idperiodo";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
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
                vr.Show();
            }
            else 
            {
                excelTabular();
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
                    cmbEmpleados.Enabled = true;
                    cmbDeptoInicial.Enabled = true;
                    cmbDeptoFinal.Enabled = true;
                    cmbEmpleadoInicial.Enabled = true;
                    cmbEmpleadoFinal.Enabled = true;
                    noReporte = 5;
                    break;
                case "Departamentos":
                    cmbEmpleados.Enabled = true;
                    cmbDeptoInicial.Enabled = true;
                    cmbDeptoFinal.Enabled = true;
                    cmbEmpleadoInicial.Enabled = false;
                    cmbEmpleadoFinal.Enabled = false;
                    noReporte = 4;
                    break;
                case "Total General":
                    cmbEmpleados.Enabled = true;
                    cmbDeptoInicial.Enabled = false;
                    cmbDeptoFinal.Enabled = false;
                    cmbEmpleadoInicial.Enabled = false;
                    cmbEmpleadoFinal.Enabled = false;
                    noReporte = 3;
                    break;
                case "Tabular":
                    cmbEmpleados.Enabled = true;
                    cmbDeptoInicial.Enabled = true;
                    cmbDeptoFinal.Enabled = true;
                    cmbEmpleadoInicial.Enabled = true;
                    cmbEmpleadoFinal.Enabled = true;
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
                    (cmbEmpleados.Text == "Alta" ? 0 : 1));
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
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
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                progreso = (contador * 100) / contadorDt;
                toolPorcentaje.Text = progreso.ToString() + "%";
                toolEtapa.Text = "Reporte a Excel";
                contador++;
                if (i != dt.Rows.Count - 1)
                {
                    totalPercepciones += decimal.Parse(dt.Rows[i][14].ToString());
                    totalDeducciones += decimal.Parse(dt.Rows[i][20].ToString());
                    if (dt.Rows[i][5].ToString() == dt.Rows[i + 1][5].ToString())
                        for (int j = 6; j < dt.Columns.Count; j++)
                        {
                            excel.Cells[iFil, iCol] = dt.Rows[i][j];
                            iCol++;
                        }
                    else
                    {
                        for (int j = 6; j < dt.Columns.Count; j++)
                        {
                            excel.Cells[iFil, iCol] = dt.Rows[i][j];
                            iCol++;
                        }
                        iFil++;
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 1];
                        rng.Font.Bold = true;
                        excel.Cells[iFil, 1] = dt.Rows[i][5];

                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 9];
                        rng.NumberFormat = "#,##0.00";
                        rng.Font.Bold = true;
                        excel.Cells[iFil, 9] = totalPercepciones.ToString();

                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 15];
                        rng.NumberFormat = "#,##0.00";
                        rng.Font.Bold = true;
                        excel.Cells[iFil, 15] = totalDeducciones.ToString();
                        iFil++;

                        totalPercepciones = 0;
                        totalDeducciones = 0;
                    }
                }
                else
                {
                    totalPercepciones += decimal.Parse(dt.Rows[i][14].ToString());
                    totalDeducciones += decimal.Parse(dt.Rows[i][20].ToString());
                    for (int j = 6; j < dt.Columns.Count; j++)
                    {
                        excel.Cells[iFil, iCol] = dt.Rows[i][j];
                        iCol++;
                    }
                    iFil++;
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 1];
                    rng.Font.Bold = true;
                    excel.Cells[iFil, 1] = dt.Rows[i][5];

                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 9];
                    rng.NumberFormat = "#,##0.00";
                    rng.Font.Bold = true;
                    excel.Cells[iFil, 9] = totalPercepciones.ToString();

                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 15];
                    rng.NumberFormat = "#,##0.00";
                    rng.Font.Bold = true;
                    excel.Cells[iFil, 15] = totalDeducciones.ToString();
                }
                iFil++;
                iCol = 1;

            }

            excel.Range["A1", "G3"].Font.Bold = true;
            excel.Range["A5", "O5"].Font.Bold = true;
            excel.Range["A5", "O5"].Interior.ColorIndex = 36;
            excel.Range["A5", "H5"].Font.ColorIndex = 1;
            excel.Range["J5", "N5"].Font.ColorIndex = 1;
            excel.Range["I5"].Font.ColorIndex = 32;
            excel.Range["O5"].Font.ColorIndex = 32;
            excel.Range["B6", "O3000"].NumberFormat = "#,##0.00";


            workSheet.SaveAs("Reporte_Tabular.xlsx");
            excel.Visible = true;

            toolPorcentaje.Text = "100%";
            toolEtapa.Text = "Reporte a Excel";
        }
    }
}
