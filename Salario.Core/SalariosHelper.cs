using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Salario.Core
{
    public class SalariosHelper : Data.Obj.DataObj
    {
        public List<Salarios> obtenerSalarios()
        {
            DataTable dtSalarios = new DataTable();
            List<Salarios> lstSalario = new List<Salarios>();
            Command.CommandText = "select idsalario, periodo, valor from salariominimo";
            Command.Parameters.Clear();
            dtSalarios = SelectData(Command);
            for (int i = 0; i < dtSalarios.Rows.Count; i++)
            {
                Salarios s = new Salarios();
                s.idsalario = int.Parse(dtSalarios.Rows[i]["idsalario"].ToString());
                s.periodo = DateTime.Parse(dtSalarios.Rows[i]["periodo"].ToString());
                s.valor = decimal.Parse(dtSalarios.Rows[i]["valor"].ToString());
                lstSalario.Add(s);
            }
            return lstSalario;
        }

        public List<Salarios> obtenerSalario(Salarios salario)
        {
            DataTable dtSalarios = new DataTable();
            List<Salarios> lstSalario = new List<Salarios>();
            Command.CommandText = "select idsalario, periodo, valor from salariominimo where idsalario = @idsalario";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idsalario", salario.idsalario);
            dtSalarios = SelectData(Command);
            for (int i = 0; i < dtSalarios.Rows.Count; i++)
            {
                Salarios s = new Salarios();
                s.idsalario = int.Parse(dtSalarios.Rows[i]["idsalario"].ToString());
                s.periodo = DateTime.Parse(dtSalarios.Rows[i]["periodo"].ToString());
                s.valor = decimal.Parse(dtSalarios.Rows[i]["valor"].ToString());
                lstSalario.Add(s);
            }
            return lstSalario;
        }

        public int insertaSalario(Salarios s)
        {
            Command.CommandText = "insert into salariominimo (periodo, valor) values (@periodo, @valor)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("periodo", s.periodo);
            Command.Parameters.AddWithValue("valor", s.valor);
            return Command.ExecuteNonQuery();
        }

        public int actualizaSalario(Salarios s)
        {
            Command.CommandText = "update salariominimo set periodo = @periodo, valor = @valor where idsalario = @idsalario";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idsalario", s.idsalario);
            Command.Parameters.AddWithValue("periodo", s.periodo);
            Command.Parameters.AddWithValue("valor", s.valor);
            return Command.ExecuteNonQuery();
        }

        public int bajaSalario(Salarios s)
        {
            Command.CommandText = "delete from salariominimo where idsalario = @idsalario";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idsalario", s.idsalario);
            return Command.ExecuteNonQuery();
        }

    }
}
