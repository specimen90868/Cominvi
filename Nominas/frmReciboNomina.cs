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
    public partial class frmReciboNomina : Form
    {
        public frmReciboNomina()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        CalculoNomina.Core.NominaHelper nh;
        Empleados.Core.EmpleadosHelper eh;
        Conceptos.Core.ConceptosHelper ch;
        #endregion

        #region VARIABLES PUBLICAS
        public int _idEmpleado;
        public DateTime _inicioPeriodo;
        public DateTime _finPeriodo;
        #endregion

        private void frmReciboNomina_Load(object sender, EventArgs e)
        {
            dgvPercepciones.RowHeadersVisible = false;
            dgvPercepciones.ColumnHeadersVisible = false;

            dgvDeducciones.RowHeadersVisible = false;
            dgvDeducciones.ColumnHeadersVisible = false;

            lblPeriodoNomina.Text = _inicioPeriodo.ToString("ddd d MMM yyyy") + " al " + _finPeriodo.ToString("ddd d MMM yyyy");
            reciboEmpleado();
        }

        private void reciboEmpleado()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            concepto.idempresa = GLOBALES.IDEMPRESA;

            List<Conceptos.Core.Conceptos> lstConceptos = new List<Conceptos.Core.Conceptos>();

            try
            {
                cnx.Open();
                lstConceptos = ch.obtenerConceptos(concepto);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Eror: \r\n \r\n" + error.Message, "Error");
            }

            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idtrabajador = _idEmpleado;
            pn.fechainicio = _inicioPeriodo;
            pn.fechafin = _finPeriodo;

            List<CalculoNomina.Core.tmpPagoNomina> lstRecibo = new List<CalculoNomina.Core.tmpPagoNomina>();
            try
            {
                cnx.Open();
                lstRecibo = nh.obtenerDatosRecibo(pn);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            var percepcion = from r in lstRecibo
                             join c in lstConceptos on r.idconcepto equals c.id
                             where c.tipoconcepto.Contains("P") && c.visible == true
                             select new
                             {
                                 NoConcepto = c.noconcepto,
                                 concepto = c.concepto,
                                 Importe = r.cantidad
                             };

            var deduccion = from r in lstRecibo
                            join c in lstConceptos on r.idconcepto equals c.id
                            where c.tipoconcepto.Contains("D") && c.visible == true
                            select new
                            {
                                NoConcepto = c.noconcepto,
                                concepto = c.concepto,
                                Importe = r.cantidad
                            };

            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.Alignment = DataGridViewContentAlignment.MiddleRight;
            estilo.Format = "C2";

            dgvPercepciones.DataSource = percepcion.ToList();
            dgvPercepciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPercepciones.Columns[0].Width = 10;
            dgvPercepciones.Columns[1].Width = 70;
            dgvPercepciones.Columns[2].Width = 90;
            dgvPercepciones.Columns[2].DefaultCellStyle = estilo;

            dgvDeducciones.DataSource = deduccion.ToList();
            dgvDeducciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDeducciones.Columns[0].Width = 10;
            dgvDeducciones.Columns[1].Width = 70;
            dgvDeducciones.Columns[2].Width = 90;
            dgvDeducciones.Columns[2].DefaultCellStyle = estilo;

            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idEmpleado;

            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();

            try 
            {
                cnx.Open();
                lstEmpleado = eh.obtenerEmpleado(empleado);
                cnx.Close();
            }
            catch (Exception error) 
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            lblNoEmpleado.Text = lstEmpleado[0].noempleado;
            lblNombre.Text = lstEmpleado[0].nombrecompleto;

            double sumaPercepciones = 0, sumaDeducciones = 0, netoPagar = 0;
            foreach (DataGridViewRow fila in dgvPercepciones.Rows)
            {
                sumaPercepciones += double.Parse(fila.Cells[2].Value.ToString());
            }

            foreach (DataGridViewRow fila in dgvDeducciones.Rows)
            {
                sumaDeducciones += double.Parse(fila.Cells[2].Value.ToString());
            }

            netoPagar = sumaPercepciones - sumaDeducciones;

            lblSumaPercepciones.Text = sumaPercepciones.ToString("C2");
            lblSumaDeducciones.Text = sumaDeducciones.ToString("C2");

            lblNeto.Text = netoPagar.ToString("C2");
         }

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar b = new frmBuscar();
            b.OnBuscar += b_OnBuscar;
            b._catalogo = GLOBALES.EMPLEADOS;
            b.Show();
        }

        void b_OnBuscar(int id, string nombre)
        {
            _idEmpleado = id;
            reciboEmpleado();
        }
    }
}
