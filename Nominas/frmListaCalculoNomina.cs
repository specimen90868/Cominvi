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
        string noempleados = "";
        DateTime inicio, fin;
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
                                        dt.Rows[i][4].ToString(), //SUELDO
                                        dt.Rows[i][5].ToString(), //DESPENSA
                                        dt.Rows[i][6].ToString(), //PREMIO DE ASISTENCIA
                                        dt.Rows[i][7].ToString(), //PREMIO DE PUNTUALIDAD
                                        dt.Rows[i][8].ToString(), //HORAS EXTRAS DOBLES
                                        dt.Rows[i][9].ToString(), //INICIO PERIODO
                                        dt.Rows[i][10].ToString()); //FIN PERIODO
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

            #region LISTAS
            List<Empleados.Core.Empleados> lstDatosEmpleado;
            List<string> variables;
            List<string> formulas;
            List<string> valores;
            List<string> sueldo;
            List<string> vacaciones;
            List<object> primaVacacional;
            List<string> despensa;
            List<string> asistencia;
            List<string> puntualidad;
            List<string> horasExtrasDobles;
            List<string> asistenciaGravada;
            List<string> asistenciaExcenta;
            List<string> puntualidadGravada;
            List<string> puntualidadExcenta;
            List<string> horasExtrasDoblesGravada;
            List<string> horasExtrasDoblesExcenta;
            List<string> isr;
            List<string> infonavit;
            #endregion

            #region DATOS DEL EMPLEADO
            emph = new Empleados.Core.EmpleadosHelper();
            emph.Command = cmd;
            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                if ((bool)fila.Cells["check"].Value)
                    noempleados += fila.Cells["noempleado"].Value.ToString() + ",";
            }
            noempleados = noempleados.Substring(0, noempleados.Count() - 1);
            lstDatosEmpleado = new List<Empleados.Core.Empleados>();
            try
            {
                cnx.Open();
                lstDatosEmpleado = emph.obtenerSalarioDiario(noempleados);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                this.Dispose();
            }
            #endregion

            #region FORMULA DE SUELDO
            ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;
            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            concepto.noconcepto = 1; //SUELDO        
            string formulaSueldo = "";
            variables = new List<string>();
            formulas = new List<string>();
            try
            {
                cnx.Open();
                formulaSueldo = (string)ch.obtenerFormula(concepto);
                cnx.Close();
                variables = GLOBALES.EXTRAEVARIABLES(formulaSueldo, "[", "]");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            {
                if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
                    formulas.Add(formulaSueldo);
            }
            #endregion

            #region CALCULO DE LA FORMULA DE SUELDO
            valores = new List<string>();
            sueldo = new List<string>();
            for (int i = 0; i < variables.Count; i++)
            {
                FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, inicio, fin, 15);
                valores = f.ObtenerValorVariable();
                for (int j = 0; j < valores.Count; j++)
                {
                    formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
                }
            }

            for (int i = 0; i < formulas.Count; i++)
            {
                MathParserTK.MathParser parser = new MathParserTK.MathParser();
                sueldo.Add(parser.Parse(formulas[i]).ToString());
            }
            #endregion

            #region VACACIONES
            vacaciones = new List<string>();
            vh = new Vacaciones.Core.VacacionesHelper();
            vh.Command = cmd;
            for (int i = 0; i < lstDatosEmpleado.Count; i++)
            {
                Vacaciones.Core.Vacaciones vacacion = new Vacaciones.Core.Vacaciones();
                vacacion.idtrabajador = lstDatosEmpleado[i].idtrabajador;
                vacacion.inicio = inicio;
                vacacion.fin = fin;
                try
                {
                    cnx.Open();
                    vacaciones.Add(vh.vacacionesPagadas(vacacion).ToString());
                    cnx.Close();
                }
                catch (Exception error) {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                    cnx.Dispose();
                }
            }
            #endregion

            #region PRIMA VACACIONAL
            primaVacacional = new List<object>();
            vh = new Vacaciones.Core.VacacionesHelper();
            vh.Command = cmd;
            for (int i = 0; i < lstDatosEmpleado.Count; i++)
            {
                Vacaciones.Core.Vacaciones vacacion = new Vacaciones.Core.Vacaciones();
                vacacion.idtrabajador = lstDatosEmpleado[i].idtrabajador;
                vacacion.inicio = inicio;
                vacacion.fin = fin;
                try {
                    cnx.Open();
                    primaVacacional.Add(vh.primaVacacional(vacacion));
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                    cnx.Dispose();
                }
            }
            #endregion

            #region DESPENSA
            Conceptos.Core.Conceptos conceptoDespensa = new Conceptos.Core.Conceptos();
            conceptoDespensa.noconcepto = 6; //DESPENSA        
            string formulaDespensa = "";
            variables = new List<string>();
            formulas = new List<string>();
            try
            {
                cnx.Open();
                formulaDespensa = (string)ch.obtenerFormula(conceptoDespensa);
                cnx.Close();
                variables = GLOBALES.EXTRAEVARIABLES(formulaDespensa, "[", "]");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            {
                if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
                    formulas.Add(formulaDespensa);
            }
            #endregion

            #region CALCULO DE LA FORMULA DE DESPENSA
            despensa = new List<string>();
            valores = new List<string>();
            for (int i = 0; i < variables.Count; i++)
            {
                FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, 15);
                valores = f.ObtenerValorVariable();
                for (int j = 0; j < valores.Count; j++)
                {
                    formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
                }
            }

            for (int i = 0; i < formulas.Count; i++)
            {
                MathParserTK.MathParser parser = new MathParserTK.MathParser();
                despensa.Add(parser.Parse(formulas[i]).ToString());
            }
            #endregion

            ///INICIO CALCULO DE PREMIO DE ASISTENCIA Y CALCULO DE CANTIDAD A EXENTAR
            #region PREMIO DE ASISTENCIAS
            Conceptos.Core.Conceptos conceptoAsistencia = new Conceptos.Core.Conceptos();
            conceptoAsistencia.noconcepto = 3; //SUELDO        
            string formulaAsistencia = "";
            variables = new List<string>();
            formulas = new List<string>();
            try
            {
                cnx.Open();
                formulaAsistencia = (string)ch.obtenerFormula(conceptoAsistencia);
                cnx.Close();
                variables = GLOBALES.EXTRAEVARIABLES(formulaAsistencia, "[", "]");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            {
                if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
                    formulas.Add(formulaAsistencia);
            }
            #endregion

            #region CALCULO DE LA FORMULA DE ASISTENCIA
            asistencia = new List<string>();
            valores = new List<string>();
            for (int i = 0; i < variables.Count; i++)
            {
                FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, 15);
                valores = f.ObtenerValorVariable();
                for (int j = 0; j < valores.Count; j++)
                {
                    formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
                }
            }

            for (int i = 0; i < formulas.Count; i++)
            {
                MathParserTK.MathParser parser = new MathParserTK.MathParser();
                asistencia.Add(parser.Parse(formulas[i]).ToString());
            }
            #endregion

            #region PREMIO DE ASISTENCIAS EXENTO
            Conceptos.Core.Conceptos conceptoAsistenciaExento = new Conceptos.Core.Conceptos();
            conceptoAsistenciaExento.noconcepto = 3; //PREMIO DE ASISTENCIA        
            string formulaAsistenciaExento = "";
            variables = new List<string>();
            formulas = new List<string>();
            try
            {
                cnx.Open();
                formulaAsistencia = (string)ch.obtenerFormulaExento(conceptoAsistenciaExento);
                cnx.Close();
                variables = GLOBALES.EXTRAEVARIABLES(formulaAsistenciaExento, "[", "]");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            {
                if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
                    formulas.Add(formulaAsistenciaExento);
            }
            #endregion

            #region CALCULO DE LA FORMULA DE ASISTENCIA EXENTA
            asistenciaExcenta = new List<string>();
            valores = new List<string>();
            for (int i = 0; i < variables.Count; i++)
            {
                FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, 15);
                valores = f.ObtenerValorVariable();
                for (int j = 0; j < valores.Count; j++)
                {
                    formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
                }
            }

            for (int i = 0; i < formulas.Count; i++)
            {
                MathParserTK.MathParser parser = new MathParserTK.MathParser();
                asistenciaExcenta.Add(parser.Parse(formulas[i]).ToString());
            }
            #endregion

            #region CALCULO DE ASISTENCIA GRAVADA
            asistenciaGravada = new List<string>();
            for (int i = 0; i < asistenciaExcenta.Count; i++)
            {
                asistenciaGravada.Add((double.Parse(asistencia[i]) - double.Parse(asistenciaExcenta[i])).ToString());
            }
            #endregion
            ///FIN CALCULO DE PREMIO DE ASISTENCIA

            ///INICIO CALCULO DE PREMIO DE PUNTUALIDAD Y CALCULO DE CANTIDAD A EXENTAR
            #region PREMIO DE PUNTUALIDAD
            Conceptos.Core.Conceptos conceptoPuntualidad = new Conceptos.Core.Conceptos();
            conceptoPuntualidad.noconcepto = 5; //SUELDO        
            string formulaPuntualidad = "";
            variables = new List<string>();
            formulas = new List<string>();
            try
            {
                cnx.Open();
                formulaPuntualidad = (string)ch.obtenerFormula(conceptoPuntualidad);
                cnx.Close();
                variables = GLOBALES.EXTRAEVARIABLES(formulaPuntualidad, "[", "]");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            {
                if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
                    formulas.Add(formulaPuntualidad);
            }
            #endregion

            #region CALCULO DE LA FORMULA DE PUNTUALIDAD
            puntualidad = new List<string>();
            valores = new List<string>();
            for (int i = 0; i < variables.Count; i++)
            {
                FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, 15);
                valores = f.ObtenerValorVariable();
                for (int j = 0; j < valores.Count; j++)
                {
                    formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
                }
            }

            for (int i = 0; i < formulas.Count; i++)
            {
                MathParserTK.MathParser parser = new MathParserTK.MathParser();
                puntualidad.Add(parser.Parse(formulas[i]).ToString());
            }
            #endregion

            #region PREMIO DE PUNTUALIDAD EXENTO
            Conceptos.Core.Conceptos conceptoPuntualidadexenta = new Conceptos.Core.Conceptos();
            conceptoPuntualidadexenta.noconcepto = 5; //PREMIO DE PUNTUALIDAD        
            string formulaPuntualidadExenta = "";
            variables = new List<string>();
            formulas = new List<string>();
            try
            {
                cnx.Open();
                formulaPuntualidadExenta = (string)ch.obtenerFormulaExento(conceptoPuntualidadexenta);
                cnx.Close();
                variables = GLOBALES.EXTRAEVARIABLES(formulaPuntualidadExenta, "[", "]");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            {
                if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
                    formulas.Add(formulaPuntualidadExenta);
            }
            #endregion

            #region CALCULO DE LA FORMULA DE PUNTUALIDAD EXENTA
            puntualidadExcenta = new List<string>();
            valores = new List<string>();
            for (int i = 0; i < variables.Count; i++)
            {
                FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, 15);
                valores = f.ObtenerValorVariable();
                for (int j = 0; j < valores.Count; j++)
                {
                    formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
                }
            }

            for (int i = 0; i < formulas.Count; i++)
            {
                MathParserTK.MathParser parser = new MathParserTK.MathParser();
                puntualidadExcenta.Add(parser.Parse(formulas[i]).ToString());
            }
            #endregion

            #region CALCULO DE PUNTUALIDAD GRAVADA
            puntualidadGravada = new List<string>();
            for (int i = 0; i < puntualidadExcenta.Count; i++)
            {
                puntualidadGravada.Add((double.Parse(puntualidad[i]) - double.Parse(puntualidadExcenta[i])).ToString());
            }
            #endregion
            ///FIN CALCULO DE PREMIO DE PUNTUALIDAD

            ///INICIO CALCULO DE HORAS EXTRAS DOBLES A EXENTAR
            #region HORAS EXTRAS DOBLES
            Conceptos.Core.Conceptos conceptoHorasExtrasDobles = new Conceptos.Core.Conceptos();
            conceptoHorasExtrasDobles.noconcepto = 2; //HORAS EXTRAS DOBLES        
            string formulaHorasExtrasDobles = "";
            variables = new List<string>();
            formulas = new List<string>();
            try
            {
                cnx.Open();
                formulaHorasExtrasDobles = (string)ch.obtenerFormula(conceptoHorasExtrasDobles);
                cnx.Close();
                variables = GLOBALES.EXTRAEVARIABLES(formulaHorasExtrasDobles, "[", "]");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            {
                if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
                    formulas.Add(formulaHorasExtrasDobles);
            }
            #endregion

            #region CALCULO DE LA FORMULA DE HORAS EXTRAS DOBLES
            horasExtrasDobles = new List<string>();
            valores = new List<string>();
            for (int i = 0; i < variables.Count; i++)
            {
                FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, 15);
                valores = f.ObtenerValorVariable();
                for (int j = 0; j < valores.Count; j++)
                {
                    formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
                }
            }

            for (int i = 0; i < formulas.Count; i++)
            {
                MathParserTK.MathParser parser = new MathParserTK.MathParser();
                horasExtrasDobles.Add(parser.Parse(formulas[i]).ToString());
            }
            #endregion

            #region HORAS EXTRAS DOBLES EXENTO
            Conceptos.Core.Conceptos conceptoHorasExtrasDoblesExenta = new Conceptos.Core.Conceptos();
            conceptoHorasExtrasDoblesExenta.noconcepto = 2; //HORAS EXTRAS DOBLES       
            string formulaHorasExtrasDoblesExenta = "";
            variables = new List<string>();
            formulas = new List<string>();
            try
            {
                cnx.Open();
                formulaHorasExtrasDoblesExenta = (string)ch.obtenerFormulaExento(conceptoHorasExtrasDoblesExenta);
                cnx.Close();
                variables = GLOBALES.EXTRAEVARIABLES(formulaHorasExtrasDoblesExenta, "[", "]");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            {
                if ((bool)dgvEmpleados.Rows[i].Cells["check"].Value)
                    formulas.Add(formulaHorasExtrasDoblesExenta);
            }
            #endregion

            #region CALCULO DE LA FORMULA DE HORAS EXTRAS DOBLES EXENTO
            horasExtrasDoblesExcenta = new List<string>();
            valores = new List<string>();
            for (int i = 0; i < variables.Count; i++)
            {
                FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado, 15);
                valores = f.ObtenerValorVariable();
                for (int j = 0; j < valores.Count; j++)
                {
                    formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
                }
            }

            for (int i = 0; i < formulas.Count; i++)
            {
                MathParserTK.MathParser parser = new MathParserTK.MathParser();
                horasExtrasDoblesExcenta.Add(parser.Parse(formulas[i]).ToString());
            }
            #endregion

            #region CALCULO DE HORAS EXTRAS DOBLES GRAVADA
            horasExtrasDoblesGravada = new List<string>();
            for (int i = 0; i < horasExtrasDoblesExcenta.Count; i++)
            {
                if (double.Parse(horasExtrasDobles[i]) > double.Parse(horasExtrasDoblesExcenta[i]))
                {
                    horasExtrasDoblesGravada.Add((double.Parse(horasExtrasDobles[i]) - double.Parse(horasExtrasDoblesExcenta[i])).ToString());
                }
                else
                {
                    horasExtrasDoblesGravada.Add("0");
                }
            }
            #endregion
            ///FIN CALCULO DE HORAS EXTRAS DOBLES

            #region CALCULO DEL ISR
            double excedente, ImpMarginal;
            isr = new List<string>();
            List<TablaIsr.Core.TablaIsr> lstIsr = new List<TablaIsr.Core.TablaIsr>();
            TablaIsr.Core.IsrHelper ih = new TablaIsr.Core.IsrHelper();
            ih.Command = cmd;
            for (int i = 0; i < lstDatosEmpleado.Count; i++)
            {
                TablaIsr.Core.TablaIsr _isr = new TablaIsr.Core.TablaIsr();
                _isr.periodo = 15;
                _isr.inferior = double.Parse(sueldo[i]);
                try
                {
                    cnx.Open();
                    lstIsr = ih.isrCorrespondiente(_isr);
                    cnx.Close();
                }
                catch (Exception error) {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                    cnx.Dispose();
                }

                for (int j = 0; j < lstIsr.Count; j++)
                {
                    excedente = double.Parse(sueldo[i]) - lstIsr[j].inferior;
                    ImpMarginal = excedente * (lstIsr[j].porcentaje / 100);
                    isr.Add((ImpMarginal + lstIsr[j].cuota).ToString());
                }
            }
            #endregion

            #region CALCULO INFONAVIT
            Conceptos.Core.Conceptos conceptoInfonavit = new Conceptos.Core.Conceptos();
            conceptoInfonavit.noconcepto = 9; //INFONAVIT        
            string formulaInfonavit = "";
            variables = new List<string>();
            infonavit = new List<string>();
            try
            {
                cnx.Open();
                formulaInfonavit = (string)ch.obtenerFormula(conceptoInfonavit);
                cnx.Close();
                variables = GLOBALES.EXTRAEVARIABLES(formulaInfonavit, "[", "]");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            for (int i = 0; i < variables.Count; i++)
            {
                FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado);
                infonavit = f.ObtenerValorVariable();
            }
            #endregion
        }
    }
}
