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
    public partial class frmListaCargaVacaciones : Form
    {
        public frmListaCargaVacaciones()
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
        Vacaciones.Core.VacacionesHelper vh;
        Conceptos.Core.ConceptosHelper ch;
        TablaIsr.Core.IsrHelper ih;
        Periodos.Core.PeriodosHelper ph;
        string noempleados = "";
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
                                    dgvCargaVacaciones.Rows.Add(
                                        dt.Rows[i][0].ToString(), //NO EMPLEADO
                                        dt.Rows[i][1].ToString(), //NOMBRE
                                        dt.Rows[i][2].ToString(), //PATERNO
                                        dt.Rows[i][3].ToString(), //MATERNO
                                        (dt.Rows[i][4].ToString() == "Si" ? true : false), //PRIMA VACACIONAL
                                        (dt.Rows[i][5].ToString() == "Si" ? true : false), //VACACIONES
                                        (dt.Rows[i][5].ToString() == "Si" ? dt.Rows[i][6].ToString() : "0"), //DIAS A PAGAR
                                        dt.Rows[i][7].ToString(), //FECHA INICIO
                                        dt.Rows[i][8].ToString()); //FECHA FIN
                                }

                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    dgvCargaVacaciones.AutoResizeColumn(i);
                                }
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n Verifique que el archivo este cerrado. \r\n \r\n Descripcion: " + error.Message);
                }
            }
        }

        private void toolAplicar_Click(object sender, EventArgs e)
        {
            if (dgvCargaVacaciones.Rows.Count == 0)
            {
                MessageBox.Show("No se puede aplicar verifique.", "Error");
                return;
            }

            workVacaciones.RunWorkerAsync();
        }

        private void dgvCargaVacaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (dgvCargaVacaciones.Columns[e.ColumnIndex].Name == "vacaciones")
            {
                DataGridViewRow row = dgvCargaVacaciones.Rows[e.RowIndex];
                DataGridViewCheckBoxCell cellSelecion = row.Cells["vacaciones"] as DataGridViewCheckBoxCell;
                if (!Convert.ToBoolean(cellSelecion.Value))
                {
                    dgvCargaVacaciones.Rows[e.RowIndex].Cells["diaspago"].Value = "0";                  
                }
            }
        }

        private void dgvCargaVacaciones_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCargaVacaciones.Columns[e.ColumnIndex].Name == "vacaciones")
            {
                DataGridViewRow row = dgvCargaVacaciones.Rows[e.RowIndex];
                DataGridViewCheckBoxCell cellSelecion = row.Cells["vacaciones"] as DataGridViewCheckBoxCell;
                if (!Convert.ToBoolean(cellSelecion.Value))
                {
                    dgvCargaVacaciones.Rows[e.RowIndex].Cells["diaspago"].Value = "0";
                }
            }
        }

        private void dgvCargaVacaciones_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvCargaVacaciones.IsCurrentCellDirty)
            {
                dgvCargaVacaciones.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void toolLimpiar_Click(object sender, EventArgs e)
        {
            dgvCargaVacaciones.Rows.Clear();
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
                for (int i = 0; i < dgvCargaVacaciones.Rows.Count; i++)
                {
                    if (txtBuscar.Text.Trim() == dgvCargaVacaciones.Rows[i].Cells["noempleado"].Value.ToString())
                        dgvCargaVacaciones.Rows[i].Selected = true;
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar no. empleado...";
            txtBuscar.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            frmVacaciones v = new frmVacaciones();
            v.OnVacacion += v_OnVacacion;
            v.MdiParent = this.MdiParent;
            v.Show();
        }

        void v_OnVacacion(string noempleado, string nombre, string paterno, string materno, bool prima, bool vacacion, int diaspago, DateTime fechainicio, DateTime fechafin)
        {
            dgvCargaVacaciones.Rows.Add(noempleado,nombre,paterno,materno,prima,vacacion,diaspago,fechainicio,fechafin);
        }

        private void workVacaciones_DoWork(object sender, DoWorkEventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            bool seCalculaPrima = false, seCalculaVacaciones = false;
            foreach (DataGridViewRow fila in dgvCargaVacaciones.Rows)
            {
                if ((bool)fila.Cells["prima"].Value) seCalculaPrima = true;
                if ((bool)fila.Cells["vacaciones"].Value) seCalculaVacaciones = true;
            }

            eh = new Empresas.Core.EmpresasHelper();
            eh.Command = cmd;

            try
            {
                cnx.Open();
                idEmpresa = eh.obtenerIdEmpresa(nombreEmpresa);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            #region LISTAS
            List<Empleados.Core.Empleados> lstDatosEmpleado;
            List<string> variables;
            List<string> formulas;
            List<string> valores;
            List<string> primaVacacional;
            List<string> exento;
            List<string> pGravada;
            List<string> isr;
            List<string> diasDerecho;
            List<string> tPrima;
            List<string> diasAPagar;
            List<string> vacaciones;
            #endregion

            #region CALCULO DE PRIMA VACACIONAL
            if (seCalculaPrima)
            {
                double primaGravada = 0, excedente = 0, ImpMarginal = 0, isrDefinitivo = 0;
                double isr1 = 0, isr2 = 0, valor1 = 0, valor2 = 0, valor3 = 0, valor4 = 0;
                int diasPeriodo = 0;

                #region DATOS DEL EMPLEADO
                emph = new Empleados.Core.EmpleadosHelper();
                emph.Command = cmd;
                foreach (DataGridViewRow fila in dgvCargaVacaciones.Rows)
                {
                    if ((bool)fila.Cells["prima"].Value)
                        noempleados += fila.Cells["noempleado"].Value.ToString() + ",";
                }
                noempleados = noempleados.Substring(0, noempleados.Count() - 1);
                lstDatosEmpleado = new List<Empleados.Core.Empleados>();
                try
                {
                    cnx.Open();
                    lstDatosEmpleado = emph.obtenerAntiguedades(noempleados);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                    this.Dispose();
                }
                #endregion

                #region DIAS DERECHO
                diasDerecho = new List<string>();
                vh = new Vacaciones.Core.VacacionesHelper();
                vh.Command = cmd;
                for (int i = 0; i < lstDatosEmpleado.Count; i++)
                {
                    Vacaciones.Core.DiasDerecho dias = new Vacaciones.Core.DiasDerecho();
                    dias.anio = lstDatosEmpleado[i].antiguedadmod;
                    try
                    {
                        cnx.Open();
                        diasDerecho.Add(vh.diasDerecho(dias).ToString());
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                    }
                }
                #endregion

                #region FORMULA DE PRIMA VACACIONAL Y ASIGNACION POR EMPLEADO
                ch = new Conceptos.Core.ConceptosHelper();
                ch.Command = cmd;
                Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                concepto.noconcepto = 4; //PRIMA VACACIONAL          
                string formulaPrimaVacacional = "";
                variables = new List<string>();
                formulas = new List<string>();
                try
                {
                    cnx.Open();
                    formulaPrimaVacacional = (string)ch.obtenerFormula(concepto);
                    cnx.Close();
                    variables = GLOBALES.EXTRAEVARIABLES(formulaPrimaVacacional, "[", "]");
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }

                for (int i = 0; i < dgvCargaVacaciones.Rows.Count; i++)
                {
                    if ((bool)dgvCargaVacaciones.Rows[i].Cells["prima"].Value)
                        formulas.Add(formulaPrimaVacacional);
                }
                #endregion

                #region CALCULO DE LA FORMULA DE PRIMA VACACIONAL
                valores = new List<string>();
                primaVacacional = new List<string>();
                for (int i = 0; i < variables.Count; i++)
                {
                    FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado);
                    valores = f.ObtenerValorVariable();
                    for (int j = 0; j < valores.Count; j++)
                    {
                        formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
                    }
                }

                for (int i = 0; i < formulas.Count; i++)
                {
                    MathParserTK.MathParser parser = new MathParserTK.MathParser();
                    primaVacacional.Add(parser.Parse(formulas[i]).ToString());
                }
                #endregion

                #region CALCULO DE LA FORMULA PRIMA VACACIONAL EXENTO
                string formulaPrimaVacacionalExento = "";
                formulas = new List<string>();
                variables = new List<string>();
                exento = new List<string>();
                try
                {
                    cnx.Open();
                    formulaPrimaVacacionalExento = (string)ch.obtenerFormulaExento(concepto);
                    cnx.Close();
                    variables = GLOBALES.EXTRAEVARIABLES(formulaPrimaVacacionalExento, "[", "]");
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }

                for (int i = 0; i < dgvCargaVacaciones.Rows.Count; i++)
                {
                    if ((bool)dgvCargaVacaciones.Rows[i].Cells["prima"].Value)
                        formulas.Add(formulaPrimaVacacionalExento);
                }

                for (int i = 0; i < variables.Count; i++)
                {
                    FormulasValores f = new FormulasValores(variables[i], lstDatosEmpleado);
                    valores = f.ObtenerValorVariable();
                    for (int j = 0; j < valores.Count; j++)
                    {
                        formulas[j] = formulas[j].Replace("[" + variables[i] + "]", valores[j]);
                    }
                }

                for (int i = 0; i < formulas.Count; i++)
                {
                    MathParserTK.MathParser parser = new MathParserTK.MathParser();
                    exento.Add(parser.Parse(formulas[i]).ToString());
                }
                #endregion

                #region PV EXENTA
                pGravada = new List<string>();
                for (int i = 0; i < primaVacacional.Count; i++)
                {
                    if (double.Parse(primaVacacional[i]) > double.Parse(exento[i]))
                        primaGravada = double.Parse(primaVacacional[i]) - double.Parse(exento[i]);
                    else
                        primaGravada = 0;
                    pGravada.Add(primaGravada.ToString());
                }
                #endregion

                #region PV GRAVADA
                ih = new TablaIsr.Core.IsrHelper();
                ph = new Periodos.Core.PeriodosHelper();
                ih.Command = cmd;
                ph.Command = cmd;
                List<TablaIsr.Core.TablaIsr> lstIsr1;
                List<TablaIsr.Core.TablaIsr> lstIsr2;
                isr = new List<string>();
                for (int i = 0; i < pGravada.Count; i++)
                {
                    if (!double.Parse(pGravada[i]).Equals(0))
                    {
                        TablaIsr.Core.TablaIsr tablaIsr = new TablaIsr.Core.TablaIsr();
                        Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
                        periodo.idperiodo = lstDatosEmpleado[i].idperiodo;
                        try
                        {
                            cnx.Open();
                            diasPeriodo = (int)ph.DiasDePago(periodo);
                            cnx.Close();
                        }
                        catch (Exception error)
                        { MessageBox.Show("Error (Periodo): \r\n \r\n" + error.Message, "Error"); cnx.Dispose(); this.Dispose(); }

                        valor1 = ((double.Parse(pGravada[i]) / lstDatosEmpleado[i].sd) * 30.4) +
                            ((new DateTime(DateTime.Now.Year, 12, 31) - new DateTime(DateTime.Now.Year, 1, 1)).TotalDays * 30);
                        tablaIsr.periodo = diasPeriodo;
                        tablaIsr.inferior = valor1;
                        try
                        {
                            cnx.Open();
                            lstIsr1 = ih.isrCorrespondiente(tablaIsr);
                            excedente = valor1 - lstIsr1[0].inferior;
                            ImpMarginal = excedente * (lstIsr1[0].porcentaje / 100);
                            isr1 = ImpMarginal + lstIsr1[0].cuota;
                            cnx.Close();
                        }
                        catch (Exception error)
                        { MessageBox.Show("Error (Isr): \r\n \r\n" + error.Message, "Error"); cnx.Dispose(); this.Dispose(); }

                        tablaIsr = new TablaIsr.Core.TablaIsr();
                        valor2 = ((new DateTime(DateTime.Now.Year, 12, 31) - new DateTime(DateTime.Now.Year, 1, 1)).TotalDays * 30);
                        tablaIsr.periodo = diasPeriodo;
                        tablaIsr.inferior = valor2;

                        try
                        {
                            cnx.Open();
                            lstIsr2 = ih.isrCorrespondiente(tablaIsr);
                            cnx.Close();
                            excedente = valor2 - lstIsr2[0].inferior;
                            ImpMarginal = excedente * (lstIsr2[0].porcentaje / 100);
                            isr2 = ImpMarginal + lstIsr2[0].cuota;
                        }
                        catch (Exception error)
                        { MessageBox.Show("Error (Isr): \r\n \r\n" + error.Message, "Error"); cnx.Dispose(); this.Dispose(); }

                        valor3 = isr1 - isr2;
                        valor4 = valor3 / ((double.Parse(pGravada[i]) / lstDatosEmpleado[i].sd) * 30.4);
                        isrDefinitivo = valor4 * double.Parse(pGravada[i]);
                    }
                    else
                    {
                        isrDefinitivo = 0;
                    }
                    isr.Add(isrDefinitivo.ToString());
                }
                #endregion

                #region PAGO DE PRIMA
                tPrima = new List<string>();
                for (int i = 0; i < lstDatosEmpleado.Count; i++)
                {
                    tPrima.Add((double.Parse(primaVacacional[i]) - double.Parse(isr[i])).ToString());
                }
                #endregion

                #region DATATABLE BULK PRIMA VACACIONAL
                bulk = new SqlBulkCopy(cnx);
                vh.bulkCommand = bulk;

                DataTable dt = new DataTable();
                DataRow dtFila;
                dt.Columns.Add("id", typeof(Int32));
                dt.Columns.Add("idtrabajador", typeof(Int32));
                dt.Columns.Add("idempresa", typeof(Int32));
                dt.Columns.Add("fechaingreso", typeof(DateTime));
                dt.Columns.Add("inicio", typeof(DateTime));
                dt.Columns.Add("fin", typeof(DateTime));
                dt.Columns.Add("sd", typeof(Double));
                dt.Columns.Add("diasderecho", typeof(Int32));
                dt.Columns.Add("diasapagar", typeof(Int32));
                dt.Columns.Add("diaspendientes", typeof(Int32));
                dt.Columns.Add("pv", typeof(Double));
                dt.Columns.Add("pexenta", typeof(Double));
                dt.Columns.Add("pgravada", typeof(Double));
                dt.Columns.Add("isrgravada", typeof(Double));
                dt.Columns.Add("pagovacaciones", typeof(Double));
                dt.Columns.Add("totalprima", typeof(Double));
                dt.Columns.Add("total", typeof(Double));
                dt.Columns.Add("fechapago", typeof(DateTime));
                dt.Columns.Add("pagada", typeof(Boolean));
                dt.Columns.Add("pvpagada", typeof(Boolean));
                int index = 1;
                for (int i = 0; i < lstDatosEmpleado.Count; i++)
                {
                    dtFila = dt.NewRow();
                    dtFila["id"] = index;
                    dtFila["idtrabajador"] = lstDatosEmpleado[i].idtrabajador;
                    dtFila["idempresa"] = idEmpresa;
                    dtFila["fechaingreso"] = lstDatosEmpleado[i].fechaantiguedad;
                    dtFila["inicio"] = DateTime.Parse(dgvCargaVacaciones.Rows[0].Cells["inicioperiodo"].Value.ToString());
                    dtFila["fin"] = DateTime.Parse(dgvCargaVacaciones.Rows[0].Cells["finperiodo"].Value.ToString());
                    dtFila["sd"] = lstDatosEmpleado[i].sd;
                    dtFila["diasderecho"] = int.Parse(diasDerecho[i]);
                    dtFila["diasapagar"] = 0;
                    dtFila["diaspendientes"] = 0;
                    dtFila["pv"] = double.Parse(primaVacacional[i]);
                    dtFila["pexenta"] = double.Parse(exento[i]);
                    dtFila["pgravada"] = double.Parse(pGravada[i]);
                    dtFila["isrgravada"] = double.Parse(isr[i]);
                    dtFila["pagovacaciones"] = 0;
                    dtFila["totalprima"] = double.Parse(tPrima[i]);
                    dtFila["total"] = double.Parse(tPrima[i]);
                    dtFila["fechapago"] = DateTime.Now;
                    dtFila["pagada"] = 0;
                    dtFila["pvpagada"] = 0;
                    dt.Rows.Add(dtFila);
                    index++;
                }

                try
                {
                    cnx.Open();
                    vh.bulkVacaciones(dt, "tmpPagoVacaciones");
                    cnx.Close();
                    MessageBox.Show("Prima vacacional aplicada.", "Confirmación");
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error (DataTable): \r\n \r\n" + error.Message, "Error");
                }
                #endregion
            }
            #endregion

            #region CALCULO DE VACACIONES
            if (seCalculaVacaciones)
            {
                #region DATOS DEL EMPLEADO
                emph = new Empleados.Core.EmpleadosHelper();
                emph.Command = cmd;
                noempleados = "";
                foreach (DataGridViewRow fila in dgvCargaVacaciones.Rows)
                {
                    if ((bool)fila.Cells["vacaciones"].Value)
                        noempleados += fila.Cells["noempleado"].Value.ToString() + ",";
                }
                noempleados = noempleados.Substring(0, noempleados.Count() - 1);
                lstDatosEmpleado = new List<Empleados.Core.Empleados>();
                try
                {
                    cnx.Open();
                    lstDatosEmpleado = emph.obtenerAntiguedades(noempleados);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                    this.Dispose();
                }
                #endregion

                #region DIAS DERECHO
                diasDerecho = new List<string>();
                vh = new Vacaciones.Core.VacacionesHelper();
                vh.Command = cmd;
                for (int i = 0; i < lstDatosEmpleado.Count; i++)
                {
                    Vacaciones.Core.DiasDerecho dias = new Vacaciones.Core.DiasDerecho();
                    dias.anio = lstDatosEmpleado[i].antiguedadmod;
                    try
                    {
                        cnx.Open();
                        diasDerecho.Add(vh.diasDerecho(dias).ToString());
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                    }
                }
                #endregion

                #region DIAS A PAGAR DATAGRID
                diasAPagar = new List<string>();
                foreach (DataGridViewRow fila in dgvCargaVacaciones.Rows)
                {
                    if ((bool)fila.Cells["vacaciones"].Value)
                        diasAPagar.Add(fila.Cells["diaspago"].Value.ToString());
                }
                #endregion

                #region CALCULO DE VACACIONES A PAGAR
                vacaciones = new List<string>();
                for (int i = 0; i < lstDatosEmpleado.Count; i++)
                {
                    vacaciones.Add((lstDatosEmpleado[i].sd * double.Parse(diasAPagar[i])).ToString());
                }
                #endregion

                #region DATATABLE BULK VACACIONES
                bulk = new SqlBulkCopy(cnx);
                vh.bulkCommand = bulk;

                DataTable dt = new DataTable();
                DataRow dtFila;
                dt.Columns.Add("id", typeof(Int32));
                dt.Columns.Add("idtrabajador", typeof(Int32));
                dt.Columns.Add("idempresa", typeof(Int32));
                dt.Columns.Add("fechaingreso", typeof(DateTime));
                dt.Columns.Add("inicio", typeof(DateTime));
                dt.Columns.Add("fin", typeof(DateTime));
                dt.Columns.Add("sd", typeof(Double));
                dt.Columns.Add("diasderecho", typeof(Int32));
                dt.Columns.Add("diasapagar", typeof(Int32));
                dt.Columns.Add("diaspendientes", typeof(Int32));
                dt.Columns.Add("pv", typeof(Double));
                dt.Columns.Add("pgravada", typeof(Double));
                dt.Columns.Add("isrgravada", typeof(Double));
                dt.Columns.Add("pagovacaciones", typeof(Double));
                dt.Columns.Add("totalprima", typeof(Double));
                dt.Columns.Add("total", typeof(Double));
                dt.Columns.Add("fechapago", typeof(DateTime));
                dt.Columns.Add("pagada", typeof(Boolean));
                dt.Columns.Add("pvpagada", typeof(Boolean));
                int index = 1;
                for (int i = 0; i < lstDatosEmpleado.Count; i++)
                {
                    dtFila = dt.NewRow();
                    dtFila["id"] = index;
                    dtFila["idtrabajador"] = lstDatosEmpleado[i].idtrabajador;
                    dtFila["idempresa"] = idEmpresa;
                    dtFila["fechaingreso"] = lstDatosEmpleado[i].fechaantiguedad;
                    dtFila["inicio"] = DateTime.Parse(dgvCargaVacaciones.Rows[0].Cells["inicioperiodo"].Value.ToString());
                    dtFila["fin"] = DateTime.Parse(dgvCargaVacaciones.Rows[0].Cells["finperiodo"].Value.ToString());
                    dtFila["sd"] = lstDatosEmpleado[i].sd;
                    dtFila["diasderecho"] = int.Parse(diasDerecho[i]);
                    dtFila["diasapagar"] = int.Parse(diasAPagar[i]);
                    dtFila["diaspendientes"] = int.Parse(diasDerecho[i]) - int.Parse(diasAPagar[i]);
                    dtFila["pv"] = 0;
                    dtFila["pgravada"] = 0;
                    dtFila["isrgravada"] = 0;
                    dtFila["pagovacaciones"] = double.Parse(vacaciones[i]);
                    dtFila["totalprima"] = 0;
                    dtFila["total"] = double.Parse(vacaciones[i]);
                    dtFila["fechapago"] = DateTime.Now;
                    dtFila["pagada"] = 0;
                    dtFila["pvpagada"] = 0;
                    dt.Rows.Add(dtFila);
                    index++;
                }

                try
                {
                    cnx.Open();
                    vh.bulkVacaciones(dt, "tmpPagoVacaciones");
                    vh.stpVacaciones();
                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error (DataTable): \r\n \r\n" + error.Message, "Error");
                }
                #endregion
            }
            #endregion
        }

        private void workVacaciones_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Vacación aplicada.", "Confirmación");
        }
    }
}
