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
        SqlCommand cmd, cmd2, cmd3;
        ReportDataSource rd, rd2, rd3;
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
        public string _orden;
        public string _netoCero;
        public string _departamentos;
        public string _empleados;
        public bool _todos;
        public int _periodo;
        #endregion

        private void frmVisorReportes_Load(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd2 = new SqlCommand();
            cmd3 = new SqlCommand();

            cmd.Connection = cnx;
            cmd2.Connection = cnx;
            cmd3.Connection = cnx;

            try
            {
                switch (_noReporte)
                {
                    case 0: //CARATULA PRENOMINA
                        dsReportes.PreNominaCaratulaDataTable dtPreNominaCaratula = new dsReportes.PreNominaCaratulaDataTable();
                        SqlDataAdapter daPreNominaCaratula = new SqlDataAdapter();
                        cmd.CommandText = "exec stp_rptPreNominaCaratula @idempresa, @tiponomina, @fechainicio, @fechafin, @periodo";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                        cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                        cmd.Parameters.AddWithValue("periodo", _periodo);
                        cmd.CommandTimeout = 90;
                        daPreNominaCaratula.SelectCommand = cmd;
                        daPreNominaCaratula.Fill(dtPreNominaCaratula);

                        dsReportes.PreNominaImagenDataTable dtPreNominaImagen = new dsReportes.PreNominaImagenDataTable();
                        SqlDataAdapter daPreNominaImagen = new SqlDataAdapter();
                        cmd2.CommandText = "exec stp_rptImagen @idpersona, @tipopersona";
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.AddWithValue("idpersona", GLOBALES.IDEMPRESA);
                        cmd2.Parameters.AddWithValue("tipopersona", 0);
                        cmd2.CommandTimeout = 90;
                        daPreNominaImagen.SelectCommand = cmd2;
                        daPreNominaImagen.Fill(dtPreNominaImagen);

                        rd = new ReportDataSource();
                        rd.Value = dtPreNominaCaratula;
                        rd.Name = "dsRptNominaCaratula";

                        rd2 = new ReportDataSource();
                        rd2.Value = dtPreNominaImagen;
                        rd2.Name = "dsCaratulaImagen";

                        rpvVisor.LocalReport.DataSources.Clear();
                        rpvVisor.LocalReport.DataSources.Add(rd);
                        rpvVisor.LocalReport.DataSources.Add(rd2);

                        rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaCaratula.rdlc";
                        rpvVisor.LocalReport.ReportPath = @"rptPreNominaCaratula.rdlc";
                        break;

                    case 1: //EMPLEADOS PRENOMINA
                        dsReportes.PreNominaEmpleadosDataTable dtPreNominaEmpleados = new dsReportes.PreNominaEmpleadosDataTable();
                        SqlDataAdapter daPreNominaEmpleados = new SqlDataAdapter();
                        cmd.CommandText = "exec stp_rptPreNominaEmpleados @idempresa, @tiponomina, @fechainicio, @fechafin, @periodo";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                        cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                        cmd.Parameters.AddWithValue("periodo", _periodo);
                        cmd.CommandTimeout = 90;
                        daPreNominaEmpleados.SelectCommand = cmd;
                        daPreNominaEmpleados.Fill(dtPreNominaEmpleados);

                        dsReportes.PreNominaCaratulaDataTable dtPreNominaCaratula2 = new dsReportes.PreNominaCaratulaDataTable();
                        SqlDataAdapter daPreNominaCaratula2 = new SqlDataAdapter();
                        cmd2.CommandText = "exec stp_rptPreNominaCaratula @idempresa, @tiponomina, @fechainicio, @fechafin, @periodo";
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd2.Parameters.AddWithValue("tiponomina", _tipoNomina);
                        cmd2.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd2.Parameters.AddWithValue("fechafin", _finPeriodo);
                        cmd2.Parameters.AddWithValue("periodo", _periodo);
                        cmd2.CommandTimeout = 90;
                        daPreNominaCaratula2.SelectCommand = cmd2;
                        daPreNominaCaratula2.Fill(dtPreNominaCaratula2);

                        dsReportes.PreNominaImagenDataTable dtPreNominaImagen2 = new dsReportes.PreNominaImagenDataTable();
                        SqlDataAdapter daPreNominaImagen2 = new SqlDataAdapter();
                        cmd3.CommandText = "exec stp_rptImagen @idpersona, @tipopersona";
                        cmd3.Parameters.Clear();
                        cmd3.Parameters.AddWithValue("idpersona", GLOBALES.IDEMPRESA);
                        cmd3.Parameters.AddWithValue("tipopersona", 0);
                        cmd3.CommandTimeout = 90;
                        daPreNominaImagen2.SelectCommand = cmd3;
                        daPreNominaImagen2.Fill(dtPreNominaImagen2);

                        rd = new ReportDataSource();
                        rd.Value = dtPreNominaEmpleados;
                        rd.Name = "dsReporteNominaEmpleados";

                        rd2 = new ReportDataSource();
                        rd2.Value = dtPreNominaCaratula2;
                        rd2.Name = "dsReporteNominaGeneral";

                        rd3 = new ReportDataSource();
                        rd3.Value = dtPreNominaImagen2;
                        rd3.Name = "dsEmpleadoImagen";

                        rpvVisor.LocalReport.DataSources.Clear();
                        rpvVisor.LocalReport.DataSources.Add(rd);
                        rpvVisor.LocalReport.DataSources.Add(rd2);
                        rpvVisor.LocalReport.DataSources.Add(rd3);

                        rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaEmpleados.rdlc";
                        rpvVisor.LocalReport.ReportPath = @"rptPreNominaEmpleados.rdlc";
                        break;

                    case 2: //DEPARTAMENTOS PRENOMINA
                        dsReportes.PreNominaDeptoDataTable dtPreNominaDepto = new dsReportes.PreNominaDeptoDataTable();
                        SqlDataAdapter daPreNominaDepto = new SqlDataAdapter();
                        cmd.CommandText = "exec stp_rptPreNominaDepto @idempresa, @tiponomina, @fechainicio, @fechafin, @periodo";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                        cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                        cmd.Parameters.AddWithValue("periodo", _periodo);
                        cmd.CommandTimeout = 90;
                        daPreNominaDepto.SelectCommand = cmd;
                        daPreNominaDepto.Fill(dtPreNominaDepto);

                        dsReportes.PreNominaCaratulaDataTable dtPreNominaCaratula1 = new dsReportes.PreNominaCaratulaDataTable();
                        SqlDataAdapter daPreNominaCaratula1 = new SqlDataAdapter();
                        cmd.CommandText = "exec stp_rptPreNominaCaratula @idempresa, @tiponomina, @fechainicio, @fechafin, @periodo";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                        cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                        cmd.Parameters.AddWithValue("periodo", _periodo);
                        cmd.CommandTimeout = 90;
                        daPreNominaCaratula1.SelectCommand = cmd;
                        daPreNominaCaratula1.Fill(dtPreNominaCaratula1);

                        dsReportes.PreNominaImagenDataTable dtPreNominaImagen3 = new dsReportes.PreNominaImagenDataTable();
                        SqlDataAdapter daPreNominaImagen3 = new SqlDataAdapter();
                        cmd3.CommandText = "exec stp_rptImagen @idpersona, @tipopersona";
                        cmd3.Parameters.Clear();
                        cmd3.Parameters.AddWithValue("idpersona", GLOBALES.IDEMPRESA);
                        cmd3.Parameters.AddWithValue("tipopersona", 0);
                        cmd3.CommandTimeout = 90;
                        daPreNominaImagen3.SelectCommand = cmd3;
                        daPreNominaImagen3.Fill(dtPreNominaImagen3);

                        rd = new ReportDataSource();
                        rd.Value = dtPreNominaDepto;
                        rd.Name = "dsReporteNominaDepto";

                        rd2 = new ReportDataSource();
                        rd2.Value = dtPreNominaCaratula1;
                        rd2.Name = "dsReporteNominaGeneral";

                        rd3 = new ReportDataSource();
                        rd3.Value = dtPreNominaImagen3;
                        rd3.Name = "dsDeptoImagen";

                        rpvVisor.LocalReport.DataSources.Clear();
                        rpvVisor.LocalReport.DataSources.Add(rd);
                        rpvVisor.LocalReport.DataSources.Add(rd2);
                        rpvVisor.LocalReport.DataSources.Add(rd3);

                        rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaDepto.rdlc";
                        rpvVisor.LocalReport.ReportPath = @"rptPreNominaDepto.rdlc";
                        break;

                    case 3: //CARATULA NOMINA
                        dsReportes.PreNominaCaratulaDataTable dtNominaCaratula = new dsReportes.PreNominaCaratulaDataTable();
                        SqlDataAdapter daNominaCaratula = new SqlDataAdapter();
                        cmd.CommandText = "exec stp_rptNominaCaratula @idempresa, @fechainicio, @fechafin, @tiponomina, @periodo";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                        cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                        cmd.Parameters.AddWithValue("periodo", _periodo);
                        cmd.CommandTimeout = 90;
                        daNominaCaratula.SelectCommand = cmd;
                        daNominaCaratula.Fill(dtNominaCaratula);

                        dsReportes.PreNominaImagenDataTable dtNominaImagen = new dsReportes.PreNominaImagenDataTable();
                        SqlDataAdapter daNominaImagen = new SqlDataAdapter();
                        cmd2.CommandText = "exec stp_rptImagen @idpersona, @tipopersona";
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.AddWithValue("idpersona", GLOBALES.IDEMPRESA);
                        cmd2.Parameters.AddWithValue("tipopersona", 0);
                        cmd2.CommandTimeout = 90;
                        daNominaImagen.SelectCommand = cmd2;
                        daNominaImagen.Fill(dtNominaImagen);


                        rd = new ReportDataSource();
                        rd.Value = dtNominaCaratula;
                        rd.Name = "dsRptNominaCaratula";

                        rd2 = new ReportDataSource();
                        rd2.Value = dtNominaImagen;
                        rd2.Name = "dsCaratulaImagen";

                        rpvVisor.LocalReport.DataSources.Clear();
                        rpvVisor.LocalReport.DataSources.Add(rd);
                        rpvVisor.LocalReport.DataSources.Add(rd2);

                        rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaCaratula.rdlc";
                        rpvVisor.LocalReport.ReportPath = @"rptPreNominaCaratula.rdlc";
                        break;

                    case 4: //DEPARTAMENTOS NOMINA
                        dsReportes.PreNominaDeptoDataTable dtNominaDepto = new dsReportes.PreNominaDeptoDataTable();
                        SqlDataAdapter daNominaDepto = new SqlDataAdapter();
                        cmd.CommandText = "exec stp_rptNominaDepto @idempresa, @fechainicio, @fechafin, @deptos, @tiponomina, @neto, @order, @todos, @periodo";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                        cmd.Parameters.AddWithValue("deptos", _departamentos);                        
                        cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                        cmd.Parameters.AddWithValue("neto", _netoCero);
                        cmd.Parameters.AddWithValue("order", _orden);
                        cmd.Parameters.AddWithValue("todos", _todos);
                        cmd.Parameters.AddWithValue("periodo", _periodo);
                        cmd.CommandTimeout = 90;
                        daNominaDepto.SelectCommand = cmd;
                        daNominaDepto.Fill(dtNominaDepto);

                        dsReportes.PreNominaCaratulaDataTable dtNominaCaratula1 = new dsReportes.PreNominaCaratulaDataTable();
                        SqlDataAdapter daNominaCaratula1 = new SqlDataAdapter();
                        cmd2.CommandText = "exec stp_rptNominaCaratula @idempresa, @fechainicio, @fechafin, @tiponomina, @periodo";
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd2.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd2.Parameters.AddWithValue("fechafin", _finPeriodo);
                        cmd2.Parameters.AddWithValue("tiponomina", _tipoNomina);
                        cmd2.Parameters.AddWithValue("periodo", _periodo);
                        cmd2.CommandTimeout = 90;
                        daNominaCaratula1.SelectCommand = cmd2;
                        daNominaCaratula1.Fill(dtNominaCaratula1);

                        dsReportes.PreNominaImagenDataTable dtNominaImagen2 = new dsReportes.PreNominaImagenDataTable();
                        SqlDataAdapter daNominaImagen2 = new SqlDataAdapter();
                        cmd3.CommandText = "exec stp_rptImagen @idpersona, @tipopersona";
                        cmd3.Parameters.Clear();
                        cmd3.Parameters.AddWithValue("idpersona", GLOBALES.IDEMPRESA);
                        cmd3.Parameters.AddWithValue("tipopersona", 0);
                        cmd3.CommandTimeout = 90;
                        daNominaImagen2.SelectCommand = cmd3;
                        daNominaImagen2.Fill(dtNominaImagen2);

                        rd = new ReportDataSource();
                        rd.Value = dtNominaDepto;
                        rd.Name = "dsReporteNominaDepto";

                        rd2 = new ReportDataSource();
                        rd2.Value = dtNominaCaratula1;
                        rd2.Name = "dsReporteNominaGeneral";

                        rd3 = new ReportDataSource();
                        rd3.Value = dtNominaImagen2;
                        rd3.Name = "dsDeptoImagen";

                        rpvVisor.LocalReport.DataSources.Clear();
                        rpvVisor.LocalReport.DataSources.Add(rd);
                        rpvVisor.LocalReport.DataSources.Add(rd2);
                        rpvVisor.LocalReport.DataSources.Add(rd3);

                        rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaDepto.rdlc";
                        rpvVisor.LocalReport.ReportPath = @"rptPreNominaDepto.rdlc";
                        break;

                    case 5: //EMPLEADOS NOMINA
                        dsReportes.PreNominaEmpleadosDataTable dtNominaEmpleados = new dsReportes.PreNominaEmpleadosDataTable();
                        SqlDataAdapter daNominaEmpleados = new SqlDataAdapter();
                        cmd.CommandText = "exec stp_rptNominaEmpleados @idempresa, @fechainicio, @fechafin, @empleados, @tiponomina, @neto, @order, @todos, @periodo";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                        cmd.Parameters.AddWithValue("empleados", _empleados);
                        cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                        cmd.Parameters.AddWithValue("neto", _netoCero);
                        cmd.Parameters.AddWithValue("order", _orden);
                        cmd.Parameters.AddWithValue("todos", _todos);
                        cmd.Parameters.AddWithValue("periodo", _periodo);
                        cmd.CommandTimeout = 90;
                        daNominaEmpleados.SelectCommand = cmd;
                        daNominaEmpleados.Fill(dtNominaEmpleados);

                        dsReportes.PreNominaCaratulaDataTable dtNominaCaratula2 = new dsReportes.PreNominaCaratulaDataTable();
                        SqlDataAdapter daNominaCaratula2 = new SqlDataAdapter();
                        cmd2.CommandText = "exec stp_rptNominaCaratula @idempresa, @fechainicio, @fechafin, @tiponomina, @periodo";
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd2.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd2.Parameters.AddWithValue("fechafin", _finPeriodo);
                        cmd2.Parameters.AddWithValue("tiponomina", _tipoNomina);
                        cmd2.Parameters.AddWithValue("periodo", _periodo);
                        cmd2.CommandTimeout = 90;
                        daNominaCaratula2.SelectCommand = cmd2;
                        daNominaCaratula2.Fill(dtNominaCaratula2);

                        dsReportes.PreNominaImagenDataTable dtNominaImagen3 = new dsReportes.PreNominaImagenDataTable();
                        SqlDataAdapter daNominaImagen3 = new SqlDataAdapter();
                        cmd3.CommandText = "exec stp_rptImagen @idpersona, @tipopersona";
                        cmd3.Parameters.Clear();
                        cmd3.Parameters.AddWithValue("idpersona", GLOBALES.IDEMPRESA);
                        cmd3.Parameters.AddWithValue("tipopersona", 0);
                        cmd3.CommandTimeout = 90;
                        daNominaImagen3.SelectCommand = cmd3;
                        daNominaImagen3.Fill(dtNominaImagen3);

                        rd = new ReportDataSource();
                        rd.Value = dtNominaEmpleados;
                        rd.Name = "dsReporteNominaEmpleados";

                        rd2 = new ReportDataSource();
                        rd2.Value = dtNominaCaratula2;
                        rd2.Name = "dsReporteNominaGeneral";

                        rd3 = new ReportDataSource();
                        rd3.Value = dtNominaImagen3;
                        rd3.Name = "dsEmpleadoImagen";

                        rpvVisor.LocalReport.DataSources.Clear();
                        rpvVisor.LocalReport.DataSources.Add(rd);
                        rpvVisor.LocalReport.DataSources.Add(rd2);
                        rpvVisor.LocalReport.DataSources.Add(rd3);

                        rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaEmpleados.rdlc";
                        rpvVisor.LocalReport.ReportPath = @"rptPreNominaEmpleados.rdlc";
                        break;
                    case 7:

                        dsReportes.PreNominaEmpleadosDataTable dtReciboEmpleados = new dsReportes.PreNominaEmpleadosDataTable();
                        SqlDataAdapter daReciboEmpleados = new SqlDataAdapter();
                        cmd.CommandText = "exec stp_rptNominaEmpleados @idempresa, @fechainicio, @fechafin, @empleados, @tiponomina, @neto, @order, @todos, @periodo";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                        cmd.Parameters.AddWithValue("empleados", _empleados);
                        cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                        cmd.Parameters.AddWithValue("neto", _netoCero);
                        cmd.Parameters.AddWithValue("order", _orden);
                        cmd.Parameters.AddWithValue("todos", _todos);
                        cmd.Parameters.AddWithValue("periodo", _periodo);
                        cmd.CommandTimeout = 90;
                        daReciboEmpleados.SelectCommand = cmd;
                        daReciboEmpleados.Fill(dtReciboEmpleados);

                        dsReportes.PreNominaImagenDataTable dtReciboImagen = new dsReportes.PreNominaImagenDataTable();
                        SqlDataAdapter daReciboImagen = new SqlDataAdapter();
                        cmd3.CommandText = "exec stp_rptImagen @idpersona, @tipopersona";
                        cmd3.Parameters.Clear();
                        cmd3.Parameters.AddWithValue("idpersona", GLOBALES.IDEMPRESA);
                        cmd3.Parameters.AddWithValue("tipopersona", 0);
                        cmd3.CommandTimeout = 90;
                        daReciboImagen.SelectCommand = cmd3;
                        daReciboImagen.Fill(dtReciboImagen);

                        rd = new ReportDataSource();
                        rd.Value = dtReciboEmpleados;
                        rd.Name = "dsRptRecibo";

                        rd2 = new ReportDataSource();
                        rd2.Value = dtReciboImagen;
                        rd2.Name = "dsReciboImagen";

                        rpvVisor.LocalReport.DataSources.Clear();
                        rpvVisor.LocalReport.DataSources.Add(rd);
                        rpvVisor.LocalReport.DataSources.Add(rd2);

                        rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaRecibo.rdlc";
                        rpvVisor.LocalReport.ReportPath = @"rptPreNominaRecibo.rdlc";

                        break;

                    case 8:

                        dsReportes.NominaRecibosDataTable dtReciboNomina = new dsReportes.NominaRecibosDataTable();
                        SqlDataAdapter daReciboNomina = new SqlDataAdapter();
                        cmd.CommandText = "exec stp_rptNominaRecibo @idempresa, @fechainicio, @fechafin, @deptoInicial, @deptoFinal, @empleadoInicial, @empleadoFinal, @tiponomina, @neto, @order, @periodo";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                        cmd.Parameters.AddWithValue("deptoinicial", _deptoInicio);
                        cmd.Parameters.AddWithValue("deptofinal", _deptoFin);
                        cmd.Parameters.AddWithValue("empleadoInicial", _empleadoInicio);
                        cmd.Parameters.AddWithValue("empleadoFinal", _empleadoFin);
                        cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                        cmd.Parameters.AddWithValue("neto", _netoCero);
                        cmd.Parameters.AddWithValue("order", _orden);
                        cmd.Parameters.AddWithValue("periodo", _periodo);
                        cmd.CommandTimeout = 300;
                        daReciboNomina.SelectCommand = cmd;
                        daReciboNomina.Fill(dtReciboNomina);

                        rd = new ReportDataSource();
                        rd.Value = dtReciboNomina;
                        rd.Name = "dsNominaRecibo";

                        rpvVisor.LocalReport.DataSources.Clear();
                        rpvVisor.LocalReport.DataSources.Add(rd);

                        rpvVisor.LocalReport.ReportEmbeddedResource = "rptNominaRecibos.rdlc";
                        rpvVisor.LocalReport.ReportPath = @"rptNominaRecibos.rdlc";

                        break;
                    case 9:

                        dsReportes.PreNominaEmpleadosDataTable dtReciboPreEmpleados = new dsReportes.PreNominaEmpleadosDataTable();
                        SqlDataAdapter daReciboPreEmpleados = new SqlDataAdapter();
                        cmd.CommandText = "exec stp_rptPreNominaEmpleados @idempresa, @tiponomina, @fechainicio, @fechafin, @periodo";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                        cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                        cmd.Parameters.AddWithValue("periodo", _periodo);
                        cmd.CommandTimeout = 90;
                        daReciboPreEmpleados.SelectCommand = cmd;
                        daReciboPreEmpleados.Fill(dtReciboPreEmpleados);

                        dsReportes.PreNominaImagenDataTable dtReciboPreImagen = new dsReportes.PreNominaImagenDataTable();
                        SqlDataAdapter daReciboPreImagen = new SqlDataAdapter();
                        cmd3.CommandText = "exec stp_rptImagen @idpersona, @tipopersona";
                        cmd3.Parameters.Clear();
                        cmd3.Parameters.AddWithValue("idpersona", GLOBALES.IDEMPRESA);
                        cmd3.Parameters.AddWithValue("tipopersona", 0);
                        cmd3.CommandTimeout = 90;
                        daReciboPreImagen.SelectCommand = cmd3;
                        daReciboPreImagen.Fill(dtReciboPreImagen);

                        rd = new ReportDataSource();
                        rd.Value = dtReciboPreEmpleados;
                        rd.Name = "dsRptRecibo";

                        rd2 = new ReportDataSource();
                        rd2.Value = dtReciboPreImagen;
                        rd2.Name = "dsReciboImagen";

                        rpvVisor.LocalReport.DataSources.Clear();
                        rpvVisor.LocalReport.DataSources.Add(rd);
                        rpvVisor.LocalReport.DataSources.Add(rd2);

                        rpvVisor.LocalReport.ReportEmbeddedResource = "rptPreNominaRecibo.rdlc";
                        rpvVisor.LocalReport.ReportPath = @"rptPreNominaRecibo.rdlc";

                        break;
                    case 10:

                        dsReportes.NominaRecibosDataTable dtImpresionNomina = new dsReportes.NominaRecibosDataTable();
                        SqlDataAdapter daImpresionNomina = new SqlDataAdapter();
                        cmd.CommandText = "exec stp_rptReciboCfdi @idempresa, @fechainicio, @fechafin, @empleados, @tiponomina, @todos, @periodo";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd.Parameters.AddWithValue("fechafin", _finPeriodo);
                        cmd.Parameters.AddWithValue("todos", _todos);
                        cmd.Parameters.AddWithValue("empleados", _empleados);
                        cmd.Parameters.AddWithValue("tiponomina", _tipoNomina);
                        cmd.Parameters.AddWithValue("periodo", _periodo);
                        cmd.CommandTimeout = 300;
                        daImpresionNomina.SelectCommand = cmd;
                        daImpresionNomina.Fill(dtImpresionNomina);

                        rd = new ReportDataSource();
                        rd.Value = dtImpresionNomina;
                        rd.Name = "dsNominaRecibo";

                        rpvVisor.LocalReport.DataSources.Clear();
                        rpvVisor.LocalReport.DataSources.Add(rd);

                        rpvVisor.LocalReport.ReportEmbeddedResource = "rptNominaRecibos.rdlc";
                        rpvVisor.LocalReport.ReportPath = @"rptNominaRecibos.rdlc";
                        dtImpresionNomina.Dispose();
                        daImpresionNomina.Dispose();
                        break;

                    case 11:
                        dsReportes.EstatusEmpleadoDataTable dtEstatusEmpleado = new dsReportes.EstatusEmpleadoDataTable();
                        dsReportes.AltasDataTable dtAltaEmpleado = new dsReportes.AltasDataTable();
                        dsReportes.ReingresosDataTable dtReingresoEmpledo = new dsReportes.ReingresosDataTable();
                        dsReportes.BajasDataTable dtBajaEmpleado = new dsReportes.BajasDataTable();
                        dsReportes.FaltasDataTable dtFaltasEmpleado = new dsReportes.FaltasDataTable();
                        dsReportes.IncidenciasDataTable dtIncidenciasEmpleado = new dsReportes.IncidenciasDataTable();
                        dsReportes.VacacionesPrimaDataTable dtVacacionesEmpleado = new dsReportes.VacacionesPrimaDataTable();
                        dsReportes.ConceptosDataTable dtConceptosEmpleado = new dsReportes.ConceptosDataTable();
                        dsReportes.PreNominaImagenDataTable dtImagenEmpleado = new dsReportes.PreNominaImagenDataTable();

                        SqlDataAdapter daEstatusEmpleado = new SqlDataAdapter();
                        SqlDataAdapter daAltaEmpleado = new SqlDataAdapter();
                        SqlDataAdapter daReingresoEmpleado = new SqlDataAdapter();
                        SqlDataAdapter daBajaEmpleado = new SqlDataAdapter();
                        SqlDataAdapter daFaltasEmpleado = new SqlDataAdapter();
                        SqlDataAdapter daIncideciasEmpleado = new SqlDataAdapter();
                        SqlDataAdapter daVacacionesEmplead = new SqlDataAdapter();
                        SqlDataAdapter daConceptosEmpleado = new SqlDataAdapter();
                        SqlDataAdapter daImagenEmpleado = new SqlDataAdapter();
                        //ESTATUS EMPLEADOS
                        cmd.CommandText = @"select t.idtrabajador, t.nombrecompleto, t.noempleado, t.fechaantiguedad,
                            t.sdi, t.estatus as estatusnomina, te.estatus as estatuscatalogo
                            from trabajadores t inner join trabajadoresestatus te
                            on t.idtrabajador = te.idtrabajador
                            where te.idempresa = @idempresa and te.idtrabajador = @idtrabajador";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("idtrabajador", _empleadoInicio);
                        cmd.CommandTimeout = 90;
                        daEstatusEmpleado.SelectCommand = cmd;
                        daEstatusEmpleado.Fill(dtEstatusEmpleado);
                        //ALTAS DEL EMPLEADO
                        cmd.CommandText = @"select idtrabajador, fechaingreso, diasproporcionales, periodoinicio, periodofin
                        from suaAltas where idempresa = @idempresa and periodoinicio = @periodoinicio and idtrabajador = @idtrabajador";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("idtrabajador", _empleadoInicio);
                        cmd.Parameters.AddWithValue("periodoinicio", _inicioPeriodo);
                        cmd.CommandTimeout = 90;
                        daAltaEmpleado.SelectCommand = cmd;
                        daAltaEmpleado.Fill(dtAltaEmpleado);
                        //REINGRESO DEL EMPLEADO
                        cmd.CommandText = @"select idtrabajador, fechaingreso, diasproporcionales, periodoinicio, periodofin
                        from suaReingresos where idempresa = @idempresa and periodoinicio = @periodoinicio and idtrabajador = @idtrabajador";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("idtrabajador", _empleadoInicio);
                        cmd.Parameters.AddWithValue("periodoinicio", _inicioPeriodo);
                        cmd.CommandTimeout = 90;
                        daReingresoEmpleado.SelectCommand = cmd;
                        daReingresoEmpleado.Fill(dtReingresoEmpledo);
                        //BAJA DEL EMPLEADO
                        cmd.CommandText = @"select idtrabajador, fecha, diasproporcionales, periodoinicio, periodofin
                        from suaBajas where idempresa = @idempresa and periodoinicio = @periodoinicio and idtrabajador = @idtrabajador";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("idtrabajador", _empleadoInicio);
                        cmd.Parameters.AddWithValue("periodoinicio", _inicioPeriodo);
                        cmd.CommandTimeout = 90;
                        daBajaEmpleado.SelectCommand = cmd;
                        daBajaEmpleado.Fill(dtBajaEmpleado);
                        //FALTAS DEL EMPLEADO
                        cmd.CommandText = @"select idtrabajador, fecha, faltas, fechainicio, fechafin 
                        from faltas where idempresa = @idempresa and fechainicio = @fechainicio and idtrabajador = @idtrabajador";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("idtrabajador", _empleadoInicio);
                        cmd.Parameters.AddWithValue("fechainicio", _inicioPeriodo);
                        cmd.CommandTimeout = 90;
                        daFaltasEmpleado.SelectCommand = cmd;
                        daFaltasEmpleado.Fill(dtFaltasEmpleado);
                        //INCIDENCAS DEL EMPLEADO
                        cmd.CommandText = @"select idtrabajador, fechainicio, fechafin, isnull(sum(dias),0) as dias, 
                        periodoinicio, periodofin from incidencias
                        where idempresa = @idempresa and periodoinicio = @periodoinicio and idtrabajador = @idtrabajador
                        group by idtrabajador, fechainicio, fechafin, periodoinicio, periodofin";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("idtrabajador", _empleadoInicio);
                        cmd.Parameters.AddWithValue("periodoinicio", _inicioPeriodo);
                        cmd.CommandTimeout = 90;
                        daIncideciasEmpleado.SelectCommand = cmd;
                        daIncideciasEmpleado.Fill(dtIncidenciasEmpleado);
                        //VACACIONES DEL EMPLEADO
                        cmd.CommandText = @"select idtrabajador, diaspago, periodoinicio, periodofin,
                        case when vacacionesprima = 'P' then 'Prima Vacacional' else 'Vacaciones' end vacacionesprima
                        from vacacionesprima
                        where idempresa = @idempresa and periodoinicio = @periodoinicio and idtrabajador = @idtrabajador";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempresa", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("idtrabajador", _empleadoInicio);
                        cmd.Parameters.AddWithValue("periodoinicio", _inicioPeriodo);
                        cmd.CommandTimeout = 90;
                        daVacacionesEmplead.SelectCommand = cmd;
                        daVacacionesEmplead.Fill(dtVacacionesEmpleado);
                        //CONCEPTOS DEL EMPLEADO
                        cmd.CommandText = @"select ct.idempleado, count(idconcepto) as cantidad, c.concepto
                        from conceptotrabajador ct
                        inner join conceptos c on ct.idconcepto = c.id
                        where idempleado = @idempleado
                        group by ct.idempleado, c.concepto, c.noconcepto";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idempleado", _empleadoInicio);
                        cmd.CommandTimeout = 90;
                        daConceptosEmpleado.SelectCommand = cmd;
                        daConceptosEmpleado.Fill(dtConceptosEmpleado);
                        //LOGO DE LA EMPRESA
                        cmd.CommandText = "exec stp_rptImagen @idpersona, @tipopersona";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("idpersona", GLOBALES.IDEMPRESA);
                        cmd.Parameters.AddWithValue("tipopersona", 0);
                        cmd.CommandTimeout = 90;
                        daImagenEmpleado.SelectCommand = cmd;
                        daImagenEmpleado.Fill(dtImagenEmpleado);

                        rpvVisor.LocalReport.DataSources.Clear();

                        rd = new ReportDataSource();
                        rd.Value = dtEstatusEmpleado;
                        rd.Name = "dsEstatusEmpleado";
                        rpvVisor.LocalReport.DataSources.Add(rd);

                        rd = new ReportDataSource();
                        rd.Value = dtAltaEmpleado;
                        rd.Name = "dsAltas";
                        rpvVisor.LocalReport.DataSources.Add(rd);

                        rd = new ReportDataSource();
                        rd.Value = dtReingresoEmpledo;
                        rd.Name = "dsReingresos";
                        rpvVisor.LocalReport.DataSources.Add(rd);

                        rd = new ReportDataSource();
                        rd.Value = dtBajaEmpleado;
                        rd.Name = "dsBajas";
                        rpvVisor.LocalReport.DataSources.Add(rd);

                        rd = new ReportDataSource();
                        rd.Value = dtFaltasEmpleado;
                        rd.Name = "dsFaltas";
                        rpvVisor.LocalReport.DataSources.Add(rd);

                        rd = new ReportDataSource();
                        rd.Value = dtIncidenciasEmpleado;
                        rd.Name = "dsIncidencias";
                        rpvVisor.LocalReport.DataSources.Add(rd);

                        rd = new ReportDataSource();
                        rd.Value = dtVacacionesEmpleado;
                        rd.Name = "dsVacacionesPrima";
                        rpvVisor.LocalReport.DataSources.Add(rd);

                        rd = new ReportDataSource();
                        rd.Value = dtConceptosEmpleado;
                        rd.Name = "dsConceptos";
                        rpvVisor.LocalReport.DataSources.Add(rd);

                        rd = new ReportDataSource();
                        rd.Value = dtImagenEmpleado;
                        rd.Name = "dsImagen";
                        rpvVisor.LocalReport.DataSources.Add(rd);

                        rpvVisor.LocalReport.ReportEmbeddedResource = "rptNominaDiagnostico.rdlc";
                        rpvVisor.LocalReport.ReportPath = @"rptNominaDiagnostico.rdlc";
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("El reporte no se puede generar debido al tiempo de espera. \r\n \r\n Tiempo de espera agotado.", "Error");
            }
            this.rpvVisor.ZoomPercent = 150;
            this.rpvVisor.RefreshReport();
        }

        private void frmVisorReportes_FormClosed(object sender, FormClosedEventArgs e)
        {
            rpvVisor.Dispose();
        }
    }
}
