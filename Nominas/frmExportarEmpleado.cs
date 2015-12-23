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
                chkListCampos.Items.Add(lstExportacion[i].campo, lstExportacion[i].activo);
            }
        }

        private void toolExportar_Click(object sender, EventArgs e)
        {
            string c = "";
            foreach (var item in chkListCampos.CheckedItems)
            {
                switch (item.ToString())
                {
                    case "Nombre": campos += "t.nombres,"; break;
                    case "Paterno": campos += "t.paterno,"; break;
                    case "Materno": campos += "t.materno,"; break;
                    case "NombreCompleto": campos += "t.nombrecompleto,"; break;
                    case "Departamento": campos += "depto.descripcion as departamento,"; break;
                    case "Puesto": campos += "p.descripcion as puesto,"; break;
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
                    case "Estatus": campos += "case when t.estatus = 1 then 'Alta' else 'Baja' end as estatus,"; break;
                    case "Cuenta": campos += "t.cuenta,"; break;
                    case "Clabe": campos += "t.clabe,"; break;
                    case "IDBancario": campos += "t.idbancario,"; break;
                    case "Calle": campos += "coalesce(d.calle,0) as calle,"; break;
                    case "Exterior": campos += "coalesce(d.exterior,0) as exterior,"; break;
                    case "Interior": campos += "coalesce(d.interior,0) as interior,"; break;
                    case "Colonia": campos += "coalesce(d.colonia,0) as colonia,"; break;
                    case "CP": campos += "coalesce(d.cp,0) as cp,"; break;
                    case "Ciudad": campos += "coalesce(d.ciudad,0) as ciudad,"; break;
                    case "Estado": campos += "coalesce(d.estado,0) as estado,"; break;
                    case "Pais": campos += "coalesce(d.pais,0) as pais,"; break;
                    case "EstadoCivil": campos += "coalesce(cat.descripcion,0,0) as estadocivil,"; break;
                    case "Sexo": campos += "coalesce(cat.descripcion,0) as sexo,"; break;
                    case "Escolaridad": campos += "coalesce(cat.descripcion,0,0) as escolaridad,"; break;
                    case "Nacionalidad": campos += "coalesce(c.nacionalidad,0) as nacionalidad,"; break;
                    case "Credito": campos += "isnull(i.credito,0) as credito,"; break;
                    case "TipoDescuento": campos += "case when coalesce(i.descuento,0) = 1 then 'Porcentaje' when coalesce(i.descuento,0) = 2 then 'Cuota Fija' when coalesce(i.descuento,0) = 3 then 'VSM' when coalesce(i.descuento,0) = 0 then 'NA' end as descuento,"; break;
                    case "Descuento": campos += "coalesce(i.valordescuento,0) as valordescuento,"; break;
                }
            }
            c = campos.Substring(0, campos.Length - 1);
            campos = c;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Exportacion.Core.ExportacionHelper();
            eh.Command = cmd;

            DataTable dt = new DataTable();

            try
            {
                cnx.Open();
                dt = eh.datosExportar(GLOBALES.IDEMPRESA, 1, c);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Al obtener los datos para exportar. \r\n \r\n" + error.Message, "Error");
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
            for (int i = 0; i < dt.Rows.Count; i++)
            {
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

            excel.Range["A1", "AI1"].Font.Bold = true;
            excel.Range["A1", "AI1"].Interior.ColorIndex = 36;

            workSheet.SaveAs("Reporte_Trabajadores.xlsx");
            excel.Visible = true;
        }

        private void frmExportarEmpleado_FormClosing(object sender, FormClosingEventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Exportacion.Core.ExportacionHelper();
            eh.Command = cmd;


            string s = "";
            for (int x = 0; x <= chkListCampos.CheckedItems.Count - 1; x++)
            {
                s = s + "Checked Item " + (x + 1).ToString() + " = " + chkListCampos.CheckedItems[x].ToString() + "\n";
            }
            MessageBox.Show(s);

            foreach (var item in chkListCampos.Items)
            {
                switch (item.ToString())
                {
                    case "Nombre":  break;
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
                    case "Calle": campos += "coalesce(d.calle,0) as calle,"; break;
                    case "Exterior": campos += "coalesce(d.exterior,0) as exterior,"; break;
                    case "Interior": campos += "coalesce(d.interior,0) as interior,"; break;
                    case "Colonia": campos += "coalesce(d.colonia,0) as colonia,"; break;
                    case "CP": campos += "coalesce(d.cp,0) as cp,"; break;
                    case "Ciudad": campos += "coalesce(d.ciudad,0) as ciudad,"; break;
                    case "Estado": campos += "coalesce(d.estado,0) as estado,"; break;
                    case "Pais": campos += "coalesce(d.pais,0) as pais,"; break;
                    case "EstadoCivil": campos += "coalesce(c.estadocivil,0) as estadocivil,"; break;
                    case "Sexo": campos += "coalesce(c.sexo,0) as sexo,"; break;
                    case "Escolaridad": campos += "coalesce(c.escolaridad,0) as escolaridad,"; break;
                    case "Nacionalidad": campos += "coalesce(c.nacionalidad,0) as nacionalidad,"; break;
                    case "Credito": campos += "isnull(i.credito,0) as credito,"; break;
                    case "TipoDescuento": campos += "coalesce(i.descuento,0) as descuento,"; break;
                    case "Descuento": campos += "coalesce(i.valordescuento,0) as valordescuento,"; break;
                }
            }
        }
    }
}
