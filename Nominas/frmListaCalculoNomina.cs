using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
        string NetoCero, Orden;
        bool FLAGCARGA = false;
        bool FLAGPRIMERCALCULO = true;
        Empresas.Core.EmpresasHelper eh;
        Empleados.Core.EmpleadosHelper emph;
        Conceptos.Core.ConceptosHelper ch;
        Vacaciones.Core.VacacionesHelper vh;
        CalculoNomina.Core.NominaHelper nh;
        Faltas.Core.FaltasHelper fh;
        Incapacidad.Core.IncapacidadHelper ih;
        ProgramacionConcepto.Core.ProgramacionHelper pch;
        Movimientos.Core.MovimientosHelper mh;
        List<CalculoNomina.Core.tmpPagoNomina> lstValoresNomina;
        List<CalculoNomina.Core.DatosEmpleado> lstEmpleadosNomina;
        List<CalculoNomina.Core.DatosFaltaIncapacidad> lstEmpleadosFaltaIncapacidad;
        CheckBox chk;
        DataTable dt;
        #endregion

        #region VARIABLES PUBLICAS
        public int _periodo;
        public int _tipoNomina;
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

                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                try
                {

                    using (OleDbConnection con = new OleDbConnection(conStr))
                    {
                        using (OleDbCommand cmdO = new OleDbCommand())
                        {
                            cmdO.Connection = con;
                            con.Open();
                            DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            sheetName = dtExcelSchema.Rows[5]["TABLE_NAME"].ToString();
                            con.Close();
                        }
                    }

                    using (OleDbConnection con = new OleDbConnection(conStr))
                    {
                        using (OleDbCommand cmdO = new OleDbCommand())
                        {
                            using (OleDbDataAdapter oda = new OleDbDataAdapter())
                            {
                                dt = new DataTable();
                                cmdO.CommandText = "SELECT * From [" + sheetName + "]";
                                cmdO.Connection = con;
                                con.Open();
                                oda.SelectCommand = cmdO;
                                oda.Fill(dt);
                                con.Close();
                            }
                        }
                    }
                    workHoras.RunWorkerAsync();
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
            toolPrenomina.Enabled = false;
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
            dgvEmpleados.Columns["seleccion"].Visible = false;
            chk.Visible = false;
            #endregion

            NominaActual();

            if (_tipoNomina == GLOBALES.NORMAL)
                CargaPerfil("Normal");
            if (_tipoNomina == GLOBALES.ESPECIAL)
                CargaPerfil("Especial");
        }

        private void CargaPerfil(string nombre)
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES(nombre);

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Prenomina":
                        toolPrenomina.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Calcular": toolCalcular.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Cargar Faltas": toolStripButton1.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Autorizar": toolAutorizar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Reportes": toolReportes.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;

                }
            }
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
            fn._tipoNomina = _tipoNomina;
            fn.OnFiltro += fn_OnFiltro;
            fn.ShowDialog();
        }

        void fn_OnFiltro(int filtro, int de, int hasta)
        {
            switch (filtro)
            { 
                case 0:
                    var empleadoDepto = from f in lstEmpleadosNomina where f.iddepartamento >= de && f.iddepartamento <= hasta orderby f.noempleado ascending select f;
                    dgvEmpleados.DataSource = empleadoDepto.ToList();
                    for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
                    {
                        dgvEmpleados.AutoResizeColumn(i);
                    }

                    var empleadoDeptoFalta = from f in lstEmpleadosFaltaIncapacidad where f.iddepartamento >= de && f.iddepartamento <= hasta orderby f.noempleado ascending select f;
                    dgvFaltas.DataSource = empleadoDeptoFalta.ToList();
                    for (int i = 1; i < dgvFaltas.Columns.Count; i++)
                    {
                        dgvFaltas.AutoResizeColumn(i);
                    }
                    
                    break;
                case 1:
                    var empleadoPuesto = from f in lstEmpleadosNomina where f.idpuesto >= de && f.idpuesto <= hasta orderby f.noempleado ascending select f;
                    dgvEmpleados.DataSource = empleadoPuesto.ToList();
                    for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
                    {
                        dgvEmpleados.AutoResizeColumn(i);
                    }

                    var empleadoPuestoFalta = from f in lstEmpleadosFaltaIncapacidad where f.idpuesto >= de && f.idpuesto <= hasta orderby f.noempleado ascending select f;
                    dgvFaltas.DataSource = empleadoPuestoFalta.ToList();
                    for (int i = 1; i < dgvFaltas.Columns.Count; i++)
                    {
                        dgvFaltas.AutoResizeColumn(i);
                    }
                    
                    break;
                case 2:
                    var empleadoNoEmpleado = from f in lstEmpleadosNomina where f.idtrabajador >= de && f.idtrabajador <= hasta orderby f.noempleado ascending select f;
                    dgvEmpleados.DataSource = empleadoNoEmpleado.ToList();
                    for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
                    {
                        dgvEmpleados.AutoResizeColumn(i);
                    }

                    var empleadoNoEmpleadoFalta = from f in lstEmpleadosFaltaIncapacidad where f.idtrabajador >= de && f.idtrabajador <= hasta orderby f.noempleado ascending select f;
                    dgvFaltas.DataSource = empleadoNoEmpleadoFalta.ToList();
                    for (int i = 1; i < dgvFaltas.Columns.Count; i++)
                    {
                        dgvFaltas.AutoResizeColumn(i);
                    }
                   
                    break;
            }

            
        }

        private void toolPuesto_Click(object sender, EventArgs e)
        {
            frmFiltroNomina fn = new frmFiltroNomina();
            fn._filtro = 1;
            fn._tipoNomina = _tipoNomina;
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

            dgvFaltas.DataSource = lstEmpleadosFaltaIncapacidad;
            for (int i = 1; i < dgvFaltas.Columns.Count; i++)
            {
                dgvFaltas.AutoResizeColumn(i);
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
                existe = (int)nh.existeNomina(GLOBALES.IDEMPRESA, dtpPeriodoInicio.Value.Date, dtpPeriodoFin.Value.Date);
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
                DateTime dt = dtpPeriodoInicio.Value.Date;
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
            rn._inicioPeriodo = dtpPeriodoInicio.Value.Date;
            rn._finPeriodo = dtpPeriodoFin.Value.Date;
            rn.Show();
        }

        private void workerCalculo_DoWork(object sender, DoWorkEventArgs e)
        {
            //bool activoInfonavit = false;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            //string listaIds = "";

            #region LISTAS
            List<CalculoNomina.Core.Nomina> lstDatosNomina;
            //List<CalculoNomina.Core.Nomina> lstDatoTrabajador;
            List<CalculoNomina.Core.NominaRecalculo> _lstDatosNomina;
            //List<CalculoNomina.Core.NominaRecalculo> _lstDatoTrabajador;
            #endregion

            if (FLAGPRIMERCALCULO)
            {
                #region DATOS DE LA NOMINA (PERCEPCIONES)
                nh = new CalculoNomina.Core.NominaHelper();
                nh.Command = cmd;
                lstDatosNomina = new List<CalculoNomina.Core.Nomina>();
                try
                {
                    cnx.Open();
                    if (_tipoNomina == GLOBALES.NORMAL)
                        lstDatosNomina = nh.obtenerDatosNomina(GLOBALES.IDEMPRESA, GLOBALES.ACTIVO, "P");
                    if (_tipoNomina == GLOBALES.ESPECIAL)
                        lstDatosNomina = nh.obtenerDatosNomina(GLOBALES.IDEMPRESA, GLOBALES.INACTIVO, "P");
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }
                #endregion

                #region CALCULO DE PERCEPCIONES
                workerCalculo.ReportProgress(50, "Calculo de percepciones. Espere...");
                lstValoresNomina = new List<CalculoNomina.Core.tmpPagoNomina>();
                lstValoresNomina = CALCULO.PERCEPCIONES(lstDatosNomina, 
                                    dtpPeriodoInicio.Value.Date, 
                                    dtpPeriodoFin.Value.Date, 
                                    _tipoNomina);
                workerCalculo.ReportProgress(100, "Calculo de percepciones. Terminado.");
                #endregion

                #region MOSTRAR DATOS EN EL GRID
                int contadorNomina = lstValoresNomina.Count;
                int progreso = 0;
                int contadorGrid = dgvEmpleados.Rows.Count;
                int contador = 0;
                foreach (DataGridViewRow fila in dgvEmpleados.Rows)
                {
                    progreso = (contador * 100) / contadorGrid;
                    workerCalculo.ReportProgress(progreso, "Mostrar datos");
                    contador++;
                    for (int i = 0; i < lstValoresNomina.Count; i++)
                    {
                        if ((int)fila.Cells["idtrabajador"].Value == lstValoresNomina[i].idtrabajador)
                        {
                            switch (lstValoresNomina[i].noconcepto)
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
                workerCalculo.ReportProgress(100, "Mostrar datos");
                #endregion

                #region VERIFICACION DE PRIMA VACACIONAL Y VACACIONES
                vh = new Vacaciones.Core.VacacionesHelper();
                vh.Command = cmd;
                contadorGrid = dgvEmpleados.Rows.Count;
                contador = 0;
                progreso = 0;
                foreach (DataGridViewRow fila in dgvEmpleados.Rows)
                {
                    progreso = (contador * 100) / contadorGrid;
                    workerCalculo.ReportProgress(progreso, "Verificación de Prima Vacacional y Vacaciones");
                    contador++;

                    if (double.Parse(fila.Cells["sueldo"].Value.ToString()) != 0)
                    {
                        Vacaciones.Core.Vacaciones vacacion = new Vacaciones.Core.Vacaciones();
                        vacacion.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                        vacacion.inicio = dtpPeriodoInicio.Value.Date;
                        vacacion.fin = dtpPeriodoFin.Value.Date;
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

                            CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                            vn.idtrabajador = (int)fila.Cells["idtrabajador"].Value;
                            vn.idempresa = GLOBALES.IDEMPRESA;
                            vn.idconcepto = lstConcepto[0].id;
                            vn.noconcepto = 4;
                            vn.tipoconcepto = lstConcepto[0].tipoconcepto;
                            vn.fechainicio = dtpPeriodoInicio.Value.Date;
                            vn.fechafin = dtpPeriodoFin.Value.Date;
                            vn.exento = lstPrima[0].pexenta;
                            vn.gravado = lstPrima[0].pgravada;
                            vn.cantidad = lstPrima[0].pv;
                            vn.guardada = false;
                            vn.tiponomina = _tipoNomina;
                            vn.modificado = false;
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

                            CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                            vn.idtrabajador = (int)fila.Cells["idtrabajador"].Value;
                            vn.idempresa = GLOBALES.IDEMPRESA;
                            vn.idconcepto = lstConcepto[0].id;
                            vn.noconcepto = 7;
                            vn.tipoconcepto = lstConcepto[0].tipoconcepto;
                            vn.fechainicio = dtpPeriodoInicio.Value.Date;
                            vn.fechafin = dtpPeriodoFin.Value.Date;
                            vn.exento = 0;
                            vn.gravado = vacacionesPagadas;
                            vn.cantidad = vacacionesPagadas;
                            vn.guardada = false;
                            vn.tiponomina = _tipoNomina;
                            vn.modificado = false;
                            lstValoresNomina.Add(vn);
                        }
                    }
                }
                workerCalculo.ReportProgress(100, "Verificación de Prima Vacacional y Vacaciones");
                #endregion

                BulkData(lstValoresNomina);

                #region DATOS DE LA NOMINA (DEDUCCIONES)
                nh = new CalculoNomina.Core.NominaHelper();
                nh.Command = cmd;
                lstDatosNomina = new List<CalculoNomina.Core.Nomina>();
                try
                {
                    cnx.Open();
                    if (_tipoNomina == GLOBALES.NORMAL)
                        lstDatosNomina = nh.obtenerDatosNomina(GLOBALES.IDEMPRESA, GLOBALES.ACTIVO, "D");
                    if (_tipoNomina == GLOBALES.ESPECIAL)
                        lstDatosNomina = nh.obtenerDatosNomina(GLOBALES.IDEMPRESA, GLOBALES.INACTIVO, "D");
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }
                #endregion

                #region CALCULO DE DEDUCCIONES
                workerCalculo.ReportProgress(50, "Calculo de deducciones. Espere...");
                List<CalculoNomina.Core.tmpPagoNomina> lstDeducciones = new List<CalculoNomina.Core.tmpPagoNomina>();
                lstDeducciones = CALCULO.ISR_SUBSIDIO(lstDatosNomina, lstValoresNomina,
                                    dtpPeriodoInicio.Value.Date,
                                    dtpPeriodoFin.Value.Date,
                                    _tipoNomina);
                workerCalculo.ReportProgress(100, "Calculo de deducciones. Terminado.");
                #endregion

                BulkData(lstDeducciones);

                #region PROGRAMACION DE MOVIMIENTOS
                List<CalculoNomina.Core.tmpPagoNomina> lstOtrasDeducciones = new List<CalculoNomina.Core.tmpPagoNomina>();
                pch = new ProgramacionConcepto.Core.ProgramacionHelper();
                pch.Command = cmd;
                contadorGrid = dgvEmpleados.Rows.Count;
                contador = 0;
                progreso = 0;
                foreach (DataGridViewRow fila in dgvEmpleados.Rows)
                {
                    progreso = (contador * 100) / contadorGrid;
                    workerCalculo.ReportProgress(progreso, "Otras Deducciones");
                    contador++;

                    if (double.Parse(fila.Cells["sueldo"].Value.ToString()) != 0)
                    {
                        int existe = 0;
                        ProgramacionConcepto.Core.ProgramacionConcepto programacion = new ProgramacionConcepto.Core.ProgramacionConcepto();
                        programacion.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());

                        List<ProgramacionConcepto.Core.ProgramacionConcepto> lstProgramacion = new List<ProgramacionConcepto.Core.ProgramacionConcepto>();

                        try
                        {
                            cnx.Open();
                            existe = (int)pch.existeProgramacion(programacion);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                            cnx.Dispose();
                        }

                        if (existe != 0)
                        {
                            try
                            {
                                cnx.Open();
                                lstProgramacion = pch.obtenerProgramacion(programacion);
                                cnx.Close();
                            }
                            catch (Exception error)
                            {
                                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                                cnx.Dispose();
                            }

                            for (int i = 0; i < lstProgramacion.Count; i++)
                            {
                                if (dtpPeriodoFin.Value <= lstProgramacion[i].fechafin)
                                {
                                    ch = new Conceptos.Core.ConceptosHelper();
                                    ch.Command = cmd;
                                    Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                                    concepto.id = lstProgramacion[i].idconcepto;
                                    List<Conceptos.Core.Conceptos> lstNoConcepto = new List<Conceptos.Core.Conceptos>();
                                    try
                                    {
                                        cnx.Open();
                                        lstNoConcepto = ch.obtenerConcepto(concepto);
                                        cnx.Close();
                                    }
                                    catch (Exception error) { MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error"); }

                                    CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                                    vn.idtrabajador = (int)fila.Cells["idtrabajador"].Value;
                                    vn.idempresa = GLOBALES.IDEMPRESA;
                                    vn.idconcepto = lstProgramacion[i].idconcepto;
                                    vn.noconcepto = lstNoConcepto[0].noconcepto;
                                    vn.tipoconcepto = lstNoConcepto[0].tipoconcepto;
                                    vn.fechainicio = dtpPeriodoInicio.Value.Date;
                                    vn.fechafin = dtpPeriodoFin.Value.Date;
                                    vn.exento = 0;
                                    vn.gravado = 0;
                                    vn.cantidad = lstProgramacion[i].cantidad;
                                    vn.guardada = false;
                                    vn.tiponomina = _tipoNomina;
                                    vn.modificado = false;
                                    lstOtrasDeducciones.Add(vn);
                                }
                            }
                        }
                    }
                }
                workerCalculo.ReportProgress(100, "Otras Deducciones");
                #endregion

                #region MOVIMIENTOS
                mh = new Movimientos.Core.MovimientosHelper();
                mh.Command = cmd;

                contadorGrid = dgvEmpleados.Rows.Count;
                contador = 0;
                progreso = 0;
                foreach (DataGridViewRow fila in dgvEmpleados.Rows)
                {
                    progreso = (contador * 100) / contadorGrid;
                    workerCalculo.ReportProgress(progreso, "Movimientos");
                    contador++;

                    if (double.Parse(fila.Cells["sueldo"].Value.ToString()) != 0)
                    {
                        int existe = 0;
                        Movimientos.Core.Movimientos mov = new Movimientos.Core.Movimientos();
                        mov.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                        mov.fechainicio = dtpPeriodoInicio.Value.Date;
                        mov.fechafin = dtpPeriodoFin.Value.Date;

                        List<Movimientos.Core.Movimientos> lstMovimiento = new List<Movimientos.Core.Movimientos>();

                        try
                        {
                            cnx.Open();
                            existe = (int)mh.existeMovimiento(mov);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                            cnx.Dispose();
                        }

                        if (existe != 0)
                        {
                            try
                            {
                                cnx.Open();
                                lstMovimiento = mh.obtenerMovimiento(mov);
                                cnx.Close();
                            }
                            catch (Exception error)
                            {
                                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                                cnx.Dispose();
                            }

                            for (int i = 0; i < lstMovimiento.Count; i++)
                            {
                                ch = new Conceptos.Core.ConceptosHelper();
                                ch.Command = cmd;
                                Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                                concepto.id = lstMovimiento[i].idconcepto;
                                List<Conceptos.Core.Conceptos> lstNoConcepto = new List<Conceptos.Core.Conceptos>();
                                try
                                {
                                    cnx.Open();
                                    lstNoConcepto = ch.obtenerConcepto(concepto);
                                    cnx.Close();
                                }
                                catch (Exception error) { MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error"); }

                                CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                                vn.idtrabajador = (int)fila.Cells["idtrabajador"].Value;
                                vn.idempresa = GLOBALES.IDEMPRESA;
                                vn.idconcepto = lstMovimiento[i].idconcepto;
                                vn.noconcepto = lstNoConcepto[0].noconcepto;
                                vn.tipoconcepto = lstNoConcepto[0].tipoconcepto;
                                vn.fechainicio = dtpPeriodoInicio.Value.Date;
                                vn.fechafin = dtpPeriodoFin.Value.Date;
                                vn.exento = 0;
                                vn.gravado = 0;
                                vn.cantidad = lstMovimiento[i].cantidad;
                                vn.guardada = false;
                                vn.tiponomina = _tipoNomina;
                                vn.modificado = false;
                                lstOtrasDeducciones.Add(vn);
                            }
                        }
                    }
                }
                workerCalculo.ReportProgress(100, "Otras Deducciones");
                #endregion

                #region CALCULO DE ISR RETENIDO Y SUBSIDIO PAGADO

                ch = new Conceptos.Core.ConceptosHelper();
                ch.Command = cmd;
                Conceptos.Core.Conceptos conceptoSubsidio = new Conceptos.Core.Conceptos();
                Conceptos.Core.Conceptos conceptoIsr = new Conceptos.Core.Conceptos();
                Conceptos.Core.Conceptos sub = new Conceptos.Core.Conceptos();
                Conceptos.Core.Conceptos isr = new Conceptos.Core.Conceptos();

                conceptoSubsidio.noconcepto = 16; //SUBSIDIO AL EMPLEO
                conceptoSubsidio.idempresa = GLOBALES.IDEMPRESA;

                conceptoIsr.noconcepto = 17; //ISR
                conceptoIsr.idempresa = GLOBALES.IDEMPRESA;

                sub.noconcepto = 15;
                sub.idempresa = GLOBALES.IDEMPRESA;

                isr.noconcepto = 8;
                isr.idempresa = GLOBALES.IDEMPRESA;

                List<Conceptos.Core.Conceptos> lstConceptoSubsidio = new List<Conceptos.Core.Conceptos>();
                List<Conceptos.Core.Conceptos> lstConceptoIsr = new List<Conceptos.Core.Conceptos>();

                List<Conceptos.Core.Conceptos> lstSub = new List<Conceptos.Core.Conceptos>();
                List<Conceptos.Core.Conceptos> lstIsr = new List<Conceptos.Core.Conceptos>();
                try
                {
                    cnx.Open();
                    lstConceptoSubsidio = ch.obtenerConceptoNomina(conceptoSubsidio);
                    lstConceptoIsr = ch.obtenerConceptoNomina(conceptoIsr);
                    lstSub = ch.obtenerConceptoNomina(sub);
                    lstIsr = ch.obtenerConceptoNomina(isr);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }

                contadorNomina = lstDeducciones.Count;
                progreso = 0;
                double subsidio = 0, isrr = 0, ispt = 0;
                //bool FLAGSUBSIDIO = false, FLAGISR = false;
                for (int i = 0; i < lstDeducciones.Count; i++)
                {
                    progreso = (i * 100) / contadorNomina;

                    if (lstDeducciones[i].noconcepto == 8)
                    {
                        isrr = lstDeducciones[i].cantidad;
                        //FLAGISR = true;
                        for (int j = 0; j < lstDeducciones.Count; j++)
                        {
                            if (lstDeducciones[j].noconcepto == 15 && lstDeducciones[i].idtrabajador == lstDeducciones[j].idtrabajador)
                            {
                                subsidio = lstDeducciones[j].cantidad;
                                ispt = ((isrr - subsidio) / 30.4) * _periodo;
                                if (ispt <= 0)
                                {
                                    CalculoNomina.Core.tmpPagoNomina pSubsidio = new CalculoNomina.Core.tmpPagoNomina();
                                    pSubsidio.idtrabajador = lstDeducciones[i].idtrabajador;
                                    pSubsidio.idempresa = GLOBALES.IDEMPRESA;
                                    pSubsidio.idconcepto = lstConceptoSubsidio[0].id;
                                    pSubsidio.noconcepto = 16;
                                    pSubsidio.tipoconcepto = lstConceptoSubsidio[0].tipoconcepto;
                                    pSubsidio.exento = 0;
                                    pSubsidio.gravado = 0;
                                    pSubsidio.cantidad = 0;
                                    pSubsidio.fechainicio = dtpPeriodoInicio.Value.Date;
                                    pSubsidio.fechafin = dtpPeriodoFin.Value.Date;
                                    pSubsidio.guardada = false;
                                    pSubsidio.tiponomina = _tipoNomina;
                                    pSubsidio.modificado = false;
                                    lstOtrasDeducciones.Add(pSubsidio);

                                    CalculoNomina.Core.tmpPagoNomina pIsr = new CalculoNomina.Core.tmpPagoNomina();
                                    pIsr.idtrabajador = lstDeducciones[i].idtrabajador;
                                    pIsr.idempresa = GLOBALES.IDEMPRESA;
                                    pIsr.idconcepto = lstConceptoIsr[0].id;
                                    pIsr.noconcepto = 17;
                                    pIsr.tipoconcepto = lstConceptoIsr[0].tipoconcepto;
                                    pIsr.exento = 0;
                                    pIsr.gravado = 0;
                                    pIsr.cantidad = 0;
                                    pIsr.fechainicio = dtpPeriodoInicio.Value.Date;
                                    pIsr.fechafin = dtpPeriodoFin.Value.Date;
                                    pIsr.guardada = false;
                                    pIsr.tiponomina = _tipoNomina;
                                    pIsr.modificado = false;
                                    lstOtrasDeducciones.Add(pIsr);

                                }
                                else
                                {
                                    CalculoNomina.Core.tmpPagoNomina pSubsidio = new CalculoNomina.Core.tmpPagoNomina();
                                    pSubsidio.idtrabajador = lstDeducciones[i].idtrabajador;
                                    pSubsidio.idempresa = GLOBALES.IDEMPRESA;
                                    pSubsidio.idconcepto = lstConceptoSubsidio[0].id;
                                    pSubsidio.noconcepto = 16;
                                    pSubsidio.tipoconcepto = lstConceptoSubsidio[0].tipoconcepto;
                                    pSubsidio.exento = 0;
                                    pSubsidio.gravado = 0;
                                    pSubsidio.cantidad = 0;
                                    pSubsidio.fechainicio = dtpPeriodoInicio.Value.Date;
                                    pSubsidio.fechafin = dtpPeriodoFin.Value.Date;
                                    pSubsidio.guardada = false;
                                    pSubsidio.tiponomina = _tipoNomina;
                                    pSubsidio.modificado = false;
                                    lstOtrasDeducciones.Add(pSubsidio);

                                    CalculoNomina.Core.tmpPagoNomina pIsr = new CalculoNomina.Core.tmpPagoNomina();
                                    pIsr.idtrabajador = lstDeducciones[i].idtrabajador;
                                    pIsr.idempresa = GLOBALES.IDEMPRESA;
                                    pIsr.idconcepto = lstConceptoIsr[0].id;
                                    pIsr.noconcepto = 17;
                                    pIsr.tipoconcepto = lstConceptoIsr[0].tipoconcepto;
                                    pIsr.exento = 0;
                                    pIsr.gravado = 0;
                                    pIsr.cantidad = ispt;
                                    pIsr.fechainicio = dtpPeriodoInicio.Value.Date;
                                    pIsr.fechafin = dtpPeriodoFin.Value.Date;
                                    pIsr.guardada = false;
                                    pIsr.tiponomina = _tipoNomina;
                                    pIsr.modificado = false;
                                    lstOtrasDeducciones.Add(pIsr);

                                }
                            }
                        }
                    }
                    workerCalculo.ReportProgress(progreso, "Subsidio al empleo e ISR a Retener.");
                }
                workerCalculo.ReportProgress(100, "Subsidio al empleo e ISR a Retener.");
                #endregion

                BulkData(lstOtrasDeducciones);

                #region NETOS NEGATIVOS
                nh = new CalculoNomina.Core.NominaHelper();
                nh.Command = cmd;
                List<CalculoNomina.Core.NetosNegativos> lstNetos = new List<CalculoNomina.Core.NetosNegativos>();
                try
                {
                    cnx.Open();
                    lstNetos = nh.obtenerNetosNegativos(GLOBALES.IDEMPRESA, dtpPeriodoInicio.Value.Date, dtpPeriodoFin.Value.Date);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Lista de Netos. \r\n \r\n" + error.Message, "Error");
                }
                List<CalculoNomina.Core.NetosNegativos> lstPercepcion = new List<CalculoNomina.Core.NetosNegativos>();
                List<CalculoNomina.Core.NetosNegativos> lstDeduccion = new List<CalculoNomina.Core.NetosNegativos>();
                lstPercepcion = lstNetos.Where(n => n.tipoconcepto == "P").ToList();
                lstDeduccion = lstNetos.Where(n => n.tipoconcepto == "D").ToList();
                decimal percepciones = 0, deducciones = 0;
                decimal total = 0;

                try
                {
                    int contadorNetosNegativos = 0;
                    string linea1 = "";
                    string noEmpleado = "", nombreCompleto = "";
                    using (StreamWriter sw = new StreamWriter(@"C:\Temp\NetosNegativos.txt"))
                    {
                        linea1 = "Periodo: " + dtpPeriodoInicio.Value.ToShortDateString() + " al " + dtpPeriodoFin.Value.ToShortDateString();
                        sw.WriteLine(linea1);
                        foreach (DataGridViewRow fila in dgvEmpleados.Rows)
                        {
                            for (int i = 0; i < lstPercepcion.Count; i++)
                            {
                                if (int.Parse(fila.Cells["idtrabajador"].Value.ToString()) == lstPercepcion[i].idtrabajador)
                                {
                                    noEmpleado = lstPercepcion[i].noempleado;
                                    nombreCompleto = lstPercepcion[i].nombrecompleto;
                                    percepciones += lstPercepcion[i].cantidad;
                                }

                            }
                            for (int i = 0; i < lstDeduccion.Count; i++)
                            {
                                if (int.Parse(fila.Cells["idtrabajador"].Value.ToString()) == lstDeduccion[i].idtrabajador)
                                {
                                    deducciones += lstDeduccion[i].cantidad;
                                }

                            }
                            total = percepciones - deducciones;
                            if (total < 0)
                            {
                                contadorNetosNegativos++;
                                linea1 = noEmpleado + ", " + nombreCompleto + ", Cantidad Neta Negativa: " + total.ToString();
                                sw.WriteLine(linea1);
                                total = 0;
                                percepciones = 0;
                                deducciones = 0;
                            }
                        }
                        sw.WriteLine("TOTAL CANTIDADES NEGATIVAS: " + contadorNetosNegativos.ToString());
                    }
                    if (contadorNetosNegativos != 0)
                        MessageBox.Show("CANTIDADES NEGATIVAS. VERIFIQUE ARCHIVO EN C:\\Temp\\NetosNegativos.txt", "Información");
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
                #endregion

                FLAGPRIMERCALCULO = false;

                #region CALCULO
                //lstValoresNomina = new List<tmpPagoNomina>();
                //int contadorNomina = lstDatosNomina.Count;
                //int progreso = 0;
                //for (int i = 0; i < lstDatosNomina.Count; i++)
                //{
                //    progreso = (i * 100) / contadorNomina;

                //    tmpPagoNomina vn = new tmpPagoNomina();
                //    vn.idtrabajador = lstDatosNomina[i].idtrabajador;
                //    vn.idempresa = GLOBALES.IDEMPRESA;
                //    vn.idconcepto = lstDatosNomina[i].id;
                //    vn.noconcepto = lstDatosNomina[i].noconcepto;
                //    vn.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                //    vn.fechainicio = dtpPeriodoInicio.Value.Date;
                //    vn.fechafin = dtpPeriodoFin.Value.Date;
                //    vn.guardada = false;
                //    vn.tiponomina = _tipoNomina;
                //    vn.modificado = false;

                //    lstDatoTrabajador = new List<CalculoNomina.Core.Nomina>();
                //    CalculoNomina.Core.Nomina nt = new CalculoNomina.Core.Nomina();
                //    nt.idtrabajador = lstDatosNomina[i].idtrabajador;
                //    nt.dias = lstDatosNomina[i].dias;
                //    nt.salariominimo = lstDatosNomina[i].salariominimo;
                //    nt.antiguedadmod = lstDatosNomina[i].antiguedadmod;
                //    nt.sdi = lstDatosNomina[i].sdi;
                //    nt.sd = lstDatosNomina[i].sd;
                //    nt.id = lstDatosNomina[i].id;
                //    nt.noconcepto = lstDatosNomina[i].noconcepto;
                //    nt.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                //    nt.formula = lstDatosNomina[i].formula;
                //    nt.formulaexento = lstDatosNomina[i].formulaexento;
                //    lstDatoTrabajador.Add(nt);

                //    Infonavit.Core.InfonavitHelper infh = new Infonavit.Core.InfonavitHelper();
                //    infh.Command = cmd;

                //    Infonavit.Core.Infonavit inf = new Infonavit.Core.Infonavit();
                //    inf.idtrabajador = lstDatosNomina[i].idtrabajador;
                //    inf.idempresa = GLOBALES.IDEMPRESA;

                //    if (lstDatosNomina[i].noconcepto == 10 || lstDatosNomina[i].noconcepto == 11 || lstDatosNomina[i].noconcepto == 12)
                //    {
                //        cnx.Open();
                //        activoInfonavit = (bool)infh.activoInfonavit(inf);
                //        cnx.Close();
                //    }

                //    FormulasValores f = new FormulasValores(lstDatoTrabajador, dtpPeriodoInicio.Value.Date, dtpPeriodoFin.Value.Date);
                //    vn.cantidad = double.Parse(f.calcularFormula().ToString());
                //    vn.exento = double.Parse(f.calcularFormulaExento().ToString());
                //    vn.gravado = double.Parse(f.calcularFormula().ToString()) - double.Parse(f.calcularFormulaExento().ToString());

                //    switch (lstDatosNomina[i].noconcepto)
                //    {
                //        case 1:
                //            if (vn.cantidad == 0)
                //            {
                //                i++;
                //                vn.gravado = 0;
                //                lstValoresNomina.Add(vn);
                //                int contadorDatosNomina = i;
                //                for (int j = i; j < lstDatosNomina.Count; j++)
                //                {
                //                    contadorDatosNomina = j;
                //                    if (lstDatosNomina[j].idtrabajador == vn.idtrabajador)
                //                    {
                //                        tmpPagoNomina vnCero = new tmpPagoNomina();
                //                        vnCero.idtrabajador = lstDatosNomina[j].idtrabajador;
                //                        vnCero.idempresa = GLOBALES.IDEMPRESA;
                //                        vnCero.idconcepto = lstDatosNomina[j].id;
                //                        vnCero.noconcepto = lstDatosNomina[j].noconcepto;
                //                        vnCero.tipoconcepto = lstDatosNomina[j].tipoconcepto;
                //                        vnCero.fechainicio = dtpPeriodoInicio.Value.Date;
                //                        vnCero.fechafin = dtpPeriodoFin.Value.Date;
                //                        vnCero.guardada = false;
                //                        vnCero.tiponomina = _tipoNomina;
                //                        vnCero.modificado = false;
                //                        vnCero.cantidad = 0;
                //                        vnCero.exento = 0;
                //                        vnCero.gravado = 0;
                //                        lstValoresNomina.Add(vnCero);
                //                    }
                //                    else
                //                    {
                //                        --contadorDatosNomina;
                //                        break;
                //                    }
                //                }
                //                i = contadorDatosNomina;
                //            }
                //            else
                //                lstValoresNomina.Add(vn);
                //            break;
                //        case 2: // HORAS EXTRAS DOBLES
                //            if (vn.cantidad <= vn.exento)
                //            {
                //                vn.exento = vn.cantidad;
                //                vn.gravado = 0;

                //            }
                //            lstValoresNomina.Add(vn);
                //            break;
                //        case 3: // PREMIO DE ASISTENCIA
                //            if (vn.cantidad <= vn.exento)
                //            {
                //                vn.exento = vn.cantidad;
                //                vn.gravado = 0;
                //            }
                //            lstValoresNomina.Add(vn);
                //            break;
                //        case 5: // PREMIO DE PUNTUALIDAD
                //            if (vn.cantidad <= vn.exento)
                //            {
                //                vn.exento = vn.cantidad;
                //                vn.gravado = 0;
                //            }
                //            lstValoresNomina.Add(vn);
                //            break;
                //        case 10:
                //            if (!activoInfonavit)
                //            {
                //                vn.cantidad = 0;
                //                vn.exento = 0;
                //                vn.gravado = 0;
                //            }
                //            lstValoresNomina.Add(vn);
                //            break;
                //        case 11:
                //            if (!activoInfonavit)
                //            {
                //                vn.cantidad = 0;
                //                vn.exento = 0;
                //                vn.gravado = 0;
                //            }
                //            lstValoresNomina.Add(vn);
                //            break;
                //        case 12:
                //            if (!activoInfonavit)
                //            {
                //                vn.cantidad = 0;
                //                vn.exento = 0;
                //                vn.gravado = 0;
                //            }
                //            lstValoresNomina.Add(vn);
                //            break;
                //        default:
                //            lstValoresNomina.Add(vn);
                //            break;
                //    }

                //    workerCalculo.ReportProgress(progreso, "Calculo de Nómina");
                //}
                //workerCalculo.ReportProgress(100, "Calculo de Nómina");
                #endregion
            }
            else
            {
                #region DATOS DE LA NOMINA PARA RECALCULO (PERCEPCIONES)
                nh = new CalculoNomina.Core.NominaHelper();
                nh.Command = cmd;

                _lstDatosNomina = new List<CalculoNomina.Core.NominaRecalculo>();
                try
                {
                    cnx.Open();
                    _lstDatosNomina = nh.obtenerDatosNominaRecalculo(GLOBALES.IDEMPRESA, _tipoNomina, dtpPeriodoInicio.Value.Date, dtpPeriodoFin.Value.Date, "P");
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }
                #endregion

                #region RECALCULO DE PERCEPCIONES
                workerCalculo.ReportProgress(50, "Recalculo de percepciones. Espere...");
                CALCULO.RECALCULO_PERCEPCIONES(_lstDatosNomina,
                                    dtpPeriodoInicio.Value.Date,
                                    dtpPeriodoFin.Value.Date,
                                    _tipoNomina);
                workerCalculo.ReportProgress(100, "Recalculo de percepciones. Terminado.");
                #endregion

                #region MOSTRAR DATOS EN EL GRID
                int contadorGrid = dgvEmpleados.Rows.Count;
                int contador = 0;
                int progreso = 0;
                List<CalculoNomina.Core.tmpPagoNomina> lstPreNomina = new List<CalculoNomina.Core.tmpPagoNomina>();
                CalculoNomina.Core.tmpPagoNomina pre = new CalculoNomina.Core.tmpPagoNomina();
                pre.idempresa = GLOBALES.IDEMPRESA;
                pre.fechainicio = dtpPeriodoInicio.Value.Date;
                pre.fechafin = dtpPeriodoFin.Value.Date;
                try
                {
                    cnx.Open();
                    lstPreNomina = nh.obtenerPreNomina(pre);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Obtencion de Prenomina para cargar el grid. \r\n \r\n" + error.Message, "Error");
                    return;
                }

                foreach (DataGridViewRow fila in dgvEmpleados.Rows)
                {
                    progreso = (contador * 100) / contadorGrid;
                    workerCalculo.ReportProgress(progreso, "Mostrar datos");
                    contador++;

                    for (int i = 0; i < lstPreNomina.Count; i++)
                    {
                        if ((int)fila.Cells["idtrabajador"].Value == lstPreNomina[i].idtrabajador)
                        {
                            switch (lstPreNomina[i].noconcepto)
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
                workerCalculo.ReportProgress(100, "Mostrar datos");
                #endregion

                #region VERIFICACION DE PRIMA VACACIONAL Y VACACIONES
                vh = new Vacaciones.Core.VacacionesHelper();
                vh.Command = cmd;
                contadorGrid = dgvEmpleados.Rows.Count;
                contador = 0;
                progreso = 0;
                foreach (DataGridViewRow fila in dgvEmpleados.Rows)
                {
                    progreso = (contador * 100) / contadorGrid;
                    workerCalculo.ReportProgress(progreso, "Verificación de Prima Vacacional y Vacaciones");
                    contador++;

                    if (double.Parse(fila.Cells["sueldo"].Value.ToString()) != 0)
                    {
                        Vacaciones.Core.Vacaciones vacacion = new Vacaciones.Core.Vacaciones();
                        vacacion.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                        vacacion.inicio = dtpPeriodoInicio.Value.Date;
                        vacacion.fin = dtpPeriodoFin.Value.Date;
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

                            CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                            vn.idtrabajador = (int)fila.Cells["idtrabajador"].Value;
                            vn.idempresa = GLOBALES.IDEMPRESA;
                            vn.idconcepto = lstConcepto[0].id;
                            vn.noconcepto = 4;
                            vn.tipoconcepto = lstConcepto[0].tipoconcepto;
                            vn.fechainicio = dtpPeriodoInicio.Value.Date;
                            vn.fechafin = dtpPeriodoFin.Value.Date;
                            vn.exento = lstPrima[0].pexenta;
                            vn.gravado = lstPrima[0].pgravada;
                            vn.cantidad = lstPrima[0].pv;
                            vn.guardada = false;
                            vn.tiponomina = _tipoNomina;
                            vn.modificado = false;
                            cnx.Open();
                            nh.actualizaConcepto(vn);
                            cnx.Close();
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

                            CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                            vn.idtrabajador = (int)fila.Cells["idtrabajador"].Value;
                            vn.idempresa = GLOBALES.IDEMPRESA;
                            vn.idconcepto = lstConcepto[0].id;
                            vn.noconcepto = 7;
                            vn.tipoconcepto = lstConcepto[0].tipoconcepto;
                            vn.fechainicio = dtpPeriodoInicio.Value.Date;
                            vn.fechafin = dtpPeriodoFin.Value.Date;
                            vn.exento = 0;
                            vn.gravado = vacacionesPagadas;
                            vn.cantidad = vacacionesPagadas;
                            vn.guardada = false;
                            vn.tiponomina = _tipoNomina;
                            vn.modificado = false;
                            cnx.Open();
                            nh.actualizaConcepto(vn);
                            cnx.Close();
                        }
                    }
                }
                workerCalculo.ReportProgress(100, "Verificación de Prima Vacacional y Vacaciones");
                #endregion

                #region DATOS DE LA NOMINA PARA EL RECALCULO (DEDUCCIONES)
                nh = new CalculoNomina.Core.NominaHelper();
                nh.Command = cmd;
                lstDatosNomina = new List<CalculoNomina.Core.Nomina>();
                
                List<CalculoNomina.Core.tmpPagoNomina> lstRecalculoPercepciones = new List<CalculoNomina.Core.tmpPagoNomina>();
                CalculoNomina.Core.tmpPagoNomina recalculopercepciones = new CalculoNomina.Core.tmpPagoNomina();
                recalculopercepciones.idempresa = GLOBALES.IDEMPRESA;
                recalculopercepciones.fechainicio = dtpPeriodoInicio.Value.Date;
                recalculopercepciones.fechafin = dtpPeriodoFin.Value.Date;
                recalculopercepciones.tiponomina = _tipoNomina;
                recalculopercepciones.tipoconcepto = "P";

                try
                {
                    cnx.Open();
                    _lstDatosNomina = nh.obtenerDatosNominaRecalculo(GLOBALES.IDEMPRESA, _tipoNomina, dtpPeriodoInicio.Value.Date, dtpPeriodoFin.Value.Date, "D");
                    lstRecalculoPercepciones = nh.obtenerPercepciones(recalculopercepciones);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }
                #endregion

                #region RECALCULO DE DEDUCCIONES
                workerCalculo.ReportProgress(50, "Recalculo de deducciones. Espere...");
                CALCULO.RECALCULO_ISR_SUBSIDIO(_lstDatosNomina, lstRecalculoPercepciones,
                                    dtpPeriodoInicio.Value.Date,
                                    dtpPeriodoFin.Value.Date,
                                    _tipoNomina);
                workerCalculo.ReportProgress(100, "Recalculo de deducciones. Terminado.");
                #endregion

                #region RECALCULO DE ISR RETENIDO Y SUBSIDIO PAGADO

                ch = new Conceptos.Core.ConceptosHelper();
                ch.Command = cmd;
                Conceptos.Core.Conceptos conceptoSubsidio = new Conceptos.Core.Conceptos();
                Conceptos.Core.Conceptos conceptoIsr = new Conceptos.Core.Conceptos();
                Conceptos.Core.Conceptos sub = new Conceptos.Core.Conceptos();
                Conceptos.Core.Conceptos isr = new Conceptos.Core.Conceptos();

                conceptoSubsidio.noconcepto = 16; //SUBSIDIO AL EMPLEO
                conceptoSubsidio.idempresa = GLOBALES.IDEMPRESA;

                conceptoIsr.noconcepto = 17; //ISR
                conceptoIsr.idempresa = GLOBALES.IDEMPRESA;

                sub.noconcepto = 15;
                sub.idempresa = GLOBALES.IDEMPRESA;

                isr.noconcepto = 8;
                isr.idempresa = GLOBALES.IDEMPRESA;

                List<Conceptos.Core.Conceptos> lstConceptoSubsidio = new List<Conceptos.Core.Conceptos>();
                List<Conceptos.Core.Conceptos> lstConceptoIsr = new List<Conceptos.Core.Conceptos>();

                List<Conceptos.Core.Conceptos> lstSub = new List<Conceptos.Core.Conceptos>();
                List<Conceptos.Core.Conceptos> lstIsr = new List<Conceptos.Core.Conceptos>();

                List<CalculoNomina.Core.tmpPagoNomina> lstRecalculoDeducciones = new List<CalculoNomina.Core.tmpPagoNomina>();
                CalculoNomina.Core.tmpPagoNomina recalculodeducciones = new CalculoNomina.Core.tmpPagoNomina();
                recalculodeducciones.idempresa = GLOBALES.IDEMPRESA;
                recalculodeducciones.fechainicio = dtpPeriodoInicio.Value.Date;
                recalculodeducciones.fechafin = dtpPeriodoFin.Value.Date;
                recalculodeducciones.tiponomina = _tipoNomina;
                recalculodeducciones.tipoconcepto = "D";

                try
                {
                    cnx.Open();
                    lstConceptoSubsidio = ch.obtenerConceptoNomina(conceptoSubsidio);
                    lstConceptoIsr = ch.obtenerConceptoNomina(conceptoIsr);
                    lstSub = ch.obtenerConceptoNomina(sub);
                    lstIsr = ch.obtenerConceptoNomina(isr);
                    lstRecalculoDeducciones = nh.obtenerDeducciones(recalculodeducciones);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }

                int contadorNomina = lstRecalculoDeducciones.Count;
                progreso = 0;
                double subsidio = 0, isrr = 0, ispt = 0;
                for (int i = 0; i < lstRecalculoDeducciones.Count; i++)
                {
                    progreso = (i * 100) / contadorNomina;

                    if (lstRecalculoDeducciones[i].noconcepto == 8)
                    {
                        isrr = lstRecalculoDeducciones[i].cantidad;
                        for (int j = 0; j < lstRecalculoDeducciones.Count; j++)
                        {
                            if (lstRecalculoDeducciones[j].noconcepto == 15 && lstRecalculoDeducciones[i].idtrabajador == lstRecalculoDeducciones[j].idtrabajador)
                            {
                                subsidio = lstRecalculoDeducciones[j].cantidad;
                                ispt = ((isrr - subsidio) / 30.4) * _periodo;
                                if (ispt <= 0)
                                {
                                    CalculoNomina.Core.tmpPagoNomina pIsr = new CalculoNomina.Core.tmpPagoNomina();
                                    pIsr.id = lstRecalculoDeducciones[i].id;
                                    pIsr.idtrabajador = lstRecalculoDeducciones[i].idtrabajador;
                                    pIsr.idempresa = GLOBALES.IDEMPRESA;
                                    pIsr.idconcepto = lstConceptoIsr[0].id;
                                    pIsr.noconcepto = 17;
                                    pIsr.tipoconcepto = lstConceptoIsr[0].tipoconcepto;
                                    pIsr.exento = 0;
                                    pIsr.gravado = 0;
                                    pIsr.cantidad = 0;
                                    pIsr.fechainicio = dtpPeriodoInicio.Value.Date;
                                    pIsr.fechafin = dtpPeriodoFin.Value.Date;
                                    pIsr.guardada = false;
                                    pIsr.tiponomina = _tipoNomina;
                                    pIsr.modificado = false;

                                    cnx.Open();
                                    nh.actualizaConcepto(pIsr);
                                    cnx.Close();
                                }
                                else
                                {
                                    CalculoNomina.Core.tmpPagoNomina pIsr = new CalculoNomina.Core.tmpPagoNomina();
                                    pIsr.id = lstRecalculoDeducciones[i].id;
                                    pIsr.idtrabajador = lstRecalculoDeducciones[i].idtrabajador;
                                    pIsr.idempresa = GLOBALES.IDEMPRESA;
                                    pIsr.idconcepto = lstConceptoIsr[0].id;
                                    pIsr.noconcepto = 17;
                                    pIsr.tipoconcepto = lstConceptoIsr[0].tipoconcepto;
                                    pIsr.exento = 0;
                                    pIsr.gravado = 0;
                                    pIsr.cantidad = ispt;
                                    pIsr.fechainicio = dtpPeriodoInicio.Value.Date;
                                    pIsr.fechafin = dtpPeriodoFin.Value.Date;
                                    pIsr.guardada = false;
                                    pIsr.tiponomina = _tipoNomina;
                                    pIsr.modificado = false;

                                    cnx.Open();
                                    nh.actualizaConcepto(pIsr);
                                    cnx.Close();

                                }
                            }
                        }
                    }
                    workerCalculo.ReportProgress(progreso, "Subsidio al empleo e ISR a Retener.");
                }
                workerCalculo.ReportProgress(100, "Subsidio al empleo e ISR a Retener.");
                #endregion

                #region CALCULO
                //int contadorNomina = _lstDatosNomina.Count;
                //int progreso = 0;
                //for (int i = 0; i < _lstDatosNomina.Count; i++)
                //{
                //    progreso = (i * 100) / contadorNomina;

                //    if (!_lstDatosNomina[i].modificado)
                //    {
                //        CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                //        vn.id = _lstDatosNomina[i].id;
                //        vn.idtrabajador = _lstDatosNomina[i].idtrabajador;
                //        vn.idempresa = GLOBALES.IDEMPRESA;
                //        vn.idconcepto = _lstDatosNomina[i].idconcepto;
                //        vn.noconcepto = _lstDatosNomina[i].noconcepto;
                //        vn.tipoconcepto = _lstDatosNomina[i].tipoconcepto;
                //        vn.fechainicio = dtpPeriodoInicio.Value.Date;
                //        vn.fechafin = dtpPeriodoFin.Value.Date;
                //        vn.guardada = false;
                //        vn.tiponomina = _tipoNomina;
                //        vn.modificado = false;

                //        _lstDatoTrabajador = new List<CalculoNomina.Core.NominaRecalculo>();
                //        CalculoNomina.Core.NominaRecalculo nt = new CalculoNomina.Core.NominaRecalculo();
                //        nt.idtrabajador = _lstDatosNomina[i].idtrabajador;
                //        nt.dias = _lstDatosNomina[i].dias;
                //        nt.salariominimo = _lstDatosNomina[i].salariominimo;
                //        nt.antiguedadmod = _lstDatosNomina[i].antiguedadmod;
                //        nt.sdi = _lstDatosNomina[i].sdi;
                //        nt.sd = _lstDatosNomina[i].sd;
                //        nt.idconcepto = _lstDatosNomina[i].idconcepto;
                //        nt.noconcepto = _lstDatosNomina[i].noconcepto;
                //        nt.tipoconcepto = _lstDatosNomina[i].tipoconcepto;
                //        nt.formula = _lstDatosNomina[i].formula;
                //        nt.formulaexento = _lstDatosNomina[i].formulaexento;
                //        _lstDatoTrabajador.Add(nt);

                //        Infonavit.Core.InfonavitHelper infh = new Infonavit.Core.InfonavitHelper();
                //        infh.Command = cmd;

                //        Infonavit.Core.Infonavit inf = new Infonavit.Core.Infonavit();
                //        inf.idtrabajador = _lstDatosNomina[i].idtrabajador;
                //        inf.idempresa = GLOBALES.IDEMPRESA;

                //        if (_lstDatosNomina[i].noconcepto == 10 || _lstDatosNomina[i].noconcepto == 11 || _lstDatosNomina[i].noconcepto == 12)
                //        {
                //            cnx.Open();
                //            activoInfonavit = (bool)infh.activoInfonavit(inf);
                //            cnx.Close();
                //        }

                //        FormulasValores f = new FormulasValores(_lstDatoTrabajador, dtpPeriodoInicio.Value.Date, dtpPeriodoFin.Value.Date);
                //        vn.cantidad = double.Parse(f.recalcularFormula().ToString());
                //        vn.exento = double.Parse(f.recalcularFormulaExento().ToString());
                //        vn.gravado = double.Parse(f.recalcularFormula().ToString()) - double.Parse(f.recalcularFormulaExento().ToString());

                //        switch (_lstDatosNomina[i].noconcepto)
                //        {
                //            case 1:
                //                if (vn.cantidad == 0)
                //                {
                //                    i++;
                //                    vn.gravado = 0;

                //                    cnx.Open();
                //                    nh.actualizaConcepto(vn);
                //                    cnx.Close();

                //                    int contadorDatosNomina = i;
                //                    for (int j = i; j < _lstDatosNomina.Count; j++)
                //                    {
                //                        contadorDatosNomina = j;
                //                        if (_lstDatosNomina[j].idtrabajador == vn.idtrabajador)
                //                        {
                //                            CalculoNomina.Core.tmpPagoNomina vnCero = new CalculoNomina.Core.tmpPagoNomina();
                //                            vnCero.id = _lstDatosNomina[j].id;
                //                            vnCero.idtrabajador = _lstDatosNomina[j].idtrabajador;
                //                            vnCero.idempresa = GLOBALES.IDEMPRESA;
                //                            vnCero.idconcepto = _lstDatosNomina[j].idconcepto;
                //                            vnCero.noconcepto = _lstDatosNomina[j].noconcepto;
                //                            vnCero.tipoconcepto = _lstDatosNomina[j].tipoconcepto;
                //                            vnCero.fechainicio = dtpPeriodoInicio.Value.Date;
                //                            vnCero.fechafin = dtpPeriodoFin.Value.Date;
                //                            vnCero.guardada = false;
                //                            vnCero.tiponomina = _tipoNomina;
                //                            vnCero.modificado = false;
                //                            vnCero.cantidad = 0;
                //                            vnCero.exento = 0;
                //                            vnCero.gravado = 0;

                //                            cnx.Open();
                //                            nh.actualizaConcepto(vnCero);
                //                            cnx.Close();
                //                        }
                //                        else
                //                        {
                //                            --contadorDatosNomina;
                //                            break;
                //                        }
                //                    }
                //                    i = contadorDatosNomina;
                //                }
                //                else
                //                {
                //                    cnx.Open();
                //                    nh.actualizaConcepto(vn);
                //                    cnx.Close();
                //                }
                //                break;
                //            case 2: // HORAS EXTRAS DOBLES
                //                if (vn.cantidad <= vn.exento)
                //                {
                //                    vn.exento = vn.cantidad;
                //                    vn.gravado = 0;

                //                }
                //                cnx.Open();
                //                nh.actualizaConcepto(vn);
                //                cnx.Close();
                //                break;
                //            case 3: // PREMIO DE ASISTENCIA
                //                if (vn.cantidad <= vn.exento)
                //                {
                //                    vn.exento = vn.cantidad;
                //                    vn.gravado = 0;
                //                }
                //                cnx.Open();
                //                nh.actualizaConcepto(vn);
                //                cnx.Close();
                //                break;
                //            case 5: // PREMIO DE PUNTUALIDAD
                //                if (vn.cantidad <= vn.exento)
                //                {
                //                    vn.exento = vn.cantidad;
                //                    vn.gravado = 0;
                //                }
                //                cnx.Open();
                //                nh.actualizaConcepto(vn);
                //                cnx.Close();
                //                break;
                //            case 10:
                //                if (!activoInfonavit)
                //                {
                //                    vn.cantidad = 0;
                //                    vn.exento = 0;
                //                    vn.gravado = 0;
                //                }
                //                cnx.Open();
                //                nh.actualizaConcepto(vn);
                //                cnx.Close();
                //                break;
                //            case 11:
                //                if (!activoInfonavit)
                //                {
                //                    vn.cantidad = 0;
                //                    vn.exento = 0;
                //                    vn.gravado = 0;
                //                }
                //                cnx.Open();
                //                nh.actualizaConcepto(vn);
                //                cnx.Close();
                //                break;
                //            case 12:
                //                if (!activoInfonavit)
                //                {
                //                    vn.cantidad = 0;
                //                    vn.exento = 0;
                //                    vn.gravado = 0;
                //                }
                //                cnx.Open();
                //                nh.actualizaConcepto(vn);
                //                cnx.Close();
                //                break;
                //            default:
                //                cnx.Open();
                //                nh.actualizaConcepto(vn);
                //                cnx.Close();
                //                break;
                //        }

                //        workerCalculo.ReportProgress(progreso, "Calculo de Nómina");
                //    }
                //}
                //workerCalculo.ReportProgress(100, "Calculo de Nómina");
                #endregion

            }

            #region DATOS DE LA NOMINA COMENTADA
            //foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            //{
            //    if ((bool)fila.Cells["seleccion"].Value && double.Parse(fila.Cells["sueldo"].Value.ToString()) == 0)
            //    {
            //        listaIds += fila.Cells["idtrabajador"].Value + ",";
            //    }
            //}

            //if (listaIds.Equals("")) return;

            //listaIds.Substring(listaIds.Length - 1, 1);

            //nh = new CalculoNomina.Core.NominaHelper();
            //nh.Command = cmd;
            //lstDatosNomina = new List<CalculoNomina.Core.Nomina>();
            //try
            //{
            //    cnx.Open();
            //    if (_tipoNomina == GLOBALES.NORMAL)
            //        lstDatosNomina = nh.obtenerDatosNomina(GLOBALES.IDEMPRESA, GLOBALES.ACTIVO, listaIds);
            //    if (_tipoNomina == GLOBALES.ESPECIAL)
            //        lstDatosNomina = nh.obtenerDatosNomina(GLOBALES.IDEMPRESA, GLOBALES.INACTIVO, listaIds);
            //    cnx.Close();
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //}
            #endregion

            #region CALCULO COMENTADA
            //lstValoresNomina = new List<tmpPagoNomina>();
            //int contadorNomina = lstDatosNomina.Count;
            //int progreso = 0;
            //for (int i = 0; i < lstDatosNomina.Count; i++)
            //{
            //    progreso = (i * 100) / contadorNomina;

            //    tmpPagoNomina vn = new tmpPagoNomina();
            //    vn.idtrabajador = lstDatosNomina[i].idtrabajador;
            //    vn.idempresa = GLOBALES.IDEMPRESA;
            //    vn.idconcepto = lstDatosNomina[i].id;
            //    vn.noconcepto = lstDatosNomina[i].noconcepto;
            //    vn.tipoconcepto = lstDatosNomina[i].tipoconcepto;
            //    vn.fechainicio = dtpPeriodoInicio.Value.Date;
            //    vn.fechafin = dtpPeriodoFin.Value.Date;
            //    vn.guardada = false;
            //    vn.tiponomina = _tipoNomina;

            //    lstDatoTrabajador = new List<CalculoNomina.Core.Nomina>();
            //    CalculoNomina.Core.Nomina nt = new CalculoNomina.Core.Nomina();
            //    nt.idtrabajador = lstDatosNomina[i].idtrabajador;
            //    nt.dias = lstDatosNomina[i].dias;
            //    nt.salariominimo = lstDatosNomina[i].salariominimo;
            //    nt.antiguedadmod = lstDatosNomina[i].antiguedadmod;
            //    nt.sdi = lstDatosNomina[i].sdi;
            //    nt.sd = lstDatosNomina[i].sd;
            //    nt.id = lstDatosNomina[i].id;
            //    nt.noconcepto = lstDatosNomina[i].noconcepto;
            //    nt.tipoconcepto = lstDatosNomina[i].tipoconcepto;
            //    nt.formula = lstDatosNomina[i].formula;
            //    nt.formulaexento = lstDatosNomina[i].formulaexento;
            //    lstDatoTrabajador.Add(nt);

            //    FormulasValores f = new FormulasValores(lstDatoTrabajador, dtpPeriodoInicio.Value.Date, dtpPeriodoFin.Value.Date);
            //    vn.cantidad = double.Parse(f.calcularFormula().ToString());
            //    vn.exento = double.Parse(f.calcularFormulaExento().ToString());
            //    vn.gravado = double.Parse(f.calcularFormula().ToString()) - double.Parse(f.calcularFormulaExento().ToString());

            //    switch (lstDatosNomina[i].noconcepto)
            //    {
            //        case 1:
            //            if (vn.cantidad == 0)
            //            {
            //                i++;
            //                vn.gravado = 0;
            //                lstValoresNomina.Add(vn);
            //                int contadorDatosNomina = i;
            //                for (int j = i; j < lstDatosNomina.Count; j++)
            //                {
            //                    contadorDatosNomina = j;
            //                    if (lstDatosNomina[j].idtrabajador == vn.idtrabajador)
            //                    {
            //                        tmpPagoNomina vnCero = new tmpPagoNomina();
            //                        vnCero.idtrabajador = lstDatosNomina[j].idtrabajador;
            //                        vnCero.idempresa = GLOBALES.IDEMPRESA;
            //                        vnCero.idconcepto = lstDatosNomina[j].id;
            //                        vnCero.noconcepto = lstDatosNomina[j].noconcepto;
            //                        vnCero.tipoconcepto = lstDatosNomina[j].tipoconcepto;
            //                        vnCero.fechainicio = dtpPeriodoInicio.Value.Date;
            //                        vnCero.fechafin = dtpPeriodoFin.Value.Date;
            //                        vnCero.guardada = false;
            //                        vnCero.tiponomina = _tipoNomina;
            //                        vnCero.cantidad = 0;
            //                        vnCero.exento = 0;
            //                        vnCero.gravado = 0;
            //                        lstValoresNomina.Add(vnCero);
            //                    }
            //                    else
            //                    {
            //                        --contadorDatosNomina;
            //                        break;
            //                    }
            //                }
            //                i = contadorDatosNomina;
            //            }
            //            else
            //                lstValoresNomina.Add(vn);
            //            break;
            //        case 2: // HORAS EXTRAS DOBLES
            //            if (vn.cantidad <= vn.exento)
            //            {
            //                vn.exento = vn.cantidad;
            //                vn.gravado = 0;
                            
            //            }
            //            lstValoresNomina.Add(vn);
            //            break;
            //        case 3: // PREMIO DE ASISTENCIA
            //            if (vn.cantidad <= vn.exento)
            //            {
            //                vn.exento = vn.cantidad;
            //                vn.gravado = 0;
            //            }
            //            lstValoresNomina.Add(vn);
            //            break;
            //        case 5: // PREMIO DE PUNTUALIDAD
            //            if (vn.cantidad <= vn.exento)
            //            {
            //                vn.exento = vn.cantidad;
            //                vn.gravado = 0;
            //            }
            //            lstValoresNomina.Add(vn);
            //            break;
            //        default:
            //            lstValoresNomina.Add(vn);
            //            break;
            //    }

            //    workerCalculo.ReportProgress(progreso, "Calculo de Nómina");
            //}
            //workerCalculo.ReportProgress(100, "Calculo de Nómina");
            #endregion

            #region VERIFICACION DE PRIMA VACACIONAL Y VACACIONES COMENTADA
            //vh = new Vacaciones.Core.VacacionesHelper();
            //vh.Command = cmd;
            //int contadorGrid = dgvEmpleados.Rows.Count;
            //int contador = 0;
            //progreso = 0;
            //foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            //{
            //    progreso = (contador * 100) / contadorGrid;
            //    workerCalculo.ReportProgress(progreso, "Verificación de Prima Vacacional y Vacaciones");
            //    contador++;

            //    if ((bool)fila.Cells["seleccion"].Value && double.Parse(fila.Cells["sueldo"].Value.ToString()) != 0)
            //    {
            //        Vacaciones.Core.Vacaciones vacacion = new Vacaciones.Core.Vacaciones();
            //        vacacion.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
            //        vacacion.inicio = dtpPeriodoInicio.Value.Date;
            //        vacacion.fin = dtpPeriodoFin.Value.Date;
            //        List<Vacaciones.Core.Vacaciones> lstPrima = new List<Vacaciones.Core.Vacaciones>();
            //        double vacacionesPagadas = 0;
            //        try
            //        {
            //            cnx.Open();
            //            lstPrima = vh.primaVacacional(vacacion);
            //            vacacionesPagadas = double.Parse(vh.vacacionesPagadas(vacacion).ToString());
            //            cnx.Close();
            //        }
            //        catch (Exception error)
            //        {
            //            MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //            cnx.Dispose();
            //        }

            //        if (lstPrima[0].pv != 0)
            //        {
            //            ch = new Conceptos.Core.ConceptosHelper();
            //            ch.Command = cmd;
            //            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            //            concepto.noconcepto = 4; //PRIMA VACACIONAL
            //            concepto.idempresa = GLOBALES.IDEMPRESA;
            //            List<Conceptos.Core.Conceptos> lstConcepto = new List<Conceptos.Core.Conceptos>();
            //            try
            //            {
            //                cnx.Open();
            //                lstConcepto = ch.obtenerConceptoNomina(concepto);
            //                cnx.Close();
            //            }
            //            catch (Exception error)
            //            {
            //                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //            }

            //            tmpPagoNomina vn = new tmpPagoNomina();
            //            vn.idtrabajador = (int)fila.Cells["idtrabajador"].Value;
            //            vn.idempresa = GLOBALES.IDEMPRESA;
            //            vn.idconcepto = lstConcepto[0].id;
            //            vn.noconcepto = 4;
            //            vn.tipoconcepto = lstConcepto[0].tipoconcepto;
            //            vn.fechainicio = dtpPeriodoInicio.Value.Date;
            //            vn.fechafin = dtpPeriodoFin.Value.Date;
            //            vn.exento = lstPrima[0].pexenta;
            //            vn.gravado = lstPrima[0].pgravada;
            //            vn.cantidad = lstPrima[0].pv;
            //            vn.guardada = false;
            //            vn.tiponomina = _tipoNomina;
            //            lstValoresNomina.Add(vn);
            //        }

            //        if (vacacionesPagadas != 0)
            //        {
            //            ch = new Conceptos.Core.ConceptosHelper();
            //            ch.Command = cmd;
            //            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            //            concepto.noconcepto = 7; //VACACIONES
            //            concepto.idempresa = GLOBALES.IDEMPRESA;
            //            List<Conceptos.Core.Conceptos> lstConcepto = new List<Conceptos.Core.Conceptos>();
            //            try
            //            {
            //                cnx.Open();
            //                lstConcepto = ch.obtenerConceptoNomina(concepto);
            //                cnx.Close();
            //            }
            //            catch (Exception error)
            //            {
            //                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //            }

            //            tmpPagoNomina vn = new tmpPagoNomina();
            //            vn.idtrabajador = (int)fila.Cells["idtrabajador"].Value;
            //            vn.idempresa = GLOBALES.IDEMPRESA;
            //            vn.idconcepto = lstConcepto[0].id;
            //            vn.noconcepto = 7;
            //            vn.tipoconcepto = lstConcepto[0].tipoconcepto;
            //            vn.fechainicio = dtpPeriodoInicio.Value.Date;
            //            vn.fechafin = dtpPeriodoFin.Value.Date;
            //            vn.exento = 0;
            //            vn.gravado = vacacionesPagadas;
            //            vn.cantidad = vacacionesPagadas;
            //            vn.guardada = false;
            //            vn.tiponomina = _tipoNomina;
            //            lstValoresNomina.Add(vn);
            //        }
            //    }
            //}
            //workerCalculo.ReportProgress(100, "Verificación de Prima Vacacional y Vacaciones");
            #endregion

            #region PROGRAMACION DE DEDUCCIONES COMENTADA
            //pch = new ProgramacionConcepto.Core.ProgramacionHelper();
            //pch.Command = cmd;
            //contadorGrid = dgvEmpleados.Rows.Count;
            //contador = 0;
            //progreso = 0;
            //foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            //{
            //    progreso = (contador * 100) / contadorGrid;
            //    workerCalculo.ReportProgress(progreso, "Otras Deducciones");
            //    contador++;

            //    if ((bool)fila.Cells["seleccion"].Value && double.Parse(fila.Cells["sueldo"].Value.ToString()) == 0)
            //    {
            //        int existe = 0;
            //        ProgramacionConcepto.Core.ProgramacionConcepto programacion = new ProgramacionConcepto.Core.ProgramacionConcepto();
            //        programacion.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());

            //        List<ProgramacionConcepto.Core.ProgramacionConcepto> lstProgramacion = new List<ProgramacionConcepto.Core.ProgramacionConcepto>();
                    
            //        try
            //        {
            //            cnx.Open();
            //            existe = (int)pch.existeProgramacion(programacion);
            //            cnx.Close();
            //        }
            //        catch (Exception error)
            //        {
            //            MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //            cnx.Dispose();
            //        }

            //        if (existe != 0)
            //        {
            //            try
            //            {
            //                cnx.Open();
            //                lstProgramacion = pch.obtenerProgramacion(programacion);
            //                cnx.Close();
            //            }
            //            catch (Exception error)
            //            {
            //                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //                cnx.Dispose();
            //            }

            //            for (int i = 0; i < lstProgramacion.Count; i++)
            //            {
            //                if (dtpPeriodoFin.Value <= lstProgramacion[i].fechafin)
            //                {
            //                    ch = new Conceptos.Core.ConceptosHelper();
            //                    ch.Command = cmd;
            //                    Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            //                    concepto.id = lstProgramacion[i].idconcepto;
            //                    List<Conceptos.Core.Conceptos> lstNoConcepto = new List<Conceptos.Core.Conceptos>();
            //                    try {
            //                        cnx.Open();
            //                        lstNoConcepto = ch.obtenerConcepto(concepto);
            //                        cnx.Close();
            //                    }
            //                    catch (Exception error) { MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error"); }

            //                    tmpPagoNomina vn = new tmpPagoNomina();
            //                    vn.idtrabajador = (int)fila.Cells["idtrabajador"].Value;
            //                    vn.idempresa = GLOBALES.IDEMPRESA;
            //                    vn.idconcepto = lstProgramacion[i].idconcepto;
            //                    vn.noconcepto = lstNoConcepto[0].noconcepto;
            //                    vn.tipoconcepto = "D";
            //                    vn.fechainicio = dtpPeriodoInicio.Value.Date;
            //                    vn.fechafin = dtpPeriodoFin.Value.Date;
            //                    vn.exento = 0;
            //                    vn.gravado = 0;
            //                    vn.cantidad = lstProgramacion[i].cantidad;
            //                    vn.guardada = false;
            //                    vn.tiponomina = _tipoNomina;
            //                    lstValoresNomina.Add(vn);
            //                }
            //            }
            //        }
            //    }
            //}
            //workerCalculo.ReportProgress(100, "Otras Deducciones");
            #endregion

            #region CALCULO DE ISR RETENIDO Y SUBSIDIO PAGADO COMENTADA

            //ch = new Conceptos.Core.ConceptosHelper();
            //ch.Command = cmd;
            //Conceptos.Core.Conceptos conceptoSubsidio = new Conceptos.Core.Conceptos();
            //Conceptos.Core.Conceptos conceptoIsr = new Conceptos.Core.Conceptos();
            //Conceptos.Core.Conceptos sub = new Conceptos.Core.Conceptos();
            //Conceptos.Core.Conceptos isr = new Conceptos.Core.Conceptos();

            //conceptoSubsidio.noconcepto = 16; //SUBSIDIO AL EMPLEO
            //conceptoSubsidio.idempresa = GLOBALES.IDEMPRESA;

            //conceptoIsr.noconcepto = 17; //ISR
            //conceptoIsr.idempresa = GLOBALES.IDEMPRESA;

            //sub.noconcepto = 15;
            //sub.idempresa = GLOBALES.IDEMPRESA;

            //isr.noconcepto = 8;
            //isr.idempresa = GLOBALES.IDEMPRESA;

            //List<Conceptos.Core.Conceptos> lstConceptoSubsidio = new List<Conceptos.Core.Conceptos>();
            //List<Conceptos.Core.Conceptos> lstConceptoIsr = new List<Conceptos.Core.Conceptos>();

            //List<Conceptos.Core.Conceptos> lstSub = new List<Conceptos.Core.Conceptos>();
            //List<Conceptos.Core.Conceptos> lstIsr = new List<Conceptos.Core.Conceptos>();
            //try
            //{
            //    cnx.Open();
            //    lstConceptoSubsidio = ch.obtenerConceptoNomina(conceptoSubsidio);
            //    lstConceptoIsr = ch.obtenerConceptoNomina(conceptoIsr);
            //    lstSub = ch.obtenerConceptoNomina(sub);
            //    lstIsr = ch.obtenerConceptoNomina(isr);
            //    cnx.Close();
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //}

            //contadorNomina = lstDatosNomina.Count;
            //progreso = 0;
            //double subsidio = 0, isrr = 0;
            //bool FLAGSUBSIDIO = false, FLAGISR = false;
            //for (int i = 0; i < lstValoresNomina.Count; i++)
            //{
            //    progreso = (i * 100) / contadorNomina;

            //    if (lstValoresNomina[i].noconcepto == 8)
            //    {
            //        isrr = lstValoresNomina[i].cantidad;
            //        FLAGISR = true;
            //    }

            //    if (lstValoresNomina[i].noconcepto == 15)
            //    {
            //        subsidio = lstValoresNomina[i].cantidad;
            //        FLAGSUBSIDIO = true;
            //    }

            //    if (FLAGISR && FLAGSUBSIDIO)
            //    {
            //        if (isrr != 0 && subsidio != 0)
            //        {
            //            if (subsidio > isrr)
            //            {
            //                tmpPagoNomina pSubsidio = new tmpPagoNomina();
            //                pSubsidio.idtrabajador = lstValoresNomina[i].idtrabajador;
            //                pSubsidio.idempresa = GLOBALES.IDEMPRESA;
            //                pSubsidio.idconcepto = lstConceptoSubsidio[0].id;
            //                pSubsidio.noconcepto = 16;
            //                pSubsidio.tipoconcepto = lstConceptoSubsidio[0].tipoconcepto;
            //                pSubsidio.exento = 0;
            //                pSubsidio.gravado = 0;
            //                pSubsidio.cantidad = subsidio - isrr;
            //                pSubsidio.fechainicio = dtpPeriodoInicio.Value.Date;
            //                pSubsidio.fechafin = dtpPeriodoFin.Value.Date;
            //                pSubsidio.guardada = false;
            //                pSubsidio.tiponomina = _tipoNomina;
            //                lstValoresNomina.Add(pSubsidio);

            //                tmpPagoNomina pIsr = new tmpPagoNomina();
            //                pIsr.idtrabajador = lstValoresNomina[i].idtrabajador;
            //                pIsr.idempresa = GLOBALES.IDEMPRESA;
            //                pIsr.idconcepto = lstConceptoIsr[0].id;
            //                pIsr.noconcepto = 17;
            //                pIsr.tipoconcepto = lstConceptoIsr[0].tipoconcepto;
            //                pIsr.exento = 0;
            //                pIsr.gravado = 0;
            //                pIsr.cantidad = 0;
            //                pIsr.fechainicio = dtpPeriodoInicio.Value.Date;
            //                pIsr.fechafin = dtpPeriodoFin.Value.Date;
            //                pIsr.guardada = false;
            //                pIsr.tiponomina = _tipoNomina;
            //                lstValoresNomina.Add(pIsr);
            //            }
            //            else
            //            {
            //                tmpPagoNomina pSubsidio = new tmpPagoNomina();
            //                pSubsidio.idtrabajador = lstValoresNomina[i].idtrabajador;
            //                pSubsidio.idempresa = GLOBALES.IDEMPRESA;
            //                pSubsidio.idconcepto = lstConceptoSubsidio[0].id;
            //                pSubsidio.noconcepto = 16;
            //                pSubsidio.tipoconcepto = lstConceptoSubsidio[0].tipoconcepto;
            //                pSubsidio.exento = 0;
            //                pSubsidio.gravado = 0;
            //                pSubsidio.cantidad = 0;
            //                pSubsidio.fechainicio = dtpPeriodoInicio.Value.Date;
            //                pSubsidio.fechafin = dtpPeriodoFin.Value.Date;
            //                pSubsidio.guardada = false;
            //                pSubsidio.tiponomina = _tipoNomina;
            //                lstValoresNomina.Add(pSubsidio);

            //                tmpPagoNomina pIsr = new tmpPagoNomina();
            //                pIsr.idtrabajador = lstValoresNomina[i].idtrabajador;
            //                pIsr.idempresa = GLOBALES.IDEMPRESA;
            //                pIsr.idconcepto = lstConceptoIsr[0].id;
            //                pIsr.noconcepto = 17;
            //                pIsr.tipoconcepto = lstConceptoIsr[0].tipoconcepto;
            //                pIsr.exento = 0;
            //                pIsr.gravado = 0;
            //                pIsr.cantidad = isrr - subsidio;
            //                pIsr.fechainicio = dtpPeriodoInicio.Value.Date;
            //                pIsr.fechafin = dtpPeriodoFin.Value.Date;
            //                pIsr.guardada = false;
            //                pIsr.tiponomina = _tipoNomina;
            //                lstValoresNomina.Add(pIsr);
            //            }
            //            FLAGISR = false;
            //            FLAGSUBSIDIO = false;
            //        }
            //        else
            //        {
            //            tmpPagoNomina pSubsidio = new tmpPagoNomina();
            //            pSubsidio.idtrabajador = lstValoresNomina[i].idtrabajador;
            //            pSubsidio.idempresa = GLOBALES.IDEMPRESA;
            //            pSubsidio.idconcepto = lstConceptoSubsidio[0].id;
            //            pSubsidio.noconcepto = 16;
            //            pSubsidio.tipoconcepto = lstConceptoSubsidio[0].tipoconcepto;
            //            pSubsidio.exento = 0;
            //            pSubsidio.gravado = 0;
            //            pSubsidio.cantidad = 0;
            //            pSubsidio.fechainicio = dtpPeriodoInicio.Value.Date;
            //            pSubsidio.fechafin = dtpPeriodoFin.Value.Date;
            //            pSubsidio.guardada = false;
            //            pSubsidio.tiponomina = _tipoNomina;
            //            lstValoresNomina.Add(pSubsidio);

            //            tmpPagoNomina pIsr = new tmpPagoNomina();
            //            pIsr.idtrabajador = lstValoresNomina[i].idtrabajador;
            //            pIsr.idempresa = GLOBALES.IDEMPRESA;
            //            pIsr.idconcepto = lstConceptoIsr[0].id;
            //            pIsr.noconcepto = 17;
            //            pIsr.tipoconcepto = lstConceptoIsr[0].tipoconcepto;
            //            pIsr.exento = 0;
            //            pIsr.gravado = 0;
            //            pIsr.cantidad = 0;
            //            pIsr.fechainicio = dtpPeriodoInicio.Value.Date;
            //            pIsr.fechafin = dtpPeriodoFin.Value.Date;
            //            pIsr.guardada = false;
            //            pIsr.tiponomina = _tipoNomina;
            //            lstValoresNomina.Add(pIsr);

            //            FLAGISR = false;
            //            FLAGSUBSIDIO = false;
            //        }
            //    }
            //    workerCalculo.ReportProgress(progreso, "Subsidio al empleo e ISR a Retener.");
            //}
            //workerCalculo.ReportProgress(100, "Subsidio al empleo e ISR a Retener.");
            #endregion

            #region BULK DATA COMENTADA
            //DataTable dt = new DataTable();
            //DataRow dtFila;
            //dt.Columns.Add("id", typeof(Int32));
            //dt.Columns.Add("idtrabajador", typeof(Int32));
            //dt.Columns.Add("idempresa", typeof(Int32));
            //dt.Columns.Add("idconcepto", typeof(Int32));
            //dt.Columns.Add("noconcepto", typeof(Int32));
            //dt.Columns.Add("tipoconcepto", typeof(String));
            //dt.Columns.Add("exento", typeof(Double));
            //dt.Columns.Add("gravado", typeof(Double));
            //dt.Columns.Add("cantidad", typeof(Double));
            //dt.Columns.Add("fechainicio", typeof(DateTime));
            //dt.Columns.Add("fechafin", typeof(DateTime));
            //dt.Columns.Add("guardada", typeof(Boolean));
            //dt.Columns.Add("tiponomina", typeof(Int32));

            //contadorNomina = lstValoresNomina.Count;
            //for (int i = 0; i < lstValoresNomina.Count; i++)
            //{
            //    progreso = (i * 100) / contadorNomina;
            //    workerCalculo.ReportProgress(progreso, "BulkData");
            //    dtFila = dt.NewRow();
            //    dtFila["id"] = i + 1;
            //    dtFila["idtrabajador"] = lstValoresNomina[i].idtrabajador;
            //    dtFila["idempresa"] = lstValoresNomina[i].idempresa;
            //    dtFila["idconcepto"] = lstValoresNomina[i].idconcepto;
            //    dtFila["noconcepto"] = lstValoresNomina[i].noconcepto;
            //    dtFila["tipoconcepto"] = lstValoresNomina[i].tipoconcepto;
            //    dtFila["exento"] = lstValoresNomina[i].exento;
            //    dtFila["gravado"] = lstValoresNomina[i].gravado;
            //    dtFila["cantidad"] = lstValoresNomina[i].cantidad;
            //    dtFila["fechainicio"] = lstValoresNomina[i].fechainicio;
            //    dtFila["fechafin"] = lstValoresNomina[i].fechafin;
            //    dtFila["guardada"] = lstValoresNomina[i].guardada;
            //    dtFila["tiponomina"] = lstValoresNomina[i].tiponomina;
            //    dt.Rows.Add(dtFila);
            //}
            //workerCalculo.ReportProgress(100, "BulkData");
            //bulk = new SqlBulkCopy(cnx);
            //nh.bulkCommand = bulk;

            //try
            //{
            //    cnx.Open();
            //    nh.bulkNomina(dt, "tmpPagoNomina");
            //    cnx.Close();
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: \r\n \r\n" + error.Message + "\r\n \r\n Error Bulk Nomina.", "Error");
            //}
            #endregion

            #region MOSTRAR DATOS EN EL GRID COMENTADA
            //contadorGrid = dgvEmpleados.Rows.Count;
            //contador = 0;
            //foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            //{
            //    progreso = (contador * 100) / contadorGrid;
            //    workerCalculo.ReportProgress(progreso, "Mostrar datos");
            //    contador++;
            //    if ((bool)fila.Cells["seleccion"].Value)
            //    {
            //        for (int i = 0; i < lstValoresNomina.Count; i++)
            //        {
            //            if ((int)fila.Cells["idtrabajador"].Value == lstValoresNomina[i].idtrabajador)
            //            {
            //                switch (lstValoresNomina[i].noconcepto)
            //                {
            //                    case 1:
            //                        fila.Cells["sueldo"].Value = lstValoresNomina[i].cantidad;
            //                        break;
            //                    case 2:
            //                        fila.Cells["horas"].Value = lstValoresNomina[i].cantidad;
            //                        break;
            //                    case 3:
            //                        fila.Cells["asistencia"].Value = lstValoresNomina[i].cantidad;
            //                        break;
            //                    case 5:
            //                        fila.Cells["puntualidad"].Value = lstValoresNomina[i].cantidad;
            //                        break;
            //                    case 6:
            //                        fila.Cells["despensa"].Value = lstValoresNomina[i].cantidad;
            //                        break;
            //                }
            //            }
            //        }
            //    }
            //}
            //workerCalculo.ReportProgress(100, "Mostrar datos");
            #endregion

            #region NETOS NEGATIVOS COMENTADA
            //cnx = new SqlConnection(cdn);
            //cmd = new SqlCommand();
            //cmd.Connection = cnx;

            //nh = new CalculoNomina.Core.NominaHelper();
            //nh.Command = cmd;
            //List<CalculoNomina.Core.NetosNegativos> lstNetos = new List<CalculoNomina.Core.NetosNegativos>();
            //try
            //{
            //    cnx.Open();
            //    lstNetos = nh.obtenerNetosNegativos(GLOBALES.IDEMPRESA, dtpPeriodoInicio.Value.Date, dtpPeriodoFin.Value.Date);
            //    cnx.Close();
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: Lista de Netos. \r\n \r\n" + error.Message, "Error");
            //}
            //List<CalculoNomina.Core.NetosNegativos> lstPercepcion = new List<CalculoNomina.Core.NetosNegativos>();
            //List<CalculoNomina.Core.NetosNegativos> lstDeduccion = new List<CalculoNomina.Core.NetosNegativos>();
            //lstPercepcion = lstNetos.Where(n => n.tipoconcepto == "P").ToList();
            //lstDeduccion = lstNetos.Where(n => n.tipoconcepto == "D").ToList();
            //decimal percepciones = 0, deducciones = 0;
            //decimal total = 0;

            //try
            //{
            //    int contadorNetosNegativos = 0;
            //    string linea1 = "";
            //    string noEmpleado = "", nombreCompleto = "";
            //    using (StreamWriter sw = new StreamWriter(@"C:\Temp\NetosNegativos.txt"))
            //    {
            //        linea1 = "Periodo: " + dtpPeriodoInicio.Value.ToShortDateString() + " al " + dtpPeriodoFin.Value.ToShortDateString();
            //        sw.WriteLine(linea1);
            //        foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            //        {
            //            for (int i = 0; i < lstPercepcion.Count; i++)
            //            {
            //                noEmpleado = lstPercepcion[i].noempleado;
            //                nombreCompleto = lstPercepcion[i].nombrecompleto;
            //                if (int.Parse(fila.Cells["idtrabajador"].Value.ToString()) == lstPercepcion[i].idtrabajador)
            //                {
            //                    percepciones += lstPercepcion[i].cantidad;
            //                }
            //            }
            //            for (int i = 0; i < lstDeduccion.Count; i++)
            //            {
            //                if (int.Parse(fila.Cells["idtrabajador"].Value.ToString()) == lstDeduccion[i].idtrabajador)
            //                {
            //                    deducciones += lstDeduccion[i].cantidad;
            //                }
            //            }
            //            total = percepciones - deducciones;
            //            if (total < 0)
            //            {
            //                contadorNetosNegativos++;
            //                linea1 = noEmpleado + ", " + nombreCompleto + ", Cantidad Neta Negativa: " + total.ToString();
            //                sw.WriteLine(linea1);
            //                total = 0;
            //                percepciones = 0;
            //                deducciones = 0;
            //            }
            //        }
            //        sw.WriteLine("TOTAL CANTIDADES NEGATIVAS: " + contadorNetosNegativos.ToString());
            //    }
            //    if (contadorNetosNegativos != 0)
            //        MessageBox.Show("CANTIDADES NEGATIVAS. VERIFIQUE ARCHIVO EN C:\\Temp\\NetosNegativos.txt", "Información");
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            //}
            #endregion
        }

        private void BulkData(List<CalculoNomina.Core.tmpPagoNomina> lstValores)
        {
            #region BULK DATA
            DataTable dt = new DataTable();
            DataRow dtFila;
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("idtrabajador", typeof(Int32));
            dt.Columns.Add("idempresa", typeof(Int32));
            dt.Columns.Add("idconcepto", typeof(Int32));
            dt.Columns.Add("noconcepto", typeof(Int32));
            dt.Columns.Add("tipoconcepto", typeof(String));
            dt.Columns.Add("exento", typeof(Double));
            dt.Columns.Add("gravado", typeof(Double));
            dt.Columns.Add("cantidad", typeof(Double));
            dt.Columns.Add("fechainicio", typeof(DateTime));
            dt.Columns.Add("fechafin", typeof(DateTime));
            dt.Columns.Add("guardada", typeof(Boolean));
            dt.Columns.Add("tiponomina", typeof(Int32));
            dt.Columns.Add("modificado", typeof(Boolean));
            
            int contadorNomina = lstValores.Count;
            int progreso = 0;
            for (int i = 0; i < lstValores.Count; i++)
            {
                progreso = (i * 100) / contadorNomina;
                workerCalculo.ReportProgress(progreso, "BulkData");
                dtFila = dt.NewRow();
                dtFila["id"] = i + 1;
                dtFila["idtrabajador"] = lstValores[i].idtrabajador;
                dtFila["idempresa"] = lstValores[i].idempresa;
                dtFila["idconcepto"] = lstValores[i].idconcepto;
                dtFila["noconcepto"] = lstValores[i].noconcepto;
                dtFila["tipoconcepto"] = lstValores[i].tipoconcepto;
                dtFila["exento"] = lstValores[i].exento;
                dtFila["gravado"] = lstValores[i].gravado;
                dtFila["cantidad"] = lstValores[i].cantidad;
                dtFila["fechainicio"] = lstValores[i].fechainicio;
                dtFila["fechafin"] = lstValores[i].fechafin;
                dtFila["guardada"] = lstValores[i].guardada;
                dtFila["tiponomina"] = lstValores[i].tiponomina;
                dtFila["modificado"] = lstValores[i].modificado;
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
            pn.fechainicio = dtpPeriodoInicio.Value.Date;
            pn.fechafin = dtpPeriodoFin.Value.Date;

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
            pn.fechainicio = dtpPeriodoInicio.Value.Date;
            pn.fechafin = dtpPeriodoFin.Value.Date;
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

            fh = new Faltas.Core.FaltasHelper();
            fh.Command = cmd;

            //ih = new Incapacidad.Core.IncapacidadHelper();
            //ih.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = inicio;
            pn.fechafin = fin;

            Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
            falta.idempresa = GLOBALES.IDEMPRESA;
            falta.fechainicio = inicio;
            falta.fechafin = fin;

            //Incapacidad.Core.Incapacidades incapacidad = new Incapacidad.Core.Incapacidades();
            //incapacidad.idempresa = GLOBALES.IDEMPRESA;
            //incapacidad.fechainicio = inicio;
            //incapacidad.fechafin = fin;

            List<CalculoNomina.Core.tmpPagoNomina> lstPreNomina = new List<CalculoNomina.Core.tmpPagoNomina>();
            List<Faltas.Core.Faltas> lstFaltas = new List<Faltas.Core.Faltas>();
            List<Incapacidad.Core.Incapacidades> lstIncapacidad = new List<Incapacidad.Core.Incapacidades>();
            try
            {
                cnx.Open();
                lstPreNomina = nh.obtenerPreNomina(pn);
                lstFaltas = fh.obtenerFaltas(falta);
                //lstIncapacidad = ih.obtenerIncapacidades(incapacidad);
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
                        switch (lstPreNomina[i].noconcepto)
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

            foreach (DataGridViewRow fila in dgvFaltas.Rows)
            {
                for (int i = 0; i < lstFaltas.Count; i++)
                {
                    if ((int)fila.Cells["idtrabajadorfalta"].Value == lstFaltas[i].idtrabajador)
                    {
                        foreach (DataGridViewColumn columna in dgvFaltas.Columns)
                        {
                            if (columna.Name == lstFaltas[i].fecha.ToShortDateString())
                            {
                                FLAGCARGA = true;
                                fila.Cells[columna.Name].Value = lstFaltas[i].faltas;
                            }
                        }
                        //fila.Cells["faltas"].Value = lstFaltas[i].faltas;
                    }
                }

                //for (int i = 0; i < lstIncapacidad.Count; i++)
                //{
                //    if ((int)fila.Cells["idtrabajadorfalta"].Value == lstIncapacidad[i].idtrabajador)
                //    {
                //        fila.Cells["incapacidad"].Value = lstIncapacidad[i].diastomados;
                //    }
                //}
            }
            toolPrenomina.Enabled = false;
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

            #region ASIGNACION DE DATOS DEL GRIDVIEW FALTAS
            if (_periodo == 7)
            {
                DateTime dt = dtpPeriodoInicio.Value;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                for (int i = 0; i <= 6; i++)
                {
                    dgvFaltas.Columns.Add(dt.AddDays(i).ToShortDateString(), dt.AddDays(i).ToShortDateString());
                }
            }
            else
            {
                if (dtpPeriodoInicio.Value.Day <= 15)
                {
                    dtpPeriodoInicio.Value = new DateTime(dtpPeriodoInicio.Value.Year, dtpPeriodoInicio.Value.Month, 1);
                    for (int i = 0; i < 15; i++)
                        dgvFaltas.Columns.Add(dtpPeriodoInicio.Value.AddDays(i).ToShortDateString(), dtpPeriodoInicio.Value.AddDays(i).ToShortDateString());
                }
                else
                {
                    int diasMes = DateTime.DaysInMonth(dtpPeriodoInicio.Value.Year, dtpPeriodoInicio.Value.Month);
                    dtpPeriodoInicio.Value = new DateTime(dtpPeriodoInicio.Value.Year, dtpPeriodoInicio.Value.Month, 16);
                    for (int i = 0; i < (diasMes - 15); i++)
                        dgvFaltas.Columns.Add(dtpPeriodoInicio.Value.AddDays(i).ToShortDateString(), dtpPeriodoInicio.Value.AddDays(i).ToShortDateString());
                }
            }
            dgvFaltas.Columns["idtrabajadorfalta"].DataPropertyName = "idtrabajador";
            dgvFaltas.Columns["iddepartamentofalta"].DataPropertyName = "iddepartamento";
            dgvFaltas.Columns["idpuestofalta"].DataPropertyName = "idpuesto";
            dgvFaltas.Columns["noempleadofalta"].DataPropertyName = "noempleado";
            dgvFaltas.Columns["nombrefalta"].DataPropertyName = "nombres";
            dgvFaltas.Columns["paternofalta"].DataPropertyName = "paterno";
            dgvFaltas.Columns["maternofalta"].DataPropertyName = "materno";
            #endregion

            #region LISTADO DE EMPLEADOS GRID
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            
            lstEmpleadosNomina = new List<CalculoNomina.Core.DatosEmpleado>();
            lstEmpleadosFaltaIncapacidad = new List<CalculoNomina.Core.DatosFaltaIncapacidad>();
            
            try
            {
                cnx.Open();
                if (_tipoNomina == GLOBALES.NORMAL)
                {
                    lstEmpleadosNomina = nh.obtenerDatosEmpleado(GLOBALES.IDEMPRESA, GLOBALES.ACTIVO);
                    lstEmpleadosFaltaIncapacidad = nh.obtenerDatosFaltaInc(GLOBALES.IDEMPRESA, GLOBALES.ACTIVO);
                }

                if (_tipoNomina == GLOBALES.ESPECIAL)
                {
                    lstEmpleadosNomina = nh.obtenerDatosEmpleado(GLOBALES.IDEMPRESA, GLOBALES.INACTIVO);
                    lstEmpleadosFaltaIncapacidad = nh.obtenerDatosFaltaInc(GLOBALES.IDEMPRESA, GLOBALES.INACTIVO);
                }
                    
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            dgvEmpleados.DataSource = lstEmpleadosNomina;
            dgvFaltas.DataSource = lstEmpleadosFaltaIncapacidad;

            for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
                dgvEmpleados.AutoResizeColumn(i);

            for (int i = 1; i < dgvFaltas.Columns.Count; i++)
                dgvFaltas.AutoResizeColumn(i);
           
            #endregion
        }

        private void fechasColumnas()
        {
            #region DISEÑO DEL GRIDVIEW FALTAS
            
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
            pn.fechainicio = dtpPeriodoInicio.Value.Date;
            pn.fechafin = dtpPeriodoFin.Value.Date;
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
                    nh.stpAutorizaNomina(GLOBALES.IDEMPRESA, dtpPeriodoInicio.Value.Date, dtpPeriodoFin.Value.Date, GLOBALES.IDUSUARIO);
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

        private void toolNoEmpleado_Click(object sender, EventArgs e)
        {
            frmFiltroNomina fn = new frmFiltroNomina();
            fn._filtro = 2;
            fn._tipoNomina = _tipoNomina;
            fn.OnFiltro += fn_OnFiltro;
            fn.ShowDialog();
        }

        private void workHoras_DoWork(object sender, DoWorkEventArgs e)
        {
            int totalRegistroDt = dt.Rows.Count;
            int indice = 0;
            nombreEmpresa = dt.Columns[1].ColumnName;
            if (DateTime.Parse(dt.Rows[2][5].ToString()) == dtpPeriodoInicio.Value.Date)
            {
                for (int i = 2; i < dt.Rows.Count; i++)
                {
                    workHoras.ReportProgress(indice, "Horas Extras");
                    foreach (DataGridViewRow fila in dgvEmpleados.Rows)
                    {
                        if (dt.Rows[i][0].ToString() == fila.Cells["noempleado"].Value.ToString())
                        {
                            fila.Cells["horas"].Value = dt.Rows[i][4].ToString();
                            for (int j = 0; j < lstValoresNomina.Count(); j++)
                            {
                                if (int.Parse(fila.Cells["idtrabajador"].Value.ToString()) == lstValoresNomina[j].idtrabajador &&
                                    lstValoresNomina[j].idconcepto == 2)
                                {
                                    nh = new CalculoNomina.Core.NominaHelper();
                                    nh.Command = cmd;
                                    lstValoresNomina[j].cantidad = double.Parse(fila.Cells["horas"].Value.ToString());
                                    lstValoresNomina[j].gravado = double.Parse(fila.Cells["horas"].Value.ToString()) - lstValoresNomina[j].exento;
                                    CalculoNomina.Core.tmpPagoNomina hora = new CalculoNomina.Core.tmpPagoNomina();
                                    hora.idempresa = GLOBALES.IDEMPRESA;
                                    hora.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                                    hora.idconcepto = 2;
                                    hora.fechainicio = dtpPeriodoInicio.Value.Date;
                                    hora.fechafin = dtpPeriodoFin.Value.Date;
                                    hora.cantidad = lstValoresNomina[j].cantidad;
                                    hora.gravado = lstValoresNomina[j].gravado;
                                    hora.modificado = true;
                                    try
                                    {
                                        cnx.Open();
                                        nh.actualizaHorasExtrasDespensa(hora);
                                        cnx.Close();
                                    }
                                    catch (Exception error)
                                    {
                                        MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                                    }
                                }
                            }
                        }
                    }
                    indice++;
                }
                cnx.Dispose();
            }
            else
            {
                if (workHoras.IsBusy)
                {
                    workHoras.CancelAsync();
                    MessageBox.Show("La fecha es diferente al periodo a calcular.", "Error");
                }
            }
        }

        private void workHoras_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolPorcentaje.Text = e.ProgressPercentage.ToString() + "%";
            toolEtapa.Text = e.UserState.ToString();
        }

        private void workHoras_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolPorcentaje.Text = "Completado.";
        }

        private void toolCaratula_Click(object sender, EventArgs e)
        {
            frmVisorReportes vr = new frmVisorReportes();
            vr._noReporte = 0;
            vr._inicioPeriodo = dtpPeriodoInicio.Value.Date;
            vr._finPeriodo = dtpPeriodoFin.Value.Date;
            vr.Show();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportes r = new frmReportes();
            r.OnReporte += r_OnReporte;
            r._inicio = dtpPeriodoInicio.Value.Date;
            r._fin = dtpPeriodoFin.Value.Date;
            r._ReportePreNomina = true;
            r._noReporte = 1;
            r.Show();
        }

        private void toolReporteDepto_Click(object sender, EventArgs e)
        {
            frmReportes r = new frmReportes();
            r.OnReporte += r_OnReporte;
            r._inicio = dtpPeriodoInicio.Value.Date;
            r._fin = dtpPeriodoFin.Value.Date;
            r._ReportePreNomina = true;
            r._noReporte = 2;
            r.Show();
        }

        private void dgvFaltas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (FLAGCARGA)
            {
                FLAGCARGA = false;
                return;
            }

            if (dgvFaltas.Columns[e.ColumnIndex].Name.ToString() == "")
                return;
            if (dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value.ToString() == "" ||
                dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value.ToString() == "0")
                return;

            try
            {
                int.Parse(dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value.ToString());
            }
            catch
            {
                MessageBox.Show("Dato incorrecto. SOLO NUMEROS.", "Información");
                dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value = "";
                return;
            }

            int existe = 0;
            int faltas = int.Parse(dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value.ToString());
            DateTime fechaColumna = DateTime.Parse(dgvFaltas.Columns[e.ColumnIndex].Name.ToString());

            if (faltas > 1 || faltas < 0)
            {
                MessageBox.Show("Solo se admite el valor de 1 falta por dia.", "Información");
                dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value = "";
                return;
            }

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Incidencias.Core.IncidenciasHelper ih = new Incidencias.Core.IncidenciasHelper();
            ih.Command = cmd;

            try
            {
                cnx.Open();
                existe = (int)ih.existeIncidenciaEnFalta(int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["idtrabajadorfalta"].Value.ToString()), fechaColumna.Date);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n No se pudo verificar la incapacidad, verifique.", "Error");
                dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value = "";
                cnx.Dispose();
            }

            if (existe == 0)
            {
                fh = new Faltas.Core.FaltasHelper();
                fh.Command = cmd;
                Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
                falta.idtrabajador = int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["idtrabajadorfalta"].Value.ToString());
                falta.idempresa = GLOBALES.IDEMPRESA;
                falta.periodo = _periodo;
                falta.faltas = int.Parse(dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value.ToString());
                falta.fechainicio = dtpPeriodoInicio.Value.Date;
                falta.fechafin = dtpPeriodoFin.Value.Date;
                falta.fecha = DateTime.Parse(dgvFaltas.Columns[e.ColumnIndex].Name.ToString());

                try
                {
                    cnx.Open();
                    fh.insertaFalta(falta);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n Se ingreso un valor incorrecto, verifique.", "Error");
                    dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value = "";
                    cnx.Dispose();
                }
            }
            else
            {
                MessageBox.Show("La falta ingresada, se empalma con una incapacidad del trabajador.", "Error");
                dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value = "";
            }

            #region CODIGO COMENTADO
            //if (dgvFaltas.Columns[e.ColumnIndex].Name == "falta")
            //{
            //    if (int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["falta"].Value.ToString()) > _periodo)
            //    {
            //        MessageBox.Show("La falta ingresada es mayor al periodo. Verifique", "Error");
            //        dgvFaltas.Rows[e.RowIndex].Cells["falta"].Value = 0;
            //        return;
            //    }

            //    if (dgvFaltas.Rows[e.RowIndex].Cells["falta"].Value.ToString() != "0")
            //    {
            //        if (dgvFaltas.Rows[e.RowIndex].Cells["incapacidad"].Value.ToString() != "0")
            //        {
            //            borraIncapacidad(e.ColumnIndex + 1, e.RowIndex);
            //            dgvFaltas.Rows[e.RowIndex].Cells["incapacidad"].Value = 0;
            //        }

            //        cnx = new SqlConnection(cdn);
            //        cmd = new SqlCommand();
            //        cmd.Connection = cnx;
            //        fh = new Faltas.Core.FaltasHelper();
            //        fh.Command = cmd;
            //        Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
            //        falta.idtrabajador = int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["idtrabajadorfalta"].Value.ToString());
            //        falta.idempresa = GLOBALES.IDEMPRESA;
            //        falta.periodo = _periodo;
            //        falta.faltas = int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["falta"].Value.ToString());
            //        falta.fechainicio = dtpPeriodoInicio.Value.Date;
            //        falta.fechafin = dtpPeriodoFin.Value.Date;

            //        try
            //        {
            //            cnx.Open();
            //            fh.insertaFalta(falta);
            //            cnx.Close();
            //            cnx.Dispose();
            //        }
            //        catch (Exception error)
            //        {
            //            MessageBox.Show("Error: \r\n \r\n Se ingreso un valor incorrecto, verifique.", "Error");
            //            dgvFaltas.Rows[e.RowIndex].Cells["falta"].Value = 0;
            //            cnx.Dispose();
            //        }
            //    }
            //    else
            //    {
            //        borraFalta(e.ColumnIndex, e.RowIndex);
            //    }
            //}

            //if (dgvFaltas.Columns[e.ColumnIndex].Name == "incapacidad")
            //{
            //    if (int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["incapacidad"].Value.ToString()) > _periodo)
            //    {
            //        MessageBox.Show("La incapacidad ingresada es mayor al periodo. Verifique", "Error");
            //        dgvFaltas.Rows[e.RowIndex].Cells["incapacidad"].Value = 0;
            //        return;
            //    }

            //    if (dgvFaltas.Rows[e.RowIndex].Cells["incapacidad"].Value.ToString() != "0")
            //    {
            //        if (dgvFaltas.Rows[e.RowIndex].Cells["falta"].Value.ToString() != "0")
            //        {
            //            borraFalta(e.ColumnIndex - 1, e.RowIndex);
            //            dgvFaltas.Rows[e.RowIndex].Cells["falta"].Value = 0;
            //        }

            //        cnx = new SqlConnection(cdn);
            //        cmd = new SqlCommand();
            //        cmd.Connection = cnx;
            //        ih = new Incapacidad.Core.IncapacidadHelper();
            //        ih.Command = cmd;
            //        Incapacidad.Core.Incapacidades incapacidad = new Incapacidad.Core.Incapacidades();
            //        incapacidad.idtrabajador = int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["idtrabajadorfalta"].Value.ToString());
            //        incapacidad.idempresa = GLOBALES.IDEMPRESA;
            //        incapacidad.diasincapacidad = 0;
            //        incapacidad.diastomados = int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["incapacidad"].Value.ToString());
            //        incapacidad.diasrestantes = 0;
            //        incapacidad.diasapagar = 0;
            //        incapacidad.tipo = 0;
            //        incapacidad.aplicada = 1;
            //        incapacidad.consecutiva = 1;
            //        incapacidad.fechainicio = dtpPeriodoInicio.Value.Date;
            //        incapacidad.fechafin = dtpPeriodoFin.Value.Date;

            //        try
            //        {
            //            cnx.Open();
            //            ih.insertaIncapacidad(incapacidad);
            //            cnx.Close();
            //            cnx.Dispose();
            //        }                    
            //        catch (Exception error)
            //        {
            //            MessageBox.Show("Error: \r\n \r\n Se ingreso un valor incorrecto, verifique.", "Error");
            //            dgvFaltas.Rows[e.RowIndex].Cells["incapacidad"].Value = 0;
            //            cnx.Dispose();
            //        }
            //    }
            //    else
            //    {
            //        borraIncapacidad(e.ColumnIndex, e.RowIndex);
            //    }
            //}
            #endregion
        }

        private void dgvFaltas_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //if (dgvFaltas.IsCurrentCellDirty)
            //    dgvFaltas.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //else
            //    dgvFaltas.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvFaltas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && dgvFaltas.CurrentCell.Selected && dgvFaltas.CurrentCell.ReadOnly != true)
            {
                int columna = dgvFaltas.CurrentCell.ColumnIndex;
                int fila = dgvFaltas.CurrentCell.RowIndex;
                //if (dgvFaltas.Columns[columna].Name == "falta")
                borraFalta(fila, columna);
                //if (dgvFaltas.Columns[columna].Name == "incapacidad")
                //    borraIncapacidad(columna, fila);
                dgvFaltas.CurrentCell.Value = "";
            } 
        }

        private void borraFalta(int fila, int columna)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            fh = new Faltas.Core.FaltasHelper();
            fh.Command = cmd;
            Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
            falta.idtrabajador = int.Parse(dgvFaltas.Rows[fila].Cells["idtrabajadorfalta"].Value.ToString());
            falta.fechainicio = dtpPeriodoInicio.Value.Date;
            falta.fechafin = dtpPeriodoFin.Value.Date;
            falta.fecha = DateTime.Parse(dgvFaltas.Columns[columna].Name.ToString());
           

            try
            {
                cnx.Open();
                fh.eliminaFaltaExistente(falta);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                cnx.Dispose();
            }

            //if (dgvFaltas.Columns[columna].Name == "falta"){}
        }

        private void borraIncapacidad(int columna, int fila)
        {
            if (dgvFaltas.Columns[columna].Name == "incapacidad")
            {
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                ih = new Incapacidad.Core.IncapacidadHelper();
                ih.Command = cmd;
                
                try
                {
                    cnx.Open();
                    ih.eliminaIncapadidad(int.Parse(dgvFaltas.Rows[fila].Cells["idtrabajadorfalta"].Value.ToString()), dtpPeriodoInicio.Value.Date, dtpPeriodoFin.Value.Date);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                    cnx.Dispose();
                }
            }
        }

        private void dgvEmpleados_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (lstValoresNomina == null)
                return;

            cnx = new SqlConnection(cdn);
            cmd.Connection = cnx;

            if (dgvEmpleados.Columns[e.ColumnIndex].Name == "horas")
            {
                for (int i = 0; i < lstValoresNomina.Count(); i++)
                {
                    if (int.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["idtrabajador"].Value.ToString()) == lstValoresNomina[i].idtrabajador && lstValoresNomina[i].noconcepto == 2)
                    {
                        nh = new CalculoNomina.Core.NominaHelper();
                        nh.Command = cmd;
                        lstValoresNomina[i].cantidad = double.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["horas"].Value.ToString());
                        if (lstValoresNomina[i].cantidad <= lstValoresNomina[i].exento)
                        {
                            lstValoresNomina[i].exento = lstValoresNomina[i].cantidad;
                            lstValoresNomina[i].gravado = 0;
                        }
                        else
                        {
                            lstValoresNomina[i].gravado = double.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["horas"].Value.ToString()) - lstValoresNomina[i].exento;
                        }
                        
                        CalculoNomina.Core.tmpPagoNomina hora = new CalculoNomina.Core.tmpPagoNomina();
                        hora.idempresa = GLOBALES.IDEMPRESA;
                        hora.idtrabajador = int.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["idtrabajador"].Value.ToString());
                        hora.noconcepto = 2; //CONCEPTO HORAS EXTRAS DOBLES
                        hora.fechainicio = dtpPeriodoInicio.Value.Date;
                        hora.fechafin = dtpPeriodoFin.Value.Date;
                        hora.cantidad = lstValoresNomina[i].cantidad;
                        hora.exento = lstValoresNomina[i].exento;
                        hora.gravado = lstValoresNomina[i].gravado;
                        hora.modificado = true;
                        try
                        {
                            cnx.Open();
                            nh.actualizaHorasExtrasDespensa(hora);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                        }
                    }
                }
            }

            if (dgvEmpleados.Columns[e.ColumnIndex].Name == "despensa")
            {
                for (int i = 0; i < lstValoresNomina.Count(); i++)
                {
                    if (int.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["idtrabajador"].Value.ToString()) == lstValoresNomina[i].idtrabajador && lstValoresNomina[i].noconcepto == 6)
                    {
                        nh = new CalculoNomina.Core.NominaHelper();
                        nh.Command = cmd;
                        lstValoresNomina[i].cantidad = double.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["despensa"].Value.ToString());
                        lstValoresNomina[i].gravado = double.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["despensa"].Value.ToString());
                        CalculoNomina.Core.tmpPagoNomina despensa = new CalculoNomina.Core.tmpPagoNomina();
                        despensa.idempresa = GLOBALES.IDEMPRESA;
                        despensa.idtrabajador = int.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["idtrabajador"].Value.ToString());
                        despensa.noconcepto = 6; //CONCEPTO DESPENSA
                        despensa.fechainicio = dtpPeriodoInicio.Value.Date;
                        despensa.fechafin = dtpPeriodoFin.Value.Date;
                        despensa.cantidad = lstValoresNomina[i].cantidad;
                        despensa.gravado = lstValoresNomina[i].gravado;
                        despensa.modificado = true;
                        try
                        {
                            cnx.Open();
                            nh.actualizaHorasExtrasDespensa(despensa);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                        }
                    }
                }
            }
        }

        private void toolTabular_Click(object sender, EventArgs e)
        {
            frmReportes r = new frmReportes();
            r.OnReporte += r_OnReporte;
            r._inicio = dtpPeriodoInicio.Value.Date;
            r._fin = dtpPeriodoFin.Value.Date;
            r._noReporte = 6;
            r._ReportePreNomina = true;
            r.Show();
        }

        void r_OnReporte(string netocero, string orden, int noreporte)
        {
            NetoCero = netocero;
            Orden = orden;

            if (noreporte != 6)
            {
                frmVisorReportes vr = new frmVisorReportes();
                vr._noReporte = noreporte;
                vr._inicioPeriodo = dtpPeriodoInicio.Value.Date;
                vr._finPeriodo = dtpPeriodoFin.Value.Date;
                vr._orden = orden;
                vr._netoCero = netocero;
                vr._noReporte = noreporte;
                vr.Show();
            }
            else
            {
                workExcel.RunWorkerAsync();
            }
        }

        private void workExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = dtpPeriodoInicio.Value.Date;
            pn.fechafin = dtpPeriodoFin.Value.Date;

            DataTable dt = new DataTable();
            try
            {
                cnx.Open();
                dt = nh.obtenerPreNominaTabular(pn, NetoCero, Orden);
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
            decimal totalSueldo = 0, totalHoras = 0, totalAsistencia = 0, totalPuntualidad = 0, totalDespensa = 0,
                totalPrimaVacacional = 0, totalVacaciones = 0, totalAguinaldo = 0, totalIsr = 0, totalInfonavitPorcentaje = 0, totalInfonavitVSM = 0, totalInfonavitFijo = 0,
                totalResponsabilidades = 0, totalPrestamoEmpresa = 0, totalIsrRetenido = 0, totalPension = 0, totalDescTrab = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                progreso = (contador * 100) / contadorDt;
                workExcel.ReportProgress(progreso, "Reporte a Excel");
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
                    //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 1];
                    //rng.Font.Bold = true;
                    //excel.Cells[iFil, 1] = dt.Rows[i][5];

                    //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 9];
                    //rng.NumberFormat = "#,##0.00";
                    //rng.Font.Bold = true;
                    //excel.Cells[iFil, 10] = totalPercepciones.ToString();

                    //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 15];
                    //rng.NumberFormat = "#,##0.00";
                    //rng.Font.Bold = true;
                    //excel.Cells[iFil, 16] = totalDeducciones.ToString();
                    //iFil++;

                    #region AGRUPADO POR DEPARTAMENTO
                    //if (dt.Rows[i][5].ToString() == dt.Rows[i + 1][5].ToString())
                    //    for (int j = 6; j < dt.Columns.Count; j++)
                    //    {
                    //        excel.Cells[iFil, iCol] = dt.Rows[i][j];
                    //        iCol++;
                    //    }
                    //else
                    //{
                    //    for (int j = 6; j < dt.Columns.Count; j++)
                    //    {
                    //        excel.Cells[iFil, iCol] = dt.Rows[i][j];
                    //        iCol++;
                    //    }
                    //    iFil++;
                    //    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 1];
                    //    rng.Font.Bold = true;
                    //    excel.Cells[iFil, 1] = dt.Rows[i][5];

                    //    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 9];
                    //    rng.NumberFormat = "#,##0.00";
                    //    rng.Font.Bold = true;
                    //    excel.Cells[iFil, 9] = totalPercepciones.ToString();

                    //    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 15];
                    //    rng.NumberFormat = "#,##0.00";
                    //    rng.Font.Bold = true;
                    //    excel.Cells[iFil, 15] = totalDeducciones.ToString();
                    //    iFil++;

                    //    totalPercepciones = 0;
                    //    totalDeducciones = 0;
                    //}
                    #endregion
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
                    //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 1];
                    //rng.Font.Bold = true;
                    //excel.Cells[iFil, 1] = dt.Rows[i][5];

                    //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 9];
                    //rng.NumberFormat = "#,##0.00";
                    //rng.Font.Bold = true;
                    //excel.Cells[iFil, 9] = totalPercepciones.ToString();

                    //rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 15];
                    //rng.NumberFormat = "#,##0.00";
                    //rng.Font.Bold = true;
                    //excel.Cells[iFil, 15] = totalDeducciones.ToString();
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

            workExcel.ReportProgress(100, "Reporte a Excel");
        }

        private void workExcel_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolPorcentaje.Text = e.ProgressPercentage.ToString() + "%";
            toolEtapa.Text = e.UserState.ToString();
        }

        private void workExcel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolPorcentaje.Text = "Completado.";
        }

        //BOTON DE CARGA DE FALTAS
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            
            fh = new Faltas.Core.FaltasHelper();
            fh.Command = cmd;

            Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
            falta.idempresa = GLOBALES.IDEMPRESA;
            falta.fechainicio = dtpPeriodoInicio.Value.Date;
            falta.fechafin = dtpPeriodoFin.Value.Date;

            List<Faltas.Core.Faltas> lstFaltas = new List<Faltas.Core.Faltas>();

            try
            {
                cnx.Open();
                lstFaltas = fh.obtenerFaltas(falta);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }


            foreach (DataGridViewRow fila in dgvFaltas.Rows)
            {
                for (int i = 0; i < lstFaltas.Count; i++)
                {
                    if (int.Parse(fila.Cells["idtrabajadorfalta"].Value.ToString()) == lstFaltas[i].idtrabajador)
                    {
                        foreach (DataGridViewColumn columna in dgvFaltas.Columns)
                        {
                            if (columna.Name == lstFaltas[i].fecha.ToShortDateString())
                            {
                                FLAGCARGA = true;
                                fila.Cells[columna.Name].Value = lstFaltas[i].faltas;
                            }
                        }
                    }
                }
            }
        }

        private void toolOrdenEmpleado_Click(object sender, EventArgs e)
        {
            var ordenEmpleado = from f in lstEmpleadosNomina orderby f.noempleado ascending select f;
            dgvEmpleados.DataSource = ordenEmpleado.ToList();
            for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
            {
                dgvEmpleados.AutoResizeColumn(i);
            }

            var ordenEmpleadoFalta = from f in lstEmpleadosFaltaIncapacidad orderby f.noempleado ascending select f;
            dgvFaltas.DataSource = ordenEmpleadoFalta.ToList();
            for (int i = 1; i < dgvFaltas.Columns.Count; i++)
            {
                dgvFaltas.AutoResizeColumn(i);
            }
        }

        private void toolOrdenNombre_Click(object sender, EventArgs e)
        {
            var ordenNombre = from f in lstEmpleadosNomina orderby f.nombres ascending select f;
            dgvEmpleados.DataSource = ordenNombre.ToList();
            for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
            {
                dgvEmpleados.AutoResizeColumn(i);
            }

            var ordenNombreFalta = from f in lstEmpleadosFaltaIncapacidad orderby f.nombres ascending select f;
            dgvFaltas.DataSource = ordenNombreFalta.ToList();
            for (int i = 1; i < dgvFaltas.Columns.Count; i++)
            {
                dgvFaltas.AutoResizeColumn(i);
            }
        }

        private void toolOrdenPaterno_Click(object sender, EventArgs e)
        {
            var ordenPaterno = from f in lstEmpleadosNomina orderby f.paterno ascending select f;
            dgvEmpleados.DataSource = ordenPaterno.ToList();
            for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
            {
                dgvEmpleados.AutoResizeColumn(i);
            }

            var ordenPaternoFalta = from f in lstEmpleadosFaltaIncapacidad orderby f.paterno ascending select f;
            dgvFaltas.DataSource = ordenPaternoFalta.ToList();
            for (int i = 1; i < dgvFaltas.Columns.Count; i++)
            {
                dgvFaltas.AutoResizeColumn(i);
            }
        }

        private void toolOrdenMaterno_Click(object sender, EventArgs e)
        {
            var ordenMaterno = from f in lstEmpleadosNomina orderby f.materno ascending select f;
            dgvEmpleados.DataSource = ordenMaterno.ToList();
            for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
            {
                dgvEmpleados.AutoResizeColumn(i);
            }

            var ordenMaternoFalta = from f in lstEmpleadosFaltaIncapacidad orderby f.materno ascending select f;
            dgvFaltas.DataSource = ordenMaternoFalta.ToList();
            for (int i = 1; i < dgvFaltas.Columns.Count; i++)
            {
                dgvFaltas.AutoResizeColumn(i);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmSobreRecibo sr = new frmSobreRecibo();
            sr._tipoNormalEspecial = _tipoNomina;
            sr._inicioPeriodo = dtpPeriodoInicio.Value.Date;
            sr._finPeriodo = dtpPeriodoFin.Value.Date;
            sr.Show();
        }
        
    }

    public class tmpPagoNomina
    {
        public int id { get; set; }
        public int idtrabajador { get; set; }
        public int idempresa { get; set; }
        public int idconcepto { get; set; }
        public int noconcepto { get; set; }
        public string tipoconcepto { get; set; }
        public double exento { get; set; }
        public double gravado { get; set; }
        public double cantidad { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public bool guardada { get; set; }
        public int tiponomina { get; set; }
        public bool modificado { get; set; }
    }
}
