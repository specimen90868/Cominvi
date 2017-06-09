using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
        DateTime periodoInicioPosterior, periodoFinPosterior;
        #endregion

        #region VARIABLES PUBLICAS
        public int _tipoNormalEspecial;
        public DateTime _inicioPeriodo, _finPeriodo;
        public int _periodo;
        public bool _obracivil;
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
                       join p in lstPuesto on emp.idpuesto equals p.idpuesto
                       select new { 
                           emp.noempleado,
                           emp.nombrecompleto,
                           emp.sd,
                           d.descripcion,
                           p.nombre,
                           emp.fechaingreso
                       };
            foreach (var i in dato)
            {
                mtxtNoEmpleado.Text = i.noempleado;
                txtNombreCompleto.Text = i.nombrecompleto;
                txtDepartamento.Text = i.descripcion;
                txtPuesto.Text = i.nombre;
                txtSueldo.Text = "$ " + i.sd.ToString("#,##0.00");
                txtFechaIngreso.Text = i.fechaingreso.ToString("dd-MM-yyyy");
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
            int existeConcepto = 0;
            int estatus = 0;
            int existeAltaReingreso = 0;
            //string noConceptosPercepciones = "", noConceptosDeducciones = "";
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            List<CalculoNomina.Core.Nomina> lstConceptosPercepciones = new List<CalculoNomina.Core.Nomina>();
            List<CalculoNomina.Core.Nomina> lstConceptosDeducciones = new List<CalculoNomina.Core.Nomina>();

            List<CalculoNomina.Core.Nomina> lstConceptosPercepcionesModificados = new List<CalculoNomina.Core.Nomina>();
            List<CalculoNomina.Core.Nomina> lstConceptosDeduccionesModificados = new List<CalculoNomina.Core.Nomina>();

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            Empleados.Core.EmpleadosHelper eh = new Empleados.Core.EmpleadosHelper();
            eh.Command = cmd;

            Altas.Core.AltasHelper ah = new Altas.Core.AltasHelper();
            ah.Command = cmd;

            Reingreso.Core.ReingresoHelper rh = new Reingreso.Core.ReingresoHelper();
            rh.Command = cmd;

            if (_periodo == 7)
            {
                periodoInicioPosterior = _finPeriodo.AddDays(1);
                periodoFinPosterior = _finPeriodo.AddDays(7);
            }
            else
            {
                periodoInicioPosterior = _finPeriodo.AddDays(1);

                if (periodoInicioPosterior.Day <= 15)
                    periodoFinPosterior = _finPeriodo.AddDays(15);
                else
                    periodoFinPosterior = new DateTime(periodoInicioPosterior.Year, periodoInicioPosterior.Month,
                        DateTime.DaysInMonth(periodoInicioPosterior.Year, periodoInicioPosterior.Month));
            }

            try
            {
                cnx.Open();
                estatus = int.Parse(eh.obtenerEstatus(idTrabajador).ToString());
                cnx.Close();

                if (estatus == 0)
                {
                    cnx.Open();
                    nh.eliminaPreNomina(idTrabajador);
                    cnx.Close();
                 }
                else
                {

                    cnx.Open();
                    existeAltaReingreso = ah.existeAlta(GLOBALES.IDEMPRESA, idTrabajador, periodoInicioPosterior, periodoFinPosterior);
                    cnx.Close();
                    
                    if (existeAltaReingreso != 0)
                    {
                        cnx.Open();
                        nh.eliminaPreNomina(idTrabajador);
                        cnx.Close();
                        MessageBox.Show("INFORMACION:\r\n\r\nEl trabajador a calcular fue dado de alta con fecha posterior al calculo actual.\r\nNo se calcula el trabajador.", "Información");
                        return;
                    }

                    cnx.Open();
                    existeAltaReingreso = rh.existeReingreso(GLOBALES.IDEMPRESA, idTrabajador, periodoInicioPosterior, periodoFinPosterior);
                    cnx.Close();

                    if (existeAltaReingreso != 0)
                    {
                        cnx.Open();
                        nh.eliminaPreNomina(idTrabajador);
                        cnx.Close();
                        MessageBox.Show("INFORMACION:\r\n\r\nEl trabajador a calcular fue dado de alta con fecha posterior al calculo actual.\r\nNo se calcula el trabajador.", "Información");
                        return;
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al verificar el estatus del trabajador(ID): " + idTrabajador + "\r\n\r\n" + error.Message, "Error");
                cnx.Close();
            }

           
            #region CONCEPTOS Y FORMULAS DEL TRABAJADOR
            try
            {
                /*************************************************************
                    * Se obtienen los conceptos con el campo modificado en 1
                    * de la tabla tmpPagoNomina con el SP stp_DatosNominaRecalculoTrabajador
                    *************************************************************/
                //cnx.Open();
                //lstConceptosPercepcionesModificados = nh.conceptosNominaTrabajador(GLOBALES.IDEMPRESA, "P", idTrabajador,
                //    _tipoNormalEspecial, _inicioPeriodo.Date, _finPeriodo.Date);
                //lstConceptosDeduccionesModificados = nh.conceptosNominaTrabajador(GLOBALES.IDEMPRESA, "D", idTrabajador,
                //    _tipoNormalEspecial, _inicioPeriodo.Date, _finPeriodo.Date);
                //cnx.Close();
                /***************** TERMINA *****************************/


                /*********************************************************
                    * Se verifica si las listas de los conceptos modificados
                    * son diferentes de 0, si son diferentes, se acumulan en la varible
                    * noConceptosPercepciones y noConceptosDeducciones
                    *********************************************************/
                //if (lstConceptosPercepcionesModificados.Count != 0)
                //{
                //    for (int i = 0; i < lstConceptosPercepcionesModificados.Count; i++)
                //        if (lstConceptosPercepcionesModificados[i].modificado)
                //            noConceptosPercepciones += lstConceptosPercepcionesModificados[i].noconcepto + ",";
                //    if (noConceptosPercepciones != "")
                //        noConceptosPercepciones = noConceptosPercepciones.Substring(0, noConceptosPercepciones.Length - 1);
                //}
                //else
                //    noConceptosPercepciones = "";

                //if (lstConceptosDeduccionesModificados.Count != 0)
                //{
                //    for (int i = 0; i < lstConceptosDeduccionesModificados.Count; i++)
                //        if (lstConceptosDeduccionesModificados[i].modificado)
                //            noConceptosDeducciones += lstConceptosDeduccionesModificados[i].noconcepto + ",";
                //    if (noConceptosDeducciones != "")
                //        noConceptosDeducciones = noConceptosDeducciones.Substring(0, noConceptosDeducciones.Length - 1);
                //}
                //else
                //    noConceptosDeducciones = "";
                /************************TERMINA***************************/


                /*****************************************************************
                    * Se llenan las listas con los conceptos que no estan modificados
                    * con el SP stp_DatosNominaTrabajador para el calculo.
                    *****************************************************************/
                cnx.Open();
                nh.eliminaNominaTrabajador(idTrabajador, _inicioPeriodo, _finPeriodo, _tipoNormalEspecial);
                lstConceptosPercepciones = nh.conceptosNominaTrabajadorPercepciones(GLOBALES.IDEMPRESA, idTrabajador, _inicioPeriodo, _finPeriodo, _periodo);
                lstConceptosDeducciones = nh.conceptosNominaTrabajadorDeducciones(GLOBALES.IDEMPRESA, idTrabajador, _inicioPeriodo, _finPeriodo, _periodo);
                cnx.Close();
                /**************************TERMINA*********************************/
            }
            catch
            {
                MessageBox.Show("Error: Al Obtener los conceptos del trabajador.\r\n \r\n La ventan se cerrara.", "Error");
                this.Dispose();
            }
            #endregion

            #region CALCULO DE PERCEPCIONES
            List<CalculoNomina.Core.tmpPagoNomina> lstPercepciones = new List<CalculoNomina.Core.tmpPagoNomina>();
            lstPercepciones = CALCULO.PERCEPCIONES(lstConceptosPercepciones, _inicioPeriodo.Date, _finPeriodo.Date, _tipoNormalEspecial);
            #endregion

            #region BULK DATOS PERCEPCIONES
            BulkData(lstPercepciones);
            #endregion

            #region OBTENCION DE PERCEPCIONES
            lstPercepciones = new List<CalculoNomina.Core.tmpPagoNomina>();
            CalculoNomina.Core.tmpPagoNomina per = new CalculoNomina.Core.tmpPagoNomina();
            per.idempresa = GLOBALES.IDEMPRESA;
            per.idtrabajador = idTrabajador;
            per.fechainicio = _inicioPeriodo.Date;
            per.fechafin = _finPeriodo.Date;
            per.tipoconcepto = "P";
            per.tiponomina = _tipoNormalEspecial;
            cnx = new SqlConnection(cdn);
            cnx.Open();
            lstPercepciones = nh.obtenerPercepcionesTrabajador(per);
            cnx.Close();
            #endregion

            #region CALCULO DE DEDUCCIONES
            List<CalculoNomina.Core.tmpPagoNomina> lstDeducciones = new List<CalculoNomina.Core.tmpPagoNomina>();
            lstDeducciones = CALCULO.DEDUCCIONES(lstConceptosDeducciones, lstPercepciones, _inicioPeriodo.Date, _finPeriodo.Date, _tipoNormalEspecial);
            #endregion

            #region BULK DATOS DEDUCCIONES
            BulkData(lstDeducciones);
            #endregion

            #region PROGRAMACION DE MOVIMIENTOS
            List<CalculoNomina.Core.tmpPagoNomina> lstOtrasDeducciones = new List<CalculoNomina.Core.tmpPagoNomina>();

            decimal percepciones = lstPercepciones.Where(f => f.tipoconcepto == "P").Sum(f => f.cantidad);

            if (percepciones != 0)
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


                            CalculoNomina.Core.tmpPagoNomina pne = new CalculoNomina.Core.tmpPagoNomina();
                            pne.idempresa = GLOBALES.IDEMPRESA;
                            pne.idtrabajador = idTrabajador;
                            pne.fechainicio = _inicioPeriodo.Date;
                            pne.fechafin = _finPeriodo.Date;
                            pne.noconcepto = lstNoConcepto[0].noconcepto;

                            nh = new CalculoNomina.Core.NominaHelper();
                            nh.Command = cmd;
                            try
                            {
                                cnx.Open();
                                existeConcepto = (int)nh.existeConcepto(pne);
                                cnx.Close();
                            }
                            catch (Exception error)
                            {
                                MessageBox.Show("Error al obtener la existencia del concepto." + error.Message + "\r\n" + error.Source, "Error");
                                cnx.Dispose();
                            }

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

                            if (lstNoConcepto[0].gravado && !lstNoConcepto[0].exento)
                            {
                                vn.gravado = lstProgramacion[i].cantidad;
                                vn.exento = 0;
                            }

                            if (lstNoConcepto[0].gravado && lstNoConcepto[0].exento)
                            {
                                CalculoFormula formulaExcento = new CalculoFormula(idTrabajador, _inicioPeriodo.Date, _finPeriodo.Date, lstNoConcepto[0].formulaexento);
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
            else
            {
                decimal vacacion = lstPercepciones.Where(f => f.noconcepto == 7).Sum(f => f.cantidad);
                if (vacacion != 0)
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

                                CalculoNomina.Core.tmpPagoNomina pne = new CalculoNomina.Core.tmpPagoNomina();
                                pne.idempresa = GLOBALES.IDEMPRESA;
                                pne.idtrabajador = idTrabajador;
                                pne.fechainicio = _inicioPeriodo.Date;
                                pne.fechafin = _finPeriodo.Date;
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

                                if (lstNoConcepto[0].gravado && !lstNoConcepto[0].exento)
                                {
                                    vn.gravado = lstProgramacion[i].cantidad;
                                    vn.exento = 0;
                                }

                                if (lstNoConcepto[0].gravado && lstNoConcepto[0].exento)
                                {
                                    CalculoFormula formulaExcento = new CalculoFormula(idTrabajador, _inicioPeriodo.Date, _finPeriodo.Date, lstNoConcepto[0].formulaexento);
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
            }
            #endregion

            #region MOVIMIENTOS

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Movimientos.Core.MovimientosHelper mh = new Movimientos.Core.MovimientosHelper();
            mh.Command = cmd;
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            percepciones = lstPercepciones.Where(f => f.tipoconcepto == "P").Sum(f => f.cantidad);

            if (percepciones != 0)
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

                        if (lstNoConcepto[0].gravado && !lstNoConcepto[0].exento)
                        {
                            vn.gravado = lstMovimiento[i].cantidad;
                            vn.exento = 0;
                        }

                        if (lstNoConcepto[0].gravado && lstNoConcepto[0].exento)
                        {
                            CalculoFormula formulaExcento = new CalculoFormula(idTrabajador, _inicioPeriodo.Date, _finPeriodo.Date, lstNoConcepto[0].formulaexento);
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

                        try
                        {
                            cnx.Open();
                            existe = (int)nh.existeConcepto(vn);
                            cnx.Close();
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error: Al verificar existencia de los movimientos. \r\n" + error.Message, "Error");
                        }
                        

                        if (existe == 0)
                        {
                            lstOtrasDeducciones.Add(vn);
                        }
                        else
                        {
                            try
                            {
                                cnx.Open();
                                nh.actualizaConcepto(vn);
                                cnx.Close();
                            }
                            catch (Exception error)
                            {
                                MessageBox.Show("Error: Al actualizar los movimientos. " +  error.Message, "Error");
                            }
                            
                        }
                    }
                }
            }
            #endregion

            #region BULK DATOS PROGRAMACION DE MOVIMIENTOS
            BulkData(lstOtrasDeducciones);
            #endregion

            #region APLICACION DE DEPTOS/PUESTOS
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Aplicaciones.Core.AplicacionesHelper aplicah = new Aplicaciones.Core.AplicacionesHelper();
            aplicah.Command = cmd;
            Aplicaciones.Core.Aplicaciones aplicacion;
            List<Aplicaciones.Core.Aplicaciones> lstAplicacion = new List<Aplicaciones.Core.Aplicaciones>();
            aplicacion = new Aplicaciones.Core.Aplicaciones();
            aplicacion.idtrabajador = idTrabajador;
            aplicacion.periodoinicio = _inicioPeriodo.Date;
            aplicacion.periodofin = _finPeriodo.Date;
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
            empleado.idtrabajador = idTrabajador;
            List<Empleados.Core.Empleados> lstEmpleadoA = new List<Empleados.Core.Empleados>();
            cnx.Open();
            lstEmpleadoA = emph.obtenerEmpleado(empleado);
            cnx.Close();

            if (lstAplicacion.Count != 0)
            {
                for (int i = 0; i < lstAplicacion.Count; i++)
                {
                    if (lstAplicacion[i].periodoinicio == _inicioPeriodo.Date && lstAplicacion[i].periodofin == _finPeriodo.Date)
                    {
                        if (lstAplicacion.Count == 1)
                        {
                            if (lstAplicacion[i].deptopuesto == "D")
                            {
                                cnx.Open();
                                aplicah.aplica(idTrabajador, _inicioPeriodo.Date, _finPeriodo.Date, _periodo, "D", lstAplicacion[i].iddeptopuesto);
                                aplicah.aplica(idTrabajador, _inicioPeriodo.Date, _finPeriodo.Date, _periodo, "P", lstEmpleadoA[0].idpuesto);
                                cnx.Close();
                            }
                            else if (lstAplicacion[i].deptopuesto == "P")
                            {
                                cnx.Open();
                                aplicah.aplica(idTrabajador, _inicioPeriodo.Date, _finPeriodo.Date, _periodo, "D", lstEmpleadoA[0].iddepartamento);
                                aplicah.aplica(idTrabajador, _inicioPeriodo.Date, _finPeriodo.Date, _periodo, "P", lstAplicacion[i].iddeptopuesto);
                                cnx.Close();
                            }
                        }
                        else
                        {
                            if (lstAplicacion[i].deptopuesto == "D")
                            {
                                cnx.Open();
                                aplicah.aplica(idTrabajador, _inicioPeriodo.Date, _finPeriodo.Date, _periodo, "D", lstAplicacion[i].iddeptopuesto);
                                cnx.Close();
                            }
                            else if (lstAplicacion[i].deptopuesto == "P")
                            {
                                cnx.Open();
                                aplicah.aplica(idTrabajador, _inicioPeriodo.Date, _finPeriodo.Date, _periodo, "P", lstAplicacion[i].iddeptopuesto);
                                cnx.Close();
                            }
                        }
                    }
                }
            }
            else
            {
                cnx.Open();
                aplicah.aplica(idTrabajador, _inicioPeriodo.Date, _finPeriodo.Date, _periodo, "D", lstEmpleadoA[0].iddepartamento);
                aplicah.aplica(idTrabajador, _inicioPeriodo.Date, _finPeriodo.Date, _periodo, "P", lstEmpleadoA[0].idpuesto);
                cnx.Close();
            }
            #endregion

            #region MOSTRAR DATOS
            muestraDatos();
            #endregion

            #region PERIODO
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            object noPeriodo = 0;
            List<CalculoNomina.Core.PagoNomina> lstPagoNomina = new List<CalculoNomina.Core.PagoNomina>();
            try
            {
                if (_tipoNormalEspecial == GLOBALES.NORMAL)
                {
                    cnx.Open();
                    noPeriodo = int.Parse(nh.obtenerNoPeriodo(_periodo, _inicioPeriodo).ToString());
                    nh.actualizarNoPeriodo(GLOBALES.IDEMPRESA, _inicioPeriodo.Date, _finPeriodo.Date, int.Parse(noPeriodo.ToString()));
                    cnx.Close();
                }
                else if (_tipoNormalEspecial == GLOBALES.EXTRAORDINARIO_NORMAL)
                {
                    cnx.Open();
                    lstPagoNomina = nh.obtenerNoPeriodoExtraordinario(GLOBALES.IDEMPRESA, _tipoNormalEspecial, _periodo);
                    if (lstPagoNomina.Count == 0)
                        noPeriodo = 1;
                    else
                    {
                        if (lstPagoNomina[0].anio == _inicioPeriodo.Year)
                            noPeriodo = lstPagoNomina[0].noperiodo + 1;
                        else
                            noPeriodo = 1;
                    }
                    nh.actualizarNoPeriodo(GLOBALES.IDEMPRESA, _inicioPeriodo.Date, _finPeriodo.Date, int.Parse(noPeriodo.ToString()));
                    cnx.Close();
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al actualizar el No. de Periodo", "Error");
                cnx.Dispose();
                return;
            }
            #endregion
            
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
            dt.Columns.Add("anio", typeof(Int32));

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
                dtFila["anio"] = _inicioPeriodo.Year;
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

            #region PERIODO
            cmd = new SqlCommand();
            cmd.Connection = cnx;
            nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;
            int noPeriodo = 0;
            try
            {
                cnx.Open();
                noPeriodo = int.Parse(nh.obtenerNoPeriodo(_periodo, _inicioPeriodo).ToString());
                nh.actualizarNoPeriodo(GLOBALES.IDEMPRESA, _inicioPeriodo.Date, _finPeriodo.Date, noPeriodo);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Al actualizar el No. de Periodo", "Error");
                cnx.Dispose();
                return;
            }
            #endregion
        }

        private void muestraDatos()
        {
            dgvPercepciones.DataSource = null;
            dgvPercepciones.Columns.Clear();
            dgvDeducciones.DataSource = null;
            dgvDeducciones.Columns.Clear();

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            List<CalculoNomina.Core.tmpPagoNomina> lstReciboPercepciones = new List<CalculoNomina.Core.tmpPagoNomina>();
            List<CalculoNomina.Core.tmpPagoNomina> lstReciboDeducciones = new List<CalculoNomina.Core.tmpPagoNomina>();

            CalculoNomina.Core.tmpPagoNomina pnp = new CalculoNomina.Core.tmpPagoNomina();
            pnp.idtrabajador = idTrabajador;
            pnp.fechainicio = _inicioPeriodo.Date;
            pnp.fechafin = _finPeriodo.Date;
            

            CalculoNomina.Core.tmpPagoNomina pnd = new CalculoNomina.Core.tmpPagoNomina();
            pnd.idtrabajador = idTrabajador;
            pnd.fechainicio = _inicioPeriodo.Date;
            pnd.fechafin = _finPeriodo.Date;
            
            
            try
            {
                cnx.Open();
                lstReciboPercepciones = nh.obtenerDatosReciboPercepciones(pnp, _periodo);
                lstReciboDeducciones = nh.obtenerDatosReciboDeducciones(pnd, _periodo);
                cnx.Close();
                
            }
            catch {
                MessageBox.Show("Error: Al obtener la prenomina del trabajador. Se cerrará la ventana","Error");
                this.Dispose();
            }

            if (lstReciboPercepciones.Count != 0)
            {
                string formulaDiasAPagar = "[DiasLaborados]-[Faltas]-[DiasIncapacidad]";
                
                Conceptos.Core.ConceptosHelper conceptoh = new Conceptos.Core.ConceptosHelper();
                conceptoh.Command = cmd;

                Conceptos.Core.Conceptos c = new Conceptos.Core.Conceptos();
                c.idempresa = GLOBALES.IDEMPRESA;

                List<Conceptos.Core.Conceptos> lstConceptos = new List<Conceptos.Core.Conceptos>();

                CalculoFormula cf = new CalculoFormula(idTrabajador, _inicioPeriodo, _finPeriodo, formulaDiasAPagar);
                int diasAPagar = int.Parse(cf.calcularFormula().ToString());

                try
                {
                    cnx.Open();
                    lstConceptos = conceptoh.obtenerConceptos(c, _periodo);
                    cnx.Close();
                    cnx.Dispose();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: Al obtener lista de conceptos. (Función Muestra Datos.)\r\n \r\n" + error.Message, "Error");
                }

                var percepcion = from r in lstReciboPercepciones
                                 join co in lstConceptos on r.idconcepto equals co.id
                                 where co.visible == true && r.cantidad != 0
                                 select new
                                 {
                                     NoConcepto = co.noconcepto,
                                     Concepto = co.concepto,
                                     Importe = r.cantidad
                                 };

                var deduccion = from r in lstReciboDeducciones
                                join co in lstConceptos on r.idconcepto equals co.id
                                where co.visible == true && r.cantidad != 0
                                select new 
                                {
                                    NoConcepto = co.noconcepto,
                                    Concepto = co.concepto,
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

                decimal sumaPercepciones = 0, sumaDeducciones = 0, netoPagar = 0;
                decimal subsidio = lstReciboDeducciones.Where(d => d.noconcepto == 16).Sum(d => d.cantidad);
                foreach (DataGridViewRow fila in dgvPercepciones.Rows)
                {
                    sumaPercepciones += decimal.Parse(fila.Cells[2].Value.ToString());
                }

                foreach (DataGridViewRow fila in dgvDeducciones.Rows)
                {
                    sumaDeducciones += decimal.Parse(fila.Cells[2].Value.ToString());
                }

                sumaPercepciones += subsidio;
                sumaDeducciones = sumaDeducciones - subsidio;

                netoPagar = sumaPercepciones - sumaDeducciones;
                txtPercepciones.Text = "$ " + sumaPercepciones.ToString("#,##0.00");
                txtDeducciones.Text = "$ " + sumaDeducciones.ToString("#,##0.00");
                txtNeto.Text = "$ " + netoPagar.ToString("#,##0.00");

                dgvPercepciones.Columns.Add("dias", "dias");
                if (diasAPagar != 0)
                    dgvPercepciones.Rows[0].Cells[3].Value = diasAPagar.ToString() + " dias";
                if (netoPagar < 0)
                    MessageBox.Show("El neto a pagar es negativo: " + netoPagar.ToString("#,##0.00"), "Información");
            }
            
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
                MessageBox.Show("Error: Al obtener las Faltas. (Funcion Muestra Faltas.) \r\n \r\n" + error.Message, "Error");
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
                MessageBox.Show("Error: Al obtener las Incapacidades. (Función Muestra Incidencias.) \r\n \r\n" + error.Message, "Error");
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
                lstConceptos = ch.obtenerConceptos(c, _periodo);
                lstEmpleado = emph.obtenerEmpleado(empleado);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Al obtener las Programaciones. (Función Muestra Programación.)\r\n \r\n" + error.Message, "Error");
            }

            var prog = from p in lstProgramacionConcepto
                       join co in lstConceptos on p.idconcepto equals co.id
                       join em in lstEmpleado on p.idtrabajador equals em.idtrabajador
                       select new {
                           p.idprogramacion,
                           p.idtrabajador,
                           co.id,
                           em.noempleado,
                           em.nombrecompleto,
                           co.concepto,
                           p.cantidad,
                           p.fechafin
                       };

            dgvProgramacion.Columns["idpc"].DataPropertyName = "idprogramacion";
            dgvProgramacion.Columns["idtrabajadorpc"].DataPropertyName = "idtrabajador";
            dgvProgramacion.Columns["idconceptopc"].DataPropertyName = "id";
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
                lstConceptos = ch.obtenerConceptos(c, _periodo);
                lstEmpleado = emph.obtenerEmpleado(empleado);
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Al obtener los Movimientos. (Función Muestra Movimientos.) \r\n \r\n" + error.Message, "Error");
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
                MessageBox.Show("Error: Al obtener Infonavit. (Función Muestra Infonavit.)\r\n \r\n" + error.Message, "Error");
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

            Complementos.Core.ComplementoHelper comh = new Complementos.Core.ComplementoHelper();
            comh.Command = cmd;

            Vacaciones.Core.VacacionesPrima v = new Vacaciones.Core.VacacionesPrima();
            v.idtrabajador = idTrabajador;

            Complementos.Core.Complemento comp = new Complementos.Core.Complemento();
            comp.idtrabajador = idTrabajador;
 
            List<Vacaciones.Core.VacacionesPrima> lstVacacionesPrima = new List<Vacaciones.Core.VacacionesPrima>();

            try
            {
                cnx.Open();
                lstVacacionesPrima = vh.obtenerVacacionesPrimaTrabajador(v);
                txtObservaciones.Text = comh.obtenerObservacionesTrabajador(comp).ToString();
                cnx.Close();
                cnx.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: Al obtener las Vacaciones. (Función Muestra Vacaciones.) \r\n \r\n" + error.Message, "Error");
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

        void da_OnCantidad(decimal cantidad)
        {
 
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            Conceptos.Core.ConceptosHelper ch = new Conceptos.Core.ConceptosHelper();
            ch.Command = cmd;

            Conceptos.Core.Conceptos concepto = new Conceptos.Core.Conceptos();
            concepto.noconcepto = 2; //HORAS EXTRAS DOBLES
            concepto.idempresa = GLOBALES.IDEMPRESA;

            cnx.Open();
            string formulaexento = ch.obtenerFormulaExento(concepto).ToString();
            cnx.Close();

            CalculoFormula cf = new CalculoFormula(idTrabajador, _inicioPeriodo, _finPeriodo, formulaexento);
            decimal exento = decimal.Parse(cf.calcularFormula().ToString());

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

        void da_OnDespensa(decimal cantidad)
        {

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina despensa = new CalculoNomina.Core.tmpPagoNomina();
            despensa.idempresa = GLOBALES.IDEMPRESA;
            despensa.idtrabajador = idTrabajador;
            despensa.noconcepto = 6; //CONCEPTO DESPENSA
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
            b._tipoNomina = _tipoNormalEspecial;
            b._periodo = _periodo;
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

            //if (int.Parse(txtFalta.Text) > 1 || int.Parse(txtFalta.Text) <= 0)
            //{
            //    MessageBox.Show("Error: El valor ingreso debe ser 1.", "Error");
            //    return;
            //}
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DateTime periodoInicio, periodoFin;
            bool EsAlta = false, EsReingreso = false, EsBaja = false;

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            Empleados.Core.EmpleadosHelper emph = new Empleados.Core.EmpleadosHelper();
            emph.Command = cmd;

            Altas.Core.AltasHelper ah = new Altas.Core.AltasHelper();
            ah.Command = cmd;

            Reingreso.Core.ReingresoHelper rh = new Reingreso.Core.ReingresoHelper();
            rh.Command = cmd;

            Bajas.Core.BajasHelper bh = new Bajas.Core.BajasHelper();
            bh.Command = cmd;

            List<Altas.Core.Altas> lstAlta = new List<Altas.Core.Altas>();
            List<Reingreso.Core.Reingresos> lstReingreso = new List<Reingreso.Core.Reingresos>();
            List<Bajas.Core.Bajas> lstBaja = new List<Bajas.Core.Bajas>();

            Altas.Core.Altas alta = new Altas.Core.Altas();
            alta.idempresa = GLOBALES.IDEMPRESA;
            alta.idtrabajador = idTrabajador;
            alta.periodoInicio = _inicioPeriodo.Date;
            alta.periodoFin = _finPeriodo.Date;

            Reingreso.Core.Reingresos reingreso = new Reingreso.Core.Reingresos();
            reingreso.idempresa = GLOBALES.IDEMPRESA;
            reingreso.idtrabajador = idTrabajador;
            reingreso.periodoinicio = _inicioPeriodo.Date;
            reingreso.periodofin = _finPeriodo.Date;

            Bajas.Core.Bajas baja = new Bajas.Core.Bajas();
            baja.idempresa = GLOBALES.IDEMPRESA;
            baja.idtrabajador = idTrabajador;
            baja.periodoinicio = _inicioPeriodo.Date;
            baja.periodofin = _finPeriodo.Date;

            int idperiodo = 0;
            try
            {
                cnx.Open();
                idperiodo = (int)emph.obtenerIdPeriodo(idTrabajador);

                lstAlta = ah.obtenerAlta(alta);
                lstReingreso = rh.obtenerReingreso(reingreso);
                lstBaja = bh.obtenerBaja(baja);
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

            if (lstAlta.Count != 0)
                EsAlta = true;
            if (lstBaja.Count != 0)
                EsBaja = true;
            if (lstReingreso.Count != 0)
                EsReingreso = true;

            Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
            fh.Command = cmd;

            List<Faltas.Core.Faltas> lstFaltas = new List<Faltas.Core.Faltas>();

            int faltas = int.Parse(txtFalta.Text);
            if (int.Parse(txtFalta.Text) > 15)
                faltas = 15;

            for (int i = 0; i < faltas; i++)
            {
                int existe = 0;
                int existeVacacion = 0;
                try
                {
                    cnx.Open();
                    existe = (int)fh.existeFalta(idTrabajador, dtpFecha.Value.AddDays(i).Date);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al verificar existencia de falta.", "Error");
                    cnx.Dispose();
                    return;
                }

                if (existe != 0)
                {
                    MessageBox.Show("Ya existe una falta con esa fecha.", "Error");
                    //return;
                }
                else
                {
                    Incidencias.Core.IncidenciasHelper ih = new Incidencias.Core.IncidenciasHelper();
                    ih.Command = cmd;

                    Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
                    vh.Command = cmd;

                    bool FLAG_FALTAS = false;

                    if (EsAlta)
                    {
                        if (dtpFecha.Value.AddDays(i).Date < lstAlta[0].fechaingreso.Date)
                        {
                            MessageBox.Show("Error: Alta del empleado, Fecha de Ingreso = " + lstAlta[0].fechaingreso.Date.ToShortDateString() + "\r\n Fecha de la falta es menor.", "Error");
                            FLAG_FALTAS = true;
                        }
                        else
                            FLAG_FALTAS = false;
                    }

                    if (EsReingreso)
                    {
                        if (dtpFecha.Value.AddDays(i).Date < lstReingreso[0].fechaingreso.Date)
                        {
                            MessageBox.Show("Error: Alta del empleado, Fecha de Reingreso = " + lstReingreso[0].fechaingreso.Date.ToShortDateString() + "\r\n Fecha de la falta es menor.", "Error");
                            FLAG_FALTAS = true;
                        }
                        else
                            FLAG_FALTAS = false;
                    }

                    if (EsBaja)
                    {
                        if (dtpFecha.Value.AddDays(i).Date > lstBaja[0].fecha.Date)
                        {
                            MessageBox.Show("Error: Alta del empleado, Fecha de Reingreso = " + lstBaja[0].fecha.Date.ToShortDateString() + "\r\n Fecha de la falta es mayor.", "Error");
                            FLAG_FALTAS = true;
                        }
                        else
                            FLAG_FALTAS = false;
                    }

                    if (!FLAG_FALTAS)
                    {
                        try
                        {
                            cnx.Open();
                            existe = (int)ih.existeIncidenciaEnFalta(idTrabajador, dtpFecha.Value.AddDays(i).Date);
                            existeVacacion = (int)vh.existeVacacionEnFalta(idTrabajador, dtpFecha.Value.AddDays(i).Date);
                            cnx.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Error: Al guardar la falta.", "Error");
                            cnx.Dispose();
                            return;
                        }

                        if (existe == 0 && existeVacacion == 0)
                        {
                            Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
                            falta.idtrabajador = idTrabajador;
                            falta.idempresa = GLOBALES.IDEMPRESA;
                            falta.faltas = 1;
                            falta.fechainicio = periodoInicio.Date;
                            falta.fechafin = periodoFin.Date;
                            falta.fecha = dtpFecha.Value.AddDays(i).Date;
                            falta.periodo = periodo;
                            lstFaltas.Add(falta);
                        }
                        else
                            MessageBox.Show("La falta ingresada, se empalma con una incapacidad y/o dia de vacación del trabajador.",
                                "Error");
                    }
                    
                }
            }

            if (lstFaltas.Count != 0)
            {
                SqlBulkCopy bulk = new SqlBulkCopy(cnx);
                fh.bulkCommand = bulk;

                DataTable dtFalta = new DataTable();
                DataRow dtFilaFalta;
                dtFalta.Columns.Add("id", typeof(Int32));
                dtFalta.Columns.Add("idtrabajador", typeof(Int32));
                dtFalta.Columns.Add("idempresa", typeof(Int32));
                dtFalta.Columns.Add("periodo", typeof(Int32));
                dtFalta.Columns.Add("faltas", typeof(Int32));
                dtFalta.Columns.Add("fechainicio", typeof(DateTime));
                dtFalta.Columns.Add("fechafin", typeof(DateTime));
                dtFalta.Columns.Add("fecha", typeof(DateTime));

                for (int i = 0; i < lstFaltas.Count; i++)
                {
                    dtFilaFalta = dtFalta.NewRow();
                    dtFilaFalta["id"] = i + 1;
                    dtFilaFalta["idtrabajador"] = lstFaltas[i].idtrabajador;
                    dtFilaFalta["idempresa"] = lstFaltas[i].idempresa;
                    dtFilaFalta["periodo"] = lstFaltas[i].periodo;
                    dtFilaFalta["faltas"] = lstFaltas[i].faltas;
                    dtFilaFalta["fechainicio"] = lstFaltas[i].fechainicio;
                    dtFilaFalta["fechafin"] = lstFaltas[i].fechafin;
                    dtFilaFalta["fecha"] = lstFaltas[i].fecha;
                    dtFalta.Rows.Add(dtFilaFalta);
                }

                try
                {
                    cnx.Open();
                    fh.bulkFaltas(dtFalta, "tmpFaltas");
                    fh.stpFaltas();
                    cnx.Close();
                    cnx.Dispose();
                    muestraFaltas();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: \r\n \r\n" + error.Message + "\r\n \r\n Error Bulk Faltas.", "Error");
                }
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
            pc._periodo = _periodo;
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
            pc.idprogramacion = int.Parse(dgvProgramacion.Rows[fila].Cells["idpc"].Value.ToString());

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
            m.OnMovimientoNuevo += m_OnMovimientoNuevo;
            m._idEmpleado = idTrabajador;
            m._periodo = _periodo;
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
                if (lstVacacionesPrima[i].vacacionesprima == "P")
                {
                    txtDiasPagoPV.Text = lstVacacionesPrima[i].diaspago.ToString();
                    txtDiasPagoV.Text = "0";
                    cmbConceptoVacaciones.SelectedIndex = 1;
                }
                else
                {
                    txtDiasPagoV.Text = lstVacacionesPrima[i].diaspago.ToString();
                    txtDiasPagoPV.Text = "0";
                    dtpFechaInicioVacaciones.Value = lstVacacionesPrima[i].fechainicio;
                    cmbConceptoVacaciones.SelectedIndex = 0;
                }
                    
                txtDiasPendientes.Text = lstVacacionesPrima[i].diaspendientes.ToString();

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

                txtDiasPagoPV.Clear();
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

            Complementos.Core.ComplementoHelper ch = new Complementos.Core.ComplementoHelper();
            ch.Command = cmd;

            Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
            empleado.idtrabajador = idTrabajador;

            Complementos.Core.Complemento c = new Complementos.Core.Complemento();
            c.idtrabajador = idTrabajador;
            c.observaciones = txtObservaciones.Text;

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

            int existePrima = 0;
            int existeVacacion = 0;

            if (cmbConceptoVacaciones.SelectedIndex == 0) // PRIMA VACACIONAL Y VACACIONES
            {
                Vacaciones.Core.VacacionesPrima vp = new Vacaciones.Core.VacacionesPrima();
                vp.idtrabajador = idTrabajador;
                vp.periodoinicio = _inicioPeriodo;
                vp.periodofin = _finPeriodo;
                vp.vacacionesprima = "P";
               
                try
                {
                    cnx.Open();
                    existePrima = (int)vh.existeVacacionesPrima(vp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al obtener la existencia de prima vacacional del empleado.", "Error");
                    cnx.Dispose();
                    return;
                }

                vp = new Vacaciones.Core.VacacionesPrima();
                vp.idtrabajador = idTrabajador;
                vp.periodoinicio = _inicioPeriodo;
                vp.periodofin = _finPeriodo;
                vp.vacacionesprima = "V";

                try
                {
                    cnx.Open();
                    existeVacacion = (int)vh.existeVacacionesPrima(vp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al obtener la existencia de vacaciones del empleado.", "Error");
                    cnx.Dispose();
                    return;
                }

                //VALIDACION DE EXISTENCIA DE PRIMA VACACIONAL Y VACACIONES
                if (existePrima != 0 || existeVacacion != 0)
                {
                    MessageBox.Show("Error: Los datos a ingresar ya existen y/o existe la Prima Vacaciona o Vacaciones.", "Error");
                    cnx.Dispose();
                    return;
                }
                else
                {
                    Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
                    fh.Command = cmd;

                    Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
                    falta.idempresa = GLOBALES.IDEMPRESA;
                    falta.idtrabajador = idTrabajador;
                    falta.fechainicio = _inicioPeriodo.Date;
                    falta.fechafin = _finPeriodo.Date;

                    int existeFaltas = 0;
                    try
                    {
                        cnx.Open();
                        existeFaltas = (int)fh.existeFalta(falta);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: Al obtener las faltas del trabajador. \r\n" + error.Message, "Error");
                        cnx.Dispose();
                        return;
                    }

                    Incidencias.Core.IncidenciasHelper ih = new Incidencias.Core.IncidenciasHelper();
                    ih.Command = cmd;

                    int existeIncapacidad = 0;
                    try
                    {
                        cnx.Open();
                        existeIncapacidad = int.Parse(ih.diasIncidencia(idTrabajador, _inicioPeriodo.Date, _finPeriodo.Date).ToString());
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: Al obtener los dias de incidencia del trabajador. \r\n" + error.Message, "Error");
                        cnx.Dispose();
                        return;
                    }

                    int diasPagoReales = int.Parse(txtDiasPagoPV.Text) + existeFaltas + existeIncapacidad;
                    if (diasPagoReales > _periodo)
                    {
                        diasPagoReales = _periodo - existeFaltas - existeIncapacidad;
                        MessageBox.Show("Existen faltas del trabajador, se ajustaron las vacaciones a: " + diasPagoReales.ToString() + " dia(s).", "Información");
                    }
                    else
                    {
                        diasPagoReales = int.Parse(txtDiasPagoPV.Text);
                    }

                    vp = new Vacaciones.Core.VacacionesPrima();
                    vp.idtrabajador = idTrabajador;
                    vp.idempresa = GLOBALES.IDEMPRESA;
                    vp.periodoinicio = _inicioPeriodo;
                    vp.periodofin = _finPeriodo;
                    vp.diasderecho = dias;
                    vp.diaspago = diasPagoReales;
                    vp.diaspendientes = dias - diasPagoReales;
                    vp.fechapago = DateTime.Now.Date;
                    vp.fechainicio = DateTime.Now.Date;
                    vp.fechafin = DateTime.Now.Date;
                    vp.vacacionesprima = "P";

                    try
                    {
                        cnx.Open();
                        vh.insertaVacacion(vp);
                        cnx.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Error: Al ingresar el registro de prima vacacional.", "Error");
                        cnx.Dispose();
                        return;
                    }

                    diasPagoReales = int.Parse(txtDiasPagoV.Text) + existeFaltas + existeIncapacidad;
                    if (diasPagoReales > _periodo)
                    {
                        diasPagoReales = _periodo - existeFaltas - existeIncapacidad;
                        MessageBox.Show("Existen faltas del trabajador, se ajustaron las vacaciones a: " + diasPagoReales.ToString() + " dia(s).", "Información");
                    }
                    else
                    {
                        diasPagoReales = int.Parse(txtDiasPagoV.Text);
                    }

                    vp = new Vacaciones.Core.VacacionesPrima();
                    vp.idtrabajador = idTrabajador;
                    vp.idempresa = GLOBALES.IDEMPRESA;
                    vp.periodoinicio = _inicioPeriodo;
                    vp.periodofin = _finPeriodo;
                    vp.diasderecho = dias;
                    vp.diaspago = diasPagoReales;
                    vp.diaspendientes = dias - diasPagoReales;
                    vp.fechapago = DateTime.Now.Date;
                    vp.fechainicio = dtpFechaInicioVacaciones.Value.Date;
                    vp.fechafin = dtpFechaInicioVacaciones.Value.AddDays(diasPagoReales - 1);
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
                        MessageBox.Show("Error: Al ingresar el registro de prima vacacional.", "Error");
                        cnx.Dispose();
                        return;
                    }
                }
            }
            else // SOLO PRIMA VACACIONAL
            {
                Vacaciones.Core.VacacionesPrima vp = new Vacaciones.Core.VacacionesPrima();
                vp.idtrabajador = idTrabajador;
                vp.periodoinicio = _inicioPeriodo;
                vp.periodofin = _finPeriodo;
                vp.vacacionesprima = "P";

                try
                {
                    cnx.Open();
                    existePrima = (int)vh.existeVacacionesPrima(vp);
                    cnx.Close();
                }
                catch
                {
                    MessageBox.Show("Error: Al obtener la existencia de vacaciones del empleado.", "Error");
                    cnx.Dispose();
                    return;
                }

                //VALIDACION DE EXISTENCIA DE PRIMA VACACIONAL
                if (existePrima != 0)
                {
                    MessageBox.Show("Error: Los datos a ingresar ya existen.", "Error");
                    cnx.Dispose();
                    return;
                }
                else
                {
                    Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
                    fh.Command = cmd;

                    Faltas.Core.Faltas falta = new Faltas.Core.Faltas();
                    falta.idempresa = GLOBALES.IDEMPRESA;
                    falta.idtrabajador = idTrabajador;
                    falta.fechainicio = _inicioPeriodo.Date;
                    falta.fechafin = _finPeriodo.Date;

                    int existeFaltas = 0;
                    try
                    {
                        cnx.Open();
                        existeFaltas = (int)fh.existeFalta(falta);
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: Al obtener las faltas del trabajador. \r\n" + error.Message, "Error");
                        cnx.Dispose();
                        return;
                    }

                    Incidencias.Core.IncidenciasHelper ih = new Incidencias.Core.IncidenciasHelper();
                    ih.Command = cmd;

                    int existeIncapacidad = 0;
                    try
                    {
                        cnx.Open();
                        existeIncapacidad = int.Parse(ih.diasIncidencia(idTrabajador, _inicioPeriodo.Date, _finPeriodo.Date).ToString());
                        cnx.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error: Al obtener los dias de incidencia del trabajador. \r\n" + error.Message, "Error");
                        cnx.Dispose();
                        return;
                    }

                    int diasPagoReales = int.Parse(txtDiasPagoPV.Text) + existeFaltas + existeIncapacidad;
                    if (diasPagoReales > _periodo)
                    {
                        diasPagoReales = _periodo - existeFaltas - existeIncapacidad;
                        MessageBox.Show("Existen faltas del trabajador, se ajustaron las vacaciones a: " + diasPagoReales.ToString() + " dia(s).", "Información");
                    }
                    else
                    {
                        diasPagoReales = int.Parse(txtDiasPagoPV.Text);
                    }

                    vp = new Vacaciones.Core.VacacionesPrima();
                    vp.idtrabajador = idTrabajador;
                    vp.idempresa = GLOBALES.IDEMPRESA;
                    vp.periodoinicio = _inicioPeriodo;
                    vp.periodofin = _finPeriodo;
                    vp.diasderecho = dias;
                    vp.diaspago = diasPagoReales;
                    vp.diaspendientes = dias - diasPagoReales;
                    vp.fechapago = DateTime.Now.Date;
                    vp.fechainicio = DateTime.Now.Date;
                    vp.fechafin = DateTime.Now.Date;
                    vp.vacacionesprima = "P";

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
                        MessageBox.Show("Error: Al ingresar el registro de prima vacacional.", "Error");
                        cnx.Dispose();
                        return;
                    }
                }
            }
        }

        private void toolEliminar_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina pn = new CalculoNomina.Core.tmpPagoNomina();
            pn.idempresa = GLOBALES.IDEMPRESA;
            pn.idtrabajador = idTrabajador;
            pn.fechainicio = _inicioPeriodo.Date;
            pn.fechafin = _finPeriodo.Date;

            try{
                cnx.Open();
                nh.eliminaCalculo(pn);
                cnx.Close();

                dgvPercepciones.DataSource = null;
                dgvDeducciones.DataSource = null;
                dgvPercepciones.Columns.Clear();
                dgvDeducciones.Columns.Clear();
            }
            catch{
                MessageBox.Show("Error: Al eliminar los datos de la nomina.", "Error");
                this.Dispose();
            }
        }

        private void btnGuardarObservaciones_Click(object sender, EventArgs e)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;
         
            Complementos.Core.ComplementoHelper ch = new Complementos.Core.ComplementoHelper();
            ch.Command = cmd;

            Complementos.Core.Complemento c = new Complementos.Core.Complemento();
            c.idtrabajador = idTrabajador;
            c.observaciones = txtObservaciones.Text;

            try
            {
                cnx.Open();
                ch.actualizaObservacionesTrabajador(c);
                cnx.Close();
                cnx.Dispose();
            }
            catch
            {
                MessageBox.Show("Error: Al actualizar las observaciones.", "Error");
                cnx.Dispose();
                return;
            }
        }

        private void cmbConceptoVacaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConceptoVacaciones.Text == "Prima Vacacional y Vacaciones")
            {
                dtpFechaInicioVacaciones.Enabled = true;
                txtDiasPagoPV.Enabled = true;
                txtDiasPagoV.Enabled = true;
            }
            else if (cmbConceptoVacaciones.Text == "Prima Vacacional")
            {
                dtpFechaInicioVacaciones.Enabled = false;
                txtDiasPagoPV.Enabled = true;
                txtDiasPagoV.Enabled = false;
            }
        }

        private void frmSobreRecibo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F3)
            {
                btnBuscar_Click_1(sender, e);
            }
        }

        private void toolSubsidio_Click(object sender, EventArgs e)
        {
            frmDiasAusentismo da = new frmDiasAusentismo();
            da.Text = "Subsidio";
            da.lblTexto.Text = "Ingrese la cantidad para el subsidio.";
            da.lblCantidad.Text = "Cantidad:";
            da.OnSubsidio += da_OnSubsidio;
            da.ShowDialog();
        }

        void da_OnSubsidio(decimal cantidad)
        {

            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina subsidio = new CalculoNomina.Core.tmpPagoNomina();
            subsidio.idempresa = GLOBALES.IDEMPRESA;
            subsidio.idtrabajador = idTrabajador;
            subsidio.noconcepto = 16; //CONCEPTO SUBSIDIO
            subsidio.fechainicio = _inicioPeriodo.Date;
            subsidio.fechafin = _finPeriodo.Date;
            subsidio.cantidad = cantidad;
            subsidio.exento = 0;
            subsidio.gravado = 0;
            subsidio.modificado = true;
            try
            {
                cnx.Open();
                nh.actualizaHorasExtrasDespensa(subsidio);
                cnx.Close();
                cnx.Dispose();

                muestraDatos();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
        }

        private void toolIspt_Click(object sender, EventArgs e)
        {
            frmDiasAusentismo da = new frmDiasAusentismo();
            da.Text = "I.S.R.";
            da.lblTexto.Text = "Ingrese la cantidad para el I.S.R.";
            da.lblCantidad.Text = "Cantidad:";
            da.OnIsr += da_OnIsr;
            da.ShowDialog();
        }

        private void da_OnIsr(decimal cantidad) 
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina isr = new CalculoNomina.Core.tmpPagoNomina();
            isr.idempresa = GLOBALES.IDEMPRESA;
            isr.idtrabajador = idTrabajador;
            isr.noconcepto = 17; //CONCEPTO ISR
            isr.fechainicio = _inicioPeriodo.Date;
            isr.fechafin = _finPeriodo.Date;
            isr.cantidad = cantidad;
            isr.exento = 0;
            isr.gravado = 0;
            isr.modificado = true;
            try
            {
                cnx.Open();
                nh.actualizaHorasExtrasDespensa(isr);
                cnx.Close();
                cnx.Dispose();

                muestraDatos();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
        }

        private void toolInfonavit_Click(object sender, EventArgs e)
        {
            frmDiasAusentismo da = new frmDiasAusentismo();
            da.Text = "Infonavit";
            da.lblTexto.Text = "Ingrese la cantidad para el infonavit.";
            da.lblCantidad.Text = "Cantidad:";
            da.OnInfonavit += da_OnInfonavit;
            da.ShowDialog();
        }

        void da_OnInfonavit(decimal cantidad)
        {
            cnx = new SqlConnection(cdn);
            cmd = new SqlCommand();
            cmd.Connection = cnx;

            CalculoNomina.Core.NominaHelper nh = new CalculoNomina.Core.NominaHelper();
            nh.Command = cmd;

            CalculoNomina.Core.tmpPagoNomina infonavit = new CalculoNomina.Core.tmpPagoNomina();
            infonavit.idempresa = GLOBALES.IDEMPRESA;
            infonavit.idtrabajador = idTrabajador;
            infonavit.noconcepto = 9; //CONCEPTO SUBSIDIO
            infonavit.fechainicio = _inicioPeriodo.Date;
            infonavit.fechafin = _finPeriodo.Date;
            infonavit.cantidad = cantidad;
            infonavit.exento = 0;
            infonavit.gravado = 0;
            infonavit.modificado = true;

            try
            {
                cnx.Open();
                nh.actualizaHorasExtrasDespensa(infonavit);
                cnx.Close();
                cnx.Dispose();

                muestraDatos();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: \r\n \r\n" + error.Message, "Error");
            }
        }

        private void toolEditar_Click(object sender, EventArgs e)
        {
            int fila = dgvProgramacion.CurrentCell.RowIndex;
            frmProgramacionConcepto pc = new frmProgramacionConcepto();
            pc.OnProgramacion += pc_OnProgramacion;
            pc._idEmpleado = idTrabajador;
            pc._nombreEmpleado = txtNombreCompleto.Text;
            pc._tipoOperacion = GLOBALES.MODIFICAR;
            pc._periodo = _periodo;
            pc._idprogramacion = int.Parse(dgvProgramacion.Rows[fila].Cells[0].Value.ToString());
            pc.Show();
        }

        private void toolDiagnostico_Click(object sender, EventArgs e)
        {
            frmVisorReportes vr = new frmVisorReportes();
            vr._noReporte = 11;
            vr._inicioPeriodo = _inicioPeriodo;
            vr._empleadoInicio = idTrabajador;
            vr.Show();
        }

        private void frmSobreRecibo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mtxtNoEmpleado.Text != "")
            {
                cnx.Dispose();
                cmd.Dispose();
            }
            
        }

        private void contextEliminar_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in lstvFechasFalta.SelectedItems)
            {
                cnx = new SqlConnection(cdn);
                cmd = new SqlCommand();
                cmd.Connection = cnx;

                Faltas.Core.FaltasHelper fh = new Faltas.Core.FaltasHelper();
                fh.Command = cmd;

                try
                {
                    cnx.Open();
                    fh.eliminaFalta(idTrabajador, DateTime.Parse(i.Text).Date);
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
        }

    }
}
