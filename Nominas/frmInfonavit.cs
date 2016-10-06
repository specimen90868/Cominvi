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
        public bool _estatusInfonavit;
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
            cargaCombo();
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

                Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
                dh.Command = cmd;

                List<Infonavit.Core.Infonavit> lstInfonavit;
                List<Departamento.Core.Depto> lstDepartamento = new List<Departamento.Core.Depto>();
                List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();

                Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
                empleado.idtrabajador = _idEmpleado;

                Departamento.Core.Depto dpto = new Departamento.Core.Depto();
                dpto.idempresa = GLOBALES.IDEMPRESA;

                Infonavit.Core.Infonavit i = new Infonavit.Core.Infonavit();
                i.idtrabajador = _idEmpleado;
                i.activo = _estatusInfonavit;

                try
                {
                    cnx.Open();
                    Periodo = (int)eh.obtenerDiasPeriodo(_idEmpleado);
                    lstEmpleado = eh.obtenerEmpleado(empleado);
                    lstDepartamento = dh.obtenerDepartamentos(dpto);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error al obtener los dias del periodo. \r\n \r\n La ventana se cerrará. \r\n \r\n" + error.Message, "Error");
                    cnx.Dispose();
                    this.Dispose();
                }

                var dato = from emp in lstEmpleado
                           join d in lstDepartamento on emp.iddepartamento equals d.id
                           select new
                           {
                               emp.noempleado,
                               emp.nombrecompleto,
                               d.descripcion
                           };
                foreach (var inf in dato)
                {
                    mtxtNoEmpleado.Text = inf.noempleado;
                    txtDepartamento.Text = inf.descripcion;
                }

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
                        if (lstInfonavit[j].activo)
                            chkInactivo.Checked = false;
                        else
                            chkInactivo.Checked = true;
                        txtDescripcion.Text = lstInfonavit[j].descripcion;
                        dtpFechaAplicacion.Value = lstInfonavit[j].fecha;
                        cmbEstatusInfonavit.SelectedValue = lstInfonavit[j].estatus;
                        //dtpInicioPeriodo.Value = lstInfonavit[j].inicio.AddDays(1);
                        //dtpFinPeriodo.Value = lstInfonavit[j].fin;
                        
                        switch (lstInfonavit[j].descuento)
                        {
                                //Porcentaje
                            case 1:
                                rbtnPorcentaje.Checked = true;
                                break;
                            case 2:
                                rbtnPesos.Checked = true;
                                if (Periodo == 7)
                                    txtValor.Text = ((lstInfonavit[j].valordescuento * decimal.Parse((30.4).ToString())) / Periodo).ToString();
                                else
                                    txtValor.Text = (lstInfonavit[j].valordescuento * 2).ToString();
                                break;
                            case 3:
                                rbtnVsmdf.Checked = true;
                                txtValor.Text = lstInfonavit[j].valordescuento.ToString();
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
                    GLOBALES.INHABILITAR(this, typeof(MaskedTextBox));
                    GLOBALES.INHABILITAR(this, typeof(ComboBox));
                    toolGuardar.Enabled = false;
                    toolBuscar.Enabled = false;
                }
                else
                {
                    toolVentana.Text = "Edición del Crédito";
                    lblEmpleado.Text = _nombreEmpleado;
                    toolBuscar.Enabled = false;
                    //obtenerPeriodoActual();
                }

                if (_modificar == 1)
                {
                    //dtpInicioPeriodo.Enabled = true;
                }
            }
        }

        private void cargaCombo()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Catalogos.Core.CatalogosHelper ch = new Catalogos.Core.CatalogosHelper();
            ch.Command = cmd;
            Catalogos.Core.Catalogo cat = new Catalogos.Core.Catalogo();
            cat.grupodescripcion = "ESTATUS INFONAVIT";
            List<Catalogos.Core.Catalogo> lstCatalogo = new List<Catalogos.Core.Catalogo>();
            try
            {
                cnx.Open();
                lstCatalogo = ch.obtenerGrupo(cat);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener el catalogo.", "Error");
                cnx.Dispose();
            }
            cmbEstatusInfonavit.DataSource = lstCatalogo;
            cmbEstatusInfonavit.DisplayMember = "descripcion";
            cmbEstatusInfonavit.ValueMember = "id";
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

            //bool alta_reingreso = ChecaFechaAltaReingreso();

            //if (dtpFechaAplicacion.Value.Date > dtpFinPeriodo.Value.Date)
            //{
            //    MessageBox.Show("La fecha de aplicacion es mayor al periodo.", "Error");
            //    return;
            //}

            //if (dtpFechaAplicacion.Value.Date < dtpInicioPeriodo.Value.Date)
            //{
            //    MessageBox.Show("La fecha de aplicacion es menor al periodo.", "Error");
            //    return;
            //}

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            ih = new Infonavit.Core.InfonavitHelper();
            ih.Command = cmd;

            PeriodoFechaAplicacion();

            Infonavit.Core.Infonavit i = new Infonavit.Core.Infonavit();
            i.idtrabajador = _idEmpleado;
            i.idempresa = GLOBALES.IDEMPRESA;
            i.credito = txtNumeroCredito.Text;
            i.descuento = Descuento;
            i.activo = chkInactivo.Checked == true ? false : true;
            i.descripcion = txtDescripcion.Text;
            i.dias = (int)(periodoFin.Date - dtpFechaAplicacion.Value.Date).TotalDays + 1;
            i.fecha = dtpFechaAplicacion.Value.Date;
            i.inicio = periodoInicio.Date;
            i.fin = periodoFin.Date;
            i.registro = DateTime.Now;
            i.idusuario = GLOBALES.IDUSUARIO;
            i.estatus = int.Parse(cmbEstatusInfonavit.SelectedValue.ToString());

            if (rbtnPesos.Checked)
                if (Periodo == 7)
                    i.valordescuento = (decimal.Parse(txtValor.Text) / decimal.Parse((30.4).ToString())) * Periodo;
                else
                    i.valordescuento = decimal.Parse(txtValor.Text) / 2;
            if (rbtnVsmdf.Checked)
                i.valordescuento = decimal.Parse(txtValor.Text);
            if (rbtnPorcentaje.Checked)
                i.valordescuento = decimal.Parse(txtValor.Text);

            Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Conceptos.Core.ConceptoTrabajador ctInfonavit = new Conceptos.Core.ConceptoTrabajador();
            ctInfonavit.idempleado = _idEmpleado;

            Conceptos.Core.ConceptoTrabajador ctSeguroInfonavit = new Conceptos.Core.ConceptoTrabajador();
            ctSeguroInfonavit.idempleado = _idEmpleado;

            try
            {
                cnx.Open();
                ctInfonavit.idconcepto = (int)ch.obtenerIdConcepto(9, GLOBALES.IDEMPRESA, Periodo);
                ctSeguroInfonavit.idconcepto = (int)ch.obtenerIdConcepto(21, GLOBALES.IDEMPRESA, Periodo);
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
                        int existeCredito = 0;
                        cnx.Open();
                        existeCredito = int.Parse(ih.existeInfonavit(_idEmpleado, txtNumeroCredito.Text.Trim()).ToString());
                        if (existeCredito != 0)
                        {
                            MessageBox.Show("El número de credito que desea ingresar ya existe.\r\n" +
                                            "Si es una modificación del crédito use la opción \"Modificación\"");
                            this.Dispose();
                        }
                        else
                        {
                            ih.insertaInfonavit(i);
                            ch.insertaConceptoTrabajador(ctInfonavit);
                            ch.insertaConceptoTrabajador(ctSeguroInfonavit);
                        }
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
                            ih.actualizaEstatusInfonavit(IdInfonavit, DateTime.Now, GLOBALES.IDUSUARIO);
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
            b._busqueda = GLOBALES.FORMULARIOS;
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

            Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
            dh.Command = cmd;

            List<Departamento.Core.Depto> lstDepartamento = new List<Departamento.Core.Depto>();
            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idEmpleado;

            Departamento.Core.Depto dpto = new Departamento.Core.Depto();
            dpto.idempresa = GLOBALES.IDEMPRESA;

            try
            {
                cnx.Open();
                lstEmpleado = eh.obtenerEmpleado(empleado);
                lstDepartamento = dh.obtenerDepartamentos(dpto);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al obtener los datos del departamento. \r\n \r\n La ventana se cerrará. \r\n \r\n" + error.Message, "Error");
                cnx.Dispose();
                this.Dispose();
            }

            var dato = from emp in lstEmpleado
                       join d in lstDepartamento on emp.iddepartamento equals d.id
                       select new
                       {
                           emp.noempleado,
                           emp.nombrecompleto,
                           d.descripcion
                       };
            foreach (var inf in dato)
            {
                mtxtNoEmpleado.Text = inf.noempleado;
                txtDepartamento.Text = inf.descripcion;
            }


            try
            {
                cnx.Open();
                Periodo = (int)eh.obtenerDiasPeriodo(_idEmpleado);
                cnx.Close();
                //obtenerPeriodoActual();
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

        private void PeriodoFechaAplicacion()
        {
            if (Periodo == 7)
            {
                DateTime dt = dtpFechaAplicacion.Value.Date;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                periodoInicio = dt;
                periodoFin = dt.AddDays(6);
            }
            else
            {
                if (dtpFechaAplicacion.Value.Day <= 15)
                {
                    periodoInicio = new DateTime(dtpFechaAplicacion.Value.Year, dtpFechaAplicacion.Value.Month, 1);
                    periodoFin = new DateTime(dtpFechaAplicacion.Value.Year, dtpFechaAplicacion.Value.Month, 15);
                }
                else
                {
                    periodoInicio = new DateTime(dtpFechaAplicacion.Value.Year, dtpFechaAplicacion.Value.Month, 16);
                    periodoFin = new DateTime(dtpFechaAplicacion.Value.Year, dtpFechaAplicacion.Value.Month, DateTime.DaysInMonth(dtpFechaAplicacion.Value.Year, dtpFechaAplicacion.Value.Month));
                }

            }
        }

        private void ChecaFechaAltaReingreso()
        {

            DateTime inicio = DateTime.Now.Date, fin = DateTime.Now.Date;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Altas.Core.AltasHelper ah = new Altas.Core.AltasHelper();
            ah.Command = cmd;

            Reingreso.Core.ReingresoHelper rh = new Reingreso.Core.ReingresoHelper();
            rh.Command = cmd;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            List<CalculoNomina.Core.tmpPagoNomina> lstUltimaNomina = new List<CalculoNomina.Core.tmpPagoNomina>();

            try
            {
                cnx.Open();
                lstUltimaNomina = nh.obtenerUltimaNomina(GLOBALES.IDEMPRESA, false);
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            if (lstUltimaNomina.Count != 0)
            {
                if (Periodo == 7)
                {
                    inicio = lstUltimaNomina[0].fechafin.AddDays(1);
                    fin = lstUltimaNomina[0].fechafin.AddDays(7);
                }
                else
                {
                    inicio = lstUltimaNomina[0].fechafin.AddDays(1);
                    if (inicio.Day <= 15)
                        fin = lstUltimaNomina[0].fechafin.AddDays(15);
                    else
                        fin = new DateTime(inicio.Year, inicio.Month,
                            DateTime.DaysInMonth(inicio.Year, inicio.Month));
                }
            }

            Altas.Core.Altas a = new Altas.Core.Altas();
            a.idtrabajador = _idEmpleado;
            a.periodoInicio = inicio.Date;
            a.periodoFin = fin.Date;

            Reingreso.Core.Reingresos r = new Reingreso.Core.Reingresos();
            r.idtrabajador = _idEmpleado;
            r.periodoinicio = inicio.Date;
            r.periodofin = fin.Date;

            int existeAlta = 0;
            int existeReingreso = 0;

            try
            {
                cnx.Open();
                existeAlta = (int)ah.existeAlta(a);
                existeReingreso = (int)rh.existeReingreso(r);
                cnx.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al obtener la existencia de la alta.", "Error");
                cnx.Dispose();
            }

            DateTime fechaAlta = DateTime.Now.Date;
            DateTime fechaReingreso = DateTime.Now.Date;
            if (existeAlta != 0)
            {
                try
                {
                    cnx.Open();
                    fechaAlta = DateTime.Parse(ah.fechaAlta(a).ToString());
                    cnx.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al obtener la fecha de alta.", "Error");
                    cnx.Dispose();
                }

                if (dtpFechaAplicacion.Value.Date < fechaAlta)
                    dtpFechaAplicacion.Value = fechaAlta;
            }

            if (existeReingreso != 0)
            {
                try
                {
                    cnx.Open();
                    fechaReingreso = DateTime.Parse(rh.fechaReingreso(r).ToString());
                    cnx.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Al obtener la fecha de alta.", "Error");
                    cnx.Dispose();
                }

                if (dtpFechaAplicacion.Value.Date < fechaReingreso)
                    dtpFechaAplicacion.Value = fechaReingreso;
            }
        }
    }
}
