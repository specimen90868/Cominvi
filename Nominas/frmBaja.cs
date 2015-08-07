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
    public partial class frmBaja : Form
    {
        public frmBaja()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        bool ausentismo = false;
        int diasAusentismo = 0;
        #endregion

        #region VARIABLES PUBLICAS
        public int _idempleado;
        public string _nombreEmpleado;
        #endregion

        private void frmBaja_Load(object sender, EventArgs e)
        {
            lblNombreEmpleado.Text = _nombreEmpleado;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Catalogos.Core.CatalogosHelper ch = new Catalogos.Core.CatalogosHelper();
            ch.Command = cmd;

            Catalogos.Core.Catalogo c = new Catalogos.Core.Catalogo();
            c.grupodescripcion = "BAJA";

            List<Catalogos.Core.Catalogo> lstBaja = new List<Catalogos.Core.Catalogo>();

            try
            {
                cnx.Open();
                lstBaja = ch.obtenerGrupo(c);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
            }

            cmbMotivoBaja.DataSource = lstBaja.ToList();
            cmbMotivoBaja.DisplayMember = "descripcion";
            cmbMotivoBaja.ValueMember = "id";
        }

        private bool CalculaDiaHabil()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            DiasFestivos.Core.DiasFestivosHelper dh = new DiasFestivos.Core.DiasFestivosHelper();
            dh.Command = cmd;
            List<DiasFestivos.Core.DiasFestivos> lstDias = new List<DiasFestivos.Core.DiasFestivos>();
            try
            {
                cnx.Open();
                lstDias = dh.obtenerDiasFestivos();
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n " + error.Message,"Error");
            }

            double totalDias = (DateTime.Now - dtpFechaBaja.Value).TotalHours / 24;
            double contador = 0;
            double diasNoLaborables = 0;
            double diasHabiles = 0;
            DateTime dia;

            totalDias = Math.Truncate(totalDias);
            while (contador < totalDias)
            {
                dia = dtpFechaBaja.Value.AddDays(contador);
                if (dia.DayOfWeek == DayOfWeek.Saturday || dia.DayOfWeek == DayOfWeek.Sunday)
                {
                    diasNoLaborables += 1;
                }
                contador += 1;
            }

            var feriado = (from d in lstDias
                           where d.diafestivo >= dtpFechaBaja.Value && d.diafestivo <= DateTime.Now
                           select d.id).Count();
            diasHabiles = totalDias - diasNoLaborables - (double)feriado;

            if (diasHabiles > 5)
                return true;
            else
                return false;
        }

        private void dtpFechaBaja_Leave(object sender, EventArgs e)
        {
            bool excede = CalculaDiaHabil();
            if (excede)
            {
                MessageBox.Show("Fecha excede los 5 dias hábiles.", "Información");
                btnAceptar.Enabled = false;
            }
            else
                btnAceptar.Enabled = true;
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Desea dar de baja al empleado?","Confirmación",MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                if (ausentismo)
                {
                    frmDiasAusentismo da = new frmDiasAusentismo();
                    da.OnDiasAusentismo += da_OnDiasAusentismo;
                    da.ShowDialog();
                }

                if (diasAusentismo == 0 && ausentismo)
                {
                    MessageBox.Show("El número de dias es 0 o se presionó el boton cancelar. Por favor revisar.", "Error");
                    return;
                }

                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                Empresas.Core.EmpresasHelper ep = new Empresas.Core.EmpresasHelper();
                ep.Command = cmd;

                Empresas.Core.Empresas empresa = new Empresas.Core.Empresas();
                empresa.idempresa = GLOBALES.IDEMPRESA;

                Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
                eh.Command = cmd;

                Empleados.Core.Empleados emp = new Empleados.Core.Empleados();
                emp.idtrabajador = _idempleado;
                emp.estatus = GLOBALES.INACTIVO;

                Historial.Core.HistorialHelper hp = new Historial.Core.HistorialHelper();
                hp.Command = cmd;

                Historial.Core.Historial h = new Historial.Core.Historial();
                h.idtrabajador = _idempleado;
                h.tipomovimiento = GLOBALES.mBAJA;
                h.fecha_imss = dtpFechaBaja.Value;
                h.fecha_sistema = DateTime.Now;
                h.idempresa = GLOBALES.IDEMPRESA;
                h.motivobaja = int.Parse(cmbMotivoBaja.SelectedValue.ToString());

                Ausentismo.Core.AusentismoHelper ah = new Ausentismo.Core.AusentismoHelper();
                ah.Command = cmd;

                Ausentismo.Core.Ausentismo a = new Ausentismo.Core.Ausentismo();
                a.idtrabajador = _idempleado;
                a.idempresa = GLOBALES.IDEMPRESA;
                a.fecha_imss = dtpFechaBaja.Value;
                a.dias = diasAusentismo;

                try
                {
                    cnx.Open();
                    h.valor = (double)(decimal)eh.obtenerSalarioDiarioIntegrado(emp);
                    hp.insertarHistorial(h);
                    eh.bajaEmpleado(emp);

                    if (ausentismo)
                    {
                        a.registropatronal = (string)ep.obtenerRegistroPatronal(empresa);
                        a.nss = (string)eh.obtenerNss(emp);
                        ah.insertaAusentismo(a);
                    }

                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
                MessageBox.Show("Baja exitosa.", "Información");
                btnAceptar.Enabled = false;
            }
        }

        void da_OnDiasAusentismo(int dias)
        {
            diasAusentismo = dias;
        }

        private void cmbMotivoBaja_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMotivoBaja.Text.Equals("AUSENTISMO"))
                ausentismo = true;
            else
                ausentismo = false;
        }
    }
}
