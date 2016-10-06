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
    public partial class frmExportarEmpleado : Form
    {
        public frmExportarEmpleado()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Exportacion.Core.ExportacionHelper eh;
        Periodos.Core.PeriodosHelper ph;
        string campos = "";
        string tablaDireccion = "";
        string tablaComplemento = "";
        string tablaInfonavit = "";
        string nombreArchivo = "";
        int tipoNomina = 0;
        int periodo = 0;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoReporte;
        #endregion

        private void frmExportarEmpleado_Load(object sender, EventArgs e)
        {

            lstvCampos.View = View.Details;
            lstvCampos.CheckBoxes = false;
            lstvCampos.GridLines = false;
            lstvCampos.MultiSelect = false;
            lstvCampos.Columns.Add("Campos", 100, HorizontalAlignment.Left);

            lstvOrdenCampos.View = View.Details;
            lstvOrdenCampos.CheckBoxes = false;
            lstvOrdenCampos.MultiSelect = false;
            lstvOrdenCampos.GridLines = false;
            lstvOrdenCampos.Columns.Add("Campos", 100, HorizontalAlignment.Left);


            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Exportacion.Core.ExportacionHelper();
            eh.Command = cmd;

            ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos periodo = new Periodos.Core.Periodos();
            periodo.idempresa = GLOBALES.IDEMPRESA;


            List<Periodos.Core.Periodos> lstPeriodos = new List<Periodos.Core.Periodos>();
            List<Exportacion.Core.Exportacion> lstExportacion = new List<Exportacion.Core.Exportacion>();

            try {
                cnx.Open();
                lstExportacion = eh.obtenerDatos(GLOBALES.IDEMPRESA, "frmListaEmpleados");
                lstPeriodos = ph.obtenerPeriodos(periodo);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error) 
            {
                MessageBox.Show("Error: Al obtener campos a exportar.\r\n \r\n" + error.Message,"Error");
                this.Dispose();
            }
            
            for (int i = 0; i < lstExportacion.Count; i++)
            {
                lstvCampos.Items.Add(lstExportacion[i].campo);
                if (lstExportacion[i].activo)
                    lstvOrdenCampos.Items.Add(lstExportacion[i].campo);
            }

            cmbPeriodo.DataSource = lstPeriodos;
            cmbPeriodo.DisplayMember = "pago";
            cmbPeriodo.ValueMember = "dias";

            cmbTipoNomina.SelectedIndex = 0;

            if (_tipoReporte == GLOBALES.EXPORTACATALOGO_GENERAL)
            {
                cmbPeriodo.Enabled = false;
                cmbTipoNomina.Enabled = false;
                dtpFechaInicio.Enabled = false;
            }
            else
            {
                cmbPeriodo.Enabled = true;
                cmbTipoNomina.Enabled = true;
                dtpFechaInicio.Enabled = true;
            }
        }

        private void toolExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Guardar como";
            sfd.Filter = "Archivo de Excel (*.xlsx)|*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                nombreArchivo = sfd.FileName;
                campos = "";

                tipoNomina = cmbTipoNomina.SelectedIndex;
                periodo = (int)cmbPeriodo.SelectedValue;

                #region CAMPOS

                for (int i = 0; i < lstvOrdenCampos.Items.Count; i++)
                {
                    switch (lstvOrdenCampos.Items[i].Text)
                    {
                        case "Nombre": campos += "t.nombres,"; break;
                        case "Paterno": campos += "t.paterno,"; break;
                        case "Materno": campos += "t.materno,"; break;
                        case "NombreCompleto": campos += "t.nombrecompleto,"; break;
                        case "Departamento": campos += "depto.descripcion,"; break;
                        case "Puesto": campos += "p.descripcion,"; break;
                        case "FechaIngreso": campos += "t.fechaingreso,"; break;
                        case "FechaAntiguedad": campos += "t.fechaantiguedad,"; break;
                        case "FechaNacimiento": campos += "t.fechanacimiento,"; break;
                        case "Edad": campos += "t.edad,"; break;
                        case "RFC": campos += "t.rfc,"; break;
                        case "CURP": campos += "t.curp,"; break;
                        case "NSS": campos += "t.nss,"; break;
                        case "SDI": campos += "t.sdi,"; break;
                        case "SD": campos += "t.sd,"; break;
                        case "Sueldo": campos += "t.sueldo,"; break;
                        case "Estatus": campos += "t.estatus,"; break;
                        case "Cuenta": campos += "t.cuenta,"; break;
                        case "Clabe": campos += "t.clabe,"; break;
                        case "IDBancario": campos += "t.idbancario,"; break;
                        case "Calle": campos += "d.calle,"; tablaDireccion = direccion(true); break;
                        case "Exterior": campos += "d.exterior,"; tablaDireccion = direccion(true); break;
                        case "Interior": campos += "d.interior,"; tablaDireccion = direccion(true); break;
                        case "Colonia": campos += "d.colonia,"; tablaDireccion = direccion(true); break;
                        case "CP": campos += "d.cp,"; tablaDireccion = direccion(true); break;
                        case "Ciudad": campos += "d.ciudad,"; tablaDireccion = direccion(true); break;
                        case "Estado": campos += "d.estado,"; tablaDireccion = direccion(true); break;
                        case "Pais": campos += "d.pais,"; tablaDireccion = direccion(true); break;
                        case "EstadoCivil": campos += "(select descripcion from Catalogo where id = c.estadocivil) as estadocivil,"; break;
                        case "Sexo": campos += "(select descripcion from Catalogo where id = c.sexo) as sexo,"; break;
                        case "Escolaridad": campos += "(select descripcion from Catalogo where id = c.escolaridad) as escolaridad,"; break;
                        case "Nacionalidad": campos += "c.nacionalidad,"; tablaComplemento = complemento(true); break;
                        case "Credito": campos += "i.credito,"; tablaInfonavit = infonavit(true); break;
                        case "Descuento": campos += "i.descuento,"; infonavit(true); break;
                        case "TipoDescuento": campos += "i.valordescuento,"; infonavit(true); break;
                        case "NoEmpleado": campos += "t.noempleado,"; break;
                        case "Periodo": campos += "t.idperiodo,"; break;
                        case "FechaBaja": campos += "b.fecha,"; break;
                    }
                }

                #endregion

                workerExportar.RunWorkerAsync();
            }
        }

        private void workerExportar_DoWork(object sender, DoWorkEventArgs e)
        {
            string c = "";

            c = campos.Substring(0, campos.Length - 1);
            
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Exportacion.Core.ExportacionHelper();
            eh.Command = cmd;

            DataTable dt = new DataTable();

            try
            {
                cnx.Open();
                if (_tipoReporte == GLOBALES.EXPORTACATALOGO_GENERAL)
                    dt = eh.datosExportar(GLOBALES.IDEMPRESA, c, tablaDireccion + tablaComplemento + tablaInfonavit);
                else
                    dt = eh.datosExportar(GLOBALES.IDEMPRESA, c, tablaDireccion + tablaComplemento + tablaInfonavit, tipoNomina, periodo,
                        dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Al obtener los datos para exportar. \r\n \r\n" + error.Message, "Error");
                workerExportar.ReportProgress(0, "Cancelado");
                workerExportar.CancelAsync();
                return;
            }

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Workbooks.Add();

            Microsoft.Office.Interop.Excel._Worksheet workSheet = excel.ActiveSheet;
            //SE COLOCAN LOS TITULOS DE LAS COLUMNAS
            int iCol = 1;
            int iFil = 2;
            int i = 0;
            if (_tipoReporte == GLOBALES.EXPORTACATALOGO_GENERAL)
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    excel.Cells[1, iCol] = dt.Columns[i].ColumnName;
                    iCol++;
                }
            else
                for (i = 1; i < dt.Columns.Count; i++)
                {
                    excel.Cells[1, iCol] = dt.Columns[i].ColumnName;
                    iCol++;
                }
            

            iCol = 1;
            int progreso = 0;
            int totalRegistro = dt.Rows.Count;
            if (_tipoReporte == GLOBALES.EXPORTACATALOGO_GENERAL)
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    progreso = (i * 100) / totalRegistro;
                    workerExportar.ReportProgress(progreso);
                    if (i != dt.Rows.Count - 1)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            excel.Cells[iFil, iCol] = dt.Rows[i][j];
                            iCol++;
                        }
                        iFil++;
                    }
                    else
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            excel.Cells[iFil, iCol] = dt.Rows[i][j];
                            iCol++;
                        }
                    }
                    iCol = 1;
                }
            else
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    progreso = (i * 100) / totalRegistro;
                    workerExportar.ReportProgress(progreso);
                    if (i != dt.Rows.Count - 1)
                    {
                        for (int j = 1; j < dt.Columns.Count; j++)
                        {
                            excel.Cells[iFil, iCol] = dt.Rows[i][j];
                            iCol++;
                        }
                        iFil++;
                    }
                    else
                    {
                        for (int j = 1; j < dt.Columns.Count; j++)
                        {
                            excel.Cells[iFil, iCol] = dt.Rows[i][j];
                            iCol++;
                        }
                    }
                    iCol = 1;
                }

            workerExportar.ReportProgress(100);

            excel.Range["A:AI"].EntireColumn.AutoFit();
            excel.Range["A1:AI1"].Font.Bold = true;
            excel.Range["A2"].Select();
            excel.ActiveWindow.FreezePanes = true;
            excel.Range["A1"].AutoFilter(1, System.Type.Missing);

            workSheet.SaveAs(nombreArchivo);
            excel.Visible = true;
        }

        private void workerExportar_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolPorcentaje.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void workerExportar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolPorcentaje.Text = "Completado.";
        }

        private string direccion(bool status)
        {
            string tabla = "";
            if (status)
                tabla = " left join dbo.Direcciones d on t.idtrabajador = d.idpersona ";
            return tabla;
        }

        private string complemento(bool status)
        {
            string tabla = "";
            if (status)
                tabla = " left join dbo.Complementos c on t.idtrabajador = c.idtrabajador ";
            return tabla;
        }

        private string infonavit(bool status)
        {
            string tabla = "";
            if (status)
                tabla = " left join dbo.Infonavit i on t.idtrabajador = i.idtrabajador ";
            return tabla;
        }

        private void lstvCampos_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        private void lstvOrdenCampos_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void lstvOrdenCampos_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                ListViewItem item;
                item = (ListViewItem)(e.Data.GetData(typeof(ListViewItem)));
                lstvOrdenCampos.Items.Add(item.Text);
                guardaEstatus(true, item.Text);
            }
            catch (Exception)
            {
                return;
            }
          
        }

        private void lstvOrdenCampos_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        private void lstvCampos_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void lstvCampos_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                ListViewItem item;
                item = (ListViewItem)(e.Data.GetData(typeof(ListViewItem)));
                lstvOrdenCampos.Items.RemoveAt(item.Index);
                guardaEstatus(false, item.Text);
            }
            catch (Exception)
            {
                return;
            }
            
        }

        private void guardaEstatus(bool estatus, string campo)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Exportacion.Core.ExportacionHelper();
            eh.Command = cmd;

            for (int i = 0; i < lstvOrdenCampos.Items.Count; i++)
            {
                Exportacion.Core.Exportacion exp = new Exportacion.Core.Exportacion();
                exp.activo = estatus;
                exp.campo = campo;
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo: " + lstvOrdenCampos.Items[i].Text, "Error");
                    this.Dispose();
                }
            }
            cnx.Dispose();
        }

        private void btnArriba_Click(object sender, EventArgs e)
        {
            int currentIndex = lstvOrdenCampos.SelectedItems[0].Index;
            ListViewItem item = lstvOrdenCampos.Items[currentIndex];
            if (currentIndex > 0)
            {
                lstvOrdenCampos.Items.RemoveAt(currentIndex);
                lstvOrdenCampos.Items.Insert(currentIndex - 1, item);
            }
            else
            {
                /*If the item is the top item make it the last*/
                lstvOrdenCampos.Items.RemoveAt(currentIndex);
                lstvOrdenCampos.Items.Insert(lstvOrdenCampos.Items.Count, item);
            }
        }

        private void btnAbajo_Click(object sender, EventArgs e)
        {
            int currentIndex = lstvOrdenCampos.SelectedItems[0].Index;
            int total = lstvOrdenCampos.Items.Count - 1;
            ListViewItem item = lstvOrdenCampos.Items[currentIndex];
            if (currentIndex < total)
            {
                lstvOrdenCampos.Items.RemoveAt(currentIndex);
                lstvOrdenCampos.Items.Insert(currentIndex + 1, item);
            }
            else
            {
                /*If the item is the top item make it the last*/
                lstvOrdenCampos.Items.RemoveAt(currentIndex);
                lstvOrdenCampos.Items.Insert(lstvOrdenCampos.Items.Count, item);
            }
        }

        private void frmExportarEmpleado_FormClosing(object sender, FormClosingEventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Exportacion.Core.ExportacionHelper exph = new Exportacion.Core.ExportacionHelper();
            exph.Command = cmd;

            for (int i = 0; i < lstvOrdenCampos.Items.Count; i++)
            {
                Exportacion.Core.Exportacion exp = new Exportacion.Core.Exportacion();
                exp.orden = i;
                exp.campo = lstvOrdenCampos.Items[i].Text;
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    exph.actualizaOrden(exp);
                    cnx.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al actualizar el orden de los campos.", "Error");
                    cnx.Dispose();
                }
            }
            cnx.Dispose();
        }

        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            var dias = cmbPeriodo.SelectedValue;
            if ((int)dias == 7)
            {
                DateTime dt = dtpFechaInicio.Value;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                dtpFechaInicio.Value = dt;
                dtpFechaFin.Value = dt.AddDays(6);
            }
            else
            {
                if (dtpFechaInicio.Value.Day <= 15)
                {
                    dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
                    dtpFechaFin.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 15);
                }
                else
                {
                    dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 16);
                    dtpFechaFin.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, DateTime.DaysInMonth(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month));
                }
            }
        }

    }
}
