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
    public partial class frmSobreRecibo : Form
    {
        public frmSobreRecibo()
        {
            InitializeComponent();
        }

        #region VARIABLES GLOBALES
        SqlConnection cnx;
        SqlCommand cmd;
        string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
        int idTrabajador = 0;
        bool FLAGCALCULO = false, FLAGCARGA = false;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoNormalEspecial;
        public DateTime _inicioPeriodo, _finPeriodo;
        #endregion

        void b_OnBuscar(int id, string nombre)
        {
            idTrabajador = id;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Puestos.Core.PuestosHelper ph = new Puestos.Core.PuestosHelper();
            ph.Command = cmd;

            Departamento.Core.DeptoHelper dh = new Departamento.Core.DeptoHelper();
            dh.Command = cmd;

            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();
            List<Departamento.Core.Depto> lstDepartamento = new List<Departamento.Core.Depto>();
            List<Puestos.Core.Puestos> lstPuesto = new List<Puestos.Core.Puestos>();

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = id;

            Departamento.Core.Depto dpto = new Departamento.Core.Depto();
            dpto.idempresa = GLOBALES.IDEMPRESA;

            Puestos.Core.Puestos puesto = new Puestos.Core.Puestos();
            puesto.idempresa = GLOBALES.IDEMPRESA;

            try {
                cnx.Open();
                lstEmpleado = eh.obtenerEmpleado(empleado);
                lstDepartamento = dh.obtenerDepartamentos(dpto);
                lstPuesto = ph.obtenerPuestos(puesto);
                cnx.Close();
                cnx.Dispose();
            }
            catch
            {
                MessageBox.Show("Error al obtener el empleado.","Error");
            }

            var dato = from emp in lstEmpleado
                       join d in lstDepartamento on emp.iddepartamento equals d.id
                       join p in lstPuesto on emp.idpuesto equals p.id
                       select new { 
                           emp.noempleado,
                           emp.nombrecompleto,
                           emp.sueldo,
                           d.descripcion,
                           p.nombre
                       };
            foreach (var i in dato)
            {
                mtxtNoEmpleado.Text = i.noempleado;
                txtNombreCompleto.Text = i.nombrecompleto;
                txtDepartamento.Text = i.descripcion;
                txtPuesto.Text = i.nombre;
                txtSueldo.Text = "$ " + i.sueldo.ToString("#,##0.00");
            }

            dgvPercepciones.DataSource = null;
            dgvDeducciones.DataSource = null;
            txtPercepciones.Text = "$ 0.00";
            txtDeducciones.Text = "$ 0.00";
            txtNeto.Text = "$ 0.00";

            muestraDatos();
            muestraFaltas();
            muestraIncidencias();
            muestraProgramacion();
            muestraMovimientos();
            muestraInfonavit();
            muestraVacaciones();
        }

        private void toolCalcular_Click(object sender, EventArgs e)
        {
            string noConceptosPercepciones = "", noConceptosDeducciones = "";
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            List<CalculoNomina.Core.Nomina> lstConceptosPercepciones = new List<CalculoNomina.Core.Nomina>();
            List<CalculoNomina.Core.Nomina> lstConceptosDeducciones = new List<CalculoNomina.Core.Nomina>();

            List<CalculoNomina.Core.Nomina> lstConceptosPercepcionesModificados = new List<CalculoNomina.Core.Nomina>();
            List<CalculoNomina.Core.Nomina> lstConceptosDeduccionesModificados = new List<CalculoNomina.Core.Nomina>();

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            if (!FLAGCALCULO)
            {
                #region CONCEPTOS Y FORMULAS DEL TRABAJADOR (PERCEPCIONES)
                try
                {
                    cnx.Open();
                    lstConceptosPercepcionesModificados = nh.conceptosNominaTrabajador(GLOBALES.IDEMPRESA, "P", idTrabajador,
                        _tipoNormalEspecial, _inicioPeriodo.Date, _finPeriodo.Date);
                    lstConceptosDeduccionesModificados = nh.conceptosNominaTrabajador(GLOBALES.IDEMPRESA, "D", idTrabajador,
                        _tipoNormalEspecial, _inicioPeriodo.Date, _finPeriodo.Date);
                    cnx.Close();

                    if (lstConceptosPercepcionesModificados.Count != 0)
                    {
                        for (int i = 0; i < lstConceptosPercepcionesModificados.Count; i++)
                            if (lstConceptosPercepcionesModificados[i].modificado)
                                noConceptosPercepciones += lstConceptosPercepcionesModificados[i].noconcepto + ",";
                        noConceptosPercepciones = noConceptosPercepciones.Substring(0, noConceptosPercepciones.Length - 1);
                    }
                    else
                        noConceptosPercepciones = "";

                    if (lstConceptosDeduccionesModificados.Count != 0)
                    {
                        for (int i = 0; i < lstConceptosDeduccionesModificados.Count; i++)
                            if (lstConceptosDeduccionesModificados[i].modificado)
                                noConceptosDeducciones += lstConceptosDeduccionesModificados[i].noconcepto + ",";
                        noConceptosDeducciones = noConceptosDeducciones.Substring(0, noConceptosDeducciones.Length - 1);
                    }
                    else
                        noConceptosDeducciones = "";

                    cnx.Open();
                    lstConceptosPercepciones = nh.conceptosNominaTrabajador(GLOBALES.IDEMPRESA, "P", idTrabajador, noConceptosPercepciones);
                    lstConceptosDeducciones = nh.conceptosNominaTrabajador(GLOBALES.IDEMPRESA, "D", idTrabajador, noConceptosDeducciones);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al Obtener los conceptos del trabajador.\r\n \r\n La ventan se cerrara.", "Error");
                    this.Dispose();
                }
                #endregion

                #region CALCULO DE PERCEPCIONES
                List<CalculoNomina.Core.tmpPagoNomina> lstPercepciones = new List<CalculoNomina.Core.tmpPagoNomina>();
                lstPercepciones = CALCULOTRABAJADORES.PERCEPCIONES(lstConceptosPercepciones, _inicioPeriodo.Date, _finPeriodo.Date, _tipoNormalEspecial);
                #endregion

                #region BULK DATOS PERCEPCIONES
                BulkData(lstPercepciones);
                #endregion

                #region CALCULO DE DEDUCCIONES
                List<CalculoNomina.Core.tmpPagoNomina> lstDeducciones = new List<CalculoNomina.Core.tmpPagoNomina>();
                lstDeducciones = CALCULOTRABAJADORES.DEDUCCIONES(lstConceptosDeducciones, lstPercepciones, _inicioPeriodo.Date, _finPeriodo.Date, _tipoNormalEspecial);
                #endregion

                #region BULK DATOS DEDUCCIONES
                BulkData(lstDeducciones);
                #endregion

                #region PROGRAMACION DE MOVIMIENTOS
                List<CalculoNomina.Core.tmpPagoNomina> lstOtrasDeducciones = new List<CalculoNomina.Core.tmpPagoNomina>();

                double sueldo = lstPercepciones.Where(f => f.noconcepto == 1).Sum(f => f.cantidad);

                if (sueldo != 0)
                {
                    cnx = new SqlConnection(cdn);
                    cmd = new SqlCommand();
                    cmd.Connection = cnx;

                    int existe = 0;
                    ProgramacionConcepto.Core.ProgramacionHelper pch = new ProgramacionConcepto.Core.ProgramacionHelper();
                    pch.Command = cmd;

                    ProgramacionConcepto.Core.ProgramacionConcepto programacion = new ProgramacionConcepto.Core.ProgramacionConcepto();
                    programacion.idtrabajador = idTrabajador;

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
                            if (_finPeriodo.Date <= lstProgramacion[i].fechafin)
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

                                CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                                vn.idtrabajador = idTrabajador;
                                vn.idempresa = GLOBALES.IDEMPRESA;
                                vn.idconcepto = lstProgramacion[i].idconcepto;
                                vn.noconcepto = lstNoConcepto[0].noconcepto;
                                vn.tipoconcepto = lstNoConcepto[0].tipoconcepto;
                                vn.fechainicio = _inicioPeriodo.Date;
                                vn.fechafin = _finPeriodo.Date;
                                vn.exento = 0;
                                vn.gravado = 0;
                                vn.cantidad = lstProgramacion[i].cantidad;
                                vn.guardada = true;
                                vn.tiponomina = _tipoNormalEspecial;
                                vn.modificado = false;
                                lstOtrasDeducciones.Add(vn);
                            }
                        }
                    }
                }
                #endregion

                #region MOVIMIENTOS
                Movimientos.Core.MovimientosHelper mh = new Movimientos.Core.MovimientosHelper();
                mh.Command = cmd;

                sueldo = lstPercepciones.Where(f => f.noconcepto == 1).Sum(f => f.cantidad);

                if (sueldo != 0)
                {
                    int existe = 0;
                    Movimientos.Core.Movimientos mov = new Movimientos.Core.Movimientos();
                    mov.idtrabajador = idTrabajador;
                    mov.fechainicio = _inicioPeriodo.Date;
                    mov.fechafin = _finPeriodo.Date;

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
                            vn.idtrabajador = idTrabajador;
                            vn.idempresa = GLOBALES.IDEMPRESA;
                            vn.idconcepto = lstMovimiento[i].idconcepto;
                            vn.noconcepto = lstNoConcepto[0].noconcepto;
                            vn.tipoconcepto = lstNoConcepto[0].tipoconcepto;
                            vn.fechainicio = _inicioPeriodo.Date;
                            vn.fechafin = _finPeriodo.Date;
                            vn.exento = 0;
                            vn.gravado = 0;
                            vn.cantidad = lstMovimiento[i].cantidad;
                            vn.guardada = true;
                            vn.tiponomina = _tipoNormalEspecial;
                            vn.modificado = false;
                            lstOtrasDeducciones.Add(vn);
                        }
                    }
                }
                #endregion

                #region BULK DATOS PROGRAMACION DE MOVIMIENTOS
                BulkData(lstOtrasDeducciones);
                #endregion

                #region MOSTRAR DATOS
                muestraDatos();
                #endregion
            }
            else
            {
                #region CONCEPTOS Y FORMULAS DEL TRABAJADOR (PERCEPCIONES)
                try
                {
                    cnx.Open();
                    lstConceptosPercepciones = nh.conceptosNominaTrabajador(GLOBALES.IDEMPRESA, "P", idTrabajador, _tipoNormalEspecial, _inicioPeriodo, _finPeriodo);
                    lstConceptosDeducciones = nh.conceptosNominaTrabajador(GLOBALES.IDEMPRESA, "D", idTrabajador, _tipoNormalEspecial, _inicioPeriodo, _finPeriodo);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al obtener los conceptos del trabajador.\r\n \r\n La ventan se cerrara.", "Error");
                    this.Dispose();
                }
                #endregion

                #region RECALCULO DE PERCEPCIONES
                CALCULOTRABAJADORES.RECALCULO_PERCEPCIONES(lstConceptosPercepciones, _inicioPeriodo.Date, _finPeriodo.Date, _tipoNormalEspecial);
                #endregion

                #region CALCULO DE DEDUCCIONES
                List<CalculoNomina.Core.tmpPagoNomina> lstRecalculoPercepciones = new List<CalculoNomina.Core.tmpPagoNomina>();
                CalculoNomina.Core.tmpPagoNomina recalculopercepciones = new CalculoNomina.Core.tmpPagoNomina();
                recalculopercepciones.idempresa = GLOBALES.IDEMPRESA;
                recalculopercepciones.fechainicio = _inicioPeriodo.Date;
                recalculopercepciones.fechafin = _finPeriodo.Date;
                recalculopercepciones.tiponomina = _tipoNormalEspecial;
                recalculopercepciones.tipoconcepto = "P";
                recalculopercepciones.idtrabajador = idTrabajador;

                try
                {
                    cnx.Open();
                    lstRecalculoPercepciones = nh.obtenerPercepcionesTrabajador(recalculopercepciones);
                    cnx.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
                }
                CALCULOTRABAJADORES.RECALCULO_DEDUCCIONES(lstConceptosDeducciones, lstRecalculoPercepciones, _inicioPeriodo.Date, _finPeriodo.Date, _tipoNormalEspecial);
                #endregion

                #region PROGRAMACION DE MOVIMIENTOS
                
                ProgramacionConcepto.Core.ProgramacionHelper pch = new ProgramacionConcepto.Core.ProgramacionHelper();
                pch.Command = cmd;

                double sueldo = lstRecalculoPercepciones.Where(f => f.noconcepto == 1).Sum(f => f.cantidad);

                if (sueldo != 0)
                {
                    int existe = 0;
                    ProgramacionConcepto.Core.ProgramacionConcepto programacion = new ProgramacionConcepto.Core.ProgramacionConcepto();
                    programacion.idtrabajador = idTrabajador;

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
                            if (_finPeriodo.Date <= lstProgramacion[i].fechafin)
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

                                CalculoNomina.Core.tmpPagoNomina vn = new CalculoNomina.Core.tmpPagoNomina();
                                vn.idtrabajador = idTrabajador;
                                vn.idempresa = GLOBALES.IDEMPRESA;
                                vn.idconcepto = lstProgramacion[i].idconcepto;
                                vn.noconcepto = lstNoConcepto[0].noconcepto;
                                vn.tipoconcepto = lstNoConcepto[0].tipoconcepto;
                                vn.fechainicio = _inicioPeriodo.Date;
                                vn.fechafin = _finPeriodo.Date;
                                vn.exento = 0;
                                vn.gravado = 0;
                                vn.cantidad = lstProgramacion[i].cantidad;
                                vn.guardada = true;
                                vn.tiponomina = _tipoNormalEspecial;
                                vn.modificado = false;
                                cnx.Open();
                                nh.actualizaConcepto(vn);
                                cnx.Close();
                            }
                        }
                    }
                }
                #endregion

                #region MOVIMIENTOS
                Movimientos.Core.MovimientosHelper mh = new Movimientos.Core.MovimientosHelper();
                mh.Command = cmd;

                sueldo = lstRecalculoPercepciones.Where(f => f.noconcepto == 1).Sum(f => f.cantidad);

                if (sueldo != 0)
                {
                    int existe = 0;
                    Movimientos.Core.Movimientos mov = new Movimientos.Core.Movimientos();
                    mov.idtrabajador = idTrabajador;
                    mov.fechainicio = _inicioPeriodo.Date;
                    mov.fechafin = _finPeriodo.Date;

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
                            vn.idtrabajador = idTrabajador;
                            vn.idempresa = GLOBALES.IDEMPRESA;
                            vn.idconcepto = lstMovimiento[i].idconcepto;
                            vn.noconcepto = lstNoConcepto[0].noconcepto;
                            vn.tipoconcepto = lstNoConcepto[0].tipoconcepto;
                            vn.fechainicio = _inicioPeriodo.Date;
                            vn.fechafin = _finPeriodo.Date;
                            vn.exento = 0;
                            vn.gravado = 0;
                            vn.cantidad = lstMovimiento[i].cantidad;
                            vn.guardada = true;
                            vn.tiponomina = _tipoNormalEspecial;
                            vn.modificado = false;
                            cnx.Open();
                            nh.actualizaConcepto(vn);
                            cnx.Close();
                        }
                    }
                }
                #endregion

                #region MOSTRAR DATOS
                muestraDatos();
                #endregion
            }
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
            dt.Columns.Add("exento", typeof(Double));
            dt.Columns.Add("gravado", typeof(Double));
            dt.Columns.Add("cantidad", typeof(Double));
            dt.Columns.Add("fechainicio", typeof(DateTime));
            dt.Columns.Add("fechafin", typeof(DateTime));
            dt.Columns.Add("guardada", typeof(Boolean));
            dt.Columns.Add("tiponomina", typeof(Int32));
            dt.Columns.Add("modificado", typeof(Boolean));

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
                dtFila["guardada"] = lstValores[i].guardada;
                dtFila["tiponomina"] = lstValores[i].tiponomina;
                dtFila["modificado"] = lstValores[i].modificado;
                dt.Rows.Add(dtFila);
            }

            cnx = new SqlConnection(cdn);
            SqlBulkCopy bulk = new SqlBulkCopy(cnx);
            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
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

        private void muestraDatos()
        {
            
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pre = new CalculoNomina.Core.tmpPagoNomina();
            pre.idtrabajador = idTrabajador;
            pre.fechainicio = _inicioPeriodo;
            pre.fechafin = _finPeriodo;

            List<CalculoNomina.Core.tmpPagoNomina> lstRecibo = new List<CalculoNomina.Core.tmpPagoNomina>();
            
            try
            {
                cnx.Open();
                lstRecibo = nh.obtenerDatosRecibo(pre);
                cnx.Close();
                
            }
            catch {
                MessageBox.Show("Error: Al obtener la prenomina del trabajador. Se cerrará la ventana","Error");
                this.Dispose();
            }

            if (lstRecibo.Count != 0)
            {
                FLAGCALCULO = true;

                Conceptos.Core.ConceptosHelper conceptoh = new Conceptos.Core.ConceptosHelper();
                conceptoh.Command = cmd;

                Conceptos.Core.Conceptos c = new Conceptos.Core.Conceptos();
                c.idempresa = GLOBALES.IDEMPRESA;

                List<Conceptos.Core.Conceptos> lstConceptos = new List<Conceptos.Core.Conceptos>();

                try
                {
                    cnx.Open();
                    lstConceptos = conceptoh.obtenerConceptos(c);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Eror: \r\n \r\n" + error.Message, "Error");
                }

                var percepcion = from r in lstRecibo
                                 join co in lstConceptos on r.idconcepto equals co.id
                                 where co.tipoconcepto.Contains("P") && co.visible == true && r.cantidad != 0
                                 select new
                                 {
                                     NoConcepto = co.noconcepto,
                                     concepto = co.concepto,
                                     Importe = r.cantidad
                                 };

                var deduccion = from r in lstRecibo
                                join co in lstConceptos on r.idconcepto equals co.id
                                where co.tipoconcepto.Contains("D") && co.visible == true && r.cantidad != 0
                                select new
                                {
                                    NoConcepto = co.noconcepto,
                                    concepto = co.concepto,
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
                txtPercepciones.Text = "$ " + sumaPercepciones.ToString("#,##0.00");
                txtDeducciones.Text = "$ " + sumaDeducciones.ToString("#,##0.00");
                txtNeto.Text = "$ " + netoPagar.ToString("#,##0.00");
            }
            else
                FLAGCALCULO = false;
        }

        private void muestraFaltas()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
            fh.Command = cmd;

            Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
            falta.idempresa = GLOBALES.IDEMPRESA;
            falta.fechainicio = _inicioPeriodo.Date;
            falta.fechafin = _finPeriodo.Date;
            falta.idtrabajador = idTrabajador;

            List<Faltas.Core.Faltas> lstFaltas = new List<Faltas.Core.Faltas>();

            try
            {
                cnx.Open();
                lstFaltas = fh.obtenerFaltaTrabajador(falta);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            lstvFechasFalta.Clear();
            lstvFechasFalta.View = View.Details;
            lstvFechasFalta.GridLines = true;
            lstvFechasFalta.Columns.Add("Fecha", 70, HorizontalAlignment.Right);

            for (int i = 0; i < lstFaltas.Count; i++)
            {
                lstvFechasFalta.Items.Add(lstFaltas[i].fecha.ToShortDateString());
            }
        }

        private void muestraIncidencias()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Incidencias.Core.IncidenciasHelper ih = new Incidencias.Core.IncidenciasHelper();
            ih.Command = cmd;

            Incidencias.Core.Incidencias incidencia = new Incidencias.Core.Incidencias();
            incidencia.fechainicio = _inicioPeriodo.Date;
            incidencia.fechafin = _finPeriodo.Date;
            incidencia.idtrabajador = idTrabajador;

            List<Incidencias.Core.Incidencias> lstIncidencias = new List<Incidencias.Core.Incidencias>();

            try
            {
                cnx.Open();
                lstIncidencias = ih.obtenerIndicenciasTrabajador(incidencia);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            lstvIncidencias.Clear();
            lstvIncidencias.View = View.Details;
            lstvIncidencias.GridLines = true;
            lstvIncidencias.Columns.Add("Certificado", 70, HorizontalAlignment.Right);

            for (int i = 0; i < lstIncidencias.Count; i++)
            {
                lstvIncidencias.Items.Add(lstIncidencias[i].certificado);
            }
        }

        private void muestraProgramacion()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ProgramacionConcepto.Core.ProgramacionHelper pch = new ProgramacionConcepto.Core.ProgramacionHelper();
            pch.Command = cmd;

            Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Empleados.Core.EmpleadosHelper emph = new Empleados.Core.EmpleadosHelper();
            emph.Command = cmd;

            ProgramacionConcepto.Core.ProgramacionConcepto pc = new ProgramacionConcepto.Core.ProgramacionConcepto();
            pc.idtrabajador = idTrabajador;

            Conceptos.Core.Conceptos c = new Conceptos.Core.Conceptos();
            c.idempresa = GLOBALES.IDEMPRESA;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = idTrabajador;

            List<ProgramacionConcepto.Core.ProgramacionConcepto> lstProgramacionConcepto = new List<ProgramacionConcepto.Core.ProgramacionConcepto>();
            List<Conceptos.Core.Conceptos> lstConceptos = new List<Conceptos.Core.Conceptos>();
            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();

            try
            {
                cnx.Open();
                lstProgramacionConcepto = pch.obtenerProgramacion(pc);
                lstConceptos = ch.obtenerConceptos(c);
                lstEmpleado = emph.obtenerEmpleado(empleado);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            var prog = from p in lstProgramacionConcepto
                       join co in lstConceptos on p.idconcepto equals co.id
                       join em in lstEmpleado on p.idtrabajador equals em.idtrabajador
                       select new {
                           p.id,
                           p.idtrabajador,
                           em.noempleado,
                           em.nombrecompleto,
                           co.concepto,
                           p.cantidad,
                           p.fechafin
                       };

            dgvProgramacion.Columns["idpc"].DataPropertyName = "id";
            dgvProgramacion.Columns["idtrabajadorpc"].DataPropertyName = "idtrabajador";
            dgvProgramacion.Columns["noempleadopc"].DataPropertyName = "noempleado";
            dgvProgramacion.Columns["nombrepc"].DataPropertyName = "nombrecompleto";
            dgvProgramacion.Columns["conceptopc"].DataPropertyName = "concepto";
            dgvProgramacion.Columns["cantidadpc"].DataPropertyName = "cantidad";
            dgvProgramacion.Columns["fechafinpc"].DataPropertyName = "fechafin";
            dgvProgramacion.DataSource = prog.ToList();

            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.Alignment = DataGridViewContentAlignment.MiddleRight;
            estilo.Format = "C2";

            dgvProgramacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProgramacion.Columns[5].DefaultCellStyle = estilo;
        }

        private void muestraMovimientos()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Movimientos.Core.MovimientosHelper mh = new Movimientos.Core.MovimientosHelper();
            mh.Command = cmd;

            Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Empleados.Core.EmpleadosHelper emph = new Empleados.Core.EmpleadosHelper();
            emph.Command = cmd;

            Movimientos.Core.Movimientos m = new Movimientos.Core.Movimientos();
            m.idtrabajador = idTrabajador;

            Conceptos.Core.Conceptos c = new Conceptos.Core.Conceptos();
            c.idempresa = GLOBALES.IDEMPRESA;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = idTrabajador;

            List<Movimientos.Core.Movimientos> lstMovimientos = new List<Movimientos.Core.Movimientos>();
            List<Conceptos.Core.Conceptos> lstConceptos = new List<Conceptos.Core.Conceptos>();
            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();

            try
            {
                cnx.Open();
                lstMovimientos = mh.obtenerMovimientosTrabajador(m);
                lstConceptos = ch.obtenerConceptos(c);
                lstEmpleado = emph.obtenerEmpleado(empleado);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            var prog = from mov in lstMovimientos
                       join co in lstConceptos on mov.idconcepto equals co.id
                       join em in lstEmpleado on mov.idtrabajador equals em.idtrabajador
                       select new
                       {
                           mov.id,
                           mov.idtrabajador,
                           em.noempleado,
                           em.nombrecompleto,
                           co.concepto,
                           mov.cantidad,
                           mov.fechainicio,
                           mov.fechafin
                       };

            dgvMovimientos.Columns["idm"].DataPropertyName = "id";
            dgvMovimientos.Columns["idtrabajadorm"].DataPropertyName = "idtrabajador";
            dgvMovimientos.Columns["noempleadom"].DataPropertyName = "noempleado";
            dgvMovimientos.Columns["nombrem"].DataPropertyName = "nombrecompleto";
            dgvMovimientos.Columns["conceptom"].DataPropertyName = "concepto";
            dgvMovimientos.Columns["cantidadm"].DataPropertyName = "cantidad";
            dgvMovimientos.Columns["periodoiniciom"].DataPropertyName = "fechainicio";
            dgvMovimientos.Columns["periodofinm"].DataPropertyName = "fechafin";
            dgvMovimientos.DataSource = prog.ToList();

            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.Alignment = DataGridViewContentAlignment.MiddleRight;
            estilo.Format = "C2";

            dgvProgramacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProgramacion.Columns[5].DefaultCellStyle = estilo;
        }

        private void muestraInfonavit()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Infonavit.Core.InfonavitHelper ih = new Infonavit.Core.InfonavitHelper();
            ih.Command = cmd;

            Infonavit.Core.Infonavit inf = new Infonavit.Core.Infonavit();
            inf.idtrabajador = idTrabajador;

            List<Infonavit.Core.Infonavit> lstInfonavit = new List<Infonavit.Core.Infonavit>();

            try
            {
                cnx.Open();
                lstInfonavit = ih.obtenerInfonavitTrabajador(inf);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            lstvInfonavit.Clear();
            lstvInfonavit.View = View.Details;
            lstvInfonavit.GridLines = true;
            lstvInfonavit.Columns.Add("Crédito Infonavit", 100, HorizontalAlignment.Right);

            for (int i = 0; i < lstInfonavit.Count; i++)
            {
                lstvInfonavit.Items.Add(lstInfonavit[i].credito);
            }
        }

        private void muestraVacaciones()
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
            vh.Command = cmd;

            Vacaciones.Core.VacacionesPrima v = new Vacaciones.Core.VacacionesPrima();
            v.idtrabajador = idTrabajador;

            List<Vacaciones.Core.VacacionesPrima> lstVacacionesPrima = new List<Vacaciones.Core.VacacionesPrima>();

            try
            {
                cnx.Open();
                lstVacacionesPrima = vh.obtenerVacacionesPrimaTrabajador(v);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }

            lstvVacaciones.Clear();
            lstvVacaciones.View = View.Details;
            lstvVacaciones.GridLines = true;
            lstvVacaciones.Columns.Add("ID", 30, HorizontalAlignment.Right);
            lstvVacaciones.Columns.Add("Periodo Inicio", 80, HorizontalAlignment.Right);
            lstvVacaciones.Columns.Add("Periodo Fin", 80, HorizontalAlignment.Right);

            for (int i = 0; i < lstVacacionesPrima.Count; i++)
            {
                lstvVacaciones.Items.Add(lstVacacionesPrima[i].id.ToString());
                lstvVacaciones.Items[i].SubItems.Add(lstVacacionesPrima[i].periodoinicio.ToShortDateString());
                lstvVacaciones.Items[i].SubItems.Add(lstVacacionesPrima[i].periodofin.ToShortDateString());
            }
            
        }

        private void frmSobreRecibo_Load(object sender, EventArgs e)
        {
            dgvPercepciones.RowHeadersVisible = false;
            dgvPercepciones.ColumnHeadersVisible = false;

            dgvDeducciones.RowHeadersVisible = false;
            dgvDeducciones.ColumnHeadersVisible = false;
        }

        private void toolHoraExtra_Click(object sender, EventArgs e)
        {
            frmDiasAusentismo da = new frmDiasAusentismo();
            da.Text = "Horas Extras Dobles";
            da.lblTexto.Text = "Ingrese las horas extras dobles.";
            da.lblCantidad.Text = "Cantidad:";
            da.OnCantidad += da_OnCantidad;
            da.ShowDialog();
        }

        void da_OnCantidad(double cantidad)
        {
            if (cantidad == 0)
                return;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            concepto.noconcepto = 2;
            concepto.idempresa = GLOBALES.IDEMPRESA;

            cnx.Open();
            string formulaexento = ch.obtenerFormulaExento(concepto).ToString();
            cnx.Close();

            CalculoFormula cf = new CalculoFormula(idTrabajador, _inicioPeriodo, _finPeriodo, formulaexento);
            double exento = double.Parse(cf.calcularFormula().ToString());

            double gravado = 0;
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
            hora.idtrabajador = idTrabajador;
            hora.noconcepto = 2; //CONCEPTO HORAS EXTRAS DOBLES
            hora.fechainicio = _inicioPeriodo.Date;
            hora.fechafin = _finPeriodo.Date;
            hora.cantidad = cantidad;
            hora.exento = exento;
            hora.gravado = gravado;
            hora.modificado = true;
            try
            {
                cnx.Open();
                nh.actualizaHorasExtrasDespensa(hora);
                cnx.Close();
                cnx.Dispose();

                muestraDatos();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
        }

        private void toolAyudaDespensa_Click(object sender, EventArgs e)
        {
            frmDiasAusentismo da = new frmDiasAusentismo();
            da.Text = "Ayuda de Despensa";
            da.lblTexto.Text = "Ingrese la cantidad para la despensa.";
            da.lblCantidad.Text = "Cantidad:";
            da.OnDespensa += da_OnDespensa;
            da.ShowDialog();
        }

        void da_OnDespensa(double cantidad)
        {
            if (cantidad == 0)
                return;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina despensa = new CalculoNomina.Core.tmpPagoNomina();
            despensa.idempresa = GLOBALES.IDEMPRESA;
            despensa.idtrabajador = idTrabajador;
            despensa.noconcepto = 6; //CONCEPTO HORAS EXTRAS DOBLES
            despensa.fechainicio = _inicioPeriodo.Date;
            despensa.fechafin = _finPeriodo.Date;
            despensa.cantidad = cantidad;
            despensa.exento = 0;
            despensa.gravado = cantidad;
            despensa.modificado = true;
            try
            {
                cnx.Open();
                nh.actualizaHorasExtrasDespensa(despensa);
                cnx.Close();
                cnx.Dispose();

                muestraDatos();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
        }

        private void lstvFechasFalta_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
            fh.Command = cmd;

            List<Faltas.Core.Faltas> lstFalta = new List<Faltas.Core.Faltas>();

            if (lstvFechasFalta.SelectedItems.Count > 0)
            {
                ListViewItem listItem = lstvFechasFalta.SelectedItems[0];
                dtpFecha.Value = DateTime.Parse(listItem.Text);

                try
                {
                    cnx.Open();
                    lstFalta = fh.obtenerFalta(idTrabajador, GLOBALES.IDEMPRESA, dtpFecha.Value.Date);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch
                {
                    MessageBox.Show("Error al obtener la falta.","Error");
                    cnx.Dispose();
                }
            }

            for (int i = 0; i < lstFalta.Count; i++)
            {
                dtpFecha.Value = lstFalta[i].fecha;
                txtFalta.Text = lstFalta[i].faltas.ToString();
            }
            
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            frmBuscar b = new frmBuscar();
            b.OnBuscar += b_OnBuscar;
            b._catalogo = GLOBALES.EMPLEADOS;
            b._tipoNomina = GLOBALES.NORMAL;
            b.Show();
        }

        private void txtFalta_Leave(object sender, EventArgs e)
        {
            try
            {
                int.Parse(txtFalta.Text);
            }
            catch {
                MessageBox.Show("Error:  Solo números.", "Error");
                return;
            }

            if (int.Parse(txtFalta.Text) > 1 || int.Parse(txtFalta.Text) <= 0)
            {
                MessageBox.Show("Error: El valor ingreso debe ser 1.", "Error");
                return;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DateTime periodoInicio, periodoFin;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Empleados.Core.EmpleadosHelper emph = new Empleados.Core.EmpleadosHelper();
            emph.Command = cmd;

            int idperiodo = 0;
            try
            {
                cnx.Open();
                idperiodo = (int)emph.obtenerIdPeriodo(idTrabajador);
                cnx.Close();
            }
            catch
            {
                MessageBox.Show("Error: al obtener el Id del Periodo.","Error");
                cnx.Dispose();
                return;
            }

            Periodos.Core.PeriodosHelper ph = new Periodos.Core.PeriodosHelper();
            ph.Command = cmd;

            Periodos.Core.Periodos p = new Periodos.Core.Periodos();
            p.idperiodo = idperiodo;

            int periodo = 0;
            try
            {
                cnx.Open();
                periodo = (int)ph.DiasDePago(p);
                cnx.Close();
            }
            catch
            {
                MessageBox.Show("Error: al obtener los dias de pago.", "Error");
                cnx.Dispose();
                return;
            }

            if (periodo == 7)
            {
                DateTime dt = dtpFecha.Value.Date;
                while (dt.DayOfWeek != DayOfWeek.Monday) dt = dt.AddDays(-1);
                periodoInicio = dt;
                periodoFin = dt.AddDays(6);
            }
            else
            {
                if (dtpFecha.Value.Day <= 15)
                {
                    periodoInicio = new DateTime(dtpFecha.Value.Year, dtpFecha.Value.Month, 1);
                    periodoFin = new DateTime(dtpFecha.Value.Year, dtpFecha.Value.Month, 15);
                }
                else
                {
                    periodoInicio = new DateTime(dtpFecha.Value.Year, dtpFecha.Value.Month, 16);
                    periodoFin = new DateTime(dtpFecha.Value.Year, dtpFecha.Value.Month, DateTime.DaysInMonth(dtpFecha.Value.Year, dtpFecha.Value.Month));
                }
            }

            Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
            fh.Command = cmd;

            Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
            falta.idtrabajador = idTrabajador;
            falta.idempresa = GLOBALES.IDEMPRESA;
            falta.faltas = int.Parse(txtFalta.Text);
            falta.fechainicio = periodoInicio.Date;
            falta.fechafin = periodoFin.Date;
            falta.fecha = dtpFecha.Value.Date;
            falta.periodo = periodo;

            int existe = 0;
            try
            {
                cnx.Open();
                existe = (int)fh.existeFalta(idTrabajador, dtpFecha.Value.Date);
                cnx.Close();
            }
            catch 
            {
                MessageBox.Show("Error: Al verificar existencia de falta.","Error");
                cnx.Dispose();
            }

            if (existe != 0)
            {
                MessageBox.Show("Ya existe una falta con esa fecha.", "Error");
                return;
            }
            else
            {
                Incidencias.Core.IncidenciasHelper ih = new Incidencias.Core.IncidenciasHelper();
                ih.Command = cmd;

                try
                {
                    cnx.Open();
                    existe = (int)ih.existeIncidenciaEnFalta(idTrabajador, dtpFecha.Value.Date);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al guardar la falta.", "Error");
                    cnx.Dispose();
                    return;
                }

                if (existe == 0)
                    try
                    {
                        cnx.Open();
                        fh.insertaFalta(falta);
                        cnx.Close();
                        cnx.Dispose();
                        muestraFaltas();
                    }
                    catch
                    {
                        MessageBox.Show("Error: Al guardar la falta.", "Error");
                        cnx.Dispose();
                    }
                else
                    MessageBox.Show("La falta ingresada, se empalma con una incapacidad del trabajador.", "Error");
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
            fh.Command = cmd;

            try
            {
                cnx.Open();
                fh.eliminaFalta(idTrabajador, dtpFecha.Value.Date);
                cnx.Close();
                cnx.Dispose();
                muestraFaltas();
            }
            catch
            {
                MessageBox.Show("Error: Al eliminar la falta.", "Error");
                cnx.Dispose();
            }

        }

        private void lstvIncidencias_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Incidencias.Core.IncidenciasHelper ih = new Incidencias.Core.IncidenciasHelper();
            ih.Command = cmd;

            List<Incidencias.Core.Incidencias> lstIncidencia = new List<Incidencias.Core.Incidencias>();

            Incidencias.Core.Incidencias incidencia = new Incidencias.Core.Incidencias();
            incidencia.fechainicio = _inicioPeriodo;
            incidencia.fechafin = _finPeriodo;
            incidencia.idtrabajador = idTrabajador;

            if (lstvIncidencias.SelectedItems.Count > 0)
            {
                ListViewItem listItem = lstvIncidencias.SelectedItems[0];
                incidencia.certificado = listItem.Text;

                try
                {
                    cnx.Open();
                    lstIncidencia = ih.obtenerIndicenciaTrabajador(incidencia);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch
                {
                    MessageBox.Show("Error al obtener la incapacidad.", "Error");
                    cnx.Dispose();
                }
            }

            for (int i = 0; i < lstIncidencia.Count; i++)
            {
                txtDiasIncapacidad.Text = lstIncidencia[i].dias.ToString();
                dtpInicioInc.Value = lstIncidencia[i].inicioincapacidad;
                dtpFinInc.Value = lstIncidencia[i].finincapacidad;
                txtCertificado.Text = lstIncidencia[i].certificado;
            }
        }

        private void toolAgregarProgramacion_Click(object sender, EventArgs e)
        {
            frmProgramacionConcepto pc = new frmProgramacionConcepto();
            pc.OnProgramacion += pc_OnProgramacion;
            pc._idEmpleado = idTrabajador;
            pc._nombreEmpleado = txtNombreCompleto.Text;
            pc._tipoOperacion = GLOBALES.NUEVO;
            pc.Show();
        }

        void pc_OnProgramacion()
        {
            muestraProgramacion();
        }

        private void toolEliminarProgramacion_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            ProgramacionConcepto.Core.ProgramacionHelper pch = new ProgramacionConcepto.Core.ProgramacionHelper();
            pch.Command = cmd;

            int fila = dgvProgramacion.CurrentCell.RowIndex;
            ProgramacionConcepto.Core.ProgramacionConcepto pc = new ProgramacionConcepto.Core.ProgramacionConcepto();
            pc.id = int.Parse(dgvProgramacion.Rows[fila].Cells[0].Value.ToString());

            try{
                cnx.Open();
                pch.eliminaProgramacion(pc);
                cnx.Close();
                cnx.Dispose();
                muestraProgramacion();
            }
            catch
            {
                MessageBox.Show("Error: Al eliminar el concepto.","Error");
                cnx.Dispose();
                return;
            }
        }

        private void toolAgregarMovimiento_Click(object sender, EventArgs e)
        {
            frmMovimientos m = new frmMovimientos();
            m._ventana = "Movimiento";
            m.OnMovimientoNuevo += m_OnMovimientoNuevo;
            m._idEmpleado = idTrabajador;
            m._nombreEmpleado = txtNombreCompleto.Text;
            m.Show();
        }

        void m_OnMovimientoNuevo()
        {
            muestraMovimientos();
        }

        private void toolEliminarMovimiento_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Movimientos.Core.MovimientosHelper mh = new Movimientos.Core.MovimientosHelper();
            mh.Command = cmd;

            int fila = dgvMovimientos.CurrentCell.RowIndex;
            Movimientos.Core.Movimientos m = new Movimientos.Core.Movimientos();
            m.id = int.Parse(dgvMovimientos.Rows[fila].Cells[0].Value.ToString());

            try
            {
                cnx.Open();
                mh.eliminaMovimiento(m);
                cnx.Close();
                cnx.Dispose();
                muestraMovimientos();
            }
            catch
            {
                MessageBox.Show("Error: Al eliminar el movimiento.", "Error");
                cnx.Dispose();
                return;
            }
        }

        private void lstvInfonavit_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Infonavit.Core.InfonavitHelper ih = new Infonavit.Core.InfonavitHelper();
            ih.Command = cmd;

            List<Infonavit.Core.Infonavit> lstInfonavit = new List<Infonavit.Core.Infonavit>();

            Infonavit.Core.Infonavit infonavit = new Infonavit.Core.Infonavit();
            infonavit.idtrabajador = idTrabajador;

            if (lstvInfonavit.SelectedItems.Count > 0)
            {
                ListViewItem listItem = lstvInfonavit.SelectedItems[0];
                infonavit.credito = listItem.Text;

                try
                {
                    cnx.Open();
                    lstInfonavit = ih.obtenerInfonavitCredito(infonavit);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch
                {
                    MessageBox.Show("Error al obtener la informacion de Infonavit.", "Error");
                    cnx.Dispose();
                }
            }

            for (int i = 0; i < lstInfonavit.Count; i++)
            {
                chkActivo.Checked = lstInfonavit[i].activo;
                txtNumeroCredito.Text = lstInfonavit[i].credito;
                txtValor.Text = lstInfonavit[i].valordescuento.ToString();
                dtpFechaAplicacion.Value = lstInfonavit[i].fecha;
                if (lstInfonavit[i].descuento == GLOBALES.dPORCENTAJE)
                    rbtnPorcentaje.Checked = true;
                if (lstInfonavit[i].descuento == GLOBALES.dPESOS)
                    rbtnPesos.Checked = true;
                if (lstInfonavit[i].descuento == GLOBALES.dVSMDF)
                    rbtnVsmdf.Checked = true;
            }
        }

        private void lstvVacaciones_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
            vh.Command = cmd;

            List<Vacaciones.Core.VacacionesPrima> lstVacacionesPrima = new List<Vacaciones.Core.VacacionesPrima>();

            if (lstvVacaciones.SelectedItems.Count > 0)
            {
                ListViewItem listItem = lstvVacaciones.SelectedItems[0];
                try
                {
                    cnx.Open();
                    lstVacacionesPrima = vh.obtenerVacacionesPrimaTrabajador(int.Parse(listItem.Text), idTrabajador, DateTime.Parse(listItem.SubItems[1].Text), DateTime.Parse(listItem.SubItems[2].Text));
                    cnx.Close();
                    cnx.Dispose();
                }
                catch
                {
                    MessageBox.Show("Error al obtener la informacion de las vacaciones.", "Error");
                    cnx.Dispose();
                }
            }

            for (int i = 0; i < lstVacacionesPrima.Count; i++)
            {
                txtDiasPago.Text = lstVacacionesPrima[i].diaspago.ToString();
                txtDiasPendientes.Text = lstVacacionesPrima[i].diaspendientes.ToString();
                if (lstVacacionesPrima[i].vacacionesprima == "P")
                    cmbConceptoVacaciones.SelectedIndex = 0;
                else
                    cmbConceptoVacaciones.SelectedIndex = 1;
            }
        }

        private void btnEliminarVP_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
            vh.Command = cmd;

            Vacaciones.Core.VacacionesPrima vp = new Vacaciones.Core.VacacionesPrima();
            
            if (lstvVacaciones.SelectedItems.Count > 0)
            {
                ListViewItem listItem = lstvVacaciones.SelectedItems[0];
                vp.id = int.Parse(listItem.Text);
                vp.periodoinicio = DateTime.Parse(listItem.SubItems[1].Text);
                vp.periodofin = DateTime.Parse(listItem.SubItems[2].Text);
            }

            try
            {
                cnx.Open();
                vh.eliminaVacacion(vp);
                cnx.Close();
                cnx.Dispose();
                muestraVacaciones();

                txtDiasPago.Clear();
                txtDiasPendientes.Clear();
            }
            catch
            {
                MessageBox.Show("Error: Al eliminar el registro de vacacion.", "Error");
                cnx.Dispose();
            }

        }

        private void btnGuardarVP_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Empleados.Core.EmpleadosHelper emph = new Empleados.Core.EmpleadosHelper();
            emph.Command = cmd;

            Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
            vh.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = idTrabajador;

            List<Empleados.Core.Empleados> lstEmpleado = new List<Empleados.Core.Empleados>();

            try
            {
                cnx.Open();
                lstEmpleado = emph.obtenerEmpleado(empleado);
                cnx.Close();
            }
            catch
            {
                MessageBox.Show("Error: Al obtener la antigüedad del empleado.", "Error");
                cnx.Dispose();
                return;
            }

            Vacaciones.Core.DiasDerecho dd = new Vacaciones.Core.DiasDerecho();
            dd.anio = lstEmpleado[0].antiguedadmod;

            int dias = 0;
            try
            {
                cnx.Open();
                dias = (int)vh.diasDerecho(dd);
                cnx.Close();
            }
            catch
            {
                MessageBox.Show("Error: Al obtener los dias por derecho del empleado.", "Error");
                cnx.Dispose();
                return;
            }

            Vacaciones.Core.VacacionesPrima vp = new Vacaciones.Core.VacacionesPrima();
            vp.idtrabajador = idTrabajador;
            vp.periodoinicio = _inicioPeriodo;
            vp.periodofin = _finPeriodo;
            if (cmbConceptoVacaciones.SelectedIndex == 0)
                vp.vacacionesprima = "P";
            else
                vp.vacacionesprima = "V";
 
            int existe = 0;
            try
            {
                cnx.Open();
                existe = (int)vh.existeVacacionesPrima(vp);
                cnx.Close();
            }
            catch
            {
                MessageBox.Show("Error: Al obtener la existencia de vacaciones del empleado.", "Error");
                cnx.Dispose();
                return;
            }

            if (existe != 0)
            {
                MessageBox.Show("Error: Los datos a ingresar ya existen.", "Error");
                cnx.Dispose();
                return;
            }
            else
            {
                vp = new Vacaciones.Core.VacacionesPrima();
                vp.idtrabajador = idTrabajador;
                vp.idempresa = GLOBALES.IDEMPRESA;
                vp.periodoinicio = _inicioPeriodo;
                vp.periodofin = _finPeriodo;
                vp.diasderecho = dias;
                vp.diaspago = int.Parse(txtDiasPago.Text);
                vp.diaspendientes = dias - int.Parse(txtDiasPago.Text);
                vp.fechapago = DateTime.Now.Date;

                if (cmbConceptoVacaciones.SelectedIndex == 0)
                    vp.vacacionesprima = "P";
                else
                    vp.vacacionesprima = "V";

                try
                {
                    cnx.Open();
                    vh.insertaVacacion(vp);
                    cnx.Close();
                    cnx.Dispose();
                    muestraVacaciones();
                }
                catch
                {
                    MessageBox.Show("Error: Al ingresar el registro de vacacion.", "Error");
                    cnx.Dispose();
                    return;
                }
            }

        }
    }
}
