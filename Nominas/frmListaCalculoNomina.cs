using System;
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
                MessageBox.Show("Error: \r\n \r\n" + error.Message,"Error");
            }

            dgvEmpleados.DataSource = lstEmpleadosNomina;

            for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
            {
                dgvEmpleados.AutoResizeColumn(i);
            }
            #endregion

            //#region FORMULA DE SUELDO
            //ch = new Conceptos.Core.ConceptosHelper();
            //ch.Command = cmd;
            //Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            //concepto.noconcepto = 1; //SUELDO        
            //string formulaSueldo = "";
            //variables = new List<string>();
            //formulas = new List<string>();
            //try
            //{
            //    cnx.Open();
            //    formulaSueldo = (string)ch.obtenerFormula(concepto);
            //    cnx.Close();
            //    variables = GLOBALES.EXTRAEVARIABLES(formulaSueldo, "[", "]");
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //}

            //for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            //{
            //    if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
            //        formulas.Add(formulaSueldo);
            //}
            //#endregion

            //#region CALCULO DE LA FORMULA DE SUELDO
            //valores = new List<string>();
            //sueldo = new List<string>();
            //for (int i = 0; i < variables.Count; i++)
            //{
            //    FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, inicio, fin, 15);
            //    valores = f.ObtenerValorVariable();
            //    for (int j = 0; j < valores.Count; j++)
            //    {
            //        formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
            //    }
            //}

            //for (int i = 0; i < formulas.Count; i++)
            //{
            //    MathParserTK.MathParser parser = new MathParserTK.MathParser();
            //    sueldo.Add(parser.Parse(formulas[i]).ToString());
            //}
            //#endregion

            //#region VACACIONES
            //vacaciones = new List<string>();
            //vh = new Vacaciones.Core.VacacionesHelper();
            //vh.Command = cmd;
            //for (int i = 0; i < lstDatosEmpleado.Count; i++)
            //{
            //    Vacaciones.Core.Vacaciones vacacion = new Vacaciones.Core.Vacaciones();
            //    vacacion.idtrabajador = lstDatosEmpleado[i].idtrabajador;
            //    vacacion.inicio = inicio;
            //    vacacion.fin = fin;
            //    try
            //    {
            //        cnx.Open();
            //        vacaciones.Add(vh.vacacionesPagadas(vacacion).ToString());
            //        cnx.Close();
            //    }
            //    catch (Exception error) {
            //        MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //        cnx.Dispose();
            //    }
            //}
            //#endregion

            //#region PRIMA VACACIONAL
            //primaVacacional = new List<object>();
            //vh = new Vacaciones.Core.VacacionesHelper();
            //vh.Command = cmd;
            //for (int i = 0; i < lstDatosEmpleado.Count; i++)
            //{
            //    Vacaciones.Core.Vacaciones vacacion = new Vacaciones.Core.Vacaciones();
            //    vacacion.idtrabajador = lstDatosEmpleado[i].idtrabajador;
            //    vacacion.inicio = inicio;
            //    vacacion.fin = fin;
            //    try {
            //        cnx.Open();
            //        primaVacacional.Add(vh.primaVacacional(vacacion));
            //        cnx.Close();
            //    }
            //    catch (Exception error)
            //    {
            //        MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //        cnx.Dispose();
            //    }
            //}
            //#endregion

            //#region DESPENSA
            //Conceptos.Core.Conceptos conceptoDespensa = new Conceptos.Core.Conceptos();
            //conceptoDespensa.noconcepto = 6; //DESPENSA        
            //string formulaDespensa = "";
            //variables = new List<string>();
            //formulas = new List<string>();
            //try
            //{
            //    cnx.Open();
            //    formulaDespensa = (string)ch.obtenerFormula(conceptoDespensa);
            //    cnx.Close();
            //    variables = GLOBALES.EXTRAEVARIABLES(formulaDespensa, "[", "]");
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //}

            //for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            //{
            //    if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
            //        formulas.Add(formulaDespensa);
            //}
            //#endregion

            //#region CALCULO DE LA FORMULA DE DESPENSA
            //despensa = new List<string>();
            //valores = new List<string>();
            //for (int i = 0; i < variables.Count; i++)
            //{
            //    FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, 15);
            //    valores = f.ObtenerValorVariable();
            //    for (int j = 0; j < valores.Count; j++)
            //    {
            //        formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
            //    }
            //}

            //for (int i = 0; i < formulas.Count; i++)
            //{
            //    MathParserTK.MathParser parser = new MathParserTK.MathParser();
            //    despensa.Add(parser.Parse(formulas[i]).ToString());
            //}
            //#endregion

            /////INICIO CALCULO DE PREMIO DE ASISTENCIA Y CALCULO DE CANTIDAD A EXENTAR
            //#region PREMIO DE ASISTENCIAS
            //Conceptos.Core.Conceptos conceptoAsistencia = new Conceptos.Core.Conceptos();
            //conceptoAsistencia.noconcepto = 3; //SUELDO        
            //string formulaAsistencia = "";
            //variables = new List<string>();
            //formulas = new List<string>();
            //try
            //{
            //    cnx.Open();
            //    formulaAsistencia = (string)ch.obtenerFormula(conceptoAsistencia);
            //    cnx.Close();
            //    variables = GLOBALES.EXTRAEVARIABLES(formulaAsistencia, "[", "]");
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //}

            //for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            //{
            //    if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
            //        formulas.Add(formulaAsistencia);
            //}
            //#endregion

            //#region CALCULO DE LA FORMULA DE ASISTENCIA
            //asistencia = new List<string>();
            //valores = new List<string>();
            //for (int i = 0; i < variables.Count; i++)
            //{
            //    FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, 15);
            //    valores = f.ObtenerValorVariable();
            //    for (int j = 0; j < valores.Count; j++)
            //    {
            //        formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
            //    }
            //}

            //for (int i = 0; i < formulas.Count; i++)
            //{
            //    MathParserTK.MathParser parser = new MathParserTK.MathParser();
            //    asistencia.Add(parser.Parse(formulas[i]).ToString());
            //}
            //#endregion

            //#region PREMIO DE ASISTENCIAS EXENTO
            //Conceptos.Core.Conceptos conceptoAsistenciaExento = new Conceptos.Core.Conceptos();
            //conceptoAsistenciaExento.noconcepto = 3; //PREMIO DE ASISTENCIA        
            //string formulaAsistenciaExento = "";
            //variables = new List<string>();
            //formulas = new List<string>();
            //try
            //{
            //    cnx.Open();
            //    formulaAsistencia = (string)ch.obtenerFormulaExento(conceptoAsistenciaExento);
            //    cnx.Close();
            //    variables = GLOBALES.EXTRAEVARIABLES(formulaAsistenciaExento, "[", "]");
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //}

            //for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            //{
            //    if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
            //        formulas.Add(formulaAsistenciaExento);
            //}
            //#endregion

            //#region CALCULO DE LA FORMULA DE ASISTENCIA EXENTA
            //asistenciaExcenta = new List<string>();
            //valores = new List<string>();
            //for (int i = 0; i < variables.Count; i++)
            //{
            //    FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, 15);
            //    valores = f.ObtenerValorVariable();
            //    for (int j = 0; j < valores.Count; j++)
            //    {
            //        formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
            //    }
            //}

            //for (int i = 0; i < formulas.Count; i++)
            //{
            //    MathParserTK.MathParser parser = new MathParserTK.MathParser();
            //    asistenciaExcenta.Add(parser.Parse(formulas[i]).ToString());
            //}
            //#endregion

            //#region CALCULO DE ASISTENCIA GRAVADA
            //asistenciaGravada = new List<string>();
            //for (int i = 0; i < asistenciaExcenta.Count; i++)
            //{
            //    asistenciaGravada.Add((double.Parse(asistencia[i]) - double.Parse(asistenciaExcenta[i])).ToString());
            //}
            //#endregion
            /////FIN CALCULO DE PREMIO DE ASISTENCIA

            /////INICIO CALCULO DE PREMIO DE PUNTUALIDAD Y CALCULO DE CANTIDAD A EXENTAR
            //#region PREMIO DE PUNTUALIDAD
            //Conceptos.Core.Conceptos conceptoPuntualidad = new Conceptos.Core.Conceptos();
            //conceptoPuntualidad.noconcepto = 5; //SUELDO        
            //string formulaPuntualidad = "";
            //variables = new List<string>();
            //formulas = new List<string>();
            //try
            //{
            //    cnx.Open();
            //    formulaPuntualidad = (string)ch.obtenerFormula(conceptoPuntualidad);
            //    cnx.Close();
            //    variables = GLOBALES.EXTRAEVARIABLES(formulaPuntualidad, "[", "]");
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //}

            //for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            //{
            //    if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
            //        formulas.Add(formulaPuntualidad);
            //}
            //#endregion

            //#region CALCULO DE LA FORMULA DE PUNTUALIDAD
            //puntualidad = new List<string>();
            //valores = new List<string>();
            //for (int i = 0; i < variables.Count; i++)
            //{
            //    FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, 15);
            //    valores = f.ObtenerValorVariable();
            //    for (int j = 0; j < valores.Count; j++)
            //    {
            //        formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
            //    }
            //}

            //for (int i = 0; i < formulas.Count; i++)
            //{
            //    MathParserTK.MathParser parser = new MathParserTK.MathParser();
            //    puntualidad.Add(parser.Parse(formulas[i]).ToString());
            //}
            //#endregion

            //#region PREMIO DE PUNTUALIDAD EXENTO
            //Conceptos.Core.Conceptos conceptoPuntualidadexenta = new Conceptos.Core.Conceptos();
            //conceptoPuntualidadexenta.noconcepto = 5; //PREMIO DE PUNTUALIDAD        
            //string formulaPuntualidadExenta = "";
            //variables = new List<string>();
            //formulas = new List<string>();
            //try
            //{
            //    cnx.Open();
            //    formulaPuntualidadExenta = (string)ch.obtenerFormulaExento(conceptoPuntualidadexenta);
            //    cnx.Close();
            //    variables = GLOBALES.EXTRAEVARIABLES(formulaPuntualidadExenta, "[", "]");
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //}

            //for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            //{
            //    if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
            //        formulas.Add(formulaPuntualidadExenta);
            //}
            //#endregion

            //#region CALCULO DE LA FORMULA DE PUNTUALIDAD EXENTA
            //puntualidadExcenta = new List<string>();
            //valores = new List<string>();
            //for (int i = 0; i < variables.Count; i++)
            //{
            //    FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, 15);
            //    valores = f.ObtenerValorVariable();
            //    for (int j = 0; j < valores.Count; j++)
            //    {
            //        formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
            //    }
            //}

            //for (int i = 0; i < formulas.Count; i++)
            //{
            //    MathParserTK.MathParser parser = new MathParserTK.MathParser();
            //    puntualidadExcenta.Add(parser.Parse(formulas[i]).ToString());
            //}
            //#endregion

            //#region CALCULO DE PUNTUALIDAD GRAVADA
            //puntualidadGravada = new List<string>();
            //for (int i = 0; i < puntualidadExcenta.Count; i++)
            //{
            //    puntualidadGravada.Add((double.Parse(puntualidad[i]) - double.Parse(puntualidadExcenta[i])).ToString());
            //}
            //#endregion
            /////FIN CALCULO DE PREMIO DE PUNTUALIDAD

            /////INICIO CALCULO DE HORAS EXTRAS DOBLES A EXENTAR
            //#region HORAS EXTRAS DOBLES
            //Conceptos.Core.Conceptos conceptoHorasExtrasDobles = new Conceptos.Core.Conceptos();
            //conceptoHorasExtrasDobles.noconcepto = 2; //HORAS EXTRAS DOBLES        
            //string formulaHorasExtrasDobles = "";
            //variables = new List<string>();
            //formulas = new List<string>();
            //try
            //{
            //    cnx.Open();
            //    formulaHorasExtrasDobles = (string)ch.obtenerFormula(conceptoHorasExtrasDobles);
            //    cnx.Close();
            //    variables = GLOBALES.EXTRAEVARIABLES(formulaHorasExtrasDobles, "[", "]");
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //}

            //for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            //{
            //    if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
            //        formulas.Add(formulaHorasExtrasDobles);
            //}
            //#endregion

            //#region CALCULO DE LA FORMULA DE HORAS EXTRAS DOBLES
            //horasExtrasDobles = new List<string>();
            //valores = new List<string>();
            //for (int i = 0; i < variables.Count; i++)
            //{
            //    FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, 15);
            //    valores = f.ObtenerValorVariable();
            //    for (int j = 0; j < valores.Count; j++)
            //    {
            //        formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
            //    }
            //}

            //for (int i = 0; i < formulas.Count; i++)
            //{
            //    MathParserTK.MathParser parser = new MathParserTK.MathParser();
            //    horasExtrasDobles.Add(parser.Parse(formulas[i]).ToString());
            //}
            //#endregion

            //#region HORAS EXTRAS DOBLES EXENTO
            //Conceptos.Core.Conceptos conceptoHorasExtrasDoblesExenta = new Conceptos.Core.Conceptos();
            //conceptoHorasExtrasDoblesExenta.noconcepto = 2; //HORAS EXTRAS DOBLES       
            //string formulaHorasExtrasDoblesExenta = "";
            //variables = new List<string>();
            //formulas = new List<string>();
            //try
            //{
            //    cnx.Open();
            //    formulaHorasExtrasDoblesExenta = (string)ch.obtenerFormulaExento(conceptoHorasExtrasDoblesExenta);
            //    cnx.Close();
            //    variables = GLOBALES.EXTRAEVARIABLES(formulaHorasExtrasDoblesExenta, "[", "]");
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //}

            //for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            //{
            //    if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
            //        formulas.Add(formulaHorasExtrasDoblesExenta);
            //}
            //#endregion

            //#region CALCULO DE LA FORMULA DE HORAS EXTRAS DOBLES EXENTO
            //horasExtrasDoblesExcenta = new List<string>();
            //valores = new List<string>();
            //for (int i = 0; i < variables.Count; i++)
            //{
            //    FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, 15);
            //    valores = f.ObtenerValorVariable();
            //    for (int j = 0; j < valores.Count; j++)
            //    {
            //        formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
            //    }
            //}

            //for (int i = 0; i < formulas.Count; i++)
            //{
            //    MathParserTK.MathParser parser = new MathParserTK.MathParser();
            //    horasExtrasDoblesExcenta.Add(parser.Parse(formulas[i]).ToString());
            //}
            //#endregion

            //#region CALCULO DE HORAS EXTRAS DOBLES GRAVADA
            //horasExtrasDoblesGravada = new List<string>();
            //for (int i = 0; i < horasExtrasDoblesExcenta.Count; i++)
            //{
            //    if (double.Parse(horasExtrasDobles[i]) > double.Parse(horasExtrasDoblesExcenta[i]))
            //    {
            //        horasExtrasDoblesGravada.Add((double.Parse(horasExtrasDobles[i]) - double.Parse(horasExtrasDoblesExcenta[i])).ToString());
            //    }
            //    else
            //    {
            //        horasExtrasDoblesGravada.Add("0");
            //    }
            //}
            //#endregion
            /////FIN CALCULO DE HORAS EXTRAS DOBLES

            //#region CALCULO DEL ISR
            //double excedente, ImpMarginal;
            //isr = new List<string>();
            //List<TablaIsr.Core.TablaIsr> lstIsr = new List<TablaIsr.Core.TablaIsr>();
            //TablaIsr.Core.IsrHelper ih = new TablaIsr.Core.IsrHelper();
            //ih.Command = cmd;
            //for (int i = 0; i < lstDatosEmpleado.Count; i++)
            //{
            //    TablaIsr.Core.TablaIsr _isr = new TablaIsr.Core.TablaIsr();
            //    _isr.periodo = 15;
            //    _isr.inferior = double.Parse(sueldo[i]);
            //    try
            //    {
            //        cnx.Open();
            //        lstIsr = ih.isrCorrespondiente(_isr);
            //        cnx.Close();
            //    }
            //    catch (Exception error) {
            //        MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //        cnx.Dispose();
            //    }

            //    for (int j = 0; j < lstIsr.Count; j++)
            //    {
            //        excedente = double.Parse(sueldo[i]) - lstIsr[j].inferior;
            //        ImpMarginal = excedente * (lstIsr[j].porcentaje / 100);
            //        isr.Add((ImpMarginal + lstIsr[j].cuota).ToString());
            //    }
            //}
            //#endregion

            //#region CALCULO INFONAVIT
            //Conceptos.Core.Conceptos conceptoInfonavit = new Conceptos.Core.Conceptos();
            //conceptoInfonavit.noconcepto = 9; //INFONAVIT        
            //string formulaInfonavit = "";
            //variables = new List<string>();
            //infonavit = new List<string>();
            //try
            //{
            //    cnx.Open();
            //    formulaInfonavit = (string)ch.obtenerFormula(conceptoInfonavit);
            //    cnx.Close();
            //    variables = GLOBALES.EXTRAEVARIABLES(formulaInfonavit, "[", "]");
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            //}

            //for (int i = 0; i < variables.Count; i++)
            //{
            //    FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado);
            //    infonavit = f.ObtenerValorVariable();
            //}
            //#endregion
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
                if ((bool)fila.Cells["seleccion"].Value)
                {
                    listaIds += fila.Cells["idtrabajador"].Value + ",";
                }
            }

            if (listaIds.Equals("")) return;

            listaIds.Substring(listaIds.Length - 1, 1);

            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            lstDatosNomina = new List<CalculoNomina.Core.Nomina>();
            try {
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
            for (int i = 0; i < lstDatosNomina.Count; i++)
            {
                tmpPagoNomina vn = new tmpPagoNomina();
                vn.idtrabajador = lstDatosNomina[i].idtrabajador;
                vn.idempresa = GLOBALES.IDEMPRESA;
                vn.idconcepto = lstDatosNomina[i].id;
                vn.tipoconcepto = lstDatosNomina[i].tipoconcepto;
                vn.fechainicio = dtpPeriodoInicio.Value;
                vn.fechafin = dtpPeriodoFin.Value;

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
                vn.cantidad = (double)f.calcularFormula();
                vn.exento = double.Parse(f.calcularFormulaExento().ToString());
                vn.gravado = (double)f.calcularFormula() - double.Parse(f.calcularFormulaExento().ToString());

                lstValoresNomina.Add(vn);
            }
            #endregion

            #region VERIFICACION DE PRIMA VACACIONAL Y VACACIONES
            vh = new Vacaciones.Core.VacacionesHelper();
            vh.Command = cmd;

            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
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
                        catch (Exception error) {
                            MessageBox.Show("Error: \r\n \r\n" + error.Message,"Error");
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
                        lstValoresNomina.Add(vn);
                    }
                }
            }
            
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

            for (int i = 0; i < lstValoresNomina.Count; i++)
            {
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
                dt.Rows.Add(dtFila);
            }

            bulk = new SqlBulkCopy(cnx);
            nh.bulkCommand = bulk;

            try {
                cnx.Open();
                nh.bulkNomina(dt, "tmpPagoNomina");
                cnx.Close();
            }
            catch (Exception error) {
                MessageBox.Show("Error: \r\n \r\n" + error.Message + "\r\n \r\n Error Bulk Nomina.", "Error");
            }
            #endregion

            #region MOSTRAR DATOS EN EL GRID
            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                if ((bool)fila.Cells["seleccion"].Value)
                {
                    for (int i = 0; i < lstValoresNomina.Count; i++)
                    {
                        if ((int)fila.Cells["idtrabajador"].Value == lstValoresNomina[i].idtrabajador)
                        {
                            switch(lstValoresNomina[i].idconcepto)
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
            #endregion
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
            try {
                cnx.Open();
                existe = (int)nh.existePreNomina(dtpPeriodoInicio.Value, dtpPeriodoFin.Value);
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
                MessageBox.Show("Error: \r\n \r\n" + error.Message,"Error");
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
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = dtpPeriodoInicio.Value;
            pn.fechafin = dtpPeriodoFin.Value;

            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                if ((bool)fila.Cells["seleccion"].Value)
                {
                    pn.idtrabajador = (int)fila.Cells["idtrabajador"].Value;
                    try {
                        cnx.Open();
                        nh.eliminaCalculo(pn);
                        cnx.Close();
                    }
                    catch (Exception error) 
                    {
                        MessageBox.Show("Error: \r\n \r\n" + error.Message + "\r\n \r\n Error en descalculo: " + fila.Cells["noempleado"].Value.ToString(), "Error");
                    }
                }
            }
            cnx.Dispose();
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
    }
}
