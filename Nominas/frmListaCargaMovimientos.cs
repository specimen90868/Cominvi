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
    public partial class frmListaCargaMovimientos : Form
    {
        public frmListaCargaMovimientos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        SqlBulkCopy bulk;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        string ruta, nombreEmpresa = "";
        string ExcelConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;'";
        int idEmpresa;
        Empleados.Core.EmpleadosHelper emph;
        Movimientos.Core.MovimientosHelper mh;
        Conceptos.Core.ConceptosHelper ch;
        #endregion

        #region VARIABLES PUBLICA
        public int _tipoNomina;
        public DateTime _inicioPeriodo;
        public DateTime _finPeriodo;
        #endregion

        private void toolNuevo_Click(object sender, EventArgs e)
        {
            frmMovimientos m = new frmMovimientos();
            m._tipoNomina = _tipoNomina;
            m._ventana = "Carga";
            m.OnMovimiento += m_OnMovimiento;
            m.MdiParent = this.MdiParent;
            m.Show();
        }

        void m_OnMovimiento(int idEmpleado, string concepto, double cantidad, DateTime inicio, DateTime fin)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            emph = new Empleados.Core.EmpleadosHelper();
            emph.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = idEmpleado;
            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();
            try
            {
                cnx.Open();
                lstEmpleado = emph.obtenerEmpleado(empleado);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Obtener No. de Empleado. \r\n \r\n" + error.Message, "Error");
                return;
            }

            dgvMovimientos.Rows.Add(lstEmpleado[0].noempleado, concepto, cantidad.ToString(), inicio.ToShortDateString(), fin.ToShortDateString());
        }

        private void toolCargar_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            string conStr, sheetName;
            DateTime inicio, fin;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleccionar Excel";
            ofd.RestoreDirectory = false;
            ofd.InitialDirectory = @"C:\";
            ofd.Filter = "Documento de Excel|*.xls;*.xlsx";
            if (DialogResult.OK == ofd.ShowDialog())
            {
                ruta = ofd.FileName;
                conStr = string.Empty;
                conStr = string.Format(ExcelConString, ruta);

                try
                {

                    using (OleDbConnection con = new OleDbConnection(conStr))
                    {
                        using (OleDbCommand cmdOle = new OleDbCommand())
                        {
                            cmdOle.Connection = con;
                            con.Open();
                            DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            sheetName = dtExcelSchema.Rows[5]["TABLE_NAME"].ToString();
                            con.Close();
                        }
                    }

                    using (OleDbConnection con = new OleDbConnection(conStr))
                    {
                        using (OleDbCommand cmdOle = new OleDbCommand())
                        {
                            using (OleDbDataAdapter oda = new OleDbDataAdapter())
                            {
                                DataTable dt = new DataTable();
                                cmdOle.CommandText = "SELECT * From [" + sheetName + "]";
                                cmdOle.Connection = con;
                                con.Open();
                                oda.SelectCommand = cmdOle;
                                oda.Fill(dt);
                                con.Close();

                                nombreEmpresa = dt.Columns[1].ColumnName;
                                idEmpresa = int.Parse(dt.Columns[3].ColumnName.ToString());
                                inicio = DateTime.Parse(dt.Rows[1][1].ToString());
                                fin = DateTime.Parse(dt.Rows[2][1].ToString());

                                if (GLOBALES.IDEMPRESA != idEmpresa)
                                {
                                    MessageBox.Show("Los datos a ingresar pertenecen a otra empresa. Verifique. \r\n \r\n La ventana se cerrara." , "Error");
                                    this.Dispose();
                                }

                                if (inicio != _inicioPeriodo && fin != _finPeriodo)
                                {
                                    MessageBox.Show("Los datos a ingresar pertenecen a otro periodo. Verifique. \r\n \r\n La ventana se cerrara.", "Error");
                                    this.Dispose();
                                }

                                for (int i = 5; i < dt.Rows.Count; i++)
                                {
                                    for (int j = 1; j < dt.Columns.Count; j++)
                                    {
                                        if (dt.Rows[i][j].ToString() != "")
                                        {
                                            dgvMovimientos.Rows.Add(
                                                dt.Rows[i][0].ToString(), //no empleado
                                                dt.Rows[i][j].ToString(), //cantidad
                                                dt.Rows[4][j].ToString(), //concepto
                                                dt.Rows[1][1].ToString(), //fecha inicio
                                                dt.Rows[2][1].ToString()); //fecha fin
                                        }
                                    }
                                }

                                for (int i = 0; i < dgvMovimientos.Columns.Count; i++)
                                {
                                    dgvMovimientos.AutoResizeColumn(i);
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

        private void toolLimpiar_Click(object sender, EventArgs e)
        {
            dgvMovimientos.Rows.Clear();
        }

        private void toolAplicar_Click(object sender, EventArgs e)
        {
            if (dgvMovimientos.Rows.Count == 0)
            {
                MessageBox.Show("No se puede aplicar verifique.", "Error");
                return;
            }

            workMovimientos.RunWorkerAsync();
        }

        private void workMovimientos_DoWork(object sender, DoWorkEventArgs e)
        {
            string formulaexento = "";
            int idConcepto = 0, idEmpleado = 0;
            int existeConcepto = 0;
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            List<CalculoNomina.Core.tmpPagoNomina> lstMovimientos = new List<CalculoNomina.Core.tmpPagoNomina>();

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            emph = new Empleados.Core.EmpleadosHelper();
            emph.Command = cmd;

            foreach (DataGridViewRow fila in dgvMovimientos.Rows)
            {
                try
                {
                    cnx.Open();
                    idConcepto = (int)ch.obtenerIdConcepto(fila.Cells["concepto"].Value.ToString(), idEmpresa);
                    idEmpleado = (int)emph.obtenerIdTrabajador(fila.Cells["noempleado"].Value.ToString(), idEmpresa);
                    cnx.Close();

                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Obtener ID del concepto. \r\n \r\n" + error.Message, "Error");
                    return;
                }

                Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                concepto.id = idConcepto;

                List<Conceptos.Core.Conceptos> lstConcepto = new List<Conceptos.Core.Conceptos>();

                try
                {
                    cnx.Open();
                    lstConcepto = ch.obtenerConcepto(concepto);
                    cnx.Close();
                }
                catch {
                    MessageBox.Show("Error al obtener el concepto. \r\n \r\n Esta ventana se cerrará.", "Error");
                    cnx.Dispose();
                    this.Dispose();
                }

                CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
                pn.idempresa = GLOBALES.IDEMPRESA;
                pn.idtrabajador = idEmpleado;
                pn.idconcepto = idConcepto;
                pn.noconcepto = lstConcepto[0].noconcepto;
                pn.tipoconcepto = lstConcepto[0].tipoconcepto;
                pn.fechainicio = DateTime.Parse(dgvMovimientos.Rows[0].Cells["inicio"].Value.ToString());
                pn.fechafin = DateTime.Parse(dgvMovimientos.Rows[0].Cells["fin"].Value.ToString());
                pn.modificado = true;
                pn.guardada = false;
                pn.tiponomina = _tipoNomina;

                switch (lstConcepto[0].tipoconcepto)
                {
                    case "P":
                        Conceptos.Core.Conceptos _concepto = new Conceptos.Core.Conceptos();
                        concepto.noconcepto = lstConcepto[0].noconcepto;
                        concepto.idempresa = GLOBALES.IDEMPRESA;

                        try
                        {
                            cnx.Open();
                            formulaexento = ch.obtenerFormulaExento(concepto).ToString();
                            cnx.Close();
                        }
                        catch 
                        {
                            MessageBox.Show("Error al obtener la formula de exento.\r\n \r\n Esta ventana se cerrará.", "Error");
                            cnx.Dispose();
                            workMovimientos.CancelAsync();
                            this.Dispose();
                        }

                        if (formulaexento == "0")
                        {
                            pn.cantidad = double.Parse(fila.Cells["cantidad"].Value.ToString());
                            pn.exento = 0;
                            pn.gravado = double.Parse(fila.Cells["cantidad"].Value.ToString());
                        }
                        else {
                            CalculoFormula cf = new CalculoFormula(idEmpleado,
                                DateTime.Parse(dgvMovimientos.Rows[0].Cells["inicio"].Value.ToString()),
                                DateTime.Parse(dgvMovimientos.Rows[0].Cells["fin"].Value.ToString()),
                                formulaexento);

                            double exento = double.Parse(cf.calcularFormula().ToString());
                            double cantidad = double.Parse(fila.Cells["cantidad"].Value.ToString());
                            double gravado = 0;

                            if (cantidad <= exento)
                            {
                                exento = cantidad;
                                gravado = 0;
                            }
                            else
                            {
                                gravado = cantidad - exento;
                            }

                            pn.cantidad = cantidad;
                            pn.exento = exento;
                            pn.gravado = gravado;
                        }

                        try
                        {
                            CalculoNomina.Core.tmpPagoNomina pne = new CalculoNomina.Core.tmpPagoNomina();
                            pne.idempresa = GLOBALES.IDEMPRESA;
                            pne.idtrabajador = idEmpleado;
                            pne.fechainicio = DateTime.Parse(dgvMovimientos.Rows[0].Cells["inicio"].Value.ToString());
                            pne.fechafin = DateTime.Parse(dgvMovimientos.Rows[0].Cells["fin"].Value.ToString());
                            pne.noconcepto = lstConcepto[0].noconcepto;

                            cnx.Open();
                            existeConcepto = (int)nh.existeConcepto(pne);
                            cnx.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Error al obtener la existencia del concepto.\r\n \r\n Esta ventana se cerrará.", "Error");
                            cnx.Dispose();
                            workMovimientos.CancelAsync();
                            this.Dispose();
                        }

                        if (existeConcepto == 0)
                        {
                            lstMovimientos.Add(pn);
                        }
                        else
                        {
                            try
                            {
                                cnx.Open();
                                nh.actualizaConceptoModificado(pn);
                                cnx.Close();
                            }
                            catch
                            {
                                MessageBox.Show("Error al obtener la existencia del concepto.\r\n \r\n Esta ventana se cerrará.", "Error");
                                cnx.Dispose();
                                workMovimientos.CancelAsync();
                                this.Dispose();
                            }
                        }
                        
                        break;

                    case "D":
                        pn.cantidad = double.Parse(fila.Cells["cantidad"].Value.ToString());
                        pn.exento = 0;
                        pn.gravado = 0;

                        try
                        {
                            CalculoNomina.Core.tmpPagoNomina pne = new CalculoNomina.Core.tmpPagoNomina();
                            pne.idempresa = GLOBALES.IDEMPRESA;
                            pne.idtrabajador = idEmpleado;
                            pne.fechainicio = DateTime.Parse(dgvMovimientos.Rows[0].Cells["inicio"].Value.ToString());
                            pne.fechafin = DateTime.Parse(dgvMovimientos.Rows[0].Cells["fin"].Value.ToString());
                            pne.noconcepto = lstConcepto[0].noconcepto;

                            cnx.Open();
                            existeConcepto = (int)nh.existeConcepto(pne);
                            cnx.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Error al obtener la existencia del concepto.\r\n \r\n Esta ventana se cerrará.", "Error");
                            cnx.Dispose();
                            workMovimientos.CancelAsync();
                            this.Dispose();
                        }

                        if (existeConcepto == 0)
                        {
                            lstMovimientos.Add(pn);
                        }
                        else
                        {
                            try
                            {
                                cnx.Open();
                                nh.actualizaConceptoModificado(pn);
                                cnx.Close();
                            }
                            catch
                            {
                                MessageBox.Show("Error al obtener la existencia del concepto.\r\n \r\n Esta ventana se cerrará.", "Error");
                                cnx.Dispose();
                                workMovimientos.CancelAsync();
                                this.Dispose();
                            }
                        }
                        break;
                }

            }

            bulk = new SqlBulkCopy(cnx);
            nh.bulkCommand = bulk;

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
            
            int index = 1;
            for (int i = 0; i < lstMovimientos.Count; i++)
            {
                dtFila = dt.NewRow();
                dtFila["id"] = i + 1;
                dtFila["idtrabajador"] = lstMovimientos[i].idtrabajador;
                dtFila["idempresa"] = lstMovimientos[i].idempresa;
                dtFila["idconcepto"] = lstMovimientos[i].idconcepto;
                dtFila["noconcepto"] = lstMovimientos[i].noconcepto;
                dtFila["tipoconcepto"] = lstMovimientos[i].tipoconcepto;
                dtFila["exento"] = lstMovimientos[i].exento;
                dtFila["gravado"] = lstMovimientos[i].gravado;
                dtFila["cantidad"] = lstMovimientos[i].cantidad;
                dtFila["fechainicio"] = lstMovimientos[i].fechainicio;
                dtFila["fechafin"] = lstMovimientos[i].fechafin;
                dtFila["guardada"] = lstMovimientos[i].guardada;
                dtFila["tiponomina"] = lstMovimientos[i].tiponomina;
                dtFila["modificado"] = lstMovimientos[i].modificado;
                dt.Rows.Add(dtFila);
                index++;
            }

            try
            {
                cnx.Open();
                nh.bulkNomina(dt, "tmpPagoNomina");
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error (DataTable): \r\n \r\n" + error.Message, "Error");
            }

        }

        private void workMovimientos_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void workMovimientos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Movimientos importados", "Confirmacón");
            dgvMovimientos.Rows.Clear();
        }

        private void frmListaCargaMovimientos_Load(object sender, EventArgs e)
        {
            CargaPerfil();
        }

        private void CargaPerfil()
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES("Carga movimientos");

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Crear":
                        toolNuevo.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Cargar": toolCargar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Aplicar": toolAplicar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;

                }
            }
        }
    }
}
