using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nominas
{
    public partial class frmListaCalculoNomina : Form
    {
        public frmListaCalculoNomina()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        SqlBulkCopy bulk;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;       
        string NetoCero, Orden;
        bool FLAGCARGA = false;
        bool FLAGPRIMERCALCULO = true;
        bool EXISTEPRENOMINA = false;

        CalculoNomina.Core.NominaHelper nh;
        Faltas.Core.FaltasHelper fh;
        List<CalculoNomina.Core.tmpPagoNomina> lstValoresNomina;
        List<CalculoNomina.Core.DatosEmpleado> lstEmpleadosNomina;
        List<CalculoNomina.Core.DatosFaltaIncapacidad> lstEmpleadosFaltaIncapacidad;
        DateTime periodoInicio, periodoFin;
        DateTime periodoInicioPosterior, periodoFinPosterior;
        #endregion

        #region VARIABLES PUBLICAS
        public int _periodo;
        public int _tipoNomina;
        public bool _obracivil;
        #endregion

        private void toolCargar_Click(object sender, EventArgs e)
        {
            frmListaCargaMovimientos lcm = new frmListaCargaMovimientos();
            lcm._tipoNomina = _tipoNomina;
            lcm._periodo = _periodo;
            lcm._inicioPeriodo = periodoInicio.Date;
            lcm._finPeriodo = periodoFin.Date;
            lcm.ShowDialog();
            if (_tipoNomina == GLOBALES.EXTRAORDINARIO_NORMAL)
                movimientosEspeciales();
        }

        private void movimientosEspeciales()
        {
            string idtrabajadores = "";

            dgvEmpleados.Rows.Clear();
            
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = periodoInicio;
            pn.fechafin = periodoFin;
            pn.obracivil = false;

            List<CalculoNomina.Core.tmpPagoNomina> lstPreNominaEspecial = new List<CalculoNomina.Core.tmpPagoNomina>();
            List<CalculoNomina.Core.DatosEmpleado> lstEmp = new List<CalculoNomina.Core.DatosEmpleado>();

            try
            {
                cnx.Open();
                lstPreNominaEspecial = nh.obtenerPreNomina(pn, _periodo);
                if (lstPreNominaEspecial.Count != 0)
                {
                    var dato = lstPreNominaEspecial.GroupBy(id => id.idtrabajador).Select(grp => grp.First()).ToList();
                    foreach (var a in dato)
                    {
                        idtrabajadores += a.idtrabajador.ToString() + ",";
                    }
                    idtrabajadores = idtrabajadores.Substring(0, idtrabajadores.Length - 1);
                    lstEmp = nh.obtenerDatosEmpleado(GLOBALES.IDEMPRESA, idtrabajadores, _periodo);
                }
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Nomina especial. \r\n \r\n" + error.Message, "Error");
            }

            if (lstPreNominaEspecial.Count != 0)
            {
                dgvEmpleados.Columns["idtrabajador"].DataPropertyName = "idtrabajador";
                dgvEmpleados.Columns["iddepartamento"].DataPropertyName = "iddepartamento";
                dgvEmpleados.Columns["idpuesto"].DataPropertyName = "idpuesto";
                dgvEmpleados.Columns["noempleado"].DataPropertyName = "noempleado";
                dgvEmpleados.Columns["nombres"].DataPropertyName = "nombres";
                dgvEmpleados.Columns["paterno"].DataPropertyName = "paterno";
                dgvEmpleados.Columns["materno"].DataPropertyName = "materno";
                dgvEmpleados.Columns["sueldo"].DataPropertyName = "sueldo";
                dgvEmpleados.Columns["despensa"].DataPropertyName = "despensa";
                dgvEmpleados.Columns["asistencia"].DataPropertyName = "asistencia";
                dgvEmpleados.Columns["puntualidad"].DataPropertyName = "puntualidad";
                dgvEmpleados.Columns["horas"].DataPropertyName = "horas";
                dgvEmpleados.DataSource = lstEmp;

                foreach (DataGridViewRow fila in dgvEmpleados.Rows)
                {
                    for (int i = 0; i < lstPreNominaEspecial.Count; i++)
                    {
                        if ((int)fila.Cells["idtrabajador"].Value == lstPreNominaEspecial[i].idtrabajador)
                        {
                            switch (lstPreNominaEspecial[i].noconcepto)
                            {
                                case 1:
                                    fila.Cells["sueldo"].Value = lstPreNominaEspecial[i].cantidad;
                                    break;
                                case 2:
                                    fila.Cells["horas"].Value = lstPreNominaEspecial[i].cantidad;
                                    break;
                                case 3:
                                    fila.Cells["asistencia"].Value = lstPreNominaEspecial[i].cantidad;
                                    break;
                                case 5:
                                    fila.Cells["puntualidad"].Value = lstPreNominaEspecial[i].cantidad;
                                    break;
                                case 6:
                                    fila.Cells["despensa"].Value = lstPreNominaEspecial[i].cantidad;
                                    break;
                            }
                        }
                    }
                }
                calculoNoPeriodo();
            }
        }

        private void frmListaCalculoNomina_Load(object sender, EventArgs e)
        {
            FLAGPRIMERCALCULO = false;

            if (_obracivil)
            {
                toolGuardar.Visible = true;
                toolAutorizar.Visible = false;
                toolSeparadorAutorizar.Visible = false;
            }
            else
            {
                toolGuardar.Visible = false;
                toolAutorizar.Visible = true;
                toolSeparadorGuardar.Visible = false;
            }
            

            if (_tipoNomina == GLOBALES.NORMAL)
                CargaPerfil("Normal");
            if (_tipoNomina == GLOBALES.EXTRAORDINARIO_NORMAL)
                CargaPerfil("Normal");
            obtenerPeriodoCalculo();

            #region DISEÑO EXTRA DEL GRID EMPLEADOS
            //chk = new CheckBox();
            //Rectangle rect = dgvEmpleados.GetCellDisplayRectangle(0, -1, true);
            //chk.Size = new Size(18, 18);
            //chk.Location = new Point(50, 10);
            //chk.CheckedChanged += new EventHandler(chk_CheckedChanged);
            //dgvEmpleados.Controls.Add(chk);
            //DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            //estilo.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvEmpleados.Columns["seleccion"].HeaderCell.Style = estilo;
            //dgvEmpleados.Columns["seleccion"].Visible = false;
            //chk.Visible = false;
            #endregion

            disenoGridFaltas();

            if (_tipoNomina != GLOBALES.EXTRAORDINARIO_NORMAL)
            {
                CargaPreNomina(periodoInicio, periodoFin);
                if (!EXISTEPRENOMINA)
                    calcular();
            }
            else
                movimientosEspeciales();
        }

        private void obtenerPeriodoCalculo()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            List<CalculoNomina.Core.tmpPagoNomina> lstUltimaNomina = new List<CalculoNomina.Core.tmpPagoNomina>();

            if (_tipoNomina != GLOBALES.EXTRAORDINARIO_NORMAL)
            {
                try
                {
                    cnx.Open();
                    lstUltimaNomina = nh.obtenerUltimaNomina(GLOBALES.IDEMPRESA, GLOBALES.NORMAL, _periodo);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }

                if (lstUltimaNomina.Count != 0)
                {
                    if (_periodo == 7)
                    {
                        periodoInicio = lstUltimaNomina[0].fechafin.AddDays(1);
                        periodoFin = lstUltimaNomina[0].fechafin.AddDays(7);
                        periodoInicioPosterior = periodoFin.AddDays(1);
                        periodoFinPosterior = periodoFin.AddDays(7);

                        if (_obracivil)
                        {
                            try
                            {
                                lstUltimaNomina = new List<CalculoNomina.Core.tmpPagoNomina>();
                                cnx.Open();
                                lstUltimaNomina = nh.obtenerUltimaNomina(GLOBALES.IDEMPRESA, _obracivil);
                                cnx.Close();

                                if (lstUltimaNomina[0].guardada)
                                {
                                    periodoInicio = lstUltimaNomina[0].fechafin.AddDays(1);
                                    periodoFin = lstUltimaNomina[0].fechafin.AddDays(7);
                                    periodoInicioPosterior = periodoFin.AddDays(1);
                                    periodoFinPosterior = periodoFin.AddDays(7);
                                }
                                else
                                {
                                    periodoInicio = lstUltimaNomina[0].fechainicio;
                                    periodoFin = lstUltimaNomina[0].fechafin;
                                    periodoInicioPosterior = periodoFin.AddDays(1);
                                    periodoFinPosterior = periodoFin.AddDays(7);
                                }
                            }
                            catch (Exception error)
                            {
                                MessageBox.Show("Error: PreNomina de Obra Civil.\r\n \r\n" + error.Message, "Error");
                            }
                        }
                    }
                    else
                    {
                        periodoInicio = lstUltimaNomina[0].fechafin.AddDays(1);
                        
                        if (periodoInicio.Day <= 15)
                            periodoFin = lstUltimaNomina[0].fechafin.AddDays(15);
                        else
                            periodoFin = new DateTime(periodoInicio.Year, periodoInicio.Month, 
                                DateTime.DaysInMonth(periodoInicio.Year, periodoInicio.Month));

                        periodoInicioPosterior = periodoFin.AddDays(1);
                        if (periodoInicioPosterior.Day <= 15)
                            periodoFinPosterior = periodoFin.AddDays(15);
                        else
                            periodoFinPosterior = new DateTime(periodoInicioPosterior.Year, periodoInicioPosterior.Month,
                                DateTime.DaysInMonth(periodoInicioPosterior.Year, periodoInicioPosterior.Month));
                    }

                }
                else
                {
                    List<CalculoNomina.Core.tmpPagoNomina> lstPreNomina = new List<CalculoNomina.Core.tmpPagoNomina>();

                    try
                    {
                        cnx.Open();
                        lstPreNomina = nh.obtenerPreNominaTemp(GLOBALES.IDEMPRESA, _obracivil, _periodo);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                    }

                    if (lstPreNomina.Count == 0)
                    {
                        DialogResult respuesta = MessageBox.Show("Primer periodo de calculo. ¿Desea elegirlo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (DialogResult.Yes == respuesta)
                        {
                            frmCambioPeriodo cp = new frmCambioPeriodo();
                            cp._periodo = _periodo;
                            cp._tipoNomina = _tipoNomina;
                            cp.OnNuevoPeriodo += cp_OnNuevoPeriodo;
                            cp.ShowDialog();
                        }
                        else
                        {
                            DateTime dt = DateTime.Now.Date;
                            if (_periodo == 7)
                            {
                                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                                periodoInicio = dt;
                                periodoFin = dt.AddDays(6);
                                periodoInicioPosterior = periodoFin.AddDays(1);
                                periodoFinPosterior = periodoFin.AddDays(7);
                            }
                            else
                            {

                                if (dt.Day <= 15)
                                {
                                    periodoInicio = new DateTime(dt.Year, dt.Month, 1);
                                    periodoFin = new DateTime(dt.Year, dt.Month, 15);
                                    periodoInicioPosterior = periodoFin.AddDays(1);
                                    periodoFinPosterior = new DateTime(periodoInicioPosterior.Year, periodoInicioPosterior.Month,
                                        DateTime.DaysInMonth(periodoInicioPosterior.Year, periodoInicioPosterior.Month) - 15);
                                }
                                else
                                {
                                    periodoInicio = new DateTime(dt.Year, dt.Month, 16);
                                    periodoFin = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
                                    periodoInicioPosterior = periodoFin.AddDays(1);
                                    periodoFinPosterior = periodoFin.AddDays(15);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (_periodo == 7)
                        {
                            periodoInicio = lstPreNomina[0].fechainicio.Date;
                            periodoFin = lstPreNomina[0].fechafin.Date;
                            periodoInicioPosterior = periodoFin.AddDays(1);
                            periodoFinPosterior = periodoFin.AddDays(7);
                        }
                        else
                        {
                            periodoInicio = lstPreNomina[0].fechainicio.Date;
                            periodoFin = lstPreNomina[0].fechafin.Date;
                            if (periodoInicio.Day <= 15)
                            {
                                periodoInicioPosterior = periodoFin.AddDays(1);
                                periodoFinPosterior = new DateTime(periodoInicioPosterior.Year, periodoInicioPosterior.Month,
                                    DateTime.DaysInMonth(periodoInicioPosterior.Year, periodoInicioPosterior.Month) - 15);
                            }
                            else
                            {
                                periodoInicioPosterior = periodoFin.AddDays(1);
                                periodoFinPosterior = periodoFin.AddDays(15);
                            }
                        }
                    }
                }
                this.Text = String.Format("Periodo de Pago: Del {0} al {1}.", periodoInicio.ToShortDateString(), periodoFin.ToShortDateString());
                toolPeriodo.Text = String.Format("Periodo de Pago: Del {0} al {1}.", periodoInicio.ToShortDateString(), periodoFin.ToShortDateString());
            }
            else
            {
                periodoInicio = DateTime.Now.Date;
                periodoFin = DateTime.Now.Date;

                this.Text = String.Format("Pago extraordinario: Del {0}.", periodoInicio.ToShortDateString());
                toolPeriodo.Text = String.Format("Pago extraordinario: Del {0}.", periodoInicio.ToShortDateString());
            }
        }

        private void disenoGridFaltas()
        {
            DateTime inicioGrid = periodoInicio;
            DateTime finGrid = periodoFin;

            dgvFaltas.Columns.Add("idtrabajadorfalta","Id");
            dgvFaltas.Columns.Add("iddepartamentofalta", "IdDepartamento");
            dgvFaltas.Columns.Add("idpuestofalta", "IdPuesto");
            dgvFaltas.Columns.Add("noempleadofalta", "No. Empleado");
            dgvFaltas.Columns.Add("nombrefalta", "Nombre");
            dgvFaltas.Columns.Add("paternofalta", "Ap. Paterno");
            dgvFaltas.Columns.Add("maternofalta", "Ap. Materno");

            dgvFaltas.Columns["idtrabajadorfalta"].Visible = false;
            dgvFaltas.Columns["iddepartamentofalta"].Visible = false;
            dgvFaltas.Columns["idpuestofalta"].Visible = false;

            if (_tipoNomina != GLOBALES.EXTRAORDINARIO_NORMAL)
            {
                if (_periodo == 7)
                {
                    for (int i = 0; i <= 6; i++)
                        dgvFaltas.Columns.Add(inicioGrid.AddDays(i).ToShortDateString(), inicioGrid.AddDays(i).ToShortDateString());
                }
                else
                {
                    if (periodoInicio.Day <= 15)
                    {
                        for (int i = 0; i < 15; i++)
                            dgvFaltas.Columns.Add(inicioGrid.AddDays(i).ToShortDateString(), inicioGrid.AddDays(i).ToShortDateString());
                    }
                    else
                    {
                        int diasMes = DateTime.DaysInMonth(inicioGrid.Year, inicioGrid.Month);
                        for (int i = 0; i < (diasMes - 15); i++)
                            dgvFaltas.Columns.Add(inicioGrid.AddDays(i).ToShortDateString(), inicioGrid.AddDays(i).ToShortDateString());
                    }
                }
            }
            else
            {
                dgvFaltas.Columns.Add(periodoInicio.ToShortDateString(), periodoInicio.ToShortDateString());
            }
            

            dgvFaltas.Columns["idtrabajadorfalta"].DataPropertyName = "idtrabajador";
            dgvFaltas.Columns["iddepartamentofalta"].DataPropertyName = "iddepartamento";
            dgvFaltas.Columns["idpuestofalta"].DataPropertyName = "idpuesto";
            dgvFaltas.Columns["noempleadofalta"].DataPropertyName = "noempleado";
            dgvFaltas.Columns["nombrefalta"].DataPropertyName = "nombres";
            dgvFaltas.Columns["paternofalta"].DataPropertyName = "paterno";
            dgvFaltas.Columns["maternofalta"].DataPropertyName = "materno";
        }

        private void borraGridFaltas()
        {
            dgvFaltas.DataSource = null;
            dgvFaltas.Columns.Clear();
        }

        private void cargaEmpleados()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            #region DISEÑO DEL GRIDVIEW

            dgvEmpleados.Columns["idtrabajador"].DataPropertyName = "idtrabajador";
            dgvEmpleados.Columns["iddepartamento"].DataPropertyName = "iddepartamento";
            dgvEmpleados.Columns["idpuesto"].DataPropertyName = "idpuesto";
            dgvEmpleados.Columns["noempleado"].DataPropertyName = "noempleado";
            dgvEmpleados.Columns["nombres"].DataPropertyName = "nombres";
            dgvEmpleados.Columns["paterno"].DataPropertyName = "paterno";
            dgvEmpleados.Columns["materno"].DataPropertyName = "materno";
            dgvEmpleados.Columns["sueldo"].DataPropertyName = "sueldo";
            dgvEmpleados.Columns["despensa"].DataPropertyName = "despensa";
            dgvEmpleados.Columns["asistencia"].DataPropertyName = "asistencia";
            dgvEmpleados.Columns["puntualidad"].DataPropertyName = "puntualidad";
            dgvEmpleados.Columns["horas"].DataPropertyName = "horas";

            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvEmpleados.Columns[7].DefaultCellStyle = estilo;
            dgvEmpleados.Columns[8].DefaultCellStyle = estilo;
            dgvEmpleados.Columns[9].DefaultCellStyle = estilo;
            dgvEmpleados.Columns[10].DefaultCellStyle = estilo;
            dgvEmpleados.Columns[11].DefaultCellStyle = estilo;

            dgvEmpleados.Columns["noempleado"].ReadOnly = true;
            dgvEmpleados.Columns["nombres"].ReadOnly = true;
            dgvEmpleados.Columns["paterno"].ReadOnly = true;
            dgvEmpleados.Columns["materno"].ReadOnly = true;
            #endregion

            #region LISTADO DE EMPLEADOS GRID
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            lstEmpleadosNomina = new List<CalculoNomina.Core.DatosEmpleado>();
            lstEmpleadosFaltaIncapacidad = new List<CalculoNomina.Core.DatosFaltaIncapacidad>();
            DateTime fecha = new DateTime(1900,1,1);
            DateTime fechaf = new DateTime(1900, 1, 1);
            try
            {
                
                //if (_tipoNomina == GLOBALES.NORMAL || _tipoNomina == GLOBALES.EXTRAORDINARIO_NORMAL)
                //{
                    
                //}

                if (_periodo == 7)
                {
                    fecha = periodoFin.AddDays(1);
                    fechaf = periodoFin.AddDays(7);
                }
                else
                {
                    if (periodoInicio.Day < 15)
                    {
                        fecha = periodoFin.AddDays(1);
                        fechaf = periodoFin.AddDays(DateTime.DaysInMonth(periodoInicio.Year, periodoInicio.Month) - 15);
                    }
                    else
                    {
                        fecha = periodoFin.AddDays(1);
                        fechaf = periodoFin.AddDays(15);
                    }
                }
                cnx.Open();
                lstEmpleadosNomina = nh.obtenerDatosEmpleado(GLOBALES.IDEMPRESA, GLOBALES.ACTIVO, _obracivil, fecha, fechaf, _periodo);
                lstEmpleadosFaltaIncapacidad = nh.obtenerDatosFaltaInc(GLOBALES.IDEMPRESA, GLOBALES.ACTIVO, _obracivil, fecha, fechaf, _periodo);

                //if (_tipoNomina == GLOBALES.ESPECIAL || _tipoNomina == GLOBALES.EXTRAORDINARIO_ESPECIAL)
                //{
                //    lstEmpleadosNomina = nh.obtenerDatosEmpleado(GLOBALES.IDEMPRESA, GLOBALES.INACTIVO, _obracivil);
                //    lstEmpleadosFaltaIncapacidad = nh.obtenerDatosFaltaInc(GLOBALES.IDEMPRESA, GLOBALES.INACTIVO, _obracivil);
                //}

                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            dgvEmpleados.DataSource = lstEmpleadosNomina;
            dgvFaltas.DataSource = lstEmpleadosFaltaIncapacidad;

            for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
                dgvEmpleados.AutoResizeColumn(i);

            for (int i = 1; i < dgvFaltas.Columns.Count; i++)
                dgvFaltas.AutoResizeColumn(i);
            #endregion
        }

        private void cargaEmpleadosFaltas()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            #region LISTADO DE EMPLEADOS GRID FALTAS
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            lstEmpleadosFaltaIncapacidad = new List<CalculoNomina.Core.DatosFaltaIncapacidad>();
            DateTime fecha;
            DateTime fechaf;
            try
            {
                cnx.Open();
                if (_tipoNomina == GLOBALES.NORMAL || _tipoNomina == GLOBALES.EXTRAORDINARIO_NORMAL)
                {
                    if (_periodo == 7)
                    {
                        fecha = periodoInicio.AddDays(7);
                        fechaf = periodoFin.AddDays(7);
                    }
                    else
                    {
                        if (periodoInicio.Day < 15)
                        {
                            fecha = periodoInicio.AddDays(15);
                            fechaf = periodoFin.AddDays(DateTime.DaysInMonth(periodoInicio.Year, periodoInicio.Month) - 15);
                        }
                        else
                        {
                            fecha = periodoInicio.AddDays(
                                DateTime.DaysInMonth(periodoInicio.Year, periodoInicio.Month) - 15);
                            fechaf = periodoFin.AddDays(15);
                        }
                    }
                    lstEmpleadosFaltaIncapacidad = nh.obtenerDatosFaltaInc(GLOBALES.IDEMPRESA, GLOBALES.ACTIVO, _obracivil, fecha, fechaf, _periodo);
                }
                
                //if (_tipoNomina == GLOBALES.ESPECIAL || _tipoNomina == GLOBALES.EXTRAORDINARIO_ESPECIAL)
                //    lstEmpleadosFaltaIncapacidad = nh.obtenerDatosFaltaInc(GLOBALES.IDEMPRESA, GLOBALES.INACTIVO, _obracivil);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            dgvFaltas.DataSource = lstEmpleadosFaltaIncapacidad;
            for (int i = 1; i < dgvFaltas.Columns.Count; i++)
                dgvFaltas.AutoResizeColumn(i);

            #endregion
        }

        private void CargaPerfil(string nombre)
        {
            List<Autorizaciones.Core.Ediciones> lstEdiciones = GLOBALES.PERFILEDICIONES(nombre);

            for (int i = 0; i < lstEdiciones.Count; i++)
            {
                switch (lstEdiciones[i].permiso.ToString())
                {
                    case "Prenomina":
                        //toolPrenomina.Enabled = Convert.ToBoolean(lstEdiciones[i].accion);
                        break;
                    case "Calcular": toolCalcular.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Cargar Faltas": toolStripButton1.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Autorizar": toolAutorizar.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;
                    case "Reportes": toolReportes.Enabled = Convert.ToBoolean(lstEdiciones[i].accion); break;

                }
            }
        }

        //void chk_CheckedChanged(object sender, EventArgs e)
        //{
        //    foreach (DataGridViewRow row in dgvEmpleados.Rows)
        //        if (chk.Checked)
        //            row.Cells[0].Value = true;
        //        else
        //            row.Cells[0].Value = false;
        //}

        private void toolDepartamento_Click(object sender, EventArgs e)
        {
            frmFiltroNomina fn = new frmFiltroNomina();
            fn._filtro = 0;
            fn._tipoNomina = _tipoNomina;
            fn.OnFiltro += fn_OnFiltro;
            fn.ShowDialog();
        }

        void fn_OnFiltro(int filtro, int de, int hasta)
        {
            if (de.Equals(DBNull.Value) || hasta.Equals(DBNull.Value) || de == 0 || hasta == 0)
                return;

            switch (filtro)
            { 
                case 0:
                    var empleadoDepto = from f in lstEmpleadosNomina where f.iddepartamento >= de && f.iddepartamento <= hasta orderby f.noempleado ascending select f;
                    dgvEmpleados.DataSource = empleadoDepto.ToList();
                    for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
                    {
                        dgvEmpleados.AutoResizeColumn(i);
                    }

                    var empleadoDeptoFalta = from f in lstEmpleadosFaltaIncapacidad where f.iddepartamento >= de && f.iddepartamento <= hasta orderby f.noempleado ascending select f;
                    dgvFaltas.DataSource = empleadoDeptoFalta.ToList();
                    for (int i = 1; i < dgvFaltas.Columns.Count; i++)
                    {
                        dgvFaltas.AutoResizeColumn(i);
                    }
                    
                    break;
                case 1:
                    var empleadoPuesto = from f in lstEmpleadosNomina where f.idpuesto >= de && f.idpuesto <= hasta orderby f.noempleado ascending select f;
                    dgvEmpleados.DataSource = empleadoPuesto.ToList();
                    for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
                    {
                        dgvEmpleados.AutoResizeColumn(i);
                    }

                    var empleadoPuestoFalta = from f in lstEmpleadosFaltaIncapacidad where f.idpuesto >= de && f.idpuesto <= hasta orderby f.noempleado ascending select f;
                    dgvFaltas.DataSource = empleadoPuestoFalta.ToList();
                    for (int i = 1; i < dgvFaltas.Columns.Count; i++)
                    {
                        dgvFaltas.AutoResizeColumn(i);
                    }
                    
                    break;
                case 2:
                    var empleadoNoEmpleado = from f in lstEmpleadosNomina where f.idtrabajador >= de && f.idtrabajador <= hasta orderby f.noempleado ascending select f;
                    dgvEmpleados.DataSource = empleadoNoEmpleado.ToList();
                    for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
                    {
                        dgvEmpleados.AutoResizeColumn(i);
                    }

                    var empleadoNoEmpleadoFalta = from f in lstEmpleadosFaltaIncapacidad where f.idtrabajador >= de && f.idtrabajador <= hasta orderby f.noempleado ascending select f;
                    dgvFaltas.DataSource = empleadoNoEmpleadoFalta.ToList();
                    for (int i = 1; i < dgvFaltas.Columns.Count; i++)
                    {
                        dgvFaltas.AutoResizeColumn(i);
                    }
                   
                    break;
            }

            
        }

        private void toolPuesto_Click(object sender, EventArgs e)
        {
            frmFiltroNomina fn = new frmFiltroNomina();
            fn._filtro = 1;
            fn._tipoNomina = _tipoNomina;
            fn.OnFiltro += fn_OnFiltro;
            fn.ShowDialog();
        }

        private void toolTodos_Click(object sender, EventArgs e)
        {
            dgvEmpleados.DataSource = lstEmpleadosNomina;
            for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
            {
                dgvEmpleados.AutoResizeColumn(i);
            }

            dgvFaltas.DataSource = lstEmpleadosFaltaIncapacidad;
            for (int i = 1; i < dgvFaltas.Columns.Count; i++)
            {
                dgvFaltas.AutoResizeColumn(i);
            }
        }

        private void dgvEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvEmpleados.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void toolCalcular_Click(object sender, EventArgs e)
        {
            if (_tipoNomina == GLOBALES.NORMAL)
            {
                toolCalcular.Enabled = false;
                calcular();
            }
            else
            {
                toolPorcentaje.Text = "Completado.";
                toolEtapa.Text = " ";
            }

        }

        private void calcular()
        {
            workerCalculo.RunWorkerAsync();
        }

        private void BulkData(List<CalculoNomina.Core.tmpPagoNomina> lstValores)
        {
            #region BULK DATA
            DataTable dt = new DataTable();
            DataRow dtFila;
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("idtrabajador", typeof(Int32));
            dt.Columns.Add("idempresa", typeof(Int32));
            dt.Columns.Add("idconcepto", typeof(Int32));
            dt.Columns.Add("noconcepto", typeof(Int32));
            dt.Columns.Add("tipoconcepto", typeof(String));
            dt.Columns.Add("exento", typeof(Decimal));
            dt.Columns.Add("gravado", typeof(Decimal));
            dt.Columns.Add("cantidad", typeof(Decimal));
            dt.Columns.Add("fechainicio", typeof(DateTime));
            dt.Columns.Add("fechafin", typeof(DateTime));
            dt.Columns.Add("noperiodo", typeof(Int32));
            dt.Columns.Add("diaslaborados", typeof(Int32));
            dt.Columns.Add("guardada", typeof(Boolean));
            dt.Columns.Add("tiponomina", typeof(Int32));
            dt.Columns.Add("modificado", typeof(Boolean));
            dt.Columns.Add("fechapago", typeof(DateTime));
            dt.Columns.Add("obracivil", typeof(Boolean));
            dt.Columns.Add("periodo", typeof(Int32));
            dt.Columns.Add("anio",typeof(Int32));
                        
            for (int i = 0; i < lstValores.Count; i++)
            {
                dtFila = dt.NewRow();
                dtFila["id"] = i + 1;
                dtFila["idtrabajador"] = lstValores[i].idtrabajador;
                dtFila["idempresa"] = lstValores[i].idempresa;
                dtFila["idconcepto"] = lstValores[i].idconcepto;
                dtFila["noconcepto"] = lstValores[i].noconcepto;
                dtFila["tipoconcepto"] = lstValores[i].tipoconcepto;
                dtFila["exento"] = lstValores[i].exento;
                dtFila["gravado"] = lstValores[i].gravado;
                dtFila["cantidad"] = lstValores[i].cantidad;
                dtFila["fechainicio"] = lstValores[i].fechainicio;
                dtFila["fechafin"] = lstValores[i].fechafin;
                dtFila["noperiodo"] = 0;
                dtFila["diaslaborados"] = 0;
                dtFila["guardada"] = lstValores[i].guardada;
                dtFila["tiponomina"] = lstValores[i].tiponomina;
                dtFila["modificado"] = lstValores[i].modificado;
                dtFila["fechapago"] = new DateTime(1900,1,1);
                dtFila["obracivil"] = _obracivil;
                dtFila["periodo"] = _periodo;
                dtFila["anio"] = periodoInicio.Year;
                dt.Rows.Add(dtFila);
            }
            bulk = new SqlBulkCopy(cnx);
            nh.bulkCommand = bulk;

            try
            {
                cnx.Open();
                nh.bulkNomina(dt, "tmpPagoNomina");
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message + "\r\n \r\n Error Bulk Nomina.", "Error");
            }
            #endregion
        }

        #region CALCULO DE LA NOMINA
        private void workerCalculo_DoWork(object sender, DoWorkEventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Altas.Core.AltasHelper ah = new Altas.Core.AltasHelper();
            ah.Command = cmd;

            Reingreso.Core.ReingresoHelper rh = new Reingreso.Core.ReingresoHelper();
            rh.Command = cmd;

            int estatus = 0;
            int existeAltaReingreso = 0;

            #region LISTAS
            List<CalculoNomina.Core.Nomina> lstConceptosPercepciones = new List<CalculoNomina.Core.Nomina>();
            List<CalculoNomina.Core.Nomina> lstConceptosDeducciones = new List<CalculoNomina.Core.Nomina>();

            List<CalculoNomina.Core.Nomina> lstConceptosPercepcionesModificados = new List<CalculoNomina.Core.Nomina>();
            List<CalculoNomina.Core.Nomina> lstConceptosDeduccionesModificados = new List<CalculoNomina.Core.Nomina>();
            #endregion

            int progreso = 0, total = 0, indice = 0;
            int existeConcepto = 0;
            total = dgvEmpleados.Rows.Count;

            StreamWriter swLog = new StreamWriter(@"C:\Temp\LogHealthTrabajador" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" +
                        DateTime.Now.Day.ToString() + "_" + DateTime.Now.Minute.ToString() + ".txt", true);
            swLog.WriteLine(String.Format("Calculo de Nómina: Del {0} al {1}", periodoInicio.ToShortDateString(), periodoFin.ToShortDateString()));

            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                try
                {
                    cnx.Open();
                    estatus = int.Parse(eh.obtenerEstatus(int.Parse(fila.Cells["idtrabajador"].Value.ToString())).ToString());
                    cnx.Close();

                    swLog.WriteLine(String.Format("Empleado: {0}, Estatus: {1}", fila.Cells["idtrabajador"].Value.ToString(), estatus.ToString()));

                    if (estatus == 0)
                    {
                        cnx.Open();
                        nh.eliminaPreNomina(int.Parse(fila.Cells["idtrabajador"].Value.ToString()));
                        cnx.Close();
                        swLog.WriteLine(String.Format("Empleado: {0}, Se elimina Prenomina", fila.Cells["idtrabajador"].Value.ToString()));
                        continue;
                    }
                    else
                    {
                        cnx.Open();
                        existeAltaReingreso = ah.existeAlta(GLOBALES.IDEMPRESA, int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicioPosterior, periodoFinPosterior);
                        cnx.Close();
                        swLog.WriteLine(String.Format("Empleado: {0}, Existencia de Alta: {1}, Fecha Inicio: {2}, Fecha Fin: {3} ", fila.Cells["idtrabajador"].Value.ToString(), existeAltaReingreso, periodoInicioPosterior.ToShortDateString(), periodoFinPosterior.ToShortDateString()));
                        if (existeAltaReingreso != 0)
                        {
                            cnx.Open();
                            nh.eliminaPreNomina(int.Parse(fila.Cells["idtrabajador"].Value.ToString()));
                            cnx.Close();
                            swLog.WriteLine(String.Format("Empleado: {0}, Se elimina Prenomina", fila.Cells["idtrabajador"].Value.ToString()));
                            continue;
                        }

                        cnx.Open();
                        existeAltaReingreso = rh.existeReingreso(GLOBALES.IDEMPRESA, int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicioPosterior, periodoFinPosterior);
                        cnx.Close();
                        swLog.WriteLine(String.Format("Empleado: {0}, Existencia de Reingreso: {1}, Fecha Inicio: {2}, Fecha Fin: {3} ", fila.Cells["idtrabajador"].Value.ToString(), existeAltaReingreso, periodoInicioPosterior.ToShortDateString(), periodoFinPosterior.ToShortDateString()));
                        if (existeAltaReingreso != 0)
                        {
                            cnx.Open();
                            nh.eliminaPreNomina(int.Parse(fila.Cells["idtrabajador"].Value.ToString()));
                            cnx.Close();
                            swLog.WriteLine(String.Format("Empleado: {0}, Se elimina Prenomina", fila.Cells["idtrabajador"].Value.ToString()));
                            continue;
                        }
                        
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error al verificar el estatus del trabajador: " + fila.Cells["noempleado"].Value.ToString() + "\r\n\r\n" + error.Message, "Error");
                    cnx.Close();
                    continue;
                }

                progreso = (indice * 100) / total;
                indice++;

                if (FLAGPRIMERCALCULO)
                {
                    workerCalculo.ReportProgress(progreso, "CARGANDO DATOS DE LOS TRABAJADORES. ESPERE A QUE TERMINE EL PROCESO.");
                }
                else
                    workerCalculo.ReportProgress(progreso, "CALCULANDO.");

                #region CONCEPTOS Y FORMULAS DEL TRABAJADOR
                try
                {
                    cnx.Open();
                    nh.eliminaNominaTrabajador(int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio.Date, periodoFin.Date, _tipoNomina);
                    lstConceptosPercepciones = nh.conceptosNominaTrabajador(GLOBALES.IDEMPRESA, "P", int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio.Date, periodoFin.Date, _periodo);
                    lstConceptosDeducciones = nh.conceptosNominaTrabajador(GLOBALES.IDEMPRESA, "D", int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio.Date, periodoFin.Date, _periodo);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: CONCEPTOS Y FORMULAS DEL TRABAJADOR. Primer Calculo." +
                        " ID Trabajador: " + int.Parse(fila.Cells["idtrabajador"].Value.ToString()) +
                        "\r\n \r\n" + error.Message, "Error");
                    cnx.Dispose();
                    return;
                }
                #endregion

                #region CALCULO DE PERCEPCIONES
                List<CalculoNomina.Core.tmpPagoNomina> lstPercepciones = new List<CalculoNomina.Core.tmpPagoNomina>();
                lstPercepciones = CALCULO.PERCEPCIONES(lstConceptosPercepciones, periodoInicio.Date, periodoFin.Date, _tipoNomina);
                #endregion

                #region BULK DATOS PERCEPCIONES
                BulkData(lstPercepciones);
                #endregion

                #region OBTENCION DE PERCEPCIONES
                lstPercepciones = new List<CalculoNomina.Core.tmpPagoNomina>();
                CalculoNomina.Core.tmpPagoNomina per = new CalculoNomina.Core.tmpPagoNomina();
                per.idempresa = GLOBALES.IDEMPRESA;
                per.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                per.fechainicio = periodoInicio.Date;
                per.fechafin = periodoFin;
                per.tipoconcepto = "P";
                per.tiponomina = _tipoNomina;
                cnx.Open();
                lstPercepciones = nh.obtenerPercepcionesTrabajador(per);
                cnx.Close();
                #endregion

                #region CALCULO DE DEDUCCIONES
                List<CalculoNomina.Core.tmpPagoNomina> lstDeducciones = new List<CalculoNomina.Core.tmpPagoNomina>();
                lstDeducciones = CALCULO.DEDUCCIONES(lstConceptosDeducciones, lstPercepciones, periodoInicio.Date, periodoFin.Date, _tipoNomina);
                #endregion

                #region BULK DATOS DEDUCCIONES
                BulkData(lstDeducciones);
                #endregion

                #region PROGRAMACION DE MOVIMIENTOS
                List<CalculoNomina.Core.tmpPagoNomina> lstOtrasDeducciones = new List<CalculoNomina.Core.tmpPagoNomina>();
                ProgramacionConcepto.Core.ProgramacionHelper pch = new ProgramacionConcepto.Core.ProgramacionHelper();
                pch.Command = cmd;

                decimal percepciones = lstPercepciones.Where(f => f.tipoconcepto == "P").Sum(f => f.cantidad);

                if (percepciones != 0)
                {
                    int existe = 0;
                    ProgramacionConcepto.Core.ProgramacionConcepto programacion = new ProgramacionConcepto.Core.ProgramacionConcepto();
                    programacion.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());

                    List<ProgramacionConcepto.Core.ProgramacionConcepto> lstProgramacion = new List<ProgramacionConcepto.Core.ProgramacionConcepto>();

                    try
                    {
                        cnx.Open();
                        existe = (int)pch.existeProgramacion(programacion);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                        cnx.Dispose();
                    }

                    if (existe != 0)
                    {
                        try
                        {
                            cnx.Open();
                            lstProgramacion = pch.obtenerProgramacion(programacion);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                            cnx.Dispose();
                        }

                        for (int i = 0; i < lstProgramacion.Count; i++)
                        {
                            if (periodoFin.Date <= lstProgramacion[i].fechafin)
                            {
                                Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
                                ch.Command = cmd;
                                Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                                concepto.id = lstProgramacion[i].idconcepto;
                                List<Conceptos.Core.Conceptos> lstNoConcepto = new List<Conceptos.Core.Conceptos>();
                                try
                                {
                                    cnx.Open();
                                    lstNoConcepto = ch.obtenerConcepto(concepto);
                                    cnx.Close();
                                }
                                catch (Exception error) { MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error"); }

                                CalculoNomina.Core.tmpPagoNomina pne = new CalculoNomina.Core.tmpPagoNomina();
                                pne.idempresa = GLOBALES.IDEMPRESA;
                                pne.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                                pne.fechainicio = periodoInicio.Date;
                                pne.fechafin = periodoFin.Date;
                                pne.noconcepto = lstNoConcepto[0].noconcepto;

                                try
                                {
                                    cnx.Open();
                                    existeConcepto = (int)nh.existeConcepto(pne);
                                    cnx.Close();
                                }
                                catch
                                {
                                    MessageBox.Show("Error al obtener la existencia del concepto.", "Error");
                                    cnx.Dispose();
                                }

                                CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                                vn.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                                vn.idempresa = GLOBALES.IDEMPRESA;
                                vn.idconcepto = lstProgramacion[i].idconcepto;
                                vn.noconcepto = lstNoConcepto[0].noconcepto;
                                vn.tipoconcepto = lstNoConcepto[0].tipoconcepto;
                                vn.fechainicio = periodoInicio.Date;
                                vn.fechafin = periodoFin.Date;
                                vn.exento = 0;
                                vn.gravado = 0;
                                vn.cantidad = lstProgramacion[i].cantidad;
                                vn.guardada = false;
                                vn.tiponomina = _tipoNomina;
                                vn.modificado = false;
                                vn.obracivil = _obracivil;

                                if (lstNoConcepto[0].gravado && !lstNoConcepto[0].exento)
                                {
                                    vn.gravado = lstProgramacion[i].cantidad;
                                    vn.exento = 0;
                                }

                                if (lstNoConcepto[0].gravado && lstNoConcepto[0].exento)
                                {
                                    CalculoFormula formulaExcento = new CalculoFormula(int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio.Date, periodoFin.Date, lstNoConcepto[0].formulaexento);
                                    vn.exento = decimal.Parse(formulaExcento.calcularFormula().ToString());
                                    if (vn.cantidad <= vn.exento)
                                    {
                                        vn.exento = vn.cantidad;
                                        vn.gravado = 0;
                                    }
                                    else
                                    {
                                        vn.gravado = vn.cantidad - vn.exento;
                                    }
                                }

                                if (!lstNoConcepto[0].gravado && lstNoConcepto[0].exento)
                                {
                                    vn.gravado = 0;
                                    vn.exento = lstProgramacion[i].cantidad;
                                }

                                if (existeConcepto == 0)
                                {
                                    lstOtrasDeducciones.Add(vn);
                                }
                                else
                                {
                                    try
                                    {
                                        cnx.Open();
                                        nh.actualizaConceptoModificado(vn);
                                        cnx.Close();
                                    }
                                    catch
                                    {
                                        MessageBox.Show("Error al actualizar el concepto.", "Error");
                                        cnx.Dispose();
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region MOVIMIENTOS
                Movimientos.Core.MovimientosHelper mh = new Movimientos.Core.MovimientosHelper();
                mh.Command = cmd;

                if (percepciones != 0)
                {
                    int existe = 0;
                    Movimientos.Core.Movimientos mov = new Movimientos.Core.Movimientos();
                    mov.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                    mov.fechainicio = periodoInicio.Date;
                    mov.fechafin = periodoFin.Date;

                    List<Movimientos.Core.Movimientos> lstMovimiento = new List<Movimientos.Core.Movimientos>();

                    try
                    {
                        cnx.Open();
                        existe = (int)mh.existeMovimiento(mov);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                        cnx.Dispose();
                    }

                    if (existe != 0)
                    {
                        try
                        {
                            cnx.Open();
                            lstMovimiento = mh.obtenerMovimiento(mov);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                            cnx.Dispose();
                        }

                        for (int i = 0; i < lstMovimiento.Count; i++)
                        {
                            Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
                            ch.Command = cmd;
                            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                            concepto.id = lstMovimiento[i].idconcepto;
                            List<Conceptos.Core.Conceptos> lstNoConcepto = new List<Conceptos.Core.Conceptos>();
                            try
                            {
                                cnx.Open();
                                lstNoConcepto = ch.obtenerConcepto(concepto);
                                cnx.Close();
                            }
                            catch (Exception error) { MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error"); }

                            CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                            vn.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString()); ;
                            vn.idempresa = GLOBALES.IDEMPRESA;
                            vn.idconcepto = lstMovimiento[i].idconcepto;
                            vn.noconcepto = lstNoConcepto[0].noconcepto;
                            vn.tipoconcepto = lstNoConcepto[0].tipoconcepto;
                            vn.fechainicio = periodoInicio.Date;
                            vn.fechafin = periodoFin.Date;
                            vn.exento = 0;
                            vn.gravado = 0;
                            vn.cantidad = lstMovimiento[i].cantidad;
                            vn.guardada = false;
                            vn.tiponomina = _tipoNomina;
                            vn.modificado = false;

                            if (lstNoConcepto[0].gravado && !lstNoConcepto[0].exento)
                            {
                                vn.gravado = lstMovimiento[i].cantidad;
                                vn.exento = 0;
                            }

                            if (lstNoConcepto[0].gravado && lstNoConcepto[0].exento)
                            {
                                CalculoFormula formulaExcento = new CalculoFormula(int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio.Date, periodoFin.Date, lstNoConcepto[0].formulaexento);
                                vn.exento = decimal.Parse(formulaExcento.calcularFormula().ToString());
                                if (vn.cantidad <= vn.exento)
                                {
                                    vn.exento = vn.cantidad;
                                    vn.gravado = 0;
                                }
                                else
                                {
                                    vn.gravado = vn.cantidad - vn.exento;
                                }
                            }

                            if (!lstNoConcepto[0].gravado && lstNoConcepto[0].exento)
                            {
                                vn.gravado = 0;
                                vn.exento = lstMovimiento[i].cantidad;
                            }

                            cnx.Open();
                            existe = (int)nh.existeConcepto(vn);
                            cnx.Close();

                            if (existe == 0)
                            {
                                lstOtrasDeducciones.Add(vn);
                            }
                            else
                            {
                                cnx.Open();
                                nh.actualizaConcepto(vn);
                                cnx.Close();
                            }
                        }
                    }
                }
                #endregion

                #region BULK DATOS PROGRAMACION DE MOVIMIENTOS
                BulkData(lstOtrasDeducciones);
                #endregion

                #region APLICACION DE DEPTOS/PUESTOS
                Aplicaciones.Core.AplicacionesHelper aplicah = new Aplicaciones.Core.AplicacionesHelper();
                aplicah.Command = cmd;
                Aplicaciones.Core.Aplicaciones aplicacion;
                List<Aplicaciones.Core.Aplicaciones> lstAplicacion = new List<Aplicaciones.Core.Aplicaciones>();
                aplicacion = new Aplicaciones.Core.Aplicaciones();
                aplicacion.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                aplicacion.periodoinicio = periodoInicio.Date;
                aplicacion.periodofin = periodoFin.Date;
                try
                {
                    cnx.Open();
                    lstAplicacion = aplicah.obtenerFechasDeAplicacion(aplicacion);
                    cnx.Close();
                }
                catch (Exception)
                {

                }

                Empleados.Core.EmpleadosHelper emph = new Empleados.Core.EmpleadosHelper();
                emph.Command = cmd;
                Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
                empleado.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                List<Empleados.Core.Empleados> lstEmpleadoA = new List<Empleados.Core.Empleados>();
                cnx.Open();
                lstEmpleadoA = emph.obtenerEmpleado(empleado);
                cnx.Close();

                if (lstAplicacion.Count != 0)
                {
                    for (int i = 0; i < lstAplicacion.Count; i++)
                    {
                        if (lstAplicacion[i].periodoinicio == periodoInicio.Date && lstAplicacion[i].periodofin == periodoFin)
                        {
                            if (lstAplicacion.Count == 1)
                            {
                                if (lstAplicacion[i].deptopuesto == "D")
                                {
                                    cnx.Open();
                                    aplicah.aplica(int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio.Date, periodoFin.Date, _periodo, "D", lstAplicacion[i].iddeptopuesto);
                                    aplicah.aplica(int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio.Date, periodoFin.Date, _periodo, "P", lstEmpleadoA[0].idpuesto);
                                    cnx.Close();
                                }
                                else if (lstAplicacion[i].deptopuesto == "P")
                                {
                                    cnx.Open();
                                    aplicah.aplica(int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio.Date, periodoFin.Date, _periodo, "D", lstEmpleadoA[0].iddepartamento);
                                    aplicah.aplica(int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio.Date, periodoFin.Date, _periodo, "P", lstAplicacion[i].iddeptopuesto);
                                    cnx.Close();
                                }
                            }
                            else
                            {
                                if (lstAplicacion[i].deptopuesto == "D")
                                {
                                    cnx.Open();
                                    aplicah.aplica(int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio.Date, periodoFin.Date, _periodo, "D", lstAplicacion[i].iddeptopuesto);
                                    cnx.Close();
                                }
                                else if (lstAplicacion[i].deptopuesto == "P")
                                {
                                    cnx.Open();
                                    aplicah.aplica(int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio.Date, periodoFin.Date, _periodo, "P", lstAplicacion[i].iddeptopuesto);
                                    cnx.Close();
                                }
                            }
                        }
                    }
                }
                else
                {
                    cnx.Open();
                    aplicah.aplica(int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio.Date, periodoFin.Date, _periodo, "D", lstEmpleadoA[0].iddepartamento);
                    aplicah.aplica(int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio.Date, periodoFin.Date, _periodo, "P", lstEmpleadoA[0].idpuesto);
                    cnx.Close();
                }
                #endregion
            }
            swLog.Close();

            #region PERIODO
            calculoNoPeriodo();
            #endregion

            #region NETOS NEGATIVOS
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            List<CalculoNomina.Core.NetosNegativos> lstNetos;
            int contadorNetosNegativos = 0;
            StreamWriter sw = new StreamWriter(@"C:\Temp\NetosNegativos" + periodoInicio.Year.ToString() + "_" + periodoInicio.Month.ToString() + "_" +
                        periodoInicio.Day.ToString() + ".txt", true);

            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                lstNetos = new List<CalculoNomina.Core.NetosNegativos>();
                try
                {
                    cnx.Open();
                    lstNetos = nh.obtenerNetosNegativos(GLOBALES.IDEMPRESA, periodoInicio.Date, periodoFin.Date, 
                        int.Parse(fila.Cells["idtrabajador"].Value.ToString()));
                    cnx.Close();

                    decimal sumaPercepciones =  lstNetos.Where(n => n.tipoconcepto == "P").Sum(n => n.cantidad);
                    decimal sumaDeducciones = lstNetos.Where(n => n.tipoconcepto == "D").Sum(n => n.cantidad);
                    decimal subsidio = lstNetos.Where(d => d.noconcepto == 16).Sum(d => d.cantidad);
                    decimal netoPagar = 0;
                    sumaPercepciones = sumaPercepciones + subsidio;
                    sumaDeducciones = sumaDeducciones - subsidio;
                    netoPagar = sumaPercepciones - sumaDeducciones;

                    string linea1 = "";
                    string noEmpleado = "", nombreCompleto = "";
                    noEmpleado = fila.Cells["noempleado"].Value.ToString();
                    nombreCompleto = fila.Cells["nombres"].Value.ToString() + " " + fila.Cells["paterno"].Value.ToString() + " " + fila.Cells["materno"].Value.ToString();
                    if (netoPagar < 0)
                    {
                        contadorNetosNegativos++;
                        linea1 = noEmpleado + ", " + nombreCompleto + ", Cantidad Neta Negativa: " + netoPagar.ToString();
                        sw.WriteLine(linea1);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Lista de Netos. \r\n \r\n" + error.Message, "Error");
                    cnx.Dispose();
                }
            }

            sw.WriteLine("TOTAL CANTIDADES NEGATIVAS: " + contadorNetosNegativos.ToString());
            sw.Close();
            if (contadorNetosNegativos != 0)
            {
                MessageBox.Show("CANTIDADES NEGATIVAS. VERIFIQUE ARCHIVO EN C:\\Temp\\NetosNegativos" +
                   periodoInicio.Year.ToString() + "_" +
                   periodoInicio.Month.ToString() + "_" +
                   periodoInicio.Day.ToString() + ".txt", "Información");
            }

            #endregion
        }

        private void workerCalculo_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolPorcentaje.Text = e.ProgressPercentage.ToString() + "%";
            toolEtapa.Text = e.UserState.ToString();
        }

        private void workerCalculo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolPorcentaje.Text = "Completado.";
            toolEtapa.Text = " ";
            toolCalcular.Enabled = true;
            if (FLAGPRIMERCALCULO)
                FLAGPRIMERCALCULO = false;
        }

        private void calculoNoPeriodo()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            object noPeriodo = 0;
            List<CalculoNomina.Core.PagoNomina> lstPagoNomina = new List<CalculoNomina.Core.PagoNomina>();
            try
            {
                if (_tipoNomina == GLOBALES.NORMAL )
                {
                    cnx.Open();
                    noPeriodo = nh.obtenerNoPeriodo(_periodo, periodoFin).ToString();
                    nh.actualizarNoPeriodo(GLOBALES.IDEMPRESA, periodoInicio.Date, periodoFin.Date, int.Parse(noPeriodo.ToString()));
                    cnx.Close();
                }
                else if (_tipoNomina == GLOBALES.EXTRAORDINARIO_NORMAL)
                {
                    cnx.Open();
                    lstPagoNomina = nh.obtenerNoPeriodoExtraordinario(GLOBALES.IDEMPRESA, _tipoNomina, _periodo);
                    if (lstPagoNomina.Count == 0)
                        noPeriodo = 1;
                    else
                    {
                        noPeriodo = lstPagoNomina[0].noperiodo + 1;
                    }
                    nh.actualizarNoPeriodo(GLOBALES.IDEMPRESA, periodoInicio.Date, periodoFin.Date, int.Parse(noPeriodo.ToString()));
                    cnx.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al actualizar el No. de Periodo", "Error");
                cnx.Dispose();
                return;
            }
        }

        #endregion
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
        private void CargaPreNomina(DateTime inicio, DateTime fin)
        {
            cargaEmpleados();

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
           
            CalculoNomina.Core.tmpPagoNomina pnCargar = new CalculoNomina.Core.tmpPagoNomina();
            pnCargar.idempresa = GLOBALES.IDEMPRESA;
            pnCargar.fechainicio = inicio;
            pnCargar.fechafin = fin;
            pnCargar.obracivil = _obracivil;

            //CalculoNomina.Core.tmpPagoNomina pnGuardada = new CalculoNomina.Core.tmpPagoNomina();
            //pnGuardada.idempresa = GLOBALES.IDEMPRESA;
            //pnGuardada.fechainicio = periodoInicio.Date;
            //pnGuardada.fechafin = periodoFin.Date;
            //pnGuardada.guardada = false;

            List<CalculoNomina.Core.tmpPagoNomina> lstPreNomina = new List<CalculoNomina.Core.tmpPagoNomina>();
          
            try
            {
                cnx.Open();
                lstPreNomina = nh.obtenerPreNomina(pnCargar, _periodo);
                //nh.cargaPreNomina(pnGuardada);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            if (lstPreNomina.Count == 0)
                FLAGPRIMERCALCULO = true;
            else
            {
                FLAGPRIMERCALCULO = false;
                EXISTEPRENOMINA = true;
            }

            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                for (int i = 0; i < lstPreNomina.Count; i++)
                {
                    if ((int)fila.Cells["idtrabajador"].Value == lstPreNomina[i].idtrabajador)
                    {
                        switch (lstPreNomina[i].noconcepto)
                        {
                            case 1:
                                fila.Cells["sueldo"].Value = lstPreNomina[i].cantidad;
                                break;
                            case 2:
                                fila.Cells["horas"].Value = lstPreNomina[i].cantidad;
                                break;
                            case 3:
                                fila.Cells["asistencia"].Value = lstPreNomina[i].cantidad;
                                break;
                            case 5:
                                fila.Cells["puntualidad"].Value = lstPreNomina[i].cantidad;
                                break;
                            case 6:
                                fila.Cells["despensa"].Value = lstPreNomina[i].cantidad;
                                break;
                        }
                    }
                }
            }

        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            cnx.Dispose();
            cmd.Dispose();
            this.Dispose();
        }

        private void guardaPreNomina()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = periodoInicio.Date;
            pn.fechafin = periodoFin.Date;
            pn.guardada = true;
            pn.tiponomina = _tipoNomina;
            pn.obracivil = _obracivil;
            
            try
            {
                cnx.Open();
                nh.guardaPreNomina(pn, _periodo);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            try
            {
                cnx.Open();
                nh.aplicaBajaObraCivil(pn, _periodo);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            string formulaDiasAPagar = "[DiasLaborados]-[Faltas]-[DiasIncapacidad]";
            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                CalculoFormula cf = new CalculoFormula(int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio, periodoFin, formulaDiasAPagar);
                int diasAPagar = int.Parse(cf.calcularFormula().ToString());

                pn = new CalculoNomina.Core.tmpPagoNomina();
                pn.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                pn.diaslaborados = diasAPagar;
                pn.fechainicio = periodoInicio;
                pn.fechafin = periodoFin;

                try
                {
                    cnx.Open();
                    nh.actualizaDiasFechaPago(pn, _periodo);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Al actualizar los dias laborados. \r\n \r\n" + error.Message, "Error");
                    return;
                }
            }
            cnx.Dispose();
            

            MessageBox.Show("ATENCION: La Pre Nómina de Obra Civil se ha guardado, \r\n ésta ya no será modificada. \r\n\r\n Se cerrará la ventana para concluir.", "Información");
            this.Dispose();
        }

        private void toolAutorizar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Quiere autorizar el periodo?", "Confirmación", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                nh = new CalculoNomina.Core.NominaHelper();
                nh.Command = cmd;

                List<CalculoNomina.Core.tmpPagoNomina> lstTrabajadores = new List<CalculoNomina.Core.tmpPagoNomina>();

                if (_tipoNomina == GLOBALES.NORMAL)
                {
                    try
                    {
                        cnx.Open();
                        lstTrabajadores = nh.obtenerIdTrabajadoresTempNomina(GLOBALES.IDEMPRESA, _tipoNomina, periodoInicio, periodoFin, _periodo);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: Al obtener los trabajadores. \r\n \r\n" + error.Message, "Error");
                        return;
                    }

                    string formulaDiasAPagar = "[DiasLaborados]-[Faltas]-[DiasIncapacidad]";

                    for (int i = 0; i < lstTrabajadores.Count; i++)
                    {
                        CalculoFormula cf = new CalculoFormula(lstTrabajadores[i].idtrabajador, periodoInicio, periodoFin, formulaDiasAPagar);
                        int diasAPagar = int.Parse(cf.calcularFormula().ToString());

                        CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
                        pn.idtrabajador = lstTrabajadores[i].idtrabajador;
                        pn.diaslaborados = diasAPagar;
                        pn.fechainicio = periodoInicio;
                        pn.fechafin = periodoFin;

                        try
                        {
                            cnx.Open();
                            nh.actualizaDiasFechaPago(pn, _periodo);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error (Actualizacion Dias Laborados): Al actualizar los dias laborados. \r\n \r\n" + error.Message, "Error");
                            return;
                        }
                    }
                    //foreach (DataGridViewRow fila in dgvEmpleados.Rows)
                    //{
                    //    CalculoFormula cf = new CalculoFormula(int.Parse(fila.Cells["idtrabajador"].Value.ToString()), periodoInicio, periodoFin, formulaDiasAPagar);
                    //    int diasAPagar = int.Parse(cf.calcularFormula().ToString());

                    //    CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
                    //    pn.idtrabajador = int.Parse(fila.Cells["idtrabajador"].Value.ToString());
                    //    pn.diaslaborados = diasAPagar;
                    //    pn.fechainicio = periodoInicio;
                    //    pn.fechafin = periodoFin;

                    //    try
                    //    {
                    //        cnx.Open();
                    //        nh.actualizaDiasFechaPago(pn);
                    //        cnx.Close();
                    //    }
                    //    catch (Exception error)
                    //    {
                    //        MessageBox.Show("Error: Al actualizar los dias laborados. \r\n \r\n" + error.Message, "Error");
                    //        return;
                    //    }
                    //}
                }

                else if (_tipoNomina == GLOBALES.EXTRAORDINARIO_NORMAL)
                {
                    CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
                    pn.diaslaborados = 0;
                    pn.fechainicio = periodoInicio;
                    pn.fechafin = periodoFin;
                    pn.tiponomina = _tipoNomina;
                    try
                    {
                        cnx.Open();
                        nh.actualizaDiasFechaPagoExtraordinaria(pn, DateTime.Now.Date, _periodo);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error (Actualizacion dias laborados): Al actualizar los dias laborados. \r\n \r\n" + error.Message, "Error");
                        return;
                    }
                }

                try
                {
                    cnx.Open();
                    nh.stpAutorizaNomina(GLOBALES.IDEMPRESA, periodoInicio.Date, periodoFin.Date, GLOBALES.IDUSUARIO, _tipoNomina, _periodo);
                    cnx.Close();
                    cnx.Dispose();

                    if (_tipoNomina == GLOBALES.NORMAL)
                    {
                        obtenerPeriodoCalculo();
                        CargaPreNomina(periodoInicio, periodoFin);
                        cp_OnNuevoPeriodo(periodoInicio, periodoFin);
                    }
                    else if (_tipoNomina == GLOBALES.EXTRAORDINARIO_NORMAL)
                    {
                        dgvEmpleados.DataSource = null;
                        dgvEmpleados.Rows.Clear();
                    }

                    MessageBox.Show("Nomina autorizada.", "Confirmación");
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error (Autorizacion Nómina): \r\n \r\n" + error.Message, "Error");
                }
            }
        }

        private void toolNoEmpleado_Click(object sender, EventArgs e)
        {
            frmFiltroNomina fn = new frmFiltroNomina();
            fn._filtro = 2;
            fn._tipoNomina = _tipoNomina;
            fn._periodo = _periodo;
            fn.OnFiltro += fn_OnFiltro;
            fn.ShowDialog();
        }

        private void toolCaratula_Click(object sender, EventArgs e)
        {
            frmVisorReportes vr = new frmVisorReportes();
            vr._noReporte = 0;
            vr._periodo = _periodo;
            vr._tipoNomina = _tipoNomina;
            vr._inicioPeriodo = periodoInicio.Date;
            vr._finPeriodo = periodoFin.Date;
            vr.Show();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportes r = new frmReportes();
            r.OnReporte += r_OnReporte;
            r._inicio = periodoInicio.Date;
            r._fin = periodoFin.Date;
            r._ReportePreNomina = true;
            r._noReporte = 1;
            r._tipoNomina = _tipoNomina;
            r.Show();
        }

        private void toolReporteDepto_Click(object sender, EventArgs e)
        {
            frmReportes r = new frmReportes();
            r.OnReporte += r_OnReporte;
            r._inicio = periodoInicio.Date;
            r._fin = periodoFin.Date;
            r._ReportePreNomina = true;
            r._noReporte = 2;
            r._tipoNomina = _tipoNomina;
            r.Show();
        }

        private void dgvFaltas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (FLAGCARGA)
            {
                FLAGCARGA = false;
                return;
            }

            if (dgvFaltas.Columns[e.ColumnIndex].Name.ToString() == "")
                return;
            if (dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value.ToString() == "" ||
                dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value.ToString() == "0")
                return;

            try
            {
                int.Parse(dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value.ToString());
            }
            catch
            {
                MessageBox.Show("Dato incorrecto. SOLO NUMEROS.", "Información");
                dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value = "";
                return;
            }

            int existe = 0, existeVacacion = 0;
            int faltas = int.Parse(dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value.ToString());
            DateTime fechaColumna = DateTime.Parse(dgvFaltas.Columns[e.ColumnIndex].Name.ToString());

            if (faltas > 1 || faltas < 0)
            {
                MessageBox.Show("Solo se admite el valor de 1 falta por dia.", "Información");
                dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value = "";
                return;
            }

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            Incidencias.Core.IncidenciasHelper ih = new Incidencias.Core.IncidenciasHelper();
            ih.Command = cmd;

            Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
            vh.Command = cmd;

            try
            {
                cnx.Open();
                existe = (int)ih.existeIncidenciaEnFalta(int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["idtrabajadorfalta"].Value.ToString()), fechaColumna.Date);
                existeVacacion = (int)vh.existeVacacionEnFalta(int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["idtrabajadorfalta"].Value.ToString()), fechaColumna.Date);
                cnx.Close();
            }
            catch
            {
                MessageBox.Show("Error: \r\n \r\n No se pudo verificar la incapacidad, verifique.", "Error");
                dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value = "";
                cnx.Dispose();
            }

            if (existe == 0 && existeVacacion == 0)
            {
                fh = new Faltas.Core.FaltasHelper();
                fh.Command = cmd;
                Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
                falta.idtrabajador = int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["idtrabajadorfalta"].Value.ToString());
                falta.idempresa = GLOBALES.IDEMPRESA;
                falta.periodo = _periodo;
                falta.faltas = int.Parse(dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value.ToString());
                falta.fechainicio = periodoInicio.Date;
                falta.fechafin = periodoFin.Date;
                falta.fecha = DateTime.Parse(dgvFaltas.Columns[e.ColumnIndex].Name.ToString());

                try
                {
                    cnx.Open();
                    fh.insertaFalta(falta);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch
                {
                    MessageBox.Show("Error: \r\n \r\n Se ingreso un valor incorrecto, verifique.", "Error");
                    dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value = "";
                    cnx.Dispose();
                }
            }
            else
            {
                MessageBox.Show("La falta ingresada, se empalma con una incapacidad y/o dia de vacación del trabajador.", "Error");
                dgvFaltas.Rows[e.RowIndex].Cells[dgvFaltas.Columns[e.ColumnIndex].Name.ToString()].Value = "";
            }

            #region CODIGO COMENTADO
            //if (dgvFaltas.Columns[e.ColumnIndex].Name == "falta")
            //{
            //    if (int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["falta"].Value.ToString()) > _periodo)
            //    {
            //        MessageBox.Show("La falta ingresada es mayor al periodo. Verifique", "Error");
            //        dgvFaltas.Rows[e.RowIndex].Cells["falta"].Value = 0;
            //        return;
            //    }

            //    if (dgvFaltas.Rows[e.RowIndex].Cells["falta"].Value.ToString() != "0")
            //    {
            //        if (dgvFaltas.Rows[e.RowIndex].Cells["incapacidad"].Value.ToString() != "0")
            //        {
            //            borraIncapacidad(e.ColumnIndex + 1, e.RowIndex);
            //            dgvFaltas.Rows[e.RowIndex].Cells["incapacidad"].Value = 0;
            //        }

            //        cnx = new SqlConnection(cdn);
            //        cmd = new SqlCommand();
            //        cmd.Connection = cnx;
            //        fh = new Faltas.Core.FaltasHelper();
            //        fh.Command = cmd;
            //        Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
            //        falta.idtrabajador = int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["idtrabajadorfalta"].Value.ToString());
            //        falta.idempresa = GLOBALES.IDEMPRESA;
            //        falta.periodo = _periodo;
            //        falta.faltas = int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["falta"].Value.ToString());
            //        falta.fechainicio = dtpPeriodoInicio.Value.Date;
            //        falta.fechafin = dtpPeriodoFin.Value.Date;

            //        try
            //        {
            //            cnx.Open();
            //            fh.insertaFalta(falta);
            //            cnx.Close();
            //            cnx.Dispose();
            //        }
            //        catch (Exception error)
            //        {
            //            MessageBox.Show("Error: \r\n \r\n Se ingreso un valor incorrecto, verifique.", "Error");
            //            dgvFaltas.Rows[e.RowIndex].Cells["falta"].Value = 0;
            //            cnx.Dispose();
            //        }
            //    }
            //    else
            //    {
            //        borraFalta(e.ColumnIndex, e.RowIndex);
            //    }
            //}

            //if (dgvFaltas.Columns[e.ColumnIndex].Name == "incapacidad")
            //{
            //    if (int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["incapacidad"].Value.ToString()) > _periodo)
            //    {
            //        MessageBox.Show("La incapacidad ingresada es mayor al periodo. Verifique", "Error");
            //        dgvFaltas.Rows[e.RowIndex].Cells["incapacidad"].Value = 0;
            //        return;
            //    }

            //    if (dgvFaltas.Rows[e.RowIndex].Cells["incapacidad"].Value.ToString() != "0")
            //    {
            //        if (dgvFaltas.Rows[e.RowIndex].Cells["falta"].Value.ToString() != "0")
            //        {
            //            borraFalta(e.ColumnIndex - 1, e.RowIndex);
            //            dgvFaltas.Rows[e.RowIndex].Cells["falta"].Value = 0;
            //        }

            //        cnx = new SqlConnection(cdn);
            //        cmd = new SqlCommand();
            //        cmd.Connection = cnx;
            //        ih = new Incapacidad.Core.IncapacidadHelper();
            //        ih.Command = cmd;
            //        Incapacidad.Core.Incapacidades incapacidad = new Incapacidad.Core.Incapacidades();
            //        incapacidad.idtrabajador = int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["idtrabajadorfalta"].Value.ToString());
            //        incapacidad.idempresa = GLOBALES.IDEMPRESA;
            //        incapacidad.diasincapacidad = 0;
            //        incapacidad.diastomados = int.Parse(dgvFaltas.Rows[e.RowIndex].Cells["incapacidad"].Value.ToString());
            //        incapacidad.diasrestantes = 0;
            //        incapacidad.diasapagar = 0;
            //        incapacidad.tipo = 0;
            //        incapacidad.aplicada = 1;
            //        incapacidad.consecutiva = 1;
            //        incapacidad.fechainicio = dtpPeriodoInicio.Value.Date;
            //        incapacidad.fechafin = dtpPeriodoFin.Value.Date;

            //        try
            //        {
            //            cnx.Open();
            //            ih.insertaIncapacidad(incapacidad);
            //            cnx.Close();
            //            cnx.Dispose();
            //        }                    
            //        catch (Exception error)
            //        {
            //            MessageBox.Show("Error: \r\n \r\n Se ingreso un valor incorrecto, verifique.", "Error");
            //            dgvFaltas.Rows[e.RowIndex].Cells["incapacidad"].Value = 0;
            //            cnx.Dispose();
            //        }
            //    }
            //    else
            //    {
            //        borraIncapacidad(e.ColumnIndex, e.RowIndex);
            //    }
            //}
            #endregion
        }

        private void dgvFaltas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && dgvFaltas.CurrentCell.Selected && dgvFaltas.CurrentCell.ReadOnly != true)
            {
                int columna = dgvFaltas.CurrentCell.ColumnIndex;
                int fila = dgvFaltas.CurrentCell.RowIndex;
                //if (dgvFaltas.Columns[columna].Name == "falta")
                borraFalta(fila, columna);
                //if (dgvFaltas.Columns[columna].Name == "incapacidad")
                //    borraIncapacidad(columna, fila);
                dgvFaltas.CurrentCell.Value = "";
            } 
        }

        private void borraFalta(int fila, int columna)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            fh = new Faltas.Core.FaltasHelper();
            fh.Command = cmd;
            Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
            falta.idtrabajador = int.Parse(dgvFaltas.Rows[fila].Cells["idtrabajadorfalta"].Value.ToString());
            falta.fechainicio = periodoInicio.Date;
            falta.fechafin = periodoFin.Date;
            falta.fecha = DateTime.Parse(dgvFaltas.Columns[columna].Name.ToString());
            try
            {
                cnx.Open();
                fh.eliminaFaltaExistente(falta);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                cnx.Dispose();
            }

            //if (dgvFaltas.Columns[columna].Name == "falta"){}
        }

        private void dgvEmpleados_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (lstValoresNomina == null)
                return;

            cnx = new SqlConnection(cdn);
            cmd.Connection = cnx;

            if (dgvEmpleados.Columns[e.ColumnIndex].Name == "horas")
            {
                for (int i = 0; i < lstValoresNomina.Count(); i++)
                {
                    if (int.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["idtrabajador"].Value.ToString()) == lstValoresNomina[i].idtrabajador && lstValoresNomina[i].noconcepto == 2)
                    {
                        nh = new CalculoNomina.Core.NominaHelper();
                        nh.Command = cmd;

                        Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
                        ch.Command = cmd;

                        Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
                        concepto.noconcepto = 2;
                        concepto.idempresa = GLOBALES.IDEMPRESA;

                        cnx.Open();
                        string formulaexento = ch.obtenerFormulaExento(concepto).ToString();
                        cnx.Close();

                        CalculoFormula cf = new CalculoFormula(lstValoresNomina[i].idtrabajador, periodoInicio.Date, periodoFin.Date, formulaexento);
                        decimal exento = decimal.Parse(cf.calcularFormula().ToString());
                        decimal cantidad = decimal.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["horas"].Value.ToString());
                        decimal gravado = 0;

                        if (cantidad <= exento)
                        {
                            exento = cantidad;
                            gravado = 0;
                        }
                        else
                        {
                            gravado = cantidad - exento;
                        }

                        CalculoNomina.Core.tmpPagoNomina hora = new CalculoNomina.Core.tmpPagoNomina();
                        hora.idempresa = GLOBALES.IDEMPRESA;
                        hora.idtrabajador = int.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["idtrabajador"].Value.ToString());
                        hora.noconcepto = 2; //CONCEPTO HORAS EXTRAS DOBLES
                        hora.fechainicio = periodoInicio.Date;
                        hora.fechafin = periodoFin.Date;
                        hora.cantidad = cantidad;
                        hora.exento = exento;
                        hora.gravado = gravado;
                        hora.modificado = true;
                        try
                        {
                            cnx.Open();
                            nh.actualizaHorasExtrasDespensa(hora);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                        }
                    }
                }
            }

            if (dgvEmpleados.Columns[e.ColumnIndex].Name == "despensa")
            {
                for (int i = 0; i < lstValoresNomina.Count(); i++)
                {
                    if (int.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["idtrabajador"].Value.ToString()) == lstValoresNomina[i].idtrabajador && lstValoresNomina[i].noconcepto == 6)
                    {
                        nh = new CalculoNomina.Core.NominaHelper();
                        nh.Command = cmd;
                        lstValoresNomina[i].cantidad = decimal.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["despensa"].Value.ToString());
                        lstValoresNomina[i].gravado = decimal.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["despensa"].Value.ToString());
                        CalculoNomina.Core.tmpPagoNomina despensa = new CalculoNomina.Core.tmpPagoNomina();
                        despensa.idempresa = GLOBALES.IDEMPRESA;
                        despensa.idtrabajador = int.Parse(dgvEmpleados.Rows[e.RowIndex].Cells["idtrabajador"].Value.ToString());
                        despensa.noconcepto = 6; //CONCEPTO DESPENSA
                        despensa.fechainicio = periodoInicio.Date;
                        despensa.fechafin = periodoFin.Date;
                        despensa.cantidad = lstValoresNomina[i].cantidad;
                        despensa.gravado = lstValoresNomina[i].gravado;
                        despensa.modificado = true;
                        try
                        {
                            cnx.Open();
                            nh.actualizaHorasExtrasDespensa(despensa);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                        }
                    }
                }
            }
        }

        private void toolTabular_Click(object sender, EventArgs e)
        {
            frmReportes r = new frmReportes();
            r.OnReporte += r_OnReporte;
            r._inicio = periodoInicio.Date;
            r._fin = periodoFin.Date;
            r._noReporte = 6;
            r._ReportePreNomina = true;
            r._tipoNomina = _tipoNomina;
            r.Show();
        }

        void r_OnReporte(string netocero, string orden, int noreporte, int empleadoinicial, int empleadofinal)
        {
            NetoCero = "";
            Orden = "";
            NetoCero = netocero;
            Orden = orden;

            if (noreporte == 6)
                workExcel.RunWorkerAsync();
            else if (noreporte == 11)
                workerGravadosExentos.RunWorkerAsync();
            else {
                frmVisorReportes vr = new frmVisorReportes();
                vr._noReporte = noreporte;
                vr._inicioPeriodo = periodoInicio.Date;
                vr._finPeriodo = periodoFin.Date;
                vr._orden = orden;
                vr._netoCero = netocero;
                vr._noReporte = noreporte;
                vr._tipoNomina = _tipoNomina;
                vr._empleadoInicio = empleadoinicial;
                vr._empleadoFin = empleadofinal;
                vr._periodo = _periodo;
                vr.Show();
            }
                
            
        }

        private void workExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = periodoInicio.Date;
            pn.fechafin = periodoFin.Date;
            pn.tiponomina = _tipoNomina;

            DataTable dt = new DataTable();
            DataTable dtConceptos = new DataTable();
            try
            {
                cnx.Open();
                dt = nh.obtenerPreNominaTabular(pn, NetoCero, Orden, _periodo);
                dtConceptos = nh.conceptosPreNominaTabular(pn, _periodo);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Workbooks.Add();

            Microsoft.Office.Interop.Excel._Worksheet workSheet = excel.ActiveSheet;

            excel.Cells[1, 1] = dt.Rows[0][0];
            excel.Cells[1, 6] = "Periodo";
            excel.Cells[2, 1] = "RFC:";
            excel.Cells[3, 1] = "REG. PAT:";

            excel.Cells[2, 2] = dt.Rows[0][1];
            excel.Cells[3, 2] = dt.Rows[0][2];

            excel.Cells[2, 6] = dt.Rows[0][3];
            excel.Cells[2, 7] = dt.Rows[0][4];

            //SE COLOCAN LOS TITULOS DE LAS COLUMNAS
            int iCol = 1;
            for (int i = 6; i < dt.Columns.Count; i++)
            {
                excel.Cells[5, iCol] = dt.Columns[i].ColumnName;
                iCol++;
            }

            //SE COLOCAN LOS DATOS
            int contadorDt = dt.Rows.Count;
            int contador = 0;
            int progreso = 0;
            iCol = 1;
            int iFil = 6;
            Microsoft.Office.Interop.Excel.Range rng;
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                progreso = (contador * 100) / contadorDt;
                workExcel.ReportProgress(progreso, "Reporte a Excel");
                contador++;

                if (i != dt.Rows.Count - 1)
                {
                    for (int j = 6; j < dt.Columns.Count; j++)
                    {
                        excel.Cells[iFil, iCol] = dt.Rows[i][j];
                        iCol++;
                    }

                    iFil++;
                    
                    #region AGRUPADO POR DEPARTAMENTO
                    //if (dt.Rows[i][5].ToString() == dt.Rows[i + 1][5].ToString())
                    //    for (int j = 6; j < dt.Columns.Count; j++)
                    //    {
                    //        excel.Cells[iFil, iCol] = dt.Rows[i][j];
                    //        iCol++;
                    //    }
                    //else
                    //{
                    //    for (int j = 6; j < dt.Columns.Count; j++)
                    //    {
                    //        excel.Cells[iFil, iCol] = dt.Rows[i][j];
                    //        iCol++;
                    //    }
                    //    iFil++;
                    //    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 1];
                    //    rng.Font.Bold = true;
                    //    excel.Cells[iFil, 1] = dt.Rows[i][5];

                    //    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 9];
                    //    rng.NumberFormat = "#,##0.00";
                    //    rng.Font.Bold = true;
                    //    excel.Cells[iFil, 9] = totalPercepciones.ToString();

                    //    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 15];
                    //    rng.NumberFormat = "#,##0.00";
                    //    rng.Font.Bold = true;
                    //    excel.Cells[iFil, 15] = totalDeducciones.ToString();
                    //    iFil++;

                    //    totalPercepciones = 0;
                    //    totalDeducciones = 0;
                    //}
                    #endregion
                }
                else
                {
                    for (int j = 6; j < dt.Columns.Count; j++)
                    {
                        excel.Cells[iFil, iCol] = dt.Rows[i][j];
                        iCol++;
                    }
                   
                }
                iCol = 1;
            }
            iFil++;

            for (int i = 6; i < iFil; i++)
            {
                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, 2];
                rng.Columns.AutoFit();

                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, 15];
                rng.NumberFormat = "#,##0.00";
                rng.Formula = string.Format("=C{0}+D{0}+E{0}+F{0}+G{0}+H{0}+I{0}+J{0}+K{0}+L{0}+M{0}+N{0}", i);

                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, 25];
                rng.NumberFormat = "#,##0.00";
                rng.Formula = string.Format("=P{0}+Q{0}+R{0}+S{0}+T{0}+W{0}+X{0}", i);

                rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[i, 26];
                rng.NumberFormat = "#,##0.00";
                rng.Formula = string.Format("=O{0}+V{0}-Y{0}", i);
            }

            int suma = iFil - 1;
            iFil++;

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 3];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(C6:C{0})", suma.ToString());
            
            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 4];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(D6:D{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 5];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(E6:E{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 6];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(F6:F{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 7];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(G6:G{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 8];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(H6:H{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 9];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(I6:I{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 10];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(J6:J{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 11];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(K6:K{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 12];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(L6:L{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 13];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(M6:M{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 14];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            
            rng.Formula = string.Format("=SUM(N6:N{0})", suma.ToString());
            //TOTAL PERCEPCIONES
            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 15];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(O6:O{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 16];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(P6:P{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 17];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(Q6:Q{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 18];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(R6:R{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 19];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(S6:S{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 20];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(T6:T{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 21];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(U6:U{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 22];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(V6:V{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 23];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(W6:W{0})", suma.ToString());

            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 24];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(X6:X{0})", suma.ToString());

            //TOTAL DEDUCCIONES
            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 25];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(Y6:Y{0})", suma.ToString());

            //TOTAL NETO
            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[iFil, 26];
            rng.NumberFormat = "#,##0.00";
            rng.Font.Bold = true;
            rng.Formula = string.Format("=SUM(Z6:Z{0})", suma.ToString());

            excel.Range["A1", "G3"].Font.Bold = true;
            excel.Range["A5", "Y5"].Font.Bold = true;
            excel.Range["B:Z"].EntireColumn.AutoFit();
            excel.Range["A6"].Select();
            excel.ActiveWindow.FreezePanes = true;
            excel.Range["A5", "Z5"].Interior.ColorIndex = 36;
            excel.Range["A5", "N5"].Font.ColorIndex = 1;
            excel.Range["P5", "X5"].Font.ColorIndex = 1;
            excel.Range["O5"].Font.ColorIndex = 32;
            excel.Range["Y5"].Font.ColorIndex = 32;
            excel.Range["Z5"].Font.ColorIndex = 32;
            excel.Range["B6", "Z" + iFil.ToString()].NumberFormat = "#,##0.00";
            
            workSheet.SaveAs("Reporte_Tabular.xlsx");
            excel.Visible = true;

            workExcel.ReportProgress(100, "Reporte a Excel");
        }

        private void workExcel_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolPorcentaje.Text = e.ProgressPercentage.ToString() + "%";
            toolEtapa.Text = e.UserState.ToString();
        }

        private void workExcel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolPorcentaje.Text = "Completado.";
        }

        //BOTON DE VER DE FALTAS
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            
            fh = new Faltas.Core.FaltasHelper();
            fh.Command = cmd;

            Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
            falta.idempresa = GLOBALES.IDEMPRESA;
            falta.fechainicio = periodoInicio.Date;
            falta.fechafin = periodoFin.Date;

            List<Faltas.Core.Faltas> lstFaltas = new List<Faltas.Core.Faltas>();

            try
            {
                cnx.Open();
                lstFaltas = fh.obtenerFaltas(falta);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }


            foreach (DataGridViewRow fila in dgvFaltas.Rows)
            {
                for (int i = 0; i < lstFaltas.Count; i++)
                {
                    if (int.Parse(fila.Cells["idtrabajadorfalta"].Value.ToString()) == lstFaltas[i].idtrabajador)
                    {
                        foreach (DataGridViewColumn columna in dgvFaltas.Columns)
                        {
                            if (columna.Name == lstFaltas[i].fecha.ToShortDateString())
                            {
                                FLAGCARGA = true;
                                fila.Cells[columna.Name].Value = lstFaltas[i].faltas;
                            }
                        }
                    }
                }
            }
        }

        private void toolOrdenEmpleado_Click(object sender, EventArgs e)
        {
            var ordenEmpleado = from f in lstEmpleadosNomina orderby f.noempleado ascending select f;
            dgvEmpleados.DataSource = ordenEmpleado.ToList();
            for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
            {
                dgvEmpleados.AutoResizeColumn(i);
            }

            var ordenEmpleadoFalta = from f in lstEmpleadosFaltaIncapacidad orderby f.noempleado ascending select f;
            dgvFaltas.DataSource = ordenEmpleadoFalta.ToList();
            for (int i = 1; i < dgvFaltas.Columns.Count; i++)
            {
                dgvFaltas.AutoResizeColumn(i);
            }
        }

        private void toolOrdenNombre_Click(object sender, EventArgs e)
        {
            var ordenNombre = from f in lstEmpleadosNomina orderby f.nombres ascending select f;
            dgvEmpleados.DataSource = ordenNombre.ToList();
            for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
            {
                dgvEmpleados.AutoResizeColumn(i);
            }

            var ordenNombreFalta = from f in lstEmpleadosFaltaIncapacidad orderby f.nombres ascending select f;
            dgvFaltas.DataSource = ordenNombreFalta.ToList();
            for (int i = 1; i < dgvFaltas.Columns.Count; i++)
            {
                dgvFaltas.AutoResizeColumn(i);
            }
        }

        private void toolOrdenPaterno_Click(object sender, EventArgs e)
        {
            var ordenPaterno = from f in lstEmpleadosNomina orderby f.paterno ascending select f;
            dgvEmpleados.DataSource = ordenPaterno.ToList();
            for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
            {
                dgvEmpleados.AutoResizeColumn(i);
            }

            var ordenPaternoFalta = from f in lstEmpleadosFaltaIncapacidad orderby f.paterno ascending select f;
            dgvFaltas.DataSource = ordenPaternoFalta.ToList();
            for (int i = 1; i < dgvFaltas.Columns.Count; i++)
            {
                dgvFaltas.AutoResizeColumn(i);
            }
        }

        private void toolOrdenMaterno_Click(object sender, EventArgs e)
        {
            var ordenMaterno = from f in lstEmpleadosNomina orderby f.materno ascending select f;
            dgvEmpleados.DataSource = ordenMaterno.ToList();
            for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
            {
                dgvEmpleados.AutoResizeColumn(i);
            }

            var ordenMaternoFalta = from f in lstEmpleadosFaltaIncapacidad orderby f.materno ascending select f;
            dgvFaltas.DataSource = ordenMaternoFalta.ToList();
            for (int i = 1; i < dgvFaltas.Columns.Count; i++)
            {
                dgvFaltas.AutoResizeColumn(i);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmSobreRecibo sr = new frmSobreRecibo();
            sr._tipoNormalEspecial = _tipoNomina;
            sr._inicioPeriodo = periodoInicio.Date;
            sr._finPeriodo = periodoFin.Date;
            sr._periodo = _periodo;
            sr._obracivil = _obracivil;
            sr.Show();
        }

        private void toolMostrarDatos_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = periodoInicio.Date;
            pn.fechafin = periodoFin.Date;
            pn.obracivil = _obracivil;

            List<CalculoNomina.Core.tmpPagoNomina> lstPreNomina = new List<CalculoNomina.Core.tmpPagoNomina>();

            try
            {
                cnx.Open();
                lstPreNomina = nh.obtenerPreNomina(pn, _periodo);
                cnx.Close();
                cnx.Dispose();
            }
            catch
            {
                MessageBox.Show("Error: Al obtener los valores de la prenomina. (Mostrar Datos).", "Error");
                cnx.Dispose();
                return;
            }

            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                for (int i = 0; i < lstPreNomina.Count; i++)
                {
                    if (int.Parse(fila.Cells["idtrabajador"].Value.ToString()) == lstPreNomina[i].idtrabajador)
                    {
                        switch (lstPreNomina[i].noconcepto)
                        {
                            case 1:
                                fila.Cells["sueldo"].Value = lstPreNomina[i].cantidad;
                                break;
                            case 2:
                                fila.Cells["horas"].Value = lstPreNomina[i].cantidad;
                                break;
                            case 3:
                                fila.Cells["asistencia"].Value = lstPreNomina[i].cantidad;
                                break;
                            case 5:
                                fila.Cells["puntualidad"].Value = lstPreNomina[i].cantidad;
                                break;
                            case 6:
                                fila.Cells["despensa"].Value = lstPreNomina[i].cantidad;
                                break;
                        }
                    }
                }
            }
        }

        private void toolCambiaPeriodo_Click(object sender, EventArgs e)
        {
            frmCambioPeriodo cp = new frmCambioPeriodo();
            cp._periodo = _periodo;
            cp._tipoNomina = _tipoNomina;
            cp.OnNuevoPeriodo += cp_OnNuevoPeriodo;
            cp.Show();
        }

        void cp_OnNuevoPeriodo(DateTime inicio, DateTime fin)
        {
            if (inicio.Date == new DateTime(1900, 1, 1) && fin.Date == new DateTime(1900, 1, 1))
            {
                DateTime dt = DateTime.Now.Date;
                if (_periodo == 7)
                {
                    while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                    periodoInicio = dt;
                    periodoFin = dt.AddDays(6);
                    periodoInicioPosterior = periodoFin.AddDays(1);
                    periodoFinPosterior = periodoFin.AddDays(7);
                }
                else
                {

                    if (dt.Day <= 15)
                    {
                        periodoInicio = new DateTime(dt.Year, dt.Month, 1);
                        periodoFin = new DateTime(dt.Year, dt.Month, 15);
                        periodoInicioPosterior = periodoFin.AddDays(1);
                        periodoFinPosterior = new DateTime(periodoInicioPosterior.Year, periodoInicioPosterior.Month,
                            DateTime.DaysInMonth(periodoInicioPosterior.Year, periodoInicioPosterior.Month) - 15);
                    }
                    else
                    {
                        periodoInicio = new DateTime(dt.Year, dt.Month, 16);
                        periodoFin = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
                        periodoInicioPosterior = periodoFin.AddDays(1);
                        periodoFinPosterior = periodoFin.AddDays(15);
                    }
                }
            }
            else {
                periodoInicio = inicio;
                periodoFin = fin;

                if (_periodo == 7)
                {
                    periodoInicioPosterior = periodoFin.AddDays(1);
                    periodoFinPosterior = periodoFin.AddDays(7);
                }
                else
                {
                    periodoInicioPosterior = periodoFin.AddDays(1);
                    if (periodoInicioPosterior.Day <= 15)
                        periodoFinPosterior = periodoFin.AddDays(15);
                    else
                        periodoFinPosterior = new DateTime(periodoInicioPosterior.Year, periodoInicioPosterior.Month,
                            DateTime.DaysInMonth(periodoInicioPosterior.Year, periodoInicioPosterior.Month));
                }
            }
            
            borraGridFaltas();
            disenoGridFaltas();
            cargaEmpleadosFaltas();

            if (_tipoNomina != GLOBALES.EXTRAORDINARIO_NORMAL)
            {
                this.Text = String.Format("Periodo de Pago: Del {0} al {1}.", periodoInicio.ToShortDateString(), periodoFin.ToShortDateString());
                toolPeriodo.Text = String.Format("Periodo de Pago: Del {0} al {1}.", periodoInicio.ToShortDateString(), periodoFin.ToShortDateString());
            }
            else
            {
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                nh = new CalculoNomina.Core.NominaHelper();
                nh.Command = cmd;

                int existeNominaExtraordinaria = 0;
                try
                {
                    cnx.Open();
                    existeNominaExtraordinaria = (int)nh.existeNomina(GLOBALES.IDEMPRESA, periodoInicio, periodoFin, _periodo);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al obtener la existencia de la nomina extraordinaria.\r\n \r\n SE CERRARÁ LA VENTANA.", "Error");
                    cnx.Dispose();
                    this.Dispose();
                }

                if (existeNominaExtraordinaria == 0)
                {
                    this.Text = String.Format("Pago extraordinario: Del {0}.", periodoInicio.ToShortDateString());
                    toolPeriodo.Text = String.Format("Pago extraordinario: Del {0}.", periodoInicio.ToShortDateString());
                    movimientosEspeciales();
                }
                else
                {
                    MessageBox.Show("La fecha seleccionada ya ha sido calculada. \r\n \r\n SE CERRARÁ LA VENTANA.", "Información");
                    this.Dispose();
                }
                
            }
        }

        private void toolCargaVacaciones_Click(object sender, EventArgs e)
        {
            frmListaCargaVacaciones lcv = new frmListaCargaVacaciones();
            lcv._tipoNomina = _tipoNomina;
            lcv._inicioPeriodo = periodoInicio.Date;
            lcv._finPeriodo = periodoFin.Date;
            lcv.Show();
        }

        private void toolCargaFaltas_Click(object sender, EventArgs e)
        {
            frmListaCargaFaltas lcf = new frmListaCargaFaltas();
            lcf._tipoNomina = _tipoNomina;
            lcf._inicioPeriodo = periodoInicio.Date;
            lcf._finPeriodo = periodoFin.Date;
            lcf.Show();
        }

        private void toolBusqueda_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolActualizar_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            cnx.Open();
            nh.eliminaPreNomina(GLOBALES.IDEMPRESA, periodoInicio.Date, periodoFin.Date, false, _obracivil);
            cnx.Close();

            dgvEmpleados.DataSource = null;
            dgvEmpleados.Rows.Clear();
            dgvFaltas.DataSource = null;

            dgvEmpleados.Columns.Add("idtrabajador","Id");
            dgvEmpleados.Columns.Add("iddepartamento","iddepartamento");
            dgvEmpleados.Columns.Add("idpuesto","idpuesto");
            dgvEmpleados.Columns.Add("noempleado","No. Empleado");
            dgvEmpleados.Columns.Add("nombres","Nombre");
            dgvEmpleados.Columns.Add("paterno","Paterno");
            dgvEmpleados.Columns.Add("materno","Materno");
            dgvEmpleados.Columns.Add("sueldo","Sueldo");
            dgvEmpleados.Columns.Add("despensa","Despensa");
            dgvEmpleados.Columns.Add("asistencia","Asistencia");
            dgvEmpleados.Columns.Add("puntualidad","Puntualidad");
            dgvEmpleados.Columns.Add("horas","H. Extras Dobles");

            dgvEmpleados.Columns["idtrabajador"].Visible = false;
            dgvEmpleados.Columns["iddepartamento"].Visible = false;
            dgvEmpleados.Columns["idpuesto"].Visible = false;

            disenoGridFaltas();
            CargaPreNomina(periodoInicio.Date, periodoFin.Date);
            calcular();
        }

        private void toolReciboNomina_Click(object sender, EventArgs e)
        {
            frmReportes r = new frmReportes();
            r.OnReporte += r_OnReporte;
            r._inicio = periodoInicio.Date;
            r._fin = periodoFin.Date;
            r._ReportePreNomina = true;
            r._noReporte = 9;
            r._tipoNomina = _tipoNomina;
            r.Show();
        }

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            guardaPreNomina();
        }

        private void toolGravadosExentos_Click(object sender, EventArgs e)
        {
            frmReportes r = new frmReportes();
            r.OnReporte += r_OnReporte;
            r._inicio = periodoInicio.Date;
            r._fin = periodoFin.Date;
            r._noReporte = 11;
            r._ReportePreNomina = true;
            r._tipoNomina = _tipoNomina;
            r.Show();
        }

        private void workerGravadosExentos_DoWork(object sender, DoWorkEventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.fechainicio = periodoInicio.Date;
            pn.fechafin = periodoFin.Date;

            DataTable dt = new DataTable();
            try
            {
                cnx.Open();
                dt = nh.obtenerPreGravadosExentos(pn, _periodo);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No es posible generar el reporte. \r\n \r\n Verifique los parametros del reporte.", "Error");
                return;
            }

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Workbooks.Add();

            Microsoft.Office.Interop.Excel._Worksheet workSheet = excel.ActiveSheet;

            excel.Cells[1, 1] = dt.Rows[0][0];
            excel.Cells[2, 1] = "RFC:";
            excel.Cells[3, 1] = "REG. PAT:";

            excel.Cells[2, 2] = dt.Rows[0][1];
            excel.Cells[3, 2] = dt.Rows[0][2];

            //SE COLOCAN LOS TITULOS DE LAS COLUMNAS
            int iCol = 1;
            for (int i = 3; i < dt.Columns.Count; i++)
            {
                excel.Cells[5, iCol] = dt.Columns[i].ColumnName;
                iCol++;
            }
            //SE COLOCAN LOS DATOS
            int contadorDt = dt.Rows.Count;
            int contador = 0;
            int progreso = 0;
            iCol = 1;
            int iFil = 6;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                progreso = (contador * 100) / contadorDt;
                workerGravadosExentos.ReportProgress(progreso, "Reporte a Excel");
                contador++;

                if (i != dt.Rows.Count - 1)
                {
                    for (int j = 3; j < dt.Columns.Count; j++)
                    {
                        excel.Cells[iFil, iCol] = dt.Rows[i][j];
                        iCol++;
                    }
                    iFil++;
                }
                else
                {
                    for (int j = 3; j < dt.Columns.Count; j++)
                    {
                        excel.Cells[iFil, iCol] = dt.Rows[i][j];
                        iCol++;
                    }
                }

                iCol = 1;

            }
            iFil++;

            excel.Range["A1", "B3"].Font.Bold = true;
            excel.Range["B:J"].EntireColumn.AutoFit();
            excel.Range["A6"].Select();
            excel.ActiveWindow.FreezePanes = true;
            excel.Range["A5", "J5"].Font.Bold = true;
            excel.Range["A5", "J5"].Interior.ColorIndex = 36;
            excel.Range["C6", "G" + iFil.ToString()].NumberFormat = "#,##0.00";

            workSheet.SaveAs("Reporte_GravadosExentos.xlsx");
            excel.Visible = true;
        }

        private void workerGravadosExentos_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolPorcentaje.Text = e.ProgressPercentage.ToString() + "%";
            toolEtapa.Text = e.UserState.ToString();
        }

        private void workerGravadosExentos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolPorcentaje.Text = "Completado.";
        }

        private void frmListaCalculoNomina_FormClosed(object sender, FormClosedEventArgs e)
        {
            cnx.Dispose();
            cmd.Dispose();
            this.Dispose();
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {

        }

        private void toolReportes_Click(object sender, EventArgs e)
        {

        }
    }
}
