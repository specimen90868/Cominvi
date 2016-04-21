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
        Empleados.Core.EmpleadosHelper eh;
        Periodos.Core.PeriodosHelper ph;
        int periodo = 0;
        DateTime periodoInicio, periodoFin;
        #endregion

        #region VARIABLES PUBLICAS
        public int _idempleado;
        public string _nombreEmpleado;
        #endregion

        #region DELEGADOS
        public delegate void delOnBajaEmpleado(int baja);
        public event delOnBajaEmpleado OnBajaEmpleado;
        #endregion

        private void frmBaja_Load(object sender, EventArgs e)
        {
            lblNombreEmpleado.Text = _nombreEmpleado;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Empleados.Core.EmpleadosHelper();
            ph = new Periodos.Core.PeriodosHelper();
            eh.Command = cmd;
            ph.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idempleado;

            Periodos.Core.Periodos per = new Periodos.Core.Periodos();
            per.idempresa = GLOBALES.IDEMPRESA;

            Catalogos.Core.CatalogosHelper ch = new Catalogos.Core.CatalogosHelper();
            ch.Command = cmd;

            Catalogos.Core.Catalogo c = new Catalogos.Core.Catalogo();
            c.grupodescripcion = "BAJA";

            List<Catalogos.Core.Catalogo> lstBaja = new List<Catalogos.Core.Catalogo>();
            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();
            List<Periodos.Core.Periodos> lstPeriodos = new List<Periodos.Core.Periodos>();

            try
            {
                cnx.Open();
                lstEmpleado = eh.obtenerEmpleado(empleado);
                lstPeriodos = ph.obtenerPeriodos(per);
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

            var datos = from emp in lstEmpleado
                        join p in lstPeriodos on emp.idperiodo equals p.idperiodo
                        select new
                        {
                            p.dias,
                            emp.idperiodo
                        };
            foreach (var d in datos)
            {
                periodo = d.dias;
            }
            
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

            double totalDias = (DateTime.Now.Date - dtpFechaBaja.Value).TotalHours / 24;
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
            //bool excede = CalculaDiaHabil();
            //if (excede)
            //{
            //    MessageBox.Show("Fecha excede los 5 dias hábiles.", "Información");
            //    btnAceptar.Enabled = false;
            //}
            //else
            //    btnAceptar.Enabled = true;
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            int existeVacaciones = 0, existeIncapacidad = 0;
            int diasProporcionales = 0;
            int existeBaja = 0;
            DialogResult respuesta = MessageBox.Show("¿Desea dar de baja al empleado?","Confirmación",MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                //if (ausentismo)
                //{
                //    frmDiasAusentismo da = new frmDiasAusentismo();
                //    da.OnDiasAusentismo += da_OnDiasAusentismo;
                //    da.ShowDialog();
                //}

                //if (diasAusentismo == 0 && ausentismo)
                //{
                //    MessageBox.Show("El número de dias es 0 o se presionó el boton cancelar. Por favor revisar.", "Error");
                //    return;
                //}

                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                PeriodoFechaAplicacion();
                
                #region EXISTENCIA DE INCAPACIDAD
                Incidencias.Core.IncidenciasHelper ih = new Incidencias.Core.IncidenciasHelper();
                ih.Command = cmd;

                Incidencias.Core.Incidencias incidencia = new Incidencias.Core.Incidencias();
                incidencia.idtrabajador = _idempleado;
                incidencia.fechainicio = periodoInicio.Date;
                incidencia.fechafin = periodoFin.Date;

               
                try
                {
                    cnx.Open();
                    existeIncapacidad = (int)ih.existeIncidenciaBaja(incidencia);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al obtener existencia de Incapacidad.","Error");
                    cnx.Dispose();
                    return;
                }
                #endregion

                #region EXISTENCIA DE VACACIONES
                Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
                vh.Command = cmd;

                Vacaciones.Core.VacacionesPrima vp = new Vacaciones.Core.VacacionesPrima();
                vp.idtrabajador = _idempleado;
                vp.periodoinicio = periodoInicio;
                vp.periodofin = periodoFin;
                vp.vacacionesprima = "V";

               
                try
                {
                    cnx.Open();
                    existeVacaciones = (int)vh.existeVacacionesPrima(vp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al obtener existencia de Vacaciones.", "Error");
                    cnx.Dispose();
                    return;
                }
                #endregion

                if (existeIncapacidad != 0)
                {
                    DateTime fechaInicioIncidencia;
                    DateTime fechaFinIncidencia;
                    try
                    {
                        cnx.Open();
                        fechaInicioIncidencia = DateTime.Parse(ih.fechaInicio(incidencia).ToString());
                        fechaFinIncidencia = DateTime.Parse(ih.fechaFin(incidencia).ToString());
                        cnx.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Al obtener las fechas de incapacidad.", "Error");
                        cnx.Dispose();
                        return;
                    }

                    if (dtpFechaBaja.Value.Date >= fechaInicioIncidencia.Date && dtpFechaBaja.Value.Date <= fechaFinIncidencia.Date)
                    {
                        MessageBox.Show("No se puede dar de baja. La fecha de baja esta entre una incapacidad.", "Error");
                        return;
                    }
                    if (dtpFechaBaja.Value.Date <= fechaInicioIncidencia.Date)
                    {
                        MessageBox.Show("No se puede dar de baja. La fecha de baja es menor a la fecha de inicio de incapacidad.", "Error");
                        return;
                    }
                }

                if (existeVacaciones != 0)
                {
                    DateTime fechaInicioVac;
                    DateTime fechaFinVac;

                    try
                    {
                        cnx.Open();
                        fechaInicioVac = DateTime.Parse(vh.fechaInicio(vp).ToString());
                        fechaFinVac = DateTime.Parse(vh.fechaFin(vp).ToString());
                        cnx.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Al obtener las fechas de las vacaciones.", "Error");
                        cnx.Dispose();
                        return;
                    }
                    

                    if (dtpFechaBaja.Value.Date >= fechaInicioVac.Date && dtpFechaBaja.Value.Date <= fechaFinVac.Date)
                    {
                        MessageBox.Show("No se puede dar de baja. La fecha de baja coninciden con vacaciones.", "Error");
                        return;
                    }
                    if (dtpFechaBaja.Value.Date <= fechaInicioVac.Date)
                    {
                        MessageBox.Show("No se puede dar de baja. La fecha de baja es menor a la fecha de inicio de vacaciones.", "Error");
                        return;
                    }
                }

                Empresas.Core.EmpresasHelper ep = new Empresas.Core.EmpresasHelper();
                ep.Command = cmd;

                Empresas.Core.Empresas empresa = new Empresas.Core.Empresas();
                empresa.idempresa = GLOBALES.IDEMPRESA;

                Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
                eh.Command = cmd;

                Empleados.Core.Empleados emp = new Empleados.Core.Empleados();
                emp.idtrabajador = _idempleado;

                Empleados.Core.EmpleadosEstatus ee = new Empleados.Core.EmpleadosEstatus();
                ee.idtrabajador = _idempleado;
                ee.idempresa = GLOBALES.IDEMPRESA;
                ee.estatus = GLOBALES.INACTIVO;

                Historial.Core.HistorialHelper hp = new Historial.Core.HistorialHelper();
                hp.Command = cmd;

                Historial.Core.Historial h = new Historial.Core.Historial();
                h.idtrabajador = _idempleado;
                h.tipomovimiento = GLOBALES.mBAJA;
                h.fecha_imss = dtpFechaBaja.Value;
                h.fecha_sistema = DateTime.Now;
                h.idempresa = GLOBALES.IDEMPRESA;
                h.motivobaja = int.Parse(cmbMotivoBaja.SelectedValue.ToString());

                //Ausentismo.Core.AusentismoHelper ah = new Ausentismo.Core.AusentismoHelper();
                //ah.Command = cmd;

                //Ausentismo.Core.Ausentismo a = new Ausentismo.Core.Ausentismo();
                //a.idtrabajador = _idempleado;
                //a.idempresa = GLOBALES.IDEMPRESA;
                //a.fecha_imss = dtpFechaBaja.Value;
                //a.dias = diasAusentismo;

                Bajas.Core.BajasHelper bh = new Bajas.Core.BajasHelper();
                bh.Command = cmd;

                Bajas.Core.Bajas baja = new Bajas.Core.Bajas();
                baja.idtrabajador = _idempleado;
                baja.idempresa = GLOBALES.IDEMPRESA;
                baja.motivo = int.Parse(cmbMotivoBaja.SelectedValue.ToString());
                baja.fecha = dtpFechaBaja.Value.Date;
                //baja.diasproporcionales = (int)(dtpFechaBaja.Value.Date - periodoInicio.Date).TotalDays + 1;
                baja.periodoinicio = periodoInicio.Date;
                baja.periodofin = periodoFin.Date;
                baja.observaciones = txtObservaciones.Text;

                diasProporcionales = (int)(dtpFechaBaja.Value.Date - periodoInicio.Date).TotalDays + 1;
                if (diasProporcionales == 16)
                    baja.diasproporcionales = diasProporcionales - 1;
                else
                    baja.diasproporcionales = (int)(dtpFechaBaja.Value.Date - periodoInicio.Date).TotalDays + 1;

                try
                {
                    cnx.Open();
                    existeBaja = (int)bh.existeBaja(baja);
                    cnx.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al verificar la existencia de la baja.", "Error");
                    cnx.Dispose();
                    return;
                }

                if (existeBaja != 0)
                {
                    MessageBox.Show(
                        string.Format("El trabajador ya cuenta con una baja en el periodo del: \r\n \r\n {0} al {1}", 
                        periodoInicio.Date.ToShortDateString(), periodoFin.Date.ToShortDateString()),
                        "Información");
                    return;
                }

                try
                {
                    cnx.Open();
                    h.valor = (decimal)eh.obtenerSalarioDiarioIntegrado(emp);
                    hp.insertarHistorial(h);
                    eh.bajaEmpleado(ee);

                    baja.registropatronal = (string)ep.obtenerRegistroPatronal(empresa);
                    baja.nss = (string)eh.obtenerNss(emp);
                    bh.insertaBaja(baja);

                    //if (ausentismo)
                    //{
                    //    a.registropatronal = (string)ep.obtenerRegistroPatronal(empresa);
                    //    a.nss = (string)eh.obtenerNss(emp);
                    //    ah.insertaAusentismo(a);
                    //}

                    cnx.Close();
                    cnx.Dispose();

                    if(OnBajaEmpleado !=  null)
                        OnBajaEmpleado(GLOBALES.ACTIVO);
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }
                MessageBox.Show("Baja exitosa.", "Información");
                btnAceptar.Enabled = false;
                this.Dispose();
            }
            
        }

        private void PeriodoFechaAplicacion()
        {
            if (periodo == 7)
            {
                DateTime dt = dtpFechaBaja.Value.Date;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                periodoInicio = dt;
                periodoFin = dt.AddDays(6);
            }
            else
            {
                if (dtpFechaBaja.Value.Day <= 15)
                {
                    periodoInicio = new DateTime(dtpFechaBaja.Value.Year, dtpFechaBaja.Value.Month, 1);
                    periodoFin = new DateTime(dtpFechaBaja.Value.Year, dtpFechaBaja.Value.Month, 15);
                }
                else
                {
                    periodoInicio = new DateTime(dtpFechaBaja.Value.Year, dtpFechaBaja.Value.Month, 16);
                    periodoFin = new DateTime(dtpFechaBaja.Value.Year, dtpFechaBaja.Value.Month, DateTime.DaysInMonth(dtpFechaBaja.Value.Year, dtpFechaBaja.Value.Month));
                }

            }
        }
    }
}
