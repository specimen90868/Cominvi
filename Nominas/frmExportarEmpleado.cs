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
        string campos = "";
        string tablaDireccion = "";
        string tablaComplemento = "";
        string tablaInfonavit = "";
        #endregion

        private void frmExportarEmpleado_Load(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Exportacion.Core.ExportacionHelper();
            eh.Command = cmd;

            List<Exportacion.Core.Exportacion> lstExportacion = new List<Exportacion.Core.Exportacion>();

            try {
                cnx.Open();
                lstExportacion = eh.obtenerDatos(GLOBALES.IDEMPRESA, "frmListaEmpleados");
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
                switch (lstExportacion[i].campo)
                {
                    case "Nombre": chkNombre.Checked = lstExportacion[i].activo; break;
                    case "Paterno": chkPaterno.Checked = lstExportacion[i].activo; break;
                    case "Materno": chkMaterno.Checked = lstExportacion[i].activo; break;
                    case "NombreCompleto": chkNombreCompleto.Checked = lstExportacion[i].activo; break;
                    case "Departamento": chkDepartamento.Checked = lstExportacion[i].activo; break;
                    case "Puesto": chkPuesto.Checked = lstExportacion[i].activo; break;
                    case "FechaIngreso": chkFechaIngreso.Checked = lstExportacion[i].activo; break;
                    case "FechaAntiguedad": chkFechaAntiguedad.Checked = lstExportacion[i].activo; break;
                    case "FechaNacimiento": chkFechaNacimiento.Checked = lstExportacion[i].activo; break;
                    case "Edad": chkEdad.Checked = lstExportacion[i].activo; break;
                    case "RFC": chkRfc.Checked = lstExportacion[i].activo; break;
                    case "CURP": chkCurp.Checked = lstExportacion[i].activo; break;
                    case "NSS": chkNss.Checked = lstExportacion[i].activo; break;
                    case "SDI": chkSdi.Checked = lstExportacion[i].activo; break;
                    case "SD": chkSd.Checked = lstExportacion[i].activo; break;
                    case "Sueldo": chkSueldo.Checked = lstExportacion[i].activo; break;
                    case "Estatus": chkEstatus.Checked = lstExportacion[i].activo; break;
                    case "Cuenta": chkCuenta.Checked = lstExportacion[i].activo; break;
                    case "Clabe": chkClabe.Checked = lstExportacion[i].activo; break;
                    case "IDBancario": chkIdBancario.Checked = lstExportacion[i].activo; break;
                    case "Calle": chkCalle.Checked = lstExportacion[i].activo; break;
                    case "Exterior": chkExterior.Checked = lstExportacion[i].activo; break;
                    case "Interior": chkInterior.Checked = lstExportacion[i].activo; break;
                    case "Colonia": chkColonia.Checked = lstExportacion[i].activo; break;
                    case "CP": chkCp.Checked = lstExportacion[i].activo; break;
                    case "Ciudad": chkCiudad.Checked = lstExportacion[i].activo; break;
                    case "Estado": chkEstado.Checked = lstExportacion[i].activo; break;
                    case "Pais": chkPais.Checked = lstExportacion[i].activo; break;
                    case "EstadoCivil": chkEstadoCivil.Checked = lstExportacion[i].activo; break;
                    case "Sexo": chkSexo.Checked = lstExportacion[i].activo; break;
                    case "Escolaridad": chkEscolaridad.Checked = lstExportacion[i].activo; break;
                    case "Nacionalidad": chkNacionalidad.Checked = lstExportacion[i].activo; break;
                    case "Credito": chkCreditoInfonavit.Checked = lstExportacion[i].activo; break;
                    case "Descuento": chkValorDescuento.Checked = lstExportacion[i].activo; break;
                    case "TipoDescuento": chkDescuentoInfonavit.Checked = lstExportacion[i].activo; break;
                    case "NoEmpleado": chkNoEmpleado.Checked = lstExportacion[i].activo; break;
                }
            }
        }

        private void toolExportar_Click(object sender, EventArgs e)
        {
            workerExportar.RunWorkerAsync();
        }

        private void frmExportarEmpleado_FormClosing(object sender, FormClosingEventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Exportacion.Core.ExportacionHelper();
            eh.Command = cmd;

           
                Exportacion.Core.Exportacion exp = new Exportacion.Core.Exportacion();
                exp.activo = chkNombre.Checked;
                exp.campo = "Nombre";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
           
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkPaterno.Checked;
                exp.campo = "Paterno";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
               exp = new Exportacion.Core.Exportacion();
                exp.activo = chkMaterno.Checked;
                exp.campo = "Materno";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkNombreCompleto.Checked;
                exp.campo = "NombreCompleto";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkDepartamento.Checked;
                exp.campo = "Departamento";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkPuesto.Checked;
                exp.campo = "Puesto";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkFechaIngreso.Checked;
                exp.campo = "FechaIngreso";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkFechaAntiguedad.Checked;
                exp.campo = "FechaAntiguedad";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
           
               exp = new Exportacion.Core.Exportacion();
                exp.activo = chkFechaNacimiento.Checked;
                exp.campo = "FechaNacimiento";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
               exp = new Exportacion.Core.Exportacion();
                exp.activo = chkEdad.Checked;
                exp.campo = "Edad";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkRfc.Checked;
                exp.campo = "RFC";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
               exp = new Exportacion.Core.Exportacion();
                exp.activo = chkCurp.Checked;
                exp.campo = "CURP";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkNss.Checked;
                exp.campo = "NSS";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkSdi.Checked;
                exp.campo = "SDI";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkSd.Checked;
                exp.campo = "SD";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkSueldo.Checked;
                exp.campo = "Sueldo";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkEstatus.Checked;
                exp.campo = "Estatus";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkCuenta.Checked;
                exp.campo = "Cuenta";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
               exp = new Exportacion.Core.Exportacion();
                exp.activo = chkClabe.Checked;
                exp.campo = "Clabe";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkIdBancario.Checked;
                exp.campo = "IDBancario";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkCalle.Checked;
                exp.campo = "Calle";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkExterior.Checked;
                exp.campo = "Exterior";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkInterior.Checked;
                exp.campo = "Interior";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkColonia.Checked;
                exp.campo = "Colonia";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkCp.Checked;
                exp.campo = "CP";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkCiudad.Checked;
                exp.campo = "Ciudad";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
               exp = new Exportacion.Core.Exportacion();
                exp.activo = chkEstado.Checked;
                exp.campo = "Estado";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkPais.Checked;
                exp.campo = "Pais";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkEstadoCivil.Checked;
                exp.campo = "EstadoCivil";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkSexo.Checked;
                exp.campo = "Sexo";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkEscolaridad.Checked;
                exp.campo = "Escolaridad";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkNacionalidad.Checked;
                exp.campo = "Nacionalidad";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkCreditoInfonavit.Checked;
                exp.campo = "Credito";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkDescuentoInfonavit.Checked;
                exp.campo = "TipoDescuento";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }
            
                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkValorDescuento.Checked;
                exp.campo = "Descuento";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }

                exp = new Exportacion.Core.Exportacion();
                exp.activo = chkNoEmpleado.Checked;
                exp.campo = "NoEmpleado";
                exp.formulario = "frmListaEmpleados";

                try
                {
                    cnx.Open();
                    eh.actualizaExportacion(exp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al actualizar el campo.", "Error");
                    this.Dispose();
                }

                cnx.Dispose();
        }

        private void workerExportar_DoWork(object sender, DoWorkEventArgs e)
        {
            campos = "";
            string c = "";

            #region CAMPOS
            if (chkNoEmpleado.Checked)
                campos += "t.noempleado,";
            if (chkNombre.Checked)
                campos += "t.nombres,";
            if (chkPaterno.Checked)
                campos += "t.paterno,";
            if (chkMaterno.Checked)
                campos += "t.materno,";
            if (chkNombreCompleto.Checked)
                campos += "t.nombrecompleto,";
            if (chkDepartamento.Checked)
                campos += "depto.descripcion,";
            if (chkPuesto.Checked)
                campos += "p.descripcion,";
            if (chkFechaIngreso.Checked)
                campos += "t.fechaingreso,";
            if (chkFechaAntiguedad.Checked)
                campos += "t.fechaantiguedad,";
            if (chkFechaNacimiento.Checked)
                campos += "t.fechanacimiento,";
            if (chkEdad.Checked)
                campos += "t.edad,";
            if (chkRfc.Checked)
                campos += "t.rfc,";
            if (chkCurp.Checked)
                campos += "t.curp,";
            if (chkNss.Checked)
                campos += "t.nss,";
            if (chkSdi.Checked)
                campos += "t.sdi,";
            if (chkSd.Checked)
                campos += "t.sd,";
            if (chkSueldo.Checked)
                campos += "t.sueldo,";
            if (chkEstatus.Checked)
                campos += "t.estatus,";
            if (chkCuenta.Checked)
                campos += "t.cuenta,";
            if (chkClabe.Checked)
                campos += "t.clabe,";
            if (chkIdBancario.Checked)
                campos += "t.idbancario,";
            if (chkCalle.Checked)
                campos += "d.calle,";
            if (chkExterior.Checked)
                campos += "d.exterior,";
            if (chkInterior.Checked)
                campos += "d.interior,";
            if (chkColonia.Checked)
                campos += "d.colonia,";
            if (chkCp.Checked)
                campos += "d.cp,";
            if (chkCiudad.Checked)
                campos += "d.ciudad,";
            if (chkEstado.Checked)
                campos += "d.estado,";
            if (chkPais.Checked)
                campos += "d.pais,";
            if (chkEstadoCivil.Checked)
                campos += "(select descripcion from Catalogo where id = c.estadocivil) as estadocivil,";
            if (chkSexo.Checked)
                campos += "(select descripcion from Catalogo where id = c.sexo) as sexo,";
            if (chkEscolaridad.Checked)
                campos += "(select descripcion from Catalogo where id = c.escolaridad) as escolaridad,";
            if (chkNacionalidad.Checked)
                campos += "c.nacionalidad,";
            if (chkCreditoInfonavit.Checked)
                campos += "i.credito,";
            if (chkDescuentoInfonavit.Checked)
                campos += "i.descuento,";
            if (chkValorDescuento.Checked)
                campos += "i.valordescuento,";
            #endregion

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
                dt = eh.datosExportar(GLOBALES.IDEMPRESA, c, tablaDireccion + tablaComplemento + tablaInfonavit);
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
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                excel.Cells[1, iCol] = dt.Columns[i].ColumnName;
                iCol++;
            }
            iCol = 1;
            int progreso = 0;
            int totalRegistro = dt.Rows.Count;
            for (int i = 0; i < dt.Rows.Count; i++)
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

            workerExportar.ReportProgress(100);

            excel.Range["A1", "AI1"].Font.Bold = true;
            excel.Range["A1", "AI1"].Interior.ColorIndex = 36;

            workSheet.SaveAs("Reporte_Trabajadores" + DateTime.Now.Second.ToString() + ".xlsx");
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

        private void chkCalle_CheckedChanged(object sender, EventArgs e)
        {
            tablaDireccion = direccion(chkCalle.Checked);
        }

        private void chkExterior_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void chkInterior_CheckedChanged(object sender, EventArgs e)
        {
            tablaDireccion = direccion(chkInterior.Checked);
        }

        private void chkColonia_CheckedChanged(object sender, EventArgs e)
        {
            tablaDireccion = direccion(chkColonia.Checked);
        }

        private string direccion(bool status)
        {
            string tabla = "";
            if (status)
                tabla = " left join dbo.Direcciones d on t.idtrabajador = d.idpersona ";
            return tabla;
        }

        private void chkCp_CheckedChanged(object sender, EventArgs e)
        {
            tablaDireccion = direccion(chkCp.Checked);
        }

        private void chkCiudad_CheckedChanged(object sender, EventArgs e)
        {
            tablaDireccion = direccion(chkCiudad.Checked);
        }

        private void chkEstado_CheckedChanged(object sender, EventArgs e)
        {
            tablaDireccion = direccion(chkEstado.Checked);
        }

        private void chkPais_CheckedChanged(object sender, EventArgs e)
        {
            tablaDireccion = direccion(chkPais.Checked);
        }

        private void chkEstadoCivil_CheckedChanged(object sender, EventArgs e)
        {
            tablaComplemento = complemento(chkEstadoCivil.Checked);
        }

        private string complemento(bool status)
        {
            string tabla = "";
            if (status)
                tabla = " left join dbo.Complementos c on t.idtrabajador = c.idtrabajador ";
            return tabla;
        }

        private void chkSexo_CheckedChanged(object sender, EventArgs e)
        {
            tablaComplemento = complemento(chkSexo.Checked);
        }

        private void chkEscolaridad_CheckedChanged(object sender, EventArgs e)
        {
            tablaComplemento = complemento(chkEscolaridad.Checked);
        }

        private void chkNacionalidad_CheckedChanged(object sender, EventArgs e)
        {
            tablaComplemento = complemento(chkNacionalidad.Checked);
        }

        private string infonavit(bool status)
        {
            string tabla = "";
            if (status)
                tabla = " left join dbo.Infonavit i on t.idtrabajador = i.idtrabajador ";
            return tabla;
        }

        private void chkCreditoInfonavit_CheckedChanged(object sender, EventArgs e)
        {
            tablaInfonavit = infonavit(chkCreditoInfonavit.Checked);
        }

        private void chkDescuentoInfonavit_CheckedChanged(object sender, EventArgs e)
        {
            tablaInfonavit = infonavit(chkDescuentoInfonavit.Checked);
        }

        private void chkValorDescuento_CheckedChanged(object sender, EventArgs e)
        {
            tablaInfonavit = infonavit(chkValorDescuento.Checked);
        }
    }
}
