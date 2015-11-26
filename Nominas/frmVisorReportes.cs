using Microsoft.Reporting.WinForms;
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
    public partial class frmVisorReportes : Form
    {
        public frmVisorReportes()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd, cmd2;
        ReportDataSource rd, rd2;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        #endregion

        #region VARIABLES PUBLICAS
        public int _noReporte;
        public DateTime _inicioPeriodo;
        public DateTime _finPeriodo;
        public int _tipoNomina;
        public int _deptoInicio;
        public int _deptoFin;
        public int _empleadoInicio;
        public int _empleadoFin;
        #endregion

        private void frmVisorReportes_Load(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd2 = new SqlCommand();
            cmd.Connection = cnx;
            cmd2.Connection = cnx;

            switch (_noReporte)
            {
                case 0: //CARATULA PRENOMINA
                    dsReportes.PreNominaCaratulaDataTable dtPreNominaCaratula = new dsReportes.PreNominaCaratulaDataTable();
                    SqlDataAdapter daPreNominaCaratula = new SqlDataAdapter();
                    cmd.CommandText = "exec stp_rptPreNominaCaratula @idempresa, @fechainicio, @fechafin";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                    cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                    cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                    daPreNominaCaratula.SelectCommand = cmd;
                    daPreNominaCaratula.Fill(dtPreNominaCaratula);
                    rd = new ReportDataSource();
                    rd.Value = dtPreNominaCaratula;
                    rd.Name = "dsRptNominaCaratula";
                    rpvVisor.LocalReport.DataSources.Clear();
                    rpvVisor.LocalReport.DataSources.Add(rd);
                    rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaCaratula.rdlc";
                    rpvVisor.LocalReport.ReportPath = @"rptPreNominaCaratula.rdlc";
                    break;

                case 1: //EMPLEADOS PRENOMINA
                    dsReportes.PreNominaEmpleadosDataTable dtPreNominaEmpleados = new dsReportes.PreNominaEmpleadosDataTable();
                    SqlDataAdapter daPreNominaEmpleados = new SqlDataAdapter();
                    cmd.CommandText = "exec stp_rptPreNominaEmpleados @idempresa, @fechainicio, @fechafin";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                    cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                    cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                    daPreNominaEmpleados.SelectCommand = cmd;
                    daPreNominaEmpleados.Fill(dtPreNominaEmpleados);

                    dsReportes.PreNominaCaratulaDataTable dtPreNominaCaratula2 = new dsReportes.PreNominaCaratulaDataTable();
                    SqlDataAdapter daPreNominaCaratula2 = new SqlDataAdapter();
                    cmd2.CommandText = "exec stp_rptPreNominaCaratula @idempresa, @fechainicio, @fechafin";
                    cmd2.Parameters.Clear();
                    cmd2.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                    cmd2.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                    cmd2.Parameters.AddWithValue("fechafin", _finPeriodo);
                    daPreNominaCaratula2.SelectCommand = cmd2;
                    daPreNominaCaratula2.Fill(dtPreNominaCaratula2);

                    rd = new ReportDataSource();
                    rd.Value = dtPreNominaEmpleados;
                    rd.Name = "dsReporteNominaEmpleados";

                    rd2 = new ReportDataSource();
                    rd2.Value = dtPreNominaCaratula2;
                    rd2.Name = "dsReporteNominaGeneral";

                    rpvVisor.LocalReport.DataSources.Clear();
                    rpvVisor.LocalReport.DataSources.Add(rd);
                    rpvVisor.LocalReport.DataSources.Add(rd2);

                    rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaEmpleados.rdlc";
                    rpvVisor.LocalReport.ReportPath = @"rptPreNominaEmpleados.rdlc";
                    break;

                case 2: //DEPARTAMENTOS PRENOMINA
                    dsReportes.PreNominaDeptoDataTable dtPreNominaDepto = new dsReportes.PreNominaDeptoDataTable();
                    SqlDataAdapter daPreNominaDepto = new SqlDataAdapter();
                    cmd.CommandText = "exec stp_rptPreNominaDepto @idempresa, @fechainicio, @fechafin";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                    cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                    cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                    daPreNominaDepto.SelectCommand = cmd;
                    daPreNominaDepto.Fill(dtPreNominaDepto);

                    dsReportes.PreNominaCaratulaDataTable dtPreNominaCaratula1 = new dsReportes.PreNominaCaratulaDataTable();
                    SqlDataAdapter daPreNominaCaratula1 = new SqlDataAdapter();
                    cmd.CommandText = "exec stp_rptPreNominaCaratula @idempresa, @fechainicio, @fechafin";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                    cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                    cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                    daPreNominaCaratula1.SelectCommand = cmd;
                    daPreNominaCaratula1.Fill(dtPreNominaCaratula1);

                    rd = new ReportDataSource();
                    rd.Value = dtPreNominaDepto;
                    rd.Name = "dsReporteNominaDepto";

                    rd2 = new ReportDataSource();
                    rd2.Value = dtPreNominaCaratula1;
                    rd2.Name = "dsReporteNominaGeneral";

                    rpvVisor.LocalReport.DataSources.Clear();
                    rpvVisor.LocalReport.DataSources.Add(rd);
                    rpvVisor.LocalReport.DataSources.Add(rd2);

                    rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaDepto.rdlc";
                    rpvVisor.LocalReport.ReportPath = @"rptPreNominaDepto.rdlc";
                    break;

                case 3: //CARATULA NOMINA
                    dsReportes.PreNominaCaratulaDataTable dtNominaCaratula = new dsReportes.PreNominaCaratulaDataTable();
                    SqlDataAdapter daNominaCaratula = new SqlDataAdapter();
                    cmd.CommandText = "exec stp_rptNominaCaratula @idempresa, @fechainicio, @fechafin, @tiponomina";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                    cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                    cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                    cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                    daNominaCaratula.SelectCommand = cmd;
                    daNominaCaratula.Fill(dtNominaCaratula);
                    rd = new ReportDataSource();
                    rd.Value = dtNominaCaratula;
                    rd.Name = "dsRptNominaCaratula";
                    rpvVisor.LocalReport.DataSources.Clear();
                    rpvVisor.LocalReport.DataSources.Add(rd);
                    rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaCaratula.rdlc";
                    rpvVisor.LocalReport.ReportPath = @"rptPreNominaCaratula.rdlc";
                    break;

                case 4: //DEPARTAMENTOS NOMINA
                    dsReportes.PreNominaDeptoDataTable dtNominaDepto = new dsReportes.PreNominaDeptoDataTable();
                    SqlDataAdapter daNominaDepto = new SqlDataAdapter();
                    cmd.CommandText = "exec stp_rptNominaDepto @idempresa, @fechainicio, @fechafin, @deptoinicial, @deptofinal, @tiponomina";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                    cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                    cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                    cmd.Parameters.AddWithValue("deptoinicial", _deptoInicio);
                    cmd.Parameters.AddWithValue("deptofinal", _deptoFin);
                    cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                    daNominaDepto.SelectCommand = cmd;
                    daNominaDepto.Fill(dtNominaDepto);

                    dsReportes.PreNominaCaratulaDataTable dtNominaCaratula1 = new dsReportes.PreNominaCaratulaDataTable();
                    SqlDataAdapter daNominaCaratula1 = new SqlDataAdapter();
                    cmd.CommandText = "exec stp_rptNominaCaratula @idempresa, @fechainicio, @fechafin, @tiponomina";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                    cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                    cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                    cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                    daNominaCaratula1.SelectCommand = cmd;
                    daNominaCaratula1.Fill(dtNominaCaratula1);

                    rd = new ReportDataSource();
                    rd.Value = dtNominaDepto;
                    rd.Name = "dsReporteNominaDepto";

                    rd2 = new ReportDataSource();
                    rd2.Value = dtNominaCaratula1;
                    rd2.Name = "dsReporteNominaGeneral";

                    rpvVisor.LocalReport.DataSources.Clear();
                    rpvVisor.LocalReport.DataSources.Add(rd);
                    rpvVisor.LocalReport.DataSources.Add(rd2);

                    rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaDepto.rdlc";
                    rpvVisor.LocalReport.ReportPath = @"rptPreNominaDepto.rdlc";
                    break;

                case 5: //EMPLEADOS NOMINA
                    dsReportes.PreNominaEmpleadosDataTable dtNominaEmpleados = new dsReportes.PreNominaEmpleadosDataTable();
                    SqlDataAdapter daNominaEmpleados = new SqlDataAdapter();
                    cmd.CommandText = "exec stp_rptNominaEmpleados @idempresa, @fechainicio, @fechafin, @deptoInicial, @deptoFinal, @empleadoInicial, @empleadoFinal, @tiponomina";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                    cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                    cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                    cmd.Parameters.AddWithValue("deptoinicial", _deptoInicio);
                    cmd.Parameters.AddWithValue("deptofinal", _deptoFin);
                    cmd.Parameters.AddWithValue("empleadoInicial", _empleadoInicio);
                    cmd.Parameters.AddWithValue("empleadoFinal", _empleadoFin);
                    cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                    daNominaEmpleados.SelectCommand = cmd;
                    daNominaEmpleados.Fill(dtNominaEmpleados);

                    dsReportes.PreNominaCaratulaDataTable dtNominaCaratula2 = new dsReportes.PreNominaCaratulaDataTable();
                    SqlDataAdapter daNominaCaratula2 = new SqlDataAdapter();
                    cmd2.CommandText = "exec stp_rptNominaCaratula @idempresa, @fechainicio, @fechafin, @tiponomina";
                    cmd2.Parameters.Clear();
                    cmd2.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                    cmd2.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                    cmd2.Parameters.AddWithValue("fechafin", _finPeriodo);
                    cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                    daNominaCaratula2.SelectCommand = cmd2;
                    daNominaCaratula2.Fill(dtNominaCaratula2);

                    rd = new ReportDataSource();
                    rd.Value = dtNominaEmpleados;
                    rd.Name = "dsReporteNominaEmpleados";

                    rd2 = new ReportDataSource();
                    rd2.Value = dtNominaCaratula2;
                    rd2.Name = "dsReporteNominaGeneral";

                    rpvVisor.LocalReport.DataSources.Clear();
                    rpvVisor.LocalReport.DataSources.Add(rd);
                    rpvVisor.LocalReport.DataSources.Add(rd2);

                    rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaEmpleados.rdlc";
                    rpvVisor.LocalReport.ReportPath = @"rptPreNominaEmpleados.rdlc";
                    break;
            }

            this.rpvVisor.RefreshReport();
        }

        private void frmVisorReportes_FormClosed(object sender, FormClosedEventArgs e)
        {
            rpvVisor.Dispose();
        }
    }
}
