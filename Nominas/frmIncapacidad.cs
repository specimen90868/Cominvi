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
    public partial class frmIncapacidad : Form
    {
        public frmIncapacidad()
        {
            InitializeComponent();
        }

        #region VARIABLES PUBLICAS
        public int _idEmpleado = 0;
        public string _nombreEmpleado;
        public int _idIncapacidad;
        public int _tipoOperacion;
        public int _tipoForma;
        #endregion

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        SqlBulkCopy bulk;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        Empleados.Core.EmpleadosHelper eh;
        Periodos.Core.PeriodosHelper ph;
        Incidencias.Core.IncidenciasHelper ih;
        Departamento.Core.DeptoHelper dh;
        int periodo, idperiodo;
        DateTime periodoInicio, periodoFin;
        #endregion

        #region DELEGADOS
        public delegate void delOnIncapacidad();
        public event delOnIncapacidad OnIncapacidad;
        #endregion

        private void toolBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar b = new frmBuscar();
            b.OnBuscar += b_OnBuscar;
            b._catalogo = GLOBALES.EMPLEADOS;
            b._busqueda = GLOBALES.FORMULARIOS;
            b.ShowDialog();
        }

        void b_OnBuscar(int id, string nombre)
        {
            _idEmpleado = id;
            _nombreEmpleado = nombre;

            lblEmpleado.Text = nombre;

            cnx = new SqlConnection();
            cnx.ConnectionString = cdn;
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            eh = new Empleados.Core.EmpleadosHelper();
            ph = new Periodos.Core.PeriodosHelper();
            eh.Command = cmd;
            ph.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = _idEmpleado;

            Periodos.Core.Periodos per = new Periodos.Core.Periodos();
            per.idempresa = GLOBALES.IDEMPRESA;

            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();
            List<Periodos.Core.Periodos> lstPeriodos = new List<Periodos.Core.Periodos>();

            try
            {
                cnx.Open();
                lstEmpleado = eh.obtenerEmpleado(empleado);
                lstPeriodos = ph.obtenerPeriodos(per);
                cnx.Close();
               
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            var datos = from e in lstEmpleado
                        join p in lstPeriodos on e.idperiodo equals p.idperiodo
                        select new
                        {
                            p.dias,
                            e.idperiodo,
                            e.noempleado
                        };
            foreach (var d in datos)
            {
                periodo = d.dias;
                idperiodo = d.idperiodo;
                mtxtNoEmpleado.Text = d.noempleado;
            }

            dh = new Departamento.Core.DeptoHelper();
            dh.Command = cmd;

            Departamento.Core.Depto depto = new Departamento.Core.Depto();
            depto.id = lstEmpleado[0].iddepartamento;

            List<Departamento.Core.Depto> lstDepto = new List<Departamento.Core.Depto>();

            try
            {
                cnx.Open();
                lstDepto = dh.obtenerDepartamento(depto);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                
                throw;
            }

            txtDepartamento.Text = lstDepto[0].descripcion;
            //obtenerPeriodoActual();
        }

        private void frmIncapacidad_Load(object sender, EventArgs e)
        {
            dtpFinPeriodo.Enabled = false;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Catalogos.Core.CatalogosHelper ch = new Catalogos.Core.CatalogosHelper();
            ch.Command = cmd;

            List<Catalogos.Core.Catalogo> lstControlIncapacidad = new List<Catalogos.Core.Catalogo>();
            List<Catalogos.Core.Catalogo> lstIncapacidad = new List<Catalogos.Core.Catalogo>();

            try
            {
                cnx.Open();
                lstIncapacidad = ch.obtenerTipoIncapacidad();
                lstControlIncapacidad = ch.obtenerControlIncapacidad();
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al cargar controles de lista" + error.Message, "Error");
            }

            cmbTipoCaso.DataSource = lstControlIncapacidad.ToList();
            cmbTipoCaso.DisplayMember = "descripcion";
            cmbTipoCaso.ValueMember = "id";

            cmbTipoIncapacidad.DataSource = lstIncapacidad.ToList();
            cmbTipoIncapacidad.DisplayMember = "descripcion";
            cmbTipoIncapacidad.ValueMember = "id";
        }

        private void toolCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolGuardar_Click(object sender, EventArgs e)
        {
            int existe = 0;
            if (_idEmpleado == 0)
            {
                MessageBox.Show("No ha seleccionado al Empleado.", "Información");
                return;
            }
            //SE VALIDA SI TODOS LOS CAMPOS HAN SIDO LLENADOS.
            string control = GLOBALES.VALIDAR(this, typeof(TextBox));
            if (!control.Equals(""))
            {
                MessageBox.Show("Falta el campo: " + control, "Información");
                return;
            }

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            bulk = new SqlBulkCopy(cnx);
            cmd.Connection = cnx;

            Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
            fh.Command = cmd;

            DateTime finIncapacidad = dtpFechaInicio.Value.AddDays(double.Parse(txtDiasIncapacidad.Text) - 1);
            int a = 0;
            int existeFalta = 0;
            bool FLAGFALTAS = false;

            Incidencias.Core.IncidenciasHelper incidenciah = new Incidencias.Core.IncidenciasHelper();
            incidenciah.Command = cmd;
            DateTime fii, ffi;
            bool FLAG_FINICIO = false, FLAG_FFIN = false;
            List<Incidencias.Core.Incidencias> datoFechas = new List<Incidencias.Core.Incidencias>();
            try
            {
                cnx.Open();
                datoFechas = incidenciah.finIncapacidad(_idEmpleado);              
                cnx.Close();
            }
            catch {
                MessageBox.Show("Error: Al obtener la ultima fecha de incapacidad.", "Error");
                cnx.Dispose();
                return;
            }


            if (datoFechas.Count != 0)
            {
                fii = dtpFechaInicio.Value.Date;
                ffi = dtpFechaInicio.Value.AddDays(int.Parse(txtDiasIncapacidad.Text) - 1);
                for (int i = 0; i < datoFechas.Count; i++)
                {
                    if (fii.Date <= datoFechas[i].finincapacidad.Date)
                    {
                        FLAG_FINICIO = true;
                    }
                    if (ffi.Date >= datoFechas[i].inicioincapacidad.Date)
                    {
                        FLAG_FFIN = true;
                    }
                }
                //ffi = DateTime.Parse(datoFecha.ToString());
                if (FLAG_FINICIO && FLAG_FFIN)
                {
                    MessageBox.Show("Las fechas de la incapacidad se empalman con una ya existente.", "Información");
                    return;
                }
                else if (FLAG_FINICIO)
                {
                    MessageBox.Show("Las fechas de la incapacidad se empalman con una ya existente.", "Información");
                    return;
                }
            }
            

            while (dtpFechaInicio.Value.AddDays(a).Date <= finIncapacidad.Date)
            {
                try
                {
                    cnx.Open();
                    existeFalta = (int)fh.existeFalta(_idEmpleado, dtpFechaInicio.Value.AddDays(a).Date);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al obtener la existencia de faltas.", "Error");
                    cnx.Dispose();
                    return;
                }

                if (existeFalta != 0)
                {
                    try
                    {
                        cnx.Open();
                        fh.eliminaFalta(_idEmpleado, dtpFechaInicio.Value.AddDays(a).Date);
                        FLAGFALTAS = true;
                        cnx.Close();
                    }
                    catch 
                    {
                        MessageBox.Show("Error: Al eliminar la falta existente.", "Error");
                        cnx.Dispose();
                        return;
                    }
                }
                a++;
            }

            if (FLAGFALTAS)
                MessageBox.Show("Se encontraron faltas al momento de ingresar la incapacidad. \r\n \r\n Estas se quitaron.", "Confirmación");

            ih = new Incidencias.Core.IncidenciasHelper();
            ih.Command = cmd;
            ih.bulkCommand = bulk;

            Incidencias.Core.Incidencias incidencia = new Incidencias.Core.Incidencias();
            incidencia.certificado = txtCertificado.Text.Trim();

            List<Incidencias.Core.Incidencias> lstIncidencias;

            try
            {
                cnx.Open();
                existe = int.Parse(ih.existeCertificado(incidencia).ToString());
                cnx.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al consultar el certificado. \r\n \r\n Descripcion: " + error.Message, "Error");
            }

            if (existe == 0)
            {
                PeriodoFechaAplicacion();

                DateTime fechaInicioIncapacidad = dtpFechaInicio.Value.Date;
                DateTime fechaFinIncapacidad = dtpFechaInicio.Value.AddDays(double.Parse(txtDiasIncapacidad.Text) - 1).Date;
                //DateTime fechaFinPeriodo = dtpFinPeriodo.Value.Date;
                DateTime fechaFinPeriodo = periodoFin.Date;
                int diasRestantes = int.Parse(txtDiasIncapacidad.Text);
                int dias = 0;
                bool FLAG = false;
                lstIncidencias = new List<Incidencias.Core.Incidencias>();
                int i = 1;

                while (diasRestantes != 0)
                {
                    if (fechaFinIncapacidad <= fechaFinPeriodo)
                    {
                        Incidencias.Core.Incidencias incidencia2 = new Incidencias.Core.Incidencias();
                        incidencia2.id = i;
                        incidencia2.idtrabajador = _idEmpleado;
                        incidencia2.idempresa = GLOBALES.IDEMPRESA;
                        incidencia2.certificado = txtCertificado.Text.Trim();
                        incidencia2.inicioincapacidad = dtpFechaInicio.Value;
                        incidencia2.finincapacidad = dtpFechaInicio.Value.AddDays(double.Parse(txtDiasIncapacidad.Text) - 1);
                        incidencia2.periodoinicio = periodoInicio.Date;
                        incidencia2.periodofin = periodoFin.Date;
                        incidencia2.idcontrol = int.Parse(cmbTipoCaso.SelectedValue.ToString());
                        incidencia2.idincapacidad = int.Parse(cmbTipoIncapacidad.SelectedValue.ToString());

                        dias = diasRestantes;
                        incidencia2.dias = dias;                      
                        incidencia2.fechainicio = fechaInicioIncapacidad.Date;
                        incidencia2.fechafin = fechaFinIncapacidad.Date;

                        lstIncidencias.Add(incidencia2);
                        
                    }
                    else
                    {
                        Incidencias.Core.Incidencias incidencia2 = new Incidencias.Core.Incidencias();
                        incidencia2.id = i;
                        incidencia2.idtrabajador = _idEmpleado;
                        incidencia2.idempresa = GLOBALES.IDEMPRESA;
                        incidencia2.certificado = txtCertificado.Text.Trim();
                        incidencia2.inicioincapacidad = dtpFechaInicio.Value;
                        incidencia2.finincapacidad = dtpFechaInicio.Value.AddDays(double.Parse(txtDiasIncapacidad.Text) - 1);
                        incidencia2.periodoinicio = periodoInicio.Date;
                        incidencia2.periodofin = periodoFin.Date;
                        incidencia2.idcontrol = int.Parse(cmbTipoCaso.SelectedValue.ToString());
                        incidencia2.idincapacidad = int.Parse(cmbTipoIncapacidad.SelectedValue.ToString());

                        if (!FLAG)
                        {
                            dias = (int)(fechaFinPeriodo - fechaInicioIncapacidad).TotalDays + 1;
                            incidencia2.dias = dias;
                            incidencia2.fechainicio = fechaInicioIncapacidad.Date;
                            incidencia2.fechafin = fechaFinPeriodo.Date;

                            fechaInicioIncapacidad = fechaFinPeriodo.AddDays(1);
                            if (periodo == 7)
                                fechaFinPeriodo = fechaFinPeriodo.AddDays(periodo);
                            else
                            {
                                if (fechaInicioIncapacidad.Day <= 15)
                                {
                                    fechaFinPeriodo = fechaFinPeriodo.AddDays(periodo);
                                }
                                else
                                {
                                    fechaFinPeriodo = new DateTime(fechaFinPeriodo.Year, fechaFinPeriodo.Month, DateTime.DaysInMonth(fechaFinPeriodo.Year, fechaFinPeriodo.Month));
                                }
                            }
                            FLAG = true;
                        }
                        else
                        {
                            if (diasRestantes > periodo)
                            {
                                dias = (int)(fechaFinPeriodo - fechaInicioIncapacidad).TotalDays + 1;
                                incidencia2.dias = dias;
                                incidencia2.fechainicio = fechaInicioIncapacidad.Date;
                                incidencia2.fechafin = fechaFinPeriodo.Date;

                                fechaInicioIncapacidad = fechaFinPeriodo.AddDays(1);
                                if (periodo == 7)
                                    fechaFinPeriodo = fechaFinPeriodo.AddDays(periodo);
                                else
                                {
                                    if (fechaInicioIncapacidad.Day <= 15)
                                    {
                                        fechaFinPeriodo = fechaFinPeriodo.AddDays(periodo);
                                    }
                                    else
                                    {
                                        fechaFinPeriodo = new DateTime(fechaFinPeriodo.Year, fechaFinPeriodo.Month, DateTime.DaysInMonth(fechaFinPeriodo.Year, fechaFinPeriodo.Month));
                                    }
                                }
                            }
                        }
                                             
                        lstIncidencias.Add(incidencia2);
                    }
                    
                    diasRestantes = diasRestantes - dias;
                    i++;
                }
            }
            else
            {
                MessageBox.Show("El certificado que intenta guardar ya existe.", "Error");
                return;
            }

            switch (_tipoForma)
            {
                case 0://ALTA EN BASE DE DATOS

                        DataTable dt = new DataTable();
                        DataRow dtFila;
                        dt.Columns.Add("id", typeof(Int32));
                        dt.Columns.Add("idtrabajador", typeof(Int32));
                        dt.Columns.Add("idempresa", typeof(Int32));
                        dt.Columns.Add("dias", typeof(Int32));
                        dt.Columns.Add("certificado", typeof(String));
                        dt.Columns.Add("inicioincapacidad", typeof(DateTime));
                        dt.Columns.Add("finincapacidad", typeof(DateTime));
                        dt.Columns.Add("fechainicio", typeof(DateTime));
                        dt.Columns.Add("fechafin", typeof(DateTime));
                        dt.Columns.Add("periodoinicio", typeof(DateTime));
                        dt.Columns.Add("periodofin", typeof(DateTime));
                        dt.Columns.Add("idcontrol", typeof(Int32));
                        dt.Columns.Add("idincapacidad", typeof(Int32));

                        for (int i = 0; i < lstIncidencias.Count; i++)
                        {
                            dtFila = dt.NewRow();
                            dtFila["id"] = lstIncidencias[i].id;
                            dtFila["idtrabajador"] = lstIncidencias[i].idtrabajador;
                            dtFila["idempresa"] = lstIncidencias[i].idempresa;
                            dtFila["dias"] = lstIncidencias[i].dias;
                            dtFila["certificado"] = lstIncidencias[i].certificado;
                            dtFila["inicioincapacidad"] = lstIncidencias[i].inicioincapacidad;
                            dtFila["finincapacidad"] = lstIncidencias[i].finincapacidad;
                            dtFila["fechainicio"] = lstIncidencias[i].fechainicio;
                            dtFila["fechafin"] = lstIncidencias[i].fechafin;
                            dtFila["periodoinicio"] = lstIncidencias[i].periodoinicio;
                            dtFila["periodofin"] = lstIncidencias[i].periodofin;
                            dtFila["idcontrol"] = lstIncidencias[i].idcontrol;
                            dtFila["idincapacidad"] = lstIncidencias[i].idincapacidad;
                            dt.Rows.Add(dtFila);
                        }

                        try
                        {

                            cnx.Open();
                            ih.bulkIncidencia(dt, "tmpIncidencias");
                            ih.stpIncidencia(dtpInicioPeriodo.Value.Date, dtpFinPeriodo.Value.Date);
                            cnx.Close();
                            cnx.Dispose();

                            if (OnIncapacidad != null)
                                OnIncapacidad();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error al ingresar la incapacidad. \r\n \r\n Descripcion: " + error.Message, "Error");
                        }
                        this.Dispose();
                    break;

                //case 1://ALTA EN DATAGRIDVIEW
                //    try
                //    {
                //        cnx.Open();
                //        lstEmpleado = eh.obtenerEmpleado(empleado);
                //        cnx.Close();
                //        cnx.Dispose();

                //        if (OnIncapacidad != null)
                //            OnIncapacidad(lstEmpleado[0].noempleado, 
                //                lstEmpleado[0].nombres, 
                //                lstEmpleado[0].paterno, 
                //                lstEmpleado[0].materno, 
                //                int.Parse(txtDiasIncapacidad.Text), 
                //                dtpFechaInicio.Value, 
                //                dtpInicioPeriodo.Value, 
                //                dtpFinPeriodo.Value);
                //    }
                //    catch (Exception error)
                //    {
                //        MessageBox.Show("Error al ingresar la incapacidad. \r\n \r\n Descripcion: " + error.Message, "Error");
                //    }
                //    break;
            }
        }

        private void PeriodoFechaAplicacion()
        {
            if (periodo == 7)
            {
                DateTime dt = dtpFechaInicio.Value.Date;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                periodoInicio = dt;
                periodoFin = dt.AddDays(6);
            }
            else
            {
                if (dtpFechaInicio.Value.Day <= 15)
                {
                    periodoInicio = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
                    periodoFin = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 15);
                }
                else
                {
                    periodoInicio = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 16);
                    periodoFin = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, DateTime.DaysInMonth(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month));
                }

            }
        }

        private void txtCertificado_Leave(object sender, EventArgs e)
        {
            String cert = txtCertificado.Text;
            txtCertificado.Text = cert.ToUpper();
        } 
    }
}
