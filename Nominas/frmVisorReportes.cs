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
        SqlCommand cmd;
        ReportDataSource rd;
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
            cmd.Connection = cnx;

            switch (_noReporte)
            {
                case 0: //PreNomina
                    dsReportes.PreNominaNetosDataTable dtPreNominaNetos = new dsReportes.PreNominaNetosDataTable();
                    SqlDataAdapter daPreNominaNetos = new SqlDataAdapter();
                    cmd.CommandText = "exec stp_rptPreNominaNetos @idempresa, @fechainicio, @fechafin";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                    cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                    cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                    daPreNominaNetos.SelectCommand = cmd;
                    daPreNominaNetos.Fill(dtPreNominaNetos);
                    rd = new ReportDataSource();
                    rd.Value = dtPreNominaNetos;
                    rd.Name = "dsRptNomina";
                    rpvVisor.LocalReport.DataSources.Clear();
                    rpvVisor.LocalReport.DataSources.Add(rd);
                    rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaNetos.rdlc";
                    rpvVisor.LocalReport.ReportPath = @"rptPreNominaNetos.rdlc";
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
