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
    public partial class frmInfonavit : Form
    {
        public frmInfonavit()
        {
            InitializeComponent();
        }

        #region VARIABLES PUBLICAS
        public int _idEmpleado;
        public string _nombreEmpleado;
        public int _tipoOperacion;
        public int _modificar;
        #endregion

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Infonavit.Core.InfonavitHelper ih;
        Empleados.Core.EmpleadosHelper eh;
        int Descuento, Periodo, IdInfonavit;
        DateTime periodoInicio, periodoFin;
        #endregion

        #region DELEGADOS
        public delegate void delOnNuevoInfonavit(int edicion);
        public event delOnNuevoInfonavit OnNuevoInfonavit;
        #endregion

        private void frmInfonavit_Load(object sender, EventArgs e)
        {
            if (_tipoOperacion == GLOBALES.CONSULTAR || _tipoOperacion == GLOBALES.MODIFICAR)
            {
                cnx = new SqlConnection();
                cnx.ConnectionString = cdn;
                cmd = new SqlCommand();
                cmd.Connection = cnx;
                ih = new Infonavit.Core.InfonavitHelper();
                ih.Command = cmd;

                eh = new Empleados.Core.EmpleadosHelper();
                eh.Command = cmd;

                List<Infonavit.Core.Infonavit> lstInfonavit;

                Infonavit.Core.Infonavit i = new Infonavit.Core.Infonavit();
                i.idtrabajador = _idEmpleado;
                i.activo = true;

                try
                {
                    cnx.Open();
                    lstInfonavit = ih.obtenerInfonavit(i);
                    Periodo = (int)eh.obtenerDiasPeriodo(_idEmpleado);
                    cnx.Close();
                    cnx.Dispose();

                    for (int j = 0; j < lstInfonavit.Count; j++)
                    {
                        IdInfonavit = int.Parse(lstInfonavit[j].idinfonavit.ToString());
                        txtNumeroCredito.Text = lstInfonavit[j].credito;
                        txtValor.Text = lstInfonavit[j].valordescuento.ToString();
                        chkActivo.Checked = lstInfonavit[j].activo;
                        txtDescripcion.Text = lstInfonavit[j].descripcion;
                        dtpFechaAplicacion.Value = lstInfonavit[j].fecha;
                        dtpInicioPeriodo.Value = lstInfonavit[j].inicio.AddDays(1);
                        dtpFinPeriodo.Value = lstInfonavit[j].fin;
                        
                        switch (lstInfonavit[j].descuento)
                        {
                                //Porcentaje
                            case 1:
                                rbtnPorcentaje.Checked = true;
                                break;
                            case 2:
                                rbtnPesos.Checked = true;
                                break;
                            case 3:
                                rbtnVsmdf.Checked = true;
                                break;
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n " + error.Message, "Error");
                }

                if (_tipoOperacion == GLOBALES.CONSULTAR)
                {
                    toolVentana.Text = "Consulta del Crédito";
                    GLOBALES.INHABILITAR(this, typeof(TextBox));
                    GLOBALES.INHABILITAR(this, typeof(RadioButton));
                    GLOBALES.INHABILITAR(this, typeof(CheckBox));
                    GLOBALES.INHABILITAR(this, typeof(DateTimePicker));
                    toolGuardar.Enabled = false;
                    toolBuscar.Enabled = false;
                }
                else
                {
                    toolVentana.Text = "Edición del Crédito";
                    lblEmpleado.Text = _nombreEmpleado;
                    toolBuscar.Enabled = false;
                    btnCambiar.Enabled = true;
                    obtenerPeriodoActual();
                }

                if (_modificar == 1)
                {
                    dtpInicioPeriodo.Enabled = true;
                }
            }
        }

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            //SE VALIDA SI TODOS LOS CAMPOS HAN SIDO LLENADOS.
            string control = GLOBALES.VALIDAR(this, typeof(TextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            if (_idEmpleado == 0)
            {
                MessageBox.Show("No se puede guardar no ha seleccionado al Empleado", "Error");
                return;
            }

            if (dtpFechaAplicacion.Value.Date > dtpFinPeriodo.Value.Date)
            {
                MessageBox.Show("La fecha de aplicacion es mayor al periodo.", "Error");
                return;
            }

            if (dtpFechaAplicacion.Value.Date < dtpInicioPeriodo.Value.Date)
            {
                MessageBox.Show("La fecha de aplicacion es menor al periodo.", "Error");
                return;
            }

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            ih = new Infonavit.Core.InfonavitHelper();
            ih.Command = cmd;

            Infonavit.Core.Infonavit i = new Infonavit.Core.Infonavit();
            i.idtrabajador = _idEmpleado;
            i.idempresa = GLOBALES.IDEMPRESA;
            i.credito = txtNumeroCredito.Text;
            i.descuento = Descuento;
            i.valordescuento = double.Parse(txtValor.Text);
            i.activo = chkActivo.Checked;
            i.descripcion = txtDescripcion.Text;
            i.dias = (int)(dtpFinPeriodo.Value.Date - dtpFechaAplicacion.Value.Date).TotalDays + 1;
            i.fecha = dtpFechaAplicacion.Value.Date;
            i.inicio = dtpInicioPeriodo.Value.Date;
            i.fin = dtpFinPeriodo.Value.Date;

            Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Conceptos.Core.ConceptoTrabajador ct = new Conceptos.Core.ConceptoTrabajador();
            ct.idempleado = _idEmpleado;

            try
            {
                cnx.Open();
                ct.idconcepto = (int)ch.obtenerIdConcepto(9, GLOBALES.IDEMPRESA);
                cnx.Close();
            }
            catch
            {
                MessageBox.Show("Error: Al obtener el ID del Concepto Infonavit.", "Error");
                cnx.Dispose();
                return;
            }
            
            switch (_tipoOperacion)
            {
                case 0:
                    try
                    {
                        cnx.Open();
                        ih.insertaInfonavit(i);
                        ch.insertaConceptoTrabajador(ct);
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al ingresar los datos. \r\n \r\n Error: " + error.Message);
                        this.Dispose();
                    }
                    break;
                case 2:
                    try
                    {
                        cnx.Open();
                        if (_modificar == 0)
                        {
                            i.idinfonavit = IdInfonavit;
                            ih.actualizaInfonavit(i);
                        }
                        else if (_modificar == 1)
                        {
                            ih.insertaInfonavit(i);
                            ih.actualizaEstatusInfonavit(IdInfonavit);
                        }
                        cnx.Close();
                        cnx.Dispose();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al actualizar los datos. \r\n \r\n Error: " + error.Message);
                        this.Dispose();
                    }
                    break;
            }

            if (OnNuevoInfonavit != null)
                OnNuevoInfonavit(_tipoOperacion);
            this.Dispose();
        }

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar b = new frmBuscar();
            b._catalogo = GLOBALES.EMPLEADOS;
            b.OnBuscar += b_OnBuscar;
            b.Show();
        }

        void b_OnBuscar(int id, string nombre)
        {
            _idEmpleado = id;
            lblEmpleado.Text = nombre;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            try
            {
                cnx.Open();
                Periodo = (int)eh.obtenerDiasPeriodo(_idEmpleado);
                cnx.Close();
                obtenerPeriodoActual();
                btnCambiar.Enabled = true;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al obtener los dias del periodo. \r\n \r\n La ventana se cerrará. \r\n \r\n" + error.Message, "Error");
                cnx.Dispose();
                this.Dispose();
            }
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void rbtnPorcentaje_CheckedChanged(object sender, EventArgs e)
        {
            Descuento = GLOBALES.dPORCENTAJE;
        }

        private void rbtnVsmdf_CheckedChanged(object sender, EventArgs e)
        {
            Descuento = GLOBALES.dVSMDF;
        }

        private void rbtnPesos_CheckedChanged(object sender, EventArgs e)
        {
            Descuento = GLOBALES.dPESOS;
        }

        private void dtpInicioPeriodo_ValueChanged(object sender, EventArgs e)
        {
            periodo();
        }

        private void periodo()
        {
            if (Periodo == 7)
            {
                DateTime dt = dtpInicioPeriodo.Value.Date;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                dtpInicioPeriodo.Value = dt;
                dtpFinPeriodo.Value = dt.AddDays(6);
            }
            else
            {
                if (dtpInicioPeriodo.Value.Day <= 15)
                {
                    dtpInicioPeriodo.Value = new DateTime(dtpInicioPeriodo.Value.Year, dtpInicioPeriodo.Value.Month, 1);
                    dtpFinPeriodo.Value = new DateTime(dtpInicioPeriodo.Value.Year, dtpInicioPeriodo.Value.Month, 15);
                }
                else
                {
                    dtpInicioPeriodo.Value = new DateTime(dtpInicioPeriodo.Value.Year, dtpInicioPeriodo.Value.Month, 16);
                    dtpFinPeriodo.Value = new DateTime(dtpInicioPeriodo.Value.Year, dtpInicioPeriodo.Value.Month, DateTime.DaysInMonth(dtpInicioPeriodo.Value.Year, dtpInicioPeriodo.Value.Month));
                }

            }
        }

        private void obtenerPeriodoActual()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            List<CalculoNomina.Core.tmpPagoNomina> lstUltimaNomina = new List<CalculoNomina.Core.tmpPagoNomina>();

            try
            {
                cnx.Open();
                lstUltimaNomina = nh.obtenerUltimaNomina(GLOBALES.IDEMPRESA);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            if (lstUltimaNomina.Count != 0)
            {
                if (Periodo == 7)
                {
                    periodoInicio = lstUltimaNomina[0].fechafin.AddDays(1);
                    periodoFin = lstUltimaNomina[0].fechafin.AddDays(7);
                }
                else
                {
                    periodoInicio = lstUltimaNomina[0].fechafin.AddDays(1);
                    if (periodoInicio.Day <= 15)
                        periodoFin = lstUltimaNomina[0].fechafin.AddDays(15);
                    else
                        periodoFin = lstUltimaNomina[0].fechafin.AddDays(
                            DateTime.DaysInMonth(periodoInicio.Year, periodoInicio.Month) - 15);
                }
                lblPeriodo.Visible = true;
                dtpInicioPeriodo.Visible = true;
                dtpFinPeriodo.Visible = true;

                dtpInicioPeriodo.Enabled = false;
                dtpFinPeriodo.Enabled = false;
                dtpInicioPeriodo.Value = periodoInicio;
                dtpFinPeriodo.Value = periodoFin;
            }
            else
            {
                lblPeriodo.Visible = true;
                dtpInicioPeriodo.Visible = true;
                dtpFinPeriodo.Visible = true;
                periodo();
            }
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            frmCambioPeriodo cp = new frmCambioPeriodo();
            cp.OnNuevoPeriodo += cp_OnNuevoPeriodo;
            cp._periodo = Periodo;
            cp.ShowDialog();
        }

        void cp_OnNuevoPeriodo(DateTime inicio, DateTime fin)
        {
            dtpInicioPeriodo.Value = inicio;
            dtpFinPeriodo.Value = fin;
        }
    }
}
