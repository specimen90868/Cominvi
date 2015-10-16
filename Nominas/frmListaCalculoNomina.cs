﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nominas
{
    public partial class frmListaCalculoNomina : Form
    {
        public frmListaCalculoNomina()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        SqlBulkCopy bulk;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        string ruta, nombreEmpresa;
        string ExcelConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;'";
        int idEmpresa;
        Empresas.Core.EmpresasHelper eh;
        Empleados.Core.EmpleadosHelper emph;
        Conceptos.Core.ConceptosHelper ch;
        Vacaciones.Core.VacacionesHelper vh;
        CalculoNomina.Core.NominaHelper nh;
        DateTime inicio, fin;
        List<CalculoNomina.Core.DatosEmpleado> lstEmpleadosNomina;
        CheckBox chk;
        #endregion

        #region VARIABLES PUBLICAS
        public int _periodo;
        #endregion

        private void toolCargar_Click(object sender, EventArgs e)
        {
            string conStr, sheetName;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleccionar Excel";
            ofd.RestoreDirectory = false;
            ofd.InitialDirectory = @"C:\";
            if (DialogResult.OK == ofd.ShowDialog())
            {
                ruta = ofd.FileName;
                conStr = string.Empty;
                conStr = string.Format(ExcelConString, ruta);

                try
                {

                    using (OleDbConnection con = new OleDbConnection(conStr))
                    {
                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            cmd.Connection = con;
                            con.Open();
                            DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            sheetName = dtExcelSchema.Rows[5]["TABLE_NAME"].ToString();
                            con.Close();
                        }
                    }

                    using (OleDbConnection con = new OleDbConnection(conStr))
                    {
                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            using (OleDbDataAdapter oda = new OleDbDataAdapter())
                            {
                                DataTable dt = new DataTable();
                                cmd.CommandText = "SELECT * From [" + sheetName + "]";
                                cmd.Connection = con;
                                con.Open();
                                oda.SelectCommand = cmd;
                                oda.Fill(dt);
                                con.Close();

                                nombreEmpresa = dt.Columns[1].ColumnName;

                                for (int i = 2; i < dt.Rows.Count; i++)
                                {
                                    dgvEmpleados.Rows.Add(true,
                                        dt.Rows[i][0].ToString(), //NO EMPLEADO
                                        dt.Rows[i][1].ToString(), //NOMBRE
                                        dt.Rows[i][2].ToString(), //PATERNO
                                        dt.Rows[i][3].ToString(), //MATERNO
                                        0,0,0,0,
                                        dt.Rows[i][4].ToString(), //HORAS EXTRAS DOBLES
                                        dt.Rows[i][5].ToString(), //INICIO PERIODO
                                        dt.Rows[i][6].ToString()); //FIN PERIODO
                                }

                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    dgvEmpleados.AutoResizeColumn(i);
                                }
                            }
                        }
                    }
                    inicio = DateTime.Parse(dgvEmpleados.Rows[0].Cells[10].Value.ToString());
                    fin = DateTime.Parse(dgvEmpleados.Rows[0].Cells[11].Value.ToString());
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n Verifique que el archivo este cerrado. \r\n \r\n Descripcion: " + error.Message);
                }
            }
        }

        private void toolPrenomina_Click(object sender, EventArgs e)
        {
            cargaEmpleados();
        }

        private void frmListaCalculoNomina_Load(object sender, EventArgs e)
        {
            #region DISEÑO EXTRA DEL GRID
            chk = new CheckBox();
            Rectangle rect = dgvEmpleados.GetCellDisplayRectangle(0, -1, true);
            chk.Size = new Size(18, 18);
            chk.Location = new Point(50, 10);
            chk.CheckedChanged += new EventHandler(chk_CheckedChanged);
            dgvEmpleados.Controls.Add(chk);
            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvEmpleados.Columns["seleccion"].HeaderCell.Style = estilo;
            #endregion

            NominaActual();
        }

        void chk_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvEmpleados.Rows)
                if (chk.Checked)
                    row.Cells[0].Value = true;
                else
                    row.Cells[0].Value = false;
        }

        private void toolDepartamento_Click(object sender, EventArgs e)
        {
            frmFiltroNomina fn = new frmFiltroNomina();
            fn._filtro = 0;
            fn.OnFiltro += fn_OnFiltro;
            fn.ShowDialog();
        }

        void fn_OnFiltro(int filtro, int de, int hasta)
        {
            switch (filtro)
            { 
                case 0:
                    var empleadoDepto = from f in lstEmpleadosNomina where f.iddepartamento >= de && f.iddepartamento <= hasta select f;
                    dgvEmpleados.DataSource = empleadoDepto.ToList();
                    for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
                    {
                        dgvEmpleados.AutoResizeColumn(i);
                    }
                    break;
                case 1:
                    var empleadoPuesto = from f in lstEmpleadosNomina where f.idpuesto >= de && f.idpuesto <= hasta select f;
                    dgvEmpleados.DataSource = empleadoPuesto.ToList();
                    for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
                    {
                        dgvEmpleados.AutoResizeColumn(i);
                    }
                    break;
            }
        }

        private void toolPuesto_Click(object sender, EventArgs e)
        {
            frmFiltroNomina fn = new frmFiltroNomina();
            fn._filtro = 1;
            fn.OnFiltro += fn_OnFiltro;
            fn.ShowDialog();
        }

        private void toolTodos_Click(object sender, EventArgs e)
        {
            dgvEmpleados.DataSource = lstEmpleadosNomina;
            for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
            {
                dgvEmpleados.AutoResizeColumn(i);
            }
        }

        private void dgvEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvEmpleados.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void toolCalcular_Click(object sender, EventArgs e)
        {
            workerCalculo.RunWorkerAsync();
        }

        private void dtpPeriodoInicio_ValueChanged(object sender, EventArgs e)
        {
            NominaActual();
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            int existe = 0;
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            try
            {
                cnx.Open();
                existe = (int)nh.existeNomina(GLOBALES.IDEMPRESA, dtpPeriodoInicio.Value, dtpPeriodoFin.Value);
                cnx.Close();
                cnx.Dispose();

                if (existe != 0)
                {
                    MessageBox.Show("NOMINA CALCULADA \r\n \r\n " +
                                        "El periodo seleccionado se encuenta: \r\n " +
                                        "Calculado y Autorizado.", "Información");
                    toolCalcular.Enabled = false;
                    toolPrenomina.Enabled = false;
                }
                else
                {
                    toolCalcular.Enabled = true;
                    toolPrenomina.Enabled = true;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
        }

        private void NominaActual()
        {
            if (_periodo == 7)
            {
                DateTime dt = dtpPeriodoInicio.Value;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                dtpPeriodoInicio.Value = dt;
                dtpPeriodoFin.Value = dt.AddDays(6);
            }
            else
            {
                if (dtpPeriodoInicio.Value.Day <= 15)
                {
                    dtpPeriodoInicio.Value = new DateTime(dtpPeriodoInicio.Value.Year, dtpPeriodoInicio.Value.Month, 1);
                    dtpPeriodoFin.Value = new DateTime(dtpPeriodoInicio.Value.Year, dtpPeriodoInicio.Value.Month, 15);
                }
                else
                {
                    dtpPeriodoInicio.Value = new DateTime(dtpPeriodoInicio.Value.Year, dtpPeriodoInicio.Value.Month, 16);
                    dtpPeriodoFin.Value = new DateTime(dtpPeriodoInicio.Value.Year, dtpPeriodoInicio.Value.Month, DateTime.DaysInMonth(dtpPeriodoInicio.Value.Year, dtpPeriodoInicio.Value.Month));
                }
            }
        }

        private void toolDescalcular_Click(object sender, EventArgs e)
        {
            workDescalculo.RunWorkerAsync();
        }

        private void dgvEmpleados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = 0;
            fila = dgvEmpleados.CurrentCell.RowIndex;
            frmReciboNomina rn = new frmReciboNomina();
            rn._idEmpleado = int.Parse(dgvEmpleados.Rows[fila].Cells[1].Value.ToString());
            rn._inicioPeriodo = dtpPeriodoInicio.Value;
            rn._finPeriodo = dtpPeriodoFin.Value;
            rn.Show();
        }

        private void workerCalculo_DoWork(object sender, DoWorkEventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            string listaIds = "";

            #region LISTAS
            List<CalculoNomina.Core.Nomina> lstDatosNomina;
            List<tmpPagoNomina> lstValoresNomina;
            List<CalculoNomina.Core.Nomina> lstDatoTrabajador;
            #endregion

            #region DATOS DE LA NOMINA
            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                if ((bool)fila.Cells["seleccion"].Value && double.Parse(fila.Cells["sueldo"].Value.ToString()) == 0)
                {
                    listaIds += fila.Cells["idtrabajador"].Value + ",";
                }
            }

            if (listaIds.Equals("")) return;

            listaIds.Substring(listaIds.Length - 1, 1);

            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            lstDatosNomina = new List<CalculoNomina.Core.Nomina>();
            try
            {
                cnx.Open();
                lstDatosNomina = nh.obtenerDatosNomina(GLOBALES.IDEMPRESA, GLOBALES.ACTIVO, listaIds);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
            #endregion

            #region CALCULO
            lstValoresNomina = new List<tmpPagoNomina>();
            int contadorNomina = lstDatosNomina.Count;
            int progreso = 0;
            for (int i = 0; i < lstDatosNomina.Count; i++)
            {
                progreso = (i * 100) / contadorNomina;

                tmpPagoNomina vn = new tmpPagoNomina();
                vn.idtrabajador = lstDatosNomina[i].idtrabajador;
                vn.idempresa = GLOBALES.IDEMPRESA;
                vn.idconcepto = lstDatosNomina[i].id;
                vn.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                vn.fechainicio = dtpPeriodoInicio.Value;
                vn.fechafin = dtpPeriodoFin.Value;
                vn.guardada = false;

                lstDatoTrabajador = new List<CalculoNomina.Core.Nomina>();
                CalculoNomina.Core.Nomina nt = new CalculoNomina.Core.Nomina();
                nt.idtrabajador = lstDatosNomina[i].idtrabajador;
                nt.dias = lstDatosNomina[i].dias;
                nt.salariominimo = lstDatosNomina[i].salariominimo;
                nt.antiguedadmod = lstDatosNomina[i].antiguedadmod;
                nt.sdi = lstDatosNomina[i].sdi;
                nt.sd = lstDatosNomina[i].sd;
                nt.id = lstDatosNomina[i].id;
                nt.noconcepto = lstDatosNomina[i].noconcepto;
                nt.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                nt.formula = lstDatosNomina[i].formula;
                nt.formulaexento = lstDatosNomina[i].formulaexento;
                lstDatoTrabajador.Add(nt);

                FormulasValores f = new FormulasValores(lstDatoTrabajador, dtpPeriodoInicio.Value, dtpPeriodoFin.Value);
                vn.cantidad = double.Parse(f.calcularFormula().ToString());
                vn.exento = double.Parse(f.calcularFormulaExento().ToString());
                vn.gravado = double.Parse(f.calcularFormula().ToString()) - double.Parse(f.calcularFormulaExento().ToString());

                lstValoresNomina.Add(vn);
                workerCalculo.ReportProgress(progreso, "Calculo de Nómina");
            }
            workerCalculo.ReportProgress(100, "Calculo de Nómina");
            #endregion

            #region VERIFICACION DE PRIMA VACACIONAL Y VACACIONES
            vh = new Vacaciones.Core.VacacionesHelper();
            vh.Command = cmd;
            int contadorGrid = dgvEmpleados.Rows.Count;
            int contador = 0;
            progreso = 0;
            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                progreso = (contador * 100) / contadorGrid;
                workerCalculo.ReportProgress(progreso, "Verificación de Prima Vacacional y Vacaciones");
                contador++;

                if ((bool)fila.Cells["seleccion"].Value)
                {
                    Vacaciones.Core.Vacaciones vacacion = new Vacaciones.Core.Vacaciones();
                    vacacion.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                    vacacion.inicio = dtpPeriodoInicio.Value;
                    vacacion.fin = dtpPeriodoFin.Value;
                    List<Vacaciones.Core.Vacaciones> lstPrima = new List<Vacaciones.Core.Vacaciones>();
                    double vacacionesPagadas = 0;
                    try
                    {
                        cnx.Open();
                        lstPrima = vh.primaVacacional(vacacion);
                        vacacionesPagadas = double.Parse(vh.vacacionesPagadas(vacacion).ToString());
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                        cnx.Dispose();
                    }

                    if (lstPrima[0].pv != 0)
                    {
                        ch = new Conceptos.Core.ConceptosHelper();
                        ch.Command = cmd;
                        Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                        concepto.noconcepto = 4; //PRIMA VACACIONAL
                        concepto.idempresa = GLOBALES.IDEMPRESA;
                        List<Conceptos.Core.Conceptos> lstConcepto = new List<Conceptos.Core.Conceptos>();
                        try
                        {
                            cnx.Open();
                            lstConcepto = ch.obtenerConceptoNomina(concepto);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                        }

                        tmpPagoNomina vn = new tmpPagoNomina();
                        vn.idtrabajador = (int)fila.Cells["idtrabajador"].Value;
                        vn.idempresa = GLOBALES.IDEMPRESA;
                        vn.idconcepto = lstConcepto[0].id;
                        vn.tipoconcepto = lstConcepto[0].tipoconcepto;
                        vn.fechainicio = dtpPeriodoInicio.Value;
                        vn.fechafin = dtpPeriodoFin.Value;
                        vn.exento = lstPrima[0].pexenta;
                        vn.gravado = lstPrima[0].pgravada;
                        vn.cantidad = lstPrima[0].pv;
                        vn.guardada = false;
                        lstValoresNomina.Add(vn);
                    }

                    if (vacacionesPagadas != 0)
                    {
                        ch = new Conceptos.Core.ConceptosHelper();
                        ch.Command = cmd;
                        Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                        concepto.noconcepto = 7; //VACACIONES
                        concepto.idempresa = GLOBALES.IDEMPRESA;
                        List<Conceptos.Core.Conceptos> lstConcepto = new List<Conceptos.Core.Conceptos>();
                        try
                        {
                            cnx.Open();
                            lstConcepto = ch.obtenerConceptoNomina(concepto);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                        }

                        tmpPagoNomina vn = new tmpPagoNomina();
                        vn.idtrabajador = (int)fila.Cells["idtrabajador"].Value;
                        vn.idempresa = GLOBALES.IDEMPRESA;
                        vn.idconcepto = lstConcepto[0].id;
                        vn.tipoconcepto = lstConcepto[0].tipoconcepto;
                        vn.fechainicio = dtpPeriodoInicio.Value;
                        vn.fechafin = dtpPeriodoFin.Value;
                        vn.exento = 0;
                        vn.gravado = vacacionesPagadas;
                        vn.cantidad = vacacionesPagadas;
                        vn.guardada = false;
                        lstValoresNomina.Add(vn);
                    }
                }
            }
            workerCalculo.ReportProgress(100, "Verificación de Prima Vacacional y Vacaciones");
            #endregion

            #region BULK DATA
            DataTable dt = new DataTable();
            DataRow dtFila;
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("idtrabajador", typeof(Int32));
            dt.Columns.Add("idempresa", typeof(Int32));
            dt.Columns.Add("idconcepto", typeof(Int32));
            dt.Columns.Add("tipoconcepto", typeof(String));
            dt.Columns.Add("exento", typeof(Double));
            dt.Columns.Add("gravado", typeof(Double));
            dt.Columns.Add("cantidad", typeof(Double));
            dt.Columns.Add("fechainicio", typeof(DateTime));
            dt.Columns.Add("fechafin", typeof(DateTime));
            dt.Columns.Add("guardada", typeof(Boolean));

            contadorNomina = lstValoresNomina.Count;
            for (int i = 0; i < lstValoresNomina.Count; i++)
            {
                progreso = (i * 100) / contadorNomina;
                workerCalculo.ReportProgress(progreso, "BulkData");
                dtFila = dt.NewRow();
                dtFila["id"] = i + 1;
                dtFila["idtrabajador"] = lstValoresNomina[i].idtrabajador;
                dtFila["idempresa"] = lstValoresNomina[i].idempresa;
                dtFila["idconcepto"] = lstValoresNomina[i].idconcepto;
                dtFila["tipoconcepto"] = lstValoresNomina[i].tipoconcepto;
                dtFila["exento"] = lstValoresNomina[i].exento;
                dtFila["gravado"] = lstValoresNomina[i].gravado;
                dtFila["cantidad"] = lstValoresNomina[i].cantidad;
                dtFila["fechainicio"] = lstValoresNomina[i].fechainicio;
                dtFila["fechafin"] = lstValoresNomina[i].fechafin;
                dtFila["guardada"] = lstValoresNomina[i].guardada;
                dt.Rows.Add(dtFila);
            }
            workerCalculo.ReportProgress(100, "BulkData");
            bulk = new SqlBulkCopy(cnx);
            nh.bulkCommand = bulk;

            try
            {
                cnx.Open();
                nh.bulkNomina(dt, "tmpPagoNomina");
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message + "\r\n \r\n Error Bulk Nomina.", "Error");
            }
            #endregion

            #region MOSTRAR DATOS EN EL GRID
            contadorGrid = dgvEmpleados.Rows.Count;
            contador = 0;
            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                progreso = (contador * 100) / contadorGrid;
                workerCalculo.ReportProgress(progreso, "Mostrar datos");
                contador++;
                if ((bool)fila.Cells["seleccion"].Value)
                {
                    for (int i = 0; i < lstValoresNomina.Count; i++)
                    {
                        if ((int)fila.Cells["idtrabajador"].Value == lstValoresNomina[i].idtrabajador)
                        {
                            switch (lstValoresNomina[i].idconcepto)
                            {
                                case 1:
                                    fila.Cells["sueldo"].Value = lstValoresNomina[i].cantidad;
                                    break;
                                case 2:
                                    fila.Cells["horas"].Value = lstValoresNomina[i].cantidad;
                                    break;
                                case 3:
                                    fila.Cells["asistencia"].Value = lstValoresNomina[i].cantidad;
                                    break;
                                case 5:
                                    fila.Cells["puntualidad"].Value = lstValoresNomina[i].cantidad;
                                    break;
                                case 6:
                                    fila.Cells["despensa"].Value = lstValoresNomina[i].cantidad;
                                    break;
                            }
                        }
                    }
                }
            }
            workerCalculo.ReportProgress(100, "Mostrar datos");
            #endregion
        }

        private void workerCalculo_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolPorcentaje.Text = e.ProgressPercentage.ToString() + "%";
            toolEtapa.Text = e.UserState.ToString();
        }

        private void workerCalculo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolPorcentaje.Text = "Completado.";
        }

        private void workDescalculo_DoWork(object sender, DoWorkEventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = dtpPeriodoInicio.Value;
            pn.fechafin = dtpPeriodoFin.Value;

            int contadorGrid = dgvEmpleados.Rows.Count;
            int contador = 0;
            int progreso = 0;
            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                progreso = (contador * 100) / contadorGrid;
                workDescalculo.ReportProgress(progreso, "Descalculo");
                contador++;
                if ((bool)fila.Cells["seleccion"].Value)
                {
                    pn.idtrabajador = (int)fila.Cells["idtrabajador"].Value;
                    try
                    {
                        cnx.Open();
                        nh.eliminaCalculo(pn);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n" + error.Message + "\r\n \r\n Error en descalculo: " + fila.Cells["noempleado"].Value.ToString(), "Error");
                    }
                    fila.Cells["sueldo"].Value = 0;
                    fila.Cells["despensa"].Value = 0;
                    fila.Cells["asistencia"].Value = 0;
                    fila.Cells["puntualidad"].Value = 0;
                    fila.Cells["horas"].Value = 0;
                }
            }
            cnx.Dispose();
            workDescalculo.ReportProgress(100, "Descalculo");
        }

        private void workDescalculo_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolPorcentaje.Text = e.ProgressPercentage.ToString() + "%";
            toolEtapa.Text = e.UserState.ToString();
        }

        private void workDescalculo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolPorcentaje.Text = "Completado.";
        }

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = dtpPeriodoInicio.Value;
            pn.fechafin = dtpPeriodoFin.Value;
            pn.guardada = true;

            try
            {
                cnx.Open();
                nh.actualizaPreNomina(pn);
                cnx.Close();
                cnx.Dispose();

                MessageBox.Show("PreNomina Guardada.","Confirmación");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
        }

        private void toolAbrir_Click(object sender, EventArgs e)
        {
            frmSeleccionarPreNomina spn = new frmSeleccionarPreNomina();
            spn.OnPreNomina += spn_OnPreNomina;
            spn.Show();
        }

        void spn_OnPreNomina(DateTime inicio, DateTime fin)
        {
            cargaEmpleados();
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = inicio;
            pn.fechafin = fin;

            List<CalculoNomina.Core.tmpPagoNomina> lstPreNomina = new List<CalculoNomina.Core.tmpPagoNomina>();
            try
            {
                cnx.Open();
                lstPreNomina = nh.obtenerPreNomina(pn);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                for (int i = 0; i < lstPreNomina.Count; i++)
                {
                    if ((int)fila.Cells["idtrabajador"].Value == lstPreNomina[i].idtrabajador)
                    {
                        switch (lstPreNomina[i].idconcepto)
                        {
                            case 1:
                                fila.Cells["sueldo"].Value = lstPreNomina[i].cantidad;
                                break;
                            case 2:
                                fila.Cells["horas"].Value = lstPreNomina[i].cantidad;
                                break;
                            case 3:
                                fila.Cells["asistencia"].Value = lstPreNomina[i].cantidad;
                                break;
                            case 5:
                                fila.Cells["puntualidad"].Value = lstPreNomina[i].cantidad;
                                break;
                            case 6:
                                fila.Cells["despensa"].Value = lstPreNomina[i].cantidad;
                                break;
                        }
                    }
                }
            }
        }

        private void cargaEmpleados()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            #region DISEÑO DEL GRIDVIEW

            dgvEmpleados.Columns["seleccion"].DataPropertyName = "chk";
            dgvEmpleados.Columns["idtrabajador"].DataPropertyName = "idtrabajador";
            dgvEmpleados.Columns["iddepartamento"].DataPropertyName = "iddepartamento";
            dgvEmpleados.Columns["idpuesto"].DataPropertyName = "idpuesto";
            dgvEmpleados.Columns["noempleado"].DataPropertyName = "noempleado";
            dgvEmpleados.Columns["nombres"].DataPropertyName = "nombres";
            dgvEmpleados.Columns["paterno"].DataPropertyName = "paterno";
            dgvEmpleados.Columns["materno"].DataPropertyName = "materno";
            dgvEmpleados.Columns["sueldo"].DataPropertyName = "sueldo";
            dgvEmpleados.Columns["despensa"].DataPropertyName = "despensa";
            dgvEmpleados.Columns["asistencia"].DataPropertyName = "asistencia";
            dgvEmpleados.Columns["puntualidad"].DataPropertyName = "puntualidad";
            dgvEmpleados.Columns["horas"].DataPropertyName = "horas";

            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvEmpleados.Columns[8].DefaultCellStyle = estilo;
            dgvEmpleados.Columns[9].DefaultCellStyle = estilo;
            dgvEmpleados.Columns[10].DefaultCellStyle = estilo;
            dgvEmpleados.Columns[11].DefaultCellStyle = estilo;
            dgvEmpleados.Columns[12].DefaultCellStyle = estilo;

            dgvEmpleados.Columns["idtrabajador"].Visible = false;
            dgvEmpleados.Columns["noempleado"].ReadOnly = true;
            dgvEmpleados.Columns["nombres"].ReadOnly = true;
            dgvEmpleados.Columns["paterno"].ReadOnly = true;
            dgvEmpleados.Columns["materno"].ReadOnly = true;
            #endregion

            #region LISTADO DE EMPLEADOS GRID
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            lstEmpleadosNomina = new List<CalculoNomina.Core.DatosEmpleado>();

            try
            {
                cnx.Open();
                lstEmpleadosNomina = nh.obtenerDatosEmpleado(GLOBALES.IDEMPRESA, GLOBALES.ACTIVO);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            dgvEmpleados.DataSource = lstEmpleadosNomina;

            for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
            {
                dgvEmpleados.AutoResizeColumn(i);
            }
            #endregion
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            eliminarPreNomina();
        }

        private void frmListaCalculoNomina_FormClosing(object sender, FormClosingEventArgs e)
        {
            eliminarPreNomina();
        }

        private void eliminarPreNomina()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = dtpPeriodoInicio.Value;
            pn.fechafin = dtpPeriodoFin.Value;
            pn.guardada = false;

            try
            {
                cnx.Open();
                nh.eliminaPreNomina(pn);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
            this.Dispose();
        }

        private void toolAutorizar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Quiere autorizar el periodo?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                nh = new CalculoNomina.Core.NominaHelper();
                nh.Command = cmd;

                try
                {
                    cnx.Open();
                    nh.stpAutorizaNomina(GLOBALES.IDEMPRESA, dtpPeriodoInicio.Value, dtpPeriodoFin.Value, GLOBALES.IDUSUARIO);
                    cnx.Close();
                    cnx.Dispose();
                    MessageBox.Show("Nomina autorizada.", "Confirmación");
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }
            }
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
                for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
                {
                    if (txtBuscar.Text.Trim() == dgvEmpleados.Rows[i].Cells["noempleado"].Value.ToString())
                        dgvEmpleados.Rows[i].Selected = true;
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar no. empleado...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void toolReportePreNomina_Click(object sender, EventArgs e)
        {
            frmVisorReportes vr = new frmVisorReportes();
            vr._noReporte = 0;
            vr._inicioPeriodo = dtpPeriodoInicio.Value;
            vr._finPeriodo = dtpPeriodoFin.Value;
            vr.Show();
        }
    }

    public class tmpPagoNomina
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public int idconcepto { get; set; }
        public string tipoconcepto { get; set; }
        public double exento { get; set; }
        public double gravado { get; set; }
        public double cantidad { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public bool guardada { get; set; }
    }
}