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
                            sheetName = dtExcelSchema.Rows[6]["TABLE_NAME"].ToString();
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
                                        (dt.Rows[i][5].ToString() == "Si" ? true : false), //PAGO TOTAL
                                        (dt.Rows[i][5].ToString() == "Si" ? dt.Rows[i][6].ToString() : "0"), //DIAS A PAGAR

                                        (dt.Rows[i][7].ToString() == "Si" ? true : false), //VACACIONES
                                        (dt.Rows[i][7].ToString() == "Si" ? dt.Rows[i][8].ToString() : "0"), //DIAS A PAGAR
                                        dt.Rows[i][9].ToString(), //FECHA INICIO
                                        dt.Rows[i][10].ToString()); //FECHA FIN
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

            if (dgvCargaVacaciones.Columns[e.ColumnIndex].Name == "pagototal")
            {
                DataGridViewRow row = dgvCargaVacaciones.Rows[e.RowIndex];
                DataGridViewCheckBoxCell cellSelecion = row.Cells["pagototal"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(cellSelecion.Value))
                {
                    dgvCargaVacaciones.Rows[e.RowIndex].Cells["diaspagopv"].Value = "0";
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

            if (dgvCargaVacaciones.Columns[e.ColumnIndex].Name == "pagototal")
            {
                DataGridViewRow row = dgvCargaVacaciones.Rows[e.RowIndex];
                DataGridViewCheckBoxCell cellSelecion = row.Cells["pagototal"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(cellSelecion.Value))
                {
                    dgvCargaVacaciones.Rows[e.RowIndex].Cells["diaspagopv"].Value = "0";
                }
            }
        }

        private void dgvCargaVacaciones_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvCargaVacaciones.IsCurrentCellDirty)
            {
                dgvCargaVacaciones.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            else
                dgvCargaVacaciones.CommitEdit(DataGridViewDataErrorContexts.Commit);
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

        void v_OnVacacion(string noempleado, string nombre, string paterno, string materno, bool prima, bool pago, int diaspagopv, bool vacacion, int diaspago, DateTime fechainicio, DateTime fechafin)
        {
            dgvCargaVacaciones.Rows.Add(noempleado,nombre,paterno,materno,prima,pago,diaspagopv,vacacion,diaspago,fechainicio,fechafin);
        }

        private void workVacaciones_DoWork(object sender, DoWorkEventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            bool seCalculaPrima = false, seCalculaVacaciones = false;
            foreach (DataGridViewRow fila in dgvCargaVacaciones.Rows)
            {
                if (bool.Parse(fila.Cells["prima"].Value.ToString())) seCalculaPrima = true;
                if (bool.Parse(fila.Cells["vacaciones"].Value.ToString())) seCalculaVacaciones = true;
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
            #endregion

            #region CALCULO DE PRIMA VACACIONAL
            if (seCalculaPrima)
            {
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
                }
                #endregion

                #region CALCULO PRIMA VACACIONAL
                ch = new Conceptos.Core.ConceptosHelper();
                ch.Command = cmd;
                Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                concepto.idempresa = GLOBALES.IDEMPRESA;
                concepto.noconcepto = 4; //PRIMA VACACIONAL
                string formulaPrimaVacacional = "";
                string formulaExentoPrimaVacacional = "";
                double exento = 0;
                try
                {
                    cnx.Open();
                    formulaPrimaVacacional = (string)ch.obtenerFormula(concepto);
                    formulaExentoPrimaVacacional = (string)ch.obtenerFormulaExento(concepto);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }

                List<Empleados.Core.Empleados> lstEmpleado;
                List<Vacaciones.Core.Vacaciones> lstPrimaVacacional = new List<Vacaciones.Core.Vacaciones>();
                int indice = 0;
                foreach (DataGridViewRow fila in dgvCargaVacaciones.Rows)
                {
                    if (bool.Parse(fila.Cells["prima"].Value.ToString()))
                    {
                        lstEmpleado = new List<Empleados.Core.Empleados>();
                        Empleados.Core.Empleados emp = new Empleados.Core.Empleados();
                        emp.idtrabajador = lstDatosEmpleado[indice].idtrabajador;
                        emp.idsalario = lstDatosEmpleado[indice].idsalario;
                        emp.idperiodo = lstDatosEmpleado[indice].idperiodo;
                        emp.antiguedadmod = lstDatosEmpleado[indice].antiguedadmod;
                        emp.sdi = lstDatosEmpleado[indice].sdi;
                        emp.sd = lstDatosEmpleado[indice].sd;
                        emp.fechaantiguedad = lstDatosEmpleado[indice].fechaantiguedad;
                        emp.sueldo = lstDatosEmpleado[indice].sueldo;
                        lstEmpleado.Add(emp);

                        Vacaciones.Core.Vacaciones prima = new Vacaciones.Core.Vacaciones();
                        prima.idtrabajador = lstDatosEmpleado[indice].idtrabajador;
                        prima.idempresa = GLOBALES.IDEMPRESA;
                        prima.fechaingreso = lstDatosEmpleado[indice].fechaantiguedad;
                        prima.inicio = DateTime.Parse(dgvCargaVacaciones.Rows[0].Cells["inicioperiodo"].Value.ToString());
                        prima.fin = DateTime.Parse(dgvCargaVacaciones.Rows[0].Cells["finperiodo"].Value.ToString());
                        prima.sd = lstDatosEmpleado[indice].sd;
                        prima.diasapagar = 0;
                        prima.diaspendientes = 0;
                        prima.pagovacaciones = 0;
                        prima.fechapago = DateTime.Now;
                        prima.pagada = false;
                        prima.pvpagada = false;

                        vh = new Vacaciones.Core.VacacionesHelper();
                        vh.Command = cmd;
                        Vacaciones.Core.DiasDerecho dias = new Vacaciones.Core.DiasDerecho();
                        dias.anio = lstDatosEmpleado[indice].antiguedadmod;
                        try
                        {
                            cnx.Open();
                            prima.diasderecho = (int)vh.diasDerecho(dias);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                        }

                        if (bool.Parse(fila.Cells["pagototal"].Value.ToString()))
                        {
                            FormulasValores f = new FormulasValores(formulaPrimaVacacional, lstEmpleado, DateTime.Now, DateTime.Now);
                            prima.pv = (double)f.calcularFormulaVacaciones();

                            f = new FormulasValores(formulaExentoPrimaVacacional, lstEmpleado, DateTime.Now, DateTime.Now);
                            exento = (double)f.calcularFormulaVacacionesExento();
                        }
                        else
                        {
                            FormulasValores f = new FormulasValores(formulaPrimaVacacional, lstEmpleado, DateTime.Now, DateTime.Now, int.Parse(fila.Cells["diaspagopv"].Value.ToString()));
                            prima.pv = (double)f.calcularFormulaVacaciones();

                            f = new FormulasValores(formulaExentoPrimaVacacional, lstEmpleado, DateTime.Now, DateTime.Now, int.Parse(fila.Cells["diaspagopv"].Value.ToString()));
                            exento = (double)f.calcularFormulaVacacionesExento();
                        }

                        if (prima.pv > exento)
                        {
                            prima.pexenta = exento;
                            prima.pgravada = prima.pv - exento;
                        }
                        else
                        {
                            prima.pexenta = prima.pv;
                            prima.pgravada = 0;
                        }

                        prima.isrgravada = isr(prima.pgravada, lstDatosEmpleado[indice].idperiodo, lstDatosEmpleado[indice].sd);
                        prima.totalprima = prima.pv - prima.isrgravada;
                        prima.total = prima.pv - prima.isrgravada;
                        lstPrimaVacacional.Add(prima);
                        indice++;
                    }
                }
                #region CALCULO PV COMENTADA
                //for (int i = 0; i < lstDatosEmpleado.Count; i++)
                //{
                //    lstEmpleado = new List<Empleados.Core.Empleados>();
                //    Empleados.Core.Empleados emp = new Empleados.Core.Empleados();
                //    emp.idtrabajador = lstDatosEmpleado[i].idtrabajador;
                //    emp.idsalario = lstDatosEmpleado[i].idsalario;
                //    emp.idperiodo = lstDatosEmpleado[i].idperiodo;
                //    emp.antiguedadmod = lstDatosEmpleado[i].antiguedadmod;
                //    emp.sdi = lstDatosEmpleado[i].sdi;
                //    emp.sd = lstDatosEmpleado[i].sd;
                //    emp.fechaantiguedad = lstDatosEmpleado[i].fechaantiguedad;
                //    emp.sueldo = lstDatosEmpleado[i].sueldo;
                //    lstEmpleado.Add(emp);

                //    Vacaciones.Core.Vacaciones prima = new Vacaciones.Core.Vacaciones();
                //    prima.idtrabajador = lstDatosEmpleado[i].idtrabajador;
                //    prima.idempresa = GLOBALES.IDEMPRESA;
                //    prima.fechaingreso = lstDatosEmpleado[i].fechaantiguedad;
                //    prima.inicio = DateTime.Parse(dgvCargaVacaciones.Rows[0].Cells["inicioperiodo"].Value.ToString());
                //    prima.fin = DateTime.Parse(dgvCargaVacaciones.Rows[0].Cells["finperiodo"].Value.ToString());
                //    prima.sd = lstDatosEmpleado[i].sd;
                //    prima.diasapagar = 0;
                //    prima.diaspendientes = 0;
                //    prima.pagovacaciones = 0;
                //    prima.fechapago = DateTime.Now;
                //    prima.pagada = false;
                //    prima.pvpagada = false;

                //    vh = new Vacaciones.Core.VacacionesHelper();
                //    vh.Command = cmd;
                //    Vacaciones.Core.DiasDerecho dias = new Vacaciones.Core.DiasDerecho();
                //    dias.anio = lstDatosEmpleado[i].antiguedadmod;
                //    try
                //    {
                //        cnx.Open();
                //        prima.diasderecho = (int)vh.diasDerecho(dias);
                //        cnx.Close();
                //    }
                //    catch (Exception error)
                //    {
                //        MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                //    }

                //    FormulasValores f = new FormulasValores(formulaPrimaVacacional, lstEmpleado, DateTime.Now, DateTime.Now);
                //    prima.pv = (double)f.calcularFormulaVacaciones();

                //    f = new FormulasValores(formulaExentoPrimaVacacional, lstEmpleado, DateTime.Now, DateTime.Now);
                //    exento = (double)f.calcularFormulaVacacionesExento();

                //    if (prima.pv > exento)
                //    {
                //        prima.pexenta = exento;
                //        prima.pgravada = prima.pv - exento;
                //    }
                //    else
                //    {
                //        prima.pexenta = prima.pv;
                //        prima.pgravada = 0;
                //    }

                //    prima.isrgravada = isr(prima.pgravada, lstDatosEmpleado[i].idperiodo, lstDatosEmpleado[i].sd);
                //    prima.totalprima = prima.pv - prima.isrgravada;
                //    prima.total = prima.pv - prima.isrgravada;
                //    lstPrimaVacacional.Add(prima);
                //}
                #endregion
                #endregion

                bulkVacaciones(lstPrimaVacacional);
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
                    if (bool.Parse(fila.Cells["vacaciones"].Value.ToString()))
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

                #region CALCULO DE VACACIONES
                List<Vacaciones.Core.Vacaciones> lstVacaciones = new List<Vacaciones.Core.Vacaciones>();
                int i = 0;
                foreach (DataGridViewRow fila in dgvCargaVacaciones.Rows)
                {
                    if (bool.Parse(fila.Cells["vacaciones"].Value.ToString()))
                    {
                        Vacaciones.Core.Vacaciones vacacion = new Vacaciones.Core.Vacaciones();
                        vacacion.idtrabajador = lstDatosEmpleado[i].idtrabajador;
                        vacacion.idempresa = GLOBALES.IDEMPRESA;
                        vacacion.fechaingreso = lstDatosEmpleado[i].fechaantiguedad;
                        vacacion.inicio = DateTime.Parse(fila.Cells["inicioperiodo"].Value.ToString());
                        vacacion.fin = DateTime.Parse(fila.Cells["finperiodo"].Value.ToString());
                        vacacion.sd = lstDatosEmpleado[i].sd;
                        vacacion.diasapagar = int.Parse(fila.Cells["diaspago"].Value.ToString());
                        
                        vh = new Vacaciones.Core.VacacionesHelper();
                        vh.Command = cmd;
                        Vacaciones.Core.DiasDerecho dias = new Vacaciones.Core.DiasDerecho();
                        dias.anio = lstDatosEmpleado[i].antiguedadmod;
                        try
                        {
                            cnx.Open();
                            vacacion.diasderecho = (int)vh.diasDerecho(dias);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                        }

                        vacacion.diaspendientes = vacacion.diasderecho - vacacion.diasapagar;
                        vacacion.pv = 0;
                        vacacion.pexenta = 0;
                        vacacion.pgravada = 0;
                        vacacion.isrgravada = 0;
                        vacacion.pagovacaciones = lstDatosEmpleado[i].sd * int.Parse(fila.Cells["diaspago"].Value.ToString());
                        vacacion.totalprima = 0;
                        vacacion.total = lstDatosEmpleado[i].sd * int.Parse(fila.Cells["diaspago"].Value.ToString());
                        vacacion.fechapago = DateTime.Now;
                        vacacion.pagada = false;
                        vacacion.pvpagada = false;
                        lstVacaciones.Add(vacacion);
                        i++;
                    }
                }
                #endregion

                bulkVacaciones(lstVacaciones);
            }
            #endregion

            cnx.Dispose();
        }

        private void workVacaciones_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Vacación aplicada.", "Confirmación");
            dgvCargaVacaciones.Rows.Clear();
        }

        private double isr(double pgravada, int idperiodo, double sd)
        {
            double excedente = 0, ImpMarginal = 0, isrDefinitivo = 0;
            double isr1 = 0, isr2 = 0, valor1 = 0, valor2 = 0, valor3 = 0, valor4 = 0;
            int diasPeriodo = 0;

            ih = new TablaIsr.Core.IsrHelper();
            ph = new Periodos.Core.PeriodosHelper();
            ih.Command = cmd;
            ph.Command = cmd;
            List<TablaIsr.Core.TablaIsr> lstIsr1;
            List<TablaIsr.Core.TablaIsr> lstIsr2;
            
            if (!pgravada.Equals(0))
            {
                TablaIsr.Core.TablaIsr tablaIsr = new TablaIsr.Core.TablaIsr();
                Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
                periodo.idperiodo = idperiodo;
                try
                {
                    cnx.Open();
                    diasPeriodo = (int)ph.DiasDePago(periodo);
                    cnx.Close();
                }
                catch (Exception error)
                { MessageBox.Show("Error (Periodo): \r\n \r\n" + error.Message, "Error"); cnx.Dispose(); this.Dispose(); }

                valor1 = ((pgravada / sd) * 30.4) +
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
                valor4 = valor3 / ((pgravada / sd) * 30.4);
                isrDefinitivo = valor4 * pgravada;
            }
            else
            {
                isrDefinitivo = 0;
            }
            
            return isrDefinitivo;
        }

        private void bulkVacaciones(List<Vacaciones.Core.Vacaciones> lista)
        {
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
            for (int i = 0; i < lista.Count; i++)
            {
                dtFila = dt.NewRow();
                dtFila["id"] = index;
                dtFila["idtrabajador"] = lista[i].idtrabajador;
                dtFila["idempresa"] = lista[i].idempresa;
                dtFila["fechaingreso"] = lista[i].fechaingreso;
                dtFila["inicio"] = lista[i].inicio;
                dtFila["fin"] = lista[i].fin;
                dtFila["sd"] = lista[i].sd;
                dtFila["diasderecho"] = lista[i].diasderecho;
                dtFila["diasapagar"] = lista[i].diasapagar;
                dtFila["diaspendientes"] = lista[i].diaspendientes;
                dtFila["pv"] = lista[i].pv;
                dtFila["pexenta"] = lista[i].pexenta;
                dtFila["pgravada"] = lista[i].pgravada;
                dtFila["isrgravada"] = lista[i].isrgravada;
                dtFila["pagovacaciones"] = lista[i].pagovacaciones;
                dtFila["totalprima"] = lista[i].totalprima;
                dtFila["total"] = lista[i].total;
                dtFila["fechapago"] = lista[i].fechapago;
                dtFila["pagada"] = lista[i].pagada;
                dtFila["pvpagada"] = lista[i].pvpagada;
                dt.Rows.Add(dtFila);
                index++;
            }

            try
            {
                cnx.Open();
                vh.bulkVacaciones(dt, "tmpPagoVacaciones");
                vh.stpVacaciones();
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error (DataTable): \r\n \r\n" + error.Message, "Error");
            }
        }
    }
}
