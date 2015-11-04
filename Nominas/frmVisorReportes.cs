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
                case 0: //CARATULA NOMINA
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

                case 1: //EMPLEADOS NOMINA
                    dsReportes.PreNominaEmpleadosDataTable dtPreNominaEmpleados = new dsReportes.PreNominaEmpleadosDataTable();
                    SqlDataAdapter daPreNominaEmpleados = new SqlDataAdapter();
                    cmd.CommandText = "exec stp_rptPreNominaEmpleados @idempresa, @fechainicio, @fechafin";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                    cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                    cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                    daPreNominaEmpleados.SelectCommand = cmd;
                    daPreNominaEmpleados.Fill(dtPreNominaEmpleados);
                    rd = new ReportDataSource();
                    rd.Value = dtPreNominaEmpleados;
                    rd.Name = "dsReporteNominaEmpleados";
                    rpvVisor.LocalReport.DataSources.Clear();
                    rpvVisor.LocalReport.DataSources.Add(rd);
                    rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaEmpleados.rdlc";
                    rpvVisor.LocalReport.ReportPath = @"rptPreNominaEmpleados.rdlc";
                    break;

                case 2: //DEPARTAMENTOS NOMINA
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
            }

            this.rpvVisor.RefreshReport();
        }

        private void frmVisorReportes_FormClosed(object sender, FormClosedEventArgs e)
        {
            rpvVisor.Dispose();
        }
    }
}
