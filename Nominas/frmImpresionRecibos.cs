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
    public partial class frmImpresionRecibos : Form
    {
        public frmImpresionRecibos()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        private SqlConnection cnx;
        private SqlCommand cmd;
        private string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        string idDepartamentos = "";
        string idEmpleados = "";
        List<Empleados.Core.Empleados> lstEmp;
        #endregion

        private void frmImpresionRecibos_Load(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
            dh.Command = cmd;
            Departamento.Core.Depto depto = new Departamento.Core.Depto();
            depto.idempresa = GLOBALES.IDEMPRESA;

            List<CalculoNomina.Core.tmpPagoNomina> lstPeriodos = new List<CalculoNomina.Core.tmpPagoNomina>();
            List<Departamento.Core.Depto> lstDeptos = new List<Departamento.Core.Depto>();

            try
            {
                cnx.Open();
                lstPeriodos = nh.obtenerPeriodosNomina(GLOBALES.IDEMPRESA);
                lstDeptos = dh.obtenerDepartamentos(depto);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener los periodos de la empresa.", "Error");
                cnx.Dispose();
                return;
            }

            lstvPeriodos.View = View.Details;
            lstvPeriodos.CheckBoxes = true;
            lstvPeriodos.GridLines = false;
            lstvPeriodos.Columns.Add("Inicio", 100, HorizontalAlignment.Left);
            lstvPeriodos.Columns.Add("Fin", 100, HorizontalAlignment.Left);

            lstvDepartamentos.View = View.Details;
            lstvDepartamentos.CheckBoxes = true;
            lstvDepartamentos.GridLines = false;
            lstvDepartamentos.Columns.Add("Id", 60, HorizontalAlignment.Left);
            lstvDepartamentos.Columns.Add("Departamento", 150, HorizontalAlignment.Left);

            lstvEmpleados.View = View.Details;
            lstvEmpleados.CheckBoxes = true;
            lstvEmpleados.GridLines = false;
            lstvEmpleados.Columns.Add("Id", 60, HorizontalAlignment.Left);
            lstvEmpleados.Columns.Add("No. Empleado", 70, HorizontalAlignment.Left);
            lstvEmpleados.Columns.Add("Nombre", 250, HorizontalAlignment.Left);

            for (int i = 0; i < lstPeriodos.Count; i++)
            {
                ListViewItem Lista;
                Lista = lstvPeriodos.Items.Add(lstPeriodos[i].fechainicio.ToShortDateString());
                Lista.SubItems.Add(lstPeriodos[i].fechafin.ToShortDateString());
            }
            for (int i = 0; i < lstDeptos.Count; i++)
            {
                ListViewItem Lista;
                Lista = lstvDepartamentos.Items.Add(lstDeptos[i].id.ToString());
                Lista.SubItems.Add(lstDeptos[i].descripcion.ToString());
            }
        }

        private void lstvDepartamentos_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            idDepartamentos = "";
            for (int i = 0; i < lstvDepartamentos.Items.Count; i++)
            {
                if (lstvDepartamentos.Items[i].Checked)
                {
                    idDepartamentos += lstvDepartamentos.Items[i].Text + ",";
                }
            }

            if (idDepartamentos != "")
            {
                lstvEmpleados.Items.Clear();
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
                eh.Command = cmd;

                lstEmp = new List<Empleados.Core.Empleados>();
                idDepartamentos = idDepartamentos.Substring(0, idDepartamentos.Length - 1);
                try
                {
                    cnx.Open();
                    lstEmp = eh.obtenerEmpleadoPorDepto(idDepartamentos);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al obtener el listado de los empleados.", "Error");
                    cnx.Dispose();
                    return;
                }

                for (int i = 0; i < lstEmp.Count; i++)
                {
                    ListViewItem Lista;
                    Lista = lstvEmpleados.Items.Add(lstEmp[i].idtrabajador.ToString());
                    Lista.SubItems.Add(lstEmp[i].noempleado);
                    Lista.SubItems.Add(lstEmp[i].nombrecompleto);
                }
            }
            else
                lstvEmpleados.Items.Clear();
        }

        private void btnSeleccionarTodos_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstvEmpleados.Items.Count; i++)
                lstvEmpleados.Items[i].Checked = true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstvEmpleados.Items.Count; i++)
                lstvEmpleados.Items[i].Checked = false;
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
                lstvEmpleados.Items.Clear();
                if (string.IsNullOrEmpty(txtBuscar.Text) || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    for (int i = 0; i < lstEmp.Count; i++)
                    {
                        ListViewItem Lista;
                        Lista = lstvEmpleados.Items.Add(lstEmp[i].idtrabajador.ToString());
                        Lista.SubItems.Add(lstEmp[i].noempleado);
                        Lista.SubItems.Add(lstEmp[i].nombrecompleto);
                    }
                }
                else
                {
                    var busqueda = from b in lstEmp
                                   where b.noempleado.Contains(txtBuscar.Text.ToUpper())
                                   select new
                                   {
                                       b.idtrabajador,
                                       b.noempleado,
                                       b.nombrecompleto
                                   };
                    foreach (var emp in busqueda)
                    {
                        ListViewItem Lista;
                        Lista = lstvEmpleados.Items.Add(emp.idtrabajador.ToString());
                        Lista.SubItems.Add(emp.noempleado);
                        Lista.SubItems.Add(emp.nombrecompleto);
                    }
                }
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "No. de empleado";
            txtBuscar.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Italic);
            txtBuscar.ForeColor = System.Drawing.Color.Gray;
        }

        private void btnTodosDeptos_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstvDepartamentos.Items.Count; i++)
                lstvDepartamentos.Items[i].Checked = true;
        }

        private void btnLimpiarDeptos_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstvDepartamentos.Items.Count; i++)
                lstvDepartamentos.Items[i].Checked = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }
    }
}
