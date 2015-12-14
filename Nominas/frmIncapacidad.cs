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
        int periodo, idperiodo;
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
                cnx.Dispose();
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
                            e.idperiodo
                        };
            foreach (var d in datos)
            {
                periodo = d.dias;
                idperiodo = d.idperiodo;
            }

            if (periodo == 7)
            {
                DateTime dt = DateTime.Now;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                dtpInicioPeriodo.Value = dt;
                dtpFinPeriodo.Value = dt.AddDays(6);
            }
            else
            {
                if (DateTime.Now.Day <= 15)
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

        private void frmIncapacidad_Load(object sender, EventArgs e)
        {
            dtpFinPeriodo.Enabled = false;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Catalogos.Core.CatalogosHelper ch = new Catalogos.Core.CatalogosHelper();
            ch.Command = cmd;

            Catalogos.Core.Catalogo catalogo = new Catalogos.Core.Catalogo();
            catalogo.grupodescripcion = "INCAPACIDAD";

            List<Catalogos.Core.Catalogo> lstControlIncapacidad = new List<Catalogos.Core.Catalogo>();
            List<Catalogos.Core.Catalogo> lstIncapacidad = new List<Catalogos.Core.Catalogo>();

            try
            {
                cnx.Open();
                lstIncapacidad = ch.obtenerGrupo(catalogo);
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
                DateTime fechaInicioIncapacidad = dtpFechaInicio.Value.Date;
                DateTime fechaFinIncapacidad = dtpFechaInicio.Value.AddDays(double.Parse(txtDiasIncapacidad.Text) - 1).Date;
                DateTime fechaFinPeriodo = dtpFinPeriodo.Value.Date;
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
                        incidencia2.periodoinicio = dtpInicioPeriodo.Value.Date;
                        incidencia2.periodofin = dtpFinPeriodo.Value.Date;
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
                        incidencia2.periodoinicio = dtpInicioPeriodo.Value.Date;
                        incidencia2.periodofin = dtpFinPeriodo.Value.Date;
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
                            ih.stpIncidencia();
                            cnx.Close();

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

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            if (periodo == 7)
            {
                DateTime dt = dtpInicioPeriodo.Value;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                dtpInicioPeriodo.Value = dt;
                dtpFinPeriodo.Value = dt.AddDays(6);
            }
            else
            {
                if (DateTime.Now.Day <= 15)
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
    }
}
