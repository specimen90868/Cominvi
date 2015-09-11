using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nominas
{
    public class FormulasValores
    {
        private string variable;
        private List<Empleados.Core.Empleados> empleados;

        public FormulasValores(string _dato, List<Empleados.Core.Empleados> _empleados)
        {
            variable = _dato;
            empleados = _empleados;
        }

        public List<string> ObtenerValorVariable()
        {
            string cdn = ConfigurationManager.ConnectionStrings["cdnNomina"].ConnectionString;
            SqlConnection cnx = new SqlConnection(cdn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            List<string> valores = new List<string>();
            
            switch(variable)
            {
                case "SueldoDiario":
                    List<Empleados.Core.Empleados> lstEmpleado;
                    Empleados.Core.EmpleadosHelper empleadohp = new Empleados.Core.EmpleadosHelper();
                    empleadohp.Command = cmd;
                    for (int i = 0; i < empleados.Count; i++)
                    {
                        Empleados.Core.Empleados empleado = new Empleados.Core.Empleados();
                        empleado.idtrabajador = empleados[i].idtrabajador;
                        cnx.Open();
                        lstEmpleado = empleadohp.obtenerEmpleado(empleado);
                        cnx.Close();
                        valores.Add(lstEmpleado[0].sd.ToString());
                    }
                    cnx.Dispose();
                    break;
                case "SBC": 
                    break;
                case "DiasDerechoV":
                    Vacaciones.Core.VacacionesHelper vh = new Vacaciones.Core.VacacionesHelper();
                    vh.Command = cmd;
                    for (int i = 0; i < empleados.Count; i++)
                    {
                        Vacaciones.Core.DiasDerecho diasDerecho = new Vacaciones.Core.DiasDerecho();
                        diasDerecho.anio = empleados[i].antiguedadmod;
                        cnx.Open();
                        valores.Add(vh.diasDerecho(diasDerecho).ToString());
                        cnx.Close();
                    }
                    cnx.Dispose();
                    break;
                case "SalarioMinimo":
                    List<Salario.Core.Salarios> salarios = new List<Salario.Core.Salarios>();
                    Salario.Core.SalariosHelper sh = new Salario.Core.SalariosHelper();
                    sh.Command = cmd;
                    for (int i = 0; i < empleados.Count; i++)
                    {
                        cnx.Open();
                        salarios = sh.obtenerSalario(new DateTime(DateTime.Now.Year, 1, 1), empleados[i].idsalario);
                        valores.Add(salarios[0].valor.ToString());
                        cnx.Close();
                    }
                    cnx.Dispose();
                    break;
            }
            return valores;
        }
    }
}
